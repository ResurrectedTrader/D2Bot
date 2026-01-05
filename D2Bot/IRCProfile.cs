using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Meebey.SmartIrc4net;
using Newtonsoft.Json;

namespace D2Bot;

public class IRCProfile : ProfileBase
{
	public uint Port { get; set; }

	public string Host { get; set; }

	public string Username { get; set; }

	public string Nick { get; set; }

	public string Password { get; set; }

	public string Channel { get; set; }

	public string Confirmation { get; set; }

	public string Controller { get; set; }

	public string Prefix { get; set; }

	public bool MsgChannel { get; set; }

	[JsonIgnore]
	public string Runs { get; set; }

	[JsonIgnore]
	public string Chickens { get; set; }

	[JsonIgnore]
	public string Deaths { get; set; }

	[JsonIgnore]
	public string Crashes { get; set; }

	[JsonIgnore]
	public string Restarts { get; set; }

	[JsonIgnore]
	public string GameExe { get; set; }

	[JsonIgnore]
	public string Key { get; set; }

	[JsonIgnore]
	private IrcClient Client { get; set; }

	public override ProfileType Type => ProfileType.IRC;

	internal override void Start(int delay)
	{
		if (!StatusLock.TryEnterReadLock(10))
		{
			return;
		}
		if (base.Status != Status.Starting)
		{
			StatusLock.ExitReadLock();
			return;
		}
		StatusLock.ExitReadLock();
		if (delay > 0)
		{
			Thread.Sleep(delay);
		}
		StartIRC();
	}

	private void StartIRC()
	{
		//IL_00d9: Expected O, but got Unknown
		//IL_01d5: Expected O, but got Unknown
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Expected O, but got Unknown
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Expected O, but got Unknown
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Expected O, but got Unknown
		if (Client != null)
		{
			return;
		}
		Program.Runtime.TryAdd(new IntPtr(base.Name.GetHashCode()), this);
		Client = new IrcClient
		{
			SendDelay = 50,
			AutoReconnect = true,
			AutoRejoinOnKick = true,
			AutoRelogin = true,
			AutoRejoin = true,
			AutoRetry = true
		};
		Client.OnQueryMessage += new IrcEventHandler(OnMessage);
		Client.OnChannelMessage += new IrcEventHandler(OnMessage);
		bool flag = true;
		string[] array = Host.Replace(" ", "").Split(',');
		int port = (int)Port;
		while (flag)
		{
			flag = false;
			try
			{
				Client.Connect(array, port);
			}
			catch (ConnectionException ex)
			{
				Program.LogCrash((Exception)ex, "", show: false);
				PrintMessage pm = new PrintMessage("Connection refused.", "Check your internet");
				Program.GM.ConsolePrint(pm, this);
				Program.GM.SetStatus(this, "No Connection");
			}
			try
			{
				Client.Login(Nick, "D2Bot#", 0, Username, Password);
				((IrcCommands)Client).RfcJoin(Channel);
				((IrcCommands)Client).SendMessage((SendType)0, Channel, "D2Bot # IRCBot is online.");
				StatusLock.EnterWriteLock();
				base.Status = Status.Run;
				StatusLock.ExitWriteLock();
				while (base.Status != Status.Stop)
				{
					((IrcConnection)Client).ListenOnce();
				}
				((IrcCommands)Client).SendMessage((SendType)0, Nick, "Exit");
				if (((IrcConnection)Client).IsConnected)
				{
					((IrcConnection)Client).Disconnect();
				}
			}
			catch (ConnectionException ex2)
			{
				flag = true;
				Program.LogCrash((Exception)ex2, "", show: false);
				PrintMessage pm2 = new PrintMessage("Connection refused.", "Check your internet");
				Program.GM.ConsolePrint(pm2, this);
				Program.GM.SetStatus(this, "No Connection");
			}
			catch (Exception e)
			{
				Program.LogCrash(e, "", show: false);
				PrintMessage pm3 = new PrintMessage("Connection refused on exception.", "Check your crash log.");
				Program.GM.ConsolePrint(pm3, this);
				Program.GM.SetStatus(this, "Exception");
			}
			finally
			{
				if (!flag)
				{
					Client = null;
					if (base.Status != Status.Stop)
					{
						StatusLock.EnterWriteLock();
						base.Status = Status.Stop;
						StatusLock.ExitWriteLock();
					}
				}
			}
			Thread.Sleep(30000);
		}
	}

	public override void Stop()
	{
		StatusLock.EnterWriteLock();
		base.Status = Status.Stop;
		StatusLock.ExitWriteLock();
		if (Client != null && ((IrcConnection)Client).IsConnected)
		{
			((IrcCommands)Client).SendMessage((SendType)0, Nick, "Exit");
		}
	}

	public override ProfileBase DeepCopy()
	{
		IRCProfile iRCProfile = JsonConvert.DeserializeObject<IRCProfile>(JsonConvert.SerializeObject((object)this));
		iRCProfile.Status = Status.Stop;
		iRCProfile.State = "";
		return iRCProfile;
	}

	public void PostMsg(string msg, string who)
	{
		if (Client != null)
		{
			((IrcCommands)Client).SendMessage((SendType)0, who, msg);
		}
	}

	private void OnMessage(object sender, IrcEventArgs e)
	{
		bool flag = false;
		string text = "";
		string text2 = "";
		if (e.Data.MessageArray == null || e.Data.MessageArray.Length == 0)
		{
			return;
		}
		text = ((string.IsNullOrEmpty(Prefix) || !e.Data.MessageArray[0].Contains(Prefix)) ? e.Data.MessageArray[0] : e.Data.MessageArray[0].Replace(Prefix, ""));
		text = text.Trim().ToLower();
		if (text.Equals("identify") && e.Data.MessageArray[1] == Confirmation)
		{
			Controller = e.Data.Nick;
			((IrcCommands)Client).SendMessage((SendType)2, e.Data.Nick, "You have been Identified");
			flag = true;
		}
		if (e.Data.Nick == Controller && !flag)
		{
			string text3 = e.Data.RawMessage.Substring(e.Data.RawMessage.IndexOf(e.Data.MessageArray[0]) + e.Data.MessageArray[0].Length).Trim();
			ICollection<ProfileBase> collection = null;
			if (text3 != null && text3.Length > 0)
			{
				if (Program.ProfileList.ContainsKey(text3.ToLower()))
				{
					collection = new List<ProfileBase> { Program.ProfileList[text3.ToLower()] };
				}
				else if (text3.ToLower().Equals("all"))
				{
					collection = ((IEnumerable<ProfileBase>)Program.Runtime.Values.OfType<D2Profile>()).ToList();
				}
			}
			switch (text.Trim().ToLower())
			{
			case "list":
			{
				List<StringBuilder> list = new List<StringBuilder>();
				StringBuilder stringBuilder = new StringBuilder(400);
				foreach (ProfileBase value in Program.ProfileList.Values)
				{
					if (stringBuilder.Length + value.Name.Length + 2 < 400)
					{
						stringBuilder.Append(value.Name + ", ");
						continue;
					}
					list.Add(stringBuilder);
					stringBuilder = new StringBuilder(505);
				}
				if (stringBuilder.Length > 2)
				{
					stringBuilder.Length -= 2;
				}
				list.Add(stringBuilder);
				for (int i = 0; i < list.Count; i++)
				{
					((IrcCommands)Client).SendMessage((SendType)0, MsgChannel ? e.Data.Channel : e.Data.Nick, list[i].ToString());
				}
				flag = true;
				break;
			}
			case "status":
				if (collection != null)
				{
					foreach (ProfileBase item in collection)
					{
						if (item is D2Profile d2Profile4)
						{
							string text4 = "Profile: " + d2Profile4.Name + " Status: " + d2Profile4.Status.ToString() + " Runs: " + d2Profile4.Runs + " Chickens: " + d2Profile4.Chickens + " Deaths: " + d2Profile4.Deaths + " Crashes: " + d2Profile4.Crashes + " Restarts: " + d2Profile4.Restarts;
							((IrcCommands)Client).SendMessage((SendType)0, MsgChannel ? e.Data.Channel : e.Data.Nick, text4);
						}
					}
				}
				flag = true;
				break;
			case "start":
				Program.GM.StartProfiles(new List<ProfileBase>(collection));
				text2 = "Profile(s) started";
				flag = true;
				break;
			case "stop":
				Program.GM.StopProfiles(new List<ProfileBase>(collection));
				text2 = "Profile(s) stopped";
				flag = true;
				break;
			case "mule":
				if (collection != null)
				{
					foreach (ProfileBase item2 in collection)
					{
						if (item2 is D2Profile { Status: Status.Run, D2Process: not null } d2Profile2)
						{
							Program.SendCopyData(d2Profile2.D2Process.Handle, "mule", IntPtr.Zero);
						}
					}
				}
				text2 = "Mule request(s) sent";
				flag = true;
				break;
			case "startschedule":
				if (collection != null)
				{
					foreach (ProfileBase item3 in collection)
					{
						if (item3 is D2Profile d2Profile5)
						{
							d2Profile5.ScheduleEnable = true;
						}
					}
				}
				text2 = "Schedule(s) enabled";
				flag = true;
				break;
			case "stopschedule":
				if (collection != null)
				{
					foreach (ProfileBase item4 in collection)
					{
						if (item4 is D2Profile d2Profile3)
						{
							d2Profile3.ScheduleEnable = false;
						}
					}
				}
				text2 = "Schedule(s) disabled";
				flag = true;
				break;
			case "restart":
				if (collection != null)
				{
					foreach (ProfileBase item5 in collection)
					{
						if (item5 is D2Profile d2Profile)
						{
							d2Profile.Restart();
						}
					}
				}
				text2 = "Profile(s) restarted";
				flag = true;
				break;
			}
		}
		if (!flag)
		{
			Program.IRCEventMsg(JsonConvert.SerializeObject((object)e.Data), new IntPtr(1041));
		}
		if (text2.Length > 0)
		{
			((IrcCommands)Client).SendMessage((SendType)0, MsgChannel ? e.Data.Channel : e.Data.Nick, text2);
		}
	}

	public override void ShowEditor(bool init)
	{
		IRCProfileEditor iRCProfileEditor = new IRCProfileEditor();
		iRCProfileEditor.ProfileToEdit = this;
		iRCProfileEditor.IsNew = init;
		iRCProfileEditor.Show();
	}
}
