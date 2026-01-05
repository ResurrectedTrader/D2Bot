using System;

namespace PInvoke;

[Flags]
public enum LoadLibraryFlags : uint
{
	LoadAsDataFile = 2u,
	DontResolveReferences = 1u,
	LoadWithAlteredSeachPath = 8u,
	IgnoreCodeAuthzLevel = 0x10u,
	LoadAsExclusiveDataFile = 0x40u
}
