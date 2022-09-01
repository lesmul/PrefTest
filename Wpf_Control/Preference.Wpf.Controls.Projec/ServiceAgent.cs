using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows;
using System.Xml;
using Interop.PrefSales;
using Preference.Data.SqlClient;
using Preference.Diagnostics;
using Preference.Wpf.Controls.Projects.AppLogic;
using Preference.Wpf.Controls.Properties;

namespace Preference.Wpf.Controls.Projects;

public class ServiceAgent : IServiceAgent
{
	public static string ItemNameCode = "Code";

	public static string ItemNameColor = "Color";

	public static string ItemNameCustomer = "Customer";

	public static string ItemNameCurrency = "Currency";

	public static string ItemNameDBObject = "DBObject";

	public static string ItemNameDocument = "Document";

	public static string ItemNameGroup = "Group";

	public static string ItemNameLaborCost = "LaborCost";

	public static string ItemNameMaterialCost = "MaterialCost";

	public static string ItemNameName = "Name";

	public static string ItemNamePriceGroup = "PriceGroup";

	public static string ItemNameProviderName = "ProviderName";

	public static string ItemNamePurchaseGroup = "PurchaseGroup";

	public static string ItemNameRowId = "RowId";

	public static string ItemNameSymbol = "Symbol";

	public static string ParamNameAddressMain = "MainAddress";

	public static string ParamNameAddressShipping = "ShippingAddress";

	public static string ParamNameAddressBilling = "BillingAddress";

	public static string ParamNameCaption = "Caption";

	public static string ParamNameColor = "Color";

	public static string ParamNameCommissions = "Commissions";

	public static string ParamNameContactAddress = "Address";

	public static string ParamNameContactCity = "City";

	public static string ParamNameContactCompany = "Company";

	public static string ParamNameContactContactPerson = "ContactPerson";

	public static string ParamNameContactCountry = "Country";

	public static string ParamNameContactEmail = "Email";

	public static string ParamNameContactFax = "Fax";

	public static string ParamNameContactPostalCode = "PostalCode";

	public static string ParamNameContactProvince = "Province";

	public static string ParamNameContactTelephone = "Telephone";

	public static string ParamNameContingencies = "Contingencies";

	public static string ParamNameCurrencies = "Currencies";

	public static string ParamNameCustomerCode = "CustomerCode";

	public static string ParamNameCustomerName = "CustomerName";

	public static string ParamNameCustomers = "Customers";

	public static string ParamNameDBObjectList = "DBObjectList";

	public static string ParamNameDefaultObjectTypeCode = "DefaultObjectTypeCode";

	public static string ParamNameDestDocId = "DestDocumentId";

	public static string ParamNameDestDocType = "DestDocumentType";

	public static string ParamNameDocId = "DocumentId";

	public static string ParamNameDocType = "DocumentTypeCode";

	public static string ParamNameDocumentCode = "Code";

	public static string ParamNameDocumentList = "DocumentList";

	public static string ParamNameDocumentNumber = "Number";

	public static string ParamNameDocumentNumeration = "Numeration";

	public static string ParamNameDocumentSupplier = "Supplier";

	public static string ParamNameDocumentVersion = "Version";

	public static string ParamNameEntry = "Entry";

	public static string ParamNameExit = "Exit";

	public static string ParamNameException = "Exception";

	public static string ParamNameExceptions = "Exceptions";

	public static string ParamNameFinancialExpenditures = "FinancialExpenditures";

	public static string ParamNameFolderId = "FolderId";

	public static string ParamNameGroupRowId = "GroupRowId";

	public static string ParamNameGroups = "Groups";

	public static string ParamNameName = "Name";

	public static string ParamNamePriceGroupRowId = "PriceGroupRowId";

	public static string ParamNamePurchaseGroupRowId = "PurchaseGroupRowId";

	public static string ParamNameGeneralExpenditures = "GeneralExpenditures";

	public static string ParamNameGroupType = "GroupType";

	public static string ParamNameOrderNumber = "OrderNumber";

	public static string ParamNameParentFolderId = "ParentFolderId";

	public static string ParamNameProjectName = "ProjectName";

	public static string ParamNameProjectAgreedAmount = "AgreedAmount";

	public static string ParamNameProjectAgreedAmountCurrency = "AgreedAmountCurrency";

	public static string ParamNameProjectCode = "ProjectCode";

	public static string ParamNameProjectComments = "Comments";

	public static string ParamNameProjectCreationDate = "CreationDate";

	public static string ParamNameProjectCustomerAddress1 = "CustomerAddress1";

	public static string ParamNameProjectCustomerAddress2 = "CustomerAddress2";

	public static string ParamNameProjectCustomerCity = "CustomerCity";

	public static string ParamNameProjectCustomerCountry = "CustomerCountry";

	public static string ParamNameProjectCustomerPostalCode = "CustomerPostalCode";

	public static string ParamNameProjectCustomerProvince = "CustomerProvince";

	public static string ParamNameProjectDescription = "Description";

	public static string ParamNameProjectDiscount = "ProjectDiscount";

	public static string ParamNameProjectId = "ProjectId";

	public static string ParamNameProjectInvoicingAddress1 = "InvoicingAddress1";

	public static string ParamNameProjectInvoicingAddress2 = "InvoicingAddress2";

	public static string ParamNameProjectInvoicingCity = "InvoicingCity";

	public static string ParamNameProjectInvoicingCountry = "InvoicingCountry";

	public static string ParamNameProjectInvoicingPostalCode = "InvoicingPostalCode";

	public static string ParamNameProjectInvoicingProvince = "InvoicingProvince";

	public static string ParamNameProjectPricingComments = "PricingComments";

	public static string ParamNameProjectShippingAddress1 = "ShippingAddress1";

	public static string ParamNameProjectShippingAddress2 = "ShippingAddress2";

	public static string ParamNameProjectShippingCity = "ShippingCity";

	public static string ParamNameProjectShippingCountry = "ShippingCountry";

	public static string ParamNameProjectShippingPostalCode = "ShippingPostalCode";

	public static string ParamNameProjectShippingProvince = "ShippingProvince";

	public static string ParamNameRedefinedTariffName = "RedefinedTariffName";

	public static string ParamNameRelationshipType = "RelationshipType";

	public static string ParamNameShowWorkforceCost = "ShowWorkforceCost";

	public static string ParamNameSortOrder = "SortOrder";

	public static string ParamNameSourceId = "SourceId";

	public static string ParamNameSourceType = "SourceType";

	public static string ParamNameSrcFolderId = "SrcFolderId";

	public static string ParamNameTariffName = "TariffName";

	public static string ParamNameType = "Type";

	public static string ParamNameTypeName = "TypeName";

	public static string ParamNameValue = "Value";

	public static string ParamNameVersionName = "VersionName";

	public static string ProjectTariffSalesIncrement = "SalesIncrement";

	public static string ProjectTariffProviderDiscount = "ProviderDiscount";

	public static string ProjectTariffCostIncrement = "CostIncrement";

	public static string CommandNamespaceUri = "http://www.preference.com/XMLSchemas/2006/PrefCAD.Command";

	public static string MessageNamespaceUri = "http://www.preference.com/XMLSchemas/2007/PrefSuite.Messages";

	public static string PricesNamespaceUri = "PricesInput";

	public static long ParamProjectTypeCode = 36L;

	private string _strConnectionString = string.Empty;

	protected XmlNamespaceManager m_xmlNamespaceMgr;

	protected XmlDocument m_xmlCommand;

	protected XmlDocument m_xmlResults;

	protected XmlDocument m_xmlMessages;

	protected XmlElement m_rootMessages;

	protected XmlElement m_rootResults;

	public string AdoConnectionString
	{
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Expected O, but got Unknown
			SqlConnectionStringBuilder val = new SqlConnectionStringBuilder(value);
			_strConnectionString = val.get_ConnectionString();
		}
	}

	public ServiceAgent()
	{
	}

	public ServiceAgent(string connectionString)
	{
		_strConnectionString = connectionString;
	}

	public DataSet GetContactList()
	{
		string text = "SELECT\tt.rowId as DestDocumentId";
		text += " , v1.ContactId";
		text += " , v2.CompanyName";
		text += " , v1.FirstName +' '+ v1.LastName AS ContactName";
		text += " , p.Number";
		text += " , p2.Number As Fax";
		text += " , e.Email";
		text += " , v1.Address1+' '+v1.Address2 As Address";
		text += " , v1.PostalCode";
		text += " , v1.CityName";
		text += " , v1.ProvinceName";
		text += " ,v1.CountryName";
		text += " , t.rowid";
		text += " , v2.Roles";
		text += " FROM vwCMSContacts v1 ";
		text += " INNER JOIN vwCMSAccounts v2 ON v1.ContactId=v2.AccountId ";
		text += " INNER JOIN CMSAccounts t ON t.AccountId=v1.ContactId";
		text += " LEFT OUTER JOIN CMSPhones p ON t.AccountId=p.AccountId AND p.phoneRoleid <> 3";
		text += " LEFT OUTER JOIN CMSPhones p2 ON t.AccountId=p2.AccountId AND p2.phoneRoleid = 3";
		text += " LEFT OUTER JOIN CMSEmails e ON t.AccountId=e.AccountId ";
		return SqlHelper.ExecuteDataset(_strConnectionString, CommandType.Text, text);
	}

	public DataSet GetCustomerListOrderByCustomerName()
	{
		string text = "SELECT DISTINCT C.CodigoCliente AS CustomerCode";
		text += " , C.Nombre AS CustomerName";
		text += " , C.Domicilio AS MainAddress1";
		text += " , C.Domicilio2 AS MainAddress2";
		text += " , C.Localidad AS MainCity";
		text += " , C.CodigoPostal AS MainZipCode";
		text += " , C.Provincia AS MainProvince";
		text += " , C.Pais AS MainCountry";
		text += " , SA.Address1 AS ShippingAddress1";
		text += " , SA.Address2 AS ShippingAddress2";
		text += " , SA.City AS ShippingCity";
		text += " , SA.ZipCode AS ShippingZipCode";
		text += " , SA.Province AS ShippingProvince";
		text += " , SA.Country AS ShippingCountry";
		text += " , BA.Address1 AS BillingAddress1";
		text += " , BA.Address2 AS BillingAddress2";
		text += " , BA.City AS BillingCity";
		text += " , BA.ZipCode AS BillingZipCode";
		text += " , BA.Province AS BillingProvince";
		text += " , BA.Country AS BillingCountry";
		text += " FROM Clientes C";
		text += " LEFT OUTER JOIN AddressesCustomer SAC ON SAC.CustomerCode = C.CodigoCliente AND SAC.[Type] = 1";
		text += " LEFT OUTER JOIN Addresses SA ON SA.AddressId = SAC.AddressId";
		text += " LEFT OUTER JOIN AddressesCustomer BAC ON BAC.CustomerCode = C.CodigoCliente AND BAC.[Type] = 2";
		text += " LEFT OUTER JOIN Addresses BA ON BA.AddressId = BAC.AddressId";
		text += " ORDER BY C.Nombre";
		return SqlHelper.ExecuteDataset(_strConnectionString, CommandType.Text, text);
	}

	public DataSet GetSalesDocumentsOfProject(Guid projectId)
	{
		string text = "SELECT\tDRS.DestDocumentId";
		text += " ,\t\tDRS.DestDocumentType";
		text += " ,\t\tDRS.RelationshipType";
		text += " ,\t\tPAF.Type as PAF_Type";
		text += " ,\t\tPAF.Numero as PAF_Number";
		text += " ,\t\tPAF.Version as PAF_Version";
		text += " ,\t\tdbo.getstrdocumenttype(PAf.[Type]) as PAF_StrDocumentType";
		text += " ,\t\tPAF.NumeroPedido as PAF_OrderNumber";
		text += " ,\t\tPAF.NombreVersion as PAF_VersionName";
		text += " ,\t\tPAF.Nombre as PAF_Nombre";
		text += " FROM\tdbo.DocumentRelationships DRS";
		text += " INNER JOIN PAF ON PAF.RowId = DRS.DestDocumentId AND DRS.DestDocumentType= 1";
		text += $" WHERE\tDRS.SrcDocumentId='{projectId}' ";
		text += " AND\t\tDRS.SrcDocumentType=36";
		text += " AND\t\tDRS.DestDocumentType= 1";
		return SqlHelper.ExecuteDataset(_strConnectionString, CommandType.Text, text);
	}

	public DataSet GetCustomerDocumentsOfProject(Guid projectId)
	{
		string text = "SELECT\tDRS.DestDocumentId";
		text += " ,\t\tDRS.DestDocumentType";
		text += " ,\t\tDRS.RelationshipType";
		text += " , v1.ContactId";
		text += " , v2.CompanyName";
		text += " , v1.FirstName +' '+ v1.LastName AS ContactName";
		text += " , p.Number";
		text += " , p2.Number As Fax";
		text += " , e.Email";
		text += " , v1.Address1+' '+v1.Address2 As Address";
		text += " , v1.PostalCode";
		text += " , v1.CityName";
		text += " , v1.ProvinceName";
		text += " ,v1.CountryName";
		text += " , t.rowid";
		text += " , v2.Roles";
		text += " FROM vwCMSContacts v1 ";
		text += " INNER JOIN vwCMSAccounts v2 ON v1.ContactId=v2.AccountId ";
		text += " INNER JOIN CMSAccounts t ON t.AccountId=v1.ContactId";
		text += " INNER JOIN\tdbo.DocumentRelationships DRS ON DRS.DestDocumentId = t.RowId and DRS.DestDocumentType = 2";
		text += " LEFT OUTER JOIN CMSPhones p ON t.AccountId=p.AccountId AND p.phoneRoleid <> 3";
		text += " LEFT OUTER JOIN CMSPhones p2 ON t.AccountId=p2.AccountId AND p2.phoneRoleid = 3";
		text += " LEFT OUTER JOIN CMSEmails e ON t.AccountId=e.AccountId ";
		text += $"WHERE DRS.SrcDocumentId ='{projectId}'";
		text += " AND\t\tDRS.SrcDocumentType=36";
		return SqlHelper.ExecuteDataset(_strConnectionString, CommandType.Text, text);
	}

	public DataSet GetProductionLotDocumentsOfProject(Guid projectId)
	{
		string text = "SELECT\tDRS.DestDocumentId";
		text += " ,\t\tDRS.DestDocumentType";
		text += " ,\t\tDRS.RelationshipType";
		text += " ,\t\tOPT.Numero";
		text += " FROM\tdbo.DocumentRelationships DRS";
		text += " INNER JOIN Optimizaciones OPT ON OPT.DocumentId = DRS.DestDocumentId ";
		text += $" WHERE\tDRS.SrcDocumentId='{projectId}' ";
		text += " AND\t\tDRS.SrcDocumentType=36";
		text += " AND\t\tDRS.DestDocumentType= 3";
		return SqlHelper.ExecuteDataset(_strConnectionString, CommandType.Text, text);
	}

	public DataSet GetPurchasesDocumentsOfProject(Guid projectId)
	{
		string text = "SELECT\tDRS.DestDocumentId";
		text += " ,\t\tDRS.DestDocumentType";
		text += " ,\t\tDRS.RelationshipType";
		text += " ,\t\tPUR.Number AS PURCHASES_Number";
		text += " ,\t\tPUR.Numeration AS PURCHASES_Numeration";
		text += " ,\t\tPRO.Nombre AS PURCHASES_Nombre";
		text += " ,\t\tNUM.DocumentType AS PURCHASES_DocumentType";
		text += " ,\t\tdbo.getstrdocumenttype(NUM.DocumentType) AS PURCHASES_STRDocumentType";
		text += " FROM\tdbo.DocumentRelationships DRS";
		text += " INNER JOIN Purchases PUR ON PUR.DocumentId = DRS.DestDocumentId ";
		text += " INNER  JOIN Numeraciones NUM ON NUM.id = PUR.Numeration";
		text += " INNER join Proveedores PRO ON PRO.CodigoProveedor = PUR.ProviderCode";
		text += $" WHERE\tDRS.SrcDocumentId='{projectId}' ";
		text += " AND\t\tDRS.SrcDocumentType=36";
		text += " AND\t\tDRS.DestDocumentType= 4";
		return SqlHelper.ExecuteDataset(_strConnectionString, CommandType.Text, text);
	}

	public long AddNewProject(long projectCode, string projectName)
	{
		try
		{
			Guid empty = Guid.Empty;
			string empty2 = string.Empty;
			string text = "[dbo].[pa_Projects_NewProject]";
			SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(_strConnectionString, text, true);
			spParameterSet[1].Value = projectCode;
			spParameterSet[2].Value = empty;
			spParameterSet[3].Value = projectName;
			spParameterSet[4].Value = empty2;
			SqlHelper.ExecuteNonQuery(_strConnectionString, CommandType.StoredProcedure, text, spParameterSet);
			if ((int)spParameterSet[0].Value == 0)
			{
				return Convert.ToInt64(spParameterSet[1].Value);
			}
			return -1L;
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefGest");
			throw;
		}
	}

	public bool AddRelatedDocument2(long projectCode, string destDocumentId, int destDocumentType, string folderId)
	{
		try
		{
			string text = "[dbo].[pa_Projects_AddRelatedDocument2]";
			SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(_strConnectionString, text, false);
			spParameterSet[0].Value = projectCode;
			spParameterSet[1].Value = new Guid(destDocumentId);
			spParameterSet[2].Value = destDocumentType;
			spParameterSet[3].Value = DBNull.Value;
			if (!string.IsNullOrEmpty(folderId))
			{
				spParameterSet[3].Value = new Guid(folderId);
			}
			spParameterSet[4].Value = "<PrefMessages/>";
			int num = SqlHelper.ExecuteNonQuery(_strConnectionString, CommandType.StoredProcedure, text, spParameterSet);
			return true;
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefGest");
			throw;
		}
	}

	public bool ExistsProjectCode(long projectCode)
	{
		try
		{
			string text = $"SELECT ProjectCode FROM Projects WHERE ProjectCode={projectCode}";
			return SqlHelper.ExecuteDataset(_strConnectionString, CommandType.Text, text).Tables[0].Rows.Count > 0;
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefGest");
			throw;
		}
	}

	public long GetNextProjectCode()
	{
		try
		{
			string text = "SELECT ISNULL(MAX(ProjectCode),0)+1 FROM Projects";
			return Convert.ToInt64(SqlHelper.ExecuteScalar(_strConnectionString, CommandType.Text, text));
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefGest");
			throw;
		}
	}

	public long GetProjectFromRelatedDocument(string documentId, int destDocumentType)
	{
		try
		{
			string text = "[dbo].[pa_Projects_GetProjectFromRelatedDocument]";
			SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(_strConnectionString, text, false);
			spParameterSet[0].Value = new Guid(documentId);
			spParameterSet[1].Value = destDocumentType;
			spParameterSet[2].Value = DBNull.Value;
			spParameterSet[3].Value = DBNull.Value;
			spParameterSet[4].Value = "<PrefMessages/>";
			SqlHelper.ExecuteNonQuery(_strConnectionString, CommandType.StoredProcedure, text, spParameterSet);
			if (spParameterSet[3].Value != DBNull.Value)
			{
				return Convert.ToInt64(spParameterSet[3].Value);
			}
			return 0L;
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefGest");
			throw;
		}
	}

	public bool RemoveDocumentFromProject(string destDocumentId, long projectCode)
	{
		try
		{
			string text = $"DELETE FROM [dbo].[DocumentRelationships]\r\n                    FROM [dbo].[DocumentRelationships]\r\n                    INNER JOIN [dbo].[Projects] ON ProjectId = SrcDocumentId\r\n                    WHERE ProjectCode = {projectCode} AND DestDocumentId = '{destDocumentId}' AND SrcDocumentType = 36";
			return SqlHelper.ExecuteNonQuery(_strConnectionString, CommandType.Text, text) == 1;
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefGest");
			throw;
		}
	}

	public void RemoveContactsOfProject(Guid projectId)
	{
		try
		{
			string text = $"DELETE FROM [dbo].[DocumentRelationships]\r\n                    FROM [dbo].[DocumentRelationships]\r\n                    WHERE SrcDocumentId = '{projectId}' AND SrcDocumentType = 36 and DestDocumentType = 2";
			SqlHelper.ExecuteNonQuery(_strConnectionString, CommandType.Text, text);
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefGest");
			throw;
		}
	}

	public SalesDocument GetProjectSalesDocumentForValuation(Guid projectId, bool bSearchForValuatingDocument)
	{
		string text = "SELECT top 1 dr.DestDocumentId, dr.RelationshipType, p.Numero, p.Version";
		text += " FROM dbo.DocumentRelationships dr";
		text += " INNER JOIN PAF P ON p.RowId = dr.DestDocumentId";
		text += $" WHERE dr.SrcDocumentId='{projectId}' AND dr.SrcDocumentType=36 AND dr.DestDocumentType = 1 ";
		if (bSearchForValuatingDocument)
		{
			text += "AND dr.RelationshipType= 2";
		}
		DataSet dataSet = SqlHelper.ExecuteDataset(_strConnectionString, CommandType.Text, text);
		if (dataSet.Tables[0].Rows.Count > 0)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			SalesDocument salesDocument = new SalesDocument();
			salesDocument.RelationShipType = Convert.ToInt32(dataRow["RelationshipType"]);
			salesDocument.Number = Convert.ToInt32(dataRow["Numero"]);
			salesDocument.Version = Convert.ToInt32(dataRow["Version"]);
			salesDocument.DocumentId = new Guid(Convert.ToString(dataRow["DestDocumentId"]));
			salesDocument.DocumentType = DocumentTypes.SalesDocument;
			return salesDocument;
		}
		return null;
	}

	public bool TryGetFirstSalesDocumentForValuation(Guid projectId, out SalesDocument salesDoc)
	{
		salesDoc = GetProjectSalesDocumentForValuation(projectId, bSearchForValuatingDocument: true);
		if (salesDoc != null)
		{
			return true;
		}
		salesDoc = GetProjectSalesDocumentForValuation(projectId, bSearchForValuatingDocument: false);
		if (salesDoc != null)
		{
			return true;
		}
		return false;
	}

	private void AddError(string strInfo, string strExtraInfo)
	{
		XmlElement documentElement = m_xmlMessages.DocumentElement;
		XmlElement xmlElement = m_xmlMessages.CreateElement("pmsg", "Message", MessageNamespaceUri);
		documentElement.AppendChild(xmlElement);
		xmlElement.SetAttribute("Type", "Error");
		xmlElement.SetAttribute("Info", strInfo);
		xmlElement.SetAttribute("ExtraInfo", strExtraInfo);
		xmlElement.SetAttribute("Time", DateTime.Now.ToString("s"));
	}

	private void AppendMessages(string strXMLMessages)
	{
		m_xmlMessages.DocumentElement.InnerXml += strXMLMessages;
	}

	public bool ExecuteXMLCommand(XmlDocument xmlCommand, ref XmlDocument xmlResults, ref XmlDocument xmlErrors)
	{
		string strResults = "";
		string strErrors = "";
		if (ExecuteCommand(xmlCommand.OuterXml, ref strResults, ref strErrors))
		{
			xmlResults = new XmlDocument();
			xmlResults.LoadXml(strResults);
			xmlErrors = new XmlDocument();
			xmlErrors.LoadXml(strErrors);
			return true;
		}
		return false;
	}

	public bool ExecuteCommand(string strCommandXml, ref string strResults, ref string strErrors)
	{
		InitVariables();
		string text = string.Empty;
		if (!LoadXMLCommands(strCommandXml))
		{
			return false;
		}
		bool flag = false;
		try
		{
			XmlNode xmlNode = m_xmlCommand.SelectSingleNode("descendant::cmd:Connection", m_xmlNamespaceMgr);
			if (xmlNode != null)
			{
				SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder();
				sqlConnectionStringBuilder.DataSource = xmlNode.Attributes["server"].Value;
				sqlConnectionStringBuilder.InitialCatalog = xmlNode.Attributes["database"].Value;
				sqlConnectionStringBuilder.IntegratedSecurity = Convert.ToBoolean(xmlNode.Attributes["trustedConnection"].Value);
				if (!sqlConnectionStringBuilder.IntegratedSecurity)
				{
					sqlConnectionStringBuilder.UserID = xmlNode.Attributes["user"].Value;
					sqlConnectionStringBuilder.Password = xmlNode.Attributes["password"].Value;
				}
				_strConnectionString = sqlConnectionStringBuilder.ConnectionString;
			}
		}
		catch (Exception ex)
		{
			AddError("Passed Connection Failed! :" + ex.Message, ex.Source);
			flag = true;
		}
		try
		{
			XmlNodeList xmlNodeList = m_xmlCommand.SelectNodes("descendant::cmd:Command", m_xmlNamespaceMgr);
			foreach (XmlNode item in xmlNodeList)
			{
				text = item.Attributes["name"].Value;
				switch (text)
				{
				case "AddFolderToProject":
					if (!Command_AddFolderToProject(item))
					{
						flag = true;
					}
					break;
				case "AddNewProject":
					if (!Command_AddNewProject(item))
					{
						flag = true;
					}
					break;
				case "CheckNewProject":
					if (!Command_CheckNewProject(item))
					{
						flag = true;
					}
					break;
				case "CreateDefaultPricePolicyTariffs":
					if (!Command_CreateDefaultPricePolicyTariffs(item))
					{
						flag = true;
					}
					break;
				case "DeleteProjectTariffException":
					if (!Command_DeleteProjectTariffException(item))
					{
						flag = true;
					}
					break;
				case "ExistsProjectCode":
					if (!Command_ExistsProjectCode(item))
					{
						flag = true;
					}
					break;
				case "GetAllGroupsOfType":
					if (!Command_GetAllGroupsOfType(item))
					{
						flag = true;
					}
					break;
				case "GetAllProjects":
					if (!Command_GetAllProjects(item))
					{
						flag = true;
					}
					break;
				case "GetContactDetails":
					if (!Command_GetContactDetails(item))
					{
						flag = true;
					}
					break;
				case "GetCurrencies":
					if (!Command_GetCurrencies(item))
					{
						flag = true;
					}
					break;
				case "GetNextProjectCode":
					if (!Command_GetNextProjectCode(item))
					{
						flag = true;
					}
					break;
				case "GetPriceGroups":
					if (!Command_GetPriceGroups(item))
					{
						flag = true;
					}
					break;
				case "GetProductionLotDocumentDetails":
					if (!Command_GetProductionLotDocumentDetails(item))
					{
						flag = true;
					}
					break;
				case "GetShippingLotDocumentDetails":
					if (!Command_GetShippingLotDocumentDetails(item))
					{
						flag = true;
					}
					break;
				case "GetProjectDetails":
					if (!Command_GetProjectDetails(item))
					{
						flag = true;
					}
					break;
				case "GetProjectFromDocument":
					if (!Command_GetProjectFromDocument(item))
					{
						flag = true;
					}
					break;
				case "GetProjectDocuments":
					if (!Command_GetProjectDocuments(item))
					{
						flag = true;
					}
					break;
				case "GetProjectFolders":
					if (!Command_GetProjectFolders(item))
					{
						flag = true;
					}
					break;
				case "GetProjectPricePolicy":
					if (!Command_GetProjectPricePolicy(item))
					{
						flag = true;
					}
					break;
				case "GetProjectStatus":
					if (!Command_GetProjectStatus(item))
					{
						flag = true;
					}
					break;
				case "GetPurchaseDocumentDetails":
					if (!Command_GetPurchaseDocumentDetails(item))
					{
						flag = true;
					}
					break;
				case "GetSalesDocumentDetails":
					if (!Command_GetSalesDocumentDetails(item))
					{
						flag = true;
					}
					break;
				case "GetSalesDocumentPerGroupLabourCosts":
					if (!Command_GetSalesDocumentPerGroupLabourCosts(item))
					{
						flag = true;
					}
					break;
				case "GetSalesDocumentPerGroupMaterialCosts":
					if (!Command_GetSalesDocumentPerGroupMaterialCosts(item))
					{
						flag = true;
					}
					break;
				case "GetSalesDocumentPriceGroupDetails":
					if (!Command_GetSalesDocumentPriceGroupDetails(item))
					{
						flag = true;
					}
					break;
				case "GetServiceInformation":
					if (!Command_GetServiceInformation(item))
					{
						flag = true;
					}
					break;
				case "GetWarehouseDocumentDetails":
					if (!Command_GetWarehouseDocumentDetails(item))
					{
						flag = true;
					}
					break;
				case "RemoveProject":
					if (!Command_RemoveProject(item))
					{
						flag = true;
					}
					break;
				case "RemoveProjectFolder":
					if (!Command_RemoveProjectFolder(item))
					{
						flag = true;
					}
					break;
				case "RemoveProjectDocument":
					if (!Command_RemoveProjectDocument(item))
					{
						flag = true;
					}
					break;
				case "RenameProjectFolder":
					if (!Command_RenameProjectFolder(item))
					{
						flag = true;
					}
					break;
				case "SQLCommand":
					if (!Command_SQLCommand(item))
					{
						flag = true;
					}
					break;
				case "UpdateProjectDetails":
					if (!Command_UpdateProjectDetails(item))
					{
						flag = true;
					}
					break;
				case "UpdateProjectTariffException":
					if (!Command_UpdateProjectTariffException(item))
					{
						flag = true;
					}
					break;
				default:
					AddError("Command '" + text + "' not recognised", "Review the command name sent to the service");
					flag = true;
					break;
				}
			}
		}
		catch (Exception ex2)
		{
			AddError(text + " Failed! :" + ex2.Message, ex2.Source);
			flag = true;
		}
		strResults = m_xmlResults.OuterXml;
		strErrors = m_xmlMessages.OuterXml;
		return !flag;
	}

	private SqlCommand BuildSQLCommand(string strQuery, CommandType ctType)
	{
		SqlCommand sqlCommand = null;
		try
		{
			SqlConnection sqlConnection = new SqlConnection(_strConnectionString);
			sqlConnection.Open();
			sqlCommand = new SqlCommand(strQuery, sqlConnection);
			sqlCommand.CommandType = ctType;
			sqlCommand.Parameters.Clear();
			sqlCommand.CommandTimeout = 300;
			return sqlCommand;
		}
		catch (Exception ex)
		{
			AddError(ex.Message, ex.Source);
			return null;
		}
	}

	private bool Command_AddFolderToProject(XmlNode NodeCommand)
	{
		bool result = false;
		XmlNode xmlNode = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameProjectCode + "\"]", m_xmlNamespaceMgr);
		string text = ((xmlNode != null) ? xmlNode.Attributes["value"].Value : string.Empty);
		xmlNode = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameProjectId + "\"]", m_xmlNamespaceMgr);
		string text2 = ((xmlNode != null) ? xmlNode.Attributes["value"].Value : string.Empty);
		xmlNode = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameFolderId + "\"]", m_xmlNamespaceMgr);
		string text3 = ((xmlNode != null) ? xmlNode.Attributes["value"].Value : string.Empty);
		xmlNode = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameParentFolderId + "\"]", m_xmlNamespaceMgr);
		string text4 = ((xmlNode != null) ? xmlNode.Attributes["value"].Value : string.Empty);
		xmlNode = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameDefaultObjectTypeCode + "\"]", m_xmlNamespaceMgr);
		string text5 = ((xmlNode != null) ? xmlNode.Attributes["value"].Value : string.Empty);
		xmlNode = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameSortOrder + "\"]", m_xmlNamespaceMgr);
		string text6 = ((xmlNode != null) ? xmlNode.Attributes["value"].Value : string.Empty);
		xmlNode = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameCaption + "\"]", m_xmlNamespaceMgr);
		string text7 = ((xmlNode != null) ? xmlNode.Attributes["value"].Value : string.Empty);
		if (string.IsNullOrEmpty(text7) || string.Compare(text7, "null", ignoreCase: true) == 0)
		{
			text7 = "New Folder";
		}
		if (string.IsNullOrEmpty(text6) || string.Compare(text6, "null", ignoreCase: true) == 0)
		{
			text6 = "1";
		}
		if (string.IsNullOrEmpty(text5) || string.Compare(text5, "null", ignoreCase: true) == 0)
		{
			text5 = "NULL";
		}
		if (string.IsNullOrEmpty(text3))
		{
			text3 = Guid.NewGuid().ToString("D").ToUpper();
		}
		text4 = ((!string.IsNullOrEmpty(text4) && string.Compare(text4, "null", ignoreCase: true) != 0 && string.Compare(text4, Guid.Empty.ToString("D"), ignoreCase: true) != 0) ? ("'" + text4 + "'") : "NULL");
		if (string.IsNullOrEmpty(text) && string.IsNullOrEmpty(text2))
		{
			return false;
		}
		string strQuery = $"INSERT INTO [ObjectFolders]\r\n( FolderId, ParentFolderId,ObjectId,ObjectTypeCode,\r\nCaption,SortOrder,DefaultObjectTypeCode )\r\nSELECT '{text3}', {text4}, '{text2}', 36, N'{text7}', {text6}, {text5}";
		if (!string.IsNullOrEmpty(text) && string.IsNullOrEmpty(text2))
		{
			strQuery = $"INSERT INTO [ObjectFolders]\r\n( FolderId, ParentFolderId,ObjectId,ObjectTypeCode,\r\nCaption,SortOrder,DefaultObjectTypeCode )\r\nSELECT '{text3}', {text4}, p.ProjectId, 36, N'{text7}', {text6}, {text5}  FROM [dbo].Projects p WHERE p.ProjectCode = {text}";
		}
		SqlCommand sqlCommand = BuildSQLCommand(strQuery, CommandType.Text);
		int num = sqlCommand.ExecuteNonQuery();
		if (num > 0)
		{
			xmlNode = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameFolderId + "\"]", m_xmlNamespaceMgr);
			if (xmlNode != null)
			{
				xmlNode.Attributes["value"].Value = text3;
			}
			else
			{
				AddParameter(NodeCommand, ParamNameFolderId, text3);
			}
			xmlNode = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameParentFolderId + "\"]", m_xmlNamespaceMgr);
			if (xmlNode != null)
			{
				xmlNode.Attributes["value"].Value = text4;
			}
			else
			{
				AddParameter(NodeCommand, ParamNameParentFolderId, text4);
			}
			xmlNode = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameDefaultObjectTypeCode + "\"]", m_xmlNamespaceMgr);
			if (xmlNode != null)
			{
				xmlNode.Attributes["value"].Value = text5;
			}
			else
			{
				AddParameter(NodeCommand, ParamNameDefaultObjectTypeCode, text5);
			}
			xmlNode = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameSortOrder + "\"]", m_xmlNamespaceMgr);
			if (xmlNode != null)
			{
				xmlNode.Attributes["value"].Value = text6;
			}
			else
			{
				AddParameter(NodeCommand, ParamNameSortOrder, text6);
			}
			xmlNode = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameCaption + "\"]", m_xmlNamespaceMgr);
			if (xmlNode != null)
			{
				xmlNode.Attributes["value"].Value = text7;
			}
			else
			{
				AddParameter(NodeCommand, ParamNameCaption, text7);
			}
			result = true;
			m_rootResults.InnerXml += NodeCommand.OuterXml.Replace("cmd:Command", "cmd:CommandResult");
		}
		if (sqlCommand.Connection.State != 0)
		{
			sqlCommand.Connection.Close();
		}
		return result;
	}

	private bool Command_AddNewProject(XmlNode NodeCommand)
	{
		string strProjectName = string.Empty;
		long nProjectCode = 0L;
		Guid idProject = Guid.Empty;
		if (!GetCommandParams_AddNewProject(NodeCommand, ref strProjectName, ref nProjectCode, ref idProject))
		{
			return false;
		}
		string strQuery = "[dbo].[pa_Projects_NewProject]";
		SqlCommand sqlCommand = BuildSQLCommand(strQuery, CommandType.StoredProcedure);
		SqlParameter sqlParameter = sqlCommand.Parameters.Add("RETURN_VALUE", SqlDbType.Int);
		sqlParameter.Direction = ParameterDirection.ReturnValue;
		SqlParameter sqlParameter2 = sqlCommand.Parameters.Add("@nProjectCode", SqlDbType.BigInt);
		sqlParameter2.Direction = ParameterDirection.InputOutput;
		if (nProjectCode > 0)
		{
			sqlParameter2.Value = nProjectCode;
		}
		SqlParameter sqlParameter3 = sqlCommand.Parameters.Add("@guidProjectId", SqlDbType.UniqueIdentifier);
		sqlParameter3.Direction = ParameterDirection.InputOutput;
		if (idProject != Guid.Empty)
		{
			sqlParameter3.Value = idProject;
		}
		sqlParameter = sqlCommand.Parameters.Add("@strProjectName", SqlDbType.NVarChar, 60);
		sqlParameter.Value = strProjectName;
		sqlParameter.Direction = ParameterDirection.Input;
		sqlParameter = sqlCommand.Parameters.Add("@xmlMessages", SqlDbType.Xml, 4000);
		sqlParameter.Direction = ParameterDirection.Output;
		sqlCommand.ExecuteNonQuery();
		int num = (int)sqlCommand.Parameters["RETURN_VALUE"].Value;
		nProjectCode = (long)sqlCommand.Parameters["@nProjectCode"].Value;
		idProject = (Guid)sqlCommand.Parameters["@guidProjectId"].Value;
		if (sqlCommand.Parameters["@xmlMessages"].Value != DBNull.Value)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(sqlCommand.Parameters["@xmlMessages"].Value.ToString());
			AppendMessages(xmlDocument.DocumentElement.InnerXml);
		}
		if (num == 0)
		{
			XmlNode oldChild = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameProjectName + "\"]", m_xmlNamespaceMgr);
			NodeCommand.RemoveChild(oldChild);
			if (sqlParameter3.Value != DBNull.Value)
			{
				idProject = (Guid)sqlParameter3.Value;
			}
			if (sqlParameter2.Value != DBNull.Value)
			{
				nProjectCode = (long)sqlParameter2.Value;
			}
			oldChild = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameProjectId + "\"]", m_xmlNamespaceMgr);
			if (oldChild == null)
			{
				XmlElement xmlElement = m_xmlCommand.CreateElement("cmd", "Parameter", CommandNamespaceUri);
				xmlElement.SetAttribute("name", ParamNameProjectId);
				xmlElement.SetAttribute("type", "string");
				xmlElement.SetAttribute("value", idProject.ToString("D").ToUpper());
				NodeCommand.AppendChild(xmlElement);
			}
			else
			{
				oldChild.Attributes["value"].Value = idProject.ToString("D").ToUpper();
			}
			oldChild = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameProjectCode + "\"]", m_xmlNamespaceMgr);
			if (oldChild == null)
			{
				XmlElement xmlElement2 = m_xmlCommand.CreateElement("cmd", "Parameter", CommandNamespaceUri);
				xmlElement2.SetAttribute("name", ParamNameProjectCode);
				xmlElement2.SetAttribute("type", "int64");
				xmlElement2.SetAttribute("value", nProjectCode.ToString());
				NodeCommand.AppendChild(xmlElement2);
			}
			else
			{
				oldChild.Attributes["value"].Value = nProjectCode.ToString();
			}
			m_rootResults.InnerXml += NodeCommand.OuterXml.Replace("cmd:Command", "cmd:CommandResult");
		}
		if (sqlCommand.Connection.State != 0)
		{
			sqlCommand.Connection.Close();
		}
		return true;
	}

	private bool Command_CheckNewProject(XmlNode NodeCommand)
	{
		string strProjectName = string.Empty;
		long nProjectCode = 0L;
		Guid idProject = Guid.Empty;
		XmlNode xmlNode = null;
		if (!GetCommandParams_AddNewProject(NodeCommand, ref strProjectName, ref nProjectCode, ref idProject))
		{
			return false;
		}
		string empty = string.Empty;
		string strQuery = "SELECT ProjectId, ProjectCode, [Name] FROM Projects WHERE ProjectId='" + idProject.ToString() + "' OR Name='" + strProjectName + "' OR ProjectCode=" + nProjectCode;
		SqlCommand sqlCommand = BuildSQLCommand(strQuery, CommandType.Text);
		SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
		if (sqlDataReader.Read())
		{
			if (!sqlDataReader.IsDBNull(0))
			{
				idProject = sqlDataReader.GetGuid(0);
				xmlNode = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameProjectId + "\"]", m_xmlNamespaceMgr);
				if (xmlNode == null)
				{
					AddElementToNodeWithAttributes("Parameter", NodeCommand, ParamNameProjectId, "string", idProject.ToString("D").ToUpper());
				}
				else
				{
					xmlNode.Attributes["value"].Value = idProject.ToString("D").ToUpper();
				}
			}
			if (!sqlDataReader.IsDBNull(1))
			{
				nProjectCode = sqlDataReader.GetInt64(1);
				xmlNode = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameProjectCode + "\"]", m_xmlNamespaceMgr);
				if (xmlNode == null)
				{
					AddElementToNodeWithAttributes("Parameter", NodeCommand, ParamNameProjectCode, "long", nProjectCode.ToString());
				}
				else
				{
					xmlNode.Attributes["value"].Value = nProjectCode.ToString();
				}
			}
			if (!sqlDataReader.IsDBNull(2))
			{
				strProjectName = sqlDataReader.GetString(2);
				xmlNode = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameProjectName + "\"]", m_xmlNamespaceMgr);
				if (xmlNode == null)
				{
					AddElementToNodeWithAttributes("Parameter", NodeCommand, ParamNameProjectName, "string", strProjectName);
				}
				else
				{
					xmlNode.Attributes["value"].Value = nProjectCode.ToString();
				}
			}
		}
		else
		{
			NodeCommand.InnerXml = "";
		}
		m_rootResults.InnerXml += NodeCommand.OuterXml.Replace("cmd:Command", "cmd:CommandResult");
		if (sqlCommand.Connection.State != 0)
		{
			sqlCommand.Connection.Close();
		}
		return true;
	}

	private bool Command_CreateDefaultPricePolicyTariffs(XmlNode NodeCommand)
	{
		string text = Guid.NewGuid().ToString("D").ToUpper();
		string text2 = Guid.NewGuid().ToString("D").ToUpper();
		string text3 = Guid.NewGuid().ToString("D").ToUpper();
		string strQuery = "\r\n\t\t\tIF NOT EXISTS ( SELECT RowId FROM [dbo].Tariff WHERE [Name]=N'" + ProjectTariffCostIncrement + "' AND [Type]=0 )\r\n\t\t\t    INSERT INTO [dbo].Tariff ( RowId, [Name], [Type], [Currency] )\r\n\t\t\t    SELECT TOP (1) '" + text3 + "', N'" + ProjectTariffCostIncrement + "', 0, [Nombre] FROM [dbo].Monedas WHERE [Relacion] = 1\r\n\t\t\tIF NOT EXISTS ( SELECT RowId FROM [dbo].Tariff WHERE [Name]=N'" + ProjectTariffProviderDiscount + "' AND [Type]=0 )\r\n\t\t\t    INSERT INTO [dbo].Tariff ( RowId, [Name], [Type], [Currency] )\r\n\t\t\t    SELECT TOP (1) '" + text + "', N'" + ProjectTariffProviderDiscount + "', 0, [Nombre] FROM [dbo].Monedas WHERE [Relacion] = 1\r\n\t\t\tIF NOT EXISTS ( SELECT RowId FROM [dbo].Tariff WHERE [Name]=N'" + ProjectTariffSalesIncrement + "' AND [Type]=0 )\r\n\t\t\t    INSERT INTO [dbo].Tariff ( RowId, [Name], [Type], [Currency] )\r\n\t\t\t    SELECT TOP (1) '" + text2 + "',N'" + ProjectTariffSalesIncrement + "', 0, [Nombre] FROM [dbo].Monedas WHERE [Relacion] = 1\r\n\r\n\t\t\tIF NOT EXISTS ( SELECT TariffRowId FROM [dbo].TariffsContent tc INNER JOIN Tariff t ON t.RowId = tc.TariffRowId WHERE t.[Name]=N'" + ProjectTariffCostIncrement + "' AND t.[Type]=0 )\r\n\t\t\t\tINSERT INTO [dbo].TariffsContent ( [Type], TariffRowId, [Value] )\r\n\t\t\t\tSELECT 11, '" + text3 + "', 1\r\n\t\t\tIF NOT EXISTS ( SELECT TariffRowId FROM [dbo].TariffsContent tc INNER JOIN Tariff t ON t.RowId = tc.TariffRowId WHERE t.[Name]=N'" + ProjectTariffProviderDiscount + "' AND t.[Type]=0 )\r\n\t\t\t\tINSERT INTO [dbo].TariffsContent ( [Type], TariffRowId, [Value] )\r\n\t\t\t\tSELECT 11, '" + text + "', 1\r\n\t\t\tIF NOT EXISTS ( SELECT TariffRowId FROM [dbo].TariffsContent tc INNER JOIN Tariff t ON t.RowId = tc.TariffRowId WHERE t.[Name]=N'" + ProjectTariffSalesIncrement + "' AND t.[Type]=0)\r\n\t\t\t\tINSERT INTO [dbo].TariffsContent ( [Type], TariffRowId, [Value] )\r\n\t\t\t\tSELECT 11, '" + text2 + "', 1\r\n\t\t\t";
		SqlCommand sqlCommand = BuildSQLCommand(strQuery, CommandType.Text);
		int num = sqlCommand.ExecuteNonQuery();
		if (sqlCommand.Connection.State != 0)
		{
			sqlCommand.Connection.Close();
		}
		m_rootResults.InnerXml += NodeCommand.OuterXml.Replace("cmd:Command", "cmd:CommandResult");
		return true;
	}

	private bool Command_DeleteProjectTariffException(XmlNode NodeCommand)
	{
		string value = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameProjectCode + "\"]", m_xmlNamespaceMgr).Attributes["value"].Value;
		string value2 = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameRedefinedTariffName + "\"]", m_xmlNamespaceMgr).Attributes["value"].Value;
		string value3 = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameGroupRowId + "\"]", m_xmlNamespaceMgr).Attributes["value"].Value;
		string text = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameColor + "\"]", m_xmlNamespaceMgr).Attributes["value"].Value.Replace("'", "''");
		string text2;
		string text3;
		if (text != "")
		{
			text2 = "";
			text3 = "INNER JOIN Colores c ON c.RowId = tc.ColorRowId AND c.Nombre = N'" + text + "' ";
		}
		else
		{
			text2 = "AND (tc.ColorRowId IS NULL) ";
			text3 = "";
		}
		string strQuery = "\r\nDELETE FROM TariffsContent \r\nFROM TariffsContent tc \r\nINNER JOIN Tariff t ON tc.TariffRowId = t.RowId \r\nINNER JOIN Tariff t2 ON tc.RedefinedTariffRowId = t2.RowId\r\nINNER JOIN Groups g ON tc.PriceGroupRowId = g.RowId \r\nWHERE t.[Name]='" + value + "' \r\nAND t.[Type]=10 \r\nAND (tc.[Type]=11 OR tc.[Type]=10) \r\nAND t2.[Name]='" + value2 + "'\r\nAND (tc.PurchaseGroupRowId = '" + value3 + "'\r\nOR tc.PriceGroupRowId = '" + value3 + "')\r\nDELETE FROM TariffsContent \r\nFROM TariffsContent tc \r\nINNER JOIN Tariff t ON tc.TariffRowId = t.RowId \r\nINNER JOIN Tariff t2 ON tc.RedefinedTariffRowId = t2.RowId\r\nINNER JOIN PurchaseGroups g ON tc.PurchaseGroupRowId = g.RowId \r\n" + text3 + "\r\nWHERE t.[Name]='" + value + "' \r\nAND t.[Type]=10 \r\nAND (tc.[Type]=11 OR tc.[Type]=10) \r\nAND t2.[Name]='" + value2 + "'\r\nAND (tc.PurchaseGroupRowId = '" + value3 + "'\r\nOR tc.PriceGroupRowId = '" + value3 + "') " + text2 + "\r\n";
		SqlCommand sqlCommand = BuildSQLCommand(strQuery, CommandType.Text);
		sqlCommand.ExecuteNonQuery();
		if (sqlCommand.Connection.State != 0)
		{
			sqlCommand.Connection.Close();
		}
		return true;
	}

	private bool Command_ExistsProjectCode(XmlNode NodeCommand)
	{
		bool flag = false;
		XmlNode xmlNode = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameProjectCode + "\"]", m_xmlNamespaceMgr);
		if (xmlNode == null)
		{
			AddError("Missing Parameter ProjectCode", "ExistsProjectCode");
			return false;
		}
		string value = xmlNode.Attributes["value"].Value;
		string strQuery = "SELECT ProjectCode FROM Projects WHERE ProjectCode=" + value;
		SqlCommand sqlCommand = BuildSQLCommand(strQuery, CommandType.Text);
		SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
		if (sqlDataReader.Read())
		{
			AddElementToNodeWithAttributes("Parameter", NodeCommand, "Exists", "bool", "true");
		}
		else
		{
			AddElementToNodeWithAttributes("Parameter", NodeCommand, "Exists", "bool", "false");
		}
		flag = true;
		if (sqlCommand.Connection.State != 0)
		{
			sqlCommand.Connection.Close();
		}
		if (flag)
		{
			m_rootResults.InnerXml += NodeCommand.OuterXml.Replace("cmd:Command", "cmd:CommandResult");
		}
		return flag;
	}

	private bool Command_GetAllProjects(XmlNode NodeCommand)
	{
		XmlElement xmlElement = NodeCommand.OwnerDocument.CreateElement("cmd", "Parameter", CommandNamespaceUri);
		xmlElement.SetAttribute("name", "ProjectList");
		xmlElement.SetAttribute("type", "list");
		NodeCommand.AppendChild(xmlElement);
		string cmdText = "SELECT [ProjectCode] FROM [dbo].Projects";
		bool flag = false;
		SqlConnection sqlConnection = new SqlConnection(_strConnectionString);
		sqlConnection.Open();
		SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection);
		sqlCommand.CommandType = CommandType.Text;
		SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
		while (sqlDataReader.Read())
		{
			string value = sqlDataReader.GetValue(0).ToString();
			XmlElement xmlElement2 = NodeCommand.OwnerDocument.CreateElement("cmd", "Item", CommandNamespaceUri);
			xmlElement2.SetAttribute("name", ParamNameProjectCode);
			xmlElement2.SetAttribute("type", "long");
			xmlElement2.SetAttribute("value", value);
			xmlElement.AppendChild(xmlElement2);
			flag = true;
		}
		if (sqlCommand.Connection.State != 0)
		{
			sqlCommand.Connection.Close();
		}
		if (flag)
		{
			m_rootResults.InnerXml += NodeCommand.OuterXml.Replace("cmd:Command", "cmd:CommandResult");
		}
		return flag;
	}

	private bool Command_GetContactDetails(XmlNode NodeCommand)
	{
		XmlNode xmlNode = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameDocId + "\"]", m_xmlNamespaceMgr);
		XmlNode xmlNode2 = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameDocumentCode + "\"]", m_xmlNamespaceMgr);
		if (xmlNode == null && xmlNode2 == null)
		{
			AddError("Missing Parameter DocumentID or DocumentCode", "GetContactDetails");
			return false;
		}
		string text = string.Empty;
		string text2 = "SELECT TOP (1) v1.ContactId, \r\nv2.CompanyName,v1.FirstName +' '+ v1.LastName AS ContactName,p.Number,p2.Number As Fax,e.Email,\r\nv1.Address1+' '+v1.Address2 As Address, v1.PostalCode, v1.CityName, v1.ProvinceName,v1.CountryName, t.rowid, v2.Roles \r\nFROM vwCMSContacts v1 \r\nINNER JOIN vwCMSAccounts v2 ON v1.ContactId=v2.AccountId \r\nINNER JOIN CMSAccounts t ON t.AccountId=v1.ContactId\r\nLEFT OUTER JOIN CMSPhones p ON t.AccountId=p.AccountId AND p.phoneRoleid <> 3\r\nLEFT OUTER JOIN CMSPhones p2 ON t.AccountId=p2.AccountId AND p2.phoneRoleid = 3\r\nLEFT OUTER JOIN CMSEmails e ON t.AccountId=e.AccountId ";
		if (xmlNode != null)
		{
			text = "WHERE t.Rowid ='" + xmlNode.Attributes["value"].Value + "'";
		}
		else if (xmlNode2 != null)
		{
			text = "WHERE v1.ContactId =" + xmlNode2.Attributes["value"].Value;
		}
		bool flag = false;
		text2 += text;
		SqlCommand sqlCommand = BuildSQLCommand(text2, CommandType.Text);
		SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
		while (sqlDataReader.Read())
		{
			string value = sqlDataReader.GetValue(0).ToString().TrimEnd();
			string value2 = sqlDataReader.GetValue(1).ToString().TrimEnd();
			string value3 = sqlDataReader.GetValue(2).ToString().TrimEnd();
			string value4 = sqlDataReader.GetValue(3).ToString().TrimEnd();
			string value5 = sqlDataReader.GetValue(4).ToString().TrimEnd();
			string value6 = sqlDataReader.GetValue(5).ToString().TrimEnd();
			string value7 = sqlDataReader.GetValue(6).ToString().TrimEnd();
			string value8 = sqlDataReader.GetValue(7).ToString().TrimEnd();
			string value9 = sqlDataReader.GetValue(8).ToString().TrimEnd();
			string value10 = sqlDataReader.GetValue(9).ToString().TrimEnd();
			string value11 = sqlDataReader.GetValue(10).ToString().TrimEnd();
			string value12 = sqlDataReader.GetValue(11).ToString().TrimEnd();
			XmlElement xmlElement = NodeCommand.OwnerDocument.CreateElement("cmd", "Parameter", CommandNamespaceUri);
			xmlElement.SetAttribute("name", ParamNameDocumentCode);
			xmlElement.SetAttribute("type", "int");
			xmlElement.SetAttribute("value", value);
			NodeCommand.AppendChild(xmlElement);
			xmlElement = NodeCommand.OwnerDocument.CreateElement("cmd", "Parameter", CommandNamespaceUri);
			xmlElement.SetAttribute("name", ParamNameContactCompany);
			xmlElement.SetAttribute("type", "string");
			xmlElement.SetAttribute("value", value2);
			NodeCommand.AppendChild(xmlElement);
			xmlElement = NodeCommand.OwnerDocument.CreateElement("cmd", "Parameter", CommandNamespaceUri);
			xmlElement.SetAttribute("name", ParamNameContactContactPerson);
			xmlElement.SetAttribute("type", "string");
			xmlElement.SetAttribute("value", value3);
			NodeCommand.AppendChild(xmlElement);
			xmlElement = NodeCommand.OwnerDocument.CreateElement("cmd", "Parameter", CommandNamespaceUri);
			xmlElement.SetAttribute("name", ParamNameContactTelephone);
			xmlElement.SetAttribute("type", "string");
			xmlElement.SetAttribute("value", value4);
			NodeCommand.AppendChild(xmlElement);
			xmlElement = NodeCommand.OwnerDocument.CreateElement("cmd", "Parameter", CommandNamespaceUri);
			xmlElement.SetAttribute("name", ParamNameContactFax);
			xmlElement.SetAttribute("type", "string");
			xmlElement.SetAttribute("value", value5);
			NodeCommand.AppendChild(xmlElement);
			xmlElement = NodeCommand.OwnerDocument.CreateElement("cmd", "Parameter", CommandNamespaceUri);
			xmlElement.SetAttribute("name", ParamNameContactEmail);
			xmlElement.SetAttribute("type", "string");
			xmlElement.SetAttribute("value", value6);
			NodeCommand.AppendChild(xmlElement);
			xmlElement = NodeCommand.OwnerDocument.CreateElement("cmd", "Parameter", CommandNamespaceUri);
			xmlElement.SetAttribute("name", ParamNameContactAddress);
			xmlElement.SetAttribute("type", "string");
			xmlElement.SetAttribute("value", value7);
			NodeCommand.AppendChild(xmlElement);
			xmlElement = NodeCommand.OwnerDocument.CreateElement("cmd", "Parameter", CommandNamespaceUri);
			xmlElement.SetAttribute("name", ParamNameContactPostalCode);
			xmlElement.SetAttribute("type", "string");
			xmlElement.SetAttribute("value", value8);
			NodeCommand.AppendChild(xmlElement);
			xmlElement = NodeCommand.OwnerDocument.CreateElement("cmd", "Parameter", CommandNamespaceUri);
			xmlElement.SetAttribute("name", ParamNameContactCity);
			xmlElement.SetAttribute("type", "string");
			xmlElement.SetAttribute("value", value9);
			NodeCommand.AppendChild(xmlElement);
			xmlElement = NodeCommand.OwnerDocument.CreateElement("cmd", "Parameter", CommandNamespaceUri);
			xmlElement.SetAttribute("name", ParamNameContactProvince);
			xmlElement.SetAttribute("type", "string");
			xmlElement.SetAttribute("value", value10);
			NodeCommand.AppendChild(xmlElement);
			xmlElement = NodeCommand.OwnerDocument.CreateElement("cmd", "Parameter", CommandNamespaceUri);
			xmlElement.SetAttribute("name", ParamNameContactCountry);
			xmlElement.SetAttribute("type", "string");
			xmlElement.SetAttribute("value", value11);
			NodeCommand.AppendChild(xmlElement);
			xmlElement = NodeCommand.OwnerDocument.CreateElement("cmd", "Parameter", CommandNamespaceUri);
			xmlElement.SetAttribute("name", ParamNameDocId);
			xmlElement.SetAttribute("type", "string");
			xmlElement.SetAttribute("value", value12);
			NodeCommand.AppendChild(xmlElement);
			flag = true;
		}
		if (sqlCommand.Connection.State != 0)
		{
			sqlCommand.Connection.Close();
		}
		if (flag)
		{
			m_rootResults.InnerXml += NodeCommand.OuterXml.Replace("cmd:Command", "cmd:CommandResult");
		}
		return flag;
	}

	private bool Command_GetCurrencies(XmlNode NodeCommand)
	{
		XmlElement xmlElement = NodeCommand.OwnerDocument.CreateElement("cmd", "Parameter", CommandNamespaceUri);
		xmlElement.SetAttribute("name", ParamNameCurrencies);
		xmlElement.SetAttribute("type", "list");
		NodeCommand.AppendChild(xmlElement);
		string strQuery = "SELECT Nombre,Simbolo FROM [dbo].Monedas";
		bool flag = false;
		SqlCommand sqlCommand = BuildSQLCommand(strQuery, CommandType.Text);
		SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
		while (sqlDataReader.Read())
		{
			string value = sqlDataReader.GetValue(0).ToString().TrimEnd();
			string value2 = sqlDataReader.GetValue(1).ToString().TrimEnd();
			XmlElement xmlElement2 = NodeCommand.OwnerDocument.CreateElement("cmd", "Item", CommandNamespaceUri);
			xmlElement2.SetAttribute("name", ItemNameCurrency);
			xmlElement2.SetAttribute("type", "list");
			xmlElement.AppendChild(xmlElement2);
			XmlElement xmlElement3 = NodeCommand.OwnerDocument.CreateElement("cmd", "Item", CommandNamespaceUri);
			xmlElement3.SetAttribute("name", ItemNameName);
			xmlElement3.SetAttribute("type", "string");
			xmlElement3.SetAttribute("value", value);
			xmlElement2.AppendChild(xmlElement3);
			xmlElement3 = NodeCommand.OwnerDocument.CreateElement("cmd", "Item", CommandNamespaceUri);
			xmlElement3.SetAttribute("name", ItemNameSymbol);
			xmlElement3.SetAttribute("type", "string");
			xmlElement3.SetAttribute("value", value2);
			xmlElement2.AppendChild(xmlElement3);
			flag = true;
		}
		if (sqlCommand.Connection.State != 0)
		{
			sqlCommand.Connection.Close();
		}
		if (flag)
		{
			m_rootResults.InnerXml += NodeCommand.OuterXml.Replace("cmd:Command", "cmd:CommandResult");
		}
		return flag;
	}

	private bool Command_GetNextProjectCode(XmlNode NodeCommand)
	{
		string strQuery = "SELECT ISNULL(MAX(ProjectCode),0)+1 FROM Projects";
		bool flag = false;
		SqlCommand sqlCommand = BuildSQLCommand(strQuery, CommandType.Text);
		SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
		if (sqlDataReader.Read())
		{
			string value = sqlDataReader.GetValue(0).ToString().TrimEnd();
			XmlElement xmlElement = NodeCommand.OwnerDocument.CreateElement("cmd", "Parameter", CommandNamespaceUri);
			xmlElement.SetAttribute("name", ParamNameProjectCode);
			xmlElement.SetAttribute("type", "int64");
			xmlElement.SetAttribute("value", value);
			NodeCommand.AppendChild(xmlElement);
			flag = true;
		}
		if (sqlCommand.Connection.State != 0)
		{
			sqlCommand.Connection.Close();
		}
		if (flag)
		{
			m_rootResults.InnerXml += NodeCommand.OuterXml.Replace("cmd:Command", "cmd:CommandResult");
		}
		return flag;
	}

	private bool Command_GetPriceGroups(XmlNode NodeCommand)
	{
		XmlElement xmlElement = NodeCommand.OwnerDocument.CreateElement("cmd", "Parameter", CommandNamespaceUri);
		xmlElement.SetAttribute("name", ParamNameCurrencies);
		xmlElement.SetAttribute("type", "list");
		NodeCommand.AppendChild(xmlElement);
		string strQuery = "SELECT RowId,GroupId,GroupName FROM [dbo].Groups WHERE GroupType = 2";
		bool flag = false;
		SqlCommand sqlCommand = BuildSQLCommand(strQuery, CommandType.Text);
		SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
		while (sqlDataReader.Read())
		{
			string value = sqlDataReader.GetValue(0).ToString().TrimEnd();
			string value2 = sqlDataReader.GetValue(1).ToString().TrimEnd();
			string value3 = sqlDataReader.GetValue(2).ToString().TrimEnd();
			XmlElement xmlElement2 = NodeCommand.OwnerDocument.CreateElement("cmd", "Item", CommandNamespaceUri);
			xmlElement2.SetAttribute("name", ItemNamePriceGroup);
			xmlElement2.SetAttribute("type", "list");
			xmlElement.AppendChild(xmlElement2);
			XmlElement xmlElement3 = NodeCommand.OwnerDocument.CreateElement("cmd", "Item", CommandNamespaceUri);
			xmlElement3.SetAttribute("name", ItemNameRowId);
			xmlElement3.SetAttribute("type", "string");
			xmlElement3.SetAttribute("value", value);
			xmlElement2.AppendChild(xmlElement3);
			xmlElement3 = NodeCommand.OwnerDocument.CreateElement("cmd", "Item", CommandNamespaceUri);
			xmlElement3.SetAttribute("name", ItemNameCode);
			xmlElement3.SetAttribute("type", "string");
			xmlElement3.SetAttribute("value", value2);
			xmlElement2.AppendChild(xmlElement3);
			xmlElement3 = NodeCommand.OwnerDocument.CreateElement("cmd", "Item", CommandNamespaceUri);
			xmlElement3.SetAttribute("name", ItemNameName);
			xmlElement3.SetAttribute("type", "string");
			xmlElement3.SetAttribute("value", value3);
			xmlElement2.AppendChild(xmlElement3);
			flag = true;
		}
		if (sqlCommand.Connection.State != 0)
		{
			sqlCommand.Connection.Close();
		}
		if (flag)
		{
			m_rootResults.InnerXml += NodeCommand.OuterXml.Replace("cmd:Command", "cmd:CommandResult");
		}
		return flag;
	}

	private bool Command_GetAllGroupsOfType(XmlNode NodeCommand)
	{
		int num = 0;
		XmlNode xmlNode = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameGroupType + "\"]", m_xmlNamespaceMgr);
		if (xmlNode == null)
		{
			AddError("Missing Parameter GroupType", "GetAllGroupsOfType");
			return false;
		}
		try
		{
			num = Convert.ToInt32(xmlNode.Attributes["value"].Value);
		}
		catch (Exception)
		{
			num = 0;
		}
		if (num <= 0 || num > 4)
		{
			AddError("Invalid Parameter GroupType", "GetAllGroupsOfType");
			return false;
		}
		XmlElement xmlElement = NodeCommand.OwnerDocument.CreateElement("cmd", "Parameter", CommandNamespaceUri);
		xmlElement.SetAttribute("name", ParamNameCurrencies);
		xmlElement.SetAttribute("type", "list");
		NodeCommand.AppendChild(xmlElement);
		string strQuery = "SELECT RowId,GroupId,GroupName,'WorkForce' FROM [dbo].Groups WHERE GroupType = " + num + " ORDER BY GroupName";
		if (num == 4)
		{
			strQuery = "SELECT pg.RowId,GroupCode,GroupName, Nombre FROM [dbo].PurchaseGroups pg\r\n\t\t\tLEFT OUTER JOIN Proveedores prov ON pg.supplierid=prov.rowid ORDER BY GroupName";
		}
		bool flag = false;
		SqlCommand sqlCommand = BuildSQLCommand(strQuery, CommandType.Text);
		SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
		while (sqlDataReader.Read())
		{
			string strAttributeValue = sqlDataReader.GetValue(0).ToString().TrimEnd();
			string strAttributeValue2 = sqlDataReader.GetValue(1).ToString().TrimEnd();
			string strAttributeValue3 = sqlDataReader.GetValue(2).ToString().TrimEnd();
			string strAttributeValue4 = sqlDataReader.GetValue(3).ToString().TrimEnd();
			XmlNode parentElem = AddElementToNodeWithAttributes("Item", xmlElement, ItemNameGroup, "list");
			AddElementToNodeWithAttributes("Item", parentElem, ItemNameRowId, "string", strAttributeValue);
			AddElementToNodeWithAttributes("Item", parentElem, ItemNameCode, "string", strAttributeValue2);
			AddElementToNodeWithAttributes("Item", parentElem, ItemNameName, "string", strAttributeValue3);
			AddElementToNodeWithAttributes("Item", parentElem, ItemNameProviderName, "string", strAttributeValue4);
			flag = true;
		}
		if (sqlCommand.Connection.State != 0)
		{
			sqlCommand.Connection.Close();
		}
		if (flag)
		{
			m_rootResults.InnerXml += NodeCommand.OuterXml.Replace("cmd:Command", "cmd:CommandResult");
		}
		return flag;
	}

	private bool Command_GetProjectDetails(XmlNode NodeCommand)
	{
		bool result = false;
		XmlNode xmlNode = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameProjectCode + "\"]", m_xmlNamespaceMgr);
		if (xmlNode == null)
		{
			AddError("Missing Parameter ProjectCode", "GetProjectDetails");
			return false;
		}
		string value = xmlNode.Attributes["value"].Value;
		string strQuery = "\r\nSELECT [ProjectId]\r\n      ,[ProjectCode]\r\n      ,[Name]\r\n      ,[Description]\r\n      ,[CreationDate]\r\n      ,[Address1]\r\n      ,[Address2]\r\n      ,[PostalCode]\r\n      ,[City]\r\n      ,[Province]\r\n      ,[Country]\r\n      ,[BillingAddress1]\r\n      ,[BillingAddress2]\r\n      ,[BillingPostalCode]\r\n      ,[BillingCity]\r\n      ,[BillingProvince]\r\n      ,[BillingCountry]\r\n      ,[ShippingAddress1]\r\n      ,[ShippingAddress2]\r\n      ,[ShippingPostalCode]\r\n      ,[ShippingCity]\r\n      ,[ShippingProvince]\r\n      ,[ShippingCountry]\r\n      ,[AgreedAmount]\r\n      ,[AgreedAmountCurrency]\r\n      ,[Comments]\r\n      ,[CustomerName]\r\n      ,[ProjectDiscount]\r\n      ,[Contingencies]\r\n      ,[GeneralExpenditures]\r\n      ,[FinancialExpenditures]\r\n      ,[Commissions]\r\n      ,[CustomerCode]\r\n      ,[PricingComments]\r\n FROM [dbo].[Projects] WHERE ProjectCode=" + value;
		SqlCommand sqlCommand = BuildSQLCommand(strQuery, CommandType.Text);
		SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
		if (sqlDataReader.Read())
		{
			int num = -1;
			string value2 = ((!sqlDataReader.IsDBNull(++num)) ? sqlDataReader.GetValue(num).ToString() : string.Empty);
			value = ((!sqlDataReader.IsDBNull(++num)) ? sqlDataReader.GetValue(num).ToString() : string.Empty);
			string value3 = ((!sqlDataReader.IsDBNull(++num)) ? sqlDataReader.GetValue(num).ToString() : string.Empty);
			string value4 = ((!sqlDataReader.IsDBNull(++num)) ? sqlDataReader.GetValue(num).ToString() : string.Empty);
			string value5 = ((!sqlDataReader.IsDBNull(++num)) ? sqlDataReader.GetDateTime(num).ToString("yyyy/MM/dd HH:mm:ss", DateTimeFormatInfo.InvariantInfo) : string.Empty);
			string value6 = ((!sqlDataReader.IsDBNull(++num)) ? sqlDataReader.GetValue(num).ToString() : string.Empty);
			string value7 = ((!sqlDataReader.IsDBNull(++num)) ? sqlDataReader.GetValue(num).ToString() : string.Empty);
			string value8 = ((!sqlDataReader.IsDBNull(++num)) ? sqlDataReader.GetValue(num).ToString() : string.Empty);
			string value9 = ((!sqlDataReader.IsDBNull(++num)) ? sqlDataReader.GetValue(num).ToString() : string.Empty);
			string value10 = ((!sqlDataReader.IsDBNull(++num)) ? sqlDataReader.GetValue(num).ToString() : string.Empty);
			string value11 = ((!sqlDataReader.IsDBNull(++num)) ? sqlDataReader.GetValue(num).ToString() : string.Empty);
			string value12 = ((!sqlDataReader.IsDBNull(++num)) ? sqlDataReader.GetValue(num).ToString() : string.Empty);
			string value13 = ((!sqlDataReader.IsDBNull(++num)) ? sqlDataReader.GetValue(num).ToString() : string.Empty);
			string value14 = ((!sqlDataReader.IsDBNull(++num)) ? sqlDataReader.GetValue(num).ToString() : string.Empty);
			string value15 = ((!sqlDataReader.IsDBNull(++num)) ? sqlDataReader.GetValue(num).ToString() : string.Empty);
			string value16 = ((!sqlDataReader.IsDBNull(++num)) ? sqlDataReader.GetValue(num).ToString() : string.Empty);
			string value17 = ((!sqlDataReader.IsDBNull(++num)) ? sqlDataReader.GetValue(num).ToString() : string.Empty);
			string value18 = ((!sqlDataReader.IsDBNull(++num)) ? sqlDataReader.GetValue(num).ToString() : string.Empty);
			string value19 = ((!sqlDataReader.IsDBNull(++num)) ? sqlDataReader.GetValue(num).ToString() : string.Empty);
			string value20 = ((!sqlDataReader.IsDBNull(++num)) ? sqlDataReader.GetValue(num).ToString() : string.Empty);
			string value21 = ((!sqlDataReader.IsDBNull(++num)) ? sqlDataReader.GetValue(num).ToString() : string.Empty);
			string value22 = ((!sqlDataReader.IsDBNull(++num)) ? sqlDataReader.GetValue(num).ToString() : string.Empty);
			string value23 = ((!sqlDataReader.IsDBNull(++num)) ? sqlDataReader.GetValue(num).ToString() : string.Empty);
			string value24 = ((!sqlDataReader.IsDBNull(++num)) ? Convert.ToString(sqlDataReader.GetValue(num), CultureInfo.InvariantCulture) : string.Empty);
			string value25 = ((!sqlDataReader.IsDBNull(++num)) ? sqlDataReader.GetValue(num).ToString() : string.Empty);
			string value26 = ((!sqlDataReader.IsDBNull(++num)) ? sqlDataReader.GetValue(num).ToString() : string.Empty);
			string value27 = ((!sqlDataReader.IsDBNull(++num)) ? sqlDataReader.GetValue(num).ToString() : string.Empty);
			string value28 = ((!sqlDataReader.IsDBNull(++num)) ? Convert.ToString(sqlDataReader.GetValue(num), CultureInfo.InvariantCulture) : string.Empty);
			string value29 = ((!sqlDataReader.IsDBNull(++num)) ? Convert.ToString(sqlDataReader.GetValue(num), CultureInfo.InvariantCulture) : string.Empty);
			string value30 = ((!sqlDataReader.IsDBNull(++num)) ? Convert.ToString(sqlDataReader.GetValue(num), CultureInfo.InvariantCulture) : string.Empty);
			string value31 = ((!sqlDataReader.IsDBNull(++num)) ? Convert.ToString(sqlDataReader.GetValue(num), CultureInfo.InvariantCulture) : string.Empty);
			string value32 = ((!sqlDataReader.IsDBNull(++num)) ? Convert.ToString(sqlDataReader.GetValue(num), CultureInfo.InvariantCulture) : string.Empty);
			string value33 = ((!sqlDataReader.IsDBNull(++num)) ? sqlDataReader.GetValue(num).ToString() : string.Empty);
			string value34 = ((!sqlDataReader.IsDBNull(++num)) ? sqlDataReader.GetValue(num).ToString() : string.Empty);
			AddParameter(NodeCommand, ParamNameProjectId, value2);
			AddParameter(NodeCommand, ParamNameProjectName, value3);
			AddParameter(NodeCommand, ParamNameCustomerName, value27);
			AddParameter(NodeCommand, ParamNameProjectDescription, value4);
			AddParameter(NodeCommand, ParamNameProjectCreationDate, value5);
			AddParameter(NodeCommand, ParamNameProjectCustomerAddress1, value6);
			AddParameter(NodeCommand, ParamNameProjectCustomerAddress2, value7);
			AddParameter(NodeCommand, ParamNameProjectCustomerPostalCode, value8);
			AddParameter(NodeCommand, ParamNameProjectCustomerCity, value9);
			AddParameter(NodeCommand, ParamNameProjectCustomerProvince, value10);
			AddParameter(NodeCommand, ParamNameProjectCustomerCountry, value11);
			AddParameter(NodeCommand, ParamNameProjectInvoicingAddress1, value12);
			AddParameter(NodeCommand, ParamNameProjectInvoicingAddress2, value13);
			AddParameter(NodeCommand, ParamNameProjectInvoicingPostalCode, value14);
			AddParameter(NodeCommand, ParamNameProjectInvoicingCity, value15);
			AddParameter(NodeCommand, ParamNameProjectInvoicingProvince, value16);
			AddParameter(NodeCommand, ParamNameProjectInvoicingCountry, value17);
			AddParameter(NodeCommand, ParamNameProjectShippingAddress1, value18);
			AddParameter(NodeCommand, ParamNameProjectShippingAddress2, value19);
			AddParameter(NodeCommand, ParamNameProjectShippingPostalCode, value20);
			AddParameter(NodeCommand, ParamNameProjectShippingCity, value21);
			AddParameter(NodeCommand, ParamNameProjectShippingProvince, value22);
			AddParameter(NodeCommand, ParamNameProjectShippingCountry, value23);
			AddParameter(NodeCommand, ParamNameProjectAgreedAmount, value24);
			AddParameter(NodeCommand, ParamNameProjectAgreedAmountCurrency, value25);
			AddParameter(NodeCommand, ParamNameProjectComments, value26);
			AddParameter(NodeCommand, ParamNameProjectDiscount, value28);
			AddParameter(NodeCommand, ParamNameContingencies, value29);
			AddParameter(NodeCommand, ParamNameGeneralExpenditures, value30);
			AddParameter(NodeCommand, ParamNameFinancialExpenditures, value31);
			AddParameter(NodeCommand, ParamNameCommissions, value32);
			AddParameter(NodeCommand, ParamNameCustomerCode, value33);
			AddParameter(NodeCommand, ParamNameProjectPricingComments, value34);
			result = true;
			m_rootResults.InnerXml += NodeCommand.OuterXml.Replace("cmd:Command", "cmd:CommandResult");
		}
		else
		{
			AddError("Project does not exist!", "Entered project code does not correspond to any project");
		}
		if (sqlCommand.Connection.State != 0)
		{
			sqlCommand.Connection.Close();
		}
		return result;
	}

	private void AddParameter(XmlNode node, string name, string value)
	{
		XmlElement xmlElement = node.OwnerDocument.CreateElement("cmd", "Parameter", CommandNamespaceUri);
		xmlElement.SetAttribute("name", name);
		xmlElement.SetAttribute("value", value);
		node.AppendChild(xmlElement);
	}

	public DataSet GetDocumentDocuments(Guid documentId, int documentTypeCode)
	{
		string text = $"SELECT * FROM Objects_GetRelatedDocuments ('{documentId}', {documentTypeCode}, NULL)";
		return SqlHelper.ExecuteDataset(_strConnectionString, CommandType.Text, text);
	}

	public DataSet GetDocumentDocuments(Guid documentId, int documentTypeCode, int destDocumentTypeCode)
	{
		DataSet dataSet = new DataSet();
		DataTable dataTable = new DataTable();
		dataTable.Columns.Add("SrcFolderId", typeof(Guid));
		dataTable.Columns.Add("DestDocumentId", typeof(Guid));
		dataTable.Columns.Add("DestDocumentType", typeof(int));
		dataTable.Columns.Add("RelationshipType", typeof(int));
		dataSet.Tables.Add(dataTable);
		DataSet documentDocuments = GetDocumentDocuments(documentId, documentTypeCode);
		foreach (DataRow row in documentDocuments.Tables[0].Rows)
		{
			int num = 0;
			if (!row.IsNull("DestDocumentType"))
			{
				num = Convert.ToInt32(row["DestDocumentType"]);
			}
			if (num == destDocumentTypeCode)
			{
				DataRow dataRow2 = dataTable.NewRow();
				dataRow2["SrcFolderId"] = row["SrcFolderId"];
				dataRow2["DestDocumentId"] = row["DestDocumentId"];
				dataRow2["DestDocumentType"] = row["DestDocumentType"];
				dataRow2["RelationshipType"] = row["RelationshipType"];
				dataTable.Rows.Add(dataRow2);
			}
		}
		return dataSet;
	}

	public DataSet GetExpenseDocumentsOfProject(Guid projectId)
	{
		string text = "SELECT\tED.Number";
		text += " ,\t\tED.Title";
		text += " FROM\tExpenses.Documents ED";
		text += " INNER JOIN DocumentRelationShips DRS ON DRS.DestDocumentId = ED.DocumentId AND DRS.DestDocumentType = 39";
		text += $" WHERE\tDRS.SrcDocumentId='{projectId}' ";
		text += " AND\tDRS.SrcDocumentType = 36";
		return SqlHelper.ExecuteDataset(_strConnectionString, CommandType.Text, text);
	}

	private bool Command_GetProjectDocuments(XmlNode NodeCommand)
	{
		bool flag = false;
		string text = string.Empty;
		string text2 = string.Empty;
		Guid empty = Guid.Empty;
		Guid empty2 = Guid.Empty;
		long num = -1L;
		int num2 = -1;
		long paramProjectTypeCode = ParamProjectTypeCode;
		XmlNode xmlNode = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameProjectId + "\"]", m_xmlNamespaceMgr);
		if (xmlNode != null)
		{
			text = xmlNode.Attributes["value"].Value;
		}
		xmlNode = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameFolderId + "\"]", m_xmlNamespaceMgr);
		if (xmlNode != null)
		{
			text2 = xmlNode.Attributes["value"].Value;
		}
		if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2))
		{
			XmlElement xmlElement = NodeCommand.OwnerDocument.CreateElement("cmd", "Parameter", CommandNamespaceUri);
			xmlElement.SetAttribute("name", ParamNameDocumentList);
			xmlElement.SetAttribute("type", "list");
			NodeCommand.AppendChild(xmlElement);
			if (string.Compare(text2, "null", ignoreCase: true) != 0)
			{
				text2 = "'" + text2 + "'";
			}
			string cmdText = $"SELECT * FROM Objects_GetRelatedDocuments ('{text}',{paramProjectTypeCode},{text2})";
			SqlConnection sqlConnection = new SqlConnection(_strConnectionString);
			sqlConnection.Open();
			SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection);
			sqlCommand.CommandType = CommandType.Text;
			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			while (sqlDataReader.Read())
			{
				XmlElement xmlElement2 = NodeCommand.OwnerDocument.CreateElement("cmd", "Item", CommandNamespaceUri);
				xmlElement2.SetAttribute("name", ItemNameDocument);
				xmlElement2.SetAttribute("type", "list");
				xmlElement.AppendChild(xmlElement2);
				empty2 = (sqlDataReader.IsDBNull(0) ? Guid.Empty : sqlDataReader.GetGuid(0));
				XmlElement xmlElement3 = NodeCommand.OwnerDocument.CreateElement("cmd", "Item", CommandNamespaceUri);
				xmlElement3.SetAttribute("name", ParamNameSrcFolderId);
				xmlElement3.SetAttribute("type", "string");
				xmlElement3.SetAttribute("value", empty2.ToString("D").ToUpper());
				xmlElement2.AppendChild(xmlElement3);
				empty = (sqlDataReader.IsDBNull(1) ? Guid.Empty : sqlDataReader.GetGuid(1));
				xmlElement3 = NodeCommand.OwnerDocument.CreateElement("cmd", "Item", CommandNamespaceUri);
				xmlElement3.SetAttribute("name", ParamNameDestDocId);
				xmlElement3.SetAttribute("type", "string");
				xmlElement3.SetAttribute("value", empty.ToString("D").ToUpper());
				xmlElement2.AppendChild(xmlElement3);
				num = (sqlDataReader.IsDBNull(2) ? (-1) : sqlDataReader.GetInt32(2));
				xmlElement3 = NodeCommand.OwnerDocument.CreateElement("cmd", "Item", CommandNamespaceUri);
				xmlElement3.SetAttribute("name", ParamNameDocType);
				xmlElement3.SetAttribute("type", "long");
				xmlElement3.SetAttribute("value", num.ToString());
				xmlElement2.AppendChild(xmlElement3);
				num2 = (sqlDataReader.IsDBNull(3) ? (-1) : sqlDataReader.GetInt16(3));
				xmlElement3 = NodeCommand.OwnerDocument.CreateElement("cmd", "Item", CommandNamespaceUri);
				xmlElement3.SetAttribute("name", ParamNameRelationshipType);
				xmlElement3.SetAttribute("type", "int");
				xmlElement3.SetAttribute("value", num2.ToString());
				xmlElement2.AppendChild(xmlElement3);
				flag = true;
			}
			if (sqlCommand.Connection.State != 0)
			{
				sqlCommand.Connection.Close();
			}
			if (flag)
			{
				m_rootResults.InnerXml += NodeCommand.OuterXml.Replace("cmd:Command", "cmd:CommandResult");
			}
		}
		return flag;
	}

	private bool Command_GetProjectFolders(XmlNode NodeCommand)
	{
		string empty = string.Empty;
		try
		{
			XmlNode xmlNode = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameProjectId + "\"]", m_xmlNamespaceMgr);
			if (xmlNode == null)
			{
				AddError("Missing Parameter Project GUID", "GetProjectFolders");
				return false;
			}
			empty = xmlNode.Attributes["value"].Value;
			string strQuery = "SELECT [dbo].[Objects_GetFoldersXML] ('" + empty + "'," + ParamProjectTypeCode + ")";
			SqlCommand sqlCommand = BuildSQLCommand(strQuery, CommandType.Text);
			XmlReader xmlReader = sqlCommand.ExecuteXmlReader();
			int num = -1;
			if (!xmlReader.IsEmptyElement)
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(xmlReader);
				NodeCommand.InnerText = xmlDocument.OuterXml;
				m_rootResults.InnerXml += NodeCommand.OuterXml.Replace("cmd:Command", "cmd:CommandResult");
				num = 0;
			}
			if (sqlCommand.Connection.State != 0)
			{
				sqlCommand.Connection.Close();
			}
			return num == 0;
		}
		catch (Exception ex)
		{
			AddError(ex.Message, ex.Source);
			return false;
		}
	}

	private bool Command_GetProjectFromDocument(XmlNode NodeCommand)
	{
		long num = 0L;
		int nDocType = 0;
		Guid guid = Guid.Empty;
		Guid idDoc = Guid.Empty;
		try
		{
			if (!GetCommandParams_GetProjectFromDocument(NodeCommand, ref idDoc, ref nDocType))
			{
				return false;
			}
			string strQuery = "[dbo].[pa_Projects_GetProjectFromRelatedDocument]";
			SqlCommand sqlCommand = BuildSQLCommand(strQuery, CommandType.StoredProcedure);
			SqlParameter sqlParameter = sqlCommand.Parameters.Add("RETURN_VALUE", SqlDbType.Int);
			sqlParameter.Direction = ParameterDirection.ReturnValue;
			SqlParameter sqlParameter2 = sqlCommand.Parameters.Add("@guidProjectId", SqlDbType.UniqueIdentifier);
			sqlParameter2.Direction = ParameterDirection.Output;
			SqlParameter sqlParameter3 = sqlCommand.Parameters.Add("@nProjectCode", SqlDbType.BigInt);
			sqlParameter3.Direction = ParameterDirection.Output;
			sqlParameter = sqlCommand.Parameters.Add("@guidDestDocumentId", SqlDbType.UniqueIdentifier);
			sqlParameter.Direction = ParameterDirection.Input;
			sqlParameter.Value = idDoc;
			sqlParameter = sqlCommand.Parameters.Add("@nDestDocumentType", SqlDbType.Int);
			sqlParameter.Direction = ParameterDirection.Input;
			sqlParameter.Value = nDocType;
			sqlParameter = sqlCommand.Parameters.Add("@xmlMessages", SqlDbType.Xml, 4000);
			sqlParameter.Direction = ParameterDirection.Output;
			sqlCommand.ExecuteNonQuery();
			int num2 = (int)sqlCommand.Parameters["RETURN_VALUE"].Value;
			if (sqlCommand.Parameters["@xmlMessages"].Value != DBNull.Value)
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.LoadXml(sqlCommand.Parameters["@xmlMessages"].Value.ToString());
				AppendMessages(xmlDocument.DocumentElement.InnerXml);
			}
			if (num2 == 0)
			{
				XmlNode oldChild = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameDocId + "\"]", m_xmlNamespaceMgr);
				NodeCommand.RemoveChild(oldChild);
				oldChild = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameDocType + "\"]", m_xmlNamespaceMgr);
				NodeCommand.RemoveChild(oldChild);
				if (sqlParameter2.Value != DBNull.Value)
				{
					guid = (Guid)sqlParameter2.Value;
				}
				if (sqlParameter3.Value != DBNull.Value)
				{
					num = (long)sqlParameter3.Value;
				}
				XmlElement xmlElement = m_xmlCommand.CreateElement("cmd", "Parameter", CommandNamespaceUri);
				xmlElement.SetAttribute("name", ParamNameProjectId);
				xmlElement.SetAttribute("type", "string");
				xmlElement.SetAttribute("value", guid.ToString("D").ToUpper());
				NodeCommand.AppendChild(xmlElement);
				xmlElement = m_xmlCommand.CreateElement("cmd", "Parameter", CommandNamespaceUri);
				xmlElement.SetAttribute("name", ParamNameProjectCode);
				xmlElement.SetAttribute("type", "int64");
				xmlElement.SetAttribute("value", num.ToString());
				NodeCommand.AppendChild(xmlElement);
				m_rootResults.InnerXml += NodeCommand.OuterXml.Replace("cmd:Command", "cmd:CommandResult");
			}
			if (sqlCommand.Connection.State != 0)
			{
				sqlCommand.Connection.Close();
			}
			return num2 == 0;
		}
		catch (Exception ex)
		{
			AddError(ex.Message, ex.Source);
			return false;
		}
	}

	private bool Command_GetProjectPricePolicy(XmlNode NodeCommand)
	{
		XmlNode xmlNode = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameProjectCode + "\"]", m_xmlNamespaceMgr);
		if (xmlNode == null)
		{
			AddError("Missing Parameter ProjectCode", "GetProjectPricePolicy");
			return false;
		}
		string value = xmlNode.Attributes["value"].Value;
		string strQuery = "\r\nSELECT TOP(1) XmlTariffChains\r\nFROM TariffChainAggregations tca\r\nINNER JOIN DocumentRelationships dr ON  tca.ObjectId = dr.DestDocumentId AND dr.DestDocumentType = 1\r\nINNER JOIN Projects pr ON dr.SrcDocumentId = pr.ProjectId\r\nWHERE pr.ProjectCode = @code\r\n";
		SqlCommand sqlCommand = BuildSQLCommand(strQuery, CommandType.Text);
		sqlCommand.Parameters.AddWithValue("@code", value);
		XmlReader xmlReader = sqlCommand.ExecuteXmlReader();
		if (!xmlReader.IsEmptyElement)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(xmlReader);
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
			xmlNamespaceManager.AddNamespace("pri", PricesNamespaceUri);
			XmlNode xmlNode2 = xmlDocument.SelectSingleNode("/pri:TariffChains/pri:TariffChain[pri:TariffChainFlag[@id=\"chainForPurchases\"]]/pri:Tariffs/pri:Tariff[@type=\"0\"]", xmlNamespaceManager);
			if (xmlNode2 != null)
			{
				ProjectTariffProviderDiscount = xmlNode2.Attributes["name"].Value.ToString();
			}
			XmlNode xmlNode3 = xmlDocument.SelectSingleNode("/pri:TariffChains/pri:TariffChain[pri:TariffChainFlag[@id=\"chainForCost\"]]/pri:Tariffs/pri:Tariff[@type=\"0\"]", xmlNamespaceManager);
			if (xmlNode3 != null)
			{
				ProjectTariffCostIncrement = xmlNode3.Attributes["name"].Value.ToString();
			}
			XmlNode xmlNode4 = xmlDocument.SelectSingleNode("/pri:TariffChains/pri:TariffChain[pri:TariffChainFlag[@id=\"chainForPrice\"]]/pri:Tariffs/pri:Tariff[@type=\"0\"]", xmlNamespaceManager);
			if (xmlNode4 != null)
			{
				ProjectTariffSalesIncrement = xmlNode4.Attributes["name"].Value.ToString();
			}
		}
		xmlReader.Close();
		XmlElement xmlElement = NodeCommand.OwnerDocument.CreateElement("cmd", "Parameter", CommandNamespaceUri);
		xmlElement.SetAttribute("name", "RedefinedTariffNames");
		xmlElement.SetAttribute("type", "list");
		NodeCommand.AppendChild(xmlElement);
		AddElementToNodeWithAttributes("Item", xmlElement, "ForPurchase", "string", ProjectTariffProviderDiscount);
		AddElementToNodeWithAttributes("Item", xmlElement, "ForCost", "string", ProjectTariffCostIncrement);
		AddElementToNodeWithAttributes("Item", xmlElement, "ForPrice", "string", ProjectTariffSalesIncrement);
		string strQuery2 = "\r\nSELECT t.[Name], ISNULL(t2.[Name],'') AS RedefinedTariffName,\r\ntc.PurchaseGroupRowId, tc.PriceGroupRowId, c.Nombre, tc.[Value] FROM TariffsContent tc \r\nINNER JOIN vwTariff t ON tc.TariffRowId = t.RowId\r\nLEFT OUTER JOIN Colores c ON c.RowId = tc.ColorRowId\r\nLEFT OUTER JOIN vwTariff t2 ON tc.RedefinedTariffRowId = t2.RowId\r\nWHERE (tc.[Type]=10 OR tc.[Type]=11) AND t.[Type]=10 AND t.[Name]='" + value + "' \r\nAND (tc.PurchaseGroupRowId IS NOT NULL OR tc.PriceGroupRowId IS NOT NULL)\r\nUNION\r\nSELECT t.[Name], ISNULL(t2.[Name],'') AS RedefinedTariffName, tc.PurchaseGroupRowId,\r\ntc.PriceGroupRowId, c.Nombre, tc.[Value] FROM TariffsContent tc \r\nINNER JOIN vwTariff t ON tc.TariffRowId = t.RowId\r\nLEFT OUTER JOIN Colores c ON c.RowId = tc.ColorRowId\r\nLEFT OUTER JOIN vwTariff t2 ON tc.RedefinedTariffRowId = t2.RowId\r\nWHERE (tc.[Type]=10 OR tc.[Type]=11) AND t.[Type]=0 AND (t.[Name]='" + ProjectTariffProviderDiscount + "' OR t.[Name]='" + ProjectTariffSalesIncrement + "' OR t.[Name]='" + ProjectTariffCostIncrement + "') ORDER BY PurchaseGroupRowId, Nombre, PriceGroupRowId";
		bool flag = true;
		sqlCommand = BuildSQLCommand(strQuery2, CommandType.Text);
		SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
		XmlElement xmlElement2 = NodeCommand.OwnerDocument.CreateElement("cmd", "Parameter", CommandNamespaceUri);
		xmlElement2.SetAttribute("name", ParamNameExceptions);
		xmlElement2.SetAttribute("type", "list");
		NodeCommand.AppendChild(xmlElement2);
		while (sqlDataReader.Read())
		{
			string value2 = sqlDataReader.GetValue(0).ToString().TrimEnd();
			string value3 = sqlDataReader.GetValue(1).ToString().TrimEnd();
			string empty = string.Empty;
			empty = (sqlDataReader.IsDBNull(2) ? sqlDataReader.GetValue(3).ToString().TrimEnd() : sqlDataReader.GetValue(2).ToString().TrimEnd());
			string value4 = sqlDataReader.GetDouble(5).ToString(NumberFormatInfo.InvariantInfo);
			XmlElement xmlElement3 = NodeCommand.OwnerDocument.CreateElement("cmd", "Item", CommandNamespaceUri);
			xmlElement3.SetAttribute("name", ParamNameException);
			xmlElement3.SetAttribute("type", "list");
			xmlElement2.AppendChild(xmlElement3);
			XmlElement xmlElement4 = NodeCommand.OwnerDocument.CreateElement("cmd", "Item", CommandNamespaceUri);
			xmlElement4.SetAttribute("name", ParamNameTariffName);
			xmlElement4.SetAttribute("type", "string");
			xmlElement4.SetAttribute("value", value2);
			xmlElement3.AppendChild(xmlElement4);
			xmlElement4 = NodeCommand.OwnerDocument.CreateElement("cmd", "Item", CommandNamespaceUri);
			xmlElement4.SetAttribute("name", ParamNameRedefinedTariffName);
			xmlElement4.SetAttribute("type", "string");
			xmlElement4.SetAttribute("value", value3);
			xmlElement3.AppendChild(xmlElement4);
			xmlElement4 = NodeCommand.OwnerDocument.CreateElement("cmd", "Item", CommandNamespaceUri);
			xmlElement4.SetAttribute("name", ParamNameGroupRowId);
			xmlElement4.SetAttribute("type", "string");
			xmlElement4.SetAttribute("value", empty);
			xmlElement3.AppendChild(xmlElement4);
			if (!sqlDataReader.IsDBNull(4))
			{
				xmlElement4 = NodeCommand.OwnerDocument.CreateElement("cmd", "Item", CommandNamespaceUri);
				xmlElement4.SetAttribute("name", ParamNameColor);
				xmlElement4.SetAttribute("type", "string");
				xmlElement4.SetAttribute("value", sqlDataReader.GetValue(4).ToString().TrimEnd());
				xmlElement3.AppendChild(xmlElement4);
			}
			xmlElement4 = NodeCommand.OwnerDocument.CreateElement("cmd", "Item", CommandNamespaceUri);
			xmlElement4.SetAttribute("name", ParamNameValue);
			xmlElement4.SetAttribute("type", "string");
			xmlElement4.SetAttribute("value", value4);
			xmlElement3.AppendChild(xmlElement4);
			flag = true;
		}
		if (sqlCommand.Connection.State != 0)
		{
			sqlCommand.Connection.Close();
		}
		if (flag)
		{
			m_rootResults.InnerXml += NodeCommand.OuterXml.Replace("cmd:Command", "cmd:CommandResult");
		}
		return flag;
	}

	private bool Command_GetProjectStatus(XmlNode NodeCommand)
	{
		XmlNode xmlNode = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameProjectCode + "\"]", m_xmlNamespaceMgr);
		if (xmlNode == null)
		{
			AddError("Missing Parameter ProjectCode", "GetProjectStatus");
			return false;
		}
		string value = xmlNode.Attributes["value"].Value;
		XmlNode xmlNode2 = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameShowWorkforceCost + "\"]", m_xmlNamespaceMgr);
		bool flag = true;
		if (xmlNode != null)
		{
			flag = xmlNode2.Attributes["value"].Value != "0";
		}
		string strQuery = "\r\nSELECT COUNT(*) AS NumberOfInstances, ISNULL(SUM(CP.PrecioUnitario),0.0) As Importe,N'Accepted' AS Status, 1 AS StatusPosition\r\nFROM PAF\r\nINNER JOIN DocumentRelationships DR ON DR.DestDocumentId = PAF.RowId AND (DR.DestDocumentType = 1 OR DR.DestDocumentType = 2)\r\nINNER JOIN Projects PRO ON PRO.ProjectId = DR.SrcDocumentId AND DR.SrcDocumentType = 36\r\nINNER JOIN ContenidoPAF CP ON CP.Numero=PAF.Numero AND CP.Version=PAF.Version\r\nINNER JOIN EstadoModelosPAF EMP ON EMP.Numero=CP.Numero AND EMP.Version=CP.Version AND  EMP.Orden=CP.Orden\r\nWHERE \r\n((PAF.Confirmado = 1 AND PAF.Type = 2 AND NOT EXISTS (SELECT * FROM PAF P2 WHERE P2.Numero = PAF.Numero AND P2.Type = 5)) OR PAF.Type = 5)\r\nAND PAF.FechaConfirmacion IS NOT NULL AND PAF.ReadyToProdDate IS NULL AND \r\nEMP.MeasurementDate IS NULL AND PRO.ProjectCode = " + value + "\r\nUNION\r\nSELECT COUNT(*) AS NumberOfInstances, ISNULL(SUM(CP.PrecioUnitario),0.0) As Importe,N'Estimated' AS Status, 2 AS StatusPosition\r\nFROM PAF\r\nINNER JOIN DocumentRelationships DR ON DR.DestDocumentId = PAF.RowId AND (DR.DestDocumentType = 1 OR DR.DestDocumentType = 2)\r\nINNER JOIN Projects PRO ON PRO.ProjectId = DR.SrcDocumentId AND DR.SrcDocumentType = 36\r\nINNER JOIN ContenidoPAF CP ON CP.Numero=PAF.Numero AND CP.Version=PAF.Version\r\nINNER JOIN EstadoModelosPAF EMP ON EMP.Numero=CP.Numero AND EMP.Version=CP.Version AND  EMP.Orden=CP.Orden\r\nWHERE ((PAF.Confirmado = 1 AND PAF.Type = 2 AND NOT EXISTS (SELECT * FROM PAF P2 WHERE P2.Numero = PAF.Numero AND P2.Type = 5)) OR PAF.Type = 5)\r\nAND EMP.MeasurementDate IS NOT NULL AND EMP.FechaMontaje IS NULL AND PAF.ReadyToProdDate IS NULL AND PRO.ProjectCode = " + value + "\r\nUNION\r\nSELECT COUNT(*) AS NumberOfInstances, ISNULL(SUM(CP.PrecioUnitario),0.0) As Importe,N'Ordered' AS Status, 3 AS StatusPosition\r\nFROM PAF\r\nINNER JOIN DocumentRelationships DR ON DR.DestDocumentId = PAF.RowId AND (DR.DestDocumentType = 1 OR DR.DestDocumentType = 2)\r\nINNER JOIN Projects PRO ON PRO.ProjectId = DR.SrcDocumentId AND DR.SrcDocumentType = 36\r\nINNER JOIN ContenidoPAF CP ON CP.Numero=PAF.Numero AND CP.Version=PAF.Version\r\nINNER JOIN EstadoModelosPAF EMP ON EMP.Numero=CP.Numero AND EMP.Version=CP.Version AND  EMP.Orden=CP.Orden\r\nWHERE PAF.Confirmado = 1 AND PAF.Type = 2 AND PRO.ProjectCode = " + value + "\r\nUNION\r\nSELECT COUNT(*) AS NumberOfInstances, ISNULL(SUM(CP.PrecioUnitario),0.0) As Importe,N'Purchased' AS Status, 4 AS StatusPosition\r\nFROM PAF\r\nINNER JOIN DocumentRelationships DR ON DR.DestDocumentId = PAF.RowId AND (DR.DestDocumentType = 1 OR DR.DestDocumentType = 2)\r\nINNER JOIN Projects PRO ON PRO.ProjectId = DR.SrcDocumentId AND DR.SrcDocumentType = 36\r\nINNER JOIN ContenidoPAF CP ON CP.Numero=PAF.Numero AND CP.Version=PAF.Version\r\nINNER JOIN EstadoModelosPAF EMP ON EMP.Numero=CP.Numero AND EMP.Version=CP.Version AND  EMP.Orden=CP.Orden\r\nWHERE ((PAF.Confirmado = 1 AND PAF.Type = 2 AND NOT EXISTS (SELECT * FROM PAF P2 WHERE P2.Numero = PAF.Numero AND P2.Type = 5)) OR PAF.Type = 5)\r\nAND PAF.ReadyToProdDate IS NOT NULL AND EMP.FechaEntradaTaller IS NULL AND PRO.ProjectCode = " + value + "\r\nUNION\r\nSELECT COUNT(*) AS NumberOfInstances, ISNULL(SUM(CP.PrecioUnitario),0.0) As Importe,N'Started' AS Status, 5 AS StatusPosition\r\nFROM PAF\r\nINNER JOIN DocumentRelationships DR ON DR.DestDocumentId = PAF.RowId AND (DR.DestDocumentType = 1 OR DR.DestDocumentType = 2)\r\nINNER JOIN Projects PRO ON PRO.ProjectId = DR.SrcDocumentId AND DR.SrcDocumentType = 36\r\nINNER JOIN ContenidoPAF CP ON CP.Numero=PAF.Numero AND CP.Version=PAF.Version\r\nINNER JOIN EstadoModelosPAF EMP ON EMP.Numero=CP.Numero AND EMP.Version=CP.Version AND  EMP.Orden=CP.Orden\r\nWHERE ((PAF.Confirmado = 1 AND PAF.Type = 2 AND NOT EXISTS (SELECT * FROM PAF P2 WHERE P2.Numero = PAF.Numero AND P2.Type = 5)) OR PAF.Type = 5)\r\nAND EMP.FechaEntradaTaller IS NOT NULL AND EMP.FechaSalidaTaller IS NULL AND PRO.ProjectCode = " + value + "\r\nUNION\r\nSELECT COUNT(*) AS NumberOfInstances, ISNULL(SUM(CP.PrecioUnitario),0.0) As Importe,N'Finished' AS Status, 6 AS StatusPosition\r\nFROM PAF\r\nINNER JOIN DocumentRelationships DR ON DR.DestDocumentId = PAF.RowId AND (DR.DestDocumentType = 1 OR DR.DestDocumentType = 2)\r\nINNER JOIN Projects PRO ON PRO.ProjectId = DR.SrcDocumentId AND DR.SrcDocumentType = 36\r\nINNER JOIN ContenidoPAF CP ON CP.Numero=PAF.Numero AND CP.Version=PAF.Version\r\nINNER JOIN EstadoModelosPAF EMP ON EMP.Numero=CP.Numero AND EMP.Version=CP.Version AND  EMP.Orden=CP.Orden\r\nWHERE ((PAF.Confirmado = 1 AND PAF.Type = 2 AND NOT EXISTS (SELECT * FROM PAF P2 WHERE P2.Numero = PAF.Numero AND P2.Type = 5)) OR PAF.Type = 5)\r\nAND EMP.FechaSalidaTaller IS NOT NULL AND EMP.ShippingDate IS NULL AND PRO.ProjectCode = " + value + "\r\nUNION\r\nSELECT COUNT(*) AS NumberOfInstances, ISNULL(SUM(CP.PrecioUnitario),0.0) As Importe,N'Sent' , 7 AS StatusPosition\r\nFROM PAF\r\nINNER JOIN DocumentRelationships DR ON DR.DestDocumentId = PAF.RowId AND (DR.DestDocumentType = 1 OR DR.DestDocumentType = 2)\r\nINNER JOIN Projects PRO ON PRO.ProjectId = DR.SrcDocumentId AND DR.SrcDocumentType = 36\r\nINNER JOIN ContenidoPAF CP ON CP.Numero=PAF.Numero AND CP.Version=PAF.Version\r\nINNER JOIN EstadoModelosPAF EMP ON EMP.Numero=CP.Numero AND EMP.Version=CP.Version AND  EMP.Orden=CP.Orden\r\nWHERE ((PAF.Confirmado = 1 AND PAF.Type = 2 AND NOT EXISTS (SELECT * FROM PAF P2 WHERE P2.Numero = PAF.Numero AND P2.Type = 5)) OR PAF.Type = 5)\r\nAND EMP.ShippingDate IS NOT NULL AND EMP.FechaEntrega IS NULL AND PRO.ProjectCode = " + value + "\r\nUNION\r\nSELECT COUNT(*) AS NumberOfInstances, ISNULL(SUM(CP.PrecioUnitario),0.0) As Importe,N'Delivered' AS Status, 8 AS StatusPosition\r\nFROM PAF\r\nINNER JOIN DocumentRelationships DR ON DR.DestDocumentId = PAF.RowId AND (DR.DestDocumentType = 1 OR DR.DestDocumentType = 2)\r\nINNER JOIN Projects PRO ON PRO.ProjectId = DR.SrcDocumentId AND DR.SrcDocumentType = 36\r\nINNER JOIN ContenidoPAF CP ON CP.Numero=PAF.Numero AND CP.Version=PAF.Version\r\nINNER JOIN EstadoModelosPAF EMP ON EMP.Numero=CP.Numero AND EMP.Version=CP.Version AND  EMP.Orden=CP.Orden\r\nWHERE ((PAF.Confirmado = 1 AND PAF.Type = 2 AND NOT EXISTS (SELECT * FROM PAF P2 WHERE P2.Numero = PAF.Numero AND P2.Type = 5)) OR PAF.Type = 5)\r\nAND EMP.FechaEntrega IS NOT NULL AND EMP.FechaMontaje IS NULL AND PRO.ProjectCode = " + value + "\r\nUNION\r\nSELECT COUNT(*) AS NumberOfInstances, ISNULL(SUM(CP.PrecioUnitario),0.0) As Importe,N'Mounted' AS Status, 9 AS StatusPosition\r\nFROM PAF\r\nINNER JOIN DocumentRelationships DR ON DR.DestDocumentId = PAF.RowId AND (DR.DestDocumentType = 1 OR DR.DestDocumentType = 2)\r\nINNER JOIN Projects PRO ON PRO.ProjectId = DR.SrcDocumentId AND DR.SrcDocumentType = 36\r\nINNER JOIN ContenidoPAF CP ON CP.Numero=PAF.Numero AND CP.Version=PAF.Version\r\nINNER JOIN EstadoModelosPAF EMP ON EMP.Numero=CP.Numero AND EMP.Version=CP.Version AND  EMP.Orden=CP.Orden\r\nWHERE ((PAF.Confirmado = 1 AND PAF.Type = 2 AND NOT EXISTS (SELECT * FROM PAF P2 WHERE P2.Numero = PAF.Numero AND P2.Type = 5)) OR PAF.Type = 5)\r\nAND EMP.FechaMontaje IS NOT NULL --AND PAF.FechaFacturado IS NULL -> Jaime: mientras no tengamos estado de facturado visible en la interfaz, quito esta condición porque sino las ventanas desaparecen cuando se facturan\r\nAND PRO.ProjectCode = " + value + "\r\nUNION\r\nSELECT Count(*) AS NumberOfInstances, ISNULL(SUM(CP.PrecioUnitario),0.0) As Importe, N'Invoiced' AS Status,10 AS StatusPosition\r\nFROM EstadoModelosPAF EMP\r\nINNER JOIN ContenidoPAF CP ON CP.Numero=EMP.Numero AND CP.Version=EMP.Version AND CP.Orden = EMP.Orden\r\nINNER JOIN PAF ON PAF.Numero = EMP.Numero AND PAF.Version = EMP.Version AND PAF.Type = 4\r\nINNER JOIN DocumentRelationships DR ON DR.DestDocumentId = PAF.RowId AND DR.SrcDocumentType = 36 AND (DR.DestDocumentType = 1 OR DR.DestDocumentType = 2)\r\nINNER JOIN Projects PRO ON PRO.ProjectId = DR.SrcDocumentId\r\nWHERE PRO.ProjectCode = " + value + "\r\nUNION\r\nSELECT Count(*) AS NumberOfInstances, ISNULL(SUM(CP.PrecioUnitario),0.0) As Importe, N'Total' AS Status,11 AS StatusPosition\r\nFROM EstadoModelosPAF EMP\r\nINNER JOIN ContenidoPAF CP ON CP.Numero=EMP.Numero AND CP.Version=EMP.Version AND CP.Orden = EMP.Orden\r\nINNER JOIN PAF ON PAF.Numero = EMP.Numero AND PAF.Version = EMP.Version AND PAF.Type = 2\r\nINNER JOIN DocumentRelationships DR ON DR.DestDocumentId = PAF.RowId AND DR.SrcDocumentType = 36 AND (DR.DestDocumentType = 1 OR DR.DestDocumentType = 2)\r\nINNER JOIN Projects PRO ON PRO.ProjectId = DR.SrcDocumentId\r\nWHERE PRO.ProjectCode = " + value + "\r\nORDER BY StatusPosition,Status\r\n";
		bool flag2 = false;
		SqlCommand sqlCommand = BuildSQLCommand(strQuery, CommandType.Text);
		SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
		XmlNode parentElem = null;
		if (sqlDataReader.HasRows)
		{
			parentElem = AddElementToNodeWithAttributes("Parameter", NodeCommand, "InstancesGroupedByStatus", "list");
		}
		while (sqlDataReader.Read())
		{
			string strAttributeValue = sqlDataReader.GetValue(0).ToString().TrimEnd();
			string strAttributeValue2 = sqlDataReader.GetDouble(1).ToString(NumberFormatInfo.InvariantInfo);
			string strAttributeValue3 = sqlDataReader.GetValue(2).ToString().TrimEnd();
			XmlNode parentElem2 = AddElementToNodeWithAttributes("Item", parentElem, "InstancesStatus", "list");
			AddElementToNodeWithAttributes("Item", parentElem2, "NumberOfInstances", "int", strAttributeValue);
			AddElementToNodeWithAttributes("Item", parentElem2, "Amount", "double", strAttributeValue2);
			AddElementToNodeWithAttributes("Item", parentElem2, "Status", "string", strAttributeValue3);
			flag2 = true;
		}
		sqlDataReader.Close();
		string text = ((!flag) ? " SELECT  0.0 AS TotalCost,N'WorkforceCost' AS Status, 4 AS StatusPosition " : ("\r\nSELECT  ISNULL(SUM(Amount*RealCost.WorkedTimeSeconds),0.0) AS TotalCost,\r\nN'WorkforceCost' AS Status, 4 AS StatusPosition\r\nFROM (\r\n    SELECT xtable.Number, xtable.Version, xtable.Position, a.value('@id', 'NCHAR(10)') AS Id, a.value('@place', 'NCHAR(30)') AS LabourPlace, a.value('@priceGroupId', 'INT') AS GroupId\r\n    FROM ( \r\n\t\t\tSELECT cpb.Numero AS Number, cpb.Version AS Version, cpb.Orden AS Position, dbo.Sales_GetItemDescriptiveXml ( cpb.Numero, cpb.Version, cpb.Orden ) as XMLDesc\r\n\t\t\tFROM ContenidoPAFBLob cpb\r\n\t\t\tINNER JOIN PAF ON cpb.Numero = PAF.Numero AND cpb.Version = PAF.Version \r\n\t\t\tLEFT OUTER JOIN DocumentRelationships DR ON DR.DestDocumentId = PAF.RowId AND DR.DestDocumentType = 1\r\n\t\t\tINNER JOIN Projects PRO ON PRO.ProjectId = DR.SrcDocumentId AND DR.SrcDocumentType = 36\r\n\t\t\tWHERE ((PAF.Confirmado = 1 AND PAF.Type = 2 AND NOT EXISTS (SELECT * FROM PAF P2 WHERE P2.Numero = PAF.Numero AND P2.Type = 5)) OR PAF.Type = 5) AND\r\n\t\t\tPRO.ProjectCode = " + value + "\r\n\t\t) xtable\r\n\tCROSS APPLY  xtable.XMLDesc.nodes('declare namespace dsc=\"ModelDescriptive\"; /Model/dsc:Model//*[local-name(.)=\"Workforce\"]') AS result(a) \r\n) Descriptive -- Table that returns the WorkForce nodes from the descriptive XML\r\nINNER JOIN (\r\n\tSELECT xtable.ProjectCode, xtable.Numero AS Number, xtable.Version AS Version, xtable.Orden AS Position, xtable.Cantidad AS Quantity , a.value('@id', 'NCHAR(10)') AS Id, a.value('@singleAmount', 'FLOAT') AS Amount\r\n\tFROM (\r\n\t\tSELECT PRO.ProjectCode, cpb.Numero, cpb.Version, cpb.Orden, cp.Cantidad, dbo.Sales_GetItemPricesXml ( cpb.Numero, cpb.Version, cpb.Orden ) AS XMLPrices\r\n\t\tFROM ContenidoPAFBLob cpb\r\n\t\tINNER JOIN PAF ON cpb.Numero = PAF.Numero AND cpb.Version = PAF.Version\r\n\t\tINNER JOIN ContenidoPAF cp ON cp.Numero = cpb.Numero AND cp.Version = cpb.Version AND cp.Orden = cpb.Orden\r\n\t\tLEFT OUTER JOIN DocumentRelationships DR ON DR.DestDocumentId = PAF.RowId AND DR.DestDocumentType = 1\r\n\t\tINNER JOIN Projects PRO ON PRO.ProjectId = DR.SrcDocumentId AND DR.SrcDocumentType = 36\r\n\t\tWHERE ((PAF.Confirmado = 1 AND PAF.Type = 2 AND NOT EXISTS (SELECT * FROM PAF P2 WHERE P2.Numero = PAF.Numero AND P2.Type = 5)) OR PAF.Type = 5) AND\r\n\t\tPRO.ProjectCode = " + value + "\r\n) xtable\r\nCROSS APPLY xtable.XMLPrices.nodes('declare namespace pro=\"PricesOutput\";\r\n      /Prices/pro:PriceEntity/pro:TariffChain[pro:TariffChainFlag[@id=\"chainForCost\"]]/pro:TariffChainResult/*[local-name(.)=\"LabourPlaceAmount\"]') AS result(a)\r\n\t\r\n\t) Costs ON Costs.Number = Descriptive.Number AND Costs.Version = Descriptive.Version AND Costs.Position = Descriptive.Position AND Costs.Id = Descriptive.Id \r\n\t-- Costs is the table that returns the WorkForce nodes from the prices XML\r\n\tINNER JOIN (\r\n\t\tselect salidas.ProjectCode,salidas.workercode,salidas.objectid,CAST((salidas.ExitDate-entradas.EntryDate)*86400 AS float) AS WorkedTimeSeconds\r\n\t\tfrom (\r\n\t\t\tselect\r\n\t\t\tPRO.ProjectCode,le.workercode,le.objectid,SUM(CAST(le.date AS float)) AS ExitDate \r\n\t\t\tfrom labourevents le \r\n\t\t\tLEFT OUTER JOIN DocumentRelationships DR ON DR.DestDocumentId = le.objectid \r\n\t\t\tINNER JOIN Projects PRO ON PRO.ProjectId = DR.SrcDocumentId AND DR.SrcDocumentType = 36\r\n\t\t\twhere (le.eventtype = 2 OR le.eventtype = 3) AND\r\n\t\t\tPRO.ProjectCode = " + value + " \r\n\t\t\tGROUP BY PRO.ProjectCode,le.workercode,le.objectid\r\n\t\t) salidas\r\n\t\tinner join (\r\n\t\t\tselect\r\n\t\t\tPRO.ProjectCode,le.workercode,le.objectid,SUM(CAST(le.date AS float)) AS EntryDate \r\n\t\t\tfrom labourevents le \r\n\t\t\tLEFT OUTER JOIN DocumentRelationships DR ON DR.DestDocumentId = le.objectid \r\n\t\t\tINNER JOIN Projects PRO ON PRO.ProjectId = DR.SrcDocumentId AND DR.SrcDocumentType = 36\r\n\t\t\twhere (le.eventtype = 1 OR le.eventtype = 4) AND\r\n\t\t\tPRO.ProjectCode = " + value + "\r\n\t\t\tGROUP BY PRO.ProjectCode,le.workercode,le.objectid\r\n\t\t) entradas\r\n\t\tON entradas.objectid=salidas.objectid \r\n\t) RealCost \r\n\tON Costs.ProjectCode = RealCost.ProjectCode\r\nLEFT OUTER JOIN PuestosMO pmo ON pmo.Nombre = Descriptive.LabourPlace -- Retrieving the labour place price group\r\nLEFT OUTER JOIN Groups g ON g.GroupId = ISNULL(Descriptive.GroupId,  pmo.IdGrupoPresupuestado) AND g.GroupType = 2\r\n"));
		strQuery = (sqlCommand.CommandText = "\r\nSELECT ISNULL(SUM(PAF.Importe),0.0) AS TotalCost,\r\nN'Sales' AS Status, 1 AS StatusPosition\r\nFROM PAF \r\nINNER JOIN DocumentRelationships DR ON DR.DestDocumentId = PAF.RowId AND DR.DestDocumentType = 1\r\nINNER JOIN Projects PRO ON PRO.ProjectId = DR.SrcDocumentId AND DR.SrcDocumentType = 36\r\nWHERE ((PAF.Confirmado = 1 AND PAF.Type = 2 AND NOT EXISTS (SELECT * FROM PAF P2 WHERE P2.Numero = PAF.Numero AND P2.Type = 5)) OR PAF.Type = 5)\r\nAND (PAF.FechaMontaje IS NOT NULL OR PAF.FechaFacturado IS NOT NULL) AND PRO.ProjectCode = " + value + "\r\nUNION\r\nSELECT ISNULL(SUM(PD.RO_TotalPacking * PD.UnitAmount), 0.0) AS TotalCost,\r\nN'Purchases' AS Status, 2 AS StatusPosition\r\nFROM Purchases PUR\r\nINNER JOIN PurchasesDetail PD ON PD.Number = PUR.Number AND PD.Numeration = PUR.Numeration\r\nINNER JOIN DocumentRelationships DR ON DR.DestDocumentId = PUR.DocumentId AND DR.DestDocumentType = 4\r\nINNER JOIN Projects PRO ON PRO.ProjectId = DR.SrcDocumentId AND DR.SrcDocumentType = 36\r\nINNER JOIN Numeraciones NUM ON NUM.id = PUR.Numeration AND NUM.Purchase = 1 AND NUM.DocumentType = 2\r\nWHERE PRO.ProjectCode = " + value + "\r\nUNION\r\nSELECT ISNULL(SUM(PAF.Importe),0.0) AS TotalCost,\r\nN'Production' AS Status, 3 AS StatusPosition\r\nFROM PAF\r\nINNER JOIN DocumentRelationships DR ON DR.DestDocumentId = PAF.RowId AND DR.DestDocumentType = 1\r\nINNER JOIN Projects PRO ON PRO.ProjectId = DR.SrcDocumentId AND DR.SrcDocumentType = 36\r\nWHERE ((PAF.Confirmado = 1 AND PAF.Type = 2 AND NOT EXISTS (SELECT * FROM PAF P2 WHERE P2.Numero = PAF.Numero AND P2.Type = 5)) OR PAF.Type = 5)\r\nAND PAF.FechaSalidaTaller IS NOT NULL AND PRO.ProjectCode = " + value + "\r\nUNION\r\n" + text + "\r\nORDER BY StatusPosition\r\n");
		sqlDataReader = sqlCommand.ExecuteReader();
		if (sqlDataReader.HasRows)
		{
			parentElem = AddElementToNodeWithAttributes("Parameter", NodeCommand, "GlobalStatus", "list");
		}
		while (sqlDataReader.Read())
		{
			string strAttributeValue4 = sqlDataReader.GetDouble(0).ToString(NumberFormatInfo.InvariantInfo);
			string strAttributeValue5 = sqlDataReader.GetValue(1).ToString().TrimEnd();
			XmlNode parentElem3 = AddElementToNodeWithAttributes("Item", parentElem, "OverallFigure", "list");
			AddElementToNodeWithAttributes("Item", parentElem3, "Amount", "double", strAttributeValue4);
			AddElementToNodeWithAttributes("Item", parentElem3, "Status", "string", strAttributeValue5);
			flag2 = true;
		}
		if (sqlCommand.Connection.State != 0)
		{
			sqlCommand.Connection.Close();
		}
		if (flag2)
		{
			m_rootResults.InnerXml += NodeCommand.OuterXml.Replace("cmd:Command", "cmd:CommandResult");
		}
		return flag2;
	}

	private bool Command_GetProductionLotDocumentDetails(XmlNode NodeCommand)
	{
		XmlNode xmlNode = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameDocId + "\"]", m_xmlNamespaceMgr);
		if (xmlNode == null)
		{
			AddError("Missing Parameter DocumentID", "GetProductionLotDocumentDetails");
			return false;
		}
		string value = xmlNode.Attributes["value"].Value;
		string strQuery = "SELECT Numero FROM Optimizaciones WHERE DocumentId = '" + value + "'";
		bool flag = false;
		SqlCommand sqlCommand = BuildSQLCommand(strQuery, CommandType.Text);
		SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
		while (sqlDataReader.Read())
		{
			string value2 = sqlDataReader.GetValue(0).ToString().TrimEnd();
			XmlElement xmlElement = NodeCommand.OwnerDocument.CreateElement("cmd", "Parameter", CommandNamespaceUri);
			xmlElement.SetAttribute("name", ParamNameDocumentNumber);
			xmlElement.SetAttribute("type", "int");
			xmlElement.SetAttribute("value", value2);
			NodeCommand.AppendChild(xmlElement);
			flag = true;
		}
		if (sqlCommand.Connection.State != 0)
		{
			sqlCommand.Connection.Close();
		}
		if (flag)
		{
			m_rootResults.InnerXml += NodeCommand.OuterXml.Replace("cmd:Command", "cmd:CommandResult");
		}
		return flag;
	}

	private bool Command_GetShippingLotDocumentDetails(XmlNode NodeCommand)
	{
		XmlNode xmlNode = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameDocId + "\"]", m_xmlNamespaceMgr);
		if (xmlNode == null)
		{
			AddError("Missing Parameter DocumentID", "GetShippingLotDocumentDetails");
			return false;
		}
		string value = xmlNode.Attributes["value"].Value;
		string strQuery = "SELECT ShippingLotCode FROM ShippingLots WHERE ShippingLotId = '" + value + "'";
		bool flag = false;
		SqlCommand sqlCommand = BuildSQLCommand(strQuery, CommandType.Text);
		SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
		while (sqlDataReader.Read())
		{
			string value2 = sqlDataReader.GetValue(0).ToString().TrimEnd();
			XmlElement xmlElement = NodeCommand.OwnerDocument.CreateElement("cmd", "Parameter", CommandNamespaceUri);
			xmlElement.SetAttribute("name", ParamNameDocumentNumber);
			xmlElement.SetAttribute("type", "int");
			xmlElement.SetAttribute("value", value2);
			NodeCommand.AppendChild(xmlElement);
			flag = true;
		}
		if (sqlCommand.Connection.State != 0)
		{
			sqlCommand.Connection.Close();
		}
		if (flag)
		{
			m_rootResults.InnerXml += NodeCommand.OuterXml.Replace("cmd:Command", "cmd:CommandResult");
		}
		return flag;
	}

	private bool Command_GetPurchaseDocumentDetails(XmlNode NodeCommand)
	{
		XmlNode xmlNode = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameDocId + "\"]", m_xmlNamespaceMgr);
		if (xmlNode == null)
		{
			AddError("Missing Parameter DocumentID", "GetPurchaseDocumentDetails");
			return false;
		}
		string value = xmlNode.Attributes["value"].Value;
		string strQuery = "\r\nSELECT \r\n\tP.Number,\r\n\tP.Numeration,\r\n\tRTRIM(PR.Nombre),\r\n\tN.DocumentType,\r\n\tdbo.getstrdocumenttype(N.DocumentType)\r\nFROM Purchases P\r\nINNER JOIN Numeraciones N ON P.Numeration = N.Id\r\nINNER JOIN Proveedores PR ON P.ProviderCode = PR.CodigoProveedor\r\nWHERE Number > 0 AND DocumentId = '" + value + "'";
		bool flag = false;
		SqlCommand sqlCommand = BuildSQLCommand(strQuery, CommandType.Text);
		SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
		while (sqlDataReader.Read())
		{
			string strAttributeValue = sqlDataReader.GetValue(0).ToString().TrimEnd();
			string strAttributeValue2 = sqlDataReader.GetValue(1).ToString().TrimEnd();
			string strAttributeValue3 = sqlDataReader.GetValue(2).ToString().TrimEnd();
			string strAttributeValue4 = sqlDataReader.GetValue(3).ToString().TrimEnd();
			string strAttributeValue5 = sqlDataReader.GetValue(4).ToString().TrimEnd();
			XmlNode xmlNode2 = AddElementToNodeWithAttributes("Parameter", NodeCommand, ParamNameDocumentNumber, "int", strAttributeValue);
			xmlNode2 = AddElementToNodeWithAttributes("Parameter", NodeCommand, ParamNameDocumentNumeration, "int", strAttributeValue2);
			xmlNode2 = AddElementToNodeWithAttributes("Parameter", NodeCommand, ParamNameDocumentSupplier, "string", strAttributeValue3);
			xmlNode2 = AddElementToNodeWithAttributes("Parameter", NodeCommand, ParamNameType, "int", strAttributeValue4);
			xmlNode2 = AddElementToNodeWithAttributes("Parameter", NodeCommand, ParamNameTypeName, "string", strAttributeValue5);
			flag = true;
		}
		if (sqlCommand.Connection.State != 0)
		{
			sqlCommand.Connection.Close();
		}
		if (flag)
		{
			m_rootResults.InnerXml += NodeCommand.OuterXml.Replace("cmd:Command", "cmd:CommandResult");
		}
		return flag;
	}

	private bool Command_GetSalesDocumentDetails(XmlNode NodeCommand)
	{
		XmlNode xmlNode = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameDocId + "\"]", m_xmlNamespaceMgr);
		XmlNode xmlNode2 = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameDocumentNumber + "\"]", m_xmlNamespaceMgr);
		XmlNode xmlNode3 = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameDocumentVersion + "\"]", m_xmlNamespaceMgr);
		bool flag = false;
		string strQuery;
		if (xmlNode != null)
		{
			string value = xmlNode.Attributes["value"].Value;
			strQuery = "SELECT RowId, Numero, Version, [Type], dbo.getstrdocumenttype([Type]), NumeroPedido, NombreVersion FROM PAF WHERE RowId = '" + value + "'";
		}
		else
		{
			if (xmlNode2 == null || xmlNode3 == null)
			{
				AddError("Missing Parameters DocumentID or Number/Version", "GetSalesDocumentDetails");
				return false;
			}
			string value2 = xmlNode.Attributes["value"].Value;
			string value3 = xmlNode.Attributes["value"].Value;
			strQuery = "SELECT RowId, Numero, Version, [Type], dbo.getstrdocumenttype([Type]), NumeroPedido, NombreVersion FROM PAF WHERE Numero = " + value2 + " AND Version = " + value3;
		}
		SqlCommand sqlCommand = BuildSQLCommand(strQuery, CommandType.Text);
		SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
		while (sqlDataReader.Read())
		{
			string strAttributeValue = sqlDataReader.GetValue(0).ToString().TrimEnd();
			string strAttributeValue2 = sqlDataReader.GetValue(1).ToString().TrimEnd();
			string strAttributeValue3 = sqlDataReader.GetValue(2).ToString().TrimEnd();
			string strAttributeValue4 = sqlDataReader.GetValue(3).ToString().TrimEnd();
			string strAttributeValue5 = sqlDataReader.GetValue(4).ToString().TrimEnd();
			string strAttributeValue6 = sqlDataReader.GetValue(5).ToString().TrimEnd();
			string strAttributeValue7 = sqlDataReader.GetValue(6).ToString().TrimEnd();
			XmlNode xmlNode4 = null;
			if (xmlNode != null)
			{
				xmlNode4 = AddElementToNodeWithAttributes("Parameter", NodeCommand, ParamNameDocumentNumber, "int", strAttributeValue2);
				xmlNode4 = AddElementToNodeWithAttributes("Parameter", NodeCommand, ParamNameDocumentVersion, "int", strAttributeValue3);
				flag = true;
			}
			else if (xmlNode2 != null && xmlNode3 != null)
			{
				xmlNode4 = AddElementToNodeWithAttributes("Parameter", NodeCommand, ParamNameDocId, "string", strAttributeValue);
				flag = true;
			}
			xmlNode4 = AddElementToNodeWithAttributes("Parameter", NodeCommand, ParamNameType, "int", strAttributeValue4);
			xmlNode4 = AddElementToNodeWithAttributes("Parameter", NodeCommand, ParamNameTypeName, "string", strAttributeValue5);
			xmlNode4 = AddElementToNodeWithAttributes("Parameter", NodeCommand, ParamNameOrderNumber, "int", strAttributeValue6);
			xmlNode4 = AddElementToNodeWithAttributes("Parameter", NodeCommand, ParamNameVersionName, "string", strAttributeValue7);
		}
		if (sqlCommand.Connection.State != 0)
		{
			sqlCommand.Connection.Close();
		}
		if (flag)
		{
			m_rootResults.InnerXml += NodeCommand.OuterXml.Replace("cmd:Command", "cmd:CommandResult");
		}
		return flag;
	}

	private bool Command_GetSalesDocumentPerGroupLabourCosts(XmlNode NodeCommand)
	{
		XmlNode xmlNode = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameDocumentNumber + "\"]", m_xmlNamespaceMgr);
		bool flag = false;
		if (xmlNode == null)
		{
			AddError("Missing Parameter in Sales Document: Number", "GetSalesDocumentPerGroupLabourCosts");
			return false;
		}
		string value = xmlNode.Attributes["value"].Value;
		xmlNode = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameDocumentVersion + "\"]", m_xmlNamespaceMgr);
		if (xmlNode == null)
		{
			AddError("Missing Parameter in Sales Document: Version", "GetSalesDocumentPerGroupLabourCosts");
			return false;
		}
		string value2 = xmlNode.Attributes["value"].Value;
		string strQuery = "\r\n--Select work labour cost for estimating salesdoc for price policy from paf xml contents\r\nSELECT ISNULL(g.GroupId, 1) AS GroupId, SUM(CASE WHEN Descriptive.LabourPlace IS NULL THEN 0 ELSE Costs.Amount*Costs.Quantity END) AS LaborCost\r\nFROM (\r\n      SELECT xtable.Number, xtable.Version, xtable.Position, a.value('@id', 'NCHAR(10)') AS Id, a.value('@place', 'NCHAR(30)') AS LabourPlace, a.value('@priceGroupId', 'INT') AS GroupId\r\n      FROM \r\n\t\t(\r\n\t\t\tSELECT cpb.Numero AS Number, cpb.Version AS Version, cpb.Orden AS Position, dbo.Sales_GetItemDescriptiveXml ( cpb.Numero, cpb.Version, cpb.Orden ) as XMLDesc\r\n\t\t\tFROM ContenidoPAFBLob cpb\r\n\t\t\tWHERE cpb.Numero = " + value + " AND cpb.Version = " + value2 + "\r\n\t\t) xtable\r\n\t\tCROSS APPLY xtable.XmlDesc.nodes('declare namespace dsc=\"ModelDescriptive\"; Model/dsc:Model//*[local-name(.)=\"Workforce\"]') AS result(a) \r\n) Descriptive -- Table that returns the WorkForce nodes from the descriptive XML\r\nINNER JOIN (\r\n\tSELECT xTable.Number, xTable.Version, xTable.Position, xTable.Quantity, a.value('@id', 'NCHAR(10)') AS Id, a.value('@amount', 'FLOAT') AS Amount\r\n\tFROM (\r\n\t\t\tSELECT cp.Numero AS Number, cp.Version AS Version, cp.Orden AS Position, cp.Cantidad AS Quantity, dbo.Sales_GetItemPricesXml ( cp.Numero, cp.Version, cp.Orden ) AS XmlPrices\r\n\t\t\tFROM ContenidoPAF cp\r\n\t\t\tWHERE cp.Numero = " + value + " AND cp.Version = " + value2 + "\r\n\t) xtable\r\n\tCROSS APPLY xtable.XMLPrices.nodes('declare namespace pro=\"PricesOutput\"; /Prices/pro:PriceEntity/pro:TariffChain[pro:TariffChainFlag[@id=\"chainForCost\"]]/pro:TariffChainResult/*[local-name(.)=\"LabourPlaceAmount\"]') AS result(a)\r\n) Costs ON Costs.Number = Descriptive.Number AND Costs.Version = Descriptive.Version AND Costs.Position = Descriptive.Position AND Costs.Id = Descriptive.Id -- Table that returns the WorkForce nodes from the prices XML\r\nLEFT OUTER JOIN PuestosMO pmo ON pmo.Nombre = Descriptive.LabourPlace -- Retrieving the labour place price group\r\nLEFT OUTER JOIN Groups g ON g.GroupId = ISNULL(Descriptive.GroupId,  pmo.IdGrupoPresupuestado) AND g.GroupType = 2\r\nGROUP BY ISNULL(g.GroupId, 1)\r\n";
		XmlNode parentElem = AddElementToNodeWithAttributes("Parameter", NodeCommand, ParamNameGroups, "list");
		SqlCommand sqlCommand = BuildSQLCommand(strQuery, CommandType.Text);
		SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
		flag = true;
		while (sqlDataReader.Read())
		{
			string strAttributeValue = sqlDataReader.GetValue(0).ToString().TrimEnd();
			string strAttributeValue2 = sqlDataReader.GetDouble(1).ToString(NumberFormatInfo.InvariantInfo);
			XmlNode parentElem2 = AddElementToNodeWithAttributes("Item", parentElem, ItemNamePriceGroup, "list");
			AddElementToNodeWithAttributes("Item", parentElem2, ItemNameCode, "string", strAttributeValue);
			AddElementToNodeWithAttributes("Item", parentElem2, ItemNameLaborCost, "string", strAttributeValue2);
			flag = true;
		}
		if (sqlCommand.Connection.State != 0)
		{
			sqlCommand.Connection.Close();
		}
		if (flag)
		{
			m_rootResults.InnerXml += NodeCommand.OuterXml.Replace("cmd:Command", "cmd:CommandResult");
		}
		return flag;
	}

	private bool Command_GetSalesDocumentPerGroupMaterialCosts(XmlNode NodeCommand)
	{
		XmlNode xmlNode = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameDocumentNumber + "\"]", m_xmlNamespaceMgr);
		bool flag = false;
		if (xmlNode == null)
		{
			AddError("Missing Parameter in Sales Document: Number", "GetSalesDocumentPerGroupLabourCosts");
			return false;
		}
		string value = xmlNode.Attributes["value"].Value;
		xmlNode = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameDocumentVersion + "\"]", m_xmlNamespaceMgr);
		if (xmlNode == null)
		{
			AddError("Missing Parameter in Sales Document: Version", "GetSalesDocumentPerGroupLabourCosts");
			return false;
		}
		string value2 = xmlNode.Attributes["value"].Value;
		string strQuery = "\r\n--Select purchase groups paf detail joining xml material prices and purchasegroup-material\r\nSELECT ISNULL(mbpg2.GroupCode, 0) AS GroupCode, ISNULL(Descriptive.Color, '') AS Color, CASE WHEN mbpg2.GroupCode IS NULL THEN '' ELSE ISNULL(mbpg2.Nombre, '') END AS SupplierName, SUM(CASE WHEN Descriptive.Material IS NULL THEN 0 ELSE Costs.Amount*Costs.Quantity END) AS MaterialCost\r\nFROM (\r\n      SELECT xtable.Number, xtable.Version, xtable.Position, a.value('@id', 'NCHAR(10)') AS Id, a.value('@material', 'NCHAR(25)') AS Material, a.value('@color', 'NCHAR(50)') AS Color\r\n\t  FROM\r\n\t  ( \r\n\t\t\tSELECT cpb.Numero AS Number, cpb.Version as Version, cpb. Orden AS Position, dbo.Sales_GetItemDescriptiveXml ( cpb.Numero, cpb.Version, cpb.Orden ) as XMLDesc\r\n\t\t\tFROM ContenidoPAFBLob cpb\r\n\t\t\tWHERE cpb.Numero = " + value + " AND cpb.Version = " + value2 + "\r\n\t  ) xtable\r\n\t  CROSS APPLY xtable.XMLDesc.nodes('declare namespace dsc=\"ModelDescriptive\"; /Model/dsc:Model//*[local-name(.)=\"GeneratedMaterial\"]') AS result(a) \r\n) Descriptive -- Table that returns the WorkForce nodes from the descriptive XML\r\nINNER JOIN (\r\n\tSELECT xtable.Number, xtable.Version, xtable.Position, xtable.Quantity, a.value('@id', 'NCHAR(10)') AS Id, a.value('@amount', 'FLOAT') AS Amount\r\n\tFROM \r\n\t(\r\n\t\tSELECT cp.Numero AS Number, cp.Version AS Version, cp.Orden AS Position, cp.Cantidad AS Quantity, dbo.Sales_GetItemPricesXml ( cp.Numero, cp.Version, cp.Orden ) AS XMLPrices\r\n\t\tFROM ContenidoPAF cp \r\n        WHERE cp.Numero = " + value + " AND cp.Version = " + value2 + "\r\n      ) xtable\r\n      CROSS APPLY xtable.XMLPrices.nodes('declare namespace pro=\"PricesOutput\"; /Prices/pro:PriceEntity/pro:TariffChain[pro:TariffChainFlag[@id=\"chainForCost\"]]/pro:TariffChainResult/*[local-name(.)=\"GeneratedMaterialAmount\"]') AS result(a)\r\n) Costs ON Costs.Number = Descriptive.Number AND Costs.Version = Descriptive.Version AND Costs.Position = Descriptive.Position AND Costs.Id = Descriptive.Id -- Table that returns the WorkForce nodes from the prices XML\r\nINNER JOIN Materiales m ON m.Referencia = Descriptive.Material \r\nINNER JOIN MaterialesBase mb ON mb.ReferenciaBase = m.ReferenciaBase\r\nLEFT OUTER JOIN \r\n(SELECT mbpg.basereferenceid,prov.codigoproveedor,pg.GroupCode,prov.Nombre FROM MaterialBasePurchaseGroups mbpg \r\nINNER JOIN PurchaseGroups pg ON mbpg.purchasegroupid=pg.rowid\r\nINNER JOIN Proveedores prov ON pg.supplierid=prov.rowid) mbpg2 \r\nON mb.rowid=mbpg2.basereferenceid  AND mb.codigoproveedor = mbpg2.codigoproveedor\r\nGROUP BY ISNULL(mbpg2.GroupCode, 0), CASE WHEN mbpg2.GroupCode IS NULL THEN '' ELSE ISNULL(mbpg2.Nombre, '') END, ISNULL(Descriptive.Color, '')\r\n";
		XmlNode parentElem = AddElementToNodeWithAttributes("Parameter", NodeCommand, ParamNameGroups, "list");
		SqlCommand sqlCommand = BuildSQLCommand(strQuery, CommandType.Text);
		SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
		flag = true;
		while (sqlDataReader.Read())
		{
			string strAttributeValue = sqlDataReader.GetValue(0).ToString().TrimEnd();
			string strAttributeValue2 = sqlDataReader.GetValue(1).ToString().TrimEnd();
			string strAttributeValue3 = sqlDataReader.GetValue(2).ToString().TrimEnd();
			string strAttributeValue4 = sqlDataReader.GetDouble(3).ToString(NumberFormatInfo.InvariantInfo);
			XmlNode parentElem2 = AddElementToNodeWithAttributes("Item", parentElem, ItemNamePurchaseGroup, "list");
			AddElementToNodeWithAttributes("Item", parentElem2, ItemNameCode, "string", strAttributeValue);
			AddElementToNodeWithAttributes("Item", parentElem2, ItemNameProviderName, "string", strAttributeValue3);
			AddElementToNodeWithAttributes("Item", parentElem2, ItemNameMaterialCost, "double", strAttributeValue4);
			AddElementToNodeWithAttributes("Item", parentElem2, ItemNameColor, "string", strAttributeValue2);
			flag = true;
		}
		if (sqlCommand.Connection.State != 0)
		{
			sqlCommand.Connection.Close();
		}
		if (flag)
		{
			m_rootResults.InnerXml += NodeCommand.OuterXml.Replace("cmd:Command", "cmd:CommandResult");
		}
		return flag;
	}

	private bool Command_GetSalesDocumentPriceGroupDetails(XmlNode NodeCommand)
	{
		XmlNode xmlNode = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameDocId + "\"]", m_xmlNamespaceMgr);
		bool flag = false;
		if (xmlNode != null)
		{
			string value = xmlNode.Attributes["value"].Value;
			string strQuery = "SELECT p3.pricegroupid, SUM(p3.materialcost*p2.Cantidad), \r\nSUM(p3.laborcost*p2.Cantidad) FROM PAF p1\r\nINNER JOIN ContenidoPAF p2 ON  p1.numero = p2.numero and p1.version = p2.version\r\nINNER JOIN PAFDetailPriceGroups p3 ON p2.idpos=p3.idpos\r\nwhere p1.rowid = '" + value + "'\r\nGROUP BY p3.pricegroupid";
			XmlElement xmlElement = NodeCommand.OwnerDocument.CreateElement("cmd", "Parameter", CommandNamespaceUri);
			xmlElement.SetAttribute("name", ParamNameGroups);
			xmlElement.SetAttribute("type", "list");
			NodeCommand.AppendChild(xmlElement);
			SqlCommand sqlCommand = BuildSQLCommand(strQuery, CommandType.Text);
			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			while (sqlDataReader.Read())
			{
				string value2 = sqlDataReader.GetValue(0).ToString().TrimEnd();
				string value3 = sqlDataReader.GetDouble(1).ToString(NumberFormatInfo.InvariantInfo);
				string value4 = sqlDataReader.GetDouble(2).ToString(NumberFormatInfo.InvariantInfo);
				XmlElement xmlElement2 = NodeCommand.OwnerDocument.CreateElement("cmd", "Item", CommandNamespaceUri);
				xmlElement2.SetAttribute("name", ItemNamePriceGroup);
				xmlElement2.SetAttribute("type", "list");
				xmlElement.AppendChild(xmlElement2);
				XmlElement xmlElement3 = NodeCommand.OwnerDocument.CreateElement("cmd", "Item", CommandNamespaceUri);
				xmlElement3.SetAttribute("name", ItemNameCode);
				xmlElement3.SetAttribute("type", "string");
				xmlElement3.SetAttribute("value", value2);
				xmlElement2.AppendChild(xmlElement3);
				xmlElement3 = NodeCommand.OwnerDocument.CreateElement("cmd", "Item", CommandNamespaceUri);
				xmlElement3.SetAttribute("name", ItemNameMaterialCost);
				xmlElement3.SetAttribute("type", "string");
				xmlElement3.SetAttribute("value", value3);
				xmlElement2.AppendChild(xmlElement3);
				xmlElement3 = NodeCommand.OwnerDocument.CreateElement("cmd", "Item", CommandNamespaceUri);
				xmlElement3.SetAttribute("name", ItemNameLaborCost);
				xmlElement3.SetAttribute("type", "string");
				xmlElement3.SetAttribute("value", value4);
				xmlElement2.AppendChild(xmlElement3);
				flag = true;
			}
			if (sqlCommand.Connection.State != 0)
			{
				sqlCommand.Connection.Close();
			}
			if (flag)
			{
				m_rootResults.InnerXml += NodeCommand.OuterXml.Replace("cmd:Command", "cmd:CommandResult");
			}
			return flag;
		}
		AddError("Missing Parameters DocumentID", "GetSalesDocumentDetails");
		return false;
	}

	private bool Command_GetServiceInformation(XmlNode NodeCommand)
	{
		return true;
	}

	private XmlNode AddElementToNodeWithAttributes(string strElemType, XmlNode parentElem, string strAttributeName, string strAttributeType)
	{
		return AddElementToNodeWithAttributes(strElemType, parentElem, strAttributeName, strAttributeType, null);
	}

	private XmlNode AddElementToNodeWithAttributes(string strElemType, XmlNode parentElem, string strAttributeName, string strAttributeType, string strAttributeValue)
	{
		XmlElement xmlElement = parentElem.OwnerDocument.CreateElement("cmd", strElemType, CommandNamespaceUri);
		xmlElement.SetAttribute("name", strAttributeName);
		xmlElement.SetAttribute("type", strAttributeType);
		if (strAttributeValue != null)
		{
			xmlElement.SetAttribute("value", strAttributeValue);
		}
		parentElem.AppendChild(xmlElement);
		return xmlElement;
	}

	private bool Command_GetWarehouseDocumentDetails(XmlNode NodeCommand)
	{
		XmlNode xmlNode = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameDocId + "\"]", m_xmlNamespaceMgr);
		if (xmlNode == null)
		{
			AddError("Missing Parameter DocumentID", "GetWarehouseDocumentDetails");
			return false;
		}
		string value = xmlNode.Attributes["value"].Value;
		string strQuery = "SELECT DocumentCode, ExitWarehouse,EntryWarehouse FROM WarehouseDocuments WHERE DocumentId = '" + value + "'";
		bool flag = false;
		SqlCommand sqlCommand = BuildSQLCommand(strQuery, CommandType.Text);
		SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
		while (sqlDataReader.Read())
		{
			string value2 = sqlDataReader.GetValue(0).ToString().TrimEnd();
			string value3 = sqlDataReader.GetValue(1).ToString().TrimEnd();
			string value4 = sqlDataReader.GetValue(2).ToString().TrimEnd();
			XmlElement xmlElement = NodeCommand.OwnerDocument.CreateElement("cmd", "Parameter", CommandNamespaceUri);
			xmlElement.SetAttribute("name", ParamNameDocumentCode);
			xmlElement.SetAttribute("type", "int");
			xmlElement.SetAttribute("value", value2);
			NodeCommand.AppendChild(xmlElement);
			xmlElement = NodeCommand.OwnerDocument.CreateElement("cmd", "Parameter", CommandNamespaceUri);
			xmlElement.SetAttribute("name", ParamNameEntry);
			xmlElement.SetAttribute("type", "int");
			xmlElement.SetAttribute("value", value3);
			NodeCommand.AppendChild(xmlElement);
			xmlElement = NodeCommand.OwnerDocument.CreateElement("cmd", "Parameter", CommandNamespaceUri);
			xmlElement.SetAttribute("name", ParamNameExit);
			xmlElement.SetAttribute("type", "int");
			xmlElement.SetAttribute("value", value4);
			NodeCommand.AppendChild(xmlElement);
			flag = true;
		}
		if (sqlCommand.Connection.State != 0)
		{
			sqlCommand.Connection.Close();
		}
		if (flag)
		{
			m_rootResults.InnerXml += NodeCommand.OuterXml.Replace("cmd:Command", "cmd:CommandResult");
		}
		return flag;
	}

	private bool Command_RemoveProject(XmlNode NodeCommand)
	{
		string empty = string.Empty;
		try
		{
			empty = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameProjectCode + "\"]", m_xmlNamespaceMgr).Attributes["value"].Value;
			string strQuery = "[dbo].[pa_Projects_RemoveProject]";
			SqlCommand sqlCommand = BuildSQLCommand(strQuery, CommandType.StoredProcedure);
			SqlParameter sqlParameter = sqlCommand.Parameters.Add("RC", SqlDbType.Int);
			sqlParameter.Direction = ParameterDirection.ReturnValue;
			SqlParameter sqlParameter2 = sqlCommand.Parameters.Add("@nProjectCode", SqlDbType.BigInt);
			sqlParameter2.Direction = ParameterDirection.Input;
			sqlParameter2.Value = Convert.ToInt64(empty);
			sqlParameter = sqlCommand.Parameters.Add("@xmlMessages", SqlDbType.Xml, 4000);
			sqlParameter.Direction = ParameterDirection.Output;
			sqlCommand.Transaction = sqlCommand.Connection.BeginTransaction();
			sqlCommand.ExecuteNonQuery();
			int num = (int)sqlCommand.Parameters["RC"].Value;
			if (num == 0)
			{
				sqlCommand.Transaction.Commit();
			}
			else
			{
				sqlCommand.Transaction.Rollback();
			}
			if (sqlCommand.Parameters["@xmlMessages"].Value != DBNull.Value)
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.LoadXml(sqlCommand.Parameters["@xmlMessages"].Value.ToString());
				AppendMessages(xmlDocument.DocumentElement.InnerXml);
			}
			if (sqlCommand.Connection.State != 0)
			{
				sqlCommand.Connection.Close();
			}
			return num == 0;
		}
		catch (Exception ex)
		{
			AddError(ex.Message, ex.Source);
			return false;
		}
	}

	private bool Command_RemoveProjectDocument(XmlNode NodeCommand)
	{
		bool result = false;
		XmlNode xmlNode = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameDocId + "\"]", m_xmlNamespaceMgr);
		string text = ((xmlNode != null) ? xmlNode.Attributes["value"].Value : string.Empty);
		xmlNode = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameProjectId + "\"]", m_xmlNamespaceMgr);
		string text2 = ((xmlNode != null) ? xmlNode.Attributes["value"].Value : string.Empty);
		xmlNode = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameProjectCode + "\"]", m_xmlNamespaceMgr);
		string text3 = ((xmlNode != null) ? xmlNode.Attributes["value"].Value : string.Empty);
		if (string.IsNullOrEmpty(text))
		{
			return false;
		}
		if (string.IsNullOrEmpty(text2) && string.IsNullOrEmpty(text3))
		{
			return false;
		}
		string strQuery = $"DELETE FROM [dbo].[DocumentRelationships]\r\nWHERE SrcDocumentId = '{text2}' AND DestDocumentId = '{text}' AND SrcDocumentType = 36";
		if (!string.IsNullOrEmpty(text3) && string.IsNullOrEmpty(text2))
		{
			strQuery = $"DELETE FROM [dbo].[DocumentRelationships]\r\nFROM [dbo].[DocumentRelationships]\r\nINNER JOIN [dbo].[Projects] ON ProjectId = SrcDocumentId\r\nWHERE ProjectCode = {text3} AND DestDocumentId = '{text}' AND SrcDocumentType = 36";
		}
		SqlCommand sqlCommand = BuildSQLCommand(strQuery, CommandType.Text);
		int num = sqlCommand.ExecuteNonQuery();
		if (num > 0)
		{
			result = true;
			m_rootResults.InnerXml += NodeCommand.OuterXml.Replace("cmd:Command", "cmd:CommandResult");
		}
		if (sqlCommand.Connection.State != 0)
		{
			sqlCommand.Connection.Close();
		}
		return result;
	}

	private bool Command_RemoveProjectFolder(XmlNode NodeCommand)
	{
		bool result = false;
		XmlNode xmlNode = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameFolderId + "\"]", m_xmlNamespaceMgr);
		string text = ((xmlNode != null) ? xmlNode.Attributes["value"].Value : string.Empty);
		if (string.IsNullOrEmpty(text))
		{
			return false;
		}
		string strQuery = $"DELETE FROM [dbo].[ObjectFolders]\r\nWHERE FolderId = '{text}' ";
		SqlCommand sqlCommand = BuildSQLCommand(strQuery, CommandType.Text);
		int num = sqlCommand.ExecuteNonQuery();
		if (num > 0)
		{
			result = true;
			m_rootResults.InnerXml += NodeCommand.OuterXml.Replace("cmd:Command", "cmd:CommandResult");
		}
		if (sqlCommand.Connection.State != 0)
		{
			sqlCommand.Connection.Close();
		}
		return result;
	}

	private bool Command_RenameProjectFolder(XmlNode NodeCommand)
	{
		bool result = false;
		XmlNode xmlNode = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameFolderId + "\"]", m_xmlNamespaceMgr);
		string text = ((xmlNode != null) ? xmlNode.Attributes["value"].Value : string.Empty);
		xmlNode = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameCaption + "\"]", m_xmlNamespaceMgr);
		string arg = ((xmlNode != null) ? xmlNode.Attributes["value"].Value : string.Empty);
		if (string.IsNullOrEmpty(text))
		{
			return false;
		}
		string strQuery = $"UPDATE [ObjectFolders] SET Caption = N'{arg}' \r\nWHERE FolderId = '{text}' ";
		SqlCommand sqlCommand = BuildSQLCommand(strQuery, CommandType.Text);
		int num = sqlCommand.ExecuteNonQuery();
		if (num > 0)
		{
			result = true;
			m_rootResults.InnerXml += NodeCommand.OuterXml.Replace("cmd:Command", "cmd:CommandResult");
		}
		if (sqlCommand.Connection.State != 0)
		{
			sqlCommand.Connection.Close();
		}
		return result;
	}

	private bool Command_SQLCommand(XmlNode NodeCommand)
	{
		bool flag = true;
		string cmdText = NodeCommand.InnerText.Normalize();
		XmlElement xmlElement = NodeCommand.OwnerDocument.CreateElement("cmd", "Parameter", CommandNamespaceUri);
		xmlElement.SetAttribute("name", ParamNameDBObjectList);
		xmlElement.SetAttribute("type", "list");
		NodeCommand.AppendChild(xmlElement);
		SqlConnection sqlConnection = new SqlConnection(_strConnectionString);
		sqlConnection.Open();
		SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection);
		sqlCommand.CommandType = CommandType.Text;
		SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
		while (sqlDataReader.Read())
		{
			XmlElement xmlElement2 = NodeCommand.OwnerDocument.CreateElement("cmd", "Item", CommandNamespaceUri);
			xmlElement2.SetAttribute("name", ItemNameDBObject);
			xmlElement2.SetAttribute("type", "list");
			xmlElement.AppendChild(xmlElement2);
			for (int i = 0; i < sqlDataReader.FieldCount; i++)
			{
				string text = (sqlDataReader.IsDBNull(i) ? string.Empty : Convert.ToString(sqlDataReader.GetValue(i), CultureInfo.InvariantCulture));
				XmlElement xmlElement3 = NodeCommand.OwnerDocument.CreateElement("cmd", "Item", CommandNamespaceUri);
				xmlElement3.SetAttribute("name", "Column" + i);
				xmlElement3.SetAttribute("type", sqlDataReader.GetFieldType(i).ToString());
				xmlElement3.SetAttribute("value", text.TrimEnd());
				xmlElement2.AppendChild(xmlElement3);
			}
			flag = true;
		}
		if (sqlDataReader.RecordsAffected > 0)
		{
			flag = true;
		}
		if (sqlCommand.Connection.State != 0)
		{
			sqlCommand.Connection.Close();
		}
		if (flag)
		{
			m_rootResults.InnerXml += NodeCommand.OuterXml.Replace("cmd:Command", "cmd:CommandResult");
		}
		return flag;
	}

	private bool Command_UpdateProjectTariffException(XmlNode NodeCommand)
	{
		bool result = false;
		string value = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameProjectCode + "\"]", m_xmlNamespaceMgr).Attributes["value"].Value;
		string value2 = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameRedefinedTariffName + "\"]", m_xmlNamespaceMgr).Attributes["value"].Value;
		string value3 = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameGroupRowId + "\"]", m_xmlNamespaceMgr).Attributes["value"].Value;
		string value4 = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameValue + "\"]", m_xmlNamespaceMgr).Attributes["value"].Value;
		string text = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameColor + "\"]", m_xmlNamespaceMgr).Attributes["value"].Value.Replace("'", "''");
		string text2;
		string text3;
		if (text != "")
		{
			text2 = "";
			text3 = "INNER JOIN Colores c ON c.RowId = tc.ColorRowId AND c.Nombre = N'" + text + "' ";
		}
		else
		{
			text2 = "AND (tc.ColorRowId IS NULL) ";
			text3 = "";
		}
		string strQuery = "IF (\r\nSELECT COUNT(*) FROM TariffsContent tc \r\nINNER JOIN Tariff t ON tc.TariffRowId = t.RowId \r\nINNER JOIN Tariff t2 ON tc.RedefinedTariffRowId = t2.RowId\r\nINNER JOIN PurchaseGroups g ON tc.PurchaseGroupRowId = g.RowId " + text3 + "\r\nWHERE t.[Name]=N'" + value + "' \r\nAND t.[Type]=10 \r\nAND (tc.[Type]=11) \r\nAND tc.PurchaseGroupRowId = '" + value3 + "'\r\nAND t2.[Name]=N'" + value2 + "' " + text2 + "\r\n) > 0\r\nUPDATE TariffsContent SET [Value] = " + value4 + ", CoefficientType=1\r\nFROM TariffsContent tc \r\nINNER JOIN Tariff t ON tc.TariffRowId = t.RowId\r\nINNER JOIN Tariff t2 ON tc.RedefinedTariffRowId = t2.RowId " + text3 + "\r\nWHERE t.[Name]=N'" + value + "' \r\nAND t.[Type]=10 \r\nAND (tc.[Type]=11) \r\nAND tc.PurchaseGroupRowId = '" + value3 + "'\r\nAND t2.[Name]=N'" + value2 + "' " + text2 + "\r\nELSE\r\nBEGIN\r\nINSERT INTO TariffsContent ( [Type], Provider, TariffRowId, RedefinedTariffRowId, PurchaseGroupRowId, ColorRowId, [Value] )\r\nSELECT 11, prov.CodigoProveedor, t.Rowid, t2.RowId, '" + value3 + "', c.RowId, " + value4 + " FROM Tariff t \r\nCROSS JOIN Tariff t2\r\nCROSS JOIN PurchaseGroups pg \r\nINNER JOIN Proveedores prov ON prov.RowId=pg.SupplierId\r\nLEFT OUTER JOIN Colores c ON c.Nombre = N'" + text + "'\r\nWHERE t.[Name]=N'" + value + "' \r\nAND t.[Type]=10 \r\nAND t2.[Name]=N'" + value2 + "'\r\nAND pg.rowid='" + value3 + "'\r\nEND";
		string strQuery2 = "\r\nSELECT [GroupType] FROM Groups WHERE RowId='" + value3 + "'";
		int num = 0;
		SqlCommand sqlCommand = BuildSQLCommand(strQuery2, CommandType.Text);
		object obj = sqlCommand.ExecuteScalar();
		if (obj != null)
		{
			num = (int)obj;
			if (num == 2)
			{
				strQuery = "IF (\r\nSELECT COUNT(*) FROM TariffsContent tc \r\nINNER JOIN Tariff t ON tc.TariffRowId = t.RowId \r\nINNER JOIN Tariff t2 ON tc.RedefinedTariffRowId = t2.RowId\r\nINNER JOIN Groups g ON tc.PriceGroupRowId = g.RowId \r\nWHERE t.[Name]='" + value + "' \r\nAND t.[Type]=10 \r\nAND (tc.[Type]=10) \r\nAND tc.PriceGroupRowId = '" + value3 + "'\r\nAND t2.[Name]='" + value2 + "'\r\n) > 0\r\nUPDATE TariffsContent SET [Value] = " + value4 + ", CoefficientType=1\r\nFROM TariffsContent tc \r\nINNER JOIN Tariff t ON tc.TariffRowId = t.RowId\r\nINNER JOIN Tariff t2 ON tc.RedefinedTariffRowId = t2.RowId\r\nWHERE t.[Name]='" + value + "' \r\nAND t.[Type]=10 \r\nAND (tc.[Type]=10) \r\nAND tc.PriceGroupRowId = '" + value3 + "'\r\nAND t2.[Name]='" + value2 + "'\r\nELSE\r\nBEGIN\r\nINSERT INTO TariffsContent ( [Type], TariffRowId, RedefinedTariffRowId, PriceGroupRowId, [Value] )\r\nSELECT 10, t.Rowid, t2.RowId, '" + value3 + "', " + value4 + " FROM Tariff t \r\nCROSS JOIN Tariff t2\r\nWHERE t.[Name]=N'" + value + "' \r\nAND t.[Type]=10 \r\nAND t2.[Name]=N'" + value2 + "'\r\nEND";
			}
		}
		sqlCommand = BuildSQLCommand(strQuery, CommandType.Text);
		num = sqlCommand.ExecuteNonQuery();
		if (0 < num)
		{
			result = true;
		}
		if (sqlCommand.Connection.State != 0)
		{
			sqlCommand.Connection.Close();
		}
		return result;
	}

	private bool Command_UpdateProjectDetails(XmlNode NodeCommand)
	{
		bool result = false;
		string value = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameProjectCode + "\"]", m_xmlNamespaceMgr).Attributes["value"].Value;
		string value2 = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameProjectName + "\"]", m_xmlNamespaceMgr).Attributes["value"].Value;
		string value3 = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameCustomerName + "\"]", m_xmlNamespaceMgr).Attributes["value"].Value;
		string value4 = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameCustomerCode + "\"]", m_xmlNamespaceMgr).Attributes["value"].Value;
		string value5 = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameProjectDescription + "\"]", m_xmlNamespaceMgr).Attributes["value"].Value;
		string value6 = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameProjectCreationDate + "\"]", m_xmlNamespaceMgr).Attributes["value"].Value;
		string value7 = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameProjectCustomerAddress1 + "\"]", m_xmlNamespaceMgr).Attributes["value"].Value;
		string value8 = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameProjectCustomerAddress2 + "\"]", m_xmlNamespaceMgr).Attributes["value"].Value;
		string value9 = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameProjectCustomerPostalCode + "\"]", m_xmlNamespaceMgr).Attributes["value"].Value;
		string value10 = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameProjectCustomerCity + "\"]", m_xmlNamespaceMgr).Attributes["value"].Value;
		string value11 = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameProjectCustomerProvince + "\"]", m_xmlNamespaceMgr).Attributes["value"].Value;
		string value12 = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameProjectCustomerCountry + "\"]", m_xmlNamespaceMgr).Attributes["value"].Value;
		string value13 = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameProjectInvoicingAddress1 + "\"]", m_xmlNamespaceMgr).Attributes["value"].Value;
		string value14 = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameProjectInvoicingAddress2 + "\"]", m_xmlNamespaceMgr).Attributes["value"].Value;
		string value15 = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameProjectInvoicingPostalCode + "\"]", m_xmlNamespaceMgr).Attributes["value"].Value;
		string value16 = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameProjectInvoicingCity + "\"]", m_xmlNamespaceMgr).Attributes["value"].Value;
		string value17 = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameProjectInvoicingProvince + "\"]", m_xmlNamespaceMgr).Attributes["value"].Value;
		string value18 = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameProjectInvoicingCountry + "\"]", m_xmlNamespaceMgr).Attributes["value"].Value;
		string value19 = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameProjectShippingAddress1 + "\"]", m_xmlNamespaceMgr).Attributes["value"].Value;
		string value20 = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameProjectShippingAddress2 + "\"]", m_xmlNamespaceMgr).Attributes["value"].Value;
		string value21 = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameProjectShippingPostalCode + "\"]", m_xmlNamespaceMgr).Attributes["value"].Value;
		string value22 = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameProjectShippingCity + "\"]", m_xmlNamespaceMgr).Attributes["value"].Value;
		string value23 = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameProjectShippingProvince + "\"]", m_xmlNamespaceMgr).Attributes["value"].Value;
		string value24 = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameProjectShippingCountry + "\"]", m_xmlNamespaceMgr).Attributes["value"].Value;
		string text = Convert.ToString(NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameProjectAgreedAmount + "\"]", m_xmlNamespaceMgr).Attributes["value"].Value, CultureInfo.InvariantCulture);
		string value25 = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameProjectAgreedAmountCurrency + "\"]", m_xmlNamespaceMgr).Attributes["value"].Value;
		string value26 = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameProjectComments + "\"]", m_xmlNamespaceMgr).Attributes["value"].Value;
		string text2 = Convert.ToString(NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameProjectDiscount + "\"]", m_xmlNamespaceMgr).Attributes["value"].Value, CultureInfo.InvariantCulture);
		string text3 = Convert.ToString(NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameContingencies + "\"]", m_xmlNamespaceMgr).Attributes["value"].Value, CultureInfo.InvariantCulture);
		string text4 = Convert.ToString(NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameGeneralExpenditures + "\"]", m_xmlNamespaceMgr).Attributes["value"].Value, CultureInfo.InvariantCulture);
		string text5 = Convert.ToString(NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameFinancialExpenditures + "\"]", m_xmlNamespaceMgr).Attributes["value"].Value, CultureInfo.InvariantCulture);
		string text6 = Convert.ToString(NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameCommissions + "\"]", m_xmlNamespaceMgr).Attributes["value"].Value, CultureInfo.InvariantCulture);
		string value27 = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameProjectPricingComments + "\"]", m_xmlNamespaceMgr).Attributes["value"].Value;
		string strQuery = "UPDATE [dbo].[Projects] \r\n   SET [Name] = N'" + value2 + "'\r\n      ,[CustomerName] = N'" + value3 + "'\r\n      ,[CustomerCode] = '" + value4 + "'\r\n      ,[Description] = N'" + value5 + "'\r\n      ,[CreationDate] = CONVERT(DATETIME, '" + value6 + "',126)\r\n      ,[Address1] = N'" + value7 + "'\r\n      ,[Address2] = N'" + value8 + "'\r\n      ,[PostalCode] = N'" + value9 + "'\r\n      ,[City] = N'" + value10 + "'\r\n      ,[Province] = N'" + value11 + "'\r\n      ,[Country] = N'" + value12 + "'\r\n      ,[BillingAddress1] = N'" + value13 + "'\r\n      ,[BillingAddress2] = N'" + value14 + "'\r\n      ,[BillingPostalCode] = N'" + value15 + "'\r\n      ,[BillingCity] = N'" + value16 + "'\r\n      ,[BillingProvince] = N'" + value17 + "'\r\n      ,[BillingCountry] = N'" + value18 + "'\r\n      ,[ShippingAddress1] = N'" + value19 + "'\r\n      ,[ShippingAddress2] = N'" + value20 + "'\r\n      ,[ShippingPostalCode] = N'" + value21 + "'\r\n      ,[ShippingCity] = N'" + value22 + "'\r\n      ,[ShippingProvince] = N'" + value23 + "'\r\n      ,[ShippingCountry] = N'" + value24 + "'\r\n      ,[AgreedAmount] = " + text + "\r\n      ,[AgreedAmountCurrency] = N'" + value25 + "'\r\n      ,[Comments] = N'" + value26 + "'\r\n      ,[ProjectDiscount] = " + text2 + "\r\n      ,[Contingencies] = " + text3 + "\r\n      ,[GeneralExpenditures] = " + text4 + "\r\n      ,[FinancialExpenditures] = " + text5 + "\r\n      ,[Commissions] = " + text6 + "\r\n      ,[PricingComments] = N'" + value27 + "'\r\n WHERE ProjectCode =" + value;
		SqlCommand sqlCommand = BuildSQLCommand(strQuery, CommandType.Text);
		int num = sqlCommand.ExecuteNonQuery();
		if (0 < num)
		{
			result = true;
		}
		if (sqlCommand.Connection.State != 0)
		{
			sqlCommand.Connection.Close();
		}
		return result;
	}

	private static int FindPurchaseGroupExpendituresInsertPosition(PrefCollection<PrefProjectPerGroupExpenditure> perGroupExpenditures, PrefGroup group, bool bCheckIfItHasBeenInserted)
	{
		int num = 0;
		foreach (PrefProjectPerGroupExpenditure perGroupExpenditure in perGroupExpenditures)
		{
			int num2 = group.Name.CompareTo(perGroupExpenditure.Group.Name);
			if (bCheckIfItHasBeenInserted && num2 == 0)
			{
				return -1;
			}
			if (num2 >= 0)
			{
				num++;
				continue;
			}
			return num;
		}
		return num;
	}

	private bool GetCommandParams_AddNewProject(XmlNode NodeCommand, ref string strProjectName, ref long nProjectCode, ref Guid idProject)
	{
		bool flag = false;
		try
		{
			strProjectName = string.Empty;
			nProjectCode = 0L;
			idProject = Guid.Empty;
			string text = string.Empty;
			string text2 = string.Empty;
			XmlNode xmlNode = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameProjectName + "\"]", m_xmlNamespaceMgr);
			if (xmlNode != null)
			{
				strProjectName = xmlNode.Attributes["value"].Value;
			}
			else
			{
				AddError("Missing parameter " + ParamNameProjectName, NodeCommand.Attributes["name"].Value);
				flag = true;
			}
			xmlNode = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameProjectCode + "\"]", m_xmlNamespaceMgr);
			if (xmlNode != null)
			{
				text = xmlNode.Attributes["value"].Value;
			}
			xmlNode = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameProjectId + "\"]", m_xmlNamespaceMgr);
			if (xmlNode != null)
			{
				text2 = xmlNode.Attributes["value"].Value;
			}
			if (!string.IsNullOrEmpty(text2))
			{
				try
				{
					idProject = new Guid(text2);
				}
				catch (Exception)
				{
					AddError("Incorrect parameter value:" + ParamNameProjectId, NodeCommand.Attributes["name"].Value);
					flag = true;
				}
			}
			if (!string.IsNullOrEmpty(text))
			{
				try
				{
					nProjectCode = Convert.ToInt64(text);
				}
				catch (Exception)
				{
					AddError("Incorrect parameter value: (" + text + ") " + ParamNameProjectCode, NodeCommand.Attributes["name"].Value);
					flag = true;
				}
			}
		}
		catch (Exception ex3)
		{
			AddError(ex3.Message, ex3.Source);
			flag = true;
		}
		return !flag;
	}

	private bool GetCommandParams_GetProjectFromDocument(XmlNode NodeCommand, ref Guid idDoc, ref int nDocType)
	{
		bool flag = false;
		try
		{
			string text = string.Empty;
			string value = string.Empty;
			XmlNode xmlNode = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameDocId + "\"]", m_xmlNamespaceMgr);
			if (xmlNode != null)
			{
				text = xmlNode.Attributes["value"].Value;
			}
			else
			{
				AddError("Missing parameter:" + ParamNameDocId, NodeCommand.Attributes["name"].Value);
				flag = true;
			}
			xmlNode = NodeCommand.SelectSingleNode("cmd:Parameter[@name=\"" + ParamNameDocType + "\"]", m_xmlNamespaceMgr);
			if (xmlNode != null)
			{
				value = xmlNode.Attributes["value"].Value;
			}
			else
			{
				AddError("Missing parameter:" + ParamNameDocType, NodeCommand.Attributes["name"].Value);
				flag = true;
			}
			if (!string.IsNullOrEmpty(text))
			{
				try
				{
					idDoc = new Guid(text);
				}
				catch (Exception)
				{
					AddError("Incorrect parameter value:" + ParamNameDocId, NodeCommand.Attributes["name"].Value);
					flag = true;
				}
			}
			if (!string.IsNullOrEmpty(value))
			{
				try
				{
					nDocType = Convert.ToInt32(value);
				}
				catch (Exception)
				{
					AddError("Incorrect parameter value:" + ParamNameDocType, NodeCommand.Attributes["name"].Value);
					flag = true;
				}
			}
		}
		catch (Exception ex3)
		{
			AddError(ex3.Message, ex3.Source);
			flag = true;
		}
		return !flag;
	}

	private void InitVariables()
	{
		m_xmlCommand = new XmlDocument();
		m_xmlNamespaceMgr = new XmlNamespaceManager(m_xmlCommand.NameTable);
		m_xmlNamespaceMgr.AddNamespace("cmd", CommandNamespaceUri);
		m_xmlNamespaceMgr.AddNamespace("pmsg", MessageNamespaceUri);
		m_xmlResults = new XmlDocument();
		m_rootResults = m_xmlResults.CreateElement("cmd", "Commands", CommandNamespaceUri);
		m_xmlResults.AppendChild(m_rootResults);
		m_xmlMessages = new XmlDocument();
		m_rootMessages = m_xmlMessages.CreateElement("pmsg", "Messages", MessageNamespaceUri);
		m_xmlMessages.AppendChild(m_rootMessages);
	}

	public bool LoadNonProjectData(out bool bShowPricePolicy)
	{
		try
		{
			using SqlConnection sqlConnection = new SqlConnection(_strConnectionString);
			string cmdText = "SELECT 1 FROM VariablesGlobales WHERE Nombre = N'ShowPricePolicyInProjects' AND Valor=N'0'";
			sqlConnection.Open();
			SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection);
			object obj = sqlCommand.ExecuteScalar();
			if (obj == null)
			{
				bShowPricePolicy = true;
			}
			else
			{
				bShowPricePolicy = false;
			}
		}
		catch (Exception ex)
		{
			bShowPricePolicy = true;
			AddError(ex.Message, ex.Source);
			return false;
		}
		return true;
	}

	public static bool LoadPurchaseGroupExpenditures(PrefCollection<PrefProjectPerGroupExpenditure> perGroupExpenditures, string strProviderDiscountTariff, string strCostIncrementTariff, string strSalesIncrementTariff)
	{
		try
		{
			using SqlConnection sqlConnection = new SqlConnection(Globals.ConnectionString);
			string cmdText = string.Format("SELECT pg.RowId, RTRIM(c.Nombre) AS Nombre, 1 AS NumTable \nFROM PurchaseGroups pg  \nINNER JOIN Tariff t ON t.Type = 0 AND t.Name = N'{0}' \nINNER JOIN TariffsContent tc ON tc.TariffRowId = t.RowId AND tc.PurchaseGroupRowId = pg.RowId \nINNER JOIN Colores c ON c.RowId = tc.ColorRowId \nUNION ALL \nSELECT pg.RowId, RTRIM(c.Nombre) AS Nombre, 2 AS NumTable \nFROM PurchaseGroups pg  \nINNER JOIN Tariff t ON t.Type = 0 AND t.Name = N'{1}' \nINNER JOIN TariffsContent tc ON tc.TariffRowId = t.RowId AND tc.PurchaseGroupRowId = pg.RowId \nINNER JOIN Colores c ON c.RowId = tc.ColorRowId \nUNION ALL \nSELECT pg.RowId, RTRIM(c.Nombre) AS Nombre, 3 AS NumTable \nFROM PurchaseGroups pg  \nINNER JOIN Tariff t ON t.Type = 0 AND t.Name = N'{2}' \nINNER JOIN TariffsContent tc ON tc.TariffRowId = t.RowId AND tc.PurchaseGroupRowId = pg.RowId \nINNER JOIN Colores c ON c.RowId = tc.ColorRowId \nORDER BY RowId, Nombre, NumTable", strProviderDiscountTariff.Replace("'", "''"), strCostIncrementTariff.Replace("'", "''"), strSalesIncrementTariff.Replace("'", "''"));
			sqlConnection.Open();
			SqlCommand selectCommand = new SqlCommand(cmdText, sqlConnection);
			SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
			DataTable dataTable = new DataTable();
			sqlDataAdapter.Fill(dataTable);
			PrefProjectPerGroupExpenditure prefProjectPerGroupExpenditure = null;
			Guid guid = Guid.Empty;
			string text = "";
			List<PrefProjectPerGroupExpenditure> list = null;
			int num = -1;
			foreach (DataRow row in dataTable.Rows)
			{
				Guid guid2 = new Guid(row[0].ToString());
				string text2 = Convert.ToString(row[1]);
				if (prefProjectPerGroupExpenditure == null || guid != guid2 || text2 != text)
				{
					if (text2 == "")
					{
						continue;
					}
					PrefGroup value = null;
					if (!PrefPurchaseGroupList.PurchaseGroups.TryGetValue(guid2, out value))
					{
						continue;
					}
					if (prefProjectPerGroupExpenditure == null || guid != guid2)
					{
						list = new List<PrefProjectPerGroupExpenditure>();
						num = FindPurchaseGroupExpendituresInsertPosition(perGroupExpenditures, value, bCheckIfItHasBeenInserted: false);
						PrefProjectPerGroupExpenditure prefProjectPerGroupExpenditure2 = new PrefProjectPerGroupExpenditure();
						prefProjectPerGroupExpenditure2.Group = value;
						prefProjectPerGroupExpenditure2.Color = "";
						prefProjectPerGroupExpenditure2.ParentCollection = perGroupExpenditures;
						prefProjectPerGroupExpenditure2.IsProviderDiscountEditable = true;
						prefProjectPerGroupExpenditure2.IsCostIncrementEditable = true;
						prefProjectPerGroupExpenditure2.IsSalesIncrementEditable = true;
						prefProjectPerGroupExpenditure2.ListColorsRedefined = list;
						perGroupExpenditures.Insert(num, prefProjectPerGroupExpenditure2);
					}
					guid = guid2;
					text = text2;
					num++;
					prefProjectPerGroupExpenditure = new PrefProjectPerGroupExpenditure();
					prefProjectPerGroupExpenditure.Group = value;
					prefProjectPerGroupExpenditure.Color = text2;
					prefProjectPerGroupExpenditure.ParentCollection = perGroupExpenditures;
					perGroupExpenditures.Insert(num, prefProjectPerGroupExpenditure);
					list.Add(prefProjectPerGroupExpenditure);
				}
				switch (Convert.ToInt32(row[2]))
				{
				case 1:
					prefProjectPerGroupExpenditure.IsProviderDiscountEditable = true;
					break;
				case 2:
					prefProjectPerGroupExpenditure.IsCostIncrementEditable = true;
					break;
				case 3:
					prefProjectPerGroupExpenditure.IsSalesIncrementEditable = true;
					break;
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, ex.Source);
			return false;
		}
		foreach (PrefGroup value2 in PrefPurchaseGroupList.PurchaseGroups.Values)
		{
			int num2 = FindPurchaseGroupExpendituresInsertPosition(perGroupExpenditures, value2, bCheckIfItHasBeenInserted: true);
			if (num2 != -1)
			{
				PrefProjectPerGroupExpenditure prefProjectPerGroupExpenditure3 = new PrefProjectPerGroupExpenditure();
				prefProjectPerGroupExpenditure3.Group = value2;
				prefProjectPerGroupExpenditure3.Color = "";
				prefProjectPerGroupExpenditure3.ParentCollection = perGroupExpenditures;
				prefProjectPerGroupExpenditure3.IsProviderDiscountEditable = true;
				prefProjectPerGroupExpenditure3.IsCostIncrementEditable = true;
				prefProjectPerGroupExpenditure3.IsSalesIncrementEditable = true;
				perGroupExpenditures.Insert(num2, prefProjectPerGroupExpenditure3);
			}
		}
		PrefProjectPerGroupExpenditure prefProjectPerGroupExpenditure4 = new PrefProjectPerGroupExpenditure();
		prefProjectPerGroupExpenditure4.Group = new PrefGroup();
		prefProjectPerGroupExpenditure4.Group.Code = 0;
		prefProjectPerGroupExpenditure4.Group.Name = Resources.IDS_WITHOUTGROUP;
		prefProjectPerGroupExpenditure4.Group.Supplier = Resources.IDS_WITHOUTGROUP;
		prefProjectPerGroupExpenditure4.Group.Type = enGroupType.None;
		prefProjectPerGroupExpenditure4.Color = "";
		prefProjectPerGroupExpenditure4.ParentCollection = perGroupExpenditures;
		prefProjectPerGroupExpenditure4.IsProviderDiscountEditable = true;
		prefProjectPerGroupExpenditure4.IsCostIncrementEditable = true;
		prefProjectPerGroupExpenditure4.IsSalesIncrementEditable = true;
		perGroupExpenditures.Add(prefProjectPerGroupExpenditure4);
		return true;
	}

	private bool LoadXMLCommands(string strCommandXml)
	{
		try
		{
			m_xmlCommand.LoadXml(strCommandXml);
		}
		catch (Exception ex)
		{
			AddError(ex.Message, ex.Source);
			return false;
		}
		return true;
	}

	public static bool RecalculateSalesDocs(List<SalesDocument> listSalesDocs, object prefUserLink)
	{
		if (listSalesDocs.Count == 0)
		{
			return true;
		}
		string text = "UPDATE ContenidoPAFTimestamps SET PricesUTCModificationTime = NULL WHERE ";
		foreach (SalesDocument listSalesDoc in listSalesDocs)
		{
			text += $"(Numero = {listSalesDoc.Number} AND Version = {listSalesDoc.Version}) OR ";
		}
		text = text.Substring(0, text.Length - 3);
		try
		{
			using SqlConnection sqlConnection = new SqlConnection(Globals.ConnectionString);
			sqlConnection.Open();
			SqlCommand sqlCommand = new SqlCommand(text, sqlConnection);
			sqlCommand.ExecuteNonQuery();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, ex.Source);
			return false;
		}
		try
		{
			foreach (SalesDocument listSalesDoc2 in listSalesDocs)
			{
				SalesDoc salesDoc = new SalesDocClass();
				salesDoc.ConnectionString = Globals.AdoConnectionString;
				if (prefUserLink != null)
				{
					salesDoc.PrefUserLink = prefUserLink;
				}
				salesDoc.Load(listSalesDoc2.Number, listSalesDoc2.Version);
				salesDoc.AutomaticRecalculate();
				salesDoc.Save();
				salesDoc = null;
			}
		}
		catch (Exception ex2)
		{
			MessageBox.Show(ex2.Message, ex2.Source);
			return false;
		}
		return true;
	}
}
