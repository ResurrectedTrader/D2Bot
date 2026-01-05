using System;
using System.Diagnostics;
using PInvoke;

namespace D2Bot;

public class Patch
{
	public enum Dll
	{
		D2CLIENT,
		D2COMMON,
		D2GFX,
		D2LANG,
		D2WIN,
		D2NET,
		D2GAME,
		D2LAUNCH,
		FOG,
		BNCLIENT,
		STORM,
		D2CMP,
		D2MULTI,
		D2MCPCLIENT,
		GAME
	}

	private Dll DLL;

	private int Offset;

	private byte[] OldCode;

	private byte[] NewCode;

	private bool Injected;

	public Patch(Dll dll, int offset, byte[] bytes)
	{
		DLL = dll;
		Offset = offset;
		OldCode = new byte[bytes.Length];
		NewCode = bytes;
		Injected = false;
	}

	public bool Install(Process p)
	{
		if (IsInstalled())
		{
			return true;
		}
		IntPtr dllOffset = GetDllOffset(p, DLL, Offset);
		Kernel32.ReadProcessMemory(p, dllOffset, ref OldCode);
		Kernel32.WriteProcessMemory(p, dllOffset, NewCode);
		Injected = true;
		return true;
	}

	public bool Remove(Process p)
	{
		if (!IsInstalled())
		{
			return true;
		}
		IntPtr dllOffset = GetDllOffset(p, DLL, Offset);
		Kernel32.WriteProcessMemory(p, dllOffset, OldCode);
		Injected = false;
		return true;
	}

	public bool IsInstalled()
	{
		return Injected;
	}

	public static IntPtr GetDllOffset(Process p, Dll dll, int offset)
	{
		string[] array = new string[15]
		{
			"D2CLIENT.dll", "D2COMMON.dll", "D2GFX.dll", "D2LANG.dll", "D2WIN.dll", "D2NET.dll", "D2GAME.dll", "D2LAUNCH.dll", "FOG.dll", "BNCLIENT.dll",
			"STORM.dll", "D2CMP.dll", "D2MULTI.dll", "D2MCPCLIENT.dll", "D2CMP.dll"
		};
		if (dll == Dll.GAME)
		{
			return IntPtr.Add(p.MainModule.BaseAddress, offset);
		}
		IntPtr intPtr = Kernel32.FindModuleHandle(p, array[(int)dll]);
		if (intPtr == IntPtr.Zero)
		{
			if (!Kernel32.LoadRemoteLibrary(p, array[(int)dll]))
			{
				return IntPtr.Zero;
			}
			intPtr = Kernel32.FindModuleHandle(p, array[(int)dll]);
		}
		return IntPtr.Add(intPtr, offset);
	}
}
