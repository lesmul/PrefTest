using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential, Size = 384)]
[NativeCppClass]
internal struct CSplitterWnd
{
	[NativeCppClass]
	internal enum ESplitType
	{

	}

	[StructLayout(LayoutKind.Sequential, Size = 12)]
	[NativeCppClass]
	[CLSCompliant(false)]
	public struct CRowColInfo
	{
		private int _003Calignment_0020member_003E;
	}

	private long _003Calignment_0020member_003E;
}
