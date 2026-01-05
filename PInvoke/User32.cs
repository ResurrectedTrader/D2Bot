using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace PInvoke;

public static class User32
{
	[DllImport("user32.dll", CharSet = CharSet.Auto)]
	private static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

	[DllImport("user32.dll")]
	public static extern IntPtr SetFocus(IntPtr hWnd);

	public static string GetClassNameFromProcess(Process p)
	{
		StringBuilder stringBuilder = new StringBuilder(100);
		GetClassName(p.MainWindowHandle, stringBuilder, stringBuilder.Capacity);
		return stringBuilder.ToString();
	}
}
