namespace D2Bot;

public class D2Item
{
	public int itemColor { get; set; }

	public int textColor { get; set; }

	public string image { get; set; }

	public string title { get; set; }

	public string header { get; set; }

	public string description { get; set; }

	public string[] sockets { get; set; }

	public Item ToItem()
	{
		string text = description.Replace("\\n", "\n").Replace("\\xff", "ÿ");
		Item item = new Item
		{
			Color = itemColor,
			TextColor = textColor,
			Name = title,
			Description = text,
			Searcheable = text.ToLower().Replace('\n', ' '),
			Header = header,
			Code = image
		};
		if (sockets != null)
		{
			for (int i = 0; i < sockets.Length; i++)
			{
				item.Socket(sockets[i]);
			}
		}
		return item;
	}
}
