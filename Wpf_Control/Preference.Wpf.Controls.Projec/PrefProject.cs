using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Windows.Input;
using System.Xml;
using Preference.Diagnostics;

namespace Preference.Wpf.Controls.Projects.AppLogic;

public class PrefProject : INotifyPropertyChanged
{
	private IServiceAgent _ServiceAgent;

	private Guid m_guidProjectId = Guid.Empty;

	private string m_strClientName = string.Empty;

	private long m_lProjectCode = -1L;

	private string m_strProjectName = string.Empty;

	private string m_strDescription = string.Empty;

	private DateTime m_dtCreationDate = DateTime.Now;

	private PrefAddress _CustomerAddress;

	private PrefAddress _ShippingAddress;

	private PrefAddress _InvoicingAddress;

	private double m_dAgreedAmount;

	private PrefCurrency m_pAgreedAmountCurrency = new PrefCurrency();

	private string m_strComments = string.Empty;

	private SalesDocument m_SalesDocForValuation;

	private ObservableCollection<Document> _DocumentsOfProject;

	private ObservableCollection<Document> _SalesDocumentsOfProject;

	private ObservableCollection<Document> _PurchasesDocumentsOfProject;

	private PrefCollection<Document> m_listDeletedDocuments = new PrefCollection<Document>();

	private PrefCollection<Contact> m_listContacts = new PrefCollection<Contact>();

	private PrefPricesPolicy m_pPricesPolicy = new PrefPricesPolicy();

	private PrefProjectStatus m_pProjectStatus = new PrefProjectStatus();

	private long _ClientCode;

	private string pricingComments = string.Empty;

	private bool m_bShowPricePolicy = true;

	private Contact _SelectedContact;

	public string PricingComments
	{
		get
		{
			return pricingComments;
		}
		set
		{
			pricingComments = value;
			OnPropertyChanged("PricingComments");
		}
	}

	public Contact SelectedContact
	{
		get
		{
			return _SelectedContact;
		}
		set
		{
			if (_SelectedContact != value)
			{
				_SelectedContact = value;
				OnPropertyChanged("SelectedContact");
			}
		}
	}

	public bool ShowPricePolicy
	{
		get
		{
			return m_bShowPricePolicy;
		}
		set
		{
			m_bShowPricePolicy = value;
		}
	}

	public bool IsPolicyChanged => PricesPolicy.PerGroupExpenditures.CollectionStatus == enStatus.Modified;

	public ObservableCollection<Document> DocumentsOfProject => _DocumentsOfProject;

	public ObservableCollection<Document> SalesDocumentsOfProject => _SalesDocumentsOfProject;

	public ObservableCollection<Document> PurchasesDocumentsOfProject => _PurchasesDocumentsOfProject;

	public PrefAddress InvoicingAddress
	{
		get
		{
			return _InvoicingAddress;
		}
		set
		{
			_InvoicingAddress = value;
		}
	}

	public PrefAddress ShippingAddress
	{
		get
		{
			return _ShippingAddress;
		}
		set
		{
			_ShippingAddress = value;
		}
	}

	public PrefAddress CustomerAddress
	{
		get
		{
			return _CustomerAddress;
		}
		set
		{
			_CustomerAddress = value;
		}
	}

	public PrefProjectStatus ProjectStatus => m_pProjectStatus;

	public PrefCollection<Contact> Contacts => m_listContacts;

	public PrefPricesPolicy PricesPolicy => m_pPricesPolicy;

	public PrefCollection<Document> DeletedDocuments => m_listDeletedDocuments;

	public string ProjectName
	{
		get
		{
			return m_strProjectName;
		}
		set
		{
			m_strProjectName = value;
			OnPropertyChanged("ProjectName");
		}
	}

	public long ProjectCode
	{
		get
		{
			return m_lProjectCode;
		}
		set
		{
			m_lProjectCode = value;
			OnPropertyChanged("ProjectCode");
		}
	}

	public long ClientCode
	{
		get
		{
			return _ClientCode;
		}
		set
		{
			bool flag = _ClientCode != value;
			_ClientCode = value;
			if (flag)
			{
				OnPropertyChanged("ClientCode");
			}
		}
	}

	public string ClientName
	{
		get
		{
			return m_strClientName;
		}
		set
		{
			bool flag = m_strClientName != value;
			m_strClientName = value;
			if (flag)
			{
				OnPropertyChanged("ClientName");
			}
		}
	}

	public Guid ProjectId => m_guidProjectId;

	public string Description
	{
		get
		{
			return m_strDescription;
		}
		set
		{
			m_strDescription = value;
			OnPropertyChanged("Description");
		}
	}

	public DateTime CreationDate
	{
		get
		{
			return m_dtCreationDate;
		}
		set
		{
			m_dtCreationDate = value;
			OnPropertyChanged("CreationDate");
		}
	}

	public double AgreedAmount
	{
		get
		{
			return m_dAgreedAmount;
		}
		set
		{
			m_dAgreedAmount = value;
			OnPropertyChanged("AgreedAmount");
		}
	}

	public PrefCurrency AgreedAmountCurrency
	{
		get
		{
			return m_pAgreedAmountCurrency;
		}
		set
		{
			m_pAgreedAmountCurrency = value;
			OnPropertyChanged("AgreedAmountCurrency");
		}
	}

	public string Comments
	{
		get
		{
			return m_strComments;
		}
		set
		{
			m_strComments = value;
			OnPropertyChanged("Comments");
		}
	}

	internal SalesDocument SalesDocForValuation
	{
		get
		{
			return m_SalesDocForValuation;
		}
		set
		{
			m_SalesDocForValuation = value;
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	public PrefProject(IServiceAgent ServiceAgent, Guid projectId)
	{
		_ServiceAgent = ServiceAgent;
		_DocumentsOfProject = new ObservableCollection<Document>();
		_SalesDocumentsOfProject = new ObservableCollection<Document>();
		_PurchasesDocumentsOfProject = new ObservableCollection<Document>();
		_CustomerAddress = new PrefAddress();
		_ShippingAddress = new PrefAddress();
		_InvoicingAddress = new PrefAddress();
	}

	public void m_listDocuments_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
	{
		switch (e.Action)
		{
		case NotifyCollectionChangedAction.Add:
			foreach (object newItem in e.NewItems)
			{
				if (newItem is Contact)
				{
					m_listContacts.Add(newItem as Contact);
					PrefContacList.Contacts.Remove(newItem as Contact);
				}
			}
			break;
		case NotifyCollectionChangedAction.Remove:
			foreach (object oldItem in e.OldItems)
			{
				if (oldItem is Contact)
				{
					m_listContacts.Remove(oldItem as Contact);
					if ((oldItem as Document).Status != enStatus.Created)
					{
						m_listDeletedDocuments.Add(oldItem as Document);
					}
					PrefContacList.Contacts.Add(oldItem as Contact);
				}
			}
			break;
		case NotifyCollectionChangedAction.Reset:
			m_listContacts.Clear();
			PrefContacList.Initialize();
			break;
		}
		OnPropertyChanged("Documents");
	}

	internal void RecalculateSalesDocs(object prefUserLink)
	{
		if (SalesDocumentsOfProject.Count <= 0)
		{
			return;
		}
		List<SalesDocument> list = new List<SalesDocument>();
		foreach (Document item2 in SalesDocumentsOfProject)
		{
			SalesDocument item = item2 as SalesDocument;
			list.Add(item);
		}
		ServiceAgent.RecalculateSalesDocs(list, prefUserLink);
	}

	private SalesDocument GetFirstSalesDocumentForValuation()
	{
		if (_ServiceAgent.TryGetFirstSalesDocumentForValuation(ProjectId, out var salesDoc))
		{
			return salesDoc;
		}
		return null;
	}

	public bool Load(long nProjectCode, ref string strCommandResults, ref string strErrors)
	{
		try
		{
			ServiceAgent serviceAgent = new ServiceAgent(Globals.ConnectionString);
			serviceAgent.LoadNonProjectData(out m_bShowPricePolicy);
			string strCommandXml = Globals.CommandsHeaderXML + "\r\n\t<cmd:Command name=\"GetProjectDetails\">\r\n\t\t<cmd:Parameter name=\"ProjectCode\" value=\"" + nProjectCode + "\"/>\r\n\t</cmd:Command>\r\n\t<cmd:Command name=\"GetProjectStatus\">\r\n\t\t<cmd:Parameter name=\"ProjectCode\" value=\"" + nProjectCode + "\"/>\r\n\t\t<cmd:Parameter name=\"ShowWorkforceCost\" value=\"" + (m_bShowPricePolicy ? "1" : "0") + "\"/>\r\n\t</cmd:Command>\r\n</cmd:Commands>";
			bool flag = serviceAgent.ExecuteCommand(strCommandXml, ref strCommandResults, ref strErrors);
			if (flag)
			{
				Logger.Instance.WriteInformation(strCommandResults, "PrefGest");
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.LoadXml(strCommandResults);
				XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
				xmlNamespaceManager.AddNamespace("cmd", Globals.PrefCADCommandNamespaceUri);
				xmlNamespaceManager.AddNamespace("pmsg", Globals.MessageNamespaceUri);
				XmlNode xmlNode = xmlDocument.SelectSingleNode("/cmd:Commands/cmd:CommandResult[@name = 'GetProjectDetails']", xmlNamespaceManager);
				string xpath = $"child::cmd:Parameter[@name = '{Globals.ParamNameProjectId}']";
				string value = xmlNode.SelectSingleNode(xpath, xmlNamespaceManager).Attributes.GetNamedItem("value").Value;
				m_guidProjectId = new Guid(value);
				ProjectCode = nProjectCode;
				ProjectName = xmlNode.SelectSingleNode("cmd:Parameter[@name=\"" + Globals.ParamNameProjectName + "\"]", xmlNamespaceManager).Attributes["value"].Value;
				string value2 = xmlNode.SelectSingleNode("cmd:Parameter[@name=\"" + Globals.ParamNameCustomerCode + "\"]", xmlNamespaceManager).Attributes["value"].Value;
				_ClientCode = ((!string.IsNullOrEmpty(value2)) ? Convert.ToInt32(value2) : 0);
				ClientName = xmlNode.SelectSingleNode("cmd:Parameter[@name=\"" + Globals.ParamNameCustomerName + "\"]", xmlNamespaceManager).Attributes["value"].Value;
				Description = xmlNode.SelectSingleNode("cmd:Parameter[@name=\"" + Globals.ParamNameProjectDescription + "\"]", xmlNamespaceManager).Attributes["value"].Value;
				CreationDate = DateTime.Parse(xmlNode.SelectSingleNode("cmd:Parameter[@name=\"" + Globals.ParamNameProjectCreationDate + "\"]", xmlNamespaceManager).Attributes["value"].Value, DateTimeFormatInfo.InvariantInfo);
				CustomerAddress.Address1 = xmlNode.SelectSingleNode("cmd:Parameter[@name=\"" + Globals.ParamNameProjectCustomerAddress1 + "\"]", xmlNamespaceManager).Attributes["value"].Value;
				CustomerAddress.Address2 = xmlNode.SelectSingleNode("cmd:Parameter[@name=\"" + Globals.ParamNameProjectCustomerAddress2 + "\"]", xmlNamespaceManager).Attributes["value"].Value;
				CustomerAddress.PostalCode = xmlNode.SelectSingleNode("cmd:Parameter[@name=\"" + Globals.ParamNameProjectCustomerPostalCode + "\"]", xmlNamespaceManager).Attributes["value"].Value;
				CustomerAddress.City = xmlNode.SelectSingleNode("cmd:Parameter[@name=\"" + Globals.ParamNameProjectCustomerCity + "\"]", xmlNamespaceManager).Attributes["value"].Value;
				CustomerAddress.Province = xmlNode.SelectSingleNode("cmd:Parameter[@name=\"" + Globals.ParamNameProjectCustomerProvince + "\"]", xmlNamespaceManager).Attributes["value"].Value;
				CustomerAddress.Country = xmlNode.SelectSingleNode("cmd:Parameter[@name=\"" + Globals.ParamNameProjectCustomerCountry + "\"]", xmlNamespaceManager).Attributes["value"].Value;
				ShippingAddress.Address1 = xmlNode.SelectSingleNode("cmd:Parameter[@name=\"" + Globals.ParamNameProjectShippingAddress1 + "\"]", xmlNamespaceManager).Attributes["value"].Value;
				ShippingAddress.Address2 = xmlNode.SelectSingleNode("cmd:Parameter[@name=\"" + Globals.ParamNameProjectShippingAddress2 + "\"]", xmlNamespaceManager).Attributes["value"].Value;
				ShippingAddress.PostalCode = xmlNode.SelectSingleNode("cmd:Parameter[@name=\"" + Globals.ParamNameProjectShippingPostalCode + "\"]", xmlNamespaceManager).Attributes["value"].Value;
				ShippingAddress.City = xmlNode.SelectSingleNode("cmd:Parameter[@name=\"" + Globals.ParamNameProjectShippingCity + "\"]", xmlNamespaceManager).Attributes["value"].Value;
				ShippingAddress.Province = xmlNode.SelectSingleNode("cmd:Parameter[@name=\"" + Globals.ParamNameProjectShippingProvince + "\"]", xmlNamespaceManager).Attributes["value"].Value;
				ShippingAddress.Country = xmlNode.SelectSingleNode("cmd:Parameter[@name=\"" + Globals.ParamNameProjectShippingCountry + "\"]", xmlNamespaceManager).Attributes["value"].Value;
				InvoicingAddress.Address1 = xmlNode.SelectSingleNode("cmd:Parameter[@name=\"" + Globals.ParamNameProjectInvoicingAddress1 + "\"]", xmlNamespaceManager).Attributes["value"].Value;
				InvoicingAddress.Address2 = xmlNode.SelectSingleNode("cmd:Parameter[@name=\"" + Globals.ParamNameProjectInvoicingAddress2 + "\"]", xmlNamespaceManager).Attributes["value"].Value;
				InvoicingAddress.PostalCode = xmlNode.SelectSingleNode("cmd:Parameter[@name=\"" + Globals.ParamNameProjectInvoicingPostalCode + "\"]", xmlNamespaceManager).Attributes["value"].Value;
				InvoicingAddress.City = xmlNode.SelectSingleNode("cmd:Parameter[@name=\"" + Globals.ParamNameProjectInvoicingCity + "\"]", xmlNamespaceManager).Attributes["value"].Value;
				InvoicingAddress.Province = xmlNode.SelectSingleNode("cmd:Parameter[@name=\"" + Globals.ParamNameProjectInvoicingProvince + "\"]", xmlNamespaceManager).Attributes["value"].Value;
				InvoicingAddress.Country = xmlNode.SelectSingleNode("cmd:Parameter[@name=\"" + Globals.ParamNameProjectInvoicingCountry + "\"]", xmlNamespaceManager).Attributes["value"].Value;
				PricesPolicy.ChangedByUser = false;
				PricesPolicy.SaleDiscount = Convert.ToDouble(xmlNode.SelectSingleNode("cmd:Parameter[@name=\"" + Globals.ParamNameProjectDiscount + "\"]", xmlNamespaceManager).Attributes["value"].Value, NumberFormatInfo.InvariantInfo);
				PricesPolicy.Expenditures[0].CoefficientAsFactor = Convert.ToDouble(xmlNode.SelectSingleNode("cmd:Parameter[@name=\"" + Globals.ParamNameContingencies + "\"]", xmlNamespaceManager).Attributes["value"].Value, NumberFormatInfo.InvariantInfo);
				PricesPolicy.Expenditures[1].CoefficientAsFactor = Convert.ToDouble(xmlNode.SelectSingleNode("cmd:Parameter[@name=\"" + Globals.ParamNameGeneralExpenditures + "\"]", xmlNamespaceManager).Attributes["value"].Value, NumberFormatInfo.InvariantInfo);
				PricesPolicy.Expenditures[2].CoefficientAsFactor = Convert.ToDouble(xmlNode.SelectSingleNode("cmd:Parameter[@name=\"" + Globals.ParamNameFinancialExpenditures + "\"]", xmlNamespaceManager).Attributes["value"].Value, NumberFormatInfo.InvariantInfo);
				PricesPolicy.Expenditures[3].CoefficientAsFactor = Convert.ToDouble(xmlNode.SelectSingleNode("cmd:Parameter[@name=\"" + Globals.ParamNameCommissions + "\"]", xmlNamespaceManager).Attributes["value"].Value, NumberFormatInfo.InvariantInfo);
				string value3 = xmlNode.SelectSingleNode("cmd:Parameter[@name=\"" + Globals.ParamNameProjectAgreedAmountCurrency + "\"]", xmlNamespaceManager).Attributes["value"].Value;
				foreach (PrefCurrency currency in PrefCurrencyList.Currencies)
				{
					if (currency.Name == value3)
					{
						AgreedAmountCurrency = currency;
						break;
					}
				}
				Comments = xmlNode.SelectSingleNode("cmd:Parameter[@name=\"" + Globals.ParamNameProjectComments + "\"]", xmlNamespaceManager).Attributes["value"].Value;
				PricingComments = xmlNode.SelectSingleNode("cmd:Parameter[@name=\"" + Globals.ParamNameProjectPricingComments + "\"]", xmlNamespaceManager).Attributes["value"].Value;
				Globals.ProjectId = ProjectId.ToString("D").ToUpper();
				string xpath2 = "descendant::cmd:Parameter[@name=\"InstancesGroupedByStatus\"]/descendant::cmd:Item[@name=\"InstancesStatus\"]";
				XmlNodeList xmlNodeList = xmlDocument.SelectNodes(xpath2, xmlNamespaceManager);
				if (xmlNodeList != null && xmlNodeList.Count > 8)
				{
					try
					{
						ProjectStatus.Accepted = Convert.ToInt32(xmlNodeList[0].ChildNodes[0].Attributes[2].Value);
						ProjectStatus.Estimated = Convert.ToInt32(xmlNodeList[1].ChildNodes[0].Attributes[2].Value);
						ProjectStatus.Ordered = Convert.ToInt32(xmlNodeList[2].ChildNodes[0].Attributes[2].Value);
						ProjectStatus.Purchased = Convert.ToInt32(xmlNodeList[3].ChildNodes[0].Attributes[2].Value);
						ProjectStatus.Started = Convert.ToInt32(xmlNodeList[4].ChildNodes[0].Attributes[2].Value);
						ProjectStatus.Finished = Convert.ToInt32(xmlNodeList[5].ChildNodes[0].Attributes[2].Value);
						ProjectStatus.Sent = Convert.ToInt32(xmlNodeList[6].ChildNodes[0].Attributes[2].Value);
						ProjectStatus.Delivered = Convert.ToInt32(xmlNodeList[7].ChildNodes[0].Attributes[2].Value);
						ProjectStatus.Mounted = Convert.ToInt32(xmlNodeList[8].ChildNodes[0].Attributes[2].Value);
						ProjectStatus.Invoiced = Convert.ToInt32(xmlNodeList[9].ChildNodes[0].Attributes[2].Value);
						ProjectStatus.TotalInstances = Convert.ToInt32(xmlNodeList[10].ChildNodes[0].Attributes[2].Value);
						AgreedAmount = Convert.ToDouble(xmlNodeList[2].ChildNodes[1].Attributes[2].Value, NumberFormatInfo.InvariantInfo);
					}
					catch (Exception ex)
					{
						Logger.Instance.WriteError(ex, "PrefGest");
					}
				}
				xpath2 = "descendant::cmd:Parameter[@name=\"GlobalStatus\"]/descendant::cmd:Item[@name=\"OverallFigure\"]";
				xmlNodeList = xmlDocument.SelectNodes(xpath2, xmlNamespaceManager);
				if (xmlNodeList != null && xmlNodeList.Count > 3)
				{
					try
					{
						ProjectStatus.TotalSales = Convert.ToDouble(xmlNodeList[0].ChildNodes[0].Attributes[2].Value, NumberFormatInfo.InvariantInfo);
						ProjectStatus.TotalPurchases = Convert.ToDouble(xmlNodeList[1].ChildNodes[0].Attributes[2].Value, NumberFormatInfo.InvariantInfo);
						ProjectStatus.TotalProduction = Convert.ToDouble(xmlNodeList[2].ChildNodes[0].Attributes[2].Value, NumberFormatInfo.InvariantInfo);
						ProjectStatus.TotalWorkforce = Convert.ToDouble(xmlNodeList[3].ChildNodes[0].Attributes[2].Value, NumberFormatInfo.InvariantInfo);
					}
					catch (Exception ex2)
					{
						Logger.Instance.WriteError(ex2, "PrefGest");
					}
				}
				ResetModifiedFlags();
				SalesDocForValuation = GetFirstSalesDocumentForValuation();
				LoadDocumentsAndContacts(m_guidProjectId, serviceAgent);
			}
			return flag;
		}
		catch (Exception ex3)
		{
			Logger.Instance.WriteError(ex3, "PrefGest");
			throw;
		}
	}

	private void LoadDocumentsAndContacts(Guid projectId, IServiceAgent serviceAgent)
	{
		Contacts.Clear();
		DocumentsOfProject.Clear();
		SalesDocumentsOfProject.Clear();
		PurchasesDocumentsOfProject.Clear();
		DataSet salesDocumentsOfProject = serviceAgent.GetSalesDocumentsOfProject(projectId);
		foreach (DataRow row in salesDocumentsOfProject.Tables[0].Rows)
		{
			int num = 0;
			string empty = string.Empty;
			string customerName = string.Empty;
			int num2 = Convert.ToInt32(row["DestDocumentType"]);
			int relationShipType = Convert.ToInt32(row["RelationShipType"]);
			int num3 = Convert.ToInt32(row["PAF_Type"]);
			int num4 = Convert.ToInt32(row["PAF_Number"]);
			int num5 = Convert.ToInt32(row["PAF_Version"]);
			string text = Convert.ToString(row["PAF_StrDocumentType"]);
			if (!row.IsNull("PAF_OrderNumber"))
			{
				num = Convert.ToInt32(row["PAF_OrderNumber"]);
			}
			if (!row.IsNull("PAF_VersionName"))
			{
				empty = Convert.ToString(row["PAF_VersionName"]);
			}
			if (!row.IsNull("PAF_Nombre"))
			{
				customerName = Convert.ToString(row["PAF_Nombre"]);
			}
			SalesDocument salesDocument = new SalesDocument();
			salesDocument.CustomerName = customerName;
			salesDocument.Number = num4;
			salesDocument.Version = num5;
			if (num != 0)
			{
				salesDocument.DocumentName = $"{text} {num4}/{num5} ({num})";
			}
			else
			{
				salesDocument.DocumentName = $"{text} {num4}/{num5}";
			}
			switch (num3)
			{
			case 1:
				salesDocument.DocumentSubType = DocumentSubTypes.SalesOffer;
				break;
			case 2:
				salesDocument.DocumentSubType = DocumentSubTypes.SalesOrder;
				break;
			case 3:
				salesDocument.DocumentSubType = DocumentSubTypes.SalesDeliveryNote;
				break;
			case 4:
				salesDocument.DocumentSubType = DocumentSubTypes.SalesInvoice;
				break;
			case 5:
				salesDocument.DocumentSubType = DocumentSubTypes.SalesPhase;
				break;
			}
			salesDocument.DocumentType = DocumentTypes.SalesDocument;
			salesDocument.RelationShipType = relationShipType;
			salesDocument.SupplierName = string.Empty;
			DocumentsOfProject.Add(salesDocument);
			SalesDocumentsOfProject.Add(salesDocument);
		}
		salesDocumentsOfProject = serviceAgent.GetPurchasesDocumentsOfProject(projectId);
		foreach (DataRow row2 in salesDocumentsOfProject.Tables[0].Rows)
		{
			int num6 = Convert.ToInt32(row2["DestDocumentType"]);
			int num7 = Convert.ToInt32(row2["RelationShipType"]);
			int num8 = Convert.ToInt32(row2["PURCHASES_Number"]);
			int numeration = Convert.ToInt32(row2["PURCHASES_Numeration"]);
			int num9 = Convert.ToInt32(row2["PURCHASES_DocumentType"]);
			string text2 = Convert.ToString(row2["PURCHASES_Nombre"]);
			string arg = Convert.ToString(row2["PURCHASES_STRDocumentType"]);
			PurchaseDocument purchaseDocument = new PurchaseDocument();
			purchaseDocument.SupplierName = text2;
			purchaseDocument.DocumentName = $"{arg} {num8} ({text2})";
			purchaseDocument.Number = num8;
			purchaseDocument.Numeration = numeration;
			switch (num9)
			{
			case 1:
				purchaseDocument.DocumentSubType = DocumentSubTypes.PurchasesOffer;
				break;
			case 2:
				purchaseDocument.DocumentSubType = DocumentSubTypes.PurchasesOrder;
				break;
			case 3:
				purchaseDocument.DocumentSubType = DocumentSubTypes.PurchasesDeliveryNote;
				break;
			case 4:
				purchaseDocument.DocumentSubType = DocumentSubTypes.PurchasesInvoice;
				break;
			case 5:
				purchaseDocument.DocumentSubType = DocumentSubTypes.PurchasesPhase;
				break;
			}
			purchaseDocument.DocumentType = DocumentTypes.PurchasesDocument;
			DocumentsOfProject.Add(purchaseDocument);
			PurchasesDocumentsOfProject.Add(purchaseDocument);
		}
		salesDocumentsOfProject = serviceAgent.GetProductionLotDocumentsOfProject(projectId);
		foreach (DataRow row3 in salesDocumentsOfProject.Tables[0].Rows)
		{
			int num10 = Convert.ToInt32(row3["DestDocumentType"]);
			int num11 = Convert.ToInt32(row3["RelationShipType"]);
			int num12 = Convert.ToInt32(row3["Numero"]);
			ProductionLotDocument productionLotDocument = new ProductionLotDocument();
			productionLotDocument.SupplierName = string.Empty;
			productionLotDocument.DocumentName = $"{num12}";
			productionLotDocument.DocumentSubType = DocumentSubTypes.ProductionLot;
			productionLotDocument.DocumentType = DocumentTypes.ProductionLot;
			productionLotDocument.Number = num12;
			DocumentsOfProject.Add(productionLotDocument);
		}
		salesDocumentsOfProject = serviceAgent.GetExpenseDocumentsOfProject(projectId);
		foreach (DataRow row4 in salesDocumentsOfProject.Tables[0].Rows)
		{
			int num13 = Convert.ToInt32(row4["Number"]);
			string text3 = string.Empty;
			if (!row4.IsNull("Title"))
			{
				text3 = Convert.ToString(row4["Title"]);
			}
			ExpensesDocument expensesDocument = new ExpensesDocument();
			expensesDocument.Number = num13;
			expensesDocument.Title = text3;
			expensesDocument.DocumentName = $"{num13}-{text3}";
			expensesDocument.DocumentSubType = DocumentSubTypes.Expense;
			expensesDocument.DocumentType = DocumentTypes.ExpenseDocument;
			DocumentsOfProject.Add(expensesDocument);
		}
		salesDocumentsOfProject = serviceAgent.GetCustomerDocumentsOfProject(projectId);
		foreach (DataRow row5 in salesDocumentsOfProject.Tables[0].Rows)
		{
			Contact item = DataRowToContact(row5);
			Contacts.Add(item);
		}
	}

	public static Contact DataRowToContact(DataRow dr)
	{
		Contact contact = new Contact();
		if (!dr.IsNull("Address"))
		{
			contact.Address = Convert.ToString(dr["Address"]).Trim();
		}
		if (!dr.IsNull("CityName"))
		{
			contact.City = Convert.ToString(dr["CityName"]).Trim();
		}
		if (!dr.IsNull("CompanyName"))
		{
			contact.CompanyName = Convert.ToString(dr["CompanyName"]).Trim();
		}
		if (!dr.IsNull("ContactId"))
		{
			contact.ContactCode = Convert.ToInt32(dr["ContactId"]);
		}
		if (!dr.IsNull("CountryName"))
		{
			contact.Country = Convert.ToString(dr["CountryName"]).Trim();
		}
		if (!dr.IsNull("ContactName"))
		{
			contact.CustomerName = Convert.ToString(dr["ContactName"]).Trim();
			contact.FullName = contact.CustomerName;
		}
		if (!dr.IsNull("DestDocumentId"))
		{
			contact.DocumentId = new Guid(Convert.ToString(dr["DestDocumentId"]));
		}
		contact.DocumentType = DocumentTypes.Customer;
		if (!dr.IsNull("Email"))
		{
			contact.Email = Convert.ToString(dr["Email"]).Trim();
		}
		if (!dr.IsNull("Fax"))
		{
			contact.Fax = Convert.ToString(dr["Fax"]).Trim();
		}
		if (!dr.IsNull("PostalCode"))
		{
			contact.PostalCode = Convert.ToString(dr["PostalCode"]).Trim();
		}
		if (!dr.IsNull("ProvinceName"))
		{
			contact.Province = Convert.ToString(dr["ProvinceName"]).Trim();
		}
		if (!dr.IsNull("Number"))
		{
			contact.Telephone = Convert.ToString(dr["Number"]).Trim();
		}
		contact.Status = enStatus.Loading;
		return contact;
	}

	internal bool LoadModifiedTariffValues(XmlNodeList nodelist, PrefProjectPerGroupExpenditure pge, bool bProviderDiscountForColor, bool bCostIncrementForColor, bool bSalesIncrementForColor, XmlNamespaceManager xmlNamespaceMgr)
	{
		foreach (XmlNode item in nodelist)
		{
			string text = item.ChildNodes[0].Attributes["value"].Value.ToString().TrimEnd();
			if (text != ProjectCode.ToString())
			{
				continue;
			}
			XmlNode xmlNode2 = item.SelectSingleNode("cmd:Item[@name='Color']", xmlNamespaceMgr);
			if (xmlNode2 == null)
			{
				continue;
			}
			string text2 = xmlNode2.Attributes["value"].Value.ToString().TrimEnd();
			if (!(text2 != pge.Color))
			{
				string text3 = item.ChildNodes[1].Attributes["value"].Value.ToString().TrimEnd();
				XmlNode xmlNode3 = item.SelectSingleNode("cmd:Item[@name='Value']", xmlNamespaceMgr);
				double num = Convert.ToDouble(xmlNode3.Attributes["value"].Value.ToString().Trim(), NumberFormatInfo.InvariantInfo);
				if (text3 == PricesPolicy.ProviderDiscountTariffName)
				{
					pge.ProviderDiscountAsFactor = num;
					pge.PreviousProviderDiscount = num;
					bProviderDiscountForColor = true;
				}
				else if (text3 == PricesPolicy.SalesIncrementTariffName)
				{
					pge.CoefficientAsFactor = num;
					pge.PreviousCoefficient = num;
					bSalesIncrementForColor = true;
				}
				else if (text3 == PricesPolicy.CostIncrementTariffName)
				{
					pge.RemnantIncrementAsFactor = num;
					pge.PreviousRemnantIncrement = num;
					bCostIncrementForColor = true;
				}
			}
		}
		foreach (XmlNode item2 in nodelist)
		{
			string text4 = item2.ChildNodes[0].Attributes["value"].Value.ToString().TrimEnd();
			if (text4 != ProjectCode.ToString())
			{
				continue;
			}
			XmlNode xmlNode5 = item2.SelectSingleNode("cmd:Item[@name='Color']", xmlNamespaceMgr);
			if (xmlNode5 == null)
			{
				string text5 = item2.ChildNodes[1].Attributes["value"].Value.ToString().TrimEnd();
				XmlNode xmlNode6 = item2.SelectSingleNode("cmd:Item[@name='Value']", xmlNamespaceMgr);
				double num2 = Convert.ToDouble(xmlNode6.Attributes["value"].Value.ToString().Trim(), NumberFormatInfo.InvariantInfo);
				if (text5 == PricesPolicy.ProviderDiscountTariffName && !bProviderDiscountForColor)
				{
					pge.ProviderDiscountAsFactor = num2;
					pge.PreviousProviderDiscount = num2;
				}
				else if (text5 == PricesPolicy.SalesIncrementTariffName && !bSalesIncrementForColor)
				{
					pge.CoefficientAsFactor = num2;
					pge.PreviousCoefficient = num2;
				}
				else if (text5 == PricesPolicy.CostIncrementTariffName && !bCostIncrementForColor)
				{
					pge.RemnantIncrementAsFactor = num2;
					pge.PreviousRemnantIncrement = num2;
				}
			}
		}
		return true;
	}

	internal bool LoadOriginalTariffValues(XmlNodeList nodelist, PrefProjectPerGroupExpenditure pge, ref bool bProviderDiscountForColor, ref bool bCostIncrementForColor, ref bool bSalesIncrementForColor, XmlNamespaceManager xmlNamespaceMgr)
	{
		foreach (XmlNode item in nodelist)
		{
			string text = item.ChildNodes[0].Attributes["value"].Value.ToString().TrimEnd();
			if (text == ProjectCode.ToString())
			{
				continue;
			}
			XmlNode xmlNode2 = item.SelectSingleNode("cmd:Item[@name='Color']", xmlNamespaceMgr);
			if (xmlNode2 == null)
			{
				continue;
			}
			string text2 = xmlNode2.Attributes["value"].Value.ToString().TrimEnd();
			if (!(text2 != pge.Color))
			{
				XmlNode xmlNode3 = item.SelectSingleNode("cmd:Item[@name='Value']", xmlNamespaceMgr);
				double num = Convert.ToDouble(xmlNode3.Attributes["value"].Value.ToString().Trim(), NumberFormatInfo.InvariantInfo);
				if (text == PricesPolicy.ProviderDiscountTariffName)
				{
					pge.OriginalProviderDiscount = num;
					pge.ProviderDiscountAsFactor = num;
					bProviderDiscountForColor = true;
				}
				else if (text == PricesPolicy.SalesIncrementTariffName)
				{
					pge.OriginalCoefficient = num;
					pge.CoefficientAsFactor = num;
					bSalesIncrementForColor = true;
				}
				else if (text == PricesPolicy.CostIncrementTariffName)
				{
					pge.OriginalRemnantIncrement = num;
					pge.RemnantIncrementAsFactor = num;
					bCostIncrementForColor = true;
				}
			}
		}
		foreach (XmlNode item2 in nodelist)
		{
			string text3 = item2.ChildNodes[0].Attributes["value"].Value.ToString().TrimEnd();
			if (text3 == ProjectCode.ToString())
			{
				continue;
			}
			XmlNode xmlNode5 = item2.SelectSingleNode("cmd:Item[@name='Color']", xmlNamespaceMgr);
			if (xmlNode5 == null)
			{
				XmlNode xmlNode6 = item2.SelectSingleNode("cmd:Item[@name='Value']", xmlNamespaceMgr);
				double num2 = Convert.ToDouble(xmlNode6.Attributes["value"].Value.ToString().Trim(), NumberFormatInfo.InvariantInfo);
				if (text3 == PricesPolicy.ProviderDiscountTariffName && !bProviderDiscountForColor)
				{
					pge.OriginalProviderDiscount = num2;
					pge.ProviderDiscountAsFactor = num2;
				}
				else if (text3 == PricesPolicy.SalesIncrementTariffName && !bSalesIncrementForColor)
				{
					pge.OriginalCoefficient = num2;
					pge.CoefficientAsFactor = num2;
				}
				else if (text3 == PricesPolicy.CostIncrementTariffName && !bCostIncrementForColor)
				{
					pge.OriginalRemnantIncrement = num2;
					pge.RemnantIncrementAsFactor = num2;
				}
			}
		}
		return true;
	}

	internal bool LoadPricesPolicyDetailData()
	{
		if (PricesPolicy.IsPriceDetailLoaded)
		{
			return true;
		}
		ServiceAgent serviceAgent = new ServiceAgent(Globals.ConnectionString);
		string strCommandXml = Globals.CommandsHeaderXML + "\r\n\t\t\t\t<cmd:Command name=\"GetProjectPricePolicy\">\r\n\t\t\t\t\t<cmd:Parameter name=\"ProjectCode\" value=\"" + ProjectCode + "\"/>\r\n\t\t\t\t</cmd:Command>\r\n\t\t\t</cmd:Commands>";
		string strResults = "";
		string strErrors = "";
		Cursor overrideCursor = Mouse.OverrideCursor;
		try
		{
			Mouse.OverrideCursor = Cursors.Wait;
			bool flag = serviceAgent.ExecuteCommand(strCommandXml, ref strResults, ref strErrors);
			if (flag)
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.LoadXml(strResults);
				XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
				xmlNamespaceManager.AddNamespace("cmd", Globals.PrefCADCommandNamespaceUri);
				xmlNamespaceManager.AddNamespace("pmsg", Globals.MessageNamespaceUri);
				string xpath = "descendant::cmd:Parameter[@name=\"RedefinedTariffNames\"]/descendant::cmd:Item[@name=\"ForPurchase\"]";
				XmlNode xmlNode = xmlDocument.SelectSingleNode(xpath, xmlNamespaceManager);
				if (xmlNode != null)
				{
					PricesPolicy.ProviderDiscountTariffName = xmlNode.Attributes["value"].Value.ToString();
				}
				xpath = "descendant::cmd:Parameter[@name=\"RedefinedTariffNames\"]/descendant::cmd:Item[@name=\"ForCost\"]";
				xmlNode = xmlDocument.SelectSingleNode(xpath, xmlNamespaceManager);
				if (xmlNode != null)
				{
					PricesPolicy.CostIncrementTariffName = xmlNode.Attributes["value"].Value.ToString();
				}
				xpath = "descendant::cmd:Parameter[@name=\"RedefinedTariffNames\"]/descendant::cmd:Item[@name=\"ForPrice\"]";
				xmlNode = xmlDocument.SelectSingleNode(xpath, xmlNamespaceManager);
				if (xmlNode != null)
				{
					PricesPolicy.SalesIncrementTariffName = xmlNode.Attributes["value"].Value.ToString();
				}
				SalesDocument salesDocForValuation = SalesDocForValuation;
				PricesPolicy.Reset();
				PricesPolicy.LoadPerGroupExpenditures(salesDocForValuation);
				foreach (PrefProjectPerGroupExpenditure perGroupExpenditure in PricesPolicy.PerGroupExpenditures)
				{
					xpath = "descendant::cmd:CommandResult[@name=\"GetProjectPricePolicy\"]/descendant::cmd:Item[@name=\"Exception\"]/cmd:Item[@name=\"" + Globals.ParamNameGroupRowId + "\"][@value=\"" + perGroupExpenditure.Group.RowId.ToString("D").ToLower() + "\"]/..";
					XmlNodeList xmlNodeList = xmlDocument.SelectNodes(xpath, xmlNamespaceManager);
					if (xmlNodeList != null)
					{
						bool bProviderDiscountForColor = false;
						bool bCostIncrementForColor = false;
						bool bSalesIncrementForColor = false;
						LoadOriginalTariffValues(xmlNodeList, perGroupExpenditure, ref bProviderDiscountForColor, ref bCostIncrementForColor, ref bSalesIncrementForColor, xmlNamespaceManager);
						LoadModifiedTariffValues(xmlNodeList, perGroupExpenditure, bProviderDiscountForColor, bCostIncrementForColor, bSalesIncrementForColor, xmlNamespaceManager);
					}
				}
				if (salesDocForValuation != null)
				{
					PricesPolicy.ValuateSalesDocument(salesDocForValuation);
				}
				ResetPricePolicyFlags();
			}
			PricesPolicy.IsPriceDetailLoaded = true;
			return flag;
		}
		finally
		{
			Mouse.OverrideCursor = overrideCursor;
		}
	}

	private void ResetModifiedFlags()
	{
		DeletedDocuments.Clear();
		foreach (Contact listContact in m_listContacts)
		{
			listContact.Status = enStatus.Unchanged;
		}
		ResetPricePolicyFlags();
	}

	private void ResetPricePolicyFlags()
	{
		PricesPolicy.PerGroupExpenditures.CollectionStatus = enStatus.Unchanged;
		foreach (PrefProjectPerGroupExpenditure perGroupExpenditure in PricesPolicy.PerGroupExpenditures)
		{
			perGroupExpenditure.StatusProviderDiscount = enStatus.Unchanged;
			perGroupExpenditure.StatusSalesIncrement = enStatus.Unchanged;
			perGroupExpenditure.StatusRemnantIncrement = enStatus.Unchanged;
		}
	}

	public bool Save(ref string strCommandResults, ref string strErrors)
	{
		XmlDocument xmlDocument = new XmlDocument();
		XmlElement xmlElement = xmlDocument.CreateElement("cmd", "Commands", Globals.PrefCADCommandNamespaceUri);
		xmlDocument.AppendChild(xmlElement);
		if (!string.IsNullOrEmpty(Globals.ConnectionPassword) && !string.IsNullOrEmpty(Globals.ConnectionUserID))
		{
			XmlElement xmlElement2 = xmlDocument.CreateElement("cmd", "Connection", Globals.PrefCADCommandNamespaceUri);
			xmlElement2.SetAttribute("server", Globals.ConnectionServer);
			xmlElement2.SetAttribute("database", Globals.ConnectionDatabase);
			xmlElement2.SetAttribute("trustedConnection", Globals.IntegratedSecurity.ToString());
			xmlElement2.SetAttribute("user", Globals.ConnectionUserID);
			xmlElement2.SetAttribute("password", Globals.ConnectionPassword);
			xmlElement.AppendChild(xmlElement2);
		}
		XmlElement xmlElement3 = xmlDocument.CreateElement("cmd", "Command", Globals.PrefCADCommandNamespaceUri);
		xmlElement3.SetAttribute("name", "UpdateProjectDetails");
		xmlElement.AppendChild(xmlElement3);
		AddParameter(xmlElement3, Globals.ParamNameProjectCode, ProjectCode.ToString());
		AddParameter(xmlElement3, Globals.ParamNameProjectName, ProjectName);
		AddParameter(xmlElement3, Globals.ParamNameCustomerCode, ClientCode.ToString());
		AddParameter(xmlElement3, Globals.ParamNameCustomerName, ClientName);
		AddParameter(xmlElement3, Globals.ParamNameProjectDescription, Description);
		AddParameter(xmlElement3, Globals.ParamNameProjectCreationDate, CreationDate.ToString("s"));
		AddParameter(xmlElement3, Globals.ParamNameProjectCustomerAddress1, CustomerAddress.Address1);
		AddParameter(xmlElement3, Globals.ParamNameProjectCustomerAddress2, CustomerAddress.Address2);
		AddParameter(xmlElement3, Globals.ParamNameProjectCustomerPostalCode, CustomerAddress.PostalCode);
		AddParameter(xmlElement3, Globals.ParamNameProjectCustomerCity, CustomerAddress.City);
		AddParameter(xmlElement3, Globals.ParamNameProjectCustomerProvince, CustomerAddress.Province);
		AddParameter(xmlElement3, Globals.ParamNameProjectCustomerCountry, CustomerAddress.Country);
		AddParameter(xmlElement3, Globals.ParamNameProjectShippingAddress1, ShippingAddress.Address1);
		AddParameter(xmlElement3, Globals.ParamNameProjectShippingAddress2, ShippingAddress.Address2);
		AddParameter(xmlElement3, Globals.ParamNameProjectShippingPostalCode, ShippingAddress.PostalCode);
		AddParameter(xmlElement3, Globals.ParamNameProjectShippingCity, ShippingAddress.City);
		AddParameter(xmlElement3, Globals.ParamNameProjectShippingProvince, ShippingAddress.Province);
		AddParameter(xmlElement3, Globals.ParamNameProjectShippingCountry, ShippingAddress.Country);
		AddParameter(xmlElement3, Globals.ParamNameProjectInvoicingAddress1, InvoicingAddress.Address1);
		AddParameter(xmlElement3, Globals.ParamNameProjectInvoicingAddress2, InvoicingAddress.Address2);
		AddParameter(xmlElement3, Globals.ParamNameProjectInvoicingPostalCode, InvoicingAddress.PostalCode);
		AddParameter(xmlElement3, Globals.ParamNameProjectInvoicingCity, InvoicingAddress.City);
		AddParameter(xmlElement3, Globals.ParamNameProjectInvoicingProvince, InvoicingAddress.Province);
		AddParameter(xmlElement3, Globals.ParamNameProjectInvoicingCountry, InvoicingAddress.Country);
		AddParameter(xmlElement3, Globals.ParamNameProjectAgreedAmount, AgreedAmount.ToString(NumberFormatInfo.InvariantInfo));
		AddParameter(xmlElement3, Globals.ParamNameProjectAgreedAmountCurrency, AgreedAmountCurrency.Name);
		AddParameter(xmlElement3, Globals.ParamNameProjectComments, Comments);
		AddParameter(xmlElement3, Globals.ParamNameProjectPricingComments, PricingComments);
		AddParameter(xmlElement3, Globals.ParamNameProjectDiscount, PricesPolicy.SaleDiscount.ToString(NumberFormatInfo.InvariantInfo));
		AddParameter(xmlElement3, Globals.ParamNameContingencies, PricesPolicy.Expenditures[0].CoefficientAsFactor.ToString(NumberFormatInfo.InvariantInfo));
		AddParameter(xmlElement3, Globals.ParamNameGeneralExpenditures, PricesPolicy.Expenditures[1].CoefficientAsFactor.ToString(NumberFormatInfo.InvariantInfo));
		AddParameter(xmlElement3, Globals.ParamNameFinancialExpenditures, PricesPolicy.Expenditures[2].CoefficientAsFactor.ToString(NumberFormatInfo.InvariantInfo));
		AddParameter(xmlElement3, Globals.ParamNameCommissions, PricesPolicy.Expenditures[3].CoefficientAsFactor.ToString(NumberFormatInfo.InvariantInfo));
		xmlElement3 = xmlDocument.CreateElement("cmd", "Command", Globals.PrefCADCommandNamespaceUri);
		xmlElement3.SetAttribute("name", "CreateDefaultPricePolicyTariffs");
		xmlElement.AppendChild(xmlElement3);
		if (PricesPolicy.IsPriceDetailLoaded)
		{
			foreach (PrefProjectPerGroupExpenditure perGroupExpenditure in PricesPolicy.PerGroupExpenditures)
			{
				if (perGroupExpenditure.StatusRemnantIncrement == enStatus.Modified)
				{
					xmlElement3 = xmlDocument.CreateElement("cmd", "Command", Globals.PrefCADCommandNamespaceUri);
					xmlElement3.SetAttribute("name", "UpdateProjectTariffException");
					AddParameter(xmlElement3, Globals.ParamNameProjectCode, ProjectCode.ToString());
					AddParameter(xmlElement3, Globals.ParamNameRedefinedTariffName, PricesPolicy.CostIncrementTariffName);
					AddParameter(xmlElement3, Globals.ParamNameGroupRowId, perGroupExpenditure.Group.RowId.ToString("D").ToUpper());
					AddParameter(xmlElement3, Globals.ParamNameValue, perGroupExpenditure.RemnantIncrementAsFactor.ToString(NumberFormatInfo.InvariantInfo));
					AddParameter(xmlElement3, Globals.ParamNameColor, perGroupExpenditure.Color);
					xmlElement.AppendChild(xmlElement3);
				}
				else if (perGroupExpenditure.StatusRemnantIncrement == enStatus.Deleted)
				{
					xmlElement3 = xmlDocument.CreateElement("cmd", "Command", Globals.PrefCADCommandNamespaceUri);
					xmlElement3.SetAttribute("name", "DeleteProjectTariffException");
					AddParameter(xmlElement3, Globals.ParamNameProjectCode, ProjectCode.ToString());
					AddParameter(xmlElement3, Globals.ParamNameRedefinedTariffName, PricesPolicy.CostIncrementTariffName);
					AddParameter(xmlElement3, Globals.ParamNameGroupRowId, perGroupExpenditure.Group.RowId.ToString("D").ToUpper());
					AddParameter(xmlElement3, Globals.ParamNameColor, perGroupExpenditure.Color);
					xmlElement.AppendChild(xmlElement3);
				}
				if (perGroupExpenditure.StatusProviderDiscount == enStatus.Modified)
				{
					xmlElement3 = xmlDocument.CreateElement("cmd", "Command", Globals.PrefCADCommandNamespaceUri);
					xmlElement3.SetAttribute("name", "UpdateProjectTariffException");
					AddParameter(xmlElement3, Globals.ParamNameProjectCode, ProjectCode.ToString());
					AddParameter(xmlElement3, Globals.ParamNameRedefinedTariffName, PricesPolicy.ProviderDiscountTariffName);
					AddParameter(xmlElement3, Globals.ParamNameGroupRowId, perGroupExpenditure.Group.RowId.ToString("D").ToUpper());
					AddParameter(xmlElement3, Globals.ParamNameValue, perGroupExpenditure.ProviderDiscountAsFactor.ToString(NumberFormatInfo.InvariantInfo));
					AddParameter(xmlElement3, Globals.ParamNameColor, perGroupExpenditure.Color);
					xmlElement.AppendChild(xmlElement3);
				}
				else if (perGroupExpenditure.StatusProviderDiscount == enStatus.Deleted)
				{
					xmlElement3 = xmlDocument.CreateElement("cmd", "Command", Globals.PrefCADCommandNamespaceUri);
					xmlElement3.SetAttribute("name", "DeleteProjectTariffException");
					AddParameter(xmlElement3, Globals.ParamNameProjectCode, ProjectCode.ToString());
					AddParameter(xmlElement3, Globals.ParamNameRedefinedTariffName, PricesPolicy.ProviderDiscountTariffName);
					AddParameter(xmlElement3, Globals.ParamNameGroupRowId, perGroupExpenditure.Group.RowId.ToString("D").ToUpper());
					AddParameter(xmlElement3, Globals.ParamNameColor, perGroupExpenditure.Color);
					xmlElement.AppendChild(xmlElement3);
				}
				if (perGroupExpenditure.StatusSalesIncrement == enStatus.Modified)
				{
					xmlElement3 = xmlDocument.CreateElement("cmd", "Command", Globals.PrefCADCommandNamespaceUri);
					xmlElement3.SetAttribute("name", "UpdateProjectTariffException");
					AddParameter(xmlElement3, Globals.ParamNameProjectCode, ProjectCode.ToString());
					AddParameter(xmlElement3, Globals.ParamNameRedefinedTariffName, PricesPolicy.SalesIncrementTariffName);
					AddParameter(xmlElement3, Globals.ParamNameGroupRowId, perGroupExpenditure.Group.RowId.ToString("D").ToUpper());
					AddParameter(xmlElement3, Globals.ParamNameValue, perGroupExpenditure.CoefficientAsFactor.ToString(NumberFormatInfo.InvariantInfo));
					AddParameter(xmlElement3, Globals.ParamNameColor, perGroupExpenditure.Color);
					xmlElement.AppendChild(xmlElement3);
				}
				else if (perGroupExpenditure.StatusSalesIncrement == enStatus.Deleted)
				{
					xmlElement3 = xmlDocument.CreateElement("cmd", "Command", Globals.PrefCADCommandNamespaceUri);
					xmlElement3.SetAttribute("name", "DeleteProjectTariffException");
					AddParameter(xmlElement3, Globals.ParamNameProjectCode, ProjectCode.ToString());
					AddParameter(xmlElement3, Globals.ParamNameRedefinedTariffName, PricesPolicy.SalesIncrementTariffName);
					AddParameter(xmlElement3, Globals.ParamNameGroupRowId, perGroupExpenditure.Group.RowId.ToString("D").ToUpper());
					AddParameter(xmlElement3, Globals.ParamNameColor, perGroupExpenditure.Color);
					xmlElement.AppendChild(xmlElement3);
				}
			}
		}
		foreach (Document listDeletedDocument in m_listDeletedDocuments)
		{
			xmlElement3 = xmlDocument.CreateElement("cmd", "Command", Globals.PrefCADCommandNamespaceUri);
			xmlElement3.SetAttribute("name", "RemoveProjectDocument");
			AddParameter(xmlElement3, Globals.ParamNameProjectCode, ProjectCode.ToString());
			AddParameter(xmlElement3, Globals.ParamNameDocId, listDeletedDocument.DocumentId.ToString("D").ToUpper());
			xmlElement.AppendChild(xmlElement3);
		}
		ServiceAgent serviceAgent = new ServiceAgent(Globals.ConnectionString);
		string outerXml = xmlDocument.OuterXml;
		bool flag = serviceAgent.ExecuteCommand(outerXml, ref strCommandResults, ref strErrors);
		if (flag)
		{
			serviceAgent.RemoveContactsOfProject(ProjectId);
			foreach (Contact listContact in m_listContacts)
			{
				serviceAgent.AddRelatedDocument2(ProjectCode, listContact.DocumentId.ToString(), Convert.ToInt32(DocumentTypes.Customer), string.Empty);
			}
			ResetModifiedFlags();
		}
		return flag;
	}

	private void AddParameter(XmlNode node, string name, string value)
	{
		XmlElement xmlElement = node.OwnerDocument.CreateElement("cmd", "Parameter", Globals.PrefCADCommandNamespaceUri);
		xmlElement.SetAttribute("name", name);
		xmlElement.SetAttribute("value", value);
		node.AppendChild(xmlElement);
	}

	protected void OnPropertyChanged(string propName)
	{
		if (this.PropertyChanged != null)
		{
			this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
		}
	}
}
