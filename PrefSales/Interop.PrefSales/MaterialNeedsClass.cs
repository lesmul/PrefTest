using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[TypeLibType(TypeLibTypeFlags.FCanCreate)]
[ComSourceInterfaces("Interop.PrefSales._IMaterialNeedsEvents")]
[Guid("B4301A42-39CE-42F9-9E84-40AAFBB1F851")]
[ClassInterface(ClassInterfaceType.None)]
public class MaterialNeedsClass : IMaterialNeeds, MaterialNeeds, _IMaterialNeedsEvents_Event
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
	public virtual extern object PrefUserLink
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743810)]
		[return: MarshalAs(UnmanagedType.IUnknown)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743810)]
		[param: In]
		[param: MarshalAs(UnmanagedType.IUnknown)]
		set;
	}

	[DispId(1610743812)]
	public virtual extern string Description
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743812)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743812)]
		[param: In]
		[param: MarshalAs(UnmanagedType.BStr)]
		set;
	}

	[DispId(1610743814)]
	public virtual extern bool ReserveMaterial
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
	public virtual extern bool OptimizeRods
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
	public virtual extern int OptimizationLevel
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
	public virtual extern bool UseRemnants
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743820)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743820)]
		[param: In]
		set;
	}

	[DispId(1610743822)]
	public virtual extern bool DontForceMaxRodsAllowed
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743822)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743822)]
		[param: In]
		set;
	}

	[DispId(1610743824)]
	public virtual extern int UseWastageAllowance
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743824)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743824)]
		[param: In]
		set;
	}

	[DispId(1610743826)]
	public virtual extern int UseFullRods
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743826)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743826)]
		[param: In]
		set;
	}

	[DispId(1610743828)]
	public virtual extern int ProductionPlantCode
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743828)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743828)]
		[param: In]
		set;
	}

	[DispId(1610743830)]
	public virtual extern bool DeleteOtherDocumentsNeeds
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743830)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743830)]
		[param: In]
		set;
	}

	[DispId(1610743832)]
	public virtual extern int Number
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743832)]
		get;
	}

	[DispId(1610743833)]
	public virtual extern int Version
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743833)]
		get;
	}

	[DispId(1610743834)]
	public virtual extern int ProductionLot
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743834)]
		get;
	}

	[DispId(1610743835)]
	public virtual extern int MNSet
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743835)]
		get;
	}

	[DispId(1610743836)]
	public virtual extern string SessionId
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743836)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
	}

	public virtual extern event _IMaterialNeedsEvents_BeforeCalculateMaterialNeedsEventHandler BeforeCalculateMaterialNeeds;

	public virtual extern event _IMaterialNeedsEvents_AfterCalculateMaterialNeedsEventHandler AfterCalculateMaterialNeeds;

	public virtual extern event _IMaterialNeedsEvents_BeforeRemoveMaterialNeedsEventHandler BeforeRemoveMaterialNeeds;

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern MaterialNeedsClass();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743837)]
	public virtual extern bool CalculateSalesDocument([In] int Number, [In] int Version, [In][MarshalAs(UnmanagedType.IUnknown)] object Connection);

	bool IMaterialNeeds.CalculateSalesDocument([In] int Number, [In] int Version, [In][MarshalAs(UnmanagedType.IUnknown)] object Connection)
	{
		//ILSpy generated this explicit interface implementation from .override directive in CalculateSalesDocument
		return this.CalculateSalesDocument(Number, Version, Connection);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743838)]
	public virtual extern bool CalculateProductionLot([In] int ProductionLot, [In][MarshalAs(UnmanagedType.IUnknown)] object Connection);

	bool IMaterialNeeds.CalculateProductionLot([In] int ProductionLot, [In][MarshalAs(UnmanagedType.IUnknown)] object Connection)
	{
		//ILSpy generated this explicit interface implementation from .override directive in CalculateProductionLot
		return this.CalculateProductionLot(ProductionLot, Connection);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743839)]
	public virtual extern bool CalculateReproductionNeeds([In][MarshalAs(UnmanagedType.BStr)] string ProductionLineId, out int ReproductionNeedsCode, [In][MarshalAs(UnmanagedType.IUnknown)] object Connection);

	bool IMaterialNeeds.CalculateReproductionNeeds([In][MarshalAs(UnmanagedType.BStr)] string ProductionLineId, out int ReproductionNeedsCode, [In][MarshalAs(UnmanagedType.IUnknown)] object Connection)
	{
		//ILSpy generated this explicit interface implementation from .override directive in CalculateReproductionNeeds
		return this.CalculateReproductionNeeds(ProductionLineId, out ReproductionNeedsCode, Connection);
	}
}
