using System;
using System.Collections.Generic;
using System.Linq;

namespace D2Bot;

internal class KeylistEditor : IListData
{
	string IListData.WindowTitle => "Key List Editor";

	string IListData.ListTitle => "Key List Name";

	string[] IListData.Columns => new string[3] { "Name", "Classic", "Expansion" };

	List<string> IListData.ListNames
	{
		get
		{
			if (Program.KeyLists.Keys.Count > 0)
			{
				return Program.KeyLists.Keys.ToList();
			}
			return null;
		}
	}

	void IListData.DeleteList(string name)
	{
		Program.KeyLists.TryRemove(name.ToLower(), out var _);
	}

	List<string[]> IListData.GetColumnData(string name)
	{
		KeyList keyList = Program.GM.GetKeyList(name);
		List<string[]> list = new List<string[]>(keyList.CDKeys.Count);
		if (keyList.CDKeys.Count > 0)
		{
			for (int i = 0; i < keyList.CDKeys.Count; i++)
			{
				list.Add(new string[3]
				{
					keyList.CDKeys[i].Name,
					keyList.CDKeys[i].Classic,
					keyList.CDKeys[i].Expansion
				});
			}
		}
		return list;
	}

	void IListData.SaveListData(string name, List<string[]> data)
	{
		KeyList keyList = Program.GM.GetKeyList(name);
		keyList.Clear();
		for (int i = 0; i < data.Count; i++)
		{
			if (data[i].Length == 3 && keycheck(data[i][1], out var key) && keycheck(data[i][2], out var key2))
			{
				keyList.AddKey(new CDKey(data[i][0], key, key2));
				continue;
			}
			keyList.Clear();
			throw new Exception("Invalid Key Format. Please check inputs!");
		}
		Program.SaveKeyLists(name.ToLower());
	}

	private bool keycheck(string v, out string key)
	{
		key = v.Replace("-", "").Replace(" ", "");
		if (key.Length == 0 || key.Length == 16 || key.Length == 26)
		{
			return true;
		}
		return false;
	}

	bool IListData.Contains(string name)
	{
		return Program.KeyLists.ContainsKey(name.ToLower());
	}
}
