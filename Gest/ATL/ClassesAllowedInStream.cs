using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ATL;

[StructLayout(LayoutKind.Explicit, Size = 8)]
[NativeCppClass]
internal struct ClassesAllowedInStream
{
	[FieldOffset(0)]
	private long _003Calignment_0020member_003E;
}
