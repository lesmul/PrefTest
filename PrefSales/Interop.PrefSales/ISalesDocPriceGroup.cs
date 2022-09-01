using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[Guid("370C7894-74C0-4D49-BF90-D0E146719167")]
[TypeLibType(TypeLibTypeFlags.FDual | TypeLibTypeFlags.FNonExtensible | TypeLibTypeFlags.FDispatchable)]
public interface ISalesDocPriceGroup
{
	[DispId(1)]
	int PriceGroupId
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1)]
		get;
	}

	[DispId(2)]
	double OriginalMaterialCost
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
	double OriginalLabourCost
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
	double OriginalMaterialPrice
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
	double OriginalLabourPrice
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
	double ModifiedMaterialCost
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
	double ModifiedLabourCost
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
	double ModifiedMaterialPrice
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
	double ModifiedLabourPrice
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(9)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(9)]
		[param: In]
		set;
	}
}
