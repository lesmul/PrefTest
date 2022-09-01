using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[Guid("2FF1D918-9698-48DD-AC20-11BA6055137B")]
[ClassInterface(ClassInterfaceType.None)]
[TypeLibType(TypeLibTypeFlags.FCanCreate)]
public class SalesDocPriceGroupClass : ISalesDocPriceGroup, SalesDocPriceGroup
{
	[DispId(1)]
	public virtual extern int PriceGroupId
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1)]
		get;
	}

	[DispId(2)]
	public virtual extern double OriginalMaterialCost
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(2)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(2)]
		[param: In]
		set;
	}

	[DispId(3)]
	public virtual extern double OriginalLabourCost
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(3)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(3)]
		[param: In]
		set;
	}

	[DispId(4)]
	public virtual extern double OriginalMaterialPrice
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(4)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(4)]
		[param: In]
		set;
	}

	[DispId(5)]
	public virtual extern double OriginalLabourPrice
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(5)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(5)]
		[param: In]
		set;
	}

	[DispId(6)]
	public virtual extern double ModifiedMaterialCost
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(6)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(6)]
		[param: In]
		set;
	}

	[DispId(7)]
	public virtual extern double ModifiedLabourCost
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(7)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(7)]
		[param: In]
		set;
	}

	[DispId(8)]
	public virtual extern double ModifiedMaterialPrice
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(8)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(8)]
		[param: In]
		set;
	}

	[DispId(9)]
	public virtual extern double ModifiedLabourPrice
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(9)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(9)]
		[param: In]
		set;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern SalesDocPriceGroupClass();
}
