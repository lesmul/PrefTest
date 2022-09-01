using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Xml;
using Preference.Data.SqlClient;
using Preference.Diagnostics;
using Preference.Wpf.Controls.Attachments.Views;
using Preference.Wpf.Controls.Projects.AppLogic;
using Preference.Wpf.Controls.Properties;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;

namespace Preference.Wpf.Controls.Projects.Views;

public class ProjectView : UserControl, IComponentConnector, IStyleConnector
{
	public static string CurrencySymbol = "";

	private IServiceAgent _ServiceAgent;

	private PrefProject m_pCurrentProject;

	public string CommandsHeaderXML = string.Empty;

	internal ProjectView ProjectMainPage;

	internal BeginStoryboard storyboardDocuments1_BeginStoryboard;

	internal Grid gridPrincipal2;

	internal Image shadow01;

	internal Rectangle rectangleFrame01;

	internal Grid gridContent;

	internal StackPanel stackpanelProgressLoop;

	internal Viewbox viewboxProgressLoop;

	internal Image imageProgressLoop;

	internal RotateTransform rotationProgressLoop;

	internal TextBlock textProgressLoop;

	internal Border borderHeader;

	internal Image imagePrefSuiteLogo;

	internal StackPanel stackpanelName;

	internal TextBlock staticProjectName;

	internal TextBox textboxProjectName;

	internal TextBlock staticProjectCode;

	internal TextBlock textblockProjectCode;

	internal TextBlock staticClientName;

	internal TextBox textboxCustomerCode;

	internal ComboBox comboboxClientName;

	internal Button buttonRefreshCustomers;

	internal StackPanel stackpanelButtonDetails;

	internal Button buttonSave;

	internal Button buttonUndo;

	internal Grid gridTab;

	internal Border borderTab;

	internal TabControl tabcontrolContent;

	internal TabItem tabitemDetails;

	internal Grid gridTabItemDetails;

	internal Grid stackPanelStatus;

	internal TextBlock staticProjectStatus;

	internal TextBlock staticAgreedAmount;

	internal TextBox textboxPrice1;

	internal TextBlock staticAgreedAmountCurrency;

	internal ComboBox comboboxPrice2;

	internal DockPanel stackPanelSalesStatus;

	internal TextBlock textblockSales;

	internal TextBlock staticSales;

	internal DockPanel stackPanelPurchasesStatus;

	internal TextBlock textblockPurchases;

	internal TextBlock staticPurchases;

	internal DockPanel stackPanelProductionStatus;

	internal TextBlock textblockProduction;

	internal TextBlock staticProduction;

	internal DockPanel stackPanelWorkforceStatus;

	internal TextBlock textblockWorkforce;

	internal TextBlock staticWorkforce;

	internal TextBlock staticProgress;

	internal Grid gridStatusBar;

	internal TextBlock staticAccepted;

	internal TextBox textblockAccepted;

	internal TextBlock staticEstimated;

	internal TextBox textblockEstimated;

	internal StackPanel stackPanelOrdered;

	internal TextBlock staticOrdered;

	internal TextBox textblockOrdered;

	internal StackPanel stackPanelPurchased;

	internal TextBlock staticPurchased;

	internal TextBox textblockPurchased;

	internal TextBlock staticStarted;

	internal TextBox textblockStarted;

	internal TextBlock staticFinished;

	internal TextBox textblockFinished;

	internal TextBlock staticSent;

	internal TextBox textblockSent;

	internal TextBlock staticDelivered;

	internal TextBox textblockDelivered;

	internal TextBlock staticMounted;

	internal TextBox textblockMounted;

	internal TextBlock staticInvoiced;

	internal TextBox textboxInvoiced;

	internal StackPanel stackPanelTotalInstances;

	internal TextBlock staticTotalInstances;

	internal TextBlock textBlockTotalInstances;

	internal ScrollViewer scrollviewerDetails;

	internal StackPanel stackpanelDetails;

	internal TextBlock staticProjectDetails;

	internal TextBlock staticProjectDescription;

	internal TextBox textboxDescription;

	internal TextBlock staticCreationDate;

	internal TextBox textboxCreationDate;

	internal Expander expanderShippingAddress;

	internal StackPanel stackPanelShippingAddress;

	internal TextBlock staticShippingAddressMain;

	internal TextBox textboxShippingAddressPart1;

	internal TextBox textboxShippingAddressPart2;

	internal TextBlock staticShippingPostalCode;

	internal TextBlock staticShippingCity;

	internal TextBlock staticShippingProvince;

	internal TextBlock staticShippingCountry;

	internal TextBox textboxShippingPostal;

	internal TextBox textboxShippingCity;

	internal TextBox textboxShippingProvince;

	internal TextBox textboxShippingCountry;

	internal Expander expanderInvoicingAddress;

	internal StackPanel stackPanelInvoicingAddress;

	internal TextBlock staticInvoicingAddressMain;

	internal TextBox textboxInvoicingAddressPart1;

	internal TextBox textboxInvoicingAddressPart2;

	internal TextBlock staticInvoicingPostalCode;

	internal TextBlock staticInvoicingCity;

	internal TextBlock staticInvoicingProvince;

	internal TextBlock staticInvoicingCountry;

	internal TextBox textboxInvoicingPostal;

	internal TextBox textboxInvoicingCity;

	internal TextBox textboxInvoicingProvince;

	internal TextBox textboxInvoicingCountry;

	internal Expander expanderCustomerAddress;

	internal StackPanel stackPanelAddress;

	internal TextBlock staticAddressMain;

	internal TextBox textboxAddressPart1;

	internal TextBox textboxAddressPart2;

	internal TextBlock staticPostalCode;

	internal TextBlock staticCity;

	internal TextBlock staticProvince;

	internal TextBlock staticCountry;

	internal TextBox textboxPostal;

	internal TextBox textboxCity;

	internal TextBox textboxProvince;

	internal TextBox textboxCountry;

	internal StackPanel stackPanelComments;

	internal TextBlock staticComments;

	internal TextBox textboxComments;

	internal StackPanel stackpanelShorcuts;

	internal StackPanel stackpanelDocuments;

	internal Image imageDocuments;

	internal StackPanel stackpanelSales;

	internal Image imageSales;

	internal StackPanel stackpanelPurchases;

	internal Image imagePurchases;

	internal StackPanel stackpanelTariffExceptions;

	internal Image imageTariffExceptions;

	internal StackPanel stackpanelContacts;

	internal Image imageContacts;

	internal StackPanel stackpanelAttachments;

	internal Image imageAttachments;

	internal TabItem tabitemTariffsExceptions;

	internal Grid gridTabItemTariffsExceptions;

	internal TextBlock staticPricesPolicy;

	internal TextBlock staticDataBasedOn;

	internal ListView listviewTariffExceptions1;

	internal GridViewColumn GroupNameColumn;

	internal GridViewColumn ColorColumn;

	internal GridViewColumn OriginCostColumn;

	internal GridViewColumn SupplierDiscountColumn;

	internal GridViewColumn PurchasePriceColumn;

	internal GridViewColumn CostIncrementColumn;

	internal GridViewColumn EffectiveCostColumn;

	internal GridViewColumn CoefficientColumn;

	internal GridViewColumn SaleColumn;

	internal CheckBox checkboxShowAllGroups;

	internal CheckBox checkboxFixSale;

	internal TextBlock staticTotalEffectiveCost;

	internal TextBlock textblockTotalEffectiveCost;

	internal TextBlock staticTotalSale;

	internal TextBlock textblockTotalSale;

	internal TextBlock staticSaleDiscount;

	internal TextBox textboxSaleDiscount;

	internal TextBlock staticTotalEffectiveSale;

	internal TextBox textboxTotalEffectiveSale;

	internal Button buttonApply;

	internal ListView listviewTariffExceptions2;

	internal GridViewColumn ConceptColumn;

	internal GridViewColumn PercentageColumn;

	internal GridViewColumn AmountColumn;

	internal TextBlock staticIndustrialBenefit;

	internal TextBlock textblockIndustrialBenefit;

	internal TextBlock staticBenefitMargin;

	internal TextBox textboxBenefitMargin;

	internal StackPanel stackPanelPriceComments;

	internal TextBlock staticPriceComments;

	internal TextBox textboxPriceComments;

	internal TabItem tabitemContacts;

	internal Grid gridTabItemContacts;

	internal TextBlock staticContacts;

	internal RadGridView gridviewContacts;

	internal GridViewDataColumn ContactCodeColumn;

	internal GridViewComboBoxColumn ContactFullNameColumn;

	internal StackPanel stackpanelButtonsContacts;

	internal Button buttonAddContact;

	internal TextBlock staticAddContact;

	internal Button buttonRemoveContact;

	internal TextBlock staticRemoveContact;

	internal TabItem tabitemDocumentsTelerik;

	internal Grid gridtTabItemDocumentsTeleril;

	internal TextBlock staticDocumentsTelerik;

	internal RadGridView gridviewDocuments;

	internal StackPanel stackpanelButtonsDocumentsTelerik;

	internal Button buttonViewDocumentTelerik;

	internal TextBlock staticViewDocumentTelerik;

	internal Button buttonValuateDocumentTelerik;

	internal TextBlock staticValuateDocumentTelerik;

	internal TabItem tabitemSalesDocuments;

	internal Grid gridtTabItemSalesDocuments;

	internal TextBlock staticSalesDocuments;

	internal RadGridView gridviewSalesDocuments;

	internal StackPanel stackpanelButtonsSalesDocuments;

	internal Button buttonViewSalesDocument;

	internal TextBlock staticViewSalesDocument;

	internal Button buttonValuateSalesDocument;

	internal TextBlock staticValuateSalesDocument;

	internal TabItem tabitemPurchasesDocuments;

	internal Grid gridtTabItemPurchasesDocuments;

	internal TextBlock staticPurchasesDocuments;

	internal RadGridView gridviewPurchasesDocuments;

	internal StackPanel stackpanelButtonsPurchasesDocuments;

	internal Button buttonViewPurchasesDocument;

	internal TextBlock staticViewPurchasesDocument;

	internal TabItem tabitemAttachments;

	internal AttachmentsView attachmentsView;

	internal Grid gridProgressBarPanel;

	internal ProgressBar progressBarMain;

	internal TextBlock textblockProgressMessage;

	private bool _contentLoaded;

	public PrefProject CurrentProject
	{
		get
		{
			return m_pCurrentProject;
		}
		set
		{
			m_pCurrentProject = value;
		}
	}

	public string ConnectionString
	{
		get
		{
			return Globals.ConnectionString;
		}
		set
		{
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_000d: Expected O, but got Unknown
			Globals.AdoConnectionString = value;
			SqlConnectionStringBuilder val = new SqlConnectionStringBuilder(value);
			Globals.ConnectionString = val.get_ConnectionString();
			attachmentsView.ConnectionString = val.get_ConnectionString();
			Logger.Instance.WriteInformation($"ConnectionString for projects: '{val.get_ConnectionString()}'", "PrefGest");
			_ServiceAgent = new ServiceAgent(val.get_ConnectionString());
		}
	}

	public object PrefUserLink { get; set; }

	public ProjectView()
	{
		Init();
	}

	public void Init()
	{
		try
		{
			int tier = RenderCapability.Tier;
			CommandsHeaderXML = Globals.CommandsHeaderXML;
			InitializeComponent();
			LocalizePage();
			SubscribeEventHandlers();
			gridProgressBarPanel.Visibility = Visibility.Collapsed;
			stackpanelProgressLoop.Visibility = Visibility.Collapsed;
			staticProjectCode.Visibility = Visibility.Collapsed;
			stackPanelOrdered.Visibility = Visibility.Collapsed;
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefGest");
			throw;
		}
	}

	public long AddNewProject(long projectCode, string projectName)
	{
		try
		{
			Guid empty = Guid.Empty;
			string empty2 = string.Empty;
			string text = "[dbo].[pa_Projects_NewProject]";
			SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(Globals.ConnectionString, text, true);
			spParameterSet[1].Value = projectCode;
			spParameterSet[2].Value = empty;
			spParameterSet[3].Value = projectName;
			spParameterSet[4].Value = empty2;
			SqlHelper.ExecuteNonQuery(Globals.ConnectionString, CommandType.StoredProcedure, text, spParameterSet);
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

	public long GetProjectFromRelatedDocument(string documentId, int destDocumentType)
	{
		try
		{
			string text = "[dbo].[pa_Projects_GetProjectFromRelatedDocument]";
			SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(Globals.ConnectionString, text, false);
			spParameterSet[0].Value = new Guid(documentId);
			spParameterSet[1].Value = destDocumentType;
			spParameterSet[2].Value = DBNull.Value;
			spParameterSet[3].Value = DBNull.Value;
			spParameterSet[4].Value = "<PrefMessages/>";
			SqlHelper.ExecuteNonQuery(Globals.ConnectionString, CommandType.StoredProcedure, text, spParameterSet);
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

	public bool AddRelatedDocument2(long projectCode, string destDocumentId, int destDocumentType, string folderId)
	{
		try
		{
			string text = "[dbo].[pa_Projects_AddRelatedDocument2]";
			SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(Globals.ConnectionString, text, false);
			spParameterSet[0].Value = projectCode;
			spParameterSet[1].Value = new Guid(destDocumentId);
			spParameterSet[2].Value = destDocumentType;
			spParameterSet[3].Value = DBNull.Value;
			if (!string.IsNullOrEmpty(folderId))
			{
				spParameterSet[3].Value = new Guid(folderId);
			}
			spParameterSet[4].Value = "<PrefMessages/>";
			int num = SqlHelper.ExecuteNonQuery(Globals.ConnectionString, CommandType.StoredProcedure, text, spParameterSet);
			return true;
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
			return SqlHelper.ExecuteNonQuery(Globals.ConnectionString, CommandType.Text, text) == 1;
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
			return Convert.ToInt64(SqlHelper.ExecuteScalar(Globals.ConnectionString, CommandType.Text, text));
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
			return SqlHelper.ExecuteDataset(Globals.ConnectionString, CommandType.Text, text).Tables[0].Rows.Count > 0;
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefGest");
			throw;
		}
	}

	private void SetPageDataBindings()
	{
		base.DataContext = CurrentProject;
		((DataControl)gridviewDocuments).set_ItemsSource((object)null);
		((DataControl)gridviewDocuments).set_ItemsSource((object)CurrentProject.DocumentsOfProject);
		((DataControl)gridviewSalesDocuments).set_ItemsSource((object)null);
		((DataControl)gridviewSalesDocuments).set_ItemsSource((object)CurrentProject.SalesDocumentsOfProject);
		((DataControl)gridviewPurchasesDocuments).set_ItemsSource((object)null);
		((DataControl)gridviewPurchasesDocuments).set_ItemsSource((object)CurrentProject.PurchasesDocumentsOfProject);
		Binding binding = new Binding("CreationDate");
		binding.Source = CurrentProject;
		binding.Mode = BindingMode.TwoWay;
		binding.IsAsync = false;
		binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
		binding.ConverterCulture = CultureInfo.CurrentCulture;
		textboxCreationDate.SetBinding(TextBox.TextProperty, binding);
		comboboxPrice2.ItemsSource = PrefCurrencyList.Currencies;
		comboboxClientName.ItemsSource = PrefCustomersList.Customers;
		listviewTariffExceptions1.ItemsSource = CurrentProject.PricesPolicy.PerGroupExpenditures;
		listviewTariffExceptions2.ItemsSource = CurrentProject.PricesPolicy.Expenditures;
		ContactFullNameColumn.set_ItemsSource((IEnumerable)PrefContacList.Contacts);
		((DataControl)gridviewContacts).set_ItemsSource((object)null);
		binding = new Binding(".");
		binding.Source = CurrentProject.Contacts;
		binding.Mode = BindingMode.TwoWay;
		binding.IsAsync = false;
		binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
		((FrameworkElement)(object)gridviewContacts).SetBinding(DataControl.ItemsSourceProperty, (BindingBase)binding);
	}

	private void SubscribeEventHandlers()
	{
		base.Loaded += MainPage_Loaded;
		imageDocuments.MouseLeftButtonUp += OnDocumentsClick;
		imageSales.MouseLeftButtonUp += OnSalesDocumentsClick;
		imagePurchases.MouseLeftButtonUp += OnPurchasesDocumentsClick;
		imageContacts.MouseLeftButtonUp += OnContactsClick;
		imageAttachments.MouseLeftButtonUp += OnAttachmentsClick;
		imageTariffExceptions.MouseLeftButtonUp += OnPricesPolicyClick;
		buttonSave.Click += OnSaveClick;
		buttonUndo.Click += OnReloadClick;
		buttonViewDocumentTelerik.Click += OnViewDocumentTelerikClick;
		buttonViewSalesDocument.Click += OnViewSalesDocumentClick;
		buttonViewPurchasesDocument.Click += OnViewPurchasesDocumentClick;
		buttonAddContact.Click += buttonAddContact_Click;
		buttonRemoveContact.Click += buttonRemoveContact_Click;
		buttonValuateDocumentTelerik.Click += buttonValuateDocumentTelerik_Click;
		buttonValuateSalesDocument.Click += buttonValuateSalesDocument_Click;
		buttonApply.Click += buttonApply_Click;
		((UIElement)(object)gridviewDocuments).KeyDown += treeviewDocumentsTelerik_KeyDown;
		((DataControl)gridviewDocuments).add_SelectionChanged((EventHandler<SelectionChangeEventArgs>)treeviewDocumentsTelerik_SelectionChanged);
		comboboxClientName.SelectionChanged += comboboxClientName_SelectionChanged;
		textboxCustomerCode.TextChanged += textboxCustomerCode_TextChanged;
		textboxCustomerCode.PreviewTextInput += textboxCustomerCode_PreviewTextInput;
		((GridViewDataControl)gridviewContacts).add_CellEditEnded((EventHandler<GridViewCellEditEndedEventArgs>)gridviewContacts_CellEditEnded);
	}

	private void gridviewContacts_CellEditEnded(object sender, GridViewCellEditEndedEventArgs e)
	{
		Contact contact = ((FrameworkElement)(object)e.get_Cell()).DataContext as Contact;
		foreach (Contact contact2 in CurrentProject.Contacts)
		{
			if (!(contact2.DocumentId == contact.DocumentId))
			{
				continue;
			}
			foreach (Contact contact3 in PrefContacList.Contacts)
			{
				if (contact3.DocumentId == contact.DocumentId)
				{
					contact2.Address = contact3.Address;
					contact2.City = contact3.City;
					contact2.CompanyName = contact3.CompanyName;
					contact2.ContactCode = contact3.ContactCode;
					break;
				}
			}
		}
	}

	private void LocalizePage()
	{
		staticAccepted.Text = Preference.Wpf.Controls.Properties.Resources.IDS_ACCEPTED;
		staticAddContact.Text = Preference.Wpf.Controls.Properties.Resources.IDS_ADDCONTACT;
		staticAddressMain.Text = Preference.Wpf.Controls.Properties.Resources.IDS_ADDRESSMAIN + ":";
		staticAgreedAmount.Text = Preference.Wpf.Controls.Properties.Resources.IDS_AGREEDAMOUNT + ":";
		staticAgreedAmountCurrency.Text = Preference.Wpf.Controls.Properties.Resources.IDS_AGREEDAMOUNTCURRENCY + ":";
		staticBenefitMargin.Text = Preference.Wpf.Controls.Properties.Resources.IDS_BENEFITMARGIN;
		staticCity.Text = Preference.Wpf.Controls.Properties.Resources.IDS_CITY + ":";
		staticClientName.Text = Preference.Wpf.Controls.Properties.Resources.IDS_CLIENTNAME;
		staticComments.Text = Preference.Wpf.Controls.Properties.Resources.IDS_COMMENTS + ":";
		staticContacts.Text = Preference.Wpf.Controls.Properties.Resources.IDS_CONTACTS;
		staticCountry.Text = Preference.Wpf.Controls.Properties.Resources.IDS_COUNTRY + ":";
		staticCreationDate.Text = Preference.Wpf.Controls.Properties.Resources.IDS_CREATIONDATE + ":";
		staticDataBasedOn.Text = "(" + Preference.Wpf.Controls.Properties.Resources.IDS_DATABASEDON + "...)";
		staticDelivered.Text = Preference.Wpf.Controls.Properties.Resources.IDS_DELIVERED;
		staticDocumentsTelerik.Text = Preference.Wpf.Controls.Properties.Resources.IDS_DOCUMENTS;
		staticSalesDocuments.Text = Preference.Wpf.Controls.Properties.Resources.IDS_SALES;
		staticPurchasesDocuments.Text = Preference.Wpf.Controls.Properties.Resources.IDS_PURCHASES;
		staticEstimated.Text = Preference.Wpf.Controls.Properties.Resources.IDS_ESTIMATED;
		staticFinished.Text = Preference.Wpf.Controls.Properties.Resources.IDS_FINISHED;
		staticIndustrialBenefit.Text = Preference.Wpf.Controls.Properties.Resources.IDS_INDUSTRIALBENEFIT;
		staticInvoiced.Text = Preference.Wpf.Controls.Properties.Resources.IDS_INVOICED;
		staticInvoicingAddressMain.Text = Preference.Wpf.Controls.Properties.Resources.IDS_ADDRESSMAIN;
		staticInvoicingCity.Text = Preference.Wpf.Controls.Properties.Resources.IDS_CITY;
		staticInvoicingCountry.Text = Preference.Wpf.Controls.Properties.Resources.IDS_COUNTRY;
		staticInvoicingPostalCode.Text = Preference.Wpf.Controls.Properties.Resources.IDS_POSTALCODE;
		staticInvoicingProvince.Text = Preference.Wpf.Controls.Properties.Resources.IDS_PROVINCE;
		staticMounted.Text = Preference.Wpf.Controls.Properties.Resources.IDS_MOUNTED;
		staticOrdered.Text = Preference.Wpf.Controls.Properties.Resources.IDS_ORDERED;
		staticPostalCode.Text = Preference.Wpf.Controls.Properties.Resources.IDS_POSTALCODE + ":";
		staticPriceComments.Text = Preference.Wpf.Controls.Properties.Resources.IDS_COMMENTS + ":";
		staticPricesPolicy.Text = Preference.Wpf.Controls.Properties.Resources.IDS_PRICESPOLICY;
		staticProduction.Text = Preference.Wpf.Controls.Properties.Resources.IDS_PRODUCTION;
		staticProgress.Text = Preference.Wpf.Controls.Properties.Resources.IDS_PROGRESS + ":";
		staticProjectCode.Text = Preference.Wpf.Controls.Properties.Resources.IDS_CODE;
		staticProjectDescription.Text = Preference.Wpf.Controls.Properties.Resources.IDS_PROJECTDESCRIPTION + ":";
		staticProjectDetails.Text = Preference.Wpf.Controls.Properties.Resources.IDS_PROJECTDETAILS;
		staticProjectName.Text = Preference.Wpf.Controls.Properties.Resources.IDS_PROJECTNAME;
		staticProjectStatus.Text = Preference.Wpf.Controls.Properties.Resources.IDS_PROJECTSTATUS;
		staticProvince.Text = Preference.Wpf.Controls.Properties.Resources.IDS_PROVINCE + ":";
		staticPurchased.Text = Preference.Wpf.Controls.Properties.Resources.IDS_PURCHASED;
		staticPurchases.Text = Preference.Wpf.Controls.Properties.Resources.IDS_PURCHASES;
		staticRemoveContact.Text = Preference.Wpf.Controls.Properties.Resources.IDS_REMOVECONTACT;
		staticSaleDiscount.Text = Preference.Wpf.Controls.Properties.Resources.IDS_SALEDISCOUNT;
		staticSales.Text = Preference.Wpf.Controls.Properties.Resources.IDS_SALES;
		staticSent.Text = Preference.Wpf.Controls.Properties.Resources.IDS_SENT;
		staticShippingAddressMain.Text = Preference.Wpf.Controls.Properties.Resources.IDS_ADDRESSMAIN;
		staticShippingCity.Text = Preference.Wpf.Controls.Properties.Resources.IDS_CITY;
		staticShippingCountry.Text = Preference.Wpf.Controls.Properties.Resources.IDS_COUNTRY;
		staticShippingPostalCode.Text = Preference.Wpf.Controls.Properties.Resources.IDS_POSTALCODE;
		staticShippingProvince.Text = Preference.Wpf.Controls.Properties.Resources.IDS_PROVINCE;
		staticStarted.Text = Preference.Wpf.Controls.Properties.Resources.IDS_STARTED;
		staticTotalEffectiveCost.Text = Preference.Wpf.Controls.Properties.Resources.IDS_TOTALEFFECTIVECOST;
		staticTotalEffectiveSale.Text = Preference.Wpf.Controls.Properties.Resources.IDS_TOTALEFFECTIVESALE;
		staticTotalSale.Text = Preference.Wpf.Controls.Properties.Resources.IDS_TOTALSALE;
		staticValuateDocumentTelerik.Text = Preference.Wpf.Controls.Properties.Resources.IDS_VALUATEDOCUMENT;
		staticValuateSalesDocument.Text = Preference.Wpf.Controls.Properties.Resources.IDS_VALUATEDOCUMENT;
		staticViewSalesDocument.Text = Preference.Wpf.Controls.Properties.Resources.IDS_VIEWDOCUMENT;
		staticViewPurchasesDocument.Text = Preference.Wpf.Controls.Properties.Resources.IDS_VIEWDOCUMENT;
		staticWorkforce.Text = Preference.Wpf.Controls.Properties.Resources.IDS_WORKFORCE;
		buttonApply.Content = Preference.Wpf.Controls.Properties.Resources.IDS_APPLY;
		buttonApply.ToolTip = Preference.Wpf.Controls.Properties.Resources.HLP_APPLYTOTALEFFECTIVESALE;
		checkboxShowAllGroups.Content = Preference.Wpf.Controls.Properties.Resources.IDS_SHOWALLGROUPS;
		checkboxFixSale.Content = Preference.Wpf.Controls.Properties.Resources.IDS_FIXSALEAMOUNT;
		expanderCustomerAddress.Header = Preference.Wpf.Controls.Properties.Resources.IDS_CUSTOMERADDRESS;
		expanderInvoicingAddress.Header = Preference.Wpf.Controls.Properties.Resources.IDS_INVOICINGADDRESS;
		expanderShippingAddress.Header = Preference.Wpf.Controls.Properties.Resources.IDS_SHIPPINGADDRESS;
		tabitemDetails.Header = Preference.Wpf.Controls.Properties.Resources.IDS_DETAILSANDSTATUS;
		tabitemAttachments.Header = Preference.Wpf.Controls.Properties.Resources.IDS_ATTACHMENTS;
		tabitemContacts.Header = Preference.Wpf.Controls.Properties.Resources.IDS_CONTACTS;
		tabitemDocumentsTelerik.Header = Preference.Wpf.Controls.Properties.Resources.IDS_DOCUMENTS;
		tabitemSalesDocuments.Header = Preference.Wpf.Controls.Properties.Resources.IDS_SALES;
		tabitemPurchasesDocuments.Header = Preference.Wpf.Controls.Properties.Resources.IDS_PURCHASES;
		tabitemTariffsExceptions.Header = Preference.Wpf.Controls.Properties.Resources.IDS_PRICESPOLICY;
		buttonSave.ToolTip = Preference.Wpf.Controls.Properties.Resources.IDS_SAVECHANGES;
		buttonUndo.ToolTip = Preference.Wpf.Controls.Properties.Resources.IDS_RELOAD;
		imageAttachments.ToolTip = Preference.Wpf.Controls.Properties.Resources.IDS_ATTACHMENTS;
		imageContacts.ToolTip = Preference.Wpf.Controls.Properties.Resources.IDS_CONTACTS;
		imageDocuments.ToolTip = Preference.Wpf.Controls.Properties.Resources.IDS_DOCUMENTS;
		imagePurchases.ToolTip = Preference.Wpf.Controls.Properties.Resources.IDS_PURCHASES;
		imageSales.ToolTip = Preference.Wpf.Controls.Properties.Resources.IDS_SALES;
		imageTariffExceptions.ToolTip = Preference.Wpf.Controls.Properties.Resources.IDS_PRICESPOLICY;
		stackPanelPurchased.ToolTip = Preference.Wpf.Controls.Properties.Resources.IDS_READYFORPRODUCTION;
		stackPanelSalesStatus.ToolTip = Preference.Wpf.Controls.Properties.Resources.TTIP_SALESSTATUS;
		stackPanelPurchasesStatus.ToolTip = Preference.Wpf.Controls.Properties.Resources.TTIP_PURCHASESSTATUS;
		stackPanelProductionStatus.ToolTip = Preference.Wpf.Controls.Properties.Resources.TTIP_PRODUCTIONSTATUS;
		stackPanelWorkforceStatus.ToolTip = Preference.Wpf.Controls.Properties.Resources.TTIP_WORKFORCESTATUS;
		GroupNameColumn.Header = Preference.Wpf.Controls.Properties.Resources.IDS_GROUP;
		OriginCostColumn.Header = Preference.Wpf.Controls.Properties.Resources.IDS_ORIGINCOST;
		CostIncrementColumn.Header = Preference.Wpf.Controls.Properties.Resources.IDS_COSTINCREMENT;
		SupplierDiscountColumn.Header = Preference.Wpf.Controls.Properties.Resources.IDS_SUPPLIERDISCOUNT;
		EffectiveCostColumn.Header = Preference.Wpf.Controls.Properties.Resources.IDS_EFFECTIVECOST;
		CoefficientColumn.Header = Preference.Wpf.Controls.Properties.Resources.IDS_COEFFICIENT;
		SaleColumn.Header = Preference.Wpf.Controls.Properties.Resources.IDS_SALE;
		PurchasePriceColumn.Header = Preference.Wpf.Controls.Properties.Resources.IDS_PURCHASEPRICE;
		GridView gridView = null;
		if (listviewTariffExceptions2.View is GridView gridView2 && gridView2.Columns.Count >= 3)
		{
			gridView2.Columns[0].Header = Preference.Wpf.Controls.Properties.Resources.IDS_CONCEPT;
			gridView2.Columns[2].Header = Preference.Wpf.Controls.Properties.Resources.IDS_AMOUNT;
		}
		((Collection<GridViewColumn>)(object)((GridViewDataControl)gridviewContacts).get_Columns())[0].set_Header((object)Preference.Wpf.Controls.Properties.Resources.IDS_CODE);
		((Collection<GridViewColumn>)(object)((GridViewDataControl)gridviewContacts).get_Columns())[1].set_Header((object)Preference.Wpf.Controls.Properties.Resources.IDS_COMPANYNAME);
		((Collection<GridViewColumn>)(object)((GridViewDataControl)gridviewContacts).get_Columns())[2].set_Header((object)Preference.Wpf.Controls.Properties.Resources.IDS_CONTACTPERSON);
		((Collection<GridViewColumn>)(object)((GridViewDataControl)gridviewContacts).get_Columns())[3].set_Header((object)Preference.Wpf.Controls.Properties.Resources.IDS_TELEPHONE);
		((Collection<GridViewColumn>)(object)((GridViewDataControl)gridviewContacts).get_Columns())[4].set_Header((object)Preference.Wpf.Controls.Properties.Resources.IDS_FAX);
		((Collection<GridViewColumn>)(object)((GridViewDataControl)gridviewContacts).get_Columns())[5].set_Header((object)Preference.Wpf.Controls.Properties.Resources.IDS_EMAIL);
		((Collection<GridViewColumn>)(object)((GridViewDataControl)gridviewContacts).get_Columns())[6].set_Header((object)Preference.Wpf.Controls.Properties.Resources.IDS_ADDRESS);
		((Collection<GridViewColumn>)(object)((GridViewDataControl)gridviewContacts).get_Columns())[7].set_Header((object)Preference.Wpf.Controls.Properties.Resources.IDS_POSTALCODE);
		((Collection<GridViewColumn>)(object)((GridViewDataControl)gridviewContacts).get_Columns())[8].set_Header((object)Preference.Wpf.Controls.Properties.Resources.IDS_CITY);
		((Collection<GridViewColumn>)(object)((GridViewDataControl)gridviewContacts).get_Columns())[9].set_Header((object)Preference.Wpf.Controls.Properties.Resources.IDS_PROVINCE);
		((Collection<GridViewColumn>)(object)((GridViewDataControl)gridviewContacts).get_Columns())[10].set_Header((object)Preference.Wpf.Controls.Properties.Resources.IDS_COUNTRY);
		((Collection<GridViewColumn>)(object)((GridViewDataControl)gridviewDocuments).get_Columns())[0].set_Header((object)Preference.Wpf.Controls.Properties.Resources.IDS_NAME);
		((Collection<GridViewColumn>)(object)((GridViewDataControl)gridviewDocuments).get_Columns())[1].set_Header((object)Preference.Wpf.Controls.Properties.Resources.IDS_DEPARTMENT);
		((Collection<GridViewColumn>)(object)((GridViewDataControl)gridviewDocuments).get_Columns())[2].set_Header((object)Preference.Wpf.Controls.Properties.Resources.IDS_TYPE);
		((Collection<GridViewColumn>)(object)((GridViewDataControl)gridviewDocuments).get_Columns())[3].set_Header((object)Preference.Wpf.Controls.Properties.Resources.IDS_CUSTOMER);
		((Collection<GridViewColumn>)(object)((GridViewDataControl)gridviewDocuments).get_Columns())[4].set_Header((object)Preference.Wpf.Controls.Properties.Resources.IDS_PROVIDER);
		((Collection<GridViewColumn>)(object)((GridViewDataControl)gridviewSalesDocuments).get_Columns())[0].set_Header((object)Preference.Wpf.Controls.Properties.Resources.IDS_NAME);
		((Collection<GridViewColumn>)(object)((GridViewDataControl)gridviewSalesDocuments).get_Columns())[1].set_Header((object)Preference.Wpf.Controls.Properties.Resources.IDS_TYPE);
		((Collection<GridViewColumn>)(object)((GridViewDataControl)gridviewSalesDocuments).get_Columns())[2].set_Header((object)Preference.Wpf.Controls.Properties.Resources.IDS_CUSTOMER);
		((Collection<GridViewColumn>)(object)((GridViewDataControl)gridviewPurchasesDocuments).get_Columns())[0].set_Header((object)Preference.Wpf.Controls.Properties.Resources.IDS_NAME);
		((Collection<GridViewColumn>)(object)((GridViewDataControl)gridviewPurchasesDocuments).get_Columns())[1].set_Header((object)Preference.Wpf.Controls.Properties.Resources.IDS_TYPE);
		((Collection<GridViewColumn>)(object)((GridViewDataControl)gridviewPurchasesDocuments).get_Columns())[2].set_Header((object)Preference.Wpf.Controls.Properties.Resources.IDS_PROVIDER);
	}

	private void MainPage_Loaded(object sender, RoutedEventArgs e)
	{
	}

	private void tabcontrolContent_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		if (e.Source == tabcontrolContent && tabcontrolContent.SelectedItem == tabitemTariffsExceptions)
		{
			CurrentProject.LoadPricesPolicyDetailData();
			if (listviewTariffExceptions1.ItemsSource == null)
			{
				listviewTariffExceptions1.ItemsSource = CurrentProject.PricesPolicy.PerGroupExpenditures;
			}
			if (listviewTariffExceptions2.ItemsSource == null)
			{
				listviewTariffExceptions2.ItemsSource = CurrentProject.PricesPolicy.Expenditures;
			}
		}
	}

	private void textboxCustomerCode_PreviewTextInput(object sender, TextCompositionEventArgs e)
	{
		bool flag = true;
		int length = e.Text.Length;
		for (int i = 0; i < length; i++)
		{
			char c = e.Text[i];
			flag &= char.IsDigit(c);
		}
		e.Handled = !flag;
		base.OnPreviewTextInput(e);
	}

	private void textboxCustomerCode_TextChanged(object sender, TextChangedEventArgs e)
	{
		int num = -1;
		try
		{
			num = Convert.ToInt32(textboxCustomerCode.Text);
		}
		catch (Exception)
		{
		}
		int num2 = -1;
		for (int i = 0; i < comboboxClientName.Items.Count; i++)
		{
			Customer customer = (Customer)comboboxClientName.Items[i];
			if (customer.CustomerCode == num)
			{
				num2 = i;
				break;
			}
		}
		if (num2 >= 0)
		{
			comboboxClientName.SelectedIndex = num2;
		}
	}

	private void comboboxClientName_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		if (comboboxClientName.SelectedItem is Customer customer)
		{
			CurrentProject.ClientCode = customer.CustomerCode;
			CurrentProject.CustomerAddress.Address1 = customer.Address;
			CurrentProject.CustomerAddress.Address2 = string.Empty;
			CurrentProject.CustomerAddress.City = customer.City;
			CurrentProject.CustomerAddress.PostalCode = customer.PostalCode;
			CurrentProject.CustomerAddress.Province = customer.Province;
			CurrentProject.CustomerAddress.Country = customer.Country;
			CurrentProject.ShippingAddress.Address1 = customer.ShippingAddress.Address1;
			CurrentProject.ShippingAddress.Address2 = customer.ShippingAddress.Address2;
			CurrentProject.ShippingAddress.City = customer.ShippingAddress.City;
			CurrentProject.ShippingAddress.PostalCode = customer.ShippingAddress.PostalCode;
			CurrentProject.ShippingAddress.Province = customer.ShippingAddress.Province;
			CurrentProject.ShippingAddress.Country = customer.ShippingAddress.Country;
			CurrentProject.InvoicingAddress.Address1 = customer.InvoicingAddress.Address1;
			CurrentProject.InvoicingAddress.Address2 = customer.InvoicingAddress.Address2;
			CurrentProject.InvoicingAddress.City = customer.InvoicingAddress.City;
			CurrentProject.InvoicingAddress.PostalCode = customer.InvoicingAddress.PostalCode;
			CurrentProject.InvoicingAddress.Province = customer.InvoicingAddress.Province;
			CurrentProject.InvoicingAddress.Country = customer.InvoicingAddress.Country;
		}
		else
		{
			CurrentProject.ClientCode = -1L;
		}
	}

	private void buttonApply_Click(object sender, RoutedEventArgs e)
	{
		try
		{
			if (CurrentProject != null)
			{
				CurrentProject.PricesPolicy.TotalSale = CurrentProject.PricesPolicy.TotalEffectiveSale;
				CurrentProject.PricesPolicy.SaleDiscount = 0.0;
			}
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefGest");
			throw;
		}
	}

	private void buttonAddContact_Click(object sender, RoutedEventArgs e)
	{
		try
		{
			Contact item = new Contact();
			CurrentProject.Contacts.Add(item);
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefGest");
			throw;
		}
	}

	private void buttonRemoveContact_Click(object sender, RoutedEventArgs e)
	{
		try
		{
			if (CurrentProject.SelectedContact != null)
			{
				CurrentProject.Contacts.Remove(CurrentProject.SelectedContact);
				CurrentProject.SelectedContact = null;
			}
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefGest");
			throw;
		}
	}

	private void CoefficientTextBox_Click(object sender, RoutedEventArgs e)
	{
		if (sender is Button button && button.TemplatedParent is ContentPresenter contentPresenter && contentPresenter.Content is PrefProjectPerGroupExpenditure prefProjectPerGroupExpenditure)
		{
			prefProjectPerGroupExpenditure.CoefficientAsFactor = prefProjectPerGroupExpenditure.OriginalCoefficient;
		}
	}

	private void CurrentProject_PropertyChanged(object sender, PropertyChangedEventArgs e)
	{
		if (e.PropertyName == "AgreedAmountCurrency")
		{
			CurrencySymbol = CurrentProject.AgreedAmountCurrency.Symbol;
			textblockTotalEffectiveCost.ToolTip = CurrentProject.AgreedAmountCurrency.Name;
			textboxTotalEffectiveSale.ToolTip = CurrentProject.AgreedAmountCurrency.Name;
			textblockTotalSale.ToolTip = CurrentProject.AgreedAmountCurrency.Name;
			textblockIndustrialBenefit.ToolTip = CurrentProject.AgreedAmountCurrency.Name;
		}
	}

	private void buttonValuateDocumentTelerik_Click(object sender, RoutedEventArgs e)
	{
		try
		{
			SalesDocument salesDocument = null;
			if (((DataControl)gridviewDocuments).get_SelectedItem() != null && ((DataControl)gridviewDocuments).get_SelectedItem() is SalesDocument)
			{
				salesDocument = (SalesDocument)((DataControl)gridviewDocuments).get_SelectedItem();
			}
			if (salesDocument != null)
			{
				staticDataBasedOn.Text = "(" + Preference.Wpf.Controls.Properties.Resources.IDS_DATABASEDON + ": " + salesDocument.Number + "/" + salesDocument.Version + ")";
				CurrentProject.SalesDocForValuation = salesDocument;
				if (CurrentProject.PricesPolicy.IsPriceDetailLoaded)
				{
					CurrentProject.PricesPolicy.ValuateSalesDocument(salesDocument);
				}
				tabcontrolContent.SelectedItem = tabitemTariffsExceptions;
			}
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefGest");
			throw;
		}
	}

	private void buttonValuateSalesDocument_Click(object sender, RoutedEventArgs e)
	{
		SalesDocument salesDocument = null;
		if (((DataControl)gridviewSalesDocuments).get_SelectedItem() != null && ((DataControl)gridviewSalesDocuments).get_SelectedItem() is SalesDocument)
		{
			salesDocument = (SalesDocument)((DataControl)gridviewSalesDocuments).get_SelectedItem();
		}
		if (salesDocument != null)
		{
			staticDataBasedOn.Text = "(" + Preference.Wpf.Controls.Properties.Resources.IDS_DATABASEDON + ": " + salesDocument.Number + "/" + salesDocument.Version + ")";
			CurrentProject.SalesDocForValuation = salesDocument;
			if (CurrentProject.PricesPolicy.IsPriceDetailLoaded)
			{
				CurrentProject.PricesPolicy.ValuateSalesDocument(salesDocument);
			}
			tabcontrolContent.SelectedItem = tabitemTariffsExceptions;
		}
	}

	private void OnViewDocumentTelerikClick(object sender, RoutedEventArgs e)
	{
		Document document = null;
		if (((DataControl)gridviewDocuments).get_SelectedItem() != null && ((DataControl)gridviewDocuments).get_SelectedItem() is Document)
		{
			document = ((DataControl)gridviewDocuments).get_SelectedItem() as Document;
		}
		if (document != null)
		{
			ViewDocument(document);
		}
	}

	private void OnViewSalesDocumentClick(object sender, RoutedEventArgs e)
	{
		Document document = null;
		if (((DataControl)gridviewSalesDocuments).get_SelectedItem() != null && ((DataControl)gridviewSalesDocuments).get_SelectedItem() is Document)
		{
			document = ((DataControl)gridviewSalesDocuments).get_SelectedItem() as Document;
		}
		if (document != null)
		{
			ViewDocument(document);
		}
	}

	private void OnViewPurchasesDocumentClick(object sender, RoutedEventArgs e)
	{
		Document document = null;
		if (((DataControl)gridviewPurchasesDocuments).get_SelectedItem() != null && ((DataControl)gridviewPurchasesDocuments).get_SelectedItem() is Document)
		{
			document = ((DataControl)gridviewPurchasesDocuments).get_SelectedItem() as Document;
		}
		if (document != null)
		{
			ViewDocument(document);
		}
	}

	private void OnAttachmentsClick(object sender, MouseButtonEventArgs e)
	{
		tabcontrolContent.SelectedItem = tabitemAttachments;
	}

	private void OnContactsClick(object sender, MouseButtonEventArgs e)
	{
		tabcontrolContent.SelectedItem = tabitemContacts;
	}

	private void OnDocumentsClick(object sender, MouseButtonEventArgs e)
	{
		tabcontrolContent.SelectedItem = tabitemDocumentsTelerik;
	}

	private void OnSalesDocumentsClick(object sender, MouseButtonEventArgs e)
	{
		tabcontrolContent.SelectedItem = tabitemSalesDocuments;
	}

	private void OnPurchasesDocumentsClick(object sender, MouseButtonEventArgs e)
	{
		tabcontrolContent.SelectedItem = tabitemPurchasesDocuments;
	}

	private void OnPricesPolicyClick(object sender, MouseButtonEventArgs e)
	{
		tabcontrolContent.SelectedItem = tabitemTariffsExceptions;
	}

	private void OnReloadClick(object sender, RoutedEventArgs e)
	{
		RefreshCurrentProject();
	}

	public void SaveCurrentProject()
	{
		try
		{
			string strCommandResults = "";
			string strErrors = "";
			bool isPolicyChanged = CurrentProject.IsPolicyChanged;
			if (!CurrentProject.Save(ref strCommandResults, ref strErrors))
			{
				MessageBox.Show("The update operation failed:" + Environment.NewLine + strErrors);
			}
			if (isPolicyChanged)
			{
				RecalculateSalesDocs();
			}
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefGest");
			throw;
		}
	}

	public void RefreshCurrentProject()
	{
		try
		{
			LoadProjectData();
			CurrentProject.PricesPolicy.IsPriceDetailLoaded = false;
			if (tabcontrolContent.SelectedItem == tabitemTariffsExceptions)
			{
				CurrentProject.LoadPricesPolicyDetailData();
			}
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefGest");
			throw;
		}
	}

	public void LoadProject(long projectCode)
	{
		try
		{
			Logger.Instance.WriteInformation($"LoadProject({projectCode})", "PrefGest");
			Globals.ProjectCode = projectCode;
			LoadProjectData();
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefGest");
			MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Hand);
		}
	}

	private void OnSaveClick(object sender, RoutedEventArgs e)
	{
		try
		{
			SaveCurrentProject();
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefGest");
			throw;
		}
	}

	private void ProviderDiscountButton_Click(object sender, RoutedEventArgs e)
	{
		if (sender is Button button && button.TemplatedParent is ContentPresenter contentPresenter && contentPresenter.Content is PrefProjectPerGroupExpenditure prefProjectPerGroupExpenditure)
		{
			prefProjectPerGroupExpenditure.ProviderDiscountAsFactor = prefProjectPerGroupExpenditure.OriginalProviderDiscount;
		}
	}

	private void RemnantIncrementButton_Click(object sender, RoutedEventArgs e)
	{
		if (sender is Button button && button.TemplatedParent is ContentPresenter contentPresenter && contentPresenter.Content is PrefProjectPerGroupExpenditure prefProjectPerGroupExpenditure)
		{
			prefProjectPerGroupExpenditure.RemnantIncrementAsFactor = prefProjectPerGroupExpenditure.OriginalRemnantIncrement;
		}
	}

	private void treeviewDocuments_KeyDown(object sender, KeyEventArgs e)
	{
		if (sender is FrameworkElement frameworkElement)
		{
			Document document = frameworkElement.DataContext as Document;
		}
	}

	private void treeviewDocumentsTelerik_KeyDown(object sender, KeyEventArgs e)
	{
		if (sender is FrameworkElement frameworkElement)
		{
			Document document = frameworkElement.DataContext as Document;
		}
	}

	private void treeviewDocumentsTelerik_SelectionChanged(object sender, SelectionChangeEventArgs e)
	{
		Document document = (Document)((DataControl)gridviewDocuments).get_SelectedItem();
		if (document != null)
		{
			if (document.DocumentType == DocumentTypes.SalesDocument)
			{
				buttonValuateDocumentTelerik.IsEnabled = true;
			}
			else
			{
				buttonValuateDocumentTelerik.IsEnabled = false;
			}
		}
	}

	private void textBoxTemplatedContactCode_TextChanged(object sender, TextChangedEventArgs e)
	{
		if (!(sender is TextBox textBox) || !(textBox.TemplatedParent is ContentPresenter contentPresenter))
		{
			return;
		}
		Contact contact = contentPresenter.Content as Contact;
		if (contentPresenter.Parent is GridViewRowPresenter reference)
		{
			ContentPresenter contentPresenter2 = VisualTreeHelper.GetChild(reference, 2) as ContentPresenter;
			ComboBox comboBox = contentPresenter2.ContentTemplate.FindName("comboBoxTemplatedContactName", contentPresenter2) as ComboBox;
			if (comboBox != null && comboBox.ItemsSource == null)
			{
				comboBox.ItemsSource = PrefContacList.Contacts;
			}
			if (comboBox != null && contact != null)
			{
				comboBox.SelectedIndex = comboBox.Items.IndexOf(contact);
			}
		}
	}

	private void comboBoxContactName_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		if (!(sender is ComboBox comboBox) || !(comboBox.TemplatedParent is ContentPresenter contentPresenter))
		{
			return;
		}
		Contact contact = comboBox.SelectedItem as Contact;
		if (!(contentPresenter.Parent is GridViewRowPresenter reference))
		{
			return;
		}
		ContentPresenter contentPresenter2 = VisualTreeHelper.GetChild(reference, 0) as ContentPresenter;
		if (contentPresenter2.ContentTemplate.FindName("textBoxTemplatedContactCode", contentPresenter2) is TextBox textBox && contact != null)
		{
			textBox.Text = contact.ContactCode.ToString();
		}
		for (int i = 0; i < CurrentProject.Contacts.Count; i++)
		{
			Contact contact2 = CurrentProject.Contacts[i];
			if (contact2.ContactCode == contact.ContactCode)
			{
				CurrentProject.Contacts[i].DocumentId = contact.DocumentId;
				break;
			}
		}
	}

	private void textBoxTemplatedContactCode_KeyDown(object sender, KeyEventArgs e)
	{
		if (sender is FrameworkElement frameworkElement && (e.Key == Key.Return || e.Key == Key.Return))
		{
			frameworkElement.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
		}
	}

	private void textBoxTemplatedContactCode_LostFocus(object sender, RoutedEventArgs e)
	{
		if (sender is FrameworkElement frameworkElement && frameworkElement.TemplatedParent is ContentPresenter contentPresenter && contentPresenter.Content is Contact contact && contact.Status == enStatus.Creating && contact.ContactCode >= 0 && contact.DocumentId != Guid.Empty)
		{
			contact.Status = enStatus.Created;
			CurrentProject.Contacts.Refresh();
		}
	}

	private void OnTemplateItemGotFocus(object sender, RoutedEventArgs e)
	{
		if (e.Source is FrameworkElement frameworkElement && frameworkElement.TemplatedParent is ContentPresenter contentPresenter && contentPresenter.Parent is GridViewRowPresenter gridViewRowPresenter && gridViewRowPresenter.TemplatedParent is Control container && ItemsControl.ItemsControlFromItemContainer(container) is Selector selector)
		{
			selector.SelectedItem = contentPresenter.Content;
			if (frameworkElement is ComboBox comboBox && comboBox.Name == "comboBoxTemplatedContactName" && comboBox.ItemsSource == null)
			{
				comboBox.ItemsSource = PrefContacList.Contacts;
			}
		}
	}

	private bool LoadProjectData()
	{
		try
		{
			Logger.Instance.WriteInformation("LoadProjectData", "PrefGest");
			string text = $"SELECT ProjectId FROM Projects WHERE ProjectCode = {Globals.ProjectCode}";
			DataSet dataSet = SqlHelper.ExecuteDataset(Globals.ConnectionString, CommandType.Text, text);
			Guid projectId = Guid.Empty;
			if (dataSet.Tables[0].Rows.Count == 1)
			{
				projectId = new Guid(Convert.ToString(dataSet.Tables[0].Rows[0]["ProjectId"]));
			}
			if (m_pCurrentProject != null)
			{
				m_pCurrentProject.PropertyChanged -= CurrentProject_PropertyChanged;
			}
			m_pCurrentProject = new PrefProject(_ServiceAgent, projectId);
			m_pCurrentProject.PropertyChanged += CurrentProject_PropertyChanged;
			SetPageDataBindings();
			long projectCode = Globals.ProjectCode;
			bool flag = false;
			if (projectCode > 0)
			{
				string strCommandResults = "";
				string strErrors = "";
				flag = CurrentProject.Load(projectCode, ref strCommandResults, ref strErrors);
				if (flag)
				{
					Logger.Instance.WriteInformation("LoadProjectData.1", "PrefGest");
					attachmentsView.LoadAttachments(CurrentProject.ProjectId, 36);
					if (CurrentProject.ShowPricePolicy)
					{
						Logger.Instance.WriteInformation("LoadProjectData.2", "PrefGest");
						SalesDocument salesDocForValuation = CurrentProject.SalesDocForValuation;
						if (salesDocForValuation != null)
						{
							staticDataBasedOn.Text = "(" + Preference.Wpf.Controls.Properties.Resources.IDS_DATABASEDON + ": " + salesDocForValuation.Number + "/" + salesDocForValuation.Version + ")";
							tabitemTariffsExceptions.Visibility = Visibility.Visible;
						}
						else
						{
							tabitemTariffsExceptions.Visibility = Visibility.Collapsed;
						}
					}
					else
					{
						tabitemTariffsExceptions.Visibility = Visibility.Collapsed;
					}
				}
				else
				{
					string empty = string.Empty;
					try
					{
						XmlDocument xmlDocument = new XmlDocument();
						xmlDocument.LoadXml(strErrors);
						empty = xmlDocument.FirstChild.FirstChild.Attributes["Info"].Value.ToString().TrimEnd();
					}
					catch (Exception)
					{
						empty = "Raw Errors: " + strErrors;
					}
				}
			}
			return flag;
		}
		catch (Exception ex2)
		{
			Logger.Instance.WriteError(ex2, "PrefGest");
			throw;
		}
	}

	private void RecalculateSalesDocs()
	{
		base.Cursor = Cursors.AppStarting;
		listviewTariffExceptions1.Cursor = Cursors.AppStarting;
		listviewTariffExceptions2.Cursor = Cursors.AppStarting;
		buttonSave.IsEnabled = false;
		stackpanelProgressLoop.Visibility = Visibility.Visible;
		stackpanelProgressLoop.BeginAnimation(UIElement.OpacityProperty, null);
		imageProgressLoop.Source = TryFindResource("ProgressLoopImage") as DrawingImage;
		stackpanelProgressLoop.Opacity = 1.0;
		textProgressLoop.Text = Preference.Wpf.Controls.Properties.Resources.IDS_SALESDOCSRECALCULATING;
		if (TryFindResource("TimeLineCircularProgress") is DoubleAnimationUsingKeyFrames animation)
		{
			rotationProgressLoop.BeginAnimation(RotateTransform.AngleProperty, animation);
		}
		CurrentProject.RecalculateSalesDocs(PrefUserLink);
		base.Cursor = Cursors.Arrow;
		listviewTariffExceptions1.Cursor = Cursors.Arrow;
		listviewTariffExceptions2.Cursor = Cursors.Arrow;
		buttonSave.IsEnabled = true;
		textProgressLoop.Text = Preference.Wpf.Controls.Properties.Resources.IDS_TASKCOMPLETED;
		rotationProgressLoop.BeginAnimation(RotateTransform.AngleProperty, null);
		imageProgressLoop.Source = TryFindResource("ProgressLoopEndedImage") as DrawingImage;
		DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFrames = new DoubleAnimationUsingKeyFrames();
		LinearDoubleKeyFrame keyFrame = new LinearDoubleKeyFrame(1.0, TimeSpan.FromSeconds(3.0));
		LinearDoubleKeyFrame keyFrame2 = new LinearDoubleKeyFrame(0.0, TimeSpan.FromSeconds(5.0));
		doubleAnimationUsingKeyFrames.KeyFrames.Add(keyFrame);
		doubleAnimationUsingKeyFrames.KeyFrames.Add(keyFrame2);
		stackpanelProgressLoop.BeginAnimation(UIElement.OpacityProperty, doubleAnimationUsingKeyFrames);
		RefreshCurrentProject();
	}

	private void ViewDocument(Document selectedDocument)
	{
		string text = string.Empty;
		switch (selectedDocument.DocumentType)
		{
		case DocumentTypes.SalesDocument:
			if (selectedDocument is SalesDocument salesDocument)
			{
				text = "prefgest:/SD=" + salesDocument.Number + "." + salesDocument.Version;
			}
			break;
		case DocumentTypes.ProductionLot:
			if (selectedDocument is ProductionLotDocument productionLotDocument)
			{
				text = "prefgest:/PL=" + productionLotDocument.Number;
			}
			break;
		case DocumentTypes.PurchasesDocument:
			if (selectedDocument is PurchaseDocument purchaseDocument)
			{
				text = "prefgest:/PD=" + purchaseDocument.Number + "." + purchaseDocument.Numeration;
			}
			break;
		case DocumentTypes.WarehouseDocument:
			if (selectedDocument is WarehouseDocument warehouseDocument)
			{
				text = "prefgest:/WD=" + warehouseDocument.Number;
			}
			break;
		case DocumentTypes.ShippingLot:
			if (selectedDocument is ShippingLot shippingLot)
			{
				text = "prefgest:/SL=" + shippingLot.Number;
			}
			break;
		}
		if (!string.IsNullOrEmpty(text))
		{
			try
			{
				Process.Start(text);
			}
			catch (Exception)
			{
				MessageBox.Show("PrefGest could not be found", "PrefProjects");
			}
		}
	}

	private void buttonRefreshCustomers_Click(object sender, RoutedEventArgs e)
	{
		Mouse.OverrideCursor = Cursors.Wait;
		try
		{
			PrefCustomersList.Initialize();
			comboboxClientName.ItemsSource = PrefCustomersList.Customers;
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefGest");
			throw;
		}
		finally
		{
			Mouse.OverrideCursor = null;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/Preference.Wpf.Controls;component/projects/views/projectview.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	internal Delegate _CreateDelegate(Type delegateType, string handler)
	{
		return Delegate.CreateDelegate(delegateType, this, handler);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	void IComponentConnector.Connect(int connectionId, object target)
	{
		//IL_0ca1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cab: Expected O, but got Unknown
		//IL_0cae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cb8: Expected O, but got Unknown
		//IL_0cbb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cc5: Expected O, but got Unknown
		//IL_0d30: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d3a: Expected O, but got Unknown
		//IL_0da5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0daf: Expected O, but got Unknown
		//IL_0e1a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e24: Expected O, but got Unknown
		switch (connectionId)
		{
		case 1:
			ProjectMainPage = (ProjectView)target;
			break;
		case 9:
			storyboardDocuments1_BeginStoryboard = (BeginStoryboard)target;
			break;
		case 10:
			gridPrincipal2 = (Grid)target;
			break;
		case 11:
			shadow01 = (Image)target;
			break;
		case 12:
			rectangleFrame01 = (Rectangle)target;
			break;
		case 13:
			gridContent = (Grid)target;
			break;
		case 14:
			stackpanelProgressLoop = (StackPanel)target;
			break;
		case 15:
			viewboxProgressLoop = (Viewbox)target;
			break;
		case 16:
			imageProgressLoop = (Image)target;
			break;
		case 17:
			rotationProgressLoop = (RotateTransform)target;
			break;
		case 18:
			textProgressLoop = (TextBlock)target;
			break;
		case 19:
			borderHeader = (Border)target;
			break;
		case 20:
			imagePrefSuiteLogo = (Image)target;
			break;
		case 21:
			stackpanelName = (StackPanel)target;
			break;
		case 22:
			staticProjectName = (TextBlock)target;
			break;
		case 23:
			textboxProjectName = (TextBox)target;
			break;
		case 24:
			staticProjectCode = (TextBlock)target;
			break;
		case 25:
			textblockProjectCode = (TextBlock)target;
			break;
		case 26:
			staticClientName = (TextBlock)target;
			break;
		case 27:
			textboxCustomerCode = (TextBox)target;
			break;
		case 28:
			comboboxClientName = (ComboBox)target;
			break;
		case 29:
			buttonRefreshCustomers = (Button)target;
			buttonRefreshCustomers.Click += buttonRefreshCustomers_Click;
			break;
		case 30:
			stackpanelButtonDetails = (StackPanel)target;
			break;
		case 31:
			buttonSave = (Button)target;
			break;
		case 32:
			buttonUndo = (Button)target;
			break;
		case 33:
			gridTab = (Grid)target;
			break;
		case 34:
			borderTab = (Border)target;
			break;
		case 35:
			tabcontrolContent = (TabControl)target;
			tabcontrolContent.SelectionChanged += tabcontrolContent_SelectionChanged;
			break;
		case 36:
			tabitemDetails = (TabItem)target;
			break;
		case 37:
			gridTabItemDetails = (Grid)target;
			break;
		case 38:
			stackPanelStatus = (Grid)target;
			break;
		case 39:
			staticProjectStatus = (TextBlock)target;
			break;
		case 40:
			staticAgreedAmount = (TextBlock)target;
			break;
		case 41:
			textboxPrice1 = (TextBox)target;
			break;
		case 42:
			staticAgreedAmountCurrency = (TextBlock)target;
			break;
		case 43:
			comboboxPrice2 = (ComboBox)target;
			break;
		case 44:
			stackPanelSalesStatus = (DockPanel)target;
			break;
		case 45:
			textblockSales = (TextBlock)target;
			break;
		case 46:
			staticSales = (TextBlock)target;
			break;
		case 47:
			stackPanelPurchasesStatus = (DockPanel)target;
			break;
		case 48:
			textblockPurchases = (TextBlock)target;
			break;
		case 49:
			staticPurchases = (TextBlock)target;
			break;
		case 50:
			stackPanelProductionStatus = (DockPanel)target;
			break;
		case 51:
			textblockProduction = (TextBlock)target;
			break;
		case 52:
			staticProduction = (TextBlock)target;
			break;
		case 53:
			stackPanelWorkforceStatus = (DockPanel)target;
			break;
		case 54:
			textblockWorkforce = (TextBlock)target;
			break;
		case 55:
			staticWorkforce = (TextBlock)target;
			break;
		case 56:
			staticProgress = (TextBlock)target;
			break;
		case 57:
			gridStatusBar = (Grid)target;
			break;
		case 58:
			staticAccepted = (TextBlock)target;
			break;
		case 59:
			textblockAccepted = (TextBox)target;
			break;
		case 60:
			staticEstimated = (TextBlock)target;
			break;
		case 61:
			textblockEstimated = (TextBox)target;
			break;
		case 62:
			stackPanelOrdered = (StackPanel)target;
			break;
		case 63:
			staticOrdered = (TextBlock)target;
			break;
		case 64:
			textblockOrdered = (TextBox)target;
			break;
		case 65:
			stackPanelPurchased = (StackPanel)target;
			break;
		case 66:
			staticPurchased = (TextBlock)target;
			break;
		case 67:
			textblockPurchased = (TextBox)target;
			break;
		case 68:
			staticStarted = (TextBlock)target;
			break;
		case 69:
			textblockStarted = (TextBox)target;
			break;
		case 70:
			staticFinished = (TextBlock)target;
			break;
		case 71:
			textblockFinished = (TextBox)target;
			break;
		case 72:
			staticSent = (TextBlock)target;
			break;
		case 73:
			textblockSent = (TextBox)target;
			break;
		case 74:
			staticDelivered = (TextBlock)target;
			break;
		case 75:
			textblockDelivered = (TextBox)target;
			break;
		case 76:
			staticMounted = (TextBlock)target;
			break;
		case 77:
			textblockMounted = (TextBox)target;
			break;
		case 78:
			staticInvoiced = (TextBlock)target;
			break;
		case 79:
			textboxInvoiced = (TextBox)target;
			break;
		case 80:
			stackPanelTotalInstances = (StackPanel)target;
			break;
		case 81:
			staticTotalInstances = (TextBlock)target;
			break;
		case 82:
			textBlockTotalInstances = (TextBlock)target;
			break;
		case 83:
			scrollviewerDetails = (ScrollViewer)target;
			break;
		case 84:
			stackpanelDetails = (StackPanel)target;
			break;
		case 85:
			staticProjectDetails = (TextBlock)target;
			break;
		case 86:
			staticProjectDescription = (TextBlock)target;
			break;
		case 87:
			textboxDescription = (TextBox)target;
			break;
		case 88:
			staticCreationDate = (TextBlock)target;
			break;
		case 89:
			textboxCreationDate = (TextBox)target;
			break;
		case 90:
			expanderShippingAddress = (Expander)target;
			break;
		case 91:
			stackPanelShippingAddress = (StackPanel)target;
			break;
		case 92:
			staticShippingAddressMain = (TextBlock)target;
			break;
		case 93:
			textboxShippingAddressPart1 = (TextBox)target;
			break;
		case 94:
			textboxShippingAddressPart2 = (TextBox)target;
			break;
		case 95:
			staticShippingPostalCode = (TextBlock)target;
			break;
		case 96:
			staticShippingCity = (TextBlock)target;
			break;
		case 97:
			staticShippingProvince = (TextBlock)target;
			break;
		case 98:
			staticShippingCountry = (TextBlock)target;
			break;
		case 99:
			textboxShippingPostal = (TextBox)target;
			break;
		case 100:
			textboxShippingCity = (TextBox)target;
			break;
		case 101:
			textboxShippingProvince = (TextBox)target;
			break;
		case 102:
			textboxShippingCountry = (TextBox)target;
			break;
		case 103:
			expanderInvoicingAddress = (Expander)target;
			break;
		case 104:
			stackPanelInvoicingAddress = (StackPanel)target;
			break;
		case 105:
			staticInvoicingAddressMain = (TextBlock)target;
			break;
		case 106:
			textboxInvoicingAddressPart1 = (TextBox)target;
			break;
		case 107:
			textboxInvoicingAddressPart2 = (TextBox)target;
			break;
		case 108:
			staticInvoicingPostalCode = (TextBlock)target;
			break;
		case 109:
			staticInvoicingCity = (TextBlock)target;
			break;
		case 110:
			staticInvoicingProvince = (TextBlock)target;
			break;
		case 111:
			staticInvoicingCountry = (TextBlock)target;
			break;
		case 112:
			textboxInvoicingPostal = (TextBox)target;
			break;
		case 113:
			textboxInvoicingCity = (TextBox)target;
			break;
		case 114:
			textboxInvoicingProvince = (TextBox)target;
			break;
		case 115:
			textboxInvoicingCountry = (TextBox)target;
			break;
		case 116:
			expanderCustomerAddress = (Expander)target;
			break;
		case 117:
			stackPanelAddress = (StackPanel)target;
			break;
		case 118:
			staticAddressMain = (TextBlock)target;
			break;
		case 119:
			textboxAddressPart1 = (TextBox)target;
			break;
		case 120:
			textboxAddressPart2 = (TextBox)target;
			break;
		case 121:
			staticPostalCode = (TextBlock)target;
			break;
		case 122:
			staticCity = (TextBlock)target;
			break;
		case 123:
			staticProvince = (TextBlock)target;
			break;
		case 124:
			staticCountry = (TextBlock)target;
			break;
		case 125:
			textboxPostal = (TextBox)target;
			break;
		case 126:
			textboxCity = (TextBox)target;
			break;
		case 127:
			textboxProvince = (TextBox)target;
			break;
		case 128:
			textboxCountry = (TextBox)target;
			break;
		case 129:
			stackPanelComments = (StackPanel)target;
			break;
		case 130:
			staticComments = (TextBlock)target;
			break;
		case 131:
			textboxComments = (TextBox)target;
			break;
		case 132:
			stackpanelShorcuts = (StackPanel)target;
			break;
		case 133:
			stackpanelDocuments = (StackPanel)target;
			break;
		case 134:
			imageDocuments = (Image)target;
			break;
		case 135:
			stackpanelSales = (StackPanel)target;
			break;
		case 136:
			imageSales = (Image)target;
			break;
		case 137:
			stackpanelPurchases = (StackPanel)target;
			break;
		case 138:
			imagePurchases = (Image)target;
			break;
		case 139:
			stackpanelTariffExceptions = (StackPanel)target;
			break;
		case 140:
			imageTariffExceptions = (Image)target;
			break;
		case 141:
			stackpanelContacts = (StackPanel)target;
			break;
		case 142:
			imageContacts = (Image)target;
			break;
		case 143:
			stackpanelAttachments = (StackPanel)target;
			break;
		case 144:
			imageAttachments = (Image)target;
			break;
		case 145:
			tabitemTariffsExceptions = (TabItem)target;
			break;
		case 146:
			gridTabItemTariffsExceptions = (Grid)target;
			break;
		case 147:
			staticPricesPolicy = (TextBlock)target;
			break;
		case 148:
			staticDataBasedOn = (TextBlock)target;
			break;
		case 149:
			listviewTariffExceptions1 = (ListView)target;
			break;
		case 150:
			GroupNameColumn = (GridViewColumn)target;
			break;
		case 151:
			ColorColumn = (GridViewColumn)target;
			break;
		case 152:
			OriginCostColumn = (GridViewColumn)target;
			break;
		case 153:
			SupplierDiscountColumn = (GridViewColumn)target;
			break;
		case 154:
			PurchasePriceColumn = (GridViewColumn)target;
			break;
		case 155:
			CostIncrementColumn = (GridViewColumn)target;
			break;
		case 156:
			EffectiveCostColumn = (GridViewColumn)target;
			break;
		case 157:
			CoefficientColumn = (GridViewColumn)target;
			break;
		case 158:
			SaleColumn = (GridViewColumn)target;
			break;
		case 159:
			checkboxShowAllGroups = (CheckBox)target;
			break;
		case 160:
			checkboxFixSale = (CheckBox)target;
			break;
		case 161:
			staticTotalEffectiveCost = (TextBlock)target;
			break;
		case 162:
			textblockTotalEffectiveCost = (TextBlock)target;
			break;
		case 163:
			staticTotalSale = (TextBlock)target;
			break;
		case 164:
			textblockTotalSale = (TextBlock)target;
			break;
		case 165:
			staticSaleDiscount = (TextBlock)target;
			break;
		case 166:
			textboxSaleDiscount = (TextBox)target;
			break;
		case 167:
			staticTotalEffectiveSale = (TextBlock)target;
			break;
		case 168:
			textboxTotalEffectiveSale = (TextBox)target;
			break;
		case 169:
			buttonApply = (Button)target;
			break;
		case 170:
			listviewTariffExceptions2 = (ListView)target;
			break;
		case 171:
			ConceptColumn = (GridViewColumn)target;
			break;
		case 172:
			PercentageColumn = (GridViewColumn)target;
			break;
		case 173:
			AmountColumn = (GridViewColumn)target;
			break;
		case 174:
			staticIndustrialBenefit = (TextBlock)target;
			break;
		case 175:
			textblockIndustrialBenefit = (TextBlock)target;
			break;
		case 176:
			staticBenefitMargin = (TextBlock)target;
			break;
		case 177:
			textboxBenefitMargin = (TextBox)target;
			break;
		case 178:
			stackPanelPriceComments = (StackPanel)target;
			break;
		case 179:
			staticPriceComments = (TextBlock)target;
			break;
		case 180:
			textboxPriceComments = (TextBox)target;
			break;
		case 181:
			tabitemContacts = (TabItem)target;
			break;
		case 182:
			gridTabItemContacts = (Grid)target;
			break;
		case 183:
			staticContacts = (TextBlock)target;
			break;
		case 184:
			gridviewContacts = (RadGridView)target;
			break;
		case 185:
			ContactCodeColumn = (GridViewDataColumn)target;
			break;
		case 186:
			ContactFullNameColumn = (GridViewComboBoxColumn)target;
			break;
		case 187:
			stackpanelButtonsContacts = (StackPanel)target;
			break;
		case 188:
			buttonAddContact = (Button)target;
			break;
		case 189:
			staticAddContact = (TextBlock)target;
			break;
		case 190:
			buttonRemoveContact = (Button)target;
			break;
		case 191:
			staticRemoveContact = (TextBlock)target;
			break;
		case 192:
			tabitemDocumentsTelerik = (TabItem)target;
			break;
		case 193:
			gridtTabItemDocumentsTeleril = (Grid)target;
			break;
		case 194:
			staticDocumentsTelerik = (TextBlock)target;
			break;
		case 195:
			gridviewDocuments = (RadGridView)target;
			break;
		case 196:
			stackpanelButtonsDocumentsTelerik = (StackPanel)target;
			break;
		case 197:
			buttonViewDocumentTelerik = (Button)target;
			break;
		case 198:
			staticViewDocumentTelerik = (TextBlock)target;
			break;
		case 199:
			buttonValuateDocumentTelerik = (Button)target;
			break;
		case 200:
			staticValuateDocumentTelerik = (TextBlock)target;
			break;
		case 201:
			tabitemSalesDocuments = (TabItem)target;
			break;
		case 202:
			gridtTabItemSalesDocuments = (Grid)target;
			break;
		case 203:
			staticSalesDocuments = (TextBlock)target;
			break;
		case 204:
			gridviewSalesDocuments = (RadGridView)target;
			break;
		case 205:
			stackpanelButtonsSalesDocuments = (StackPanel)target;
			break;
		case 206:
			buttonViewSalesDocument = (Button)target;
			break;
		case 207:
			staticViewSalesDocument = (TextBlock)target;
			break;
		case 208:
			buttonValuateSalesDocument = (Button)target;
			break;
		case 209:
			staticValuateSalesDocument = (TextBlock)target;
			break;
		case 210:
			tabitemPurchasesDocuments = (TabItem)target;
			break;
		case 211:
			gridtTabItemPurchasesDocuments = (Grid)target;
			break;
		case 212:
			staticPurchasesDocuments = (TextBlock)target;
			break;
		case 213:
			gridviewPurchasesDocuments = (RadGridView)target;
			break;
		case 214:
			stackpanelButtonsPurchasesDocuments = (StackPanel)target;
			break;
		case 215:
			buttonViewPurchasesDocument = (Button)target;
			break;
		case 216:
			staticViewPurchasesDocument = (TextBlock)target;
			break;
		case 217:
			tabitemAttachments = (TabItem)target;
			break;
		case 218:
			attachmentsView = (AttachmentsView)target;
			break;
		case 219:
			gridProgressBarPanel = (Grid)target;
			break;
		case 220:
			progressBarMain = (ProgressBar)target;
			break;
		case 221:
			textblockProgressMessage = (TextBlock)target;
			break;
		default:
			_contentLoaded = true;
			break;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	void IStyleConnector.Connect(int connectionId, object target)
	{
		switch (connectionId)
		{
		case 2:
			((TextBox)target).GotKeyboardFocus += OnTemplateItemGotFocus;
			break;
		case 3:
			((Button)target).Click += RemnantIncrementButton_Click;
			break;
		case 4:
			((TextBox)target).GotKeyboardFocus += OnTemplateItemGotFocus;
			break;
		case 5:
			((Button)target).Click += ProviderDiscountButton_Click;
			break;
		case 6:
			((TextBox)target).GotKeyboardFocus += OnTemplateItemGotFocus;
			break;
		case 7:
			((Button)target).Click += CoefficientTextBox_Click;
			break;
		case 8:
			((TextBox)target).GotKeyboardFocus += OnTemplateItemGotFocus;
			break;
		}
	}
}
