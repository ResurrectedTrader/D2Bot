namespace D2Bot;

public class PrintMessage
{
	public string msg { get; set; }

	public string tooltip { get; set; }

	public string trigger { get; set; }

	public int color { get; set; }

	public PrintMessage(string message, string tooltip = "", string trigger = "", int color = 0)
	{
		msg = message;
		this.tooltip = tooltip;
		this.trigger = trigger;
		this.color = color;
	}
}
