using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Interop.MSXML2;
using Interop.PrefCAD;
using Interop.PrefComponents;
using Interop.PrefErrors;
using Interop.PrefPrices;

namespace Interop.PrefSales;

[ComImport]
[TypeLibType(TypeLibTypeFlags.FCanCreate)]
[ClassInterface(ClassInterfaceType.None)]
[Guid("DDCFC8B3-5D89-4D5A-89DC-0414E413A322")]
[ComSourceInterfaces("Interop.PrefSales._ISalesDocEvents")]
public class SalesDocClass : ISalesDoc, SalesDoc, _ISalesDocEvents_Event
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
	public virtual extern int CommandTimeout
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743810)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743810)]
		[param: In]
		set;
	}

	[DispId(1610743812)]
	public virtual extern bool WebMode
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743812)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743812)]
		[param: In]
		set;
	}

	[ComAliasName("PrefCAD.UnitKind")]
	[DispId(1610743814)]
	public virtual extern UnitKind UnitMode
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743814)]
		[return: ComAliasName("PrefCAD.UnitKind")]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743814)]
		[param: In]
		[param: ComAliasName("PrefCAD.UnitKind")]
		set;
	}

	[DispId(1610743816)]
	public virtual extern int UserCode
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
	public virtual extern bool LockDocument
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
	public virtual extern bool IsLockedByAnotherUser
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743820)]
		get;
	}

	[DispId(1610743821)]
	public virtual extern int LockedByUserCode
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743821)]
		get;
	}

	[DispId(1610743822)]
	public virtual extern int AuditMode
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743822)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743822)]
		[param: In]
		set;
	}

	[ComAliasName("Interop.PrefSales.AuditSaveType")]
	[DispId(1610743824)]
	public virtual extern AuditSaveType AuditSaveMode
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743824)]
		[return: ComAliasName("Interop.PrefSales.AuditSaveType")]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743824)]
		[param: In]
		[param: ComAliasName("Interop.PrefSales.AuditSaveType")]
		set;
	}

	[DispId(1610743826)]
	public virtual extern bool ResellerVersion
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
	public virtual extern SalesDocFields Fields
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743828)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	[DispId(1610743829)]
	public virtual extern SalesDocProperties UserProperties
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743829)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	[ComAliasName("Interop.PrefSales.SalesDocState")]
	[DispId(1610743830)]
	public virtual extern SalesDocState State
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743830)]
		[return: ComAliasName("Interop.PrefSales.SalesDocState")]
		get;
	}

	[DispId(1610743831)]
	public virtual extern bool Blocked
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743831)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743831)]
		[param: In]
		set;
	}

	[DispId(1610743833)]
	public virtual extern bool ItemsBlocked
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743833)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743833)]
		[param: In]
		set;
	}

	[DispId(1610743835)]
	public virtual extern Options Options
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743835)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743835)]
		[param: In]
		[param: MarshalAs(UnmanagedType.Interface)]
		set;
	}

	[DispId(1610743837)]
	public virtual extern bool HasSubOrders
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743837)]
		get;
	}

	[DispId(1610743838)]
	public virtual extern bool IsSubOrder
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743838)]
		get;
	}

	[ComAliasName("Interop.PrefSales.SalesDocType")]
	[DispId(1610743839)]
	public virtual extern SalesDocType Type
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743839)]
		[return: ComAliasName("Interop.PrefSales.SalesDocType")]
		get;
	}

	[DispId(1610743840)]
	public virtual extern bool NeedRecalculate
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743840)]
		get;
	}

	[DispId(1610743841)]
	public virtual extern bool NeedRecalculateConstructiveData
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743841)]
		get;
	}

	[DispId(1610743842)]
	public virtual extern bool NeedRecalculatePrices
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743842)]
		get;
	}

	[DispId(1610743843)]
	public virtual extern int Severity
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743843)]
		get;
	}

	[DispId(1610743844)]
	public virtual extern object SalesGUI
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743844)]
		[return: MarshalAs(UnmanagedType.IUnknown)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743844)]
		[param: In]
		[param: MarshalAs(UnmanagedType.IUnknown)]
		set;
	}

	[DispId(1610743846)]
	public virtual extern object PrefUserLink
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743846)]
		[return: MarshalAs(UnmanagedType.IUnknown)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743846)]
		[param: In]
		[param: MarshalAs(UnmanagedType.IUnknown)]
		set;
	}

	[DispId(1610743848)]
	public virtual extern object PrefDatabase
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743848)]
		[return: MarshalAs(UnmanagedType.IUnknown)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743848)]
		[param: In]
		[param: MarshalAs(UnmanagedType.IUnknown)]
		set;
	}

	[DispId(1610743850)]
	public virtual extern RawMaterialVolumeItems RawMaterialVolumeItems
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743850)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	[DispId(1610743851)]
	public virtual extern ulong RowVersion
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743851)]
		get;
	}

	[DispId(1610743852)]
	public virtual extern bool CalculateDrafts
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743852)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743852)]
		[param: In]
		set;
	}

	[DispId(1610743854)]
	public virtual extern Application PrefCADApp
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[TypeLibFunc(TypeLibFuncFlags.FHidden)]
		[DispId(1610743854)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	[DispId(1610743855)]
	public virtual extern long ProjectCode
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743855)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743855)]
		[param: In]
		set;
	}

	[DispId(1610743857)]
	public virtual extern string ProjectCodeAsString
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743857)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
	}

	[DispId(1610743858)]
	public virtual extern DateTime CalculatedMeasurementDate
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743858)]
		get;
	}

	[DispId(1610743859)]
	public virtual extern DateTime CalculatedShopEntryDate
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743859)]
		get;
	}

	[DispId(1610743860)]
	public virtual extern DateTime CalculatedShopExitDate
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743860)]
		get;
	}

	[DispId(1610743861)]
	public virtual extern DateTime CalculatedShippingDate
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743861)]
		get;
	}

	[DispId(1610743862)]
	public virtual extern DateTime CalculatedDeliveryDate
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743862)]
		get;
	}

	[DispId(1610743863)]
	public virtual extern DateTime CalculatedInstallationDate
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743863)]
		get;
	}

	[DispId(1610743864)]
	public virtual extern bool UseRemoteFactory
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743864)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743864)]
		[param: In]
		set;
	}

	[DispId(1610743866)]
	public virtual extern object PlUnknown
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743866)]
		[TypeLibFunc(TypeLibFuncFlags.FHidden)]
		[return: MarshalAs(UnmanagedType.IUnknown)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743866)]
		[TypeLibFunc(TypeLibFuncFlags.FHidden)]
		[param: In]
		[param: MarshalAs(UnmanagedType.IUnknown)]
		set;
	}

	[DispId(1610743895)]
	public virtual extern bool IsActiveOffer
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743895)]
		get;
	}

	[DispId(1610743896)]
	public virtual extern bool CanAddOffer
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743896)]
		get;
	}

	[DispId(1610743898)]
	public virtual extern bool CanRemoveVersion
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743898)]
		get;
	}

	[DispId(1610743900)]
	public virtual extern bool CanRemoveNotActiveOffers
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743900)]
		get;
	}

	[DispId(1610743902)]
	public virtual extern bool CanRemoveDocument
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743902)]
		get;
	}

	[DispId(1610743904)]
	public virtual extern bool CanSetAsActiveOffer
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743904)]
		get;
	}

	[DispId(1610743906)]
	public virtual extern bool CanAcceptOffer
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743906)]
		get;
	}

	[DispId(1610743908)]
	public virtual extern bool IsAcceptedOffer
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743908)]
		get;
	}

	[DispId(1610743909)]
	public virtual extern bool CanUndoAcceptOffer
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743909)]
		get;
	}

	[DispId(1610743911)]
	public virtual extern bool CanCreateOrder
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743911)]
		get;
	}

	[DispId(1610743913)]
	public virtual extern bool CanConfirmOrder
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743913)]
		get;
	}

	[DispId(1610743915)]
	public virtual extern bool PricesAreConsolidated
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743915)]
		get;
	}

	[DispId(1610743916)]
	public virtual extern bool CanPublishDocument
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743916)]
		get;
	}

	[DispId(1610743918)]
	public virtual extern bool IsPublicDocument
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743918)]
		get;
	}

	[DispId(1610743920)]
	public virtual extern bool CanChangeOwner
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743920)]
		get;
	}

	[DispId(1610743922)]
	public virtual extern bool IsOwner
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743922)]
		get;
	}

	[DispId(1610743924)]
	public virtual extern bool CanSetAsReadyForFactory
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743924)]
		get;
	}

	[DispId(1610743927)]
	public virtual extern bool IsReadyForFactory
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743927)]
		get;
	}

	[DispId(1610743928)]
	public virtual extern short PDEOperationInProgress
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743928)]
		get;
	}

	[DispId(1610743929)]
	public virtual extern SalesDocItems Items
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743929)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	[DispId(1610743930)]
	public virtual extern double Amount
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743930)]
		get;
	}

	[DispId(1610743931)]
	public virtual extern double SubTotal
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743931)]
		get;
	}

	[DispId(1610743932)]
	public virtual extern double Tax1
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743932)]
		get;
	}

	[DispId(1610743933)]
	public virtual extern double Tax2
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743933)]
		get;
	}

	[DispId(1610743934)]
	public virtual extern double Total
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743934)]
		get;
	}

	[DispId(1610743935)]
	public virtual extern double PriceAfterDiscounts
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743935)]
		get;
	}

	[DispId(1610743936)]
	public virtual extern double PriceAfterSubTotals
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743936)]
		get;
	}

	[DispId(1610743937)]
	public virtual extern double PriceAfterDiscountsAndTaxes
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743937)]
		get;
	}

	[DispId(1610743938)]
	public virtual extern string Currency
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743938)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
	}

	[DispId(1610743939)]
	public virtual extern int CurrencyDecimals
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743939)]
		get;
	}

	[DispId(1610743940)]
	public virtual extern string CurrencySymbol
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743940)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
	}

	[DispId(1610743941)]
	public virtual extern double CurrencyRatio
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743941)]
		get;
	}

	[DispId(1610743942)]
	public virtual extern string PrintCurrency
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743942)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
	}

	[DispId(1610743943)]
	public virtual extern int PrintCurrencyDecimals
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743943)]
		get;
	}

	[DispId(1610743944)]
	public virtual extern string PrintCurrencySymbol
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743944)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
	}

	[DispId(1610743945)]
	public virtual extern double PrintCurrencyExchangeRate
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743945)]
		get;
	}

	[DispId(1610743946)]
	public virtual extern string XMLDoc
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743946)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
	}

	[DispId(1610743947)]
	public virtual extern string XMLDocToFactory
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743947)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
	}

	[DispId(1610743948)]
	public virtual extern string DataVersion
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743948)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743948)]
		[param: In]
		[param: MarshalAs(UnmanagedType.BStr)]
		set;
	}

	[DispId(1610743950)]
	public virtual extern string InstancesSubModelXML
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743950)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
	}

	[DispId(1610743952)]
	public virtual extern string SalesDocMeasuresXML
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743952)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
	}

	[DispId(1610743953)]
	public virtual extern int Language
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743953)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743953)]
		[param: In]
		set;
	}

	[DispId(1610743955)]
	public virtual extern LongCollection AdditionalLanguages
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743955)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743955)]
		[param: In]
		[param: MarshalAs(UnmanagedType.Interface)]
		set;
	}

	[DispId(1610743960)]
	public virtual extern SalesDocSubTotals SubTotals
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743960)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	[DispId(1610743961)]
	public virtual extern SalesDocDates Dates
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743961)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	[DispId(1610743962)]
	public virtual extern SalesDocVersions Versions
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743962)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	[DispId(1610743963)]
	public virtual extern SalesDocAlternatives Alternatives
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743963)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	[DispId(1610743964)]
	public virtual extern SalesDocTariffs Tariffs
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743964)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	[DispId(1610743965)]
	public virtual extern string TariffChainsXML
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743965)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743965)]
		[param: In]
		[param: MarshalAs(UnmanagedType.BStr)]
		set;
	}

	[DispId(1610743967)]
	public virtual extern bool CheckTariffChanges
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743967)]
		get;
	}

	[DispId(1610743968)]
	public virtual extern Errors Errors
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743968)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	[DispId(1610743971)]
	public virtual extern int ProcurementDays
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743971)]
		get;
	}

	[DispId(1610743972)]
	public virtual extern int ProductionDays
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743972)]
		get;
	}

	[DispId(1610743973)]
	public virtual extern int ShippingDays
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743973)]
		get;
	}

	[DispId(1610743980)]
	public virtual extern bool IsDefaultShippingAddress
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743980)]
		get;
	}

	[DispId(1610743981)]
	public virtual extern bool IsDefaultBillingAddress
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743981)]
		get;
	}

	public virtual extern event _ISalesDocEvents_IsModelEventHandler IsModel;

	public virtual extern event _ISalesDocEvents_IsMaterialEventHandler IsMaterial;

	public virtual extern event _ISalesDocEvents_IsScriptEventHandler IsScript;

	public virtual extern event _ISalesDocEvents_BeforeSaveEventHandler BeforeSave;

	public virtual extern event _ISalesDocEvents_AfterSaveEventHandler AfterSave;

	public virtual extern event _ISalesDocEvents_AfterLoadEventHandler AfterLoad;

	public virtual extern event _ISalesDocEvents_BeforeConfirmOrderEventHandler BeforeConfirmOrder;

	public virtual extern event _ISalesDocEvents_AfterConfirmOrderEventHandler AfterConfirmOrder;

	public virtual extern event _ISalesDocEvents_BeforeRemoveDocumentEventHandler BeforeRemoveDocument;

	public virtual extern event _ISalesDocEvents_AfterRemoveDocumentEventHandler AfterRemoveDocument;

	public virtual extern event _ISalesDocEvents_BeforeRemoveOfferEventHandler BeforeRemoveOffer;

	public virtual extern event _ISalesDocEvents_BeforeRemoveOrderEventHandler BeforeRemoveOrder;

	public virtual extern event _ISalesDocEvents_BeforeRemoveDeliveryNoteEventHandler BeforeRemoveDeliveryNote;

	public virtual extern event _ISalesDocEvents_BeforeRemoveInvoiceEventHandler BeforeRemoveInvoice;

	public virtual extern event _ISalesDocEvents_BeforeRemoveSubOrderEventHandler BeforeRemoveSubOrder;

	public virtual extern event _ISalesDocEvents_BeforeRemoveNotActiveOffersEventHandler BeforeRemoveNotActiveOffers;

	public virtual extern event _ISalesDocEvents_RecalculatePerVolumeEventHandler _ISalesDocEvents_Event_RecalculatePerVolume;

	public virtual extern event _ISalesDocEvents_AfterItemChangedEventHandler AfterItemChanged;

	public virtual extern event _ISalesDocEvents_AfterItemInsertedEventHandler AfterItemInserted;

	public virtual extern event _ISalesDocEvents_BeforeItemDeletedEventHandler BeforeItemDeleted;

	public virtual extern event _ISalesDocEvents_AfterItemDeletedEventHandler AfterItemDeleted;

	public virtual extern event _ISalesDocEvents_BeforeSelectCustomerEventHandler BeforeSelectCustomer;

	public virtual extern event _ISalesDocEvents_AfterCalculateCommissionEventHandler AfterCalculateCommission;

	public virtual extern event _ISalesDocEvents_AfterFieldChangedEventHandler AfterFieldChanged;

	public virtual extern event _ISalesDocEvents_FormatAliasEventHandler FormatAlias;

	public virtual extern event _ISalesDocEvents_ModifySalesmanSurchargeEventHandler ModifySalesmanSurcharge;

	public virtual extern event _ISalesDocEvents_BlockDocumentEventHandler BlockDocument;

	public virtual extern event _ISalesDocEvents_BeforeCancelDocumentEventHandler BeforeCancelDocument;

	public virtual extern event _ISalesDocEvents_AfterCancelDocumentEventHandler AfterCancelDocument;

	public virtual extern event _ISalesDocEvents_ValidateEventHandler _ISalesDocEvents_Event_Validate;

	public virtual extern event _ISalesDocEvents_BeforeRecalculateItemEventHandler BeforeRecalculateItem;

	public virtual extern event _ISalesDocEvents_AfterRecalculateItemEventHandler AfterRecalculateItem;

	public virtual extern event _ISalesDocEvents_AfterSetItemCodeEventHandler AfterSetItemCode;

	public virtual extern event _ISalesDocEvents_BeforeSetItemContextEventHandler BeforeSetItemContext;

	public virtual extern event _ISalesDocEvents_BeforeCreateSalesDocEventHandler BeforeCreateSalesDoc;

	public virtual extern event _ISalesDocEvents_AfterCreateSalesDocEventHandler AfterCreateSalesDoc;

	public virtual extern event _ISalesDocEvents_BeforeSetItemXmlEventHandler BeforeSetItemXml;

	public virtual extern event _ISalesDocEvents_AfterSetItemXmlEventHandler AfterSetItemXml;

	public virtual extern event _ISalesDocEvents_BeforeRecalculateItemPricesEventHandler BeforeRecalculateItemPrices;

	public virtual extern event _ISalesDocEvents_AfterRecalculateItemPricesEventHandler AfterRecalculateItemPrices;

	public virtual extern event _ISalesDocEvents_BeforeCalculateSchedulingEventHandler BeforeCalculateScheduling;

	public virtual extern event _ISalesDocEvents_AfterCalculateSchedulingEventHandler AfterCalculateScheduling;

	public virtual extern event _ISalesDocEvents_NewWizardSelectNumerationEventHandler NewWizardSelectNumeration;

	public virtual extern event _ISalesDocEvents_AfterNewVersionEventHandler AfterNewVersion;

	public virtual extern event _ISalesDocEvents_BeforeSetDataVersionEventHandler BeforeSetDataVersion;

	public virtual extern event _ISalesDocEvents_SearchConsolidationRiskPonderationEventHandler SearchConsolidationRiskPonderation;

	public virtual extern event _ISalesDocEvents_ReplaceItemCodeEventHandler ReplaceItemCode;

	public virtual extern event _ISalesDocEvents_AfterReplaceItemCodeEventHandler AfterReplaceItemCode;

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern SalesDocClass();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743868)]
	public virtual extern void New([In] int NumerationId, [In] int UserCode);

	void ISalesDoc.New([In] int NumerationId, [In] int UserCode)
	{
		//ILSpy generated this explicit interface implementation from .override directive in New
		this.New(NumerationId, UserCode);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743869)]
	public virtual extern void NewFromXML([In][MarshalAs(UnmanagedType.IUnknown)] object Connection, [In] int NumerationId, [In] int UserCode, [In][MarshalAs(UnmanagedType.BStr)] string XML);

	void ISalesDoc.NewFromXML([In][MarshalAs(UnmanagedType.IUnknown)] object Connection, [In] int NumerationId, [In] int UserCode, [In][MarshalAs(UnmanagedType.BStr)] string XML)
	{
		//ILSpy generated this explicit interface implementation from .override directive in NewFromXML
		this.NewFromXML(Connection, NumerationId, UserCode, XML);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743870)]
	public virtual extern void Load([In] int Number, [In] int Version);

	void ISalesDoc.Load([In] int Number, [In] int Version)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Load
		this.Load(Number, Version);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743871)]
	public virtual extern void CancelarDocumento([In] int Number, [In] int Version, [In][MarshalAs(UnmanagedType.BStr)] string message);

	void ISalesDoc.CancelarDocumento([In] int Number, [In] int Version, [In][MarshalAs(UnmanagedType.BStr)] string message)
	{
		//ILSpy generated this explicit interface implementation from .override directive in CancelarDocumento
		this.CancelarDocumento(Number, Version, message);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743872)]
	public virtual extern void LoadFromXML([In][MarshalAs(UnmanagedType.BStr)] string XML);

	void ISalesDoc.LoadFromXML([In][MarshalAs(UnmanagedType.BStr)] string XML)
	{
		//ILSpy generated this explicit interface implementation from .override directive in LoadFromXML
		this.LoadFromXML(XML);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743873)]
	public virtual extern void Reload();

	void ISalesDoc.Reload()
	{
		//ILSpy generated this explicit interface implementation from .override directive in Reload
		this.Reload();
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743874)]
	public virtual extern void Reset();

	void ISalesDoc.Reset()
	{
		//ILSpy generated this explicit interface implementation from .override directive in Reset
		this.Reset();
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743875)]
	public virtual extern void Save();

	void ISalesDoc.Save()
	{
		//ILSpy generated this explicit interface implementation from .override directive in Save
		this.Save();
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743876)]
	public virtual extern void Recalculate();

	void ISalesDoc.Recalculate()
	{
		//ILSpy generated this explicit interface implementation from .override directive in Recalculate
		this.Recalculate();
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743877)]
	public virtual extern void AutomaticRecalculate();

	void ISalesDoc.AutomaticRecalculate()
	{
		//ILSpy generated this explicit interface implementation from .override directive in AutomaticRecalculate
		this.AutomaticRecalculate();
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743878)]
	public virtual extern void RecalculatePrices();

	void ISalesDoc.RecalculatePrices()
	{
		//ILSpy generated this explicit interface implementation from .override directive in RecalculatePrices
		this.RecalculatePrices();
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743879)]
	public virtual extern void RecalculatePerVolume();

	void ISalesDoc.RecalculatePerVolume()
	{
		//ILSpy generated this explicit interface implementation from .override directive in RecalculatePerVolume
		this.RecalculatePerVolume();
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743880)]
	public virtual extern void SetAsModified();

	void ISalesDoc.SetAsModified()
	{
		//ILSpy generated this explicit interface implementation from .override directive in SetAsModified
		this.SetAsModified();
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743881)]
	public virtual extern bool ChangeCustomer([In] int CustomerCode, [In][MarshalAs(UnmanagedType.BStr)] string EntityId);

	bool ISalesDoc.ChangeCustomer([In] int CustomerCode, [In][MarshalAs(UnmanagedType.BStr)] string EntityId)
	{
		//ILSpy generated this explicit interface implementation from .override directive in ChangeCustomer
		return this.ChangeCustomer(CustomerCode, EntityId);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743882)]
	public virtual extern void CalculateCommission();

	void ISalesDoc.CalculateCommission()
	{
		//ILSpy generated this explicit interface implementation from .override directive in CalculateCommission
		this.CalculateCommission();
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743883)]
	[return: MarshalAs(UnmanagedType.BStr)]
	public virtual extern string GetAlias();

	string ISalesDoc.GetAlias()
	{
		//ILSpy generated this explicit interface implementation from .override directive in GetAlias
		return this.GetAlias();
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743884)]
	public virtual extern void UpdateGUI();

	void ISalesDoc.UpdateGUI()
	{
		//ILSpy generated this explicit interface implementation from .override directive in UpdateGUI
		this.UpdateGUI();
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743885)]
	public virtual extern bool Validate();

	bool ISalesDoc.Validate()
	{
		//ILSpy generated this explicit interface implementation from .override directive in Validate
		return this.Validate();
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743886)]
	[TypeLibFunc(TypeLibFuncFlags.FHidden)]
	public virtual extern bool FireBeforeCancelDocument([In][MarshalAs(UnmanagedType.BStr)] string Filter);

	bool ISalesDoc.FireBeforeCancelDocument([In][MarshalAs(UnmanagedType.BStr)] string Filter)
	{
		//ILSpy generated this explicit interface implementation from .override directive in FireBeforeCancelDocument
		return this.FireBeforeCancelDocument(Filter);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743887)]
	[TypeLibFunc(TypeLibFuncFlags.FHidden)]
	public virtual extern void FireAfterCancelDocument([In][MarshalAs(UnmanagedType.BStr)] string Filter);

	void ISalesDoc.FireAfterCancelDocument([In][MarshalAs(UnmanagedType.BStr)] string Filter)
	{
		//ILSpy generated this explicit interface implementation from .override directive in FireAfterCancelDocument
		this.FireAfterCancelDocument(Filter);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[TypeLibFunc(TypeLibFuncFlags.FHidden)]
	[DispId(1610743888)]
	public virtual extern void FireRecalculatePerVolume([In][MarshalAs(UnmanagedType.Interface)] TariffExceptions TariffExceptions, [In][MarshalAs(UnmanagedType.Interface)] RawMaterialVolumeItems RawMaterial);

	void ISalesDoc.FireRecalculatePerVolume([In][MarshalAs(UnmanagedType.Interface)] TariffExceptions TariffExceptions, [In][MarshalAs(UnmanagedType.Interface)] RawMaterialVolumeItems RawMaterial)
	{
		//ILSpy generated this explicit interface implementation from .override directive in FireRecalculatePerVolume
		this.FireRecalculatePerVolume(TariffExceptions, RawMaterial);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743889)]
	public virtual extern bool ExecuteCommandsWith([In][MarshalAs(UnmanagedType.Interface)] IXMLDOMDocument2 Command, [MarshalAs(UnmanagedType.Interface)] out IXMLDOMDocument2 Result, [In][MarshalAs(UnmanagedType.IUnknown)] object Connection, [MarshalAs(UnmanagedType.Interface)] out PrefMessages Messages);

	bool ISalesDoc.ExecuteCommandsWith([In][MarshalAs(UnmanagedType.Interface)] IXMLDOMDocument2 Command, [MarshalAs(UnmanagedType.Interface)] out IXMLDOMDocument2 Result, [In][MarshalAs(UnmanagedType.IUnknown)] object Connection, [MarshalAs(UnmanagedType.Interface)] out PrefMessages Messages)
	{
		//ILSpy generated this explicit interface implementation from .override directive in ExecuteCommandsWith
		return this.ExecuteCommandsWith(Command, out Result, Connection, out Messages);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743890)]
	public virtual extern bool Create();

	bool ISalesDoc.Create()
	{
		//ILSpy generated this explicit interface implementation from .override directive in Create
		return this.Create();
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743891)]
	public virtual extern void ReloadPricesAfterPDEDiscounts();

	void ISalesDoc.ReloadPricesAfterPDEDiscounts()
	{
		//ILSpy generated this explicit interface implementation from .override directive in ReloadPricesAfterPDEDiscounts
		this.ReloadPricesAfterPDEDiscounts();
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743892)]
	public virtual extern void ReloadRedefinedCurrencies();

	void ISalesDoc.ReloadRedefinedCurrencies()
	{
		//ILSpy generated this explicit interface implementation from .override directive in ReloadRedefinedCurrencies
		this.ReloadRedefinedCurrencies();
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743893)]
	public virtual extern void ConsolidatePrices();

	void ISalesDoc.ConsolidatePrices()
	{
		//ILSpy generated this explicit interface implementation from .override directive in ConsolidatePrices
		this.ConsolidatePrices();
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743894)]
	public virtual extern void CalculateProductionData([In][MarshalAs(UnmanagedType.BStr)] string XMLProperties);

	void ISalesDoc.CalculateProductionData([In][MarshalAs(UnmanagedType.BStr)] string XMLProperties)
	{
		//ILSpy generated this explicit interface implementation from .override directive in CalculateProductionData
		this.CalculateProductionData(XMLProperties);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743897)]
	public virtual extern int AddOffer([In][MarshalAs(UnmanagedType.BStr)] string VersionName);

	int ISalesDoc.AddOffer([In][MarshalAs(UnmanagedType.BStr)] string VersionName)
	{
		//ILSpy generated this explicit interface implementation from .override directive in AddOffer
		return this.AddOffer(VersionName);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743899)]
	public virtual extern bool RemoveVersion();

	bool ISalesDoc.RemoveVersion()
	{
		//ILSpy generated this explicit interface implementation from .override directive in RemoveVersion
		return this.RemoveVersion();
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743901)]
	public virtual extern bool RemoveNotActiveOffers();

	bool ISalesDoc.RemoveNotActiveOffers()
	{
		//ILSpy generated this explicit interface implementation from .override directive in RemoveNotActiveOffers
		return this.RemoveNotActiveOffers();
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743903)]
	public virtual extern bool RemoveDocument();

	bool ISalesDoc.RemoveDocument()
	{
		//ILSpy generated this explicit interface implementation from .override directive in RemoveDocument
		return this.RemoveDocument();
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743905)]
	public virtual extern void SetAsActiveOffer();

	void ISalesDoc.SetAsActiveOffer()
	{
		//ILSpy generated this explicit interface implementation from .override directive in SetAsActiveOffer
		this.SetAsActiveOffer();
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743907)]
	public virtual extern void AcceptOffer();

	void ISalesDoc.AcceptOffer()
	{
		//ILSpy generated this explicit interface implementation from .override directive in AcceptOffer
		this.AcceptOffer();
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743910)]
	public virtual extern void UndoAcceptOffer();

	void ISalesDoc.UndoAcceptOffer()
	{
		//ILSpy generated this explicit interface implementation from .override directive in UndoAcceptOffer
		this.UndoAcceptOffer();
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743912)]
	public virtual extern int CreateOrder([In][MarshalAs(UnmanagedType.BStr)] string Name);

	int ISalesDoc.CreateOrder([In][MarshalAs(UnmanagedType.BStr)] string Name)
	{
		//ILSpy generated this explicit interface implementation from .override directive in CreateOrder
		return this.CreateOrder(Name);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743914)]
	public virtual extern void ConfirmOrder();

	void ISalesDoc.ConfirmOrder()
	{
		//ILSpy generated this explicit interface implementation from .override directive in ConfirmOrder
		this.ConfirmOrder();
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743917)]
	public virtual extern int PublishDocument();

	int ISalesDoc.PublishDocument()
	{
		//ILSpy generated this explicit interface implementation from .override directive in PublishDocument
		return this.PublishDocument();
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743919)]
	public virtual extern void EnsureVariablesAreFilled();

	void ISalesDoc.EnsureVariablesAreFilled()
	{
		//ILSpy generated this explicit interface implementation from .override directive in EnsureVariablesAreFilled
		this.EnsureVariablesAreFilled();
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743921)]
	public virtual extern void ChangeOwner();

	void ISalesDoc.ChangeOwner()
	{
		//ILSpy generated this explicit interface implementation from .override directive in ChangeOwner
		this.ChangeOwner();
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743923)]
	public virtual extern void OwnerChanged([In] bool vbChanged);

	void ISalesDoc.OwnerChanged([In] bool vbChanged)
	{
		//ILSpy generated this explicit interface implementation from .override directive in OwnerChanged
		this.OwnerChanged(vbChanged);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743925)]
	public virtual extern void SetAsReadyForFactory();

	void ISalesDoc.SetAsReadyForFactory()
	{
		//ILSpy generated this explicit interface implementation from .override directive in SetAsReadyForFactory
		this.SetAsReadyForFactory();
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743926)]
	public virtual extern void UndoReadyForFactory();

	void ISalesDoc.UndoReadyForFactory()
	{
		//ILSpy generated this explicit interface implementation from .override directive in UndoReadyForFactory
		this.UndoReadyForFactory();
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743951)]
	public virtual extern bool SaveInstancesSubModelXML([In][MarshalAs(UnmanagedType.BStr)] string newVal);

	bool ISalesDoc.SaveInstancesSubModelXML([In][MarshalAs(UnmanagedType.BStr)] string newVal)
	{
		//ILSpy generated this explicit interface implementation from .override directive in SaveInstancesSubModelXML
		return this.SaveInstancesSubModelXML(newVal);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743957)]
	public virtual extern bool GetDeliveryDescription([In] int Position, [MarshalAs(UnmanagedType.BStr)] out string RowId, [MarshalAs(UnmanagedType.BStr)] out string DescriptionId, [MarshalAs(UnmanagedType.BStr)] out string Description);

	bool ISalesDoc.GetDeliveryDescription([In] int Position, [MarshalAs(UnmanagedType.BStr)] out string RowId, [MarshalAs(UnmanagedType.BStr)] out string DescriptionId, [MarshalAs(UnmanagedType.BStr)] out string Description)
	{
		//ILSpy generated this explicit interface implementation from .override directive in GetDeliveryDescription
		return this.GetDeliveryDescription(Position, out RowId, out DescriptionId, out Description);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743958)]
	public virtual extern void SetDeliveryDescription([In] int Position, [In][MarshalAs(UnmanagedType.BStr)] string RowId, [In][MarshalAs(UnmanagedType.BStr)] string DescriptionId, [In][MarshalAs(UnmanagedType.BStr)] string Description);

	void ISalesDoc.SetDeliveryDescription([In] int Position, [In][MarshalAs(UnmanagedType.BStr)] string RowId, [In][MarshalAs(UnmanagedType.BStr)] string DescriptionId, [In][MarshalAs(UnmanagedType.BStr)] string Description)
	{
		//ILSpy generated this explicit interface implementation from .override directive in SetDeliveryDescription
		this.SetDeliveryDescription(Position, RowId, DescriptionId, Description);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743959)]
	public virtual extern void ClearDeliveryDescriptions();

	void ISalesDoc.ClearDeliveryDescriptions()
	{
		//ILSpy generated this explicit interface implementation from .override directive in ClearDeliveryDescriptions
		this.ClearDeliveryDescriptions();
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743969)]
	public virtual extern void ResetErrors();

	void ISalesDoc.ResetErrors()
	{
		//ILSpy generated this explicit interface implementation from .override directive in ResetErrors
		this.ResetErrors();
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743970)]
	public virtual extern int CalculateScheduling([In] DateTime date, [In][ComAliasName("Interop.PrefSales.TaskNames")] TaskNames lastTask, [In] bool book);

	int ISalesDoc.CalculateScheduling([In] DateTime date, [In][ComAliasName("Interop.PrefSales.TaskNames")] TaskNames lastTask, [In] bool book)
	{
		//ILSpy generated this explicit interface implementation from .override directive in CalculateScheduling
		return this.CalculateScheduling(date, lastTask, book);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743974)]
	public virtual extern void CalculateCommercialDeliveryDate();

	void ISalesDoc.CalculateCommercialDeliveryDate()
	{
		//ILSpy generated this explicit interface implementation from .override directive in CalculateCommercialDeliveryDate
		this.CalculateCommercialDeliveryDate();
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743975)]
	[return: MarshalAs(UnmanagedType.BStr)]
	public virtual extern string SelectAreaFromPostalCode();

	string ISalesDoc.SelectAreaFromPostalCode()
	{
		//ILSpy generated this explicit interface implementation from .override directive in SelectAreaFromPostalCode
		return this.SelectAreaFromPostalCode();
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743976)]
	public virtual extern DateTime GetNextAvailableDate([In] DateTime date, [In][ComAliasName("Interop.PrefSales.TaskNames")] TaskNames TaskId);

	DateTime ISalesDoc.GetNextAvailableDate([In] DateTime date, [In][ComAliasName("Interop.PrefSales.TaskNames")] TaskNames TaskId)
	{
		//ILSpy generated this explicit interface implementation from .override directive in GetNextAvailableDate
		return this.GetNextAvailableDate(date, TaskId);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743977)]
	public virtual extern bool UpdateSchedulingTasks();

	bool ISalesDoc.UpdateSchedulingTasks()
	{
		//ILSpy generated this explicit interface implementation from .override directive in UpdateSchedulingTasks
		return this.UpdateSchedulingTasks();
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743978)]
	[return: ComAliasName("Interop.PrefSales.SalesDocDeliveryDateChecking")]
	public virtual extern SalesDocDeliveryDateChecking CheckDateStatus([In] DateTime newVal, [In][ComAliasName("Interop.PrefSales.TaskNames")] TaskNames task);

	SalesDocDeliveryDateChecking ISalesDoc.CheckDateStatus([In] DateTime newVal, [In][ComAliasName("Interop.PrefSales.TaskNames")] TaskNames task)
	{
		//ILSpy generated this explicit interface implementation from .override directive in CheckDateStatus
		return this.CheckDateStatus(newVal, task);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743979)]
	[return: MarshalAs(UnmanagedType.BStr)]
	public virtual extern string XMLSalesDocumentReportPDF([In][MarshalAs(UnmanagedType.BStr)] string LayoutKind, [In][MarshalAs(UnmanagedType.BStr)] string LayoutDocument, [In][MarshalAs(UnmanagedType.BStr)] string LayoutName, [In][MarshalAs(UnmanagedType.BStr)] string OutputDirectory, [In][MarshalAs(UnmanagedType.BStr)] string OutputFile, [In][MarshalAs(UnmanagedType.BStr)] string OptionsXML, [In] int UnitsMode, [In] int lLanguageId, [MarshalAs(UnmanagedType.BStr)] out string pMessage);

	string ISalesDoc.XMLSalesDocumentReportPDF([In][MarshalAs(UnmanagedType.BStr)] string LayoutKind, [In][MarshalAs(UnmanagedType.BStr)] string LayoutDocument, [In][MarshalAs(UnmanagedType.BStr)] string LayoutName, [In][MarshalAs(UnmanagedType.BStr)] string OutputDirectory, [In][MarshalAs(UnmanagedType.BStr)] string OutputFile, [In][MarshalAs(UnmanagedType.BStr)] string OptionsXML, [In] int UnitsMode, [In] int lLanguageId, [MarshalAs(UnmanagedType.BStr)] out string pMessage)
	{
		//ILSpy generated this explicit interface implementation from .override directive in XMLSalesDocumentReportPDF
		return this.XMLSalesDocumentReportPDF(LayoutKind, LayoutDocument, LayoutName, OutputDirectory, OutputFile, OptionsXML, UnitsMode, lLanguageId, out pMessage);
	}
}
