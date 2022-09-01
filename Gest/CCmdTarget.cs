using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential, Size = 64)]
[NativeCppClass]
internal static struct CCmdTarget
{
	[StructLayout(LayoutKind.Sequential, Size = 8)]
	[NativeCppClass]
	internal struct XDispatch
	{
		private long _003Calignment_0020member_003E;
	}

	[StructLayout(LayoutKind.Sequential, Size = 8)]
	[NativeCppClass]
	internal struct XConnPtContainer
	{
		private long _003Calignment_0020member_003E;
	}

	private long _003Calignment_0020member_003E;
}
