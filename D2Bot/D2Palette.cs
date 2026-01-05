using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using D2Bot.Properties;

namespace D2Bot;

public class D2Palette
{
	private int dc6_frame_ptr;

	private DC6_Header_S dc6_header;

	private DC6_FRAME_HEADER_S dc6_frame_header;

	private Color bgnd;

	private Color[] palette_shift = new Color[256];

	private byte[] colormap = new byte[256];

	private byte[,] dc6_indexed;

	private byte[] dc6_file;

	private static ConcurrentDictionary<string, byte[]> dc6_cache = new ConcurrentDictionary<string, byte[]>();

	private static Color[] palette = new Color[256];

	private static byte[] d2pal = Resources.pal;

	private static byte[] cmap_grey_brown = Resources.invgreybrown;

	private DirectBitmap current { get; set; }

	private int shift_color { get; set; }

	public D2Palette(ref Item i)
	{
		if (dc6_cache.ContainsKey(i.Code))
		{
			dc6_file = dc6_cache[i.Code];
		}
		else
		{
			MemoryStream memoryStream = new MemoryStream();
			try
			{
				Assembly.GetExecutingAssembly().GetManifestResourceStream("D2Bot.Resources." + i.Code + ".dc6").CopyTo(memoryStream);
			}
			catch
			{
				Assembly.GetExecutingAssembly().GetManifestResourceStream("D2Bot.Resources.box.dc6").CopyTo(memoryStream);
			}
			dc6_file = memoryStream.ToArray();
			dc6_cache.TryAdd(i.Code, dc6_file);
			memoryStream.Close();
		}
		shift_color = i.Color;
	}

	public DirectBitmap GetImage(ref Item i, Color? bgnd = null)
	{
		this.bgnd = bgnd ?? Color.Blue;
		return Transform(ref i);
	}

	private void LoadPalette()
	{
		Parallel.For(0, 256, delegate(int i)
		{
			Color color = Color.FromArgb(255, d2pal[i * 3 + 2], d2pal[i * 3 + 1], d2pal[i * 3]);
			palette[i] = color;
		});
		Parallel.For(0, 256, delegate(int i)
		{
			if (shift_color > -1)
			{
				colormap[i] = cmap_grey_brown[shift_color * 256 + i];
				palette_shift[i] = palette[colormap[i]];
			}
			else
			{
				palette_shift[i] = palette[i];
			}
		});
	}

	private void PaletteShift(ref Item item)
	{
		int alpha = 255;
		if (item.Description.Contains("Ethereal") || item.Description.Contains(":eth"))
		{
			alpha = 127;
		}
		if (item.Code.Equals("gemsocket"))
		{
			alpha = 100;
		}
		current = new DirectBitmap(dc6_frame_header.width, dc6_frame_header.height);
		if (bgnd == Color.Transparent)
		{
			for (int i = 0; i < dc6_frame_header.height; i++)
			{
				for (int j = 0; j < dc6_frame_header.width; j++)
				{
					int num = dc6_indexed[j, i];
					if (num != 0)
					{
						current.SetPixel(j, i, Color.FromArgb(alpha, palette_shift[num]));
					}
				}
			}
			return;
		}
		Color colour = ((!item.Description.Equals("soc")) ? Color.FromArgb(20, bgnd) : Color.FromArgb(0, bgnd));
		for (int k = 0; k < dc6_frame_header.height; k++)
		{
			for (int l = 0; l < dc6_frame_header.width; l++)
			{
				int num2 = dc6_indexed[l, k];
				if (num2 == 0)
				{
					current.SetPixel(l, k, colour);
				}
				else
				{
					current.SetPixel(l, k, Color.FromArgb(alpha, palette_shift[num2]));
				}
			}
		}
	}

	private void IndexDC6()
	{
		DC6_FRAME_HEADER_S dC6_FRAME_HEADER_S = dc6_frame_header;
		dc6_indexed = new byte[dC6_FRAME_HEADER_S.width, dC6_FRAME_HEADER_S.height];
		if (dC6_FRAME_HEADER_S.width <= 0 || dC6_FRAME_HEADER_S.height <= 0)
		{
			return;
		}
		dc6_indexed = new byte[dC6_FRAME_HEADER_S.width, dC6_FRAME_HEADER_S.height];
		long num = dc6_frame_ptr + 32;
		int num2 = 0;
		int num3 = dC6_FRAME_HEADER_S.height - 1;
		for (long num4 = 0L; num4 < dC6_FRAME_HEADER_S.length; num4++)
		{
			int num5 = dc6_file[num];
			num++;
			if (num5 == 128)
			{
				num2 = 0;
				num3--;
				continue;
			}
			if ((num5 & 0x80) == 128)
			{
				num2 += num5 & 0x7F;
				continue;
			}
			for (long num6 = 0L; num6 < num5; num6++)
			{
				byte b = dc6_file[num];
				num++;
				num4++;
				dc6_indexed[num2, num3] = b;
				num2++;
			}
		}
	}

	private void LoadHeader(ref Item i)
	{
		int num = Marshal.SizeOf(typeof(DC6_Header_S));
		IntPtr intPtr = Marshal.AllocHGlobal(num);
		try
		{
			Marshal.Copy(dc6_file, 0, intPtr, num);
			dc6_header = (DC6_Header_S)Marshal.PtrToStructure(intPtr, typeof(DC6_Header_S));
		}
		finally
		{
			Marshal.FreeHGlobal(intPtr);
		}
		long num2 = dc6_header.directions * dc6_header.frames_per_dir;
		num = Marshal.SizeOf((object)(4 * num2));
		intPtr = Marshal.AllocHGlobal(num);
		try
		{
			Marshal.Copy(dc6_file, Marshal.SizeOf(typeof(DC6_Header_S)), intPtr, num);
			dc6_frame_ptr = (int)Marshal.PtrToStructure(intPtr, typeof(int));
		}
		finally
		{
			Marshal.FreeHGlobal(intPtr);
		}
		num = Marshal.SizeOf(typeof(DC6_FRAME_HEADER_S));
		intPtr = Marshal.AllocHGlobal(num);
		try
		{
			Marshal.Copy(dc6_file, dc6_frame_ptr, intPtr, num);
			dc6_frame_header = (DC6_FRAME_HEADER_S)Marshal.PtrToStructure(intPtr, typeof(DC6_FRAME_HEADER_S));
		}
		finally
		{
			Marshal.FreeHGlobal(intPtr);
		}
		i.Width = dc6_frame_header.width;
		i.Height = dc6_frame_header.height;
		top(i);
		left(i);
	}

	private DirectBitmap Transform(ref Item i)
	{
		LoadHeader(ref i);
		IndexDC6();
		LoadPalette();
		PaletteShift(ref i);
		return current;
	}

	public static void top(Item cItem)
	{
		if (cItem.Height < 30)
		{
			cItem.Y = 1;
			cItem.Top = 32;
		}
		else if (cItem.Height < 65)
		{
			cItem.Y = 2;
			cItem.Top = 61;
		}
		else if (cItem.Height < 95)
		{
			cItem.Y = 3;
			cItem.Top = 90;
		}
		else
		{
			cItem.Y = 4;
			cItem.Top = 119;
		}
	}

	public static void left(Item cItem)
	{
		if (cItem.Width < 37)
		{
			cItem.X = 1;
			cItem.Left = 212;
		}
		else
		{
			cItem.X = 2;
			cItem.Left = 226;
		}
	}
}
