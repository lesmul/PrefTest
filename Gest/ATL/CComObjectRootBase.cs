using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ATL;

[StructLayout(LayoutKind.Sequential, Size = 8)]
[NativeCppClass]
internal struct CComObjectRootBase
{
	[StructLayout(LayoutKind.Explicit, Size = 8)]
	[NativeCppClass]
	internal struct _0024UnnamedClass_00240x6873a620_0024753_0024
	{
		[FieldOffset(0)]
		private long _003Calignment_0020member_003E;
	}

	private long _003Calignment_0020member_003E;
}
