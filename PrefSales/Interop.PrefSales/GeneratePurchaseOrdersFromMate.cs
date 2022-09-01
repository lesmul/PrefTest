using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[Guid("F2E826B8-94AE-4BFB-B9DC-B91834365C41")]
[CoClass(typeof(GeneratePurchaseOrdersFromMaterialNeedsClass))]
public interface GeneratePurchaseOrdersFromMaterialNeeds : IGeneratePurchaseOrdersFromMaterialNeeds, _IGeneratePurchaseOrdersFromMaterialNeedsEvents_Event
{
}
[ComImport]
[ComSourceInterfaces("Interop.PrefSales._IGeneratePurchaseOrdersFromMaterialNeedsEvents")]
[Guid("248F8202-CD0D-4A46-8267-0E6130CE7841")]
[ClassInterface(ClassInterfaceType.None)]
[TypeLibType(TypeLibTypeFlags.FCanCreate)]
public class GeneratePurchaseOrdersFromMaterialNeedsClass : IGeneratePurchaseOrdersFromMaterialNeeds, GeneratePurchaseOrdersFromMaterialNeeds, _IGeneratePurchaseOrdersFromMaterialNeedsEvents_Event
{
	[DispId(1610743808)]
	public virtual extern string ConnectionString
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
	public virtual extern string Currency
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
	public virtual extern int UserCode
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
	public virtual extern DateTime ShopEntryDate
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
	public virtual extern int DeltaShopEntryDate
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
	public virtual extern int FirstPurchaseOrderNumber
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
	public virtual extern int LastPurchaseOrderNumber
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743820)]
		get;
	}

	[DispId(1610743821)]
	public virtual extern int PurchaseOrderNumeration
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
	public virtual extern bool ConfirmOrder
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
	public virtual extern bool UpdatePurchasePrices
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
	public virtual extern int UnitsMode
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
	public virtual extern bool KeepMountedWithUnmounted
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
	public extern GeneratePurchaseOrdersFromMaterialNeedsClass();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743831)]
	public virtual extern bool GenerateFromSalesDocument([In] int Number, [In] int Version, [In][MarshalAs(UnmanagedType.IUnknown)] object Connection);

	bool IGeneratePurchaseOrdersFromMaterialNeeds.GenerateFromSalesDocument([In] int Number, [In] int Version, [In][MarshalAs(UnmanagedType.IUnknown)] object Connection)
	{
		//ILSpy generated this explicit interface implementation from .override directive in GenerateFromSalesDocument
		return this.GenerateFromSalesDocument(Number, Version, Connection);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743832)]
	public virtual extern bool GenerateFromProductionLot([In] int ProductionLot, [In][MarshalAs(UnmanagedType.IUnknown)] object Connection);

	bool IGeneratePurchaseOrdersFromMaterialNeeds.GenerateFromProductionLot([In] int ProductionLot, [In][MarshalAs(UnmanagedType.IUnknown)] object Connection)
	{
		//ILSpy generated this explicit interface implementation from .override directive in GenerateFromProductionLot
		return this.GenerateFromProductionLot(ProductionLot, Connection);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743833)]
	public virtual extern bool GenerateFromReproductionNeeds([In] int ReproductionNeedsCode, [In][MarshalAs(UnmanagedType.IUnknown)] object Connection);

	bool IGeneratePurchaseOrdersFromMaterialNeeds.GenerateFromReproductionNeeds([In] int ReproductionNeedsCode, [In][MarshalAs(UnmanagedType.IUnknown)] object Connection)
	{
		//ILSpy generated this explicit interface implementation from .override directive in GenerateFromReproductionNeeds
		return this.GenerateFromReproductionNeeds(ReproductionNeedsCode, Connection);
	}
}
