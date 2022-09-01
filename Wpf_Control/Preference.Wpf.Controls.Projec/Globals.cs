using System;
using System.Data.SqlClient;

namespace Preference.Wpf.Controls.Projects;

internal static class Globals
{
	private static string _strConnectionString;

	private static string _strAdoConnectionString;

	public static string ProjectId;

	public static long ProjectCode;

	public static string ItemNameCode;

	public static string ItemNameCustomer;

	public static string ItemNameCurrency;

	public static string ItemNameDBObject;

	public static string ItemNameDocument;

	public static string ItemNameGroup;

	public static string ItemNameLaborCost;

	public static string ItemNameMaterialCost;

	public static string ItemNameName;

	public static string ItemNamePriceGroup;

	public static string ItemNameProviderName;

	public static string ItemNamePurchaseGroup;

	public static string ItemNameRowId;

	public static string ItemNameSymbol;

	public static string ParamNameAddressMain;

	public static string ParamNameAddressShipping;

	public static string ParamNameAddressBilling;

	public static string ParamNameCaption;

	public static string ParamNameColor;

	public static string ParamNameCommissions;

	public static string ParamNameContactAddress;

	public static string ParamNameContactCity;

	public static string ParamNameContactCompany;

	public static string ParamNameContactContactPerson;

	public static string ParamNameContactCountry;

	public static string ParamNameContactEmail;

	public static string ParamNameContactFax;

	public static string ParamNameContactPostalCode;

	public static string ParamNameContactProvince;

	public static string ParamNameContactTelephone;

	public static string ParamNameContingencies;

	public static string ParamNameCurrencies;

	public static string ParamNameCustomerCode;

	public static string ParamNameCustomerName;

	public static string ParamNameCustomers;

	public static string ParamNameDBObjectList;

	public static string ParamNameDefaultObjectTypeCode;

	public static string ParamNameDestDocId;

	public static string ParamNameDestDocType;

	public static string ParamNameDocId;

	public static string ParamNameDocType;

	public static string ParamNameDocumentCode;

	public static string ParamNameDocumentList;

	public static string ParamNameDocumentNumber;

	public static string ParamNameDocumentNumeration;

	public static string ParamNameDocumentSupplier;

	public static string ParamNameDocumentVersion;

	public static string ParamNameEntry;

	public static string ParamNameExit;

	public static string ParamNameException;

	public static string ParamNameExceptions;

	public static string ParamNameFinancialExpenditures;

	public static string ParamNameFolderId;

	public static string ParamNameGroupRowId;

	public static string ParamNameGroups;

	public static string ParamNameName;

	public static string ParamNamePriceGroupRowId;

	public static string ParamNamePurchaseGroupRowId;

	public static string ParamNameGeneralExpenditures;

	public static string ParamNameGroupType;

	public static string ParamNameOrderNumber;

	public static string ParamNameParentFolderId;

	public static string ParamNameProjectName;

	public static string ParamNameProjectAgreedAmount;

	public static string ParamNameProjectAgreedAmountCurrency;

	public static string ParamNameProjectCode;

	public static string ParamNameProjectComments;

	public static string ParamNameProjectCreationDate;

	public static string ParamNameProjectCustomerAddress1;

	public static string ParamNameProjectCustomerAddress2;

	public static string ParamNameProjectCustomerCity;

	public static string ParamNameProjectCustomerCountry;

	public static string ParamNameProjectCustomerPostalCode;

	public static string ParamNameProjectCustomerProvince;

	public static string ParamNameProjectDescription;

	public static string ParamNameProjectDiscount;

	public static string ParamNameProjectId;

	public static string ParamNameProjectInvoicingAddress1;

	public static string ParamNameProjectInvoicingAddress2;

	public static string ParamNameProjectInvoicingCity;

	public static string ParamNameProjectInvoicingCountry;

	public static string ParamNameProjectInvoicingPostalCode;

	public static string ParamNameProjectInvoicingProvince;

	public static string ParamNameProjectPricingComments;

	public static string ParamNameProjectShippingAddress1;

	public static string ParamNameProjectShippingAddress2;

	public static string ParamNameProjectShippingCity;

	public static string ParamNameProjectShippingCountry;

	public static string ParamNameProjectShippingPostalCode;

	public static string ParamNameProjectShippingProvince;

	public static string ParamNameRedefinedTariffName;

	public static string ParamNameRelationshipType;

	public static string ParamNameSortOrder;

	public static string ParamNameSourceId;

	public static string ParamNameSourceType;

	public static string ParamNameSrcFolderId;

	public static string ParamNameTariffName;

	public static string ParamNameType;

	public static string ParamNameTypeName;

	public static string ParamNameValue;

	public static string ParamNameVersionName;

	public static string PrefCADCommandNamespaceUri;

	public static string PrefCommandNamespaceUri;

	public static string SchemaInstance;

	public static string MessageNamespaceUri;

	public static string ConnectionUserID;

	public static string ConnectionPassword;

	public static string ConnectionServer;

	public static string ConnectionDatabase;

	public static bool IntegratedSecurity;

	public static string ConnectionString
	{
		get
		{
			return _strConnectionString;
		}
		set
		{
			_strConnectionString = value;
			SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder(_strConnectionString);
			ConnectionDatabase = sqlConnectionStringBuilder.InitialCatalog;
			ConnectionPassword = sqlConnectionStringBuilder.Password;
			ConnectionServer = sqlConnectionStringBuilder.DataSource;
			ConnectionUserID = sqlConnectionStringBuilder.UserID;
			IntegratedSecurity = sqlConnectionStringBuilder.IntegratedSecurity;
		}
	}

	public static string AdoConnectionString
	{
		get
		{
			return _strAdoConnectionString;
		}
		set
		{
			_strAdoConnectionString = value;
		}
	}

	public static string CommandsHeaderXML
	{
		get
		{
			if (string.IsNullOrEmpty(_strConnectionString))
			{
				return $"<cmd:Commands xmlns:cmd='{PrefCADCommandNamespaceUri}'>";
			}
			SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder(_strConnectionString);
			return "<cmd:Commands xmlns:cmd=\"" + PrefCADCommandNamespaceUri + "\"><cmd:Connection server=\"" + sqlConnectionStringBuilder.DataSource + "\" password=\"" + sqlConnectionStringBuilder.Password + "\" user=\"" + sqlConnectionStringBuilder.UserID + "\" database=\"" + sqlConnectionStringBuilder.InitialCatalog + "\" trustedConnection=\"" + Convert.ToString(sqlConnectionStringBuilder.IntegratedSecurity) + "\"/>";
		}
	}

	static Globals()
	{
		_strConnectionString = string.Empty;
		_strAdoConnectionString = string.Empty;
		ProjectId = string.Empty;
		ProjectCode = -1L;
		ItemNameCode = "Code";
		ItemNameCustomer = "Customer";
		ItemNameCurrency = "Currency";
		ItemNameDBObject = "DBObject";
		ItemNameDocument = "Document";
		ItemNameGroup = "Group";
		ItemNameLaborCost = "LaborCost";
		ItemNameMaterialCost = "MaterialCost";
		ItemNameName = "Name";
		ItemNamePriceGroup = "PriceGroup";
		ItemNameProviderName = "ProviderName";
		ItemNamePurchaseGroup = "PurchaseGroup";
		ItemNameRowId = "RowId";
		ItemNameSymbol = "Symbol";
		ParamNameAddressMain = "MainAddress";
		ParamNameAddressShipping = "ShippingAddress";
		ParamNameAddressBilling = "BillingAddress";
		ParamNameCaption = "Caption";
		ParamNameColor = "Color";
		ParamNameCommissions = "Commissions";
		ParamNameContactAddress = "Address";
		ParamNameContactCity = "City";
		ParamNameContactCompany = "Company";
		ParamNameContactContactPerson = "ContactPerson";
		ParamNameContactCountry = "Country";
		ParamNameContactEmail = "Email";
		ParamNameContactFax = "Fax";
		ParamNameContactPostalCode = "PostalCode";
		ParamNameContactProvince = "Province";
		ParamNameContactTelephone = "Telephone";
		ParamNameContingencies = "Contingencies";
		ParamNameCurrencies = "Currencies";
		ParamNameCustomerCode = "CustomerCode";
		ParamNameCustomerName = "CustomerName";
		ParamNameCustomers = "Customers";
		ParamNameDBObjectList = "DBObjectList";
		ParamNameDefaultObjectTypeCode = "DefaultObjectTypeCode";
		ParamNameDestDocId = "DestDocumentId";
		ParamNameDestDocType = "DestDocumentType";
		ParamNameDocId = "DocumentId";
		ParamNameDocType = "DocumentTypeCode";
		ParamNameDocumentCode = "Code";
		ParamNameDocumentList = "DocumentList";
		ParamNameDocumentNumber = "Number";
		ParamNameDocumentNumeration = "Numeration";
		ParamNameDocumentSupplier = "Supplier";
		ParamNameDocumentVersion = "Version";
		ParamNameEntry = "Entry";
		ParamNameExit = "Exit";
		ParamNameException = "Exception";
		ParamNameExceptions = "Exceptions";
		ParamNameFinancialExpenditures = "FinancialExpenditures";
		ParamNameFolderId = "FolderId";
		ParamNameGroupRowId = "GroupRowId";
		ParamNameGroups = "Groups";
		ParamNameName = "Name";
		ParamNamePriceGroupRowId = "PriceGroupRowId";
		ParamNamePurchaseGroupRowId = "PurchaseGroupRowId";
		ParamNameGeneralExpenditures = "GeneralExpenditures";
		ParamNameGroupType = "GroupType";
		ParamNameOrderNumber = "OrderNumber";
		ParamNameParentFolderId = "ParentFolderId";
		ParamNameProjectName = "ProjectName";
		ParamNameProjectAgreedAmount = "AgreedAmount";
		ParamNameProjectAgreedAmountCurrency = "AgreedAmountCurrency";
		ParamNameProjectCode = "ProjectCode";
		ParamNameProjectComments = "Comments";
		ParamNameProjectCreationDate = "CreationDate";
		ParamNameProjectCustomerAddress1 = "CustomerAddress1";
		ParamNameProjectCustomerAddress2 = "CustomerAddress2";
		ParamNameProjectCustomerCity = "CustomerCity";
		ParamNameProjectCustomerCountry = "CustomerCountry";
		ParamNameProjectCustomerPostalCode = "CustomerPostalCode";
		ParamNameProjectCustomerProvince = "CustomerProvince";
		ParamNameProjectDescription = "Description";
		ParamNameProjectDiscount = "ProjectDiscount";
		ParamNameProjectId = "ProjectId";
		ParamNameProjectInvoicingAddress1 = "InvoicingAddress1";
		ParamNameProjectInvoicingAddress2 = "InvoicingAddress2";
		ParamNameProjectInvoicingCity = "InvoicingCity";
		ParamNameProjectInvoicingCountry = "InvoicingCountry";
		ParamNameProjectInvoicingPostalCode = "InvoicingPostalCode";
		ParamNameProjectInvoicingProvince = "InvoicingProvince";
		ParamNameProjectPricingComments = "PricingComments";
		ParamNameProjectShippingAddress1 = "ShippingAddress1";
		ParamNameProjectShippingAddress2 = "ShippingAddress2";
		ParamNameProjectShippingCity = "ShippingCity";
		ParamNameProjectShippingCountry = "ShippingCountry";
		ParamNameProjectShippingPostalCode = "ShippingPostalCode";
		ParamNameProjectShippingProvince = "ShippingProvince";
		ParamNameRedefinedTariffName = "RedefinedTariffName";
		ParamNameRelationshipType = "RelationshipType";
		ParamNameSortOrder = "SortOrder";
		ParamNameSourceId = "SourceId";
		ParamNameSourceType = "SourceType";
		ParamNameSrcFolderId = "SrcFolderId";
		ParamNameTariffName = "TariffName";
		ParamNameType = "Type";
		ParamNameTypeName = "TypeName";
		ParamNameValue = "Value";
		ParamNameVersionName = "VersionName";
		PrefCADCommandNamespaceUri = "http://www.preference.com/XMLSchemas/2006/PrefCAD.Command";
		PrefCommandNamespaceUri = "http://www.preference.com/XMLSchemas/2006/Command/PrefCommand.xsd";
		SchemaInstance = "http://www.w3.org/2001/XMLSchema-instance";
		MessageNamespaceUri = "http://www.preference.com/XMLSchemas/2007/PrefSuite.Messages";
		ConnectionUserID = string.Empty;
		ConnectionPassword = string.Empty;
		ConnectionServer = string.Empty;
		ConnectionDatabase = string.Empty;
		IntegratedSecurity = false;
	}
}
