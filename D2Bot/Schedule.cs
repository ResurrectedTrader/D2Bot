using System.Collections.Generic;

namespace D2Bot;

public class Schedule : INamedItem
{
	public string Name { get; set; }

	public List<Period> Times { get; set; }

	public Schedule(string name)
	{
		Name = name;
		Times = new List<Period>();
	}
}
