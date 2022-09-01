using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[TypeLibType(TypeLibTypeFlags.FCanCreate)]
[Guid("B447DBF6-FAB8-4320-9F28-BE04FE530935")]
[ClassInterface(ClassInterfaceType.None)]
public class SalesDocSubTotalClass : ISalesDocSubTotal, SalesDocSubTotal
{
	[DispId(1610743808)]
	public virtual extern string RowId
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743808)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
	}

	[DispId(1610743809)]
	public virtual extern int Position
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743809)]
		get;
	}

	[DispId(1610743810)]
	[ComAliasName("Interop.PrefSales.SalesDocSubTotalKind")]
	public virtual extern SalesDocSubTotalKind Kind
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743810)]
		[return: ComAliasName("Interop.PrefSales.SalesDocSubTotalKind")]
		get;
	}

	[DispId(1610743811)]
	public virtual extern bool IsPercentage
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743811)]
		get;
	}

	[DispId(1610743812)]
	public virtual extern bool IsSubTotal
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743812)]
		get;
	}

	[DispId(1610743813)]
	public virtual extern string DescriptionId
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
	public virtual extern bool AllowChangeDescription
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743815)]
		get;
	}

	[DispId(1610743816)]
	public virtual extern double Amount
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
	public virtual extern double Percentage
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
	public virtual extern double Minimum
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743820)]
		get;
	}

	[DispId(1610743821)]
	public virtual extern double Maximum
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743821)]
		get;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern SalesDocSubTotalClass();
}
