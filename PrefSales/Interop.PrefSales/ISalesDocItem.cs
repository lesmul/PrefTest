using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Interop.MSXML2;
using Interop.PrefComponents;
using Interop.PrefErrors;
using Interop.PrefPrices;

namespace Interop.PrefSales;

[ComImport]
[Guid("D2177AF4-B00E-47F4-A336-B950E36BCB9D")]
[TypeLibType(TypeLibTypeFlags.FDual | TypeLibTypeFlags.FNonExtensible | TypeLibTypeFlags.FDispatchable)]
public interface ISalesDocItem
{
	[DispId(1610743808)]
	SalesDoc SalesDoc
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743808)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	[DispId(1610743809)]
	SalesDocFields Fields
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743809)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	[DispId(1610743810)]
	SalesDocProperties UserProperties
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743810)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	[DispId(1610743811)]
	Errors Errors
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743811)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	[DispId(1610743812)]
	Errors ModelErrors
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743812)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	[DispId(1610743813)]
	SalesDocServiceCodes ServiceCodes
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743813)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	[DispId(1610743814)]
	SalesDocItemAddWorks AddWorks
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743814)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	[ComAliasName("Interop.PrefSales.SalesDocItemState")]
	[DispId(1610743815)]
	SalesDocItemState State
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743815)]
		[return: ComAliasName("Interop.PrefSales.SalesDocItemState")]
		get;
	}

	[ComAliasName("Interop.PrefSales.SalesDocItemType")]
	[DispId(1610743816)]
	SalesDocItemType Type
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743816)]
		[return: ComAliasName("Interop.PrefSales.SalesDocItemType")]
		get;
	}

	[DispId(1610743817)]
	bool Blocked
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743817)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743817)]
		[param: In]
		set;
	}

	[DispId(1610743819)]
	bool BlockedModel
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743819)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743819)]
		[param: In]
		set;
	}

	[DispId(1610743821)]
	string XMLProperties
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743821)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743821)]
		[param: In]
		[param: MarshalAs(UnmanagedType.BStr)]
		set;
	}

	[DispId(1610743823)]
	string XMLItem
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743823)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743823)]
		[param: In]
		[param: MarshalAs(UnmanagedType.BStr)]
		set;
	}

	[DispId(1610743825)]
	IXMLDOMDocument2 DescriptiveXMLDOM
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743825)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	[DispId(1610743826)]
	string PricesXML
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743826)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
	}

	[DispId(1610743827)]
	string DrawingVisualPropertiesXML
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743827)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743827)]
		[param: In]
		[param: MarshalAs(UnmanagedType.BStr)]
		set;
	}

	[DispId(1610743829)]
	IStream Thumbnail
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743829)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	[DispId(1610743830)]
	bool PriceUpdated
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743830)]
		get;
	}

	[DispId(1610743831)]
	bool PriceFrozenForFactory
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
	bool PriceClosed
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
	object ItemData1
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743835)]
		[return: MarshalAs(UnmanagedType.Struct)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743835)]
		[param: In]
		[param: MarshalAs(UnmanagedType.Struct)]
		set;
	}

	[DispId(1610743837)]
	string DataVersion
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743837)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
	}

	[DispId(1610743838)]
	bool NeedRecalculate
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743838)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743838)]
		[param: In]
		set;
	}

	[DispId(1610743840)]
	bool NeedRecalculateConstructiveData
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743840)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743840)]
		[param: In]
		set;
	}

	[DispId(1610743842)]
	bool NeedRecalculatePrices
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743842)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743842)]
		[param: In]
		set;
	}

	[DispId(1610743844)]
	string XMLPriceDocumentation
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743844)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
	}

	[DispId(1610743845)]
	SalesDocAlternativesContent AlternativesContent
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743845)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	[ComAliasName("Interop.PrefSales.SalesDocItemExceptionType")]
	[DispId(1610743846)]
	SalesDocItemExceptionType ResellerDiscountType
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743846)]
		[return: ComAliasName("Interop.PrefSales.SalesDocItemExceptionType")]
		get;
	}

	[ComAliasName("Interop.PrefSales.SalesDocItemExceptionType")]
	[DispId(1610743847)]
	SalesDocItemExceptionType ResellerSalesIncrementType
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743847)]
		[return: ComAliasName("Interop.PrefSales.SalesDocItemExceptionType")]
		get;
	}

	[DispId(1610743848)]
	bool IsDelivered
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743848)]
		get;
	}

	[DispId(1610743849)]
	bool IsInvoiced
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743849)]
		get;
	}

	[DispId(1610743850)]
	bool IsInPhases
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743850)]
		get;
	}

	[DispId(1610743851)]
	SalesDocPriceGroups PriceGroups
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743851)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	[DispId(1610743852)]
	string PrefItemId
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743852)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
	}

	[DispId(1610743853)]
	IXMLDOMDocument2 Filter
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743853)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743853)]
		[param: In]
		[param: MarshalAs(UnmanagedType.Interface)]
		set;
	}

	[DispId(1610743855)]
	string ExtraDataXML
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743855)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743855)]
		[param: In]
		[param: MarshalAs(UnmanagedType.BStr)]
		set;
	}

	[DispId(1610743857)]
	string DescriptiveXML
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743857)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
	}

	[DispId(1610743858)]
	double UnitPriceAfterDiscounts
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743858)]
		get;
	}

	[DispId(1610743859)]
	double PriceAfterDiscounts
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743859)]
		get;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743860)]
	void SetCode([In][MarshalAs(UnmanagedType.BStr)] string Code, [In] bool InheritProperties);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743861)]
	bool Properties([In] bool InheritProperties, [In] bool ReadOnly, [MarshalAs(UnmanagedType.Interface)] out PrefMessages Messages);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743862)]
	bool EditInPrefCAD([In] bool InheritProperties, [MarshalAs(UnmanagedType.Interface)] out PrefMessages Messages);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743863)]
	bool CanUpdateQuantity([In] double Quantity);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743864)]
	void SetQuantity([In] double Quantity);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743865)]
	void MirrorHorizontal();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743866)]
	void MirrorVertical();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743867)]
	void BOM();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743868)]
	void Recalculate([In][MarshalAs(UnmanagedType.Interface)] TariffExceptions pVal, [MarshalAs(UnmanagedType.Interface)] out PrefMessages Messages);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743869)]
	void Regenerate([MarshalAs(UnmanagedType.Interface)] out PrefMessages Messages);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743870)]
	void RecalculatePrices([In][MarshalAs(UnmanagedType.Interface)] TariffExceptions pVal, [MarshalAs(UnmanagedType.Interface)] out PrefMessages Messages);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743871)]
	bool Synchronize();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743872)]
	void SetManufacturerDiscount([In] double Discount);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743873)]
	void SetResellerPrice([In] double UnitPrice);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743874)]
	void SetResellerIncrement([In] double Increment);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743875)]
	void SetResellerSalesPrice([In] double UnitPrice);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743876)]
	void ResetResellerPrice();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743877)]
	void SetUnitPrice([In] double UnitPrice);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743878)]
	void SetUnitCost([In] double UnitCost);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743879)]
	void SetDiscount([In] double Rebate);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743880)]
	void SetDimensions([In][MarshalAs(UnmanagedType.BStr)] string Dimensions);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743881)]
	void ConvertToModel();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743882)]
	bool ExecuteCommand([In][MarshalAs(UnmanagedType.Interface)] IXMLDOMDocument2 Command, [MarshalAs(UnmanagedType.Interface)] out IXMLDOMDocument2 Result, [In] bool Regenerate);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743883)]
	bool ExecuteCommandStr([In][MarshalAs(UnmanagedType.BStr)] string Command, [MarshalAs(UnmanagedType.BStr)] out string Result, [In] bool Regenerate);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743884)]
	bool EditFilter([In] bool ReadOnly);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743885)]
	bool SendCommand([In][MarshalAs(UnmanagedType.Interface)] IXMLDOMDocument2 Command);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743886)]
	bool SendCommandStr([In][MarshalAs(UnmanagedType.BStr)] string Command);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743887)]
	bool SetPrefItemContext([In] bool InheritProperties, [In] bool ApplyOnProperties);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743888)]
	bool LinkToParentIdByNomenclature();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743889)]
	void GetNegotiatedItem([MarshalAs(UnmanagedType.BStr)] ref string bstrId);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743890)]
	void GetDeltaComparedItem([MarshalAs(UnmanagedType.BStr)] ref string bstrId);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743891)]
	[return: ComAliasName("Interop.PrefSales.SalesDocItemJitmvType")]
	SalesDocItemJitmvType GetJITMVType();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743892)]
	[return: MarshalAs(UnmanagedType.BStr)]
	string GetTranslatedField([In][MarshalAs(UnmanagedType.BStr)] string field, [In] int translationLanguageId);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743893)]
	void SetTranslatedField([In][MarshalAs(UnmanagedType.BStr)] string field, [In] int translationLanguageId, [In][MarshalAs(UnmanagedType.BStr)] string Value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743894)]
	bool StartPrefItemTransaction();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743895)]
	bool CommitPrefItemTransaction();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743896)]
	bool RollbackPrefItemTransaction();
}
