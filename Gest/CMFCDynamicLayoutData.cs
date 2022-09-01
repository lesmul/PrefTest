using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential, Size = 56)]
[NativeCppClass]
internal struct CMFCDynamicLayoutData
{
	[StructLayout(LayoutKind.Sequential, Size = 16)]
	[CLSCompliant(false)]
	[NativeCppClass]
	public struct Item
	{
		private int _003Calignment_0020member_003E;
	}

	private long _003Calignment_0020member_003E;
}
