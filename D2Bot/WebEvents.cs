using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;

namespace D2Bot;

internal class WebEvents
{
	private static Dictionary<string, HashSet<string>> events = new Dictionary<string, HashSet<string>>();

	public static void InitEvents()
	{
		AddEvent("setTag");
		AddEvent("emit");
	}

	public static void AddEvent(string type)
	{
		if (!events.ContainsKey(type))
		{
			events.Add(type, new HashSet<string>());
		}
	}

	public static bool RegisterEvent(string type, string location)
	{
		if (!events.ContainsKey(type))
		{
			return false;
		}
		events[type].Add(location);
		return true;
	}

	public static void EmitEvent(string type, string json)
	{
		foreach (string item in events[type])
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(new Uri(item));
			httpWebRequest.ProtocolVersion = HttpVersion.Version10;
			httpWebRequest.Method = "POST";
			byte[] bytes = Encoding.UTF8.GetBytes(json);
			httpWebRequest.ContentType = "application/json; charset=UTF-8";
			httpWebRequest.Accept = "application/json";
			httpWebRequest.ContentLength = bytes.Length;
			httpWebRequest.UserAgent = "D2BOT-" + Process.GetCurrentProcess().Id;
			using (Stream stream = httpWebRequest.GetRequestStream())
			{
				stream.Write(bytes, 0, bytes.Length);
			}
			httpWebRequest.GetRequestStream().Close();
		}
	}
}
