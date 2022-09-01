using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential, Size = 64)]
[NativeCppClass]
internal struct EHExceptionRecord
{
	[StructLayout(LayoutKind.Sequential, Size = 32)]
	[CLSCompliant(false)]
	[NativeCppClass]
	public struct EHParameters
	{
		private long _003Calignment_0020member_003E;
	}

	private long _003Calignment_0020member_003E;
}
