using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[Guid("9229AB6D-933A-457A-A9D4-3830352711E8")]
[TypeLibType(TypeLibTypeFlags.FDual | TypeLibTypeFlags.FNonExtensible | TypeLibTypeFlags.FDispatchable)]
public interface ISalesDocVersion
{
	[DispId(1610743808)]
	int Version
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743808)]
		get;
	}

	[DispId(1610743809)]
	string VersionName
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743809)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
	}

	[ComAliasName("Interop.PrefSales.SalesDocType")]
	[DispId(1610743810)]
	SalesDocType Type
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743810)]
		[return: ComAliasName("Interop.PrefSales.SalesDocType")]
		get;
	}

	[DispId(1610743811)]
	[ComAliasName("Interop.PrefSales.SubOrderType")]
	SubOrderType SubType
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743811)]
		[return: ComAliasName("Interop.PrefSales.SubOrderType")]
		get;
	}

	[DispId(1610743812)]
	bool IsActive
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743812)]
		get;
	}

	[DispId(1610743813)]
	bool IsAccepted
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743813)]
		get;
	}

	[DispId(1610743814)]
	int SubOrderNumber
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743814)]
		get;
	}

	[DispId(1610743815)]
	bool IsPublic
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743815)]
		get;
	}

	[DispId(1610743816)]
	DateTime ReadyForFactoryDate
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743816)]
		get;
	}

	[DispId(1610743817)]
	bool IsOwned
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743817)]
		get;
	}
}
