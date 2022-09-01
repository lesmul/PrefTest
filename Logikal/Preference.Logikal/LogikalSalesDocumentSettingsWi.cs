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

namespace Preference.Logikal;

public class LogikalSalesDocumentSettingsWindow : Window, IComponentConnector
{
	private SalesDoc _salesDoc;

	private static int nIdd;

	internal StackPanel ButtonsPannel;

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

	public LogikalSalesDocumentSettingsWindow()
	{
		InitializeComponent();
	}

	private void Window_Loaded(object sender, RoutedEventArgs e)
	{
		try
		{
			LogikalModule.EnsureLoggedIn();
			LogikalModule.LanguageId = Culture.LCID;
			LoadButtons();
		}
		catch (Exception ex)
		{
			MessageBox.Show(PrefException.GetFormattedMessage("Error loading LogiKal settings.", ex), base.Title, MessageBoxButton.OK, MessageBoxImage.Hand);
			Close();
		}
	}

	private void LoadButtons()
	{
		try
		{
			string text = "";
			for (int i = 0; i <= LogikalSettingsModule.ProgramsGetTypeCount(); i++)
			{
				int nType = LogikalSettingsModule.ProgramsGetType(i);
				text = LogikalSettingsModule.GetSettingName(nType);
				int settingKind = LogikalSettingsModule.GetSettingKind(nType);
				if (!string.IsNullOrEmpty(text) && (settingKind == 1 || settingKind == 2))
				{
					if (settingKind == 2)
					{
						AddButton(text + " (Project)", nType);
					}
					else
					{
						AddButton(text, nType);
					}
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(PrefException.GetFormattedMessage("Error loading LogiKal buttons settings.", ex), base.Title, MessageBoxButton.OK, MessageBoxImage.Hand);
		}
	}

	private void AddButton(string strContent, int nType)
	{
		Button button = new Button();
		button.Content = strContent;
		button.Margin = new Thickness(0.0, 2.0, 0.0, 2.0);
		button.Tag = nType;
		button.Click += newButtonProjectSettings_Click;
		ButtonsPannel.Children.Add(button);
	}

	private void newButtonProjectSettings_Click(object sender, RoutedEventArgs e)
	{
		Button button = sender as Button;
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
		if (button == null)
		{
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
			string projectBlobFile = GetProjectBlobFile(_salesDoc.ConnectionString, salesDocFields["Number"].Value.ToString(), salesDocFields["Version"].Value.ToString());
			num = LogikalSettingsModule.SettingsBegin(0);
			if (num < 0)
			{
				return;
			}
			LogikalSettingsModule.SettingsSetObjData(num, projectBlobFile);
			LogikalSettingsModule.SettingsSetAddress(num, "", strName, "", strStreet, strZipCode, "", strCountry, strTlf, "");
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
						LogikalSettingsModule.SettingsAddPos(nAnz: Convert.ToInt32(salesDocField8.Value), nHandle: num, bPosition: blob, nSize: blob.Length, strName: text + 1, strDescription: strDescription, nId: nIdd++);
						Marshal.ReleaseComObject(fields);
						Marshal.ReleaseComObject(salesDocField6);
						Marshal.ReleaseComObject(salesDocField7);
						Marshal.ReleaseComObject(salesDocField8);
					}
				}
			}
			if (LogikalSettingsModule.ExecuteProgramObj(num, Convert.ToInt32(button.Tag)))
			{
				base.DialogResult = true;
			}
			if (LogikalSettingsModule.ProjectBlobHasChanged(num))
			{
				LogikalSettingsModule.SettingsGetObjectDataF(num, projectBlobFile);
				SaveProjectBlobInPrefSuiteDataBase(projectBlobFile, _salesDoc.ConnectionString, salesDocFields["Number"].Value.ToString(), salesDocFields["Version"].Value.ToString());
			}
			LogikalSettingsModule.SettingsEnd(num);
			if (File.Exists(projectBlobFile))
			{
				File.Delete(projectBlobFile);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(PrefException.GetFormattedMessage("Error loading LogiKal sales document setting: " + button.Content?.ToString() + " ID:" + button.Tag, ex), base.Title, MessageBoxButton.OK, MessageBoxImage.Hand);
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

	private static void SaveProjectBlobInPrefSuiteDataBase(string strProjectBlobFile, string strConnectionString, string strNumeroPaf, string strVersionPaf)
	{
		try
		{
			using OleDbConnection oleDbConnection = new OleDbConnection(strConnectionString);
			oleDbConnection.Open();
			byte[] value = File.ReadAllBytes(strProjectBlobFile);
			OleDbCommand oleDbCommand = oleDbConnection.CreateCommand();
			oleDbCommand.CommandText = "IF NOT EXISTS ( SELECT ProjectBlob FROM PAFOrg WHERE Number =' " + strNumeroPaf + "' AND Version='" + strVersionPaf + "')INSERT INTO PAFOrg ([Number], [Version], [ProjectBlob]) VALUES ('" + strNumeroPaf + "', '" + strVersionPaf + "', ? )ELSE UPDATE PAFOrg SET [ProjectBlob] = ? WHERE Number = '" + strNumeroPaf + "' AND Version = '" + strVersionPaf + "'";
			OleDbParameter oleDbParameter = new OleDbParameter();
			oleDbCommand.Parameters.Add(oleDbParameter);
			oleDbParameter.Value = value;
			OleDbParameter oleDbParameter2 = new OleDbParameter();
			oleDbCommand.Parameters.Add(oleDbParameter2);
			oleDbParameter2.Value = value;
			oleDbCommand.ExecuteNonQuery();
		}
		catch (Exception ex)
		{
			EventLog.AddError(PrefException.GetFormattedMessage("Could not save project blob in db.", ex));
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/Preference.Logikal;component/logikalsalesdocumentsettingswindow.xaml", UriKind.Relative);
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
			((LogikalSalesDocumentSettingsWindow)target).Loaded += Window_Loaded;
			break;
		case 2:
			ButtonsPannel = (StackPanel)target;
			break;
		default:
			_contentLoaded = true;
			break;
		}
	}
}
