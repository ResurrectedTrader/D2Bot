using System.Collections.Generic;
using System.Windows.Forms;

namespace D2Bot;

public class NodeTag
{
	public string Path { get; set; }

	public List<ListViewItem> Items { get; set; }

	public Dictionary<string, List<WebItem>> Cache { get; set; }

	public NodeTag(string path)
	{
		Path = path;
	}

	public static Dictionary<string, List<WebItem>> CreateWebCache()
	{
		Dictionary<string, List<WebItem>> dictionary = new Dictionary<string, List<WebItem>>();
		if (Program.WF != null)
		{
			string[] filters = Program.WF.filters;
			foreach (string key in filters)
			{
				dictionary[key] = new List<WebItem>(10);
			}
		}
		return dictionary;
	}
}
