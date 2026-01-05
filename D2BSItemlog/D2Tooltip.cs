using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using D2Bot;

namespace D2BSItemlog;

public class D2Tooltip : ToolTip
{
	public Padding Margin { get; set; }

	public Font Font { get; set; }

	public Image Item { get; set; }

	private string Text { get; set; }

	private bool IsCentered { get; set; }

	private Point Location { get; set; }

	public D2Tooltip(IContainer dummy)
	{
		base.OwnerDraw = true;
		base.UseFading = true;
		base.BackColor = Color.Black;
		Margin = new Padding(7, 7, 7, 7);
		base.Draw += D2Tooltip_Draw;
		base.Popup += D2Tooltip_PopUp;
	}

	public static string ReplaceColorCodes(string desc)
	{
		Regex regex = new Regex("ÿc[0-9]", RegexOptions.IgnoreCase);
		string input = new Regex("ÿ#[0-9A-F]{6}", RegexOptions.IgnoreCase).Replace(desc, "");
		return regex.Replace(input, "");
	}

	public void ShowD2Tooltip(string text, IWin32Window window, int top, int left, int x, int y, bool isCentered)
	{
		D2ToolTip_Dispose();
		Item = Program.CurrentItem.GenerateToolTip();
		Text = text;
		IsCentered = isCentered;
		Location = new Point(Math.Min(Screen.PrimaryScreen.Bounds.Width - left - Item.Width - 20, x + 25), Math.Min(Screen.PrimaryScreen.Bounds.Width - top - Item.Height + 5, y - Item.Height - 20));
		Show(text, window, Location);
	}

	private void D2Tooltip_Draw(object sender, DrawToolTipEventArgs e)
	{
		e.Graphics.Clear(Color.Black);
		e.Graphics.DrawRectangle(new Pen(Color.Black, 2f), e.Bounds);
		e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
		e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
		e.Graphics.DrawImage(Item, new Point(0, 0));
	}

	private void D2Tooltip_PopUp(object sender, PopupEventArgs e)
	{
		e.ToolTipSize = new Size(Item.Width, Item.Height);
	}

	private void D2ToolTip_Dispose()
	{
		if (Item != null)
		{
			Item.Dispose();
		}
	}
}
