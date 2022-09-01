using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ATL;

[StructLayout(LayoutKind.Sequential, Size = 16)]
[NativeCppClass]
internal struct COleDateTime
{
	[NativeCppClass]
	[CLSCompliant(false)]
	public enum DateTimeStatus
	{

	}

	private long _003Calignment_0020member_003E;
}
