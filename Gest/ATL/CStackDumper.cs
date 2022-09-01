using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ATL;

[StructLayout(LayoutKind.Sequential, Size = 1)]
[NativeCppClass]
internal struct CStackDumper
{
	[StructLayout(LayoutKind.Sequential, Size = 1304)]
	[CLSCompliant(false)]
	[NativeCppClass]
	public struct _ATL_SYMBOL_INFO
	{
		private long _003Calignment_0020member_003E;
	}
}
