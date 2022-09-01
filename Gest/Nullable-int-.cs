using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using _003FA0x6873a620;

[StructLayout(LayoutKind.Sequential, Size = 16)]
[NativeCppClass]
internal struct Nullable_003Cint_003E
{
	private long _003Calignment_0020member_003E;

	[SpecialName]
	public unsafe static void _003CMarshalCopy_003E(Nullable_003Cint_003E* A_0, Nullable_003Cint_003E* A_1)
	{
		//IL_000a: Expected I4, but got I8
		//IL_0034: Incompatible stack types: I vs I8
		//IL_0036: Expected I, but got I8
		System.Runtime.CompilerServices.Unsafe.SkipInit(out __clr_placement_new_t _clr_placement_new_t);
		// IL initblk instruction
		System.Runtime.CompilerServices.Unsafe.InitBlockUnaligned(ref _clr_placement_new_t, 0, 1);
		__clr_placement_new_t* ptr = &_clr_placement_new_t;
		__clr_placement_new_t _clr_placement_new_t2 = *ptr;
		try
		{
			long num;
			if (A_0 != null)
			{
				*(long*)A_0 = (nint)System.Runtime.CompilerServices.Unsafe.AsPointer(ref _003CModule_003E._003F_003F_7_003F_0024Nullable_0040H_0040_00406B_0040);
				*(long*)((ulong)(nint)A_0 + 8uL) = 0L;
				_003CModule_003E.Nullable_003Cint_003E_002E_003D(A_0, A_1);
				num = (nint)A_0;
			}
			else
			{
				num = 0L;
			}
			Nullable_003Cint_003E* ptr2 = (Nullable_003Cint_003E*)num;
			return;
		}
		catch
		{
			//try-fault
			_003CModule_003E.delete(A_0, A_0, *ptr);
			throw;
		}
	}

	[SpecialName]
	public unsafe static void _003CMarshalDestroy_003E(Nullable_003Cint_003E* A_0)
	{
		//IL_0013: Expected I, but got I8
		*(long*)A_0 = (nint)System.Runtime.CompilerServices.Unsafe.AsPointer(ref _003CModule_003E._003F_003F_7_003F_0024Nullable_0040H_0040_00406B_0040);
		_003CModule_003E.delete((void*)(*(ulong*)((ulong)(nint)A_0 + 8uL)), 4uL);
		*(long*)((ulong)(nint)A_0 + 8uL) = 0L;
	}
}
