using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using D2Bot.Properties;

namespace D2Bot;

public class D2Font
{
	public D2Char[] bmp;

	private static bool pLoaded = false;

	private static Color[] palette = new Color[256];

	private static byte[] d2pal = Resources.pal;

	private static byte[] cmap_grey_brown = Resources.Pal1;

	private static byte[] colormap = new byte[256];

	private static int[] kerning = new int[512]
	{
		10, 12, 10, 12, 10, 12, 10, 12, 10, 12,
		10, 12, 10, 12, 10, 12, 10, 12, 10, 12,
		10, 12, 10, 12, 10, 12, 10, 12, 10, 12,
		10, 12, 10, 12, 10, 12, 10, 12, 10, 12,
		10, 12, 10, 12, 10, 12, 10, 12, 10, 12,
		10, 12, 10, 12, 10, 12, 10, 12, 10, 12,
		10, 12, 10, 12, 10, 8, 10, 8, 10, 7,
		10, 8, 10, 8, 10, 13, 10, 12, 10, 4,
		10, 5, 10, 5, 10, 6, 10, 8, 10, 5,
		10, 5, 10, 5, 10, 9, 10, 12, 10, 5,
		10, 9, 10, 8, 10, 9, 10, 9, 10, 8,
		10, 8, 10, 7, 10, 8, 10, 5, 10, 5,
		10, 6, 10, 7, 10, 6, 10, 8, 10, 11,
		10, 12, 10, 7, 10, 9, 10, 10, 10, 8,
		10, 8, 10, 10, 10, 9, 10, 5, 10, 5,
		10, 9, 10, 8, 10, 12, 10, 10, 10, 11,
		10, 9, 10, 12, 10, 10, 10, 7, 10, 11,
		10, 12, 10, 13, 10, 16, 10, 12, 10, 12,
		10, 10, 10, 5, 10, 9, 10, 5, 10, 5,
		10, 9, 10, 5, 10, 10, 10, 7, 10, 8,
		10, 8, 10, 7, 10, 7, 10, 9, 10, 7,
		10, 4, 10, 4, 10, 8, 10, 7, 10, 10,
		10, 9, 10, 10, 10, 7, 10, 10, 10, 9,
		10, 7, 10, 9, 10, 10, 10, 10, 10, 13,
		10, 10, 10, 10, 10, 7, 10, 6, 10, 3,
		10, 6, 10, 6, 10, 12, 10, 12, 10, 12,
		10, 12, 10, 12, 10, 12, 10, 12, 10, 12,
		10, 12, 10, 12, 10, 12, 10, 12, 10, 12,
		10, 12, 10, 12, 10, 12, 10, 12, 10, 12,
		10, 5, 10, 6, 10, 12, 10, 12, 10, 12,
		10, 12, 10, 12, 10, 12, 10, 12, 10, 12,
		10, 12, 10, 12, 10, 12, 10, 12, 10, 12,
		10, 8, 10, 8, 10, 7, 10, 8, 10, 7,
		10, 12, 10, 3, 10, 6, 10, 6, 10, 11,
		10, 9, 10, 7, 10, 10, 10, 4, 10, 11,
		10, 9, 10, 7, 10, 9, 10, 7, 10, 7,
		10, 5, 10, 13, 10, 9, 10, 7, 10, 7,
		10, 3, 10, 8, 10, 8, 10, 11, 10, 13,
		10, 12, 10, 8, 10, 12, 10, 12, 10, 12,
		10, 12, 10, 12, 10, 12, 10, 11, 10, 10,
		10, 8, 10, 7, 10, 8, 10, 8, 10, 5,
		10, 5, 10, 5, 10, 7, 10, 11, 10, 11,
		10, 11, 10, 11, 10, 11, 10, 12, 10, 11,
		10, 10, 10, 11, 10, 13, 10, 13, 10, 13,
		10, 12, 10, 12, 10, 8, 10, 9, 10, 11,
		10, 10, 10, 10, 10, 10, 10, 10, 10, 10,
		10, 10, 10, 8, 10, 7, 10, 6, 10, 7,
		10, 7, 10, 4, 10, 5, 10, 4, 10, 5,
		10, 8, 10, 9, 10, 10, 10, 9, 10, 9,
		10, 10, 10, 10, 10, 8, 10, 10, 10, 10,
		10, 10, 10, 10, 10, 10, 10, 10, 10, 7,
		10, 10
	};

	public D2Font()
	{
		SetBitmaps(Resources.font16);
	}

	public void SetBitmaps(byte[] ptr)
	{
		DC6_Header_S Header = default(DC6_Header_S);
		DC6_FRAME_HEADER_S[] fHeaders = new DC6_FRAME_HEADER_S[256];
		bmp = new D2Char[256];
		int[] dc6_frame_ptr = new int[256];
		LoadHeader(ptr, ref Header, ref fHeaders, ref dc6_frame_ptr);
		for (int i = 0; i < 256; i++)
		{
			bmp[i] = new D2Char
			{
				Width = fHeaders[i].width,
				Height = fHeaders[i].height
			};
			bmp[i].Data = new int[bmp[i].Width, bmp[i].Height];
			LoadBitmap(ptr, ref bmp[i], dc6_frame_ptr[i] + Marshal.SizeOf(typeof(DC6_FRAME_HEADER_S)), fHeaders[i].length);
		}
	}

	public void LoadBitmap(byte[] file, ref D2Char bitmap, int start, int size)
	{
		int num = 0;
		int num2 = bitmap.Height - 1;
		for (int i = 0; i < size; i++)
		{
			int num3 = file[start++];
			if (num3 == 128)
			{
				num = 0;
				num2--;
				continue;
			}
			if ((num3 & 0x80) == 128)
			{
				num += num3 & 0x7F;
				continue;
			}
			for (int j = 0; j < num3; j++)
			{
				int num4 = file[start++];
				i++;
				bitmap.Data[num, num2] = num4;
				num++;
			}
		}
	}

	private void LoadHeader(byte[] dc6_file, ref DC6_Header_S Header, ref DC6_FRAME_HEADER_S[] fHeaders, ref int[] dc6_frame_ptr)
	{
		int num = Marshal.SizeOf(typeof(DC6_Header_S));
		IntPtr intPtr = Marshal.AllocHGlobal(num);
		try
		{
			Marshal.Copy(dc6_file, 0, intPtr, num);
			Header = (DC6_Header_S)Marshal.PtrToStructure(intPtr, typeof(DC6_Header_S));
		}
		finally
		{
			Marshal.FreeHGlobal(intPtr);
		}
		num = Marshal.SizeOf(typeof(int));
		for (int i = 0; i < 256; i++)
		{
			intPtr = Marshal.AllocHGlobal(num);
			try
			{
				Marshal.Copy(dc6_file, Marshal.SizeOf(typeof(DC6_Header_S)) + 4 * i, intPtr, num);
				dc6_frame_ptr[i] = (int)Marshal.PtrToStructure(intPtr, typeof(int));
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
		}
		num = Marshal.SizeOf(typeof(DC6_FRAME_HEADER_S));
		for (int j = 0; j < 256; j++)
		{
			intPtr = Marshal.AllocHGlobal(num);
			try
			{
				Marshal.Copy(dc6_file, dc6_frame_ptr[j], intPtr, num);
				fHeaders[j] = (DC6_FRAME_HEADER_S)Marshal.PtrToStructure(intPtr, typeof(DC6_FRAME_HEADER_S));
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
		}
	}

	private void LoadPalette(byte[] d2pal)
	{
		if (!pLoaded)
		{
			Parallel.For(0, 256, delegate(int i)
			{
				Color color = Color.FromArgb(255, d2pal[i * 3 + 2], d2pal[i * 3 + 1], d2pal[i * 3]);
				palette[i] = color;
			});
			pLoaded = true;
		}
	}

	private DirectBitmap PaletteShift(D2Char c, int shift_color)
	{
		LoadPalette(Resources.pal);
		DirectBitmap directBitmap = new DirectBitmap(c.Width, c.Height);
		for (int i = 0; i < c.Height; i++)
		{
			for (int j = 0; j < c.Width; j++)
			{
				int num = c.Data[j, i];
				if (num != 0)
				{
					if (shift_color == 0)
					{
						directBitmap.SetPixel(j, i, Color.FromArgb(255, palette[num]));
					}
					else
					{
						directBitmap.SetPixel(j, i, Color.FromArgb(255, palette[cmap_grey_brown[shift_color * 256 + num + 439847]]));
					}
				}
			}
		}
		return directBitmap;
	}

	public Size MeasureString(string input)
	{
		int num = 0;
		int num2 = 0;
		foreach (char c in input)
		{
			if (c < 'Ā')
			{
				num += kerning[c * 2 + 1];
				num2 = ((bmp[(uint)c].Height > num2) ? bmp[(uint)c].Height : num2);
			}
		}
		return new Size(num, num2);
	}

	public void DrawString(Graphics g, Point p, string input, int color)
	{
		foreach (char c in input)
		{
			if (c < 'Ā')
			{
				DirectBitmap directBitmap = PaletteShift(bmp[(uint)c], color);
				g.DrawImage(directBitmap.Bitmap, p);
				directBitmap.Dispose();
				p.X += kerning[c * 2 + 1];
			}
		}
	}
}
