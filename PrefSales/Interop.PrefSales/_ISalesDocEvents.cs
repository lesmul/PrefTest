using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Interop.MSXML2;
using Interop.PrefCAD;
using Interop.PrefPrices;

namespace Interop.PrefSales;

[ComImport]
[TypeLibType(TypeLibTypeFlags.FDispatchable)]
[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
[Guid("80BA8A23-572B-4C12-AD4B-0AE7D313D0D8")]
public interface _ISalesDocEvents
{
	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
	[DispId(1)]
	void IsModel([MarshalAs(UnmanagedType.Interface)] SalesDoc SalesDoc, [MarshalAs(UnmanagedType.BStr)] string Name, ref bool IsModel);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
	[DispId(24)]
	void IsMaterial([MarshalAs(UnmanagedType.Interface)] SalesDoc SalesDoc, [MarshalAs(UnmanagedType.BStr)] ref string Code, ref bool IsMaterial);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
	[DispId(25)]
	void IsScript([MarshalAs(UnmanagedType.Interface)] SalesDoc SalesDoc, [MarshalAs(UnmanagedType.BStr)] ref string Code, ref bool IsScript);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
	[DispId(2)]
	void BeforeSave([MarshalAs(UnmanagedType.Interface)] SalesDoc SalesDoc, ref bool Cancel);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
	[DispId(3)]
	void AfterSave([MarshalAs(UnmanagedType.Interface)] SalesDoc SalesDoc);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
	[DispId(4)]
	void AfterLoad([MarshalAs(UnmanagedType.Interface)] SalesDoc SalesDoc);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
	[DispId(5)]
	void BeforeConfirmOrder([MarshalAs(UnmanagedType.Interface)] SalesDoc SalesDoc, ref bool Cancel);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
	[DispId(6)]
	void AfterConfirmOrder([MarshalAs(UnmanagedType.Interface)] SalesDoc SalesDoc);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
	[DispId(7)]
	void BeforeRemoveDocument([MarshalAs(UnmanagedType.Interface)] SalesDoc SalesDoc, ref bool Cancel);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
	[DispId(27)]
	void AfterRemoveDocument([MarshalAs(UnmanagedType.Interface)] SalesDoc SalesDoc, [MarshalAs(UnmanagedType.BStr)] string Filter);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
	[DispId(8)]
	void BeforeRemoveOffer([MarshalAs(UnmanagedType.Interface)] SalesDoc SalesDoc, ref bool Cancel);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
	[DispId(9)]
	void BeforeRemoveOrder([MarshalAs(UnmanagedType.Interface)] SalesDoc SalesDoc, ref bool Cancel);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
	[DispId(10)]
	void BeforeRemoveDeliveryNote([MarshalAs(UnmanagedType.Interface)] SalesDoc SalesDoc, ref bool Cancel);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
	[DispId(11)]
	void BeforeRemoveInvoice([MarshalAs(UnmanagedType.Interface)] SalesDoc SalesDoc, ref bool Cancel);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
	[DispId(12)]
	void BeforeRemoveSubOrder([MarshalAs(UnmanagedType.Interface)] SalesDoc SalesDoc, ref bool Cancel);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
	[DispId(13)]
	void BeforeRemoveNotActiveOffers([MarshalAs(UnmanagedType.Interface)] SalesDoc SalesDoc, ref bool Cancel);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
	[DispId(14)]
	void RecalculatePerVolume([MarshalAs(UnmanagedType.Interface)] SalesDoc SalesDoc, [MarshalAs(UnmanagedType.Interface)] TariffExceptions TariffExceptions, [MarshalAs(UnmanagedType.Interface)] RawMaterialVolumeItems RawMaterial);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
	[DispId(15)]
	void AfterItemChanged([MarshalAs(UnmanagedType.Interface)] SalesDocItem SalesDocItem, [MarshalAs(UnmanagedType.BStr)] string FieldName);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
	[DispId(16)]
	void AfterItemInserted([MarshalAs(UnmanagedType.Interface)] SalesDocItem SalesDocItem);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
	[DispId(21)]
	void BeforeItemDeleted([MarshalAs(UnmanagedType.Interface)] SalesDocItem SalesDocItem, ref bool Cancel);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
	[DispId(17)]
	void AfterItemDeleted([MarshalAs(UnmanagedType.Interface)] SalesDoc SalesDoc, [MarshalAs(UnmanagedType.BStr)] string IdPos);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
	[DispId(18)]
	void BeforeSelectCustomer([MarshalAs(UnmanagedType.Interface)] SalesDoc SalesDoc, int lCustomerCode, ref bool vbCancel);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
	[DispId(19)]
	void AfterCalculateCommission([MarshalAs(UnmanagedType.Interface)] SalesDoc SalesDoc, ref bool Cancel);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
	[DispId(20)]
	void AfterFieldChanged([MarshalAs(UnmanagedType.Interface)] SalesDoc SalesDoc, [MarshalAs(UnmanagedType.Interface)] SalesDocField SalesDocField);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
	[DispId(22)]
	void FormatAlias([MarshalAs(UnmanagedType.Interface)] SalesDoc SalesDoc, [MarshalAs(UnmanagedType.BStr)] ref string Alias);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
	[DispId(23)]
	void ModifySalesmanSurcharge([MarshalAs(UnmanagedType.Interface)] SalesDoc SalesDoc, ref double dblSalesmanSurcharge);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
	[DispId(26)]
	void BlockDocument([MarshalAs(UnmanagedType.Interface)] SalesDoc SalesDoc);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
	[DispId(28)]
	void BeforeCancelDocument([MarshalAs(UnmanagedType.Interface)] SalesDoc SalesDoc, [MarshalAs(UnmanagedType.BStr)] string Filter, ref bool Cancel);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
	[DispId(29)]
	void AfterCancelDocument([MarshalAs(UnmanagedType.Interface)] SalesDoc SalesDoc, [MarshalAs(UnmanagedType.BStr)] string Filter);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
	[DispId(30)]
	void Validate([MarshalAs(UnmanagedType.Interface)] SalesDoc SalesDoc, ref bool Result);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
	[DispId(31)]
	void BeforeRecalculateItem([MarshalAs(UnmanagedType.Interface)] SalesDocItem SalesDocItem, ref bool Cancel);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
	[DispId(32)]
	void AfterRecalculateItem([MarshalAs(UnmanagedType.Interface)] SalesDocItem SalesDocItem);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
	[DispId(33)]
	void AfterSetItemCode([MarshalAs(UnmanagedType.Interface)] SalesDocItem SalesDocItem);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
	[DispId(34)]
	void BeforeSetItemContext([MarshalAs(UnmanagedType.Interface)] SalesDocItem SalesDocItem, [MarshalAs(UnmanagedType.Interface)] IXMLDOMDocument2 XMLContext);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
	[DispId(35)]
	void BeforeCreateSalesDoc([MarshalAs(UnmanagedType.Interface)] SalesDoc SalesDoc, ref bool Cancel, [MarshalAs(UnmanagedType.Interface)] IXMLDOMDocument2 XMLSalesDoc);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
	[DispId(36)]
	void AfterCreateSalesDoc([MarshalAs(UnmanagedType.Interface)] SalesDoc SalesDoc);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
	[DispId(37)]
	void BeforeSetItemXml([MarshalAs(UnmanagedType.Interface)] SalesDocItem SalesDocItem, [MarshalAs(UnmanagedType.Interface)] IXMLDOMDocument2 ItemXml);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
	[DispId(38)]
	void AfterSetItemXml([MarshalAs(UnmanagedType.Interface)] SalesDocItem SalesDocItem, [MarshalAs(UnmanagedType.Interface)] IXMLDOMDocument2 ItemXml);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
	[DispId(39)]
	void BeforeRecalculateItemPrices([MarshalAs(UnmanagedType.Interface)] SalesDocItem SalesDocItem, ref bool Cancel);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
	[DispId(40)]
	void AfterRecalculateItemPrices([MarshalAs(UnmanagedType.Interface)] SalesDocItem SalesDocItem);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
	[DispId(41)]
	void BeforeCalculateScheduling([MarshalAs(UnmanagedType.Interface)] SalesDoc SalesDoc, ref bool Cancel);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
	[DispId(42)]
	void AfterCalculateScheduling([MarshalAs(UnmanagedType.Interface)] SalesDoc SalesDoc);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
	[DispId(43)]
	void NewWizardSelectNumeration([MarshalAs(UnmanagedType.Interface)] SalesDoc SalesDoc, int lDestDocumentType, ref int lNumerationId);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
	[DispId(44)]
	void AfterNewVersion([MarshalAs(UnmanagedType.Interface)] SalesDoc srcSalesDoc, int lDestNumber, int lDestVersion);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
	[DispId(45)]
	void BeforeSetDataVersion([MarshalAs(UnmanagedType.Interface)] SalesDoc SalesDoc, [MarshalAs(UnmanagedType.BStr)] string MakerId, [MarshalAs(UnmanagedType.BStr)] string DataVerId, ref bool Cancel);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
	[DispId(46)]
	void SearchConsolidationRiskPonderation([MarshalAs(UnmanagedType.Interface)] SalesDoc SalesDoc, ref double dblRiskPonderation);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
	[DispId(47)]
	void ReplaceItemCode([MarshalAs(UnmanagedType.Interface)] SalesDocItem SalesDocItem, [ComAliasName("Interop.PrefSales.SalesDocItemType")] SalesDocItemType lItemType, [MarshalAs(UnmanagedType.BStr)] ref string Code);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
	[DispId(48)]
	void AfterReplaceItemCode([MarshalAs(UnmanagedType.Interface)] SalesDocItem SalesDocItem, [ComAliasName("Interop.PrefSales.SalesDocItemType")] SalesDocItemType lOldItemType, [MarshalAs(UnmanagedType.BStr)] string OOldCode, [ComAliasName("Interop.PrefSales.SalesDocItemType")] SalesDocItemType lNewItemType, [MarshalAs(UnmanagedType.BStr)] string NewCode);
}
