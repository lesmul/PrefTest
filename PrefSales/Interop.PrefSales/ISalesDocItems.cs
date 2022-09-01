using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[Guid("75536F24-8A8E-4717-8BD2-65B7A5B95355")]
[TypeLibType(TypeLibTypeFlags.FDual | TypeLibTypeFlags.FNonExtensible | TypeLibTypeFlags.FDispatchable)]
public interface ISalesDocItems : IEnumerable
{
	[DispId(1610743808)]
	int Count
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743808)]
		get;
	}

	[DispId(0)]
	SalesDocItem this[[In][MarshalAs(UnmanagedType.Struct)] object Index]
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(0)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	[DispId(8)]
	bool Changed
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(8)]
		get;
	}

	[DispId(9)]
	bool Blocked
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(9)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(9)]
		[param: In]
		set;
	}

	[DispId(10)]
	SalesDocItem ItemFromPosition
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(10)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(-4)]
	[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "System.Runtime.InteropServices.CustomMarshalers.EnumeratorToEnumVariantMarshaler, CustomMarshalers, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	new IEnumerator GetEnumerator();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1)]
	[return: MarshalAs(UnmanagedType.Interface)]
	SalesDocItem Add([In][MarshalAs(UnmanagedType.BStr)] string IdPos);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(2)]
	[return: MarshalAs(UnmanagedType.Interface)]
	SalesDocItem Insert([In][MarshalAs(UnmanagedType.BStr)] string IdPos, [In] int SortOrder);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(3)]
	[return: MarshalAs(UnmanagedType.Interface)]
	SalesDocItem AddFromXML([In][MarshalAs(UnmanagedType.BStr)] string IdPos, [In][MarshalAs(UnmanagedType.BStr)] string XMLItem);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(4)]
	[return: MarshalAs(UnmanagedType.Interface)]
	SalesDocItem InsertFromXML([In][MarshalAs(UnmanagedType.BStr)] string IdPos, [In] int SortOrder, [In][MarshalAs(UnmanagedType.BStr)] string XMLItem);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(5)]
	[return: MarshalAs(UnmanagedType.Interface)]
	SalesDocItem AddWebItem([In][MarshalAs(UnmanagedType.BStr)] string IdPos);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(6)]
	[return: MarshalAs(UnmanagedType.Interface)]
	SalesDocItem InsertWebItem([In][MarshalAs(UnmanagedType.BStr)] string IdPos, [In] int SortOrder);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(7)]
	bool Exists([In][MarshalAs(UnmanagedType.Struct)] object Index);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(11)]
	void Remove([In][MarshalAs(UnmanagedType.Struct)] object Index);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(12)]
	[return: MarshalAs(UnmanagedType.Interface)]
	SalesDocItem AddTypedItem([In][MarshalAs(UnmanagedType.BStr)] string IdPos, [In][ComAliasName("Interop.PrefSales.SalesDocItemType")] SalesDocItemType Type);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(13)]
	[return: MarshalAs(UnmanagedType.Interface)]
	SalesDocItem InsertTypedItem([In][MarshalAs(UnmanagedType.BStr)] string IdPos, [In] int SortOrder, [In][ComAliasName("Interop.PrefSales.SalesDocItemType")] SalesDocItemType Type);
}
