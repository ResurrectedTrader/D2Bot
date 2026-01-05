using System;
using System.Runtime.InteropServices;
using System.Text;

namespace D2Bot;

public class MessageHelper
{
	public enum SendMessageTimeoutFlags : uint
	{
		SMTO_NORMAL = 0u,
		SMTO_BLOCK = 1u,
		SMTO_ABORTIFHUNG = 2u,
		SMTO_NOTIMEOUTIFNOTHUNG = 8u
	}

	public struct COPYDATASTRUCT
	{
		public IntPtr dwData;

		public int cbData;

		public IntPtr lpData;
	}

	public const int SW_HIDE = 0;

	public const int SW_SHOW = 1;

	public const int SW_MINIMIZE = 6;

	public const int WM_USER = 1024;

	public const int WM_COPYDATA = 74;

	[DllImport("User32.dll")]
	private static extern int RegisterWindowMessage(string lpString);

	[DllImport("user32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool ShowScrollBar(IntPtr hWnd, int wBar, [MarshalAs(UnmanagedType.Bool)] bool bShow);

	[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
	public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

	[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
	internal static extern void MoveWindow(IntPtr hwnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

	[DllImport("user32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, int uFlags);

	[DllImport("user32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool IsIconic(IntPtr hWnd);

	[DllImport("User32.dll")]
	public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

	[DllImport("user32.dll")]
	public static extern int ShowWindow(int hwnd, int command);

	[DllImport("User32.dll")]
	public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, ref COPYDATASTRUCT lParam);

	[DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool DestroyWindow(IntPtr hwnd);

	[DllImport("User32.dll")]
	public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

	[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
	public static extern IntPtr SendMessageTimeout(IntPtr windowHandle, uint Msg, IntPtr wParam, ref COPYDATASTRUCT lParam, SendMessageTimeoutFlags flags, uint timeout, out IntPtr result);

	[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
	public static extern IntPtr SendMessageTimeout(IntPtr windowHandle, uint Msg, IntPtr wParam, IntPtr lParam, SendMessageTimeoutFlags flags, uint timeout, out IntPtr result);

	[DllImport("User32.dll")]
	public static extern bool SetForegroundWindow(IntPtr hWnd);

	public bool bringAppToFront(IntPtr hWnd)
	{
		return SetForegroundWindow(hWnd);
	}

	public static int SendStringMessageToHandle(IntPtr hWnd, int wParam, string msg)
	{
		int result = 0;
		if (hWnd != IntPtr.Zero)
		{
			byte[] bytes = Encoding.Default.GetBytes(msg);
			int num = bytes.Length;
			COPYDATASTRUCT lParam = default(COPYDATASTRUCT);
			lParam.dwData = (IntPtr)100;
			lParam.lpData = Marshal.AllocHGlobal(bytes.Length);
			Marshal.Copy(bytes, 0, lParam.lpData, bytes.Length);
			lParam.cbData = num + 1;
			result = SendMessage(hWnd, 74, wParam, ref lParam);
		}
		return result;
	}

	public int SendMessageToHandle(IntPtr hWnd, int Msg, int wParam, int lParam)
	{
		int result = 0;
		if (hWnd != IntPtr.Zero)
		{
			result = SendMessage(hWnd, Msg, wParam, lParam);
		}
		return result;
	}

	public IntPtr FindWindowByName(string className, string windowName)
	{
		return FindWindow(className, windowName);
	}
}
