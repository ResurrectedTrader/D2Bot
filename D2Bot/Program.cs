using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using D2Bot.Properties;
using D2BSItemlog;
using Microsoft.Win32;
using Newtonsoft.Json;
using PInvoke;

namespace D2Bot;

public static class Program
{
	public static Main GM;

	public static string VER = Assembly.GetExecutingAssembly().GetName().Version.ToString();

	public static bool GA = false;

	public static ConcurrentDictionary<string, PatchData> PatchList = new ConcurrentDictionary<string, PatchData>();

	public static ConcurrentDictionary<string, string> DataCache = new ConcurrentDictionary<string, string>();

	public static ConcurrentDictionary<string, ProfileBase> ProfileList = new ConcurrentDictionary<string, ProfileBase>();

	public static ConcurrentDictionary<IntPtr, ProfileBase> Runtime = new ConcurrentDictionary<IntPtr, ProfileBase>();

	public static List<ProfileBase> Viewable = new List<ProfileBase>();

	public static ConcurrentDictionary<string, Schedule> Schedules = new ConcurrentDictionary<string, Schedule>();

	public static ConcurrentDictionary<string, KeyList> KeyLists = new ConcurrentDictionary<string, KeyList>();

	public static Dictionary<string, DateTime> RDTime = new Dictionary<string, DateTime>();

	public static string CurrentProfile;

	public static IntPtr Handle;

	public static Dictionary<string, Image> ItemBG = new Dictionary<string, Image>();

	public static HashSet<string> HoldKeyList = new HashSet<string>();

	public static HashSet<string> Versions = new HashSet<string>();

	public static D2Tooltip D2Tool;

	public static ListViewItem Previous;

	public static Item CurrentItem;

	public static GoogleTracker ReportError = new GoogleTracker("UA-101039229-1", GetProductId());

	public static WebFilters WF = null;

	public static WebConfig WC = null;

	public static WebServer WS = null;

	public const string EXCEPTIONS = "\\logs\\exceptions.log";

	public const string KEYINFO = "\\logs\\keyinfo.log";

	public const string D2BS_INI = "\\d2bs\\d2bs.ini";

	public const string D2BS_DLL = "\\d2bs\\D2BS.dll";

	public const string CDKEYS = "\\data\\cdkeys.json";

	public const string PATCH = "\\data\\patch.json";

	public const string PROFILE = "\\data\\profile.json";

	public const string SCHEDULES = "\\data\\schedules.json";

	public const string SERVER = "\\data\\server.json";

	public const string WEB_SETTINGS = "\\data\\web\\limedrop.json";

	public static string BOT_LIB = "";

	private static readonly Regex _whitespace = new Regex("\\s+");

	[STAThread]
	private static void Main()
	{
		string d2bsPath = Application.StartupPath + "\\d2bs\\";
		if (!Directory.Exists(d2bsPath))
		{
			MessageBox.Show("D2BS Folder is missing!\n\nPlease create a d2bs folder with:\n- d2bs.ini\n- A botting library folder (e.g., kolbot)", "Missing d2bs folder", MessageBoxButtons.OK, MessageBoxIcon.Error);
			return;
		}

		string[] directories = Directory.GetDirectories(d2bsPath, "*bot");
		if (directories.Length == 0)
		{
			MessageBox.Show("D2BS Folder is missing botting library!\n\nAdd a botting library folder (e.g., kolbot) to the d2bs folder.", "Missing botting library", MessageBoxButtons.OK, MessageBoxIcon.Error);
			return;
		}
		BOT_LIB = directories[0];

		if (!File.Exists(Application.StartupPath + D2BS_INI))
		{
			MessageBox.Show("D2BS configuration file missing!\n\nPlease create: d2bs\\d2bs.ini", "Missing d2bs.ini", MessageBoxButtons.OK, MessageBoxIcon.Error);
			return;
		}

		// Ensure required directories exist
		string dataPath = Application.StartupPath + "\\data";
		string logsPath = Application.StartupPath + "\\logs";
		string webPath = BOT_LIB + "\\data\\web";

		if (!Directory.Exists(dataPath))
		{
			try { Directory.CreateDirectory(dataPath); }
			catch (Exception ex)
			{
				MessageBox.Show($"Failed to create data directory:\n{dataPath}\n\n{ex.Message}", "Directory Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
		}

		if (!Directory.Exists(logsPath))
		{
			try { Directory.CreateDirectory(logsPath); }
			catch (Exception ex)
			{
				MessageBox.Show($"Failed to create logs directory:\n{logsPath}\n\n{ex.Message}", "Directory Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
		}

		if (!Directory.Exists(webPath))
		{
			try { Directory.CreateDirectory(webPath); }
			catch (Exception ex)
			{
				MessageBox.Show($"Failed to create web data directory:\n{webPath}\n\n{ex.Message}", "Directory Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
		}

		try
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(defaultValue: false);
			Kernel32.SetErrorMode(Kernel32.ErrorModes.SEM_NOGPFAULTERRORBOX);
			LoadWebConfig();
			GM = new Main();
			GM.LoadList();
			if (Settings.Default.Start_Server && WC != null)
			{
				WebEvents.InitEvents();
				if (WS != null && WS.IsActive())
				{
					WS.Stop();
				}
				WS = new WebServer(WC);
				WS.Start();
			}
			LoadProfile();
			if (IsValidURL("http://google-analytics.com"))
			{
				GA = true;
			}
			Application.Run(GM);
		}
		catch (Exception e)
		{
			LogCrash(e);
			GM.Main_Close(null, null);
			Application.Exit();
		}
		finally
		{
			if (WC != null && WS != null)
			{
				WS.Stop();
			}
		}
	}

	private static bool IsValidURL(string url)
	{
		HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
		httpWebRequest.Timeout = 5000;
		httpWebRequest.Method = "HEAD";
		try
		{
			using HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
			return httpWebResponse.StatusCode == HttpStatusCode.OK;
		}
		catch (WebException)
		{
			return false;
		}
	}

	private static string GetProductId()
	{
		RegistryKey registryKey = null;
		registryKey = ((!Environment.Is64BitOperatingSystem) ? RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32) : RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64));
		return registryKey.OpenSubKey("Software\\Microsoft\\Windows NT\\CurrentVersion").GetValue("ProductId").ToString()
			.GetHashCode()
			.ToString();
	}

	public static void LogCrash(Exception e, string comment = "", bool show = true)
	{
		string text = e.ToString() + "\r\n" + comment;
		try
		{
			if (!File.Exists(Application.StartupPath + "\\logs\\exceptions.log"))
			{
				File.Create(Application.StartupPath + "\\logs\\exceptions.log").Close();
			}
			if (show)
			{
				MessageBox.Show("Please view your exceptions.log file. D2Bot# will close now :(", "An error has occured!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			FileHandler.Append(Application.StartupPath + "\\logs\\exceptions.log", DateTime.UtcNow.ToShortDateString() + " " + DateTime.UtcNow.ToShortTimeString() + "\r\n" + text + "\r\n");
			ReportError.trackException(_whitespace.Replace(e.ToString(), "+"), fatal: true);
		}
		catch
		{
		}
	}

	public static void LoadProfile(string file = "\\data\\profile.json")
	{
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Expected O, but got Unknown
		CurrentProfile = Application.StartupPath + file;
		if (!File.Exists(Application.StartupPath + "\\logs\\keyinfo.log"))
		{
			File.Create(Application.StartupPath + "\\logs\\keyinfo.log").Close();
		}
		if (!File.Exists(Application.StartupPath + file))
		{
			File.Create(Application.StartupPath + file).Close();
		}
		if (!File.Exists(Application.StartupPath + "\\data\\schedules.json"))
		{
			File.Create(Application.StartupPath + "\\data\\schedules.json").Close();
		}
		if (!File.Exists(Application.StartupPath + "\\data\\cdkeys.json"))
		{
			File.Create(Application.StartupPath + "\\data\\cdkeys.json").Close();
		}
		if (!File.Exists(Application.StartupPath + "\\data\\patch.json"))
		{
			File.Create(Application.StartupPath + "\\data\\patch.json").Close();
		}
		string[] array = FileHandler.ReadLines(Application.StartupPath + "\\data\\patch.json");
		string[] array2 = FileHandler.ReadLines(Application.StartupPath + file);
		string[] array3 = FileHandler.ReadLines(Application.StartupPath + "\\data\\cdkeys.json");
		string[] array4 = FileHandler.ReadLines(Application.StartupPath + "\\data\\schedules.json");
		JsonSerializerSettings val = new JsonSerializerSettings
		{
			CheckAdditionalContent = false,
			MissingMemberHandling = (MissingMemberHandling)0
		};
		for (int i = 0; i < array.Length; i++)
		{
			if (string.IsNullOrEmpty(array[i]) || array[i].StartsWith("//"))
			{
				continue;
			}
			PatchData patchData = JsonConvert.DeserializeObject<PatchData>(array[i], val);
			PatchList.TryAdd(patchData.Name + patchData.Version, patchData);
			if (!Versions.Contains(patchData.Version))
			{
				Versions.Add(patchData.Version);
				ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem
				{
					CheckOnClick = true,
					DisplayStyle = ToolStripItemDisplayStyle.Text,
					ImageScaling = ToolStripItemImageScaling.None,
					Name = patchData.Version,
					Size = new Size(152, 22),
					Text = patchData.Version,
					TextImageRelation = TextImageRelation.Overlay
				};
				toolStripMenuItem.Click += GM.VersionHandler;
				GM.versionMenuItem.DropDownItems.Add(toolStripMenuItem);
				if (Settings.Default.D2_Version.Equals(patchData.Version))
				{
					toolStripMenuItem.Checked = true;
				}
			}
		}
		for (int j = 0; j < array2.Length; j++)
		{
			if (string.IsNullOrEmpty(array2[j]))
			{
				continue;
			}
			TypeFinder typeFinder = JsonConvert.DeserializeObject<TypeFinder>(array2[j], val);
			ProfileBase profileBase = null;
			try
			{
				profileBase = ((typeFinder.Type != ProfileType.D2 || array2[j].Contains("\"IsIRC\":true")) ? ((ProfileBase)JsonConvert.DeserializeObject<IRCProfile>(array2[j], val)) : ((ProfileBase)JsonConvert.DeserializeObject<D2Profile>(array2[j], val)));
			}
			catch (Exception e)
			{
				LogCrash(e, "Profile: " + array2[j], show: false);
				continue;
			}
			if (profileBase != null)
			{
				if (string.IsNullOrEmpty(profileBase.Group))
				{
					profileBase.Group = "default";
				}
				profileBase.Add();
			}
		}
		for (int k = 0; k < array3.Length; k++)
		{
			if (!string.IsNullOrEmpty(array3[k]))
			{
				KeyList keyList = null;
				try
				{
					keyList = JsonConvert.DeserializeObject<KeyList>(array3[k]);
				}
				catch (Exception e2)
				{
					LogCrash(e2, "CDKey: " + array3[k], show: false);
					continue;
				}
				KeyLists.TryAdd(keyList.Name.ToLower(), keyList);
			}
		}
		for (int l = 0; l < array4.Length; l++)
		{
			if (!string.IsNullOrEmpty(array4[l]))
			{
				Schedule schedule = null;
				try
				{
					schedule = JsonConvert.DeserializeObject<Schedule>(array4[l]);
				}
				catch (Exception e3)
				{
					LogCrash(e3, "Schedule: " + array4[l], show: false);
					continue;
				}
				Schedules.TryAdd(schedule.Name.ToLower(), schedule);
			}
		}
	}

	public static void LoadWebConfig()
	{
		if (!File.Exists(Application.StartupPath + "\\data\\server.json"))
		{
			File.Create(Application.StartupPath + "\\data\\server.json").Close();
		}
		if (!File.Exists(BOT_LIB + "\\data\\web\\limedrop.json"))
		{
			File.Create(BOT_LIB + "\\data\\web\\limedrop.json").Close();
		}
		try
		{
			string text = FileHandler.Read(Application.StartupPath + "\\data\\server.json");
			if (text.Length > 0)
			{
				WC = JsonConvert.DeserializeObject<WebConfig>(text);
				WC.path = Application.StartupPath + WC.path;
			}
			string text2 = FileHandler.Read(BOT_LIB + "\\data\\web\\limedrop.json");
			if (text2.Length > 0)
			{
				WebFilters webFilters = JsonConvert.DeserializeObject<WebFilters>(text2);
				if (webFilters.LoadFilters())
				{
					WF = webFilters;
				}
			}
		}
		catch
		{
		}
	}

	public static void SendCopyData(IntPtr hWnd, string msg, IntPtr id, int delay = 0, int retry = 1)
	{
		Thread thread = new Thread((ThreadStart)delegate
		{
			SendTimeout(hWnd, msg, id, delay, retry);
		});
		thread.IsBackground = true;
		thread.Start();
	}

	public static void ShoutGlobal(string msg, IntPtr id)
	{
		foreach (ProfileBase value in ProfileList.Values)
		{
			if (value.Type == ProfileType.D2)
			{
				D2Profile d2Profile = (D2Profile)value;
				if (d2Profile.D2Process != null && d2Profile.D2Process != null)
				{
					SendCopyData(d2Profile.MainWindowHandle, msg, id);
				}
			}
		}
	}

	public static void IRCEventMsg(string msg, IntPtr id)
	{
		foreach (ProfileBase value in ProfileList.Values)
		{
			if (value.Type == ProfileType.D2)
			{
				D2Profile d2Profile = (D2Profile)value;
				if (d2Profile.D2Process != null && d2Profile.D2Process != null && d2Profile.IRCEvent)
				{
					SendCopyData(d2Profile.MainWindowHandle, msg, id);
				}
			}
		}
	}

	public static bool SendTimeout(IntPtr hWnd, string msg, IntPtr id, int delay = 0, int retry = 1)
	{
		byte[] bytes = Encoding.UTF8.GetBytes(msg + "\0");
		MessageHelper.COPYDATASTRUCT lParam = new MessageHelper.COPYDATASTRUCT
		{
			dwData = id,
			cbData = bytes.Length,
			lpData = Marshal.AllocHGlobal(bytes.Length)
		};
		Marshal.Copy(bytes, 0, lParam.lpData, bytes.Length);
		for (int i = 0; i < retry; i++)
		{
			if (delay > 0)
			{
				Thread.Sleep(delay);
			}
			MessageHelper.SendMessageTimeout(hWnd, 74u, IntPtr.Zero, ref lParam, MessageHelper.SendMessageTimeoutFlags.SMTO_NOTIMEOUTIFNOTHUNG, 1000u, out var result);
			if (result != IntPtr.Zero)
			{
				return true;
			}
		}
		return false;
	}

	public static bool CanRenameItem(string oldName, string newName)
	{
		oldName = oldName.Trim();
		newName = newName.Trim();
		if (oldName.Equals(newName, StringComparison.OrdinalIgnoreCase))
		{
			return true;
		}
		if (!ProfileList.ContainsKey(oldName.ToLower()))
		{
			return false;
		}
		if (ProfileList.ContainsKey(newName.ToLower()))
		{
			return false;
		}
		return true;
	}

	public static bool RenameItem(string oldName, string newName)
	{
		oldName = oldName.Trim();
		newName = newName.Trim();
		if (!CanRenameItem(oldName, newName))
		{
			return false;
		}
		if (oldName.Equals(newName, StringComparison.OrdinalIgnoreCase))
		{
			return true;
		}
		if (!ProfileList.TryRemove(oldName.ToLower(), out var value))
		{
			return false;
		}
		value.Name = newName;
		ProfileList.TryAdd(newName.ToLower(), value);
		return true;
	}

	public static void SaveProfiles()
	{
		if (ProfileList.Count == 0)
		{
			return;
		}
		string[] array = new string[ProfileList.Count];
		string[] array2 = new string[ProfileList.Count + 1];
		string text = FileHandler.Read(Application.StartupPath + "\\d2bs\\d2bs.ini");
		array2[0] = text.Substring(0, text.IndexOf("; gateway=") + 10);
		int num = 0;
		int num2 = 0;
		GM.objectLock.WaitOne();
		foreach (ProfileBase @object in GM.objectProfileList.Objects)
		{
			if (@object == null)
			{
				continue;
			}
			array[num] = JsonConvert.SerializeObject((object)@object);
			num++;
			if (@object.Type != ProfileType.IRC)
			{
				D2Profile d2Profile = (D2Profile)@object;
				string text2 = "";
				if (d2Profile.Difficulty.ToLower().Equals("highest"))
				{
					text2 = "3";
				}
				else if (d2Profile.Difficulty[1] == 'e')
				{
					text2 = "2";
				}
				else if (d2Profile.Difficulty[1] == 'i')
				{
					text2 = "1";
				}
				else if (d2Profile.Difficulty[1] == 'o')
				{
					text2 = "0";
				}
				array2[num2 + 1] = "[" + d2Profile.Name + "]\r\nMode=" + d2Profile.Mode + "\r\nUsername=" + d2Profile.Account + "\r\nPassword=" + d2Profile.Password + "\r\ngateway=" + d2Profile.Realm + "\r\ncharacter=" + d2Profile.Character + "\r\nScriptPath=" + Path.GetFileName(BOT_LIB) + "\r\nDefaultGameScript=default.dbj\r\nDefaultStarterScript=" + Path.GetFileName(d2Profile.Entry) + "\r\nspdifficulty=" + text2 + "\r\n";
				num2++;
			}
		}
		GM.objectLock.ReleaseMutex();
		FileHandler.Write(CurrentProfile, string.Join("\n", array));
		FileHandler.Write(Application.StartupPath + "\\d2bs\\d2bs.ini", string.Join("\n", array2), Encoding.Unicode);
	}

	public static void SaveKeyLists(string k = null)
	{
		string[] array = new string[KeyLists.Count];
		int num = 0;
		foreach (KeyList value in KeyLists.Values)
		{
			array[num] = JsonConvert.SerializeObject((object)value);
			num++;
		}
		FileHandler.Write(Application.StartupPath + "\\data\\cdkeys.json", string.Join("\n", array));
	}

	public static void SaveSchedules(string s = null)
	{
		string[] array = new string[Schedules.Count];
		int num = 0;
		foreach (Schedule value in Schedules.Values)
		{
			array[num] = JsonConvert.SerializeObject((object)value);
			num++;
		}
		FileHandler.Write(Application.StartupPath + "\\data\\schedules.json", string.Join("\n", array));
	}

	public static void SaveAll()
	{
		SaveProfiles();
		SaveKeyLists();
		SaveSchedules();
	}

	public static void AddToKeyinfo(string info)
	{
		FileHandler.Append(Application.StartupPath + "\\logs\\keyinfo.log", info);
	}

	public static void RemoveFromLog(string profile, string key)
	{
		List<string> list = new List<string>(FileHandler.ReadLines(Application.StartupPath + "\\logs\\keyinfo.log"));
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i].Contains(profile) && list[i].Contains(key))
			{
				list.Remove(list[i]);
				i--;
			}
		}
		FileHandler.Write(Application.StartupPath + "\\logs\\keyinfo.log", string.Join("\n", list));
	}

	public static void ClearLog()
	{
		FileHandler.Write(Application.StartupPath + "\\logs\\keyinfo.log", "");
	}

	private static IEnumerable<string> Split(string str, int maxChunkSize)
	{
		for (int i = 0; i < str.Length; i += maxChunkSize)
		{
			yield return str.Substring(i, Math.Min(maxChunkSize, str.Length - i));
		}
	}

	public static void LaunchClient(D2Profile client)
	{
		if (!File.Exists(client.D2Path))
		{
			throw new Exception("Invalid Diablo II path!");
		}
		string[] files = Directory.GetFiles(Path.GetDirectoryName(client.D2Path), "*.dat*", SearchOption.AllDirectories);
		foreach (string path in files)
		{
			try
			{
				File.Delete(path);
			}
			catch
			{
			}
		}
		CDKey currentKey = client.CurrentKey;
		string text = "";
		if (currentKey != null && currentKey.Classic != null && currentKey.Classic.Length > 0)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(" -d2c \"" + currentKey.Classic + "\"");
			stringBuilder.Append(" -d2x \"" + currentKey.Expansion + "\"");
			text = stringBuilder.ToString();
		}
		else if (currentKey != null)
		{
			text = currentKey.Name;
		}
		string parameters = client.Parameters;
		string text2 = " -handle \"" + Handle + "\" -cachefix -multi -title \"" + client.Name + "\" " + parameters;
		if (!text2.Contains(" -L"))
		{
			text2 = " -profile \"" + client.Name + "\"" + text2;
		}
		if (text != null && text.ToLower().Contains("mpq"))
		{
			text2 = " -mpq \"" + text + "\"" + text2;
		}
		else if (text.Length > 0)
		{
			text2 = text + text2;
		}
		ProcessStartInfo startInfo = new ProcessStartInfo(client.D2Path)
		{
			Arguments = text2,
			UseShellExecute = false,
			WorkingDirectory = Path.GetDirectoryName(client.D2Path)
		};
		client.D2Process = new Process
		{
			StartInfo = startInfo
		};
		bool flag = File.Exists(Application.StartupPath + "\\D2BS\\D2M.dll");
		client.D2Process = Kernel32.StartSuspended(client.D2Process, startInfo);
		DACOverwrite.OverwriteDac(client);
		try
		{
			if (flag)
			{
				Kernel32.LoadRemoteLibrary(client.D2Process, Application.StartupPath + "\\D2BS\\D2M.dll");
			}
			else
			{
				Kernel32.LoadRemoteLibrary(client.D2Process, Application.StartupPath + "\\d2bs\\D2BS.dll");
			}
		}
		catch (Exception)
		{
			GM.ConsolePrint(new PrintMessage("D2M.dll failed to load"), client);
			CrashReport(client);
		}
		PatchData value;
		bool flag2 = PatchList.TryGetValue("hidewin0" + Settings.Default.D2_Version, out value);
		PatchData value2;
		bool flag3 = PatchList.TryGetValue("hidewin1" + Settings.Default.D2_Version, out value2);
		PatchData value3;
		bool flag4 = PatchList.TryGetValue("hidewin2" + Settings.Default.D2_Version, out value3);
		Patch patch = null;
		Patch patch2 = null;
		Patch patch3 = null;
		if (!client.Visible)
		{
			if (flag2)
			{
				patch = new Patch(value.Module, value.Offset, value.Data);
				patch.Install(client.D2Process);
			}
			if (flag3)
			{
				patch2 = new Patch(value2.Module, value2.Offset, value2.Data);
				patch2.Install(client.D2Process);
			}
			if (flag4)
			{
				patch3 = new Patch(value3.Module, value3.Offset, value3.Data);
				patch3.Install(client.D2Process);
			}
		}
		InstallPatches(client.D2Process);
		Kernel32.Resume(client.D2Process);
		client.D2Process.WaitForInputIdle();
		if (Runtime.ContainsKey(client.MainWindowHandle))
		{
			Runtime.TryRemove(client.MainWindowHandle, out var _);
		}
		client.MainWindowHandle = client.D2Process.MainWindowHandle;
		if (flag)
		{
			try
			{
				Kernel32.LoadRemoteLibrary(client.D2Process, Application.StartupPath + "\\d2bs\\D2BS.dll");
			}
			catch
			{
				GM.ConsolePrint(new PrintMessage("D2BS.dll failed to load"), client);
				CrashReport(client);
			}
		}
		if (!client.Visible)
		{
			if (Settings.Default.Start_Hidden)
			{
				client.HideWindow();
			}
			else
			{
				MessageHelper.SendMessageTimeout(client.MainWindowHandle, 28u, (IntPtr)0, IntPtr.Zero, MessageHelper.SendMessageTimeoutFlags.SMTO_NOTIMEOUTIFNOTHUNG, 250u, out var _);
			}
		}
		client.Crashed = 0;
		try
		{
			if (patch != null && patch.IsInstalled() && !client.D2Process.HasExited)
			{
				patch.Remove(client.D2Process);
			}
			if (patch2 != null && patch2.IsInstalled() && !client.D2Process.HasExited)
			{
				patch2.Remove(client.D2Process);
			}
			if (patch3 != null && patch3.IsInstalled() && !client.D2Process.HasExited)
			{
				patch3.Remove(client.D2Process);
			}
			MessageHelper.SendMessageTimeout(client.MainWindowHandle, 28u, (IntPtr)0, IntPtr.Zero, MessageHelper.SendMessageTimeoutFlags.SMTO_NOTIMEOUTIFNOTHUNG, 250u, out var _);
		}
		catch
		{
		}
		Runtime.TryAdd(client.MainWindowHandle, client);
		if (client.Visible)
		{
			client.ShowWindow();
		}
	}

	private static void InstallPatches(Process p)
	{
		foreach (string key in PatchList.Keys)
		{
			if (!key.StartsWith("hidewin") && !key.StartsWith("rdblock") && PatchList.TryGetValue(key, out var value))
			{
				new Patch(value.Module, value.Offset, value.Data).Install(p);
			}
		}
	}

	public static void CrashReport(D2Profile profile)
	{
		profile.Crashed++;
		profile.Stop();
		if (profile.Crashed < 6)
		{
			PrintMessage pm = new PrintMessage("Window crashed on load! Restarting.");
			GM.ConsolePrint(pm, profile);
			profile.Restart();
		}
	}
}
