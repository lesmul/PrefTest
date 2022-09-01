using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;
using Preference.Data.SqlClient;
using Preference.Wpf.Controls.Attachments.Views;
using Preference.Wpf.Controls.Expenses.Models;
using Preference.Wpf.Controls.Properties;

namespace Preference.Wpf.Controls.Expenses;

public class ExpensesControl : UserControl, IComponentConnector, IStyleConnector
{
	private ExpensesDoc _document;

	private string _Title = "Expenses Documents";

	internal ScrollViewer scrollviewer01;

	internal Grid gridPrincipal01;

	internal Image imageBgRightBottom0;

	internal Image imageBgLeftTop0;

	internal Grid gridPrincipal02;

	internal Border borderFrame01;

	internal Rectangle rectangleFrame1Bottom;

	internal Image imagePreferenceLogo0;

	internal TextBlock textblockCopyright;

	internal Image shadow01;

	internal Rectangle rectangleFrame01;

	internal Grid gridContent0;

	internal TabControl tabcontrolContent;

	internal TabItem tabitemDetails;

	internal Grid gridDocument;

	internal StackPanel stackpanelIconExpenses;

	internal StackPanel stackpanelDetails;

	internal TextBlock textblockDetails;

	internal ListView listviewItems;

	internal GridView gridviewItems;

	internal GridViewColumn ConceptColumn;

	internal GridViewColumn DescriptionColumn;

	internal GridViewColumn TypeColumn;

	internal GridViewColumn UnitPriceColumn;

	internal GridViewColumn QuantityColumn;

	internal GridViewColumn TaxColumn;

	internal GridViewColumn AmountColumn;

	internal StackPanel stackpanelTotals;

	internal TextBlock textblockTotals;

	internal Border borderTotals;

	internal StackPanel stackpanelLoadedDocument1;

	internal TextBlock textblockItemsCount;

	internal TextBox textboxItemsCount;

	internal TextBlock textblockTaxesTotal;

	internal TextBox textboxTaxes;

	internal TextBlock textblockTotalAmount;

	internal TextBox textboxTotal;

	internal StackPanel stackpanelButtonsDetails;

	internal Button buttonAddLine;

	internal TextBlock textblockAddDetail;

	internal Button buttonDeleteLine;

	internal TextBlock textblockRemoveDetail;

	internal Button buttonViewLineAttachments;

	internal TextBlock textblockViewLineAttachments;

	internal StackPanel stackpanelLoadedDoc;

	internal TextBlock textblockLoadedDoc;

	internal Border borderHeader;

	internal WrapPanel wrapPanelButtonsDocument;

	internal Button buttonOpen;

	internal Button buttonSave;

	internal Button buttonViewDocumentAttachments;

	internal Button buttonNew;

	internal Grid gridHeader;

	internal StackPanel stackpanelLoadedDocument;

	internal StackPanel stackpanelOptionalData;

	internal StackPanel stackpanelNumber;

	internal TextBlock textblockNumber;

	internal TextBox textboxNumber;

	internal StackPanel stackpanelDocumentDate;

	internal TextBlock textblockDocumentDate;

	internal TextBox textboxDate;

	internal StackPanel stackpanelCurrency;

	internal TextBlock textblockCurrency;

	internal ComboBox comboboxCurrency;

	internal StackPanel stackpanelDocumentTitle;

	internal TextBlock textblockDocumentTitle;

	internal TextBox textboxDocumentTitle;

	internal StackPanel stackpanelCostDriver;

	internal TextBlock textblockCostDriver;

	internal ComboBox comboboxCostDriver;

	internal TabItem tabitemAttachments;

	internal TextBlock textblockAttachmentsOwner;

	internal AttachmentsView attachmentsView;

	internal Border borderHeader0;

	internal Image imagePrefSuiteLogo0;

	internal StackPanel stackpanelTitle;

	internal TextBlock textblockTitle;

	internal TextBlock textblockTitleReflection;

	private bool _contentLoaded;

	public string Title
	{
		get
		{
			return _Title;
		}
		set
		{
			_Title = value;
		}
	}

	public ExpensesDoc Document
	{
		get
		{
			return _document;
		}
		set
		{
			_document = value;
			base.DataContext = _document;
			if (_document != null)
			{
				listviewItems.ItemsSource = _document.Items;
				_document.Header.PropertyChanged += Header_PropertyChanged;
			}
		}
	}

	public string AssemblyCopyright
	{
		get
		{
			object[] customAttributes = Assembly.GetAssembly(GetType()).GetCustomAttributes(typeof(AssemblyCopyrightAttribute), inherit: false);
			if (customAttributes.Length == 0)
			{
				return "";
			}
			return ((AssemblyCopyrightAttribute)customAttributes[0]).Copyright;
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
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Expected O, but got Unknown
			SqlConnectionStringBuilder val = new SqlConnectionStringBuilder(value);
			Globals.ConnectionString = val.get_ConnectionString();
			(Document as ExpensesDocLocal).Connection = new SqlConnection(val.get_ConnectionString());
			attachmentsView.ConnectionString = val.get_ConnectionString();
		}
	}

	private void Header_PropertyChanged(object sender, PropertyChangedEventArgs e)
	{
		if (Document.Header.Number <= 0)
		{
			listviewItems.IsEnabled = false;
		}
		else
		{
			listviewItems.IsEnabled = true;
		}
	}

	public ExpensesControl()
	{
		InitializeComponent();
		Document = new ExpensesDocLocal();
		tabitemAttachments.Visibility = Visibility.Collapsed;
		textboxNumber.TextChanged += textboxNumber_TextChanged;
		textboxNumber.KeyDown += textboxNumber_KeyDown;
		base.SizeChanged += ExpensesControl_SizeChanged;
		Localize();
	}

	private void textboxNumber_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.Key == Key.Return || e.Key == Key.Return)
		{
			OnLoadButtonClick(sender, new RoutedEventArgs());
		}
	}

	private void Localize()
	{
		_Title = Preference.Wpf.Controls.Properties.Resources.ExpensesDocuments;
		base.Language = XmlLanguage.GetLanguage(Thread.CurrentThread.CurrentUICulture.IetfLanguageTag);
		textblockAddDetail.Text = Preference.Wpf.Controls.Properties.Resources.AddDetail;
		textblockAttachmentsOwner.Text = Preference.Wpf.Controls.Properties.Resources.DocumentAttachments;
		textblockCopyright.Text = AssemblyCopyright + "." + Preference.Wpf.Controls.Properties.Resources.AllRightsReserved + ". www.prefsuite.com";
		textblockCostDriver.Text = Preference.Wpf.Controls.Properties.Resources.CostDriver + ":";
		textblockCurrency.Text = Preference.Wpf.Controls.Properties.Resources.Currency + ":";
		textblockDocumentDate.Text = Preference.Wpf.Controls.Properties.Resources.DocumentDate + ":";
		textblockDocumentTitle.Text = Preference.Wpf.Controls.Properties.Resources.DocumentTitle + ":";
		textblockDetails.Text = Preference.Wpf.Controls.Properties.Resources.Details;
		textblockItemsCount.Text = Preference.Wpf.Controls.Properties.Resources.DetailCount;
		textblockLoadedDoc.Text = Preference.Wpf.Controls.Properties.Resources.LoadedDocument;
		textblockNumber.Text = Preference.Wpf.Controls.Properties.Resources.Number + ":";
		textblockRemoveDetail.Text = Preference.Wpf.Controls.Properties.Resources.RemoveDetail;
		textblockTaxesTotal.Text = Preference.Wpf.Controls.Properties.Resources.TaxesTotal;
		textblockTitle.Text = Preference.Wpf.Controls.Properties.Resources.ExpensesDocuments;
		textblockTitleReflection.Text = Preference.Wpf.Controls.Properties.Resources.ExpensesDocuments;
		textblockTotalAmount.Text = Preference.Wpf.Controls.Properties.Resources.TotalAmount;
		textblockTotals.Text = Preference.Wpf.Controls.Properties.Resources.Totals;
		textblockViewLineAttachments.Text = Preference.Wpf.Controls.Properties.Resources.ViewLineAttachments;
		buttonAddLine.ToolTip = Preference.Wpf.Controls.Properties.Resources.AddDetail;
		buttonDeleteLine.ToolTip = Preference.Wpf.Controls.Properties.Resources.RemoveDetail;
		buttonNew.ToolTip = Preference.Wpf.Controls.Properties.Resources.NewDocument;
		buttonOpen.ToolTip = Preference.Wpf.Controls.Properties.Resources.OpenDocument;
		buttonSave.ToolTip = Preference.Wpf.Controls.Properties.Resources.SaveDocument;
		buttonViewDocumentAttachments.ToolTip = Preference.Wpf.Controls.Properties.Resources.ViewDocumentAttachments;
		buttonViewLineAttachments.ToolTip = Preference.Wpf.Controls.Properties.Resources.ViewLineAttachments;
		ConceptColumn.Header = Preference.Wpf.Controls.Properties.Resources.Concept;
		DescriptionColumn.Header = Preference.Wpf.Controls.Properties.Resources.Description;
		TypeColumn.Header = Preference.Wpf.Controls.Properties.Resources.Type;
		UnitPriceColumn.Header = Preference.Wpf.Controls.Properties.Resources.UnitPrice;
		QuantityColumn.Header = Preference.Wpf.Controls.Properties.Resources.Quantity;
		TaxColumn.Header = Preference.Wpf.Controls.Properties.Resources.Tax;
		AmountColumn.Header = Preference.Wpf.Controls.Properties.Resources.Amount;
		tabitemAttachments.Header = Preference.Wpf.Controls.Properties.Resources.Attachments;
		tabitemDetails.Header = Preference.Wpf.Controls.Properties.Resources.Content;
	}

	private void textboxNumber_TextChanged(object sender, TextChangedEventArgs e)
	{
		EnableControls(enable: false);
	}

	public void EnableControls(bool enable)
	{
		stackpanelDetails.IsEnabled = enable;
		stackpanelButtonsDetails.IsEnabled = enable;
		textboxDate.IsEnabled = enable;
		textboxDocumentTitle.IsEnabled = enable;
		comboboxCostDriver.IsEnabled = enable;
		comboboxCurrency.IsEnabled = enable;
		buttonSave.IsEnabled = enable;
		buttonViewDocumentAttachments.IsEnabled = enable;
	}

	private void ExpensesControl_SizeChanged(object sender, SizeChangedEventArgs e)
	{
		double num = base.ActualWidth / gridPrincipal02.Width;
		double num2 = base.ActualHeight / gridPrincipal02.Height;
		double num3 = 1.0;
		num3 = ((!(num < num2)) ? num2 : num);
		num3 = ((!(num3 < 1.1)) ? (num3 - 0.1) : 1.0);
		gridPrincipal02.LayoutTransform = new ScaleTransform(num3, num3);
	}

	private void OnTemplateItemGotFocus(object sender, RoutedEventArgs e)
	{
		if (e.Source is FrameworkElement frameworkElement && frameworkElement.TemplatedParent is ContentPresenter contentPresenter && contentPresenter.Parent is GridViewRowPresenter gridViewRowPresenter && gridViewRowPresenter.TemplatedParent is Control container && ItemsControl.ItemsControlFromItemContainer(container) is Selector selector)
		{
			selector.SelectedItem = contentPresenter.Content;
		}
	}

	private void OnLoadButtonClick(object sender, RoutedEventArgs e)
	{
		if (!Validation.GetHasError(textboxNumber) && Document.Header.Number > 0 && Document.Load(Convert.ToInt64(textboxNumber.Text)))
		{
			EnableControls(enable: true);
		}
		tabitemAttachments.Visibility = Visibility.Collapsed;
	}

	public void LoadDocument(int docNumber)
	{
		if (Document.Load(Convert.ToInt64(docNumber)))
		{
			EnableControls(enable: true);
		}
	}

	private void OnNewButtonClick(object sender, RoutedEventArgs e)
	{
		NewDocument();
	}

	public void NewDocument()
	{
		if (Document.BuildNewDocument())
		{
			EnableControls(enable: true);
		}
		tabitemAttachments.Visibility = Visibility.Collapsed;
	}

	private void OnSaveButtonClick(object sender, RoutedEventArgs e)
	{
		Save();
	}

	public void Save()
	{
		if (!Validation.GetHasError(textboxNumber))
		{
			Document.Save();
			if (TryFindResource("ExpensesDocItemTypeListResource") is ExpensesDocItemTypeList expensesDocItemTypeList)
			{
				expensesDocItemTypeList.Populate();
			}
		}
	}

	public int AddNewExpensesDoc(string name)
	{
		return Document.AddNewExpensesDoc(name);
	}

	private void OnButtonAddDetailClick(object sender, RoutedEventArgs e)
	{
		ExpensesDocItem expensesDocItem = new ExpensesDocItem();
		expensesDocItem.LineCode = Document.Items.Count + 1;
		expensesDocItem.SortOrder = Document.Items.Count + 1;
		expensesDocItem.DetailId = Guid.NewGuid();
		Document.Items.Add(expensesDocItem);
		if (listviewItems.SelectedItems != null && listviewItems.SelectedItems.Count > 0)
		{
			ExpensesDocItem expensesDocItem2 = listviewItems.SelectedItems[0] as ExpensesDocItem;
			expensesDocItem.Quantity = expensesDocItem2.Quantity;
			expensesDocItem.UnitPrice = expensesDocItem2.UnitPrice;
			expensesDocItem.TaxFactor = expensesDocItem2.TaxFactor;
			expensesDocItem.Concept = expensesDocItem2.Concept;
			expensesDocItem.Description = expensesDocItem2.Description;
			expensesDocItem.Type = expensesDocItem2.Type;
		}
	}

	private void OnButtonRemoveDetailClick(object sender, RoutedEventArgs e)
	{
		if (listviewItems.SelectedItems == null)
		{
			return;
		}
		int count = listviewItems.SelectedItems.Count;
		for (int i = 0; i < count; i++)
		{
			object obj = listviewItems.SelectedItems[0];
			if (obj is ExpensesDocItem)
			{
				Document.Items.Remove(obj as ExpensesDocItem);
			}
		}
		for (int j = 0; j < Document.Items.Count; j++)
		{
			Document.Items[j].LineCode = j + 1;
			Document.Items[j].SortOrder = j + 1;
		}
	}

	private void OnButtonViewDocumentAttachmentsClick(object sender, RoutedEventArgs e)
	{
		if (_document.Header.Number > 0)
		{
			tabitemAttachments.Visibility = Visibility.Visible;
			attachmentsView.LoadAttachments(_document.Header.DocumentId, 37);
			tabcontrolContent.SelectedIndex = 1;
			textblockAttachmentsOwner.Text = Preference.Wpf.Controls.Properties.Resources.DocumentAttachments + " (" + _document.Header.Title + ")";
		}
	}

	private void OnButtonViewLineAttachmentsClick(object sender, RoutedEventArgs e)
	{
		if (listviewItems.SelectedItems != null && listviewItems.SelectedItems.Count >= 1)
		{
			tabitemAttachments.Visibility = Visibility.Visible;
			object obj = listviewItems.SelectedItems[0];
			if (obj is ExpensesDocItem expensesDocItem)
			{
				attachmentsView.LoadAttachments(expensesDocItem.DetailId, 102);
				tabcontrolContent.SelectedIndex = 1;
				textblockAttachmentsOwner.Text = Preference.Wpf.Controls.Properties.Resources.AttachmentsOfDetailNumber + expensesDocItem.SortOrder + " (" + expensesDocItem.Concept + ")";
			}
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/Preference.Wpf.Controls;component/expenses/expensescontrol.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	void IComponentConnector.Connect(int connectionId, object target)
	{
		switch (connectionId)
		{
		case 8:
			scrollviewer01 = (ScrollViewer)target;
			break;
		case 9:
			gridPrincipal01 = (Grid)target;
			break;
		case 10:
			imageBgRightBottom0 = (Image)target;
			break;
		case 11:
			imageBgLeftTop0 = (Image)target;
			break;
		case 12:
			gridPrincipal02 = (Grid)target;
			break;
		case 13:
			borderFrame01 = (Border)target;
			break;
		case 14:
			rectangleFrame1Bottom = (Rectangle)target;
			break;
		case 15:
			imagePreferenceLogo0 = (Image)target;
			break;
		case 16:
			textblockCopyright = (TextBlock)target;
			break;
		case 17:
			shadow01 = (Image)target;
			break;
		case 18:
			rectangleFrame01 = (Rectangle)target;
			break;
		case 19:
			gridContent0 = (Grid)target;
			break;
		case 20:
			tabcontrolContent = (TabControl)target;
			break;
		case 21:
			tabitemDetails = (TabItem)target;
			break;
		case 22:
			gridDocument = (Grid)target;
			break;
		case 23:
			stackpanelIconExpenses = (StackPanel)target;
			break;
		case 24:
			stackpanelDetails = (StackPanel)target;
			break;
		case 25:
			textblockDetails = (TextBlock)target;
			break;
		case 26:
			listviewItems = (ListView)target;
			break;
		case 27:
			gridviewItems = (GridView)target;
			break;
		case 28:
			ConceptColumn = (GridViewColumn)target;
			break;
		case 29:
			DescriptionColumn = (GridViewColumn)target;
			break;
		case 30:
			TypeColumn = (GridViewColumn)target;
			break;
		case 31:
			UnitPriceColumn = (GridViewColumn)target;
			break;
		case 32:
			QuantityColumn = (GridViewColumn)target;
			break;
		case 33:
			TaxColumn = (GridViewColumn)target;
			break;
		case 34:
			AmountColumn = (GridViewColumn)target;
			break;
		case 35:
			stackpanelTotals = (StackPanel)target;
			break;
		case 36:
			textblockTotals = (TextBlock)target;
			break;
		case 37:
			borderTotals = (Border)target;
			break;
		case 38:
			stackpanelLoadedDocument1 = (StackPanel)target;
			break;
		case 39:
			textblockItemsCount = (TextBlock)target;
			break;
		case 40:
			textboxItemsCount = (TextBox)target;
			break;
		case 41:
			textblockTaxesTotal = (TextBlock)target;
			break;
		case 42:
			textboxTaxes = (TextBox)target;
			break;
		case 43:
			textblockTotalAmount = (TextBlock)target;
			break;
		case 44:
			textboxTotal = (TextBox)target;
			break;
		case 45:
			stackpanelButtonsDetails = (StackPanel)target;
			break;
		case 46:
			buttonAddLine = (Button)target;
			buttonAddLine.Click += OnButtonAddDetailClick;
			break;
		case 47:
			textblockAddDetail = (TextBlock)target;
			break;
		case 48:
			buttonDeleteLine = (Button)target;
			buttonDeleteLine.Click += OnButtonRemoveDetailClick;
			break;
		case 49:
			textblockRemoveDetail = (TextBlock)target;
			break;
		case 50:
			buttonViewLineAttachments = (Button)target;
			buttonViewLineAttachments.Click += OnButtonViewLineAttachmentsClick;
			break;
		case 51:
			textblockViewLineAttachments = (TextBlock)target;
			break;
		case 52:
			stackpanelLoadedDoc = (StackPanel)target;
			break;
		case 53:
			textblockLoadedDoc = (TextBlock)target;
			break;
		case 54:
			borderHeader = (Border)target;
			break;
		case 55:
			wrapPanelButtonsDocument = (WrapPanel)target;
			break;
		case 56:
			buttonOpen = (Button)target;
			buttonOpen.Click += OnLoadButtonClick;
			break;
		case 57:
			buttonSave = (Button)target;
			buttonSave.Click += OnSaveButtonClick;
			break;
		case 58:
			buttonViewDocumentAttachments = (Button)target;
			buttonViewDocumentAttachments.Click += OnButtonViewDocumentAttachmentsClick;
			break;
		case 59:
			buttonNew = (Button)target;
			buttonNew.Click += OnNewButtonClick;
			break;
		case 60:
			gridHeader = (Grid)target;
			break;
		case 61:
			stackpanelLoadedDocument = (StackPanel)target;
			break;
		case 62:
			stackpanelOptionalData = (StackPanel)target;
			break;
		case 63:
			stackpanelNumber = (StackPanel)target;
			break;
		case 64:
			textblockNumber = (TextBlock)target;
			break;
		case 65:
			textboxNumber = (TextBox)target;
			break;
		case 66:
			stackpanelDocumentDate = (StackPanel)target;
			break;
		case 67:
			textblockDocumentDate = (TextBlock)target;
			break;
		case 68:
			textboxDate = (TextBox)target;
			break;
		case 69:
			stackpanelCurrency = (StackPanel)target;
			break;
		case 70:
			textblockCurrency = (TextBlock)target;
			break;
		case 71:
			comboboxCurrency = (ComboBox)target;
			break;
		case 72:
			stackpanelDocumentTitle = (StackPanel)target;
			break;
		case 73:
			textblockDocumentTitle = (TextBlock)target;
			break;
		case 74:
			textboxDocumentTitle = (TextBox)target;
			break;
		case 75:
			stackpanelCostDriver = (StackPanel)target;
			break;
		case 76:
			textblockCostDriver = (TextBlock)target;
			break;
		case 77:
			comboboxCostDriver = (ComboBox)target;
			break;
		case 78:
			tabitemAttachments = (TabItem)target;
			break;
		case 79:
			textblockAttachmentsOwner = (TextBlock)target;
			break;
		case 80:
			attachmentsView = (AttachmentsView)target;
			break;
		case 81:
			borderHeader0 = (Border)target;
			break;
		case 82:
			imagePrefSuiteLogo0 = (Image)target;
			break;
		case 83:
			stackpanelTitle = (StackPanel)target;
			break;
		case 84:
			textblockTitle = (TextBlock)target;
			break;
		case 85:
			textblockTitleReflection = (TextBlock)target;
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
		case 1:
			((TextBox)target).GotKeyboardFocus += OnTemplateItemGotFocus;
			break;
		case 2:
			((TextBox)target).GotKeyboardFocus += OnTemplateItemGotFocus;
			break;
		case 3:
			((ComboBox)target).GotKeyboardFocus += OnTemplateItemGotFocus;
			break;
		case 4:
			((TextBox)target).GotKeyboardFocus += OnTemplateItemGotFocus;
			break;
		case 5:
			((TextBox)target).GotKeyboardFocus += OnTemplateItemGotFocus;
			break;
		case 6:
			((TextBox)target).GotKeyboardFocus += OnTemplateItemGotFocus;
			break;
		case 7:
			((TextBlock)target).GotKeyboardFocus += OnTemplateItemGotFocus;
			break;
		}
	}
}
