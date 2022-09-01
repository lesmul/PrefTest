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
[TypeLibType(TypeLibTypeFlags.FDual | TypeLibTypeFlags.FNonExtensible | TypeLibTypeFlags.FDispatchable)]
[Guid("0B0E5F00-BC94-4E20-80E2-1CA3A10E9E03")]
public interface ISalesDoc
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
	int CommandTimeout
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
	bool WebMode
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
	[ComAliasName("PrefCAD.UnitKind")]
	UnitKind UnitMode
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
	int UserCode
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
	bool LockDocument
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
	bool IsLockedByAnotherUser
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743820)]
		get;
	}

	[DispId(1610743821)]
	int LockedByUserCode
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743821)]
		get;
	}

	[DispId(1610743822)]
	int AuditMode
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
	[ComAliasName("Interop.PrefSales.AuditSaveType")]
	AuditSaveType AuditSaveMode
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
	bool ResellerVersion
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
	SalesDocFields Fields
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743828)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	[DispId(1610743829)]
	SalesDocProperties UserProperties
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743829)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	[ComAliasName("Interop.PrefSales.SalesDocState")]
	[DispId(1610743830)]
	SalesDocState State
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743830)]
		[return: ComAliasName("Interop.PrefSales.SalesDocState")]
		get;
	}

	[DispId(1610743831)]
	bool Blocked
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
	bool ItemsBlocked
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
	Options Options
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
	bool HasSubOrders
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743837)]
		get;
	}

	[DispId(1610743838)]
	bool IsSubOrder
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743838)]
		get;
	}

	[DispId(1610743839)]
	[ComAliasName("Interop.PrefSales.SalesDocType")]
	SalesDocType Type
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743839)]
		[return: ComAliasName("Interop.PrefSales.SalesDocType")]
		get;
	}

	[DispId(1610743840)]
	bool NeedRecalculate
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743840)]
		get;
	}

	[DispId(1610743841)]
	bool NeedRecalculateConstructiveData
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743841)]
		get;
	}

	[DispId(1610743842)]
	bool NeedRecalculatePrices
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743842)]
		get;
	}

	[DispId(1610743843)]
	int Severity
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743843)]
		get;
	}

	[DispId(1610743844)]
	object SalesGUI
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
	object PrefUserLink
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
	object PrefDatabase
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
	RawMaterialVolumeItems RawMaterialVolumeItems
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743850)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	[DispId(1610743851)]
	ulong RowVersion
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743851)]
		get;
	}

	[DispId(1610743852)]
	bool CalculateDrafts
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
	Application PrefCADApp
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[TypeLibFunc(TypeLibFuncFlags.FHidden)]
		[DispId(1610743854)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	[DispId(1610743855)]
	long ProjectCode
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
	string ProjectCodeAsString
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743857)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
	}

	[DispId(1610743858)]
	DateTime CalculatedMeasurementDate
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743858)]
		get;
	}

	[DispId(1610743859)]
	DateTime CalculatedShopEntryDate
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743859)]
		get;
	}

	[DispId(1610743860)]
	DateTime CalculatedShopExitDate
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743860)]
		get;
	}

	[DispId(1610743861)]
	DateTime CalculatedShippingDate
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743861)]
		get;
	}

	[DispId(1610743862)]
	DateTime CalculatedDeliveryDate
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743862)]
		get;
	}

	[DispId(1610743863)]
	DateTime CalculatedInstallationDate
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743863)]
		get;
	}

	[DispId(1610743864)]
	bool UseRemoteFactory
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
	object PlUnknown
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[TypeLibFunc(TypeLibFuncFlags.FHidden)]
		[DispId(1610743866)]
		[return: MarshalAs(UnmanagedType.IUnknown)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[TypeLibFunc(TypeLibFuncFlags.FHidden)]
		[DispId(1610743866)]
		[param: In]
		[param: MarshalAs(UnmanagedType.IUnknown)]
		set;
	}

	[DispId(1610743895)]
	bool IsActiveOffer
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743895)]
		get;
	}

	[DispId(1610743896)]
	bool CanAddOffer
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743896)]
		get;
	}

	[DispId(1610743898)]
	bool CanRemoveVersion
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743898)]
		get;
	}

	[DispId(1610743900)]
	bool CanRemoveNotActiveOffers
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743900)]
		get;
	}

	[DispId(1610743902)]
	bool CanRemoveDocument
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743902)]
		get;
	}

	[DispId(1610743904)]
	bool CanSetAsActiveOffer
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743904)]
		get;
	}

	[DispId(1610743906)]
	bool CanAcceptOffer
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743906)]
		get;
	}

	[DispId(1610743908)]
	bool IsAcceptedOffer
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743908)]
		get;
	}

	[DispId(1610743909)]
	bool CanUndoAcceptOffer
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743909)]
		get;
	}

	[DispId(1610743911)]
	bool CanCreateOrder
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743911)]
		get;
	}

	[DispId(1610743913)]
	bool CanConfirmOrder
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743913)]
		get;
	}

	[DispId(1610743915)]
	bool PricesAreConsolidated
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743915)]
		get;
	}

	[DispId(1610743916)]
	bool CanPublishDocument
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743916)]
		get;
	}

	[DispId(1610743918)]
	bool IsPublicDocument
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743918)]
		get;
	}

	[DispId(1610743920)]
	bool CanChangeOwner
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743920)]
		get;
	}

	[DispId(1610743922)]
	bool IsOwner
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743922)]
		get;
	}

	[DispId(1610743924)]
	bool CanSetAsReadyForFactory
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743924)]
		get;
	}

	[DispId(1610743927)]
	bool IsReadyForFactory
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743927)]
		get;
	}

	[DispId(1610743928)]
	short PDEOperationInProgress
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743928)]
		get;
	}

	[DispId(1610743929)]
	SalesDocItems Items
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743929)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	[DispId(1610743930)]
	double Amount
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743930)]
		get;
	}

	[DispId(1610743931)]
	double SubTotal
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743931)]
		get;
	}

	[DispId(1610743932)]
	double Tax1
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743932)]
		get;
	}

	[DispId(1610743933)]
	double Tax2
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743933)]
		get;
	}

	[DispId(1610743934)]
	double Total
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743934)]
		get;
	}

	[DispId(1610743935)]
	double PriceAfterDiscounts
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743935)]
		get;
	}

	[DispId(1610743936)]
	double PriceAfterSubTotals
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743936)]
		get;
	}

	[DispId(1610743937)]
	double PriceAfterDiscountsAndTaxes
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743937)]
		get;
	}

	[DispId(1610743938)]
	string Currency
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743938)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
	}

	[DispId(1610743939)]
	int CurrencyDecimals
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743939)]
		get;
	}

	[DispId(1610743940)]
	string CurrencySymbol
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743940)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
	}

	[DispId(1610743941)]
	double CurrencyRatio
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743941)]
		get;
	}

	[DispId(1610743942)]
	string PrintCurrency
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743942)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
	}

	[DispId(1610743943)]
	int PrintCurrencyDecimals
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743943)]
		get;
	}

	[DispId(1610743944)]
	string PrintCurrencySymbol
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743944)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
	}

	[DispId(1610743945)]
	double PrintCurrencyExchangeRate
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743945)]
		get;
	}

	[DispId(1610743946)]
	string XMLDoc
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743946)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
	}

	[DispId(1610743947)]
	string XMLDocToFactory
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743947)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
	}

	[DispId(1610743948)]
	string DataVersion
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
	string InstancesSubModelXML
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743950)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
	}

	[DispId(1610743952)]
	string SalesDocMeasuresXML
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743952)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
	}

	[DispId(1610743953)]
	int Language
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
	LongCollection AdditionalLanguages
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
	SalesDocSubTotals SubTotals
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743960)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	[DispId(1610743961)]
	SalesDocDates Dates
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743961)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	[DispId(1610743962)]
	SalesDocVersions Versions
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743962)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	[DispId(1610743963)]
	SalesDocAlternatives Alternatives
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743963)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	[DispId(1610743964)]
	SalesDocTariffs Tariffs
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743964)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	[DispId(1610743965)]
	string TariffChainsXML
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
	bool CheckTariffChanges
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743967)]
		get;
	}

	[DispId(1610743968)]
	Errors Errors
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743968)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	[DispId(1610743971)]
	int ProcurementDays
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743971)]
		get;
	}

	[DispId(1610743972)]
	int ProductionDays
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743972)]
		get;
	}

	[DispId(1610743973)]
	int ShippingDays
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743973)]
		get;
	}

	[DispId(1610743980)]
	bool IsDefaultShippingAddress
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743980)]
		get;
	}

	[DispId(1610743981)]
	bool IsDefaultBillingAddress
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743981)]
		get;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743868)]
	void New([In] int NumerationId, [In] int UserCode);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743869)]
	void NewFromXML([In][MarshalAs(UnmanagedType.IUnknown)] object Connection, [In] int NumerationId, [In] int UserCode, [In][MarshalAs(UnmanagedType.BStr)] string XML);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743870)]
	void Load([In] int Number, [In] int Version);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743871)]
	void CancelarDocumento([In] int Number, [In] int Version, [In][MarshalAs(UnmanagedType.BStr)] string message);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743872)]
	void LoadFromXML([In][MarshalAs(UnmanagedType.BStr)] string XML);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743873)]
	void Reload();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743874)]
	void Reset();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743875)]
	void Save();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743876)]
	void Recalculate();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743877)]
	void AutomaticRecalculate();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743878)]
	void RecalculatePrices();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743879)]
	void RecalculatePerVolume();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743880)]
	void SetAsModified();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743881)]
	bool ChangeCustomer([In] int CustomerCode, [In][MarshalAs(UnmanagedType.BStr)] string EntityId);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743882)]
	void CalculateCommission();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743883)]
	[return: MarshalAs(UnmanagedType.BStr)]
	string GetAlias();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743884)]
	void UpdateGUI();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743885)]
	bool Validate();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743886)]
	[TypeLibFunc(TypeLibFuncFlags.FHidden)]
	bool FireBeforeCancelDocument([In][MarshalAs(UnmanagedType.BStr)] string Filter);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[TypeLibFunc(TypeLibFuncFlags.FHidden)]
	[DispId(1610743887)]
	void FireAfterCancelDocument([In][MarshalAs(UnmanagedType.BStr)] string Filter);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743888)]
	[TypeLibFunc(TypeLibFuncFlags.FHidden)]
	void FireRecalculatePerVolume([In][MarshalAs(UnmanagedType.Interface)] TariffExceptions TariffExceptions, [In][MarshalAs(UnmanagedType.Interface)] RawMaterialVolumeItems RawMaterial);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743889)]
	bool ExecuteCommandsWith([In][MarshalAs(UnmanagedType.Interface)] IXMLDOMDocument2 Command, [MarshalAs(UnmanagedType.Interface)] out IXMLDOMDocument2 Result, [In][MarshalAs(UnmanagedType.IUnknown)] object Connection, [MarshalAs(UnmanagedType.Interface)] out PrefMessages Messages);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743890)]
	bool Create();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743891)]
	void ReloadPricesAfterPDEDiscounts();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743892)]
	void ReloadRedefinedCurrencies();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743893)]
	void ConsolidatePrices();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743894)]
	void CalculateProductionData([In][MarshalAs(UnmanagedType.BStr)] string XMLProperties);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743897)]
	int AddOffer([In][MarshalAs(UnmanagedType.BStr)] string VersionName);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743899)]
	bool RemoveVersion();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743901)]
	bool RemoveNotActiveOffers();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743903)]
	bool RemoveDocument();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743905)]
	void SetAsActiveOffer();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743907)]
	void AcceptOffer();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743910)]
	void UndoAcceptOffer();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743912)]
	int CreateOrder([In][MarshalAs(UnmanagedType.BStr)] string Name);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743914)]
	void ConfirmOrder();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743917)]
	int PublishDocument();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743919)]
	void EnsureVariablesAreFilled();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743921)]
	void ChangeOwner();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743923)]
	void OwnerChanged([In] bool vbChanged);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743925)]
	void SetAsReadyForFactory();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743926)]
	void UndoReadyForFactory();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743951)]
	bool SaveInstancesSubModelXML([In][MarshalAs(UnmanagedType.BStr)] string newVal);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743957)]
	bool GetDeliveryDescription([In] int Position, [MarshalAs(UnmanagedType.BStr)] out string RowId, [MarshalAs(UnmanagedType.BStr)] out string DescriptionId, [MarshalAs(UnmanagedType.BStr)] out string Description);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743958)]
	void SetDeliveryDescription([In] int Position, [In][MarshalAs(UnmanagedType.BStr)] string RowId, [In][MarshalAs(UnmanagedType.BStr)] string DescriptionId, [In][MarshalAs(UnmanagedType.BStr)] string Description);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743959)]
	void ClearDeliveryDescriptions();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743969)]
	void ResetErrors();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743970)]
	int CalculateScheduling([In] DateTime date, [In][ComAliasName("Interop.PrefSales.TaskNames")] TaskNames lastTask, [In] bool book);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743974)]
	void CalculateCommercialDeliveryDate();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743975)]
	[return: MarshalAs(UnmanagedType.BStr)]
	string SelectAreaFromPostalCode();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743976)]
	DateTime GetNextAvailableDate([In] DateTime date, [In][ComAliasName("Interop.PrefSales.TaskNames")] TaskNames TaskId);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743977)]
	bool UpdateSchedulingTasks();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743978)]
	[return: ComAliasName("Interop.PrefSales.SalesDocDeliveryDateChecking")]
	SalesDocDeliveryDateChecking CheckDateStatus([In] DateTime newVal, [In][ComAliasName("Interop.PrefSales.TaskNames")] TaskNames task);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743979)]
	[return: MarshalAs(UnmanagedType.BStr)]
	string XMLSalesDocumentReportPDF([In][MarshalAs(UnmanagedType.BStr)] string LayoutKind, [In][MarshalAs(UnmanagedType.BStr)] string LayoutDocument, [In][MarshalAs(UnmanagedType.BStr)] string LayoutName, [In][MarshalAs(UnmanagedType.BStr)] string OutputDirectory, [In][MarshalAs(UnmanagedType.BStr)] string OutputFile, [In][MarshalAs(UnmanagedType.BStr)] string OptionsXML, [In] int UnitsMode, [In] int lLanguageId, [MarshalAs(UnmanagedType.BStr)] out string pMessage);
}
