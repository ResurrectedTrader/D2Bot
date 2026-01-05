using System.Collections.Generic;

namespace D2Bot;

public interface IListData
{
	string WindowTitle { get; }

	string ListTitle { get; }

	string[] Columns { get; }

	List<string> ListNames { get; }

	List<string[]> GetColumnData(string name);

	void SaveListData(string name, List<string[]> data);

	void DeleteList(string name);

	bool Contains(string name);
}
