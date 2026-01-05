using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace PInvoke;

public static class NTDll
{
	private struct NtCreateThreadExBuffer
	{
		public int Size;

		public ulong Unknown1;

		public ulong Unknown2;

		public IntPtr Unknown3;

		public ulong Unknown4;

		public ulong Unknown5;

		public ulong Unknown6;

		public IntPtr Unknown7;

		public ulong Unknown8;
	}

	public static IntPtr CreateRemoteThread(IntPtr address, IntPtr param, IntPtr handle)
	{
		NtCreateThreadExBuffer outlpvBytesBuffer = default(NtCreateThreadExBuffer);
		outlpvBytesBuffer.Size = Marshal.SizeOf((object)outlpvBytesBuffer);
		outlpvBytesBuffer.Unknown1 = 65539uL;
		outlpvBytesBuffer.Unknown2 = 8uL;
		outlpvBytesBuffer.Unknown3 = Marshal.AllocHGlobal(4);
		outlpvBytesBuffer.Unknown4 = 0uL;
		outlpvBytesBuffer.Unknown5 = 65540uL;
		outlpvBytesBuffer.Unknown6 = 4uL;
		outlpvBytesBuffer.Unknown7 = Marshal.AllocHGlobal(4);
		outlpvBytesBuffer.Unknown8 = 0uL;
		IntPtr outhThread = IntPtr.Zero;
		NtCreateThreadEx(out outhThread, 2097151, IntPtr.Zero, handle, address, param, inCreateSuspended: false, 0uL, 0uL, 0uL, out outlpvBytesBuffer);
		if (outhThread == IntPtr.Zero)
		{
			throw new Win32Exception(Marshal.GetLastWin32Error());
		}
		return outhThread;
	}

	[DllImport("ntdll.dll")]
	private static extern int NtQueryInformationProcess(IntPtr hProcess, int processInformationClass, ref PROCESS_BASIC_INFORMATION processBasicInformation, uint processInformationLength, out uint returnLength);

	public static bool ProcessIsChildOf(Process parent, Process child)
	{
		PROCESS_BASIC_INFORMATION processBasicInformation = default(PROCESS_BASIC_INFORMATION);
		try
		{
			NtQueryInformationProcess(child.Handle, 0, ref processBasicInformation, (uint)Marshal.SizeOf((object)processBasicInformation), out var _);
			if (processBasicInformation.InheritedFromUniqueProcessId == parent.Id)
			{
				return true;
			}
		}
		catch
		{
			return false;
		}
		return false;
	}

	[DllImport("ntdll.dll", ExactSpelling = true, SetLastError = true)]
	private static extern IntPtr NtCreateThreadEx(out IntPtr outhThread, int inlpvDesiredAccess, IntPtr lpObjectAttributes, IntPtr inhProcessHandle, IntPtr lpStartAddress, IntPtr lpParameter, bool inCreateSuspended, ulong inStackZeroBits, ulong inSizeOfStackCommit, ulong inSizeOfStackReserve, [MarshalAs(UnmanagedType.Struct)] out NtCreateThreadExBuffer outlpvBytesBuffer);
}
