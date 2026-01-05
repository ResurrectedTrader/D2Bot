using System;
using System.IO;
using System.Threading;
using Newtonsoft.Json;

namespace D2Bot;

public abstract class ProfileBase : INamedItem
{
	private string m_name = string.Empty;

	[JsonIgnore]
	public ReaderWriterLockSlim StatusLock = new ReaderWriterLockSlim();

	[JsonIgnore]
	public readonly object Lock = new object();

	[JsonIgnore]
	private Status m_status = Status.Stop;

	public string Name
	{
		get
		{
			return m_name;
		}
		set
		{
			m_name = value.Trim();
		}
	}

	public string Group { get; set; }

	public abstract ProfileType Type { get; }

	[JsonIgnore]
	public Thread Handle { get; set; }

	[JsonIgnore]
	public Status Status
	{
		get
		{
			return m_status;
		}
		set
		{
			m_status = value;
			switch (value)
			{
			case Status.Busy:
				Program.GM.SetStatus(this, "Busy");
				break;
			case Status.Run:
				Program.GM.SetStatus(this, "Running");
				break;
			case Status.Stop:
				Program.GM.SetStatus(this, "Stopped");
				break;
			case Status.Starting:
				break;
			}
		}
	}

	[JsonIgnore]
	public string State { get; set; }

	[JsonIgnore]
	public bool NeedsUpdate { get; set; }

	public void Add(bool rand = false)
	{
		Program.GM.objectLock.WaitOne();
		State = "Stopped";
		Status = Status.Stop;
		if (rand && Type == ProfileType.D2)
		{
			(this as D2Profile).GameName = Guid.NewGuid().ToString().Replace("-", string.Empty)
				.Substring(0, 5) + "-";
			(this as D2Profile).GamePass = Guid.NewGuid().ToString().Replace("-", string.Empty)
				.Substring(0, 3);
		}
		if (Program.ProfileList.TryAdd(Name.ToLower(), this))
		{
			Program.GM.objectProfileList.AddObject((object)this);
		}
		Program.GM.objectLock.ReleaseMutex();
	}

	public void Remove()
	{
		Program.GM.objectLock.WaitOne();
		if (Program.ProfileList.TryRemove(Name.ToLower(), out var _))
		{
			Program.GM.objectProfileList.RemoveObject((object)this);
		}
		Program.GM.objectLock.ReleaseMutex();
	}

	public abstract void Stop();

	internal abstract void Start(int delay);

	public abstract void ShowEditor(bool init);

	public abstract ProfileBase DeepCopy();

	public void Load(int delay = 0)
	{
		if (!StatusLock.TryEnterUpgradeableReadLock(0))
		{
			return;
		}
		if (Status != Status.Stop)
		{
			StatusLock.ExitUpgradeableReadLock();
			return;
		}
		StatusLock.EnterWriteLock();
		Status = Status.Starting;
		StatusLock.ExitWriteLock();
		StatusLock.ExitUpgradeableReadLock();
		ThreadStart start = delegate
		{
			Start(delay);
		};
		Handle = new Thread(start)
		{
			IsBackground = true
		};
		Handle.Start();
	}

	public D2ProfileExport Export()
	{
		if (Type != ProfileType.D2)
		{
			return null;
		}
		return new D2ProfileExport
		{
			Name = (this as D2Profile).Name,
			Status = (this as D2Profile).State,
			Account = (this as D2Profile).Account,
			Character = (this as D2Profile).Character,
			Difficulty = (this as D2Profile).Difficulty,
			Realm = (this as D2Profile).Realm,
			Game = (this as D2Profile).GameExe,
			Entry = Path.GetFileName((this as D2Profile).Entry),
			Tag = (this as D2Profile).InfoTag
		};
	}
}
