using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ATL;

[StructLayout(LayoutKind.Sequential, Size = 56)]
[NativeCppClass]
internal struct CComTypeInfoHolder
{
	[StructLayout(LayoutKind.Sequential, Size = 16)]
	[CLSCompliant(false)]
	[NativeCppClass]
	public struct stringdispid
	{
		private long _003Calignment_0020member_003E;
	}

	private long _003Calignment_0020member_003E;
}
