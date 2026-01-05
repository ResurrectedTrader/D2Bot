using System;

namespace D2Bot;

public class GameInfo
{
	public int handle { get; set; }

	public string profile { get; set; }

	public string mpq { get; set; }

	public string gameName { get; set; }

	public string gamePass { get; set; }

	public string difficulty { get; set; }

	public string stopTime { get; set; }

	public bool error { get; set; }

	public bool switchKeys { get; set; }

	public bool rdBlocker { get; set; }

	public GameInfo(D2Profile p)
	{
		Program.GM.GetSchedule(p.Schedule);
		mpq = ((p.CurrentKey == null) ? "" : p.CurrentKey.Name);
		profile = p.Name;
		handle = Program.Handle.ToInt32();
		gameName = p.GameName;
		gamePass = p.GamePass;
		difficulty = p.Difficulty;
		error = p.Error;
		stopTime = StopTime(p);
		if (p.KeyList == null || p.KeyList.Length < 2)
		{
			switchKeys = false;
		}
		else if (Program.GM.GetKeyList(p.KeyList).CDKeys.Count > 1)
		{
			switchKeys = p.SwitchKeys;
		}
		else
		{
			switchKeys = false;
		}
		rdBlocker = false;
	}

	private string StopTime(D2Profile p)
	{
		if (p.ScheduleEnable)
		{
			TimeSpan timeSpan = new TimeSpan(0, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
			Schedule schedule = Program.GM.GetSchedule(p.Schedule);
			for (int i = 0; i < schedule.Times.Count; i += 2)
			{
				TimeSpan period = schedule.Times[i].GetPeriod();
				TimeSpan period2 = schedule.Times[i + 1].GetPeriod();
				if (period > period2 && (period <= timeSpan || timeSpan < period2))
				{
					return period2.ToString();
				}
				if ((period <= timeSpan && period2 > timeSpan) || period == period2)
				{
					return period2.ToString();
				}
			}
		}
		return "";
	}
}
