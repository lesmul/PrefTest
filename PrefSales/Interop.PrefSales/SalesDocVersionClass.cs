using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[TypeLibType(TypeLibTypeFlags.FCanCreate)]
[Guid("08D14343-EEB6-4E44-A1F1-068F916BE7AB")]
[ClassInterface(ClassInterfaceType.None)]
public class SalesDocVersionClass : ISalesDocVersion, SalesDocVersion
{
	[DispId(1610743808)]
	public virtual extern int Version
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743808)]
		get;
	}

	[DispId(1610743809)]
	public virtual extern string VersionName
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743809)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
	}

	[ComAliasName("Interop.PrefSales.SalesDocType")]
	[DispId(1610743810)]
	public virtual extern SalesDocType Type
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743810)]
		[return: ComAliasName("Interop.PrefSales.SalesDocType")]
		get;
	}

	[DispId(1610743811)]
	[ComAliasName("Interop.PrefSales.SubOrderType")]
	public virtual extern SubOrderType SubType
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743811)]
		[return: ComAliasName("Interop.PrefSales.SubOrderType")]
		get;
	}

	[DispId(1610743812)]
	public virtual extern bool IsActive
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743812)]
		get;
	}

	[DispId(1610743813)]
	public virtual extern bool IsAccepted
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743813)]
		get;
	}

	[DispId(1610743814)]
	public virtual extern int SubOrderNumber
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743814)]
		get;
	}

	[DispId(1610743815)]
	public virtual extern bool IsPublic
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743815)]
		get;
	}

	[DispId(1610743816)]
	public virtual extern DateTime ReadyForFactoryDate
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743816)]
		get;
	}

	[DispId(1610743817)]
	public virtual extern bool IsOwned
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743817)]
		get;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern SalesDocVersionClass();
}
