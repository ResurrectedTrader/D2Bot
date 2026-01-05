using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace D2Bot;

public class D2Profile : ProfileBase
{
	[JsonIgnore]
	private bool m_scheduleEnable;

	public string Account { get; set; }

	public string Password { get; set; }

	public string Character { get; set; }

	public string GameName { get; set; }

	public string GamePass { get; set; }

	public string D2Path { get; set; }

	public string Realm { get; set; }

	public string Mode { get; set; }

	public string Difficulty { get; set; }

	public string Parameters { get; set; }

	public string Entry { get; set; }

	public string Location { get; set; }

	public string KeyList { get; set; }

	public string Schedule { get; set; }

	public int GameCount { get; set; }

	public int Runs { get; set; }

	public int Chickens { get; set; }

	public int Deaths { get; set; }

	public int Crashes { get; set; }

	public int Restarts { get; set; }

	public int RunsPerKey { get; set; }

	public int KeyRuns { get; set; }

	public string InfoTag { get; set; }

	public bool Visible { get; set; }

	public bool SwitchKeys { get; set; }

	public bool ScheduleEnable
	{
		get
		{
			return m_scheduleEnable;
		}
		set
		{
			if (!value && m_scheduleEnable != value)
			{
				Stop();
			}
			m_scheduleEnable = value;
		}
	}

	[JsonIgnore]
	public string GameExe => Path.GetFileName(D2Path);

	[JsonIgnore]
	public CDKey CurrentKey { get; set; }

	[JsonIgnore]
	public string Key
	{
		get
		{
			if (CurrentKey != null)
			{
				return CurrentKey.Name;
			}
			return "";
		}
	}

	[JsonIgnore]
	public int HeartAttack { get; set; }

	[JsonIgnore]
	public int Crashed { get; set; }

	[JsonIgnore]
	public int NoResponse { get; set; }

	[JsonIgnore]
	public bool Error { get; set; }

	[JsonIgnore]
	public Process D2Process { get; set; }

	[JsonIgnore]
	public IntPtr MainWindowHandle { get; set; }

	[JsonIgnore]
	public Thread Client { get; set; }

	[JsonIgnore]
	public bool IRCEvent { get; set; }

	public override ProfileType Type => ProfileType.D2;

	public bool IncrementKey()
	{
		KeyList keyList = Program.GM.GetKeyList(KeyList);
		if (keyList == null)
		{
			return false;
		}
		CDKey currentKey = CurrentKey;
		CurrentKey = keyList.GetAndIncrement(currentKey);
		if (currentKey == CurrentKey)
		{
			MessageBox.Show("All keys in this keylist are in use or on hold. Please add more keys or resume paused keys.");
			currentKey?.InUse(value: false);
			CurrentKey = null;
			Program.GM.UpdateMPQ(this);
			return false;
		}
		Program.GM.UpdateMPQ(this);
		return true;
	}

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
		if (!string.IsNullOrEmpty(KeyList) && CurrentKey == null && !IncrementKey())
		{
			Stop();
			return;
		}
		try
		{
			Program.LaunchClient(this);
			StatusLock.EnterWriteLock();
			base.Status = Status.Run;
			StatusLock.ExitWriteLock();
		}
		catch (ThreadAbortException)
		{
		}
		catch (Exception ex2)
		{
			PrintMessage pm = new PrintMessage(ex2.Message);
			Program.GM.ConsolePrint(pm, this);
			StopClient();
		}
	}

	public void StopClient()
	{
		int num = 0;
		while (base.Status == Status.Starting && num != 25)
		{
			Thread.Sleep(100);
			num++;
		}
		if (Thread.CurrentThread != base.Handle && base.Handle != null && base.Handle.IsAlive)
		{
			base.Handle.Abort();
		}
		if (D2Process != null)
		{
			if (D2Process != null && D2Process != null && !D2Process.HasExited)
			{
				D2Process.CloseMainWindow();
				for (num = 0; num < 10; num++)
				{
					if (D2Process == null)
					{
						break;
					}
					if (D2Process.HasExited)
					{
						break;
					}
					Thread.Sleep(100);
				}
				try
				{
					D2Process.Kill();
				}
				catch
				{
				}
			}
			D2Process = null;
			HeartAttack = 0;
			Crashed = 0;
		}
		Program.Runtime.TryRemove(MainWindowHandle, out var _);
		StatusLock.EnterWriteLock();
		base.Status = Status.Stop;
		StatusLock.ExitWriteLock();
	}

	public override void Stop()
	{
		Program.GM.Queue[Math.Abs(base.Name.GetHashCode()) % 10].Add(new Worker(IntPtr.Zero, null, Main.ProfileAction.Stop, this));
	}

	public void StopThread()
	{
		StatusLock.EnterUpgradeableReadLock();
		if (base.Status == Status.Busy || base.Status == Status.Stop)
		{
			StatusLock.ExitUpgradeableReadLock();
			return;
		}
		StatusLock.EnterWriteLock();
		base.Status = Status.Busy;
		StatusLock.ExitWriteLock();
		StatusLock.ExitUpgradeableReadLock();
		StopClient();
	}

	public void Restart(bool increment = false)
	{
		StatusLock.EnterUpgradeableReadLock();
		if (base.Status == Status.Busy)
		{
			StatusLock.ExitUpgradeableReadLock();
			return;
		}
		StatusLock.EnterWriteLock();
		base.Status = Status.Busy;
		StatusLock.ExitWriteLock();
		StatusLock.ExitUpgradeableReadLock();
		Thread thread = new Thread((ThreadStart)delegate
		{
			RestartClient(increment);
		});
		thread.IsBackground = true;
		thread.Start();
	}

	private void RestartClient(bool increment)
	{
		StopClient();
		if (increment)
		{
			KeyRuns = 0;
			if (!IncrementKey())
			{
				Stop();
				return;
			}
		}
		Program.GM.UpdateRestarts(this);
		StatusLock.EnterWriteLock();
		base.Status = Status.Starting;
		StatusLock.ExitWriteLock();
		Start(0);
		StatusLock.EnterWriteLock();
		base.Status = Status.Run;
		StatusLock.ExitWriteLock();
	}

	public void HeartBeat()
	{
		HeartAttack = 0;
	}

	public void ReleaseKey()
	{
		if (CurrentKey != null)
		{
			CurrentKey.InUse(value: false);
			CurrentKey = null;
			Program.GM.UpdateMPQ(this);
		}
	}

	public override ProfileBase DeepCopy()
	{
		D2Profile d2Profile = JsonConvert.DeserializeObject<D2Profile>(JsonConvert.SerializeObject((object)this));
		d2Profile.CurrentKey = null;
		d2Profile.Status = Status.Stop;
		d2Profile.State = "";
		d2Profile.D2Process = null;
		return d2Profile;
	}

	public void ShowWindow()
	{
		Program.GM.Queue[Math.Abs(base.Name.GetHashCode()) % 10].Add(new Worker(IntPtr.Zero, null, Main.ProfileAction.Show, this));
	}

	public void ShowWindowThread()
	{
		IntPtr mainWindowHandle;
		if (D2Process == null || (mainWindowHandle = D2Process.MainWindowHandle) == IntPtr.Zero)
		{
			return;
		}
		if (Visible)
		{
			if (!(Location == ""))
			{
				try
				{
					int x = int.Parse(Location.Split(',')[0].Trim());
					int y = int.Parse(Location.Split(',')[1].Trim());
					int num = 0;
					while (num < 20 && base.Status != Status.Run)
					{
						num++;
						Thread.Sleep(50);
					}
					Thread.Sleep(100);
					MessageHelper.ShowWindow(mainWindowHandle.ToInt32(), 1);
					MessageHelper.SetWindowPos(mainWindowHandle, IntPtr.Zero, x, y, 10, 10, 65);
					return;
				}
				catch
				{
					MessageHelper.ShowWindow(mainWindowHandle.ToInt32(), 1);
					return;
				}
			}
			MessageHelper.ShowWindow(D2Process.MainWindowHandle.ToInt32(), 1);
		}
		else
		{
			MessageHelper.ShowWindow(D2Process.MainWindowHandle.ToInt32(), 6);
		}
	}

	public void HideWindow()
	{
		Program.GM.Queue[Math.Abs(base.Name.GetHashCode()) % 10].Add(new Worker(IntPtr.Zero, null, Main.ProfileAction.Hide, this));
	}

	public void HideWindowThread()
	{
		IntPtr mainWindowHandle;
		if (D2Process == null || (mainWindowHandle = D2Process.MainWindowHandle) == IntPtr.Zero)
		{
			return;
		}
		try
		{
			if (!MessageHelper.IsIconic(mainWindowHandle))
			{
				MessageHelper.ShowWindow(mainWindowHandle.ToInt32(), 6);
			}
			MessageHelper.ShowWindow(mainWindowHandle.ToInt32(), 0);
		}
		catch
		{
		}
	}

	public override void ShowEditor(bool init)
	{
		D2ProfileEditor d2ProfileEditor = new D2ProfileEditor
		{
			ProfileToEdit = this,
			IsNew = init
		};
		d2ProfileEditor.Show();
		d2ProfileEditor.DiabloPath.Select(d2ProfileEditor.DiabloPath.Text.Length, 0);
		d2ProfileEditor.EntryScript.Select(d2ProfileEditor.EntryScript.Text.Length, 0);
	}
}
