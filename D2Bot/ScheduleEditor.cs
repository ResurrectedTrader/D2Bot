using System;
using System.Collections.Generic;
using System.Linq;

namespace D2Bot;

internal class ScheduleEditor : IListData
{
	string IListData.WindowTitle => "Schedule Editor";

	string IListData.ListTitle => "Schedule Name";

	string[] IListData.Columns => new string[2] { "Start", "End" };

	List<string> IListData.ListNames
	{
		get
		{
			if (Program.Schedules.Keys.Count > 0)
			{
				return Program.Schedules.Keys.ToList();
			}
			return null;
		}
	}

	void IListData.DeleteList(string name)
	{
		Program.Schedules.TryRemove(name.ToLower(), out var _);
	}

	List<string[]> IListData.GetColumnData(string name)
	{
		Schedule schedule = Program.GM.GetSchedule(name);
		List<string[]> list = new List<string[]>(schedule.Times.Count);
		if (schedule.Times.Count > 0)
		{
			for (int i = 0; i < schedule.Times.Count; i += 2)
			{
				list.Add(new string[2]
				{
					schedule.Times[i].GetPeriod().ToString(),
					schedule.Times[i + 1].GetPeriod().ToString()
				});
			}
		}
		return list;
	}

	void IListData.SaveListData(string name, List<string[]> data)
	{
		Schedule schedule = Program.GM.GetSchedule(name);
		schedule.Times.Clear();
		for (int i = 0; i < data.Count; i++)
		{
			if (data[i].Length == 2 && data[i][0].Contains(":") && data[i][1].Contains(":"))
			{
				string[] array = data[i][0].Split(':');
				string[] array2 = data[i][1].Split(':');
				schedule.Times.Add(new Period(int.Parse(array[0]), int.Parse(array[1])));
				schedule.Times.Add(new Period(int.Parse(array2[0]), int.Parse(array2[1])));
				continue;
			}
			schedule.Times.Clear();
			throw new Exception("Invalid Schedule Format. Please check inputs!");
		}
		Program.SaveSchedules(name.ToLower());
	}

	bool IListData.Contains(string name)
	{
		return Program.Schedules.ContainsKey(name.ToLower());
	}
}
