using System;

namespace PInvoke;

[Flags]
public enum AllocationType
{
	WriteMatchFlagReset = 1,
	Commit = 0x1000,
	Reserve = 0x2000,
	CommitOrReserve = 0x3000,
	Decommit = 0x4000,
	Release = 0x8000,
	Free = 0x10000,
	Public = 0x20000,
	Mapped = 0x40000,
	Reset = 0x80000,
	TopDown = 0x100000,
	WriteWatch = 0x200000,
	Physical = 0x400000,
	SecImage = 0x1000000,
	Image = 0x1000000
}
