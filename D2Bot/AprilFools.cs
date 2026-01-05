using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace D2Bot;

internal class AprilFools
{
	private static PictureBox box;

	private static bool show = false;

	private static bool ad = false;

	private static Image ad1 = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("D2Bot.Resources.ad1.png"));

	private static Image ad2 = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("D2Bot.Resources.ad2.png"));

	public static bool isToday()
	{
		if (DateTime.Now.ToString("MMdd").Equals("0401"))
		{
			return true;
		}
		return false;
	}

	public static void Show(PictureBox pb)
	{
		box = pb;
		ShowAd();
		ShowMiner();
	}

	private static void ShowAd()
	{
		if (show)
		{
			if (ad)
			{
				box.Image = ad1;
				ad = false;
			}
			else
			{
				box.Image = ad2;
				ad = true;
			}
			box.Show();
		}
		else
		{
			box.Hide();
		}
	}

	private static void ShowMiner()
	{
		Random random = new Random();
		double num = random.NextDouble() * 0.19 + 0.0099;
		double num2 = random.NextDouble() * 0.029899999999999996 + 0.01;
		Program.GM.SetD2BotTitle("            CPU Mining: " + num.ToString("0.0000") + " kH/s :: Rate: " + num2.ToString("0.0000") + " / Day");
	}
}
