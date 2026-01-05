namespace PInvoke;

public enum ThreadAccessFlags : uint
{
	Terminate = 1u,
	SuspendResume = 2u,
	GetContext = 8u,
	SetContext = 0x10u,
	SetInformation = 0x20u,
	QueryInformation = 0x40u,
	SetThreadToken = 0x80u,
	Impersonate = 0x100u,
	DirectImpersonate = 0x200u,
	SetLimitedInformation = 0x400u,
	QueryLimitedInformation = 0x800u
}
