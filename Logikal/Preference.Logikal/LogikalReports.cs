using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Data.OleDb;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Xml;
using Interop.PrefSales;
using Preference.Exceptions;
using Preference.Logikal.Api;

namespace Preference.Logikal;

public class LogikalReports : Window, IComponentConnector
{
	private SalesDoc _salesDoc;

	private static int nIdd;

	internal Button printout_button;

	internal ComboBox reports_list;

	internal Button exit_button;

	internal Label label1;

	private bool _contentLoaded;

	public SalesDoc SalesDoc
	{
		get
		{
			return _salesDoc;
		}
		set
		{
			_salesDoc = value;
		}
	}

	public CultureInfo Culture { get; set; }

	public LogikalReports()
	{
		InitializeComponent();
	}

	private void LoadCombo()
	{
		try
		{
			int pOTypeCount = LogiDll.GetPOTypeCount();
			for (int i = 0; i <= pOTypeCount; i++)
			{
				string reportTypeName = LogikalReportsModule.GetReportTypeName(LogikalReportsModule.GetReportType(i));
				if (reportTypeName != null)
				{
					reports_list.Items.Add(new ComboItem(reportTypeName, LogikalReportsModule.GetReportType(i)));
				}
			}
		}
		catch
		{
			reports_list.Items.Clear();
			reports_list.Items.Add("Error loading Logikal Reports");
		}
	}

	private void Window_Loaded(object sender, RoutedEventArgs e)
	{
		try
		{
			LogikalModule.EnsureLoggedIn();
			LogikalModule.LanguageId = Culture.LCID;
			LoadCombo();
		}
		catch (Exception ex)
		{
			MessageBox.Show(PrefException.GetFormattedMessage("Error loading LogiKal interface.", ex), base.Title, MessageBoxButton.OK, MessageBoxImage.Hand);
		}
	}

	private void printout_button_Click(object sender, RoutedEventArgs e)
	{
		SalesDocFields salesDocFields = null;
		SalesDocField salesDocField = null;
		SalesDocField salesDocField2 = null;
		SalesDocField salesDocField3 = null;
		SalesDocField salesDocField4 = null;
		SalesDocField salesDocField5 = null;
		SalesDocField salesDocField6 = null;
		SalesDocField salesDocField7 = null;
		SalesDocField salesDocField8 = null;
		int num = 0;
		if (reports_list.SelectedItem == null)
		{
			MessageBox.Show("Select report to print, please", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			return;
		}
		try
		{
			salesDocFields = _salesDoc.Fields;
			salesDocField = salesDocFields["CustomerName"];
			string strName = salesDocField.Value.ToString();
			salesDocField2 = salesDocFields["CustomerAddress1"];
			string strStreet = salesDocField2.Value.ToString();
			salesDocField3 = salesDocFields["CustomerTelephone"];
			string strTlf = salesDocField3.Value.ToString();
			salesDocField5 = salesDocFields["CustomerPostalCode"];
			string strZipCode = salesDocField5.Value.ToString();
			salesDocField4 = salesDocFields["CustomerCity"];
			string strCountry = salesDocField4.Value.ToString();
			num = LogikalReportsModule.ReportBegin(((ComboItem)reports_list.SelectedItem).Tag);
			string projectBlobFile = GetProjectBlobFile(_salesDoc.ConnectionString, salesDocFields["Number"].Value.ToString(), salesDocFields["Version"].Value.ToString());
			LogikalReportsModule.ReportSetObjData(num, projectBlobFile);
			LogikalReportsModule.ReportSetAddress(num, "", strName, "", strStreet, strZipCode, "", strCountry, strTlf, "");
			foreach (SalesDocItem item in _salesDoc.Items)
			{
				if (item.Type == SalesDocItemType.SDIType_Custom)
				{
					string xMLItem = item.XMLItem;
					XmlDocument xmlDocument = new XmlDocument();
					xmlDocument.LoadXml(xMLItem);
					XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
					string uri = "http://www.preference.com/XMLSchemas/2006/Serialization";
					xmlNamespaceManager.AddNamespace("psr", uri);
					XmlElement xmlElement = (XmlElement)xmlDocument.SelectSingleNode("/Item/PrefItem/psr:PrefItemCustom", xmlNamespaceManager);
					if (xmlElement != null && !(xmlElement.GetAttribute("type") != "Preference.Logikal.PrefItem"))
					{
						PrefItem prefItem = new PrefItem();
						prefItem.LoadFromSerializationXml(xMLItem);
						byte[] blob = prefItem.Blob;
						SalesDocFields fields = item.Fields;
						salesDocField6 = fields["Nomenclature"];
						string strDescription = salesDocField6.Value.ToString();
						salesDocField7 = fields["SortOrder"];
						string text = salesDocField7.Value.ToString();
						salesDocField8 = fields["Quantity"];
						LogikalReportsModule.ReportAddPos(nAnz: Convert.ToInt32(salesDocField8.Value), nHandle: num, bPosition: blob, nSize: blob.Length, strName: text + 1, strDescription: strDescription, nId: nIdd++);
						Marshal.ReleaseComObject(fields);
						Marshal.ReleaseComObject(salesDocField6);
						Marshal.ReleaseComObject(salesDocField7);
						Marshal.ReleaseComObject(salesDocField8);
					}
				}
			}
			LogikalReportsModule.ReportExecute(num, 1);
			LogikalReportsModule.ReportEnd(num);
		}
		catch (Exception ex)
		{
			MessageBox.Show(PrefException.GetFormattedMessage("Error showing LogiKal report.", ex), base.Title, MessageBoxButton.OK, MessageBoxImage.Hand);
		}
		finally
		{
			Marshal.ReleaseComObject(salesDocFields);
			Marshal.ReleaseComObject(salesDocField);
			Marshal.ReleaseComObject(salesDocField2);
			Marshal.ReleaseComObject(salesDocField3);
			Marshal.ReleaseComObject(salesDocField4);
			Marshal.ReleaseComObject(salesDocField5);
		}
	}

	private void Window_Closed(object sender, EventArgs e)
	{
		Marshal.ReleaseComObject(_salesDoc);
	}

	private void exit_button_Click(object sender, RoutedEventArgs e)
	{
		Close();
	}

	private static string GetProjectBlobFile(string strConnectionString, string strNumeroPaf, string strVersionPaf)
	{
		string text = Path.GetTempPath() + "Projectblob.blb";
		try
		{
			using (OleDbConnection oleDbConnection = new OleDbConnection(strConnectionString))
			{
				oleDbConnection.Open();
				using OleDbCommand oleDbCommand = new OleDbCommand("SELECT ProjectBlob FROM PAFOrg WHERE Number =' " + strNumeroPaf + "' AND Version='" + strVersionPaf + "'", oleDbConnection);
				object obj = oleDbCommand.ExecuteScalar();
				if (obj != null && obj != DBNull.Value)
				{
					byte[] array = (byte[])oleDbCommand.ExecuteScalar();
					using FileStream fileStream = new FileStream(text, FileMode.Create, FileAccess.Write);
					fileStream.Write(array, 0, array.Length);
				}
			}
			return text;
		}
		catch (Exception ex)
		{
			EventLog.AddError(PrefException.GetFormattedMessage("Could not get project blob file from db", ex));
			return text;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/Preference.Logikal;component/logikalreports.xaml", UriKind.Relative);
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
		case 1:
			((LogikalReports)target).Loaded += Window_Loaded;
			((LogikalReports)target).Closed += Window_Closed;
			break;
		case 2:
			printout_button = (Button)target;
			printout_button.Click += printout_button_Click;
			break;
		case 3:
			reports_list = (ComboBox)target;
			break;
		case 4:
			exit_button = (Button)target;
			exit_button.Click += exit_button_Click;
			break;
		case 5:
			label1 = (Label)target;
			break;
		default:
			_contentLoaded = true;
			break;
		}
	}
}
