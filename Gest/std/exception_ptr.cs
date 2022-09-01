using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using _003FA0xe7afedcc;

namespace std;

[StructLayout(LayoutKind.Sequential, Size = 16)]
[NativeCppClass]
internal struct exception_ptr
{
	private long _003Calignment_0020member_003E;

	[SpecialName]
	public unsafe static void _003CMarshalCopy_003E(exception_ptr* A_0, exception_ptr* A_1)
	{
		//IL_000a: Expected I4, but got I8
		//IL_001d: Expected I, but got I8
		System.Runtime.CompilerServices.Unsafe.SkipInit(out __clr_placement_new_t _clr_placement_new_t);
		// IL initblk instruction
		System.Runtime.CompilerServices.Unsafe.InitBlockUnaligned(ref _clr_placement_new_t, 0, 1);
		if (A_0 != null)
		{
			_003CModule_003E.__ExceptionPtrCopy(A_0, A_1);
			exception_ptr** ptr = (exception_ptr**)A_0;
		}
		else
		{
			exception_ptr** ptr = null;
		}
	}

	[SpecialName]
	public unsafe static void _003CMarshalDestroy_003E(exception_ptr* A_0)
	{
		_003CModule_003E.__ExceptionPtrDestroy(A_0);
	}
}
