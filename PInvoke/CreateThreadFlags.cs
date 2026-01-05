using System;

namespace PInvoke;

[Flags]
public enum CreateThreadFlags
{
	RunImmediately = 0,
	CreateSuspended = 4,
	StackSizeParamIsAReservation = 0x10000,
	UseNtCreateThreadEx = 0x800000
}
