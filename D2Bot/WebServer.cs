using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using D2Bot.Properties;
using Newtonsoft.Json;

namespace D2Bot;

public class WebServer
{
	private static readonly string[] indexFiles = new string[4] { "index.html", "index.htm", "default.html", "default.htm" };

	private static readonly Dictionary<string, string> mimeTypes = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase)
	{
		{ ".asf", "video/x-ms-asf" },
		{ ".asx", "video/x-ms-asf" },
		{ ".avi", "video/x-msvideo" },
		{ ".bin", "application/octet-stream" },
		{ ".cco", "application/x-cocoa" },
		{ ".crt", "application/x-x509-ca-cert" },
		{ ".css", "text/css" },
		{ ".deb", "application/octet-stream" },
		{ ".der", "application/x-x509-ca-cert" },
		{ ".dll", "application/octet-stream" },
		{ ".dmg", "application/octet-stream" },
		{ ".ear", "application/java-archive" },
		{ ".eot", "application/octet-stream" },
		{ ".exe", "application/octet-stream" },
		{ ".flv", "video/x-flv" },
		{ ".gif", "image/gif" },
		{ ".hqx", "application/mac-binhex40" },
		{ ".htc", "text/x-component" },
		{ ".htm", "text/html" },
		{ ".html", "text/html" },
		{ ".ico", "image/x-icon" },
		{ ".img", "application/octet-stream" },
		{ ".iso", "application/octet-stream" },
		{ ".jar", "application/java-archive" },
		{ ".jardiff", "application/x-java-archive-diff" },
		{ ".jng", "image/x-jng" },
		{ ".jnlp", "application/x-java-jnlp-file" },
		{ ".jpeg", "image/jpeg" },
		{ ".jpg", "image/jpeg" },
		{ ".js", "application/x-javascript" },
		{ ".mml", "text/mathml" },
		{ ".mng", "video/x-mng" },
		{ ".mov", "video/quicktime" },
		{ ".mp3", "audio/mpeg" },
		{ ".mpeg", "video/mpeg" },
		{ ".mpg", "video/mpeg" },
		{ ".msi", "application/octet-stream" },
		{ ".msm", "application/octet-stream" },
		{ ".msp", "application/octet-stream" },
		{ ".pdb", "application/x-pilot" },
		{ ".pdf", "application/pdf" },
		{ ".pem", "application/x-x509-ca-cert" },
		{ ".pl", "application/x-perl" },
		{ ".pm", "application/x-perl" },
		{ ".png", "image/png" },
		{ ".prc", "application/x-pilot" },
		{ ".ra", "audio/x-realaudio" },
		{ ".rar", "application/x-rar-compressed" },
		{ ".rpm", "application/x-redhat-package-manager" },
		{ ".rss", "text/xml" },
		{ ".run", "application/x-makeself" },
		{ ".sea", "application/x-sea" },
		{ ".shtml", "text/html" },
		{ ".sit", "application/x-stuffit" },
		{ ".swf", "application/x-shockwave-flash" },
		{ ".tcl", "application/x-tcl" },
		{ ".tk", "application/x-tcl" },
		{ ".txt", "text/plain" },
		{ ".war", "application/java-archive" },
		{ ".wbmp", "image/vnd.wap.wbmp" },
		{ ".wmv", "video/x-ms-wmv" },
		{ ".xml", "text/xml" },
		{ ".xpi", "application/x-xpinstall" },
		{ ".zip", "application/zip" },
		{ ".map", "application/json" },
		{ ".woff", "application/x-font-woff" },
		{ ".woff2", "application/x-font-woff" },
		{ ".ttf", "application/x-font-ttf" },
		{ ".svg", "image/svg+xml" }
	};

	private Thread thread;

	private volatile bool threadActive;

	private HttpListener listener;

	private WebConfig config;

	public WebEncryption encryption;

	private static readonly int CHUNK_SIZE = 512;

	public static ConcurrentDictionary<string, WebUser> users = new ConcurrentDictionary<string, WebUser>();

	public static ConcurrentDictionary<int, string> session = new ConcurrentDictionary<int, string>();

	public static HashSet<string> wsProfilesSet;

	public static List<string> wsProfiles;

	public static int wsProfileIndex;

	public ConcurrentDictionary<string, ConcurrentQueue<string>> GameActionQueue = new ConcurrentDictionary<string, ConcurrentQueue<string>>();

	public ConcurrentDictionary<string, ConcurrentQueue<WebResponse>> RespQueue = new ConcurrentDictionary<string, ConcurrentQueue<WebResponse>>();

	public WebServer(WebConfig wc)
	{
		config = wc;
		wsProfilesSet = new HashSet<string>();
		wsProfiles = new List<string>();
		wsProfileIndex = 0;
		string[] profiles = config.profiles;
		foreach (string item in profiles)
		{
			wsProfilesSet.Add(item);
			wsProfiles.Add(item);
		}
		if (config != null && config.users != null)
		{
			WebUser[] array = config.users;
			foreach (WebUser webUser in array)
			{
				users[webUser.name] = webUser;
			}
		}
	}

	public bool IsActive()
	{
		return threadActive;
	}

	public void AddPoll(string key)
	{
		if (!string.IsNullOrEmpty(key))
		{
			RespQueue.TryAdd(key, new ConcurrentQueue<WebResponse>());
		}
	}

	public void QueueNotify(string user, WebResponse wr)
	{
		if (!string.IsNullOrEmpty(user))
		{
			AddPoll(user);
			RespQueue[user].Enqueue(wr);
		}
	}

	public bool DequeueNotify(string key, out WebResponse wr)
	{
		wr = null;
		if (RespQueue.ContainsKey(key))
		{
			return RespQueue[key].TryDequeue(out wr);
		}
		return false;
	}

	public void AddGameAction(string key, string ga)
	{
		if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(ga))
		{
			GameActionQueue.TryAdd(key, new ConcurrentQueue<string>());
			GameActionQueue[key].Enqueue(ga);
		}
	}

	public bool GetGameAction(out string ga)
	{
		ga = null;
		foreach (ConcurrentQueue<string> value in GameActionQueue.Values)
		{
			if (value.TryDequeue(out ga))
			{
				return true;
			}
		}
		return false;
	}

	public void RunScheduler(D2Profile p)
	{
		if (!wsProfilesSet.Contains(p.Name))
		{
			return;
		}
		p.StatusLock.EnterReadLock();
		if (p.Status == Status.Stop)
		{
			p.StatusLock.ExitReadLock();
			GameAction gameAction = null;
			if (p.InfoTag.Length > 0)
			{
				try
				{
					gameAction = JsonConvert.DeserializeObject<GameAction>(p.InfoTag);
					if (gameAction.action.Equals("done"))
					{
						QueueNotify(gameAction.profile, new WebResponse("GameActionNotify", "success", p.InfoTag));
						p.InfoTag = "";
					}
				}
				catch (Exception)
				{
				}
			}
			if (p.Name != wsProfiles[wsProfileIndex])
			{
				return;
			}
			if (p.InfoTag.Length == 0 && GetGameAction(out var ga))
			{
				wsProfileIndex = (wsProfileIndex + 1) % wsProfiles.Count;
				p.InfoTag = ga;
			}
			if (p.InfoTag.Length > 0)
			{
				try
				{
					p.Load();
				}
				catch (Exception)
				{
				}
			}
		}
		else
		{
			p.StatusLock.ExitReadLock();
		}
	}

	public void Start()
	{
		if (thread != null)
		{
			throw new Exception("WebServer already active. (Call stop first)");
		}
		IPAddress address = null;
		string text = "localhost";
		if (Uri.CheckHostName(config.ip) == UriHostNameType.Dns)
		{
			IPAddress[] hostAddresses = Dns.GetHostAddresses(config.ip);
			for (int i = 0; i < hostAddresses.Length; i++)
			{
				if (Uri.CheckHostName(hostAddresses[i].ToString()) == UriHostNameType.IPv4)
				{
					text = config.ip;
					address = hostAddresses[i];
					break;
				}
			}
		}
		else if (!IPAddress.TryParse(config.ip, out address))
		{
			return;
		}
		listener = new HttpListener();
		config.secure = 0;
		if (config.secure > 0)
		{
			encryption = new WebEncryption();
			if (!string.IsNullOrEmpty(Program.WC.certificate))
			{
				encryption.readStoreByHash(Program.WC.certificate);
			}
			else if (Settings.Default.SSL_Certificate != config.ip || !encryption.fetchSelfSignedCertificate())
			{
				encryption.createSelfSignedCertificate("d2botsharp", address.ToString(), text);
				Settings.Default.SSL_Certificate = config.ip;
				Settings.Default.Save();
			}
			encryption.bindCertificateToEndpoint(new IPEndPoint(address, config.secure));
			Program.GM.SetD2BotTitle("  TLS Expires: " + encryption.certificate.NotAfter.ToString());
			listener.Prefixes.Add($"https://{address.ToString()}:{config.secure}/");
			listener.Prefixes.Add($"https://{text}:{config.secure}/");
		}
		listener.Prefixes.Add($"http://{address.ToString()}:{config.port}/");
		listener.Prefixes.Add($"http://{text}:{config.port}/");
		thread = new Thread(Listen);
		thread.Start();
	}

	public void Stop()
	{
		threadActive = false;
		if (listener != null && listener.IsListening)
		{
			listener.Stop();
		}
		if (thread != null)
		{
			thread.Join();
			thread = null;
		}
		if (listener != null)
		{
			listener.Close();
			listener = null;
		}
	}

	private void Listen()
	{
		threadActive = true;
		try
		{
			listener.Start();
		}
		catch (Exception e)
		{
			Program.LogCrash(e, "", show: false);
			threadActive = false;
			return;
		}
		while (threadActive)
		{
			try
			{
				ThreadPool.QueueUserWorkItem(ProcessContextWrapper, listener.GetContext());
				if (!threadActive)
				{
					break;
				}
			}
			catch (HttpListenerException ex)
			{
				if (ex.ErrorCode != 995)
				{
					Program.LogCrash(ex);
					threadActive = false;
				}
			}
			catch (Exception e2)
			{
				Program.LogCrash(e2);
			}
		}
	}

	private void ProcessContextWrapper(object o)
	{
		try
		{
			ProcessContext(o);
		}
		catch (HttpListenerException ex)
		{
			if (ex.ErrorCode != 995)
			{
				Program.LogCrash(ex);
			}
		}
		catch (Exception e)
		{
			Program.LogCrash(e, "", show: false);
		}
	}

	private void ProcessContext(object o)
	{
		HttpListenerContext httpListenerContext = o as HttpListenerContext;
		int hashCode = (httpListenerContext.Request.RemoteEndPoint.Address.ToString() + httpListenerContext.Request.UserAgent.ToString()).GetHashCode();
		session.TryAdd(hashCode, AES.GetUniqueKey(32));
		string text = httpListenerContext.Request.Url.AbsolutePath;
		if (text != null)
		{
			text = text.Substring(1);
		}
		if (string.IsNullOrEmpty(text))
		{
			string[] array = indexFiles;
			foreach (string text2 in array)
			{
				if (File.Exists(Path.Combine(config.path, text2)))
				{
					text = text2;
					break;
				}
			}
		}
		string path = Path.Combine(config.path, text);
		bool flag = FilePathIsValid(path);
		if (httpListenerContext.Request.HttpMethod == "OPTIONS")
		{
			httpListenerContext.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept, X-Requested-With");
			httpListenerContext.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST");
			httpListenerContext.Response.AddHeader("Access-Control-Max-Age", "1728000");
		}
		httpListenerContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
		HttpStatusCode httpStatusCode;
		if (flag)
		{
			httpStatusCode = HttpStatusCode.NotFound;
			if (File.Exists(path))
			{
				try
				{
					using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
					{
						httpListenerContext.Response.ContentType = mimeTypes[Path.GetExtension(path)];
						httpListenerContext.Response.ContentLength64 = fileStream.Length;
						fileStream.CopyTo(httpListenerContext.Response.OutputStream);
						fileStream.Flush();
						httpListenerContext.Response.OutputStream.Flush();
					}
					httpStatusCode = HttpStatusCode.OK;
				}
				catch (Exception e)
				{
					Program.LogCrash(e);
					httpStatusCode = HttpStatusCode.InternalServerError;
				}
			}
		}
		else
		{
			httpStatusCode = HttpStatusCode.BadRequest;
			if (ProcessAPI(hashCode, GetDocumentContents(httpListenerContext.Request), out var statusCode, out var response))
			{
				httpListenerContext.Response.ContentType = "text/plain";
				if (response != null)
				{
					response = Convert.ToBase64String(Encoding.UTF8.GetBytes(response.Substring(0, response.Length)));
					httpListenerContext.Response.ContentLength64 = response.Length;
					int j = 0;
					try
					{
						int num;
						for (; j < response.Length; j += num)
						{
							num = Math.Min(CHUNK_SIZE, response.Length - j);
							httpListenerContext.Response.OutputStream.Write(Encoding.UTF8.GetBytes(response.Substring(j, num)), 0, num);
							httpListenerContext.Response.OutputStream.Flush();
						}
					}
					catch
					{
					}
				}
				httpStatusCode = statusCode;
			}
		}
		httpListenerContext.Response.StatusCode = (int)httpStatusCode;
		httpListenerContext.Response.AddHeader("User-Agent", "D2BOT-" + Process.GetCurrentProcess().Id);
		switch (httpStatusCode)
		{
		case HttpStatusCode.OK:
			httpListenerContext.Response.AddHeader("Date", DateTime.Now.ToString("r"));
			if (flag)
			{
				httpListenerContext.Response.AddHeader("Last-Modified", File.GetLastWriteTime(path).ToString("r"));
			}
			break;
		}
		httpListenerContext.Response.OutputStream.Close();
	}

	private bool ProcessAPI(int id, string api, out HttpStatusCode statusCode, out string response)
	{
		statusCode = HttpStatusCode.OK;
		try
		{
			byte[] bytes = Convert.FromBase64String(api);
			string message = Encoding.UTF8.GetString(bytes);
			statusCode = Program.GM.HandleWebApi(session[id], message, out response);
		}
		catch (Exception)
		{
			response = null;
		}
		if (statusCode != HttpStatusCode.OK)
		{
			return false;
		}
		return true;
	}

	private string GetDocumentContents(HttpListenerRequest request)
	{
		using Stream stream = request.InputStream;
		using StreamReader streamReader = new StreamReader(stream, Encoding.UTF8);
		return streamReader.ReadToEnd();
	}

	public static bool FilePathIsValid(string path)
	{
		bool result = false;
		if (!path.Equals("api") && !string.IsNullOrEmpty(path) && !IsBase64(path) && path.Length < 248)
		{
			try
			{
				Path.GetFileName(path);
				Path.GetDirectoryName(path);
				if (!string.IsNullOrEmpty(Path.GetExtension(path)))
				{
					result = true;
				}
			}
			catch (ArgumentException)
			{
				result = false;
			}
		}
		return result;
	}

	public static bool IsBase64(string base64String)
	{
		if (string.IsNullOrEmpty(base64String) || base64String.Length % 4 != 0 || base64String.Contains(" ") || base64String.Contains("\t") || base64String.Contains("\r") || base64String.Contains("\n") || base64String.Contains(":"))
		{
			return false;
		}
		try
		{
			Convert.FromBase64String(base64String);
			return true;
		}
		catch (Exception)
		{
		}
		return false;
	}
}
