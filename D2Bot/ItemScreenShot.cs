using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using D2Bot.Properties;

namespace D2Bot;

public static class ItemScreenShot
{
	public static D2Font F16 = new D2Font();

	public static Font SysFont = new Font(SystemFonts.DefaultFont, FontStyle.Regular);

	public static object gdiLock = new object();

	private static readonly Color COLOR_INVALID = Color.FromArgb(255, 220, 62, 71);

	private static readonly Color COLOR_NORMAL = Color.FromArgb(245, 192, 192, 192);

	private static readonly Color COLOR_ETHEREAL = Color.FromArgb(255, 96, 96, 96);

	private static readonly Color COLOR_SET = Color.FromArgb(255, 39, 182, 58);

	private static readonly Color COLOR_MAGIC = Color.FromArgb(245, 78, 77, 157);

	private static readonly Color COLOR_UNIQUE = Color.FromArgb(255, 150, 135, 115);

	private static readonly Color COLOR_CRAFTED = Color.FromArgb(255, 255, 140, 0);

	private static readonly Color COLOR_RARE = Color.FromArgb(245, 219, 189, 87);

	public static Color[] TextColors = new Color[13]
	{
		Color.FromArgb(255, 255, 255),
		Color.FromArgb(255, 77, 77),
		Color.FromArgb(0, 255, 0),
		Color.FromArgb(105, 105, 255),
		Color.FromArgb(199, 179, 119),
		Color.FromArgb(105, 105, 105),
		Color.FromArgb(0, 0, 0),
		Color.FromArgb(208, 194, 125),
		Color.FromArgb(255, 168, 0),
		Color.FromArgb(255, 255, 100),
		Color.FromArgb(0, 128, 0),
		Color.FromArgb(174, 0, 255),
		Color.FromArgb(0, 200, 0)
	};

	public static Size MeasureString(string text, Font font)
	{
		if (text == "")
		{
			return new Size(0, 0);
		}
		StringFormat stringFormat = new StringFormat(StringFormat.GenericDefault);
		RectangleF layoutRect = new RectangleF(0f, 0f, 1000f, 1000f);
		CharacterRange[] measurableCharacterRanges = new CharacterRange[1]
		{
			new CharacterRange(0, text.Length)
		};
		_ = new Region[1];
		Graphics graphics = Graphics.FromImage(new Bitmap(1, 1));
		int num = 0;
		stringFormat.SetMeasurableCharacterRanges(measurableCharacterRanges);
		layoutRect = graphics.MeasureCharacterRanges(text, font, layoutRect, stringFormat)[0].GetBounds(graphics);
		if (text.Substring(text.Length - 1, 1) == " ")
		{
			num++;
		}
		return new Size((int)layoutRect.Right + num, 0);
	}

	public static string ReplaceColorCodes(string desc)
	{
		Regex regex = new Regex("ÿc[0-9:;<]", RegexOptions.IgnoreCase);
		string input = new Regex("ÿ#[0-9A-F]{6}", RegexOptions.IgnoreCase).Replace(desc, "");
		return regex.Replace(input, "");
	}

	public static Image CreateImage(Item item)
	{
		DirectBitmap image = new D2Palette(ref item).GetImage(ref item, Color.Transparent);
		Bitmap bitmap = new Bitmap(item.X * 30 - 1, item.Y * 30 - 1);
		Graphics graphics = Graphics.FromImage(bitmap);
		graphics.DrawImage(image.Bitmap, new Point((bitmap.Width - image.Width) / 2, (bitmap.Height - image.Height) / 2));
		if (item.Socketed != null)
		{
			int num = (bitmap.Width - image.Width) / 2;
			int num2 = 14;
			int num3 = num;
			int num4 = num3 + num2;
			int num5 = num4 + num2;
			int num6 = 2;
			int num7 = num6 + num2 * 2 + 1;
			int num8 = num7 + num2 * 2 + 1;
			int num9 = num8 + num2 * 2 + 1;
			int num10 = 0;
			int num11 = -1;
			switch (item.Socketed.Count)
			{
			case 1:
				if (item.Y == 2)
				{
					if (item.X == 1)
					{
						DrawSocketItem(graphics, item.Socketed[0], num3 + num10, num6 + num2 + num11);
					}
					else
					{
						DrawSocketItem(graphics, item.Socketed[0], num4 + num10, num6 + num2 + num11);
					}
				}
				else if (item.Y == 3)
				{
					if (item.X == 1)
					{
						DrawSocketItem(graphics, item.Socketed[0], num3 + num10, num7 + num11);
					}
					else
					{
						DrawSocketItem(graphics, item.Socketed[0], num4 + num10, num7 + num11);
					}
				}
				else if (item.X == 1)
				{
					DrawSocketItem(graphics, item.Socketed[0], num3 + num10, num7 + num2 + num11);
				}
				else
				{
					DrawSocketItem(graphics, item.Socketed[0], num4 + num10, num7 + num2 + num11);
				}
				break;
			case 2:
				if (item.Y == 2)
				{
					if (item.X == 1)
					{
						DrawSocketItem(graphics, item.Socketed[0], num3 + num10, num6 + num11);
						DrawSocketItem(graphics, item.Socketed[1], num3 + num10, num7 + num11);
					}
					else
					{
						DrawSocketItem(graphics, item.Socketed[0], num4 + num10, num6 + num11);
						DrawSocketItem(graphics, item.Socketed[1], num4 + num10, num7 + num11);
					}
				}
				else if (item.Y == 3)
				{
					if (item.X == 1)
					{
						DrawSocketItem(graphics, item.Socketed[0], num3 + num10, num6 + num2 + num11);
						DrawSocketItem(graphics, item.Socketed[1], num3 + num10, num7 + num2 + num11);
					}
					else
					{
						DrawSocketItem(graphics, item.Socketed[0], num4 + num10, num6 + num2 + num11);
						DrawSocketItem(graphics, item.Socketed[1], num4 + num10, num7 + num2 + num11);
					}
				}
				else if (item.X == 1)
				{
					DrawSocketItem(graphics, item.Socketed[0], num3 + num10, num6 + num2 + num11);
					DrawSocketItem(graphics, item.Socketed[1], num3 + num10, num8 + num2 + num11);
				}
				else
				{
					DrawSocketItem(graphics, item.Socketed[0], num4 + num10, num6 + num2 + num11);
					DrawSocketItem(graphics, item.Socketed[1], num4 + num10, num8 + num2 + num11);
				}
				break;
			case 3:
				if (item.Y == 2)
				{
					DrawSocketItem(graphics, item.Socketed[0], num3 + num10, num6 + num11);
					DrawSocketItem(graphics, item.Socketed[1], num5 + num10, num6 + num11);
					DrawSocketItem(graphics, item.Socketed[2], num4 + num10, num7 + num11);
				}
				else if (item.Y == 3)
				{
					if (item.X == 1)
					{
						DrawSocketItem(graphics, item.Socketed[0], num3 + num10, num6 + num11);
						DrawSocketItem(graphics, item.Socketed[1], num3 + num10, num7 + num11);
						DrawSocketItem(graphics, item.Socketed[2], num3 + num10, num8 + num11);
					}
					else
					{
						DrawSocketItem(graphics, item.Socketed[0], num4 + num10, num6 + num11);
						DrawSocketItem(graphics, item.Socketed[1], num4 + num10, num7 + num11);
						DrawSocketItem(graphics, item.Socketed[2], num4 + num10, num8 + num11);
					}
				}
				else if (item.X == 1)
				{
					DrawSocketItem(graphics, item.Socketed[0], num3 + num10, num6 + num2 + num11);
					DrawSocketItem(graphics, item.Socketed[1], num3 + num10, num7 + num2 + num11);
					DrawSocketItem(graphics, item.Socketed[2], num3 + num10, num8 + num2 + num11);
				}
				else
				{
					DrawSocketItem(graphics, item.Socketed[0], num4 + num10, num6 + num2 + num11);
					DrawSocketItem(graphics, item.Socketed[1], num4 + num10, num7 + num2 + num11);
					DrawSocketItem(graphics, item.Socketed[2], num4 + num10, num8 + num2 + num11);
				}
				break;
			case 4:
				if (item.Y == 3)
				{
					DrawSocketItem(graphics, item.Socketed[0], num3 + num10, num6 + num2 + num11);
					DrawSocketItem(graphics, item.Socketed[1], num5 + num10, num6 + num2 + num11);
					DrawSocketItem(graphics, item.Socketed[2], num3 + num10, num7 + num2 + num11);
					DrawSocketItem(graphics, item.Socketed[3], num5 + num10, num7 + num2 + num11);
				}
				else if (item.Y == 2)
				{
					DrawSocketItem(graphics, item.Socketed[0], num3 + num10, num6 + num11);
					DrawSocketItem(graphics, item.Socketed[1], num5 + num10, num6 + num11);
					DrawSocketItem(graphics, item.Socketed[2], num3 + num10, num7 + num11);
					DrawSocketItem(graphics, item.Socketed[3], num5 + num10, num7 + num11);
				}
				else if (item.X == 1)
				{
					DrawSocketItem(graphics, item.Socketed[0], num3 + num10, num6 + num11);
					DrawSocketItem(graphics, item.Socketed[1], num3 + num10, num7 + num11);
					DrawSocketItem(graphics, item.Socketed[2], num3 + num10, num8 + num11);
					DrawSocketItem(graphics, item.Socketed[3], num3 + num10, num9 + num11);
				}
				else
				{
					DrawSocketItem(graphics, item.Socketed[0], num4 + num10, num6 + num11);
					DrawSocketItem(graphics, item.Socketed[1], num4 + num10, num7 + num11);
					DrawSocketItem(graphics, item.Socketed[2], num4 + num10, num8 + num11);
					DrawSocketItem(graphics, item.Socketed[3], num4 + num10, num9 + num11);
				}
				break;
			case 5:
				if (item.Y == 3)
				{
					DrawSocketItem(graphics, item.Socketed[0], num3 + num10, num6 + num11);
					DrawSocketItem(graphics, item.Socketed[1], num5 + num10, num6 + num11);
					DrawSocketItem(graphics, item.Socketed[2], num4 + num10, num7 + num11);
					DrawSocketItem(graphics, item.Socketed[3], num3 + num10, num8 + num11);
					DrawSocketItem(graphics, item.Socketed[4], num5 + num10, num8 + num11);
				}
				else
				{
					DrawSocketItem(graphics, item.Socketed[0], num3 + num10, num6 + num2 + num11);
					DrawSocketItem(graphics, item.Socketed[1], num5 + num10, num6 + num2 + num11);
					DrawSocketItem(graphics, item.Socketed[2], num4 + num10, num7 + num2 + num11);
					DrawSocketItem(graphics, item.Socketed[3], num3 + num10, num8 + num2 + num11);
					DrawSocketItem(graphics, item.Socketed[4], num5 + num10, num8 + num2 + num11);
				}
				break;
			case 6:
				if (item.Y == 3)
				{
					DrawSocketItem(graphics, item.Socketed[0], num3 + num10, num6 + num11);
					DrawSocketItem(graphics, item.Socketed[1], num5 + num10, num6 + num11);
					DrawSocketItem(graphics, item.Socketed[2], num3 + num10, num7 + num11);
					DrawSocketItem(graphics, item.Socketed[3], num5 + num10, num7 + num11);
					DrawSocketItem(graphics, item.Socketed[4], num3 + num10, num8 + num11);
					DrawSocketItem(graphics, item.Socketed[5], num5 + num10, num8 + num11);
				}
				else
				{
					DrawSocketItem(graphics, item.Socketed[0], num3 + num10, num6 + num2 + num11);
					DrawSocketItem(graphics, item.Socketed[1], num5 + num10, num6 + num2 + num11);
					DrawSocketItem(graphics, item.Socketed[2], num3 + num10, num7 + num2 + num11);
					DrawSocketItem(graphics, item.Socketed[3], num5 + num10, num7 + num2 + num11);
					DrawSocketItem(graphics, item.Socketed[4], num3 + num10, num8 + num2 + num11);
					DrawSocketItem(graphics, item.Socketed[5], num5 + num10, num8 + num2 + num11);
				}
				break;
			}
		}
		graphics.Dispose();
		image.Dispose();
		return bitmap;
	}

	public static void Take(D2Item d2item, bool Save = true)
	{
		Take(d2item.ToItem(), Save);
	}

	public static Image Take(Item item, bool Save = true)
	{
		DirectBitmap image = new D2Palette(ref item).GetImage(ref item);
		int num = 0;
		string text = item.Description.Split('$')[0];
		string[] array = ReplaceColorCodes(text).Split('\n');
		string[] array2 = array;
		foreach (string text2 in array2)
		{
			if (text2 != "")
			{
				Size size = default(Size);
				size = ((!Settings.Default.System_Font) ? F16.MeasureString(text2) : MeasureString(text2, SysFont));
				if (size.Width > num)
				{
					num = size.Width;
				}
			}
		}
		if (num < 100)
		{
			num = 100;
		}
		if (Settings.Default.Item_Header && item.Header != null && item.Header != null)
		{
			Size size2 = MeasureString(item.Header, new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point));
			if (size2.Width > num)
			{
				num = size2.Width;
			}
		}
		int num2 = 0;
		char c = '\n';
		float num3 = (Settings.Default.System_Font ? 18 : 16);
		Bitmap bitmap = new Bitmap(num + 14, (int)num3 * array.Length + item.Top + 1 + (Settings.Default.System_Font ? 6 : 0));
		Graphics graphics = Graphics.FromImage(bitmap);
		PointF pointF = default(Point);
		string[] array3 = text.Split(c);
		Convert.ToSingle((double)((float)bitmap.Height - (float)array3.Length * num3 - (float)item.Top) / 2.0);
		Bitmap bitmap2;
		lock (gdiLock)
		{
			bitmap2 = new Bitmap(Program.ItemBG["bgnd" + item.Y]);
		}
		bitmap2.SetResolution(graphics.DpiX, graphics.DpiY);
		graphics.Clear(Color.Black);
		graphics.SmoothingMode = SmoothingMode.AntiAlias;
		graphics.DrawImage(bitmap2, new Point(bitmap.Width / 2 - item.Left, -10));
		graphics.DrawImage(image.Bitmap, new Point((bitmap.Width - image.Width) / 2, 5));
		int num4 = (bitmap.Width - image.Width) / 2;
		int num5 = num4 + 14;
		int num6 = num5 + 14;
		int num7 = 5;
		int num8 = 34;
		int num9 = 63;
		int num10 = 92;
		int num11 = 14;
		int num12 = 1;
		int num13 = -1;
		if (item.Socketed != null)
		{
			switch (item.Socketed.Count)
			{
			case 1:
				if (item.Y == 2)
				{
					if (item.X == 1)
					{
						DrawSocketItem(graphics, item.Socketed[0], num4 + num12, num7 + num11 + num13);
					}
					else
					{
						DrawSocketItem(graphics, item.Socketed[0], num5 + num12, num7 + num11 + num13);
					}
				}
				else if (item.Y == 3)
				{
					if (item.X == 1)
					{
						DrawSocketItem(graphics, item.Socketed[0], num4 + num12, num8 + num13);
					}
					else
					{
						DrawSocketItem(graphics, item.Socketed[0], num5 + num12, num8 + num13);
					}
				}
				else if (item.X == 1)
				{
					DrawSocketItem(graphics, item.Socketed[0], num4 + num12, num8 + num11 + num13);
				}
				else
				{
					DrawSocketItem(graphics, item.Socketed[0], num5 + num12, num8 + num11 + num13);
				}
				break;
			case 2:
				if (item.Y == 2)
				{
					if (item.X == 1)
					{
						DrawSocketItem(graphics, item.Socketed[0], num4 + num12, num7 + num13);
						DrawSocketItem(graphics, item.Socketed[1], num4 + num12, num8 + num13);
					}
					else
					{
						DrawSocketItem(graphics, item.Socketed[0], num5 + num12, num7 + num13);
						DrawSocketItem(graphics, item.Socketed[1], num5 + num12, num8 + num13);
					}
				}
				else if (item.Y == 3)
				{
					if (item.X == 1)
					{
						DrawSocketItem(graphics, item.Socketed[0], num4 + num12, num7 + num11 + num13);
						DrawSocketItem(graphics, item.Socketed[1], num4 + num12, num8 + num11 + num13);
					}
					else
					{
						DrawSocketItem(graphics, item.Socketed[0], num5 + num12, num7 + num11 + num13);
						DrawSocketItem(graphics, item.Socketed[1], num5 + num12, num8 + num11 + num13);
					}
				}
				else if (item.X == 1)
				{
					DrawSocketItem(graphics, item.Socketed[0], num4 + num12, num7 + num11 + num13);
					DrawSocketItem(graphics, item.Socketed[1], num4 + num12, num9 + num11 + num13);
				}
				else
				{
					DrawSocketItem(graphics, item.Socketed[0], num5 + num12, num7 + num11 + num13);
					DrawSocketItem(graphics, item.Socketed[1], num5 + num12, num9 + num11 + num13);
				}
				break;
			case 3:
				if (item.Y == 2)
				{
					DrawSocketItem(graphics, item.Socketed[0], num4 + num12, num7 + num13);
					DrawSocketItem(graphics, item.Socketed[1], num6 + num12, num7 + num13);
					DrawSocketItem(graphics, item.Socketed[2], num5 + num12, num8 + num13);
				}
				else if (item.Y == 3)
				{
					if (item.X == 1)
					{
						DrawSocketItem(graphics, item.Socketed[0], num4 + num12, num7 + num13);
						DrawSocketItem(graphics, item.Socketed[1], num4 + num12, num8 + num13);
						DrawSocketItem(graphics, item.Socketed[2], num4 + num12, num9 + num13);
					}
					else
					{
						DrawSocketItem(graphics, item.Socketed[0], num5 + num12, num7 + num13);
						DrawSocketItem(graphics, item.Socketed[1], num5 + num12, num8 + num13);
						DrawSocketItem(graphics, item.Socketed[2], num5 + num12, num9 + num13);
					}
				}
				else if (item.X == 1)
				{
					DrawSocketItem(graphics, item.Socketed[0], num4 + num12, num7 + num11 + num13);
					DrawSocketItem(graphics, item.Socketed[1], num4 + num12, num8 + num11 + num13);
					DrawSocketItem(graphics, item.Socketed[2], num4 + num12, num9 + num11 + num13);
				}
				else
				{
					DrawSocketItem(graphics, item.Socketed[0], num5 + num12, num7 + num11 + num13);
					DrawSocketItem(graphics, item.Socketed[1], num5 + num12, num8 + num11 + num13);
					DrawSocketItem(graphics, item.Socketed[2], num5 + num12, num9 + num11 + num13);
				}
				break;
			case 4:
				if (item.Y == 3)
				{
					DrawSocketItem(graphics, item.Socketed[0], num4 + num12, num7 + num11 + num13);
					DrawSocketItem(graphics, item.Socketed[1], num6 + num12, num7 + num11 + num13);
					DrawSocketItem(graphics, item.Socketed[2], num4 + num12, num8 + num11 + num13);
					DrawSocketItem(graphics, item.Socketed[3], num6 + num12, num8 + num11 + num13);
				}
				else if (item.Y == 2)
				{
					DrawSocketItem(graphics, item.Socketed[0], num4 + num12, num7 + num13);
					DrawSocketItem(graphics, item.Socketed[1], num6 + num12, num7 + num13);
					DrawSocketItem(graphics, item.Socketed[2], num4 + num12, num8 + num13);
					DrawSocketItem(graphics, item.Socketed[3], num6 + num12, num8 + num13);
				}
				else if (item.X == 1)
				{
					DrawSocketItem(graphics, item.Socketed[0], num4 + num12, num7 + num13);
					DrawSocketItem(graphics, item.Socketed[1], num4 + num12, num8 + num13);
					DrawSocketItem(graphics, item.Socketed[2], num4 + num12, num9 + num13);
					DrawSocketItem(graphics, item.Socketed[3], num4 + num12, num10 + num13);
				}
				else
				{
					DrawSocketItem(graphics, item.Socketed[0], num5 + num12, num7 + num13);
					DrawSocketItem(graphics, item.Socketed[1], num5 + num12, num8 + num13);
					DrawSocketItem(graphics, item.Socketed[2], num5 + num12, num9 + num13);
					DrawSocketItem(graphics, item.Socketed[3], num5 + num12, num10 + num13);
				}
				break;
			case 5:
				if (item.Y == 3)
				{
					DrawSocketItem(graphics, item.Socketed[0], num4 + num12, num7 + num13);
					DrawSocketItem(graphics, item.Socketed[1], num6 + num12, num7 + num13);
					DrawSocketItem(graphics, item.Socketed[2], num5 + num12, num8 + num13);
					DrawSocketItem(graphics, item.Socketed[3], num4 + num12, num9 + num13);
					DrawSocketItem(graphics, item.Socketed[4], num6 + num12, num9 + num13);
				}
				else
				{
					DrawSocketItem(graphics, item.Socketed[0], num4 + num12, num7 + num11 + num13);
					DrawSocketItem(graphics, item.Socketed[1], num6 + num12, num7 + num11 + num13);
					DrawSocketItem(graphics, item.Socketed[2], num5 + num12, num8 + num11 + num13);
					DrawSocketItem(graphics, item.Socketed[3], num4 + num12, num9 + num11 + num13);
					DrawSocketItem(graphics, item.Socketed[4], num6 + num12, num9 + num11 + num13);
				}
				break;
			case 6:
				if (item.Y == 3)
				{
					DrawSocketItem(graphics, item.Socketed[0], num4 + num12, num7 + num13);
					DrawSocketItem(graphics, item.Socketed[1], num6 + num12, num7 + num13);
					DrawSocketItem(graphics, item.Socketed[2], num4 + num12, num8 + num13);
					DrawSocketItem(graphics, item.Socketed[3], num6 + num12, num8 + num13);
					DrawSocketItem(graphics, item.Socketed[4], num4 + num12, num9 + num13);
					DrawSocketItem(graphics, item.Socketed[5], num6 + num12, num9 + num13);
				}
				else
				{
					DrawSocketItem(graphics, item.Socketed[0], num4 + num12, num7 + num11 + num13);
					DrawSocketItem(graphics, item.Socketed[1], num6 + num12, num7 + num11 + num13);
					DrawSocketItem(graphics, item.Socketed[2], num4 + num12, num8 + num11 + num13);
					DrawSocketItem(graphics, item.Socketed[3], num6 + num12, num8 + num11 + num13);
					DrawSocketItem(graphics, item.Socketed[4], num4 + num12, num9 + num11 + num13);
					DrawSocketItem(graphics, item.Socketed[5], num6 + num12, num9 + num11 + num13);
				}
				break;
			}
		}
		for (int j = 0; j < array3.Length; j++)
		{
			pointF.Y = (float)j * num3 + (float)item.Top - 1f + (float)(Settings.Default.System_Font ? 6 : 0);
			string[] array4 = array3[j].Split('ÿ');
			for (int k = 0; k < array4.Length; k++)
			{
				if (k == 0)
				{
					if (Settings.Default.System_Font)
					{
						pointF.X = (float)(bitmap.Width - MeasureString(array[j], SysFont).Width) / 2f;
					}
					else
					{
						pointF.X = (float)(bitmap.Width - F16.MeasureString(array[j]).Width) / 2f;
					}
				}
				else if (Settings.Default.System_Font)
				{
					pointF.X += MeasureString(array4[k - 1], SysFont).Width;
				}
				else
				{
					pointF.X += F16.MeasureString(array4[k - 1]).Width;
				}
				try
				{
					if (array4[k].StartsWith("c"))
					{
						num2 = ((array4[k].Substring(1, 1) == ":") ? 10 : ((array4[k].Substring(1, 1) == ";") ? 11 : ((!(array4[k].Substring(1, 1) == "<")) ? Convert.ToInt32(array4[k].Substring(1, 1)) : 12)));
						array4[k] = array4[k].Substring(2);
					}
					else if (array4[k].StartsWith("#"))
					{
						array4[k] = array4[k].Substring(7);
					}
				}
				finally
				{
					if (array4[k] != "")
					{
						if (Settings.Default.System_Font)
						{
							graphics.DrawString(array4[k], SysFont, new SolidBrush(TextColors[num2]), pointF.X, pointF.Y);
						}
						else
						{
							F16.DrawString(graphics, new Point((int)pointF.X, (int)pointF.Y), array4[k], num2);
						}
					}
				}
			}
		}
		if (Settings.Default.Item_Header && item.Header != null && item.Header != null)
		{
			graphics.DrawString(item.Header, new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point), new SolidBrush(Color.AliceBlue), 0f, 0f);
		}
		if (Save)
		{
			if (Settings.Default.Item_Header && item.Header != null && item.Header != null)
			{
				graphics.DrawString(item.Header, new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point), new SolidBrush(Color.AliceBlue), 0f, 0f);
			}
			int num14 = Directory.GetFiles(Application.StartupPath + "\\images\\", item.Name + "*").Length + 1;
			bitmap.Save(Application.StartupPath + "\\images\\" + item.Name + num14 + ".png", ImageFormat.Png);
		}
		graphics.Dispose();
		bitmap2.Dispose();
		image.Dispose();
		return bitmap;
	}

	public static void DrawSocketItem(Graphics gph, Item i, int a, int b)
	{
		DirectBitmap image = new D2Palette(ref i).GetImage(ref i);
		if (!i.Code.Equals("gemsocket"))
		{
			gph.DrawImage(image.Bitmap, new Point(a, b));
		}
		else
		{
			gph.DrawImage(image.Bitmap, new Point(a - 1, b + 1));
		}
		image.Dispose();
	}
}
