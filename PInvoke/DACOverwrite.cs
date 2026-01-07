using System;
using System.Runtime.InteropServices;
using D2Bot;

namespace PInvoke;

public static class DACOverwrite
{
    [DllImport("advapi32.dll", SetLastError = true)]
    static extern uint GetSecurityInfo(
        IntPtr handle,
        SE_OBJECT_TYPE ObjectType,
        SECURITY_INFORMATION SecurityInfo,
        out IntPtr ppsidOwner,
        out IntPtr ppsidGroup,
        out IntPtr ppDacl,
        out IntPtr ppSacl,
        out IntPtr ppSecurityDescriptor);

    [DllImport("advapi32.dll", SetLastError = true)]
    static extern uint SetSecurityInfo(
        IntPtr handle,
        SE_OBJECT_TYPE ObjectType,
        SECURITY_INFORMATION SecurityInfo,
        IntPtr psidOwner,
        IntPtr psidGroup,
        IntPtr pDacl,
        IntPtr pSacl);

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern IntPtr OpenProcess(
        uint processAccess,
        bool bInheritHandle,
        int processId);

    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool CloseHandle(IntPtr hObject);

    [DllImport("kernel32.dll")]
    static extern IntPtr GetCurrentProcess();

    [DllImport("kernel32.dll")]
    static extern IntPtr LocalFree(IntPtr hMem);

    const uint WRITE_DAC = 0x00040000;

    [Flags]
    public enum SECURITY_INFORMATION : uint
    {
        OWNER_SECURITY_INFORMATION = 0x00000001,
        GROUP_SECURITY_INFORMATION = 0x00000002,
        DACL_SECURITY_INFORMATION = 0x00000004,
        SACL_SECURITY_INFORMATION = 0x00000008,
        LABEL_SECURITY_INFORMATION = 0x00000010,
        UNPROTECTED_DACL_SECURITY_INFORMATION = 0x20000000,
        PROTECTED_DACL_SECURITY_INFORMATION = 0x80000000,
        UNPROTECTED_SACL_SECURITY_INFORMATION = 0x10000000,
        PROTECTED_SACL_SECURITY_INFORMATION = 0x40000000
    }

    public enum SE_OBJECT_TYPE
    {
        SE_UNKNOWN_OBJECT_TYPE = 0,
        SE_FILE_OBJECT,
        SE_SERVICE,
        SE_PRINTER,
        SE_REGISTRY_KEY,
        SE_LMSHARE,
        SE_KERNEL_OBJECT,
        SE_WINDOW_OBJECT,
        SE_DS_OBJECT,
        SE_DS_OBJECT_ALL,
        SE_PROVIDER_DEFINED_OBJECT,
        SE_WMIGUID_OBJECT,
        SE_REGISTRY_WOW64_32KEY
    }

    public static bool OverwriteDac(D2Profile profile)
    {
        if (profile.D2Process == null)
            return false;
        IntPtr dacl;
        IntPtr secdesc;

        // Get the DACL of the current process
        if (GetSecurityInfo(GetCurrentProcess(),
                            SE_OBJECT_TYPE.SE_KERNEL_OBJECT,
                            SECURITY_INFORMATION.DACL_SECURITY_INFORMATION,
                            out IntPtr _,
                            out IntPtr _,
                            out dacl,
                            out IntPtr _,
                            out secdesc) != 0)
        {
            Program.GM.ConsolePrint(new PrintMessage("Failed to get process security info"), profile);
            return false;
        }

        // Open the target process with WRITE_DAC access
        IntPtr process = OpenProcess(WRITE_DAC, false, profile.D2Process.Id);
        if (process == IntPtr.Zero)
        {
            LocalFree(secdesc);
            Program.GM.ConsolePrint(new PrintMessage("Failed to open with WRITE_DAC"), profile);
            return false;
        }

        // Set the DACL of the target process
        if (SetSecurityInfo(process,
                            SE_OBJECT_TYPE.SE_KERNEL_OBJECT,
                            SECURITY_INFORMATION.DACL_SECURITY_INFORMATION |
                            SECURITY_INFORMATION.UNPROTECTED_DACL_SECURITY_INFORMATION,
                            IntPtr.Zero,
                            IntPtr.Zero,
                            dacl,
                            IntPtr.Zero) != 0)
        {
            LocalFree(secdesc);
            Program.GM.ConsolePrint(new PrintMessage("Failed to set DAC"), profile);
            CloseHandle(process);
            return false;
        }

        // Clean up
        CloseHandle(process);
        LocalFree(secdesc);
        return true;
    }
}
