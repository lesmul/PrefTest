using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[TypeLibType(TypeLibTypeFlags.FDual | TypeLibTypeFlags.FNonExtensible | TypeLibTypeFlags.FDispatchable)]
[Guid("91D00E8C-6B7E-4CC0-BB24-CC833F7500B6")]
public interface IMaterialNeeds
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
	object PrefUserLink
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
	string Description
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
	bool ReserveMaterial
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
	bool OptimizeRods
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
	int OptimizationLevel
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
	bool UseRemnants
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
	bool DontForceMaxRodsAllowed
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
	int UseWastageAllowance
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
	int UseFullRods
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
	int ProductionPlantCode
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
	bool DeleteOtherDocumentsNeeds
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
	int Number
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743832)]
		get;
	}

	[DispId(1610743833)]
	int Version
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743833)]
		get;
	}

	[DispId(1610743834)]
	int ProductionLot
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743834)]
		get;
	}

	[DispId(1610743835)]
	int MNSet
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743835)]
		get;
	}

	[DispId(1610743836)]
	string SessionId
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743836)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743837)]
	bool CalculateSalesDocument([In] int Number, [In] int Version, [In][MarshalAs(UnmanagedType.IUnknown)] object Connection);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743838)]
	bool CalculateProductionLot([In] int ProductionLot, [In][MarshalAs(UnmanagedType.IUnknown)] object Connection);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743839)]
	bool CalculateReproductionNeeds([In][MarshalAs(UnmanagedType.BStr)] string ProductionLineId, out int ReproductionNeedsCode, [In][MarshalAs(UnmanagedType.IUnknown)] object Connection);
}
