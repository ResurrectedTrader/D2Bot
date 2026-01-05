using System;

namespace D2Bot;

public static class SWP
{
	private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);

	private static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);

	private static readonly IntPtr HWND_TOP = new IntPtr(0);

	private static readonly IntPtr HWND_BOTTOM = new IntPtr(1);

	public static readonly int NOSIZE = 1;

	public static readonly int NOMOVE = 2;

	public static readonly int NOZORDER = 4;

	public static readonly int NOREDRAW = 8;

	public static readonly int NOACTIVATE = 16;

	public static readonly int DRAWFRAME = 32;

	public static readonly int FRAMECHANGED = 32;

	public static readonly int SHOWWINDOW = 64;

	public static readonly int HIDEWINDOW = 128;

	public static readonly int NOCOPYBITS = 256;

	public static readonly int NOOWNERZORDER = 512;

	public static readonly int NOREPOSITION = 512;

	public static readonly int NOSENDCHANGING = 1024;

	public static readonly int DEFERERASE = 8192;

	public static readonly int ASYNCWINDOWPOS = 16384;
}
