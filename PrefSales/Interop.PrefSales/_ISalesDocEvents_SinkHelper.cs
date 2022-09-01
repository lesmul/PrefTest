using System.Runtime.InteropServices;
using Interop.MSXML2;
using Interop.PrefCAD;
using Interop.PrefPrices;

namespace Interop.PrefSales;

[TypeLibType(TypeLibTypeFlags.FHidden)]
[ClassInterface(ClassInterfaceType.None)]
public sealed class _ISalesDocEvents_SinkHelper : _ISalesDocEvents
{
	public _ISalesDocEvents_IsModelEventHandler m_IsModelDelegate;

	public _ISalesDocEvents_IsMaterialEventHandler m_IsMaterialDelegate;

	public _ISalesDocEvents_IsScriptEventHandler m_IsScriptDelegate;

	public _ISalesDocEvents_BeforeSaveEventHandler m_BeforeSaveDelegate;

	public _ISalesDocEvents_AfterSaveEventHandler m_AfterSaveDelegate;

	public _ISalesDocEvents_AfterLoadEventHandler m_AfterLoadDelegate;

	public _ISalesDocEvents_BeforeConfirmOrderEventHandler m_BeforeConfirmOrderDelegate;

	public _ISalesDocEvents_AfterConfirmOrderEventHandler m_AfterConfirmOrderDelegate;

	public _ISalesDocEvents_BeforeRemoveDocumentEventHandler m_BeforeRemoveDocumentDelegate;

	public _ISalesDocEvents_AfterRemoveDocumentEventHandler m_AfterRemoveDocumentDelegate;

	public _ISalesDocEvents_BeforeRemoveOfferEventHandler m_BeforeRemoveOfferDelegate;

	public _ISalesDocEvents_BeforeRemoveOrderEventHandler m_BeforeRemoveOrderDelegate;

	public _ISalesDocEvents_BeforeRemoveDeliveryNoteEventHandler m_BeforeRemoveDeliveryNoteDelegate;

	public _ISalesDocEvents_BeforeRemoveInvoiceEventHandler m_BeforeRemoveInvoiceDelegate;

	public _ISalesDocEvents_BeforeRemoveSubOrderEventHandler m_BeforeRemoveSubOrderDelegate;

	public _ISalesDocEvents_BeforeRemoveNotActiveOffersEventHandler m_BeforeRemoveNotActiveOffersDelegate;

	public _ISalesDocEvents_RecalculatePerVolumeEventHandler m_RecalculatePerVolumeDelegate;

	public _ISalesDocEvents_AfterItemChangedEventHandler m_AfterItemChangedDelegate;

	public _ISalesDocEvents_AfterItemInsertedEventHandler m_AfterItemInsertedDelegate;

	public _ISalesDocEvents_BeforeItemDeletedEventHandler m_BeforeItemDeletedDelegate;

	public _ISalesDocEvents_AfterItemDeletedEventHandler m_AfterItemDeletedDelegate;

	public _ISalesDocEvents_BeforeSelectCustomerEventHandler m_BeforeSelectCustomerDelegate;

	public _ISalesDocEvents_AfterCalculateCommissionEventHandler m_AfterCalculateCommissionDelegate;

	public _ISalesDocEvents_AfterFieldChangedEventHandler m_AfterFieldChangedDelegate;

	public _ISalesDocEvents_FormatAliasEventHandler m_FormatAliasDelegate;

	public _ISalesDocEvents_ModifySalesmanSurchargeEventHandler m_ModifySalesmanSurchargeDelegate;

	public _ISalesDocEvents_BlockDocumentEventHandler m_BlockDocumentDelegate;

	public _ISalesDocEvents_BeforeCancelDocumentEventHandler m_BeforeCancelDocumentDelegate;

	public _ISalesDocEvents_AfterCancelDocumentEventHandler m_AfterCancelDocumentDelegate;

	public _ISalesDocEvents_ValidateEventHandler m_ValidateDelegate;

	public _ISalesDocEvents_BeforeRecalculateItemEventHandler m_BeforeRecalculateItemDelegate;

	public _ISalesDocEvents_AfterRecalculateItemEventHandler m_AfterRecalculateItemDelegate;

	public _ISalesDocEvents_AfterSetItemCodeEventHandler m_AfterSetItemCodeDelegate;

	public _ISalesDocEvents_BeforeSetItemContextEventHandler m_BeforeSetItemContextDelegate;

	public _ISalesDocEvents_BeforeCreateSalesDocEventHandler m_BeforeCreateSalesDocDelegate;

	public _ISalesDocEvents_AfterCreateSalesDocEventHandler m_AfterCreateSalesDocDelegate;

	public _ISalesDocEvents_BeforeSetItemXmlEventHandler m_BeforeSetItemXmlDelegate;

	public _ISalesDocEvents_AfterSetItemXmlEventHandler m_AfterSetItemXmlDelegate;

	public _ISalesDocEvents_BeforeRecalculateItemPricesEventHandler m_BeforeRecalculateItemPricesDelegate;

	public _ISalesDocEvents_AfterRecalculateItemPricesEventHandler m_AfterRecalculateItemPricesDelegate;

	public _ISalesDocEvents_BeforeCalculateSchedulingEventHandler m_BeforeCalculateSchedulingDelegate;

	public _ISalesDocEvents_AfterCalculateSchedulingEventHandler m_AfterCalculateSchedulingDelegate;

	public _ISalesDocEvents_NewWizardSelectNumerationEventHandler m_NewWizardSelectNumerationDelegate;

	public _ISalesDocEvents_AfterNewVersionEventHandler m_AfterNewVersionDelegate;

	public _ISalesDocEvents_BeforeSetDataVersionEventHandler m_BeforeSetDataVersionDelegate;

	public _ISalesDocEvents_SearchConsolidationRiskPonderationEventHandler m_SearchConsolidationRiskPonderationDelegate;

	public _ISalesDocEvents_ReplaceItemCodeEventHandler m_ReplaceItemCodeDelegate;

	public _ISalesDocEvents_AfterReplaceItemCodeEventHandler m_AfterReplaceItemCodeDelegate;

	public int m_dwCookie;

	public override void IsModel(SalesDoc P_0, string P_1, ref bool P_2)
	{
		//Error decoding local variables: Signature type sequence must have at least one element.
		if (m_IsModelDelegate != null)
		{
			m_IsModelDelegate(P_0, P_1, ref P_2);
		}
	}

	public override void IsMaterial(SalesDoc P_0, ref string P_1, ref bool P_2)
	{
		//Error decoding local variables: Signature type sequence must have at least one element.
		if (m_IsMaterialDelegate != null)
		{
			m_IsMaterialDelegate(P_0, ref P_1, ref P_2);
		}
	}

	public override void IsScript(SalesDoc P_0, ref string P_1, ref bool P_2)
	{
		//Error decoding local variables: Signature type sequence must have at least one element.
		if (m_IsScriptDelegate != null)
		{
			m_IsScriptDelegate(P_0, ref P_1, ref P_2);
		}
	}

	public override void BeforeSave(SalesDoc P_0, ref bool P_1)
	{
		//Error decoding local variables: Signature type sequence must have at least one element.
		if (m_BeforeSaveDelegate != null)
		{
			m_BeforeSaveDelegate(P_0, ref P_1);
		}
	}

	public override void AfterSave(SalesDoc P_0)
	{
		//Error decoding local variables: Signature type sequence must have at least one element.
		if (m_AfterSaveDelegate != null)
		{
			m_AfterSaveDelegate(P_0);
		}
	}

	public override void AfterLoad(SalesDoc P_0)
	{
		//Error decoding local variables: Signature type sequence must have at least one element.
		if (m_AfterLoadDelegate != null)
		{
			m_AfterLoadDelegate(P_0);
		}
	}

	public override void BeforeConfirmOrder(SalesDoc P_0, ref bool P_1)
	{
		//Error decoding local variables: Signature type sequence must have at least one element.
		if (m_BeforeConfirmOrderDelegate != null)
		{
			m_BeforeConfirmOrderDelegate(P_0, ref P_1);
		}
	}

	public override void AfterConfirmOrder(SalesDoc P_0)
	{
		//Error decoding local variables: Signature type sequence must have at least one element.
		if (m_AfterConfirmOrderDelegate != null)
		{
			m_AfterConfirmOrderDelegate(P_0);
		}
	}

	public override void BeforeRemoveDocument(SalesDoc P_0, ref bool P_1)
	{
		//Error decoding local variables: Signature type sequence must have at least one element.
		if (m_BeforeRemoveDocumentDelegate != null)
		{
			m_BeforeRemoveDocumentDelegate(P_0, ref P_1);
		}
	}

	public override void AfterRemoveDocument(SalesDoc P_0, string P_1)
	{
		//Error decoding local variables: Signature type sequence must have at least one element.
		if (m_AfterRemoveDocumentDelegate != null)
		{
			m_AfterRemoveDocumentDelegate(P_0, P_1);
		}
	}

	public override void BeforeRemoveOffer(SalesDoc P_0, ref bool P_1)
	{
		//Error decoding local variables: Signature type sequence must have at least one element.
		if (m_BeforeRemoveOfferDelegate != null)
		{
			m_BeforeRemoveOfferDelegate(P_0, ref P_1);
		}
	}

	public override void BeforeRemoveOrder(SalesDoc P_0, ref bool P_1)
	{
		//Error decoding local variables: Signature type sequence must have at least one element.
		if (m_BeforeRemoveOrderDelegate != null)
		{
			m_BeforeRemoveOrderDelegate(P_0, ref P_1);
		}
	}

	public override void BeforeRemoveDeliveryNote(SalesDoc P_0, ref bool P_1)
	{
		//Error decoding local variables: Signature type sequence must have at least one element.
		if (m_BeforeRemoveDeliveryNoteDelegate != null)
		{
			m_BeforeRemoveDeliveryNoteDelegate(P_0, ref P_1);
		}
	}

	public override void BeforeRemoveInvoice(SalesDoc P_0, ref bool P_1)
	{
		//Error decoding local variables: Signature type sequence must have at least one element.
		if (m_BeforeRemoveInvoiceDelegate != null)
		{
			m_BeforeRemoveInvoiceDelegate(P_0, ref P_1);
		}
	}

	public override void BeforeRemoveSubOrder(SalesDoc P_0, ref bool P_1)
	{
		//Error decoding local variables: Signature type sequence must have at least one element.
		if (m_BeforeRemoveSubOrderDelegate != null)
		{
			m_BeforeRemoveSubOrderDelegate(P_0, ref P_1);
		}
	}

	public override void BeforeRemoveNotActiveOffers(SalesDoc P_0, ref bool P_1)
	{
		//Error decoding local variables: Signature type sequence must have at least one element.
		if (m_BeforeRemoveNotActiveOffersDelegate != null)
		{
			m_BeforeRemoveNotActiveOffersDelegate(P_0, ref P_1);
		}
	}

	public override void RecalculatePerVolume(SalesDoc P_0, TariffExceptions P_1, RawMaterialVolumeItems P_2)
	{
		//Error decoding local variables: Signature type sequence must have at least one element.
		if (m_RecalculatePerVolumeDelegate != null)
		{
			m_RecalculatePerVolumeDelegate(P_0, P_1, P_2);
		}
	}

	public override void AfterItemChanged(SalesDocItem P_0, string P_1)
	{
		//Error decoding local variables: Signature type sequence must have at least one element.
		if (m_AfterItemChangedDelegate != null)
		{
			m_AfterItemChangedDelegate(P_0, P_1);
		}
	}

	public override void AfterItemInserted(SalesDocItem P_0)
	{
		//Error decoding local variables: Signature type sequence must have at least one element.
		if (m_AfterItemInsertedDelegate != null)
		{
			m_AfterItemInsertedDelegate(P_0);
		}
	}

	public override void BeforeItemDeleted(SalesDocItem P_0, ref bool P_1)
	{
		//Error decoding local variables: Signature type sequence must have at least one element.
		if (m_BeforeItemDeletedDelegate != null)
		{
			m_BeforeItemDeletedDelegate(P_0, ref P_1);
		}
	}

	public override void AfterItemDeleted(SalesDoc P_0, string P_1)
	{
		//Error decoding local variables: Signature type sequence must have at least one element.
		if (m_AfterItemDeletedDelegate != null)
		{
			m_AfterItemDeletedDelegate(P_0, P_1);
		}
	}

	public override void BeforeSelectCustomer(SalesDoc P_0, int P_1, ref bool P_2)
	{
		//Error decoding local variables: Signature type sequence must have at least one element.
		if (m_BeforeSelectCustomerDelegate != null)
		{
			m_BeforeSelectCustomerDelegate(P_0, P_1, ref P_2);
		}
	}

	public override void AfterCalculateCommission(SalesDoc P_0, ref bool P_1)
	{
		//Error decoding local variables: Signature type sequence must have at least one element.
		if (m_AfterCalculateCommissionDelegate != null)
		{
			m_AfterCalculateCommissionDelegate(P_0, ref P_1);
		}
	}

	public override void AfterFieldChanged(SalesDoc P_0, SalesDocField P_1)
	{
		//Error decoding local variables: Signature type sequence must have at least one element.
		if (m_AfterFieldChangedDelegate != null)
		{
			m_AfterFieldChangedDelegate(P_0, P_1);
		}
	}

	public override void FormatAlias(SalesDoc P_0, ref string P_1)
	{
		//Error decoding local variables: Signature type sequence must have at least one element.
		if (m_FormatAliasDelegate != null)
		{
			m_FormatAliasDelegate(P_0, ref P_1);
		}
	}

	public override void ModifySalesmanSurcharge(SalesDoc P_0, ref double P_1)
	{
		//Error decoding local variables: Signature type sequence must have at least one element.
		if (m_ModifySalesmanSurchargeDelegate != null)
		{
			m_ModifySalesmanSurchargeDelegate(P_0, ref P_1);
		}
	}

	public override void BlockDocument(SalesDoc P_0)
	{
		//Error decoding local variables: Signature type sequence must have at least one element.
		if (m_BlockDocumentDelegate != null)
		{
			m_BlockDocumentDelegate(P_0);
		}
	}

	public override void BeforeCancelDocument(SalesDoc P_0, string P_1, ref bool P_2)
	{
		//Error decoding local variables: Signature type sequence must have at least one element.
		if (m_BeforeCancelDocumentDelegate != null)
		{
			m_BeforeCancelDocumentDelegate(P_0, P_1, ref P_2);
		}
	}

	public override void AfterCancelDocument(SalesDoc P_0, string P_1)
	{
		//Error decoding local variables: Signature type sequence must have at least one element.
		if (m_AfterCancelDocumentDelegate != null)
		{
			m_AfterCancelDocumentDelegate(P_0, P_1);
		}
	}

	public override void Validate(SalesDoc P_0, ref bool P_1)
	{
		//Error decoding local variables: Signature type sequence must have at least one element.
		if (m_ValidateDelegate != null)
		{
			m_ValidateDelegate(P_0, ref P_1);
		}
	}

	public override void BeforeRecalculateItem(SalesDocItem P_0, ref bool P_1)
	{
		//Error decoding local variables: Signature type sequence must have at least one element.
		if (m_BeforeRecalculateItemDelegate != null)
		{
			m_BeforeRecalculateItemDelegate(P_0, ref P_1);
		}
	}

	public override void AfterRecalculateItem(SalesDocItem P_0)
	{
		//Error decoding local variables: Signature type sequence must have at least one element.
		if (m_AfterRecalculateItemDelegate != null)
		{
			m_AfterRecalculateItemDelegate(P_0);
		}
	}

	public override void AfterSetItemCode(SalesDocItem P_0)
	{
		//Error decoding local variables: Signature type sequence must have at least one element.
		if (m_AfterSetItemCodeDelegate != null)
		{
			m_AfterSetItemCodeDelegate(P_0);
		}
	}

	public override void BeforeSetItemContext(SalesDocItem P_0, IXMLDOMDocument2 P_1)
	{
		//Error decoding local variables: Signature type sequence must have at least one element.
		if (m_BeforeSetItemContextDelegate != null)
		{
			m_BeforeSetItemContextDelegate(P_0, P_1);
		}
	}

	public override void BeforeCreateSalesDoc(SalesDoc P_0, ref bool P_1, IXMLDOMDocument2 P_2)
	{
		//Error decoding local variables: Signature type sequence must have at least one element.
		if (m_BeforeCreateSalesDocDelegate != null)
		{
			m_BeforeCreateSalesDocDelegate(P_0, ref P_1, P_2);
		}
	}

	public override void AfterCreateSalesDoc(SalesDoc P_0)
	{
		//Error decoding local variables: Signature type sequence must have at least one element.
		if (m_AfterCreateSalesDocDelegate != null)
		{
			m_AfterCreateSalesDocDelegate(P_0);
		}
	}

	public override void BeforeSetItemXml(SalesDocItem P_0, IXMLDOMDocument2 P_1)
	{
		//Error decoding local variables: Signature type sequence must have at least one element.
		if (m_BeforeSetItemXmlDelegate != null)
		{
			m_BeforeSetItemXmlDelegate(P_0, P_1);
		}
	}

	public override void AfterSetItemXml(SalesDocItem P_0, IXMLDOMDocument2 P_1)
	{
		//Error decoding local variables: Signature type sequence must have at least one element.
		if (m_AfterSetItemXmlDelegate != null)
		{
			m_AfterSetItemXmlDelegate(P_0, P_1);
		}
	}

	public override void BeforeRecalculateItemPrices(SalesDocItem P_0, ref bool P_1)
	{
		//Error decoding local variables: Signature type sequence must have at least one element.
		if (m_BeforeRecalculateItemPricesDelegate != null)
		{
			m_BeforeRecalculateItemPricesDelegate(P_0, ref P_1);
		}
	}

	public override void AfterRecalculateItemPrices(SalesDocItem P_0)
	{
		//Error decoding local variables: Signature type sequence must have at least one element.
		if (m_AfterRecalculateItemPricesDelegate != null)
		{
			m_AfterRecalculateItemPricesDelegate(P_0);
		}
	}

	public override void BeforeCalculateScheduling(SalesDoc P_0, ref bool P_1)
	{
		//Error decoding local variables: Signature type sequence must have at least one element.
		if (m_BeforeCalculateSchedulingDelegate != null)
		{
			m_BeforeCalculateSchedulingDelegate(P_0, ref P_1);
		}
	}

	public override void AfterCalculateScheduling(SalesDoc P_0)
	{
		//Error decoding local variables: Signature type sequence must have at least one element.
		if (m_AfterCalculateSchedulingDelegate != null)
		{
			m_AfterCalculateSchedulingDelegate(P_0);
		}
	}

	public override void NewWizardSelectNumeration(SalesDoc P_0, int P_1, ref int P_2)
	{
		//Error decoding local variables: Signature type sequence must have at least one element.
		if (m_NewWizardSelectNumerationDelegate != null)
		{
			m_NewWizardSelectNumerationDelegate(P_0, P_1, ref P_2);
		}
	}

	public override void AfterNewVersion(SalesDoc P_0, int P_1, int P_2)
	{
		//Error decoding local variables: Signature type sequence must have at least one element.
		if (m_AfterNewVersionDelegate != null)
		{
			m_AfterNewVersionDelegate(P_0, P_1, P_2);
		}
	}

	public override void BeforeSetDataVersion(SalesDoc P_0, string P_1, string P_2, ref bool P_3)
	{
		//Error decoding local variables: Signature type sequence must have at least one element.
		if (m_BeforeSetDataVersionDelegate != null)
		{
			m_BeforeSetDataVersionDelegate(P_0, P_1, P_2, ref P_3);
		}
	}

	public override void SearchConsolidationRiskPonderation(SalesDoc P_0, ref double P_1)
	{
		//Error decoding local variables: Signature type sequence must have at least one element.
		if (m_SearchConsolidationRiskPonderationDelegate != null)
		{
			m_SearchConsolidationRiskPonderationDelegate(P_0, ref P_1);
		}
	}

	public override void ReplaceItemCode(SalesDocItem P_0, SalesDocItemType P_1, ref string P_2)
	{
		//Error decoding local variables: Signature type sequence must have at least one element.
		if (m_ReplaceItemCodeDelegate != null)
		{
			m_ReplaceItemCodeDelegate(P_0, P_1, ref P_2);
		}
	}

	public override void AfterReplaceItemCode(SalesDocItem P_0, SalesDocItemType P_1, string P_2, SalesDocItemType P_3, string P_4)
	{
		//Error decoding local variables: Signature type sequence must have at least one element.
		if (m_AfterReplaceItemCodeDelegate != null)
		{
			m_AfterReplaceItemCodeDelegate(P_0, P_1, P_2, P_3, P_4);
		}
	}

	internal _ISalesDocEvents_SinkHelper()
	{
		//Error decoding local variables: Signature type sequence must have at least one element.
		m_dwCookie = 0;
		m_IsModelDelegate = null;
		m_IsMaterialDelegate = null;
		m_IsScriptDelegate = null;
		m_BeforeSaveDelegate = null;
		m_AfterSaveDelegate = null;
		m_AfterLoadDelegate = null;
		m_BeforeConfirmOrderDelegate = null;
		m_AfterConfirmOrderDelegate = null;
		m_BeforeRemoveDocumentDelegate = null;
		m_AfterRemoveDocumentDelegate = null;
		m_BeforeRemoveOfferDelegate = null;
		m_BeforeRemoveOrderDelegate = null;
		m_BeforeRemoveDeliveryNoteDelegate = null;
		m_BeforeRemoveInvoiceDelegate = null;
		m_BeforeRemoveSubOrderDelegate = null;
		m_BeforeRemoveNotActiveOffersDelegate = null;
		m_RecalculatePerVolumeDelegate = null;
		m_AfterItemChangedDelegate = null;
		m_AfterItemInsertedDelegate = null;
		m_BeforeItemDeletedDelegate = null;
		m_AfterItemDeletedDelegate = null;
		m_BeforeSelectCustomerDelegate = null;
		m_AfterCalculateCommissionDelegate = null;
		m_AfterFieldChangedDelegate = null;
		m_FormatAliasDelegate = null;
		m_ModifySalesmanSurchargeDelegate = null;
		m_BlockDocumentDelegate = null;
		m_BeforeCancelDocumentDelegate = null;
		m_AfterCancelDocumentDelegate = null;
		m_ValidateDelegate = null;
		m_BeforeRecalculateItemDelegate = null;
		m_AfterRecalculateItemDelegate = null;
		m_AfterSetItemCodeDelegate = null;
		m_BeforeSetItemContextDelegate = null;
		m_BeforeCreateSalesDocDelegate = null;
		m_AfterCreateSalesDocDelegate = null;
		m_BeforeSetItemXmlDelegate = null;
		m_AfterSetItemXmlDelegate = null;
		m_BeforeRecalculateItemPricesDelegate = null;
		m_AfterRecalculateItemPricesDelegate = null;
		m_BeforeCalculateSchedulingDelegate = null;
		m_AfterCalculateSchedulingDelegate = null;
		m_NewWizardSelectNumerationDelegate = null;
		m_AfterNewVersionDelegate = null;
		m_BeforeSetDataVersionDelegate = null;
		m_SearchConsolidationRiskPonderationDelegate = null;
		m_ReplaceItemCodeDelegate = null;
		m_AfterReplaceItemCodeDelegate = null;
	}
}
