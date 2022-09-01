using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Interop.MSXML2;
using Interop.PrefComponents;
using Interop.PrefErrors;
using Interop.PrefPrices;

namespace Interop.PrefSales;

[ComImport]
[TypeLibType(TypeLibTypeFlags.FCanCreate)]
[ClassInterface(ClassInterfaceType.None)]
[Guid("1FA58764-3050-4F3E-B693-148D28607ED2")]
public class SalesDocItemClass : ISalesDocItem, SalesDocItem
{
	[DispId(1610743808)]
	public virtual extern SalesDoc SalesDoc
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743808)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	[DispId(1610743809)]
	public virtual extern SalesDocFields Fields
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743809)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	[DispId(1610743810)]
	public virtual extern SalesDocProperties UserProperties
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743810)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	[DispId(1610743811)]
	public virtual extern Errors Errors
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743811)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	[DispId(1610743812)]
	public virtual extern Errors ModelErrors
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743812)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	[DispId(1610743813)]
	public virtual extern SalesDocServiceCodes ServiceCodes
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743813)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	[DispId(1610743814)]
	public virtual extern SalesDocItemAddWorks AddWorks
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743814)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	[ComAliasName("Interop.PrefSales.SalesDocItemState")]
	[DispId(1610743815)]
	public virtual extern SalesDocItemState State
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743815)]
		[return: ComAliasName("Interop.PrefSales.SalesDocItemState")]
		get;
	}

	[ComAliasName("Interop.PrefSales.SalesDocItemType")]
	[DispId(1610743816)]
	public virtual extern SalesDocItemType Type
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743816)]
		[return: ComAliasName("Interop.PrefSales.SalesDocItemType")]
		get;
	}

	[DispId(1610743817)]
	public virtual extern bool Blocked
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
	public virtual extern bool BlockedModel
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
	public virtual extern string XMLProperties
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
	public virtual extern string XMLItem
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
	public virtual extern IXMLDOMDocument2 DescriptiveXMLDOM
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743825)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	[DispId(1610743826)]
	public virtual extern string PricesXML
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743826)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
	}

	[DispId(1610743827)]
	public virtual extern string DrawingVisualPropertiesXML
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
	public virtual extern IStream Thumbnail
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743829)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	[DispId(1610743830)]
	public virtual extern bool PriceUpdated
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743830)]
		get;
	}

	[DispId(1610743831)]
	public virtual extern bool PriceFrozenForFactory
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
	public virtual extern bool PriceClosed
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
	public virtual extern object ItemData1
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
	public virtual extern string DataVersion
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743837)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
	}

	[DispId(1610743838)]
	public virtual extern bool NeedRecalculate
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
	public virtual extern bool NeedRecalculateConstructiveData
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
	public virtual extern bool NeedRecalculatePrices
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
	public virtual extern string XMLPriceDocumentation
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743844)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
	}

	[DispId(1610743845)]
	public virtual extern SalesDocAlternativesContent AlternativesContent
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743845)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	[DispId(1610743846)]
	[ComAliasName("Interop.PrefSales.SalesDocItemExceptionType")]
	public virtual extern SalesDocItemExceptionType ResellerDiscountType
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743846)]
		[return: ComAliasName("Interop.PrefSales.SalesDocItemExceptionType")]
		get;
	}

	[DispId(1610743847)]
	[ComAliasName("Interop.PrefSales.SalesDocItemExceptionType")]
	public virtual extern SalesDocItemExceptionType ResellerSalesIncrementType
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743847)]
		[return: ComAliasName("Interop.PrefSales.SalesDocItemExceptionType")]
		get;
	}

	[DispId(1610743848)]
	public virtual extern bool IsDelivered
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743848)]
		get;
	}

	[DispId(1610743849)]
	public virtual extern bool IsInvoiced
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743849)]
		get;
	}

	[DispId(1610743850)]
	public virtual extern bool IsInPhases
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743850)]
		get;
	}

	[DispId(1610743851)]
	public virtual extern SalesDocPriceGroups PriceGroups
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743851)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	[DispId(1610743852)]
	public virtual extern string PrefItemId
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743852)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
	}

	[DispId(1610743853)]
	public virtual extern IXMLDOMDocument2 Filter
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
	public virtual extern string ExtraDataXML
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
	public virtual extern string DescriptiveXML
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743857)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
	}

	[DispId(1610743858)]
	public virtual extern double UnitPriceAfterDiscounts
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743858)]
		get;
	}

	[DispId(1610743859)]
	public virtual extern double PriceAfterDiscounts
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743859)]
		get;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern SalesDocItemClass();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743860)]
	public virtual extern void SetCode([In][MarshalAs(UnmanagedType.BStr)] string Code, [In] bool InheritProperties);

	void ISalesDocItem.SetCode([In][MarshalAs(UnmanagedType.BStr)] string Code, [In] bool InheritProperties)
	{
		//ILSpy generated this explicit interface implementation from .override directive in SetCode
		this.SetCode(Code, InheritProperties);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743861)]
	public virtual extern bool Properties([In] bool InheritProperties, [In] bool ReadOnly, [MarshalAs(UnmanagedType.Interface)] out PrefMessages Messages);

	bool ISalesDocItem.Properties([In] bool InheritProperties, [In] bool ReadOnly, [MarshalAs(UnmanagedType.Interface)] out PrefMessages Messages)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Properties
		return this.Properties(InheritProperties, ReadOnly, out Messages);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743862)]
	public virtual extern bool EditInPrefCAD([In] bool InheritProperties, [MarshalAs(UnmanagedType.Interface)] out PrefMessages Messages);

	bool ISalesDocItem.EditInPrefCAD([In] bool InheritProperties, [MarshalAs(UnmanagedType.Interface)] out PrefMessages Messages)
	{
		//ILSpy generated this explicit interface implementation from .override directive in EditInPrefCAD
		return this.EditInPrefCAD(InheritProperties, out Messages);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743863)]
	public virtual extern bool CanUpdateQuantity([In] double Quantity);

	bool ISalesDocItem.CanUpdateQuantity([In] double Quantity)
	{
		//ILSpy generated this explicit interface implementation from .override directive in CanUpdateQuantity
		return this.CanUpdateQuantity(Quantity);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743864)]
	public virtual extern void SetQuantity([In] double Quantity);

	void ISalesDocItem.SetQuantity([In] double Quantity)
	{
		//ILSpy generated this explicit interface implementation from .override directive in SetQuantity
		this.SetQuantity(Quantity);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743865)]
	public virtual extern void MirrorHorizontal();

	void ISalesDocItem.MirrorHorizontal()
	{
		//ILSpy generated this explicit interface implementation from .override directive in MirrorHorizontal
		this.MirrorHorizontal();
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743866)]
	public virtual extern void MirrorVertical();

	void ISalesDocItem.MirrorVertical()
	{
		//ILSpy generated this explicit interface implementation from .override directive in MirrorVertical
		this.MirrorVertical();
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743867)]
	public virtual extern void BOM();

	void ISalesDocItem.BOM()
	{
		//ILSpy generated this explicit interface implementation from .override directive in BOM
		this.BOM();
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743868)]
	public virtual extern void Recalculate([In][MarshalAs(UnmanagedType.Interface)] TariffExceptions pVal, [MarshalAs(UnmanagedType.Interface)] out PrefMessages Messages);

	void ISalesDocItem.Recalculate([In][MarshalAs(UnmanagedType.Interface)] TariffExceptions pVal, [MarshalAs(UnmanagedType.Interface)] out PrefMessages Messages)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Recalculate
		this.Recalculate(pVal, out Messages);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743869)]
	public virtual extern void Regenerate([MarshalAs(UnmanagedType.Interface)] out PrefMessages Messages);

	void ISalesDocItem.Regenerate([MarshalAs(UnmanagedType.Interface)] out PrefMessages Messages)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Regenerate
		this.Regenerate(out Messages);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743870)]
	public virtual extern void RecalculatePrices([In][MarshalAs(UnmanagedType.Interface)] TariffExceptions pVal, [MarshalAs(UnmanagedType.Interface)] out PrefMessages Messages);

	void ISalesDocItem.RecalculatePrices([In][MarshalAs(UnmanagedType.Interface)] TariffExceptions pVal, [MarshalAs(UnmanagedType.Interface)] out PrefMessages Messages)
	{
		//ILSpy generated this explicit interface implementation from .override directive in RecalculatePrices
		this.RecalculatePrices(pVal, out Messages);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743871)]
	public virtual extern bool Synchronize();

	bool ISalesDocItem.Synchronize()
	{
		//ILSpy generated this explicit interface implementation from .override directive in Synchronize
		return this.Synchronize();
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743872)]
	public virtual extern void SetManufacturerDiscount([In] double Discount);

	void ISalesDocItem.SetManufacturerDiscount([In] double Discount)
	{
		//ILSpy generated this explicit interface implementation from .override directive in SetManufacturerDiscount
		this.SetManufacturerDiscount(Discount);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743873)]
	public virtual extern void SetResellerPrice([In] double UnitPrice);

	void ISalesDocItem.SetResellerPrice([In] double UnitPrice)
	{
		//ILSpy generated this explicit interface implementation from .override directive in SetResellerPrice
		this.SetResellerPrice(UnitPrice);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743874)]
	public virtual extern void SetResellerIncrement([In] double Increment);

	void ISalesDocItem.SetResellerIncrement([In] double Increment)
	{
		//ILSpy generated this explicit interface implementation from .override directive in SetResellerIncrement
		this.SetResellerIncrement(Increment);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743875)]
	public virtual extern void SetResellerSalesPrice([In] double UnitPrice);

	void ISalesDocItem.SetResellerSalesPrice([In] double UnitPrice)
	{
		//ILSpy generated this explicit interface implementation from .override directive in SetResellerSalesPrice
		this.SetResellerSalesPrice(UnitPrice);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743876)]
	public virtual extern void ResetResellerPrice();

	void ISalesDocItem.ResetResellerPrice()
	{
		//ILSpy generated this explicit interface implementation from .override directive in ResetResellerPrice
		this.ResetResellerPrice();
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743877)]
	public virtual extern void SetUnitPrice([In] double UnitPrice);

	void ISalesDocItem.SetUnitPrice([In] double UnitPrice)
	{
		//ILSpy generated this explicit interface implementation from .override directive in SetUnitPrice
		this.SetUnitPrice(UnitPrice);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743878)]
	public virtual extern void SetUnitCost([In] double UnitCost);

	void ISalesDocItem.SetUnitCost([In] double UnitCost)
	{
		//ILSpy generated this explicit interface implementation from .override directive in SetUnitCost
		this.SetUnitCost(UnitCost);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743879)]
	public virtual extern void SetDiscount([In] double Rebate);

	void ISalesDocItem.SetDiscount([In] double Rebate)
	{
		//ILSpy generated this explicit interface implementation from .override directive in SetDiscount
		this.SetDiscount(Rebate);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743880)]
	public virtual extern void SetDimensions([In][MarshalAs(UnmanagedType.BStr)] string Dimensions);

	void ISalesDocItem.SetDimensions([In][MarshalAs(UnmanagedType.BStr)] string Dimensions)
	{
		//ILSpy generated this explicit interface implementation from .override directive in SetDimensions
		this.SetDimensions(Dimensions);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743881)]
	public virtual extern void ConvertToModel();

	void ISalesDocItem.ConvertToModel()
	{
		//ILSpy generated this explicit interface implementation from .override directive in ConvertToModel
		this.ConvertToModel();
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743882)]
	public virtual extern bool ExecuteCommand([In][MarshalAs(UnmanagedType.Interface)] IXMLDOMDocument2 Command, [MarshalAs(UnmanagedType.Interface)] out IXMLDOMDocument2 Result, [In] bool Regenerate);

	bool ISalesDocItem.ExecuteCommand([In][MarshalAs(UnmanagedType.Interface)] IXMLDOMDocument2 Command, [MarshalAs(UnmanagedType.Interface)] out IXMLDOMDocument2 Result, [In] bool Regenerate)
	{
		//ILSpy generated this explicit interface implementation from .override directive in ExecuteCommand
		return this.ExecuteCommand(Command, out Result, Regenerate);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743883)]
	public virtual extern bool ExecuteCommandStr([In][MarshalAs(UnmanagedType.BStr)] string Command, [MarshalAs(UnmanagedType.BStr)] out string Result, [In] bool Regenerate);

	bool ISalesDocItem.ExecuteCommandStr([In][MarshalAs(UnmanagedType.BStr)] string Command, [MarshalAs(UnmanagedType.BStr)] out string Result, [In] bool Regenerate)
	{
		//ILSpy generated this explicit interface implementation from .override directive in ExecuteCommandStr
		return this.ExecuteCommandStr(Command, out Result, Regenerate);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743884)]
	public virtual extern bool EditFilter([In] bool ReadOnly);

	bool ISalesDocItem.EditFilter([In] bool ReadOnly)
	{
		//ILSpy generated this explicit interface implementation from .override directive in EditFilter
		return this.EditFilter(ReadOnly);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743885)]
	public virtual extern bool SendCommand([In][MarshalAs(UnmanagedType.Interface)] IXMLDOMDocument2 Command);

	bool ISalesDocItem.SendCommand([In][MarshalAs(UnmanagedType.Interface)] IXMLDOMDocument2 Command)
	{
		//ILSpy generated this explicit interface implementation from .override directive in SendCommand
		return this.SendCommand(Command);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743886)]
	public virtual extern bool SendCommandStr([In][MarshalAs(UnmanagedType.BStr)] string Command);

	bool ISalesDocItem.SendCommandStr([In][MarshalAs(UnmanagedType.BStr)] string Command)
	{
		//ILSpy generated this explicit interface implementation from .override directive in SendCommandStr
		return this.SendCommandStr(Command);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743887)]
	public virtual extern bool SetPrefItemContext([In] bool InheritProperties, [In] bool ApplyOnProperties);

	bool ISalesDocItem.SetPrefItemContext([In] bool InheritProperties, [In] bool ApplyOnProperties)
	{
		//ILSpy generated this explicit interface implementation from .override directive in SetPrefItemContext
		return this.SetPrefItemContext(InheritProperties, ApplyOnProperties);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743888)]
	public virtual extern bool LinkToParentIdByNomenclature();

	bool ISalesDocItem.LinkToParentIdByNomenclature()
	{
		//ILSpy generated this explicit interface implementation from .override directive in LinkToParentIdByNomenclature
		return this.LinkToParentIdByNomenclature();
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743889)]
	public virtual extern void GetNegotiatedItem([MarshalAs(UnmanagedType.BStr)] ref string bstrId);

	void ISalesDocItem.GetNegotiatedItem([MarshalAs(UnmanagedType.BStr)] ref string bstrId)
	{
		//ILSpy generated this explicit interface implementation from .override directive in GetNegotiatedItem
		this.GetNegotiatedItem(ref bstrId);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743890)]
	public virtual extern void GetDeltaComparedItem([MarshalAs(UnmanagedType.BStr)] ref string bstrId);

	void ISalesDocItem.GetDeltaComparedItem([MarshalAs(UnmanagedType.BStr)] ref string bstrId)
	{
		//ILSpy generated this explicit interface implementation from .override directive in GetDeltaComparedItem
		this.GetDeltaComparedItem(ref bstrId);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743891)]
	[return: ComAliasName("Interop.PrefSales.SalesDocItemJitmvType")]
	public virtual extern SalesDocItemJitmvType GetJITMVType();

	SalesDocItemJitmvType ISalesDocItem.GetJITMVType()
	{
		//ILSpy generated this explicit interface implementation from .override directive in GetJITMVType
		return this.GetJITMVType();
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743892)]
	[return: MarshalAs(UnmanagedType.BStr)]
	public virtual extern string GetTranslatedField([In][MarshalAs(UnmanagedType.BStr)] string field, [In] int translationLanguageId);

	string ISalesDocItem.GetTranslatedField([In][MarshalAs(UnmanagedType.BStr)] string field, [In] int translationLanguageId)
	{
		//ILSpy generated this explicit interface implementation from .override directive in GetTranslatedField
		return this.GetTranslatedField(field, translationLanguageId);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743893)]
	public virtual extern void SetTranslatedField([In][MarshalAs(UnmanagedType.BStr)] string field, [In] int translationLanguageId, [In][MarshalAs(UnmanagedType.BStr)] string Value);

	void ISalesDocItem.SetTranslatedField([In][MarshalAs(UnmanagedType.BStr)] string field, [In] int translationLanguageId, [In][MarshalAs(UnmanagedType.BStr)] string Value)
	{
		//ILSpy generated this explicit interface implementation from .override directive in SetTranslatedField
		this.SetTranslatedField(field, translationLanguageId, Value);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743894)]
	public virtual extern bool StartPrefItemTransaction();

	bool ISalesDocItem.StartPrefItemTransaction()
	{
		//ILSpy generated this explicit interface implementation from .override directive in StartPrefItemTransaction
		return this.StartPrefItemTransaction();
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743895)]
	public virtual extern bool CommitPrefItemTransaction();

	bool ISalesDocItem.CommitPrefItemTransaction()
	{
		//ILSpy generated this explicit interface implementation from .override directive in CommitPrefItemTransaction
		return this.CommitPrefItemTransaction();
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743896)]
	public virtual extern bool RollbackPrefItemTransaction();

	bool ISalesDocItem.RollbackPrefItemTransaction()
	{
		//ILSpy generated this explicit interface implementation from .override directive in RollbackPrefItemTransaction
		return this.RollbackPrefItemTransaction();
	}
}
