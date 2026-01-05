namespace PInvoke;

public struct PROCESS_BASIC_INFORMATION
{
	public int ExitStatus;

	public int PebBaseAddress;

	public int AffinityMask;

	public int BasePriority;

	public uint UniqueProcessId;

	public uint InheritedFromUniqueProcessId;
}
