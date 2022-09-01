using System;
using System.Data.OleDb;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;
using Interop.PrefCAD;
using Preference.Exceptions;
using Preference.Logikal.Api;
using Preference.Logikal.Properties;
using Preference.PrefItems.Custom;
using PrefLicControl;

namespace Preference.Logikal;

[ComVisible(true)]
[ProgId("Preference.Logikal.PrefItem")]
public class PrefItem : IItem, IItemContainerDataRecipient, IDisposable
{
	private InputOfElements _inputOfElements;

	public byte[] Blob { get; private set; }

	public string ItemNumberPAF { get; set; }

	public string ItemVersionPAF { get; set; }

	public string ConnectionString { get; set; }

	public string AutomaticText => null;

	public string ProductionText => null;

	private InputOfElements InputOfElements
	{
		get
		{
			return _inputOfElements;
		}
		set
		{
			_inputOfElements = value;
		}
	}

	private bool IsNewPosition => Blob == null;

	private static bool IsLicensed { get; set; }

	static PrefItem()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Expected O, but got Unknown
		try
		{
			PrefLicControl val = new PrefLicControl();
			try
			{
				IsLicensed = val.CheckModuleUID("2E011FD8-0575-47FC-99A0-7980838DE845", "A43245D2-1DBD-456B-B1C8-956DE9621400") == 0;
			}
			finally
			{
				((IDisposable)val)?.Dispose();
			}
		}
		catch (Exception ex)
		{
			IsLicensed = false;
			EventLog.AddError(PrefException.GetFormattedMessage("Error verifying license for PrefSuite LogiKal integration module.", ex));
		}
	}

	public PrefItem()
	{
		try
		{
			if (IsLicensed)
			{
				LogikalModule.EnsureLoggedIn();
			}
		}
		catch (Exception ex)
		{
			EventLog.AddError(PrefException.GetFormattedMessage("Could not initialize LogiKal module.", ex));
		}
	}

	private void EnsureInputOfElements()
	{
		if (InputOfElements == null)
		{
			InputOfElements = new InputOfElements();
			InputOfElements.SetProjectBlob(GetProjectBlobFile());
			if (IsNewPosition)
			{
				InputOfElements.NewPosition(ElementType.Default);
			}
			else
			{
				InputOfElements.Serialization = Blob;
			}
		}
	}

	public bool Load(string loadXml)
	{
		return true;
	}

	public bool LoadFromSerializationXml(string strXml)
	{
		try
		{
			Blob = null;
			using (TextReader textReader = new StringReader(strXml))
			{
				string value = (new XPathDocument(textReader).CreateNavigator().SelectSingleNode("//LogiKalBlob") ?? throw new InvalidDataException("Invalid item serialization.")).Value;
				if (string.IsNullOrWhiteSpace(value))
				{
					throw new InvalidDataException("Invalid item serialization.");
				}
				Blob = Convert.FromBase64String(value);
				if (Blob == null)
				{
					throw new InvalidDataException("Invalid blob format.");
				}
			}
			return true;
		}
		catch (Exception ex)
		{
			EventLog.AddError(PrefException.GetFormattedMessage("Failed loading LogiKal item serialization.", ex));
			return false;
		}
	}

	public string GetSerializationXml()
	{
		try
		{
			EnsureInputOfElements();
			string text = null;
			if (Blob != null)
			{
				text = Convert.ToBase64String(Blob);
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<LogiKalBlob>");
			if (text != null)
			{
				stringBuilder.Append(text);
			}
			stringBuilder.Append("</LogiKalBlob>");
			return stringBuilder.ToString();
		}
		catch (Exception ex)
		{
			EventLog.AddError(PrefException.GetFormattedMessage("Failed retrieving LogiKal item serialization.", ex));
			return null;
		}
	}

	public string GetDescriptiveXml(long options)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		string text = null;
		string text2 = null;
		bool flag = LogFilesActivated();
		try
		{
			EnsureInputOfElements();
			byte[] exportData = InputOfElements.ExportData;
			if (exportData == null)
			{
				throw new PrefException("Empty export data returned from LogiKal.");
			}
			if (flag)
			{
				using FileStream fileStream = new FileStream(Path.GetTempPath() + "blob.blb", FileMode.Create, FileAccess.Write);
				fileStream.Write(Blob, 0, Blob.Length);
			}
			text = Path.GetTempFileName();
			File.WriteAllBytes(text, exportData);
			text2 = new DescriptiveXmlBuilder
			{
				ExportFilePath = text,
				GenerateLogFiles = flag,
				PrefSuiteConnectionString = ConnectionString,
				XmlOptions = (XMLOptionEnum)options
			}.DescriptiveXml;
		}
		catch (Exception ex)
		{
			EventLog.AddError(PrefException.GetFormattedMessage("Failed retrieving descriptive data for Logikal item.", ex));
			text2 = string.Format(CultureInfo.CurrentCulture, "<Model xmlns:dsc='ModelDescriptive'><dsc:Model prefSuiteItem='custom' name='Invalid item' description='{0}'/></Model>", PrefException.GetFormattedMessage(ex));
		}
		try
		{
			if (text != null)
			{
				if (File.Exists(text))
				{
					File.Delete(text);
					return text2;
				}
				return text2;
			}
			return text2;
		}
		catch (Exception ex2)
		{
			EventLog.AddEvent(EventLogEntryType.Warning, PrefException.GetFormattedMessage("Could not delete temporal file with LogiKal export data.", ex2));
			return text2;
		}
	}

	public bool Properties(bool readOnly, string title, int languageId)
	{
		try
		{
			if (!IsLicensed)
			{
				ShowUnlicensedModule();
				return false;
			}
			EnsureInputOfElements();
			LogikalModule.LanguageId = languageId;
			bool? flag = new PrefItemPropertiesWindow
			{
				InputOfElements = InputOfElements,
				ExecuteMode = (IsNewPosition ? InputOfElementsExecuteMode.Add : InputOfElementsExecuteMode.Edit)
			}.ShowDialog();
			if (flag.HasValue && flag.Value)
			{
				Blob = InputOfElements.Serialization;
			}
			InputOfElements.GetProjectBlob();
			SaveProjectBlobInPrefSuiteDataBase(InputOfElements.ProjectBlob);
			InputOfElements = null;
			return flag.HasValue && flag.Value;
		}
		catch (Exception ex)
		{
			EventLog.AddError(PrefException.GetFormattedMessage("Could not edit the LogiKal item properties.", ex));
			return false;
		}
	}

	public void ExecuteCustomCommand(string commnadName, string commands, out string result, out bool modified)
	{
		result = null;
		modified = false;
	}

	public void Dispose()
	{
		Dispose(bDisposing: true);
		GC.SuppressFinalize(this);
	}

	public void SetContainer(string containerDataXml)
	{
		using XmlReader xmlReader = XmlReader.Create(new StringReader(containerDataXml));
		while (xmlReader.Read())
		{
			if (xmlReader.IsStartElement() && xmlReader.Name == "SalesDoc")
			{
				string text = xmlReader["number"];
				string text2 = xmlReader["version"];
				if (!string.IsNullOrEmpty(text))
				{
					ItemNumberPAF = text;
				}
				if (!string.IsNullOrEmpty(text2))
				{
					ItemVersionPAF = text2;
				}
			}
		}
	}

	protected virtual void Dispose(bool bDisposing)
	{
		InputOfElements = null;
	}

	private static void ShowUnlicensedModule()
	{
		MessageBox.Show(Resources.UnlicensedModule, null, MessageBoxButtons.OK, MessageBoxIcon.Hand);
	}

	private bool LogFilesActivated()
	{
		string text = "0";
		using (OleDbConnection oleDbConnection = new OleDbConnection(ConnectionString))
		{
			oleDbConnection.Open();
			using OleDbCommand oleDbCommand = new OleDbCommand("SELECT Valor FROM VariablesGlobales WHERE Nombre = N'Preference.Logikal.Log'", oleDbConnection);
			OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
			if (oleDbDataReader.HasRows)
			{
				while (oleDbDataReader.Read())
				{
					text = oleDbDataReader[0].ToString();
				}
			}
		}
		if (text == "1")
		{
			return true;
		}
		return false;
	}

	private string GetProjectBlobFile()
	{
		string text = Path.GetTempPath() + "Projectblob.blb";
		try
		{
			using (OleDbConnection oleDbConnection = new OleDbConnection(ConnectionString))
			{
				oleDbConnection.Open();
				using OleDbCommand oleDbCommand = new OleDbCommand("SELECT ProjectBlob FROM PAFOrg WHERE Number =' " + ItemNumberPAF + "' AND Version='" + ItemVersionPAF + "'", oleDbConnection);
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

	private void SaveProjectBlobInPrefSuiteDataBase(string strProjectBlobFile)
	{
		try
		{
			using OleDbConnection oleDbConnection = new OleDbConnection(ConnectionString);
			oleDbConnection.Open();
			byte[] value = File.ReadAllBytes(strProjectBlobFile);
			OleDbCommand oleDbCommand = oleDbConnection.CreateCommand();
			oleDbCommand.CommandText = "IF NOT EXISTS ( SELECT ProjectBlob FROM PAFOrg WHERE Number =' " + ItemNumberPAF + "' AND Version='" + ItemVersionPAF + "')INSERT INTO PAFOrg ([Number], [Version], [ProjectBlob]) VALUES ('" + ItemNumberPAF + "', '" + ItemVersionPAF + "', ? )ELSE UPDATE PAFOrg SET [ProjectBlob] = ? WHERE Number = '" + ItemNumberPAF + "' AND Version = '" + ItemVersionPAF + "'";
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
}
