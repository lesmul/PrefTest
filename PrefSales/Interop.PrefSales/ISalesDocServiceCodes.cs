using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[Guid("70CF40DD-D2B9-4B47-9697-5480C2917209")]
[TypeLibType(TypeLibTypeFlags.FDual | TypeLibTypeFlags.FNonExtensible | TypeLibTypeFlags.FDispatchable)]
public interface ISalesDocServiceCodes : IEnumerable
{
	[DispId(1610743808)]
	int Count
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743808)]
		get;
	}

	[DispId(0)]
	SalesDocServiceCode this[[In][MarshalAs(UnmanagedType.Struct)] object Index]
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
	[return: MarshalAs(UnmanagedType.Interface)]
	SalesDocServiceCode Add([In] int ServiceCode);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743812)]
	[return: MarshalAs(UnmanagedType.Interface)]
	SalesDocServiceCode Insert([In] int ServiceCode, [In] int Position);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743813)]
	void Remove([In][MarshalAs(UnmanagedType.Struct)] object Index);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743814)]
	void Reset();
}
