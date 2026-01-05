using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace D2Bot;

public class WebItem
{
	private const string Format = "{{\"code\":\"{0}\",\"color\":{1},\"sockets\":[{2}]}}";

	public bool lod { get; set; }

	public bool sc { get; set; }

	public bool ladder { get; set; }

	public string account { get; set; }

	public string character { get; set; }

	public string description { get; set; }

	public string image { get; set; }

	private Item item { get; set; }

	public WebItem(string header, string description, Item item)
	{
		this.description = description;
		this.item = item;
		string[] array = header.Split('/');
		if (array.Length == 3 && array[2].Length > 2)
		{
			int length = array[2].Length;
			sc = array[2][length - 3] == 'S';
			lod = array[2][length - 2] == 'E';
			ladder = array[2][length - 1] == 'L';
		}
		if (array.Length > 1)
		{
			character = array[1].Trim();
		}
		if (array.Length != 0)
		{
			account = array[0].Trim();
		}
		StringBuilder stringBuilder = new StringBuilder("");
		if (item.Socketed != null)
		{
			for (int i = 0; i < item.Socketed.Count; i++)
			{
				stringBuilder.Append('"');
				stringBuilder.Append(item.Socketed[i].Code);
				stringBuilder.Append('"');
				if (i != item.Socketed.Count - 1)
				{
					stringBuilder.Append(",");
				}
			}
		}
		image = $"{{\"code\":\"{item.Code}\",\"color\":{item.Color},\"sockets\":[{stringBuilder.ToString()}]}}";
	}

	public WebItem GenerateImage()
	{
		try
		{
			Image obj = ItemScreenShot.CreateImage(item);
			MemoryStream memoryStream = new MemoryStream();
			obj.Save(memoryStream, ImageFormat.Png);
			image = Convert.ToBase64String(memoryStream.ToArray());
			obj.Dispose();
			memoryStream.Dispose();
		}
		catch (Exception e)
		{
			Program.LogCrash(e, "Item: " + item.Description, show: false);
		}
		return this;
	}
}
