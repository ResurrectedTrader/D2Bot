using System;

namespace D2Bot;

public class Worker
{
	public IntPtr handle { get; set; }

	public string msg { get; set; }

	public D2Profile d2p { get; set; }

	public Main.ProfileAction action { get; set; }

	public Worker(IntPtr wparam, string p, Main.ProfileAction a = Main.ProfileAction.None, D2Profile d2p = null)
	{
		handle = wparam;
		msg = p;
		action = a;
		this.d2p = d2p;
	}
}
