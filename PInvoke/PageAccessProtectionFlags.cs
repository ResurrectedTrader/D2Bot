using System;

namespace PInvoke;

[Flags]
public enum PageAccessProtectionFlags
{
	NoAccess = 1,
	ReadOnly = 2,
	ReadWrite = 4,
	WriteCopy = 8,
	Execute = 0x10,
	ExecuteRead = 0x20,
	ExecuteReadWrite = 0x40,
	ExecuteWriteCopy = 0x80,
	Guard = 0x100,
	NoCache = 0x200,
	WriteCombine = 0x400
}
