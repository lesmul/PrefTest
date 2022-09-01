using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[TypeLibType(TypeLibTypeFlags.FDual | TypeLibTypeFlags.FNonExtensible | TypeLibTypeFlags.FDispatchable)]
[Guid("F2E826B8-94AE-4BFB-B9DC-B91834365C41")]
public interface IGeneratePurchaseOrdersFromMaterialNeeds
{
	[DispId(1610743808)]
	string ConnectionString
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743808)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743808)]
		[param: In]
		[param: MarshalAs(UnmanagedType.BStr)]
		set;
	}

	[DispId(1610743810)]
	string Currency
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743810)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743810)]
		[param: In]
		[param: MarshalAs(UnmanagedType.BStr)]
		set;
	}

	[DispId(1610743812)]
	int UserCode
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743812)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743812)]
		[param: In]
		set;
	}

	[DispId(1610743814)]
	DateTime ShopEntryDate
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743814)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743814)]
		[param: In]
		set;
	}

	[DispId(1610743816)]
	int DeltaShopEntryDate
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
	int FirstPurchaseOrderNumber
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
	int LastPurchaseOrderNumber
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743820)]
		get;
	}

	[DispId(1610743821)]
	int PurchaseOrderNumeration
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743821)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743821)]
		[param: In]
		set;
	}

	[DispId(1610743823)]
	bool ConfirmOrder
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743823)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743823)]
		[param: In]
		set;
	}

	[DispId(1610743825)]
	bool UpdatePurchasePrices
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743825)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743825)]
		[param: In]
		set;
	}

	[DispId(1610743827)]
	int UnitsMode
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743827)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743827)]
		[param: In]
		set;
	}

	[DispId(1610743829)]
	bool KeepMountedWithUnmounted
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743829)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743829)]
		[param: In]
		set;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743831)]
	bool GenerateFromSalesDocument([In] int Number, [In] int Version, [In][MarshalAs(UnmanagedType.IUnknown)] object Connection);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743832)]
	bool GenerateFromProductionLot([In] int ProductionLot, [In][MarshalAs(UnmanagedType.IUnknown)] object Connection);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743833)]
	bool GenerateFromReproductionNeeds([In] int ReproductionNeedsCode, [In][MarshalAs(UnmanagedType.IUnknown)] object Connection);
}
