using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[Guid("F0F8E620-F911-416E-AC1F-A14426471598")]
[TypeLibType(TypeLibTypeFlags.FDual | TypeLibTypeFlags.FNonExtensible | TypeLibTypeFlags.FDispatchable)]
public interface ISalesDocSubTotal
{
	[DispId(1610743808)]
	string RowId
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743808)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
	}

	[DispId(1610743809)]
	int Position
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743809)]
		get;
	}

	[DispId(1610743810)]
	[ComAliasName("Interop.PrefSales.SalesDocSubTotalKind")]
	SalesDocSubTotalKind Kind
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743810)]
		[return: ComAliasName("Interop.PrefSales.SalesDocSubTotalKind")]
		get;
	}

	[DispId(1610743811)]
	bool IsPercentage
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743811)]
		get;
	}

	[DispId(1610743812)]
	bool IsSubTotal
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743812)]
		get;
	}

	[DispId(1610743813)]
	string DescriptionId
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743813)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743813)]
		[param: In]
		[param: MarshalAs(UnmanagedType.BStr)]
		set;
	}

	[DispId(1610743815)]
	bool AllowChangeDescription
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743815)]
		get;
	}

	[DispId(1610743816)]
	double Amount
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743816)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743816)]
		[param: In]
		set;
	}

	[DispId(1610743818)]
	double Percentage
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743818)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743818)]
		[param: In]
		set;
	}

	[DispId(1610743820)]
	double Minimum
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743820)]
		get;
	}

	[DispId(1610743821)]
	double Maximum
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743821)]
		get;
	}
}
