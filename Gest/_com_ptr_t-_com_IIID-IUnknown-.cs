using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using _003FA0x6873a620;

[StructLayout(LayoutKind.Sequential, Size = 8)]
[NativeCppClass]
internal struct _com_ptr_t_003C_com_IIID_003CIUnknown_002C_0026_GUID_00000000_0000_0000_c000_000000000046_003E_0020_003E
{
	private long _003Calignment_0020member_003E;

	[SpecialName]
	public unsafe static void _003CMarshalCopy_003E(_com_ptr_t_003C_com_IIID_003CIUnknown_002C_0026_GUID_00000000_0000_0000_c000_000000000046_003E_0020_003E* A_0, _com_ptr_t_003C_com_IIID_003CIUnknown_002C_0026_GUID_00000000_0000_0000_c000_000000000046_003E_0020_003E* A_1)
	{
		//IL_000a: Expected I4, but got I8
		//IL_001e: Expected I, but got I8
		System.Runtime.CompilerServices.Unsafe.SkipInit(out __clr_placement_new_t _clr_placement_new_t);
		// IL initblk instruction
		System.Runtime.CompilerServices.Unsafe.InitBlockUnaligned(ref _clr_placement_new_t, 0, 1);
		if (A_0 != null)
		{
			*(long*)A_0 = *(long*)A_1;
			*(long*)A_1 = 0L;
			_com_ptr_t_003C_com_IIID_003CIUnknown_002C_0026_GUID_00000000_0000_0000_c000_000000000046_003E_0020_003E** ptr = (_com_ptr_t_003C_com_IIID_003CIUnknown_002C_0026_GUID_00000000_0000_0000_c000_000000000046_003E_0020_003E**)A_0;
		}
		else
		{
			_com_ptr_t_003C_com_IIID_003CIUnknown_002C_0026_GUID_00000000_0000_0000_c000_000000000046_003E_0020_003E** ptr = null;
		}
	}

	[SpecialName]
	public unsafe static void _003CMarshalDestroy_003E(_com_ptr_t_003C_com_IIID_003CIUnknown_002C_0026_GUID_00000000_0000_0000_c000_000000000046_003E_0020_003E* A_0)
	{
		//IL_0013: Expected I, but got I8
		//IL_0013: Expected I, but got I8
		if (*(long*)A_0 != 0L)
		{
			long num = *(long*)A_0;
			((delegate* unmanaged[Cdecl, Cdecl]<IntPtr, uint>)(*(ulong*)(*(long*)(*(ulong*)A_0) + 16)))((nint)num);
		}
	}
}
