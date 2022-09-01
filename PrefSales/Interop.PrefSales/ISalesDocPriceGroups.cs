using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[TypeLibType(TypeLibTypeFlags.FDual | TypeLibTypeFlags.FNonExtensible | TypeLibTypeFlags.FDispatchable)]
[Guid("BE895971-3A2A-4652-9FDC-3AF67EDCBC90")]
public interface ISalesDocPriceGroups : IEnumerable
{
	[DispId(1610743808)]
	int Count
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743808)]
		get;
	}

	[DispId(0)]
	SalesDocPriceGroup this[[In] int n]
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(0)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(-4)]
	[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "System.Runtime.InteropServices.CustomMarshalers.EnumeratorToEnumVariantMarshaler, CustomMarshalers, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	new IEnumerator GetEnumerator();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743811)]
	bool Exists([In] int lPriceGroupId);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743812)]
	[return: MarshalAs(UnmanagedType.Interface)]
	SalesDocPriceGroup Add([In] int lPriceGroupId);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743813)]
	void Reset();
}
