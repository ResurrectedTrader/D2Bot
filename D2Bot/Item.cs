using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace D2Bot;

public class Item
{
	public static object locker = new object();

	public List<Item> Socketed;

	public int X { get; set; }

	public int Y { get; set; }

	public int Top { get; set; }

	public int Left { get; set; }

	public int Width { get; set; }

	public int Height { get; set; }

	public int Color { get; set; }

	public int TextColor { get; set; }

	public string Code { get; set; }

	public string Name { get; set; }

	public string Description { get; set; }

	public string Searcheable { get; set; }

	public string Header { get; set; }

	public string Path { get; set; }

	public List<Item> Socket(string item)
	{
		if (Socketed == null)
		{
			Socketed = new List<Item>();
		}
		Item item2 = new Item
		{
			Code = item,
			Color = -1,
			Name = "soc",
			Description = "soc"
		};
		if (item.Contains("|"))
		{
			item2.Code = item.Split('|')[0];
			item2.Color = int.Parse(item.Split('|')[1]);
		}
		Socketed.Add(item2);
		return Socketed;
	}

	public ListViewItem ListViewItem(string meta, string info)
	{
		ListViewItem listViewItem = new ListViewItem
		{
			UseItemStyleForSubItems = false
		};
		string toolTipText = Description.Split('$')[0];
		string text = Regex.Replace(Name, "^[\\d]*", "");
		if (meta.Length > 0)
		{
			listViewItem.SubItems.Add("");
			listViewItem.SubItems[0].Text = meta;
			listViewItem.SubItems[1].Text = info + text;
			listViewItem.SubItems[1].ForeColor = Main.ColorFromInt(TextColor);
		}
		else
		{
			listViewItem.SubItems[0].Text = text;
			listViewItem.SubItems[0].ForeColor = Main.ColorFromInt(TextColor);
		}
		listViewItem.ToolTipText = toolTipText;
		listViewItem.Tag = this;
		return listViewItem;
	}

	internal Image GenerateToolTip()
	{
		lock (locker)
		{
			return ItemScreenShot.Take(this, Save: false);
		}
	}

	public WebItem ToWebItem()
	{
		return new WebItem(Header, Description, this);
	}
}
