using System;

namespace PInvoke;

[Flags]
public enum ProcessCreationFlags : uint
{
	BreakawayFromJob = 0x1000000u,
	DefaultErrorMode = 0x4000000u,
	NewConsole = 0x10u,
	NewProcessGroup = 0x200u,
	NoWindow = 0x8000000u,
	ProtectedProcess = 0x40000u,
	PreserveCodeAuthzLevel = 0x2000000u,
	SeparateWowVdm = 0x800u,
	SharedWowVdm = 0x1000u,
	Suspended = 4u,
	UnicodeEnvironment = 0x400u,
	DebugOnlyThisProcess = 2u,
	DebugProcess = 1u,
	DetachedProcess = 8u,
	ExtendedStartupInfo = 0x80000u,
	InheritParentAffinity = 0x10000u
}
