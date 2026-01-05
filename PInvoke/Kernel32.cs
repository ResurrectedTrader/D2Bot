using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace PInvoke;

public static class Kernel32
{
	private struct ProcessInformation
	{
		public IntPtr Process;

		public IntPtr Thread;

		public uint ProcessId;

		public uint ThreadId;
	}

	private struct StartupInfo
	{
		public uint CB;

		public string Reserved;

		public string Desktop;

		public string Title;

		public uint X;

		public uint Y;

		public uint XSize;

		public uint YSize;

		public uint XCountChars;

		public uint YCountChars;

		public uint FillAttribute;

		public uint Flags;

		public short ShowWindow;

		public short Reserved2;

		public IntPtr lpReserved2;

		public IntPtr StdInput;

		public IntPtr StdOutput;

		public IntPtr StdError;
	}

	[Flags]
	public enum ErrorModes : uint
	{
		SYSTEM_DEFAULT = 0u,
		SEM_FAILCRITICALERRORS = 1u,
		SEM_NOALIGNMENTFAULTEXCEPT = 4u,
		SEM_NOGPFAULTERRORBOX = 2u,
		SEM_NOOPENFILEERRORBOX = 0x8000u
	}

	[DllImport("kernel32.dll")]
	public static extern ErrorModes SetErrorMode(ErrorModes uMode);

	[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
	private static extern bool CreateProcess(string Application, string CommandLine, IntPtr ProcessAttributes, IntPtr ThreadAttributes, bool InheritHandles, uint CreationFlags, IntPtr Environment, string CurrentDirectory, ref StartupInfo StartupInfo, out ProcessInformation ProcessInformation);

	[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
	private static extern uint SuspendThread(IntPtr hThread);

	[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
	private static extern uint ResumeThread(IntPtr hThread);

	[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool VirtualProtectEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, PageAccessProtectionFlags flags, out PageAccessProtectionFlags oldFlags);

	[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
	private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr baseAddress, [Out] byte[] buffer, uint size, out uint numBytesRead);

	[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
	private static extern IntPtr OpenProcess(ProcessAccessFlags dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, uint dwProcessId);

	[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
	private static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

	[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
	private static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, AllocationType flAllocationType, PageAccessProtectionFlags flProtect);

	[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress, int dwSize, AllocationType dwFreeType);

	[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool CloseHandle(IntPtr hObject);

	[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
	private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out int lpNumberOfBytesWritten);

	[DllImport("kernel32.dll", SetLastError = true)]
	private static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

	[DllImport("kernel32.dll", SetLastError = true)]
	private static extern IntPtr GetModuleHandle(string lpModuleName);

	[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
	private static extern uint WaitForSingleObject(IntPtr hHandle, uint dwMilliseconds);

	[DllImport("kernel32.dll", SetLastError = true)]
	private static extern IntPtr LoadLibraryEx(string lpFileName, IntPtr hFile, LoadLibraryFlags dwFlags);

	[DllImport("kernel32.dll", SetLastError = true)]
	private static extern IntPtr LoadLibrary(string library);

	[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool FreeLibrary(IntPtr hHandle);

	[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
	private static extern IntPtr OpenThread(ThreadAccessFlags flags, bool bInheritHandle, uint threadId);

	[DllImport("user32.dll", SetLastError = true)]
	public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

	public static bool LoadRemoteLibrary(Process p, object module)
	{
		IntPtr intPtr = WriteObject(p, module);
		bool result = CallRemoteFunction(p, "kernel32.dll", "LoadLibraryW", intPtr);
		FreeObject(p, intPtr);
		return result;
	}

	public static bool UnloadRemoteLibrary(Process p, string module)
	{
		IntPtr intPtr = FindModuleHandle(p, module);
		IntPtr intPtr2 = WriteObject(p, intPtr);
		bool result = CallRemoteFunction(p, "kernel32.dll", "FreeLibrary", intPtr2);
		FreeObject(p, intPtr2);
		return result;
	}

	public static bool CallRemoteFunction(Process p, string module, string function, IntPtr param)
	{
		return CallRemoteFunction(p.Id, module, function, param);
	}

	public static bool CallRemoteFunction(int pid, string module, string function, IntPtr param)
	{
		IntPtr intPtr = LoadLibraryEx(module, LoadLibraryFlags.LoadAsDataFile);
		IntPtr procAddress = GetProcAddress(intPtr, function);
		if (intPtr == IntPtr.Zero || procAddress == IntPtr.Zero)
		{
			return false;
		}
		IntPtr intPtr2 = CreateRemoteThread(pid, procAddress, param, CreateThreadFlags.RunImmediately);
		if (intPtr2 != IntPtr.Zero)
		{
			WaitForSingleObject(intPtr2, uint.MaxValue);
		}
		return intPtr2 != IntPtr.Zero;
	}

	public static bool ReadProcessMemory(Process p, IntPtr address, ref byte[] buffer)
	{
		IntPtr processHandle = GetProcessHandle(p, ProcessAccessFlags.VMRead);
		if (!VirtualProtect(new IntPtr(p.Id), address, (uint)buffer.Length, PageAccessProtectionFlags.ExecuteReadWrite, out var oldFlags))
		{
			throw new Win32Exception(Marshal.GetLastWin32Error());
		}
		if (!ReadProcessMemory(processHandle, address, buffer, (uint)buffer.Length, out var numBytesRead))
		{
			throw new Win32Exception(Marshal.GetLastWin32Error());
		}
		if (!VirtualProtect(new IntPtr(p.Id), address, (uint)buffer.Length, oldFlags, out var _))
		{
			throw new Win32Exception(Marshal.GetLastWin32Error());
		}
		CloseProcessHandle(processHandle);
		return numBytesRead == buffer.Length;
	}

	public static IntPtr WriteObject(Process p, object data)
	{
		byte[] rawBytes = GetRawBytes(data);
		IntPtr intPtr = VirtualAlloc(p, IntPtr.Zero, (uint)rawBytes.Length, AllocationType.CommitOrReserve, PageAccessProtectionFlags.ReadWrite);
		WriteProcessMemory(p, intPtr, rawBytes);
		return intPtr;
	}

	public static void FreeObject(Process p, IntPtr address)
	{
		VirtualFree(p, address);
	}

	public static IntPtr FindModuleHandle(Process p, string module)
	{
		foreach (ProcessModule module2 in p.Modules)
		{
			if (Path.GetFileName(module2.FileName).ToLowerInvariant() == Path.GetFileName(module).ToLowerInvariant())
			{
				return module2.BaseAddress;
			}
		}
		return IntPtr.Zero;
	}

	public static IntPtr FindOffset(string moduleName, string function)
	{
		IntPtr intPtr = LoadLibraryEx(moduleName, LoadLibraryFlags.LoadAsDataFile);
		if (intPtr != IntPtr.Zero)
		{
			IntPtr procAddress = GetProcAddress(intPtr, function);
			FreeLibrary(intPtr);
			return (IntPtr)(procAddress.ToInt32() - intPtr.ToInt32());
		}
		return IntPtr.Zero;
	}

	[DebuggerHidden]
	public static bool VirtualProtect(IntPtr pid, IntPtr address, uint size, PageAccessProtectionFlags flags, out PageAccessProtectionFlags oldFlags)
	{
		IntPtr processHandle = GetProcessHandle(pid, ProcessAccessFlags.VMOperation);
		bool result = VirtualProtectEx(processHandle, address, size, flags, out oldFlags);
		CloseHandle(processHandle);
		return result;
	}

	[DebuggerHidden]
	public static IntPtr GetProcessHandle(IntPtr pid, ProcessAccessFlags flags)
	{
		IntPtr intPtr = OpenProcess(flags, bInheritHandle: false, (uint)(int)pid);
		if (intPtr == IntPtr.Zero)
		{
			throw new Win32Exception(Marshal.GetLastWin32Error());
		}
		return intPtr;
	}

	[DebuggerHidden]
	public static IntPtr LoadLibraryEx(string module, LoadLibraryFlags flags)
	{
		return LoadLibraryEx(module, IntPtr.Zero, flags);
	}

	[DebuggerHidden]
	public static IntPtr GetProcessHandle(Process p, ProcessAccessFlags flags)
	{
		IntPtr intPtr = OpenProcess(flags, bInheritHandle: false, (uint)p.Id);
		if (intPtr == IntPtr.Zero)
		{
			throw new Win32Exception(Marshal.GetLastWin32Error());
		}
		return intPtr;
	}

	[DebuggerHidden]
	public static void CloseProcessHandle(IntPtr handle)
	{
		if (!CloseHandle(handle))
		{
			throw new Win32Exception(Marshal.GetLastWin32Error());
		}
	}

	[DebuggerHidden]
	public static IntPtr VirtualAlloc(Process p, IntPtr address, uint size, AllocationType type, PageAccessProtectionFlags flags)
	{
		IntPtr processHandle = GetProcessHandle(p, ProcessAccessFlags.VMOperation);
		IntPtr intPtr = VirtualAllocEx(processHandle, address, size, type, flags);
		if (intPtr == IntPtr.Zero)
		{
			throw new Win32Exception(Marshal.GetLastWin32Error());
		}
		CloseProcessHandle(processHandle);
		return intPtr;
	}

	[DebuggerHidden]
	public static void VirtualFree(Process p, IntPtr address)
	{
		IntPtr processHandle = GetProcessHandle(p, ProcessAccessFlags.VMOperation);
		bool num = VirtualFreeEx(processHandle, address, 0, AllocationType.Release);
		CloseProcessHandle(processHandle);
		if (!num)
		{
			throw new Win32Exception(Marshal.GetLastWin32Error());
		}
	}

	[DebuggerHidden]
	public static int WriteProcessMemory(Process p, IntPtr address, byte[] buffer)
	{
		IntPtr processHandle = GetProcessHandle(p, ProcessAccessFlags.VMOperation | ProcessAccessFlags.VMWrite);
		if (!WriteProcessMemory(processHandle, address, buffer, (uint)buffer.Length, out var lpNumberOfBytesWritten))
		{
			throw new Win32Exception(Marshal.GetLastWin32Error());
		}
		CloseProcessHandle(processHandle);
		return lpNumberOfBytesWritten;
	}

	[DebuggerHidden]
	public static IntPtr CreateRemoteThread(Process p, IntPtr address, IntPtr param, CreateThreadFlags flags)
	{
		return CreateRemoteThread(p.Id, address, param, flags);
	}

	[DebuggerHidden]
	public static IntPtr CreateRemoteThread(int pid, IntPtr address, IntPtr param, CreateThreadFlags flags)
	{
		IntPtr processHandle = GetProcessHandle(new IntPtr(pid), ProcessAccessFlags.CreateThread | ProcessAccessFlags.VMOperation | ProcessAccessFlags.VMRead | ProcessAccessFlags.VMWrite | ProcessAccessFlags.QueryInformation);
		IntPtr intPtr = CreateRemoteThread(processHandle, IntPtr.Zero, 0u, address, param, (uint)flags, IntPtr.Zero);
		if (intPtr == IntPtr.Zero)
		{
			throw new Win32Exception(Marshal.GetLastWin32Error());
		}
		CloseProcessHandle(processHandle);
		return intPtr;
	}

	[DebuggerHidden]
	public static void SuspendThread(int tid)
	{
		IntPtr intPtr = OpenThread(ThreadAccessFlags.SuspendResume, bInheritHandle: false, (uint)tid);
		SuspendThread(intPtr);
		CloseHandle(intPtr);
	}

	[DebuggerHidden]
	public static void ResumeThread(int tid)
	{
		IntPtr intPtr = OpenThread(ThreadAccessFlags.SuspendResume, bInheritHandle: false, (uint)tid);
		ResumeThread(intPtr);
		CloseHandle(intPtr);
	}

	[DebuggerHidden]
	public static byte[] GetRawBytes(object anything)
	{
		if (anything.GetType().Equals(typeof(string)))
		{
			return Encoding.Unicode.GetBytes((string)anything);
		}
		int num = Marshal.SizeOf(anything);
		IntPtr intPtr = Marshal.AllocHGlobal(num);
		Marshal.StructureToPtr(anything, intPtr, fDeleteOld: false);
		byte[] array = new byte[num];
		Marshal.Copy(intPtr, array, 0, num);
		Marshal.FreeHGlobal(intPtr);
		return array;
	}

	public static uint StartProcess(string directory, string application, ProcessCreationFlags flags, params string[] parameters)
	{
		StartupInfo StartupInfo = default(StartupInfo);
		ProcessInformation ProcessInformation = default(ProcessInformation);
		if (CreateProcess(application, string.Concat(application, string.Concat(parameters)), IntPtr.Zero, IntPtr.Zero, InheritHandles: false, (uint)flags, IntPtr.Zero, directory, ref StartupInfo, out ProcessInformation))
		{
			return ProcessInformation.ProcessId;
		}
		return uint.MaxValue;
	}

	public static Process StartSuspended(Process process, ProcessStartInfo startInfo)
	{
		return Process.GetProcessById((int)StartProcess(startInfo.WorkingDirectory, startInfo.FileName, ProcessCreationFlags.Suspended, startInfo.Arguments));
	}

	public static void Suspend(Process process)
	{
		foreach (ProcessThread thread in process.Threads)
		{
			SuspendThread(new IntPtr(thread.Id));
		}
	}

	public static void Resume(Process process)
	{
		foreach (ProcessThread thread in process.Threads)
		{
			ResumeThread(thread.Id);
		}
	}

	public static void Suspend(ProcessThread thread)
	{
		SuspendThread(new IntPtr(thread.Id));
	}

	public static void Resume(ProcessThread thread)
	{
		ResumeThread(new IntPtr(thread.Id));
	}
}
