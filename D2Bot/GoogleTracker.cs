using System.Collections;
using System.Net;
using System.Web;

namespace D2Bot;

public class GoogleTracker
{
	private string googleURL = "http://www.google-analytics.com/collect";

	private string googleVersion = "1";

	private string googleTrackingID = "UA-XXXX-Y";

	private string googleClientID = "555";

	public GoogleTracker(string trackingID, string clientID)
	{
		googleTrackingID = trackingID;
		googleClientID = clientID;
	}

	public void trackEvent(string category, string action, string label, string value)
	{
		Hashtable hashtable = baseValues();
		hashtable.Add("t", "event");
		hashtable.Add("ec", category);
		hashtable.Add("ea", action);
		if (label != null)
		{
			hashtable.Add("el", label);
		}
		if (value != null)
		{
			hashtable.Add("ev", value);
		}
		postData(hashtable);
	}

	public void trackPage(string hostname, string page, string title)
	{
		Hashtable hashtable = baseValues();
		hashtable.Add("t", "pageview");
		hashtable.Add("dh", hostname);
		hashtable.Add("dp", page);
		hashtable.Add("dt", title);
		postData(hashtable);
	}

	public void trackScreen(string screen)
	{
		Hashtable hashtable = baseValues();
		hashtable.Add("t", "screenview");
		hashtable.Add("an", "D2BotSharp");
		hashtable.Add("av", Program.VER);
		hashtable.Add("aid", "com.d2bot.app");
		hashtable.Add("aiid", "com.net.d2bot");
		hashtable.Add("cd", screen);
	}

	public void trackException(string description, bool fatal)
	{
		Hashtable hashtable = baseValues();
		hashtable.Add("t", "exception");
		hashtable.Add("exd", description);
		hashtable.Add("exf", fatal ? "1" : "0");
		postData(hashtable);
	}

	private Hashtable baseValues()
	{
		return new Hashtable
		{
			{ "v", googleVersion },
			{ "tid", googleTrackingID },
			{ "cid", googleClientID }
		};
	}

	private bool postData(Hashtable values)
	{
		string text = "";
		foreach (object key in values.Keys)
		{
			if (text != "")
			{
				text += "&";
			}
			if (values[key] != null)
			{
				text = text + key.ToString() + "=" + HttpUtility.UrlEncode(values[key].ToString());
			}
		}
		using (WebClient webClient = new WebClient())
		{
			webClient.UploadString(googleURL, "POST", text);
		}
		return true;
	}
}
