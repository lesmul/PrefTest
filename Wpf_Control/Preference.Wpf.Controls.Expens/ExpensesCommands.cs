using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using Preference.Commands;
using Preference.Messages;

namespace Preference.Wpf.Controls.Expenses.Models;

public class ExpensesCommands
{
	protected abstract class CmdBase
	{
		private SqlConnection _Conn;

		private PrefMessages _Messages;

		private string _resultXml = "";

		public string ResultXml
		{
			get
			{
				return _resultXml;
			}
			set
			{
				_resultXml = value;
			}
		}

		public SqlConnection Connection
		{
			get
			{
				return _Conn;
			}
			set
			{
				_Conn = value;
			}
		}

		public PrefMessages Messages
		{
			get
			{
				return _Messages;
			}
			set
			{
				_Messages = value;
			}
		}

		public virtual bool Execute()
		{
			try
			{
				if (!CheckPrecondition())
				{
					return false;
				}
			}
			catch (Exception exception)
			{
				if (Messages != null)
				{
					Messages.SetException(exception);
				}
				return false;
			}
			return true;
		}

		protected virtual bool CheckPrecondition()
		{
			if (_Conn == null)
			{
				return false;
			}
			if (_Conn.State == ConnectionState.Closed)
			{
				_Conn.Open();
			}
			return true;
		}
	}

	protected class CmdSaveDocument : CmdBase
	{
		private XmlElement _NodeDocument;

		public XmlElement NodeDocument
		{
			set
			{
				_NodeDocument = value;
			}
		}

		public override bool Execute()
		{
			if (!base.Execute())
			{
				return false;
			}
			try
			{
				if (!SaveDocument())
				{
					return false;
				}
			}
			catch (Exception exception)
			{
				if (base.Messages != null)
				{
					base.Messages.SetException(exception);
				}
				return false;
			}
			return true;
		}

		protected override bool CheckPrecondition()
		{
			if (_NodeDocument == null)
			{
				throw new Exception("Node Document not specified");
			}
			return base.CheckPrecondition();
		}

		private bool SaveDocument()
		{
			SqlCommand sqlCommand = base.Connection.CreateCommand();
			sqlCommand.CommandText = $"EXEC Expenses.pa_SaveDocument N'{_NodeDocument.OuterXml}' ";
			sqlCommand.CommandType = CommandType.Text;
			sqlCommand.ExecuteNonQuery();
			return true;
		}
	}

	protected class CmdGetDocument : CmdBase
	{
		private long m_lNumber;

		public long Number
		{
			set
			{
				m_lNumber = value;
			}
		}

		public override bool Execute()
		{
			if (!base.Execute())
			{
				return false;
			}
			try
			{
				if (!GetDocument())
				{
					return false;
				}
			}
			catch (Exception exception)
			{
				if (base.Messages != null)
				{
					base.Messages.SetException(exception);
				}
				return false;
			}
			return true;
		}

		protected override bool CheckPrecondition()
		{
			if (0 >= m_lNumber)
			{
				throw new Exception("Number not specified");
			}
			return base.CheckPrecondition();
		}

		private bool GetDocument()
		{
			SqlCommand sqlCommand = base.Connection.CreateCommand();
			sqlCommand.CommandText = $"SELECT Expenses.GetDocument({m_lNumber})";
			sqlCommand.CommandType = CommandType.Text;
			XmlReader xmlReader = sqlCommand.ExecuteXmlReader();
			if (xmlReader.Read())
			{
				base.ResultXml = xmlReader.ReadOuterXml();
			}
			xmlReader.Close();
			return true;
		}
	}

	protected class CmdGetExpensesDocumentItemTypes : CmdBase
	{
		public override bool Execute()
		{
			if (!base.Execute())
			{
				return false;
			}
			try
			{
				if (!GetTypes())
				{
					return false;
				}
			}
			catch (Exception exception)
			{
				if (base.Messages != null)
				{
					base.Messages.SetException(exception);
				}
				return false;
			}
			return true;
		}

		private bool GetTypes()
		{
			SqlCommand sqlCommand = base.Connection.CreateCommand();
			sqlCommand.CommandText = "\r\nWITH XMLNAMESPACES('" + Defines.ExpensesXmlNamespace + "' as ped)\r\nSELECT \r\n(\r\n\tSELECT \r\n\t\t'ped:Code/@Value'=T.Code,\r\n\t\t'ped:TypeId/@Value'=T.TypeId,\r\n\t\t'ped:Name/@Value'=T.Name,\r\n\t\t'ped:Description/@Value'=T.Description\r\n\tFROM Expenses.Types T\r\n\tFOR XML PATH('ped:Type'), TYPE\r\n)\r\nFOR XML PATH('ped:ExpensesDocumentItemTypes')\r\n";
			sqlCommand.CommandType = CommandType.Text;
			XmlReader xmlReader = sqlCommand.ExecuteXmlReader();
			if (xmlReader.Read())
			{
				base.ResultXml = xmlReader.ReadOuterXml();
			}
			xmlReader.Close();
			return true;
		}
	}

	protected class CmdGetGlobalvariable : CmdBase
	{
		private string _globalVariableName;

		public string GlobalVariableName
		{
			get
			{
				return _globalVariableName;
			}
			set
			{
				_globalVariableName = value;
			}
		}

		protected override bool CheckPrecondition()
		{
			if (_globalVariableName == null)
			{
				throw new Exception("GlobalVariableName not specified");
			}
			return base.CheckPrecondition();
		}

		public override bool Execute()
		{
			if (!base.Execute())
			{
				return false;
			}
			try
			{
				string commandText = "\r\nWITH XMLNAMESPACES('" + Defines.ExpensesXmlNamespace + "' as ped)\r\nSELECT \r\n(\r\n\tSELECT \r\n\t\t'ped:Name/@Value'=VG.Nombre,\r\n\t\t'ped:Value/@Value'=VG.Valor\r\n\tFROM VariablesGlobales VG\r\n\tWHERE Nombre = '" + GlobalVariableName + "'\r\n\tFOR XML PATH('ped:GlobalVariable'), TYPE\r\n)\r\nFOR XML PATH('ped:GetGlobalVariable')\r\n\t\t\t\t\t";
				SqlCommand sqlCommand = base.Connection.CreateCommand();
				sqlCommand.CommandText = commandText;
				sqlCommand.CommandType = CommandType.Text;
				XmlReader xmlReader = sqlCommand.ExecuteXmlReader();
				if (xmlReader.Read())
				{
					base.ResultXml = xmlReader.ReadOuterXml();
				}
				xmlReader.Close();
				return true;
			}
			catch (Exception exception)
			{
				if (base.Messages != null)
				{
					base.Messages.SetException(exception);
				}
				return false;
			}
		}
	}

	protected class CmdGetNextAvailableDocumentNumber : CmdBase
	{
		public override bool Execute()
		{
			if (!base.Execute())
			{
				return false;
			}
			try
			{
				string commandText = "\r\nWITH XMLNAMESPACES('" + Defines.ExpensesXmlNamespace + "' as ped)\r\nSELECT 'ped:NextNumber/@Value'=MAX(Number)+1  FROM Expenses.Documents\r\nFOR XML PATH('ped:GetNextAvailableDocumentNumber')\r\n\t\t\t\t\t";
				SqlCommand sqlCommand = base.Connection.CreateCommand();
				sqlCommand.CommandText = commandText;
				sqlCommand.CommandType = CommandType.Text;
				XmlReader xmlReader = sqlCommand.ExecuteXmlReader();
				if (xmlReader.Read())
				{
					base.ResultXml = xmlReader.ReadOuterXml();
				}
				xmlReader.Close();
				return true;
			}
			catch (Exception exception)
			{
				if (base.Messages != null)
				{
					base.Messages.SetException(exception);
				}
				return false;
			}
		}
	}

	protected class CmdInsertNewDocument : CmdBase
	{
		private string _title = string.Empty;

		public string Title
		{
			get
			{
				return _title;
			}
			set
			{
				_title = value;
			}
		}

		public override bool Execute()
		{
			//IL_004a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0050: Expected O, but got Unknown
			if (!base.Execute())
			{
				return false;
			}
			try
			{
				string commandText = "\r\nINSERT INTO Expenses.Documents (Title)\r\nVALUES ('" + Title + "')\r\nSELECT NUMBER,DOCUMENTID FROM Expenses.Documents\r\nWHERE NUMBER IN (SELECT MAX(Number) FROM Expenses.Documents)\r\n\t\t\t\t\t";
				SqlCommand sqlCommand = base.Connection.CreateCommand();
				sqlCommand.CommandText = commandText;
				sqlCommand.CommandType = CommandType.Text;
				SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
				if (sqlDataReader.Read())
				{
					CommandXMLWriter val = new CommandXMLWriter();
					val.CreateDocument();
					XmlElement xmlElement = val.CreateCommandResult("InsertNewDocument");
					val.AddIntParameter(xmlElement, "Number", sqlDataReader.GetInt32(0));
					val.AddStringParameter(xmlElement, "DocumentId", sqlDataReader.GetInt32(0).ToString());
					base.ResultXml = val.GetXml();
				}
				return true;
			}
			catch (Exception exception)
			{
				if (base.Messages != null)
				{
					base.Messages.SetException(exception);
				}
				return false;
			}
		}
	}

	protected class CmdGetCurrencyList : CmdBase
	{
		public override bool Execute()
		{
			//IL_0038: Unknown result type (might be due to invalid IL or missing references)
			//IL_003e: Expected O, but got Unknown
			if (!base.Execute())
			{
				return false;
			}
			try
			{
				string commandText = "SELECT Nombre,Simbolo FROM [dbo].Monedas";
				SqlCommand sqlCommand = base.Connection.CreateCommand();
				sqlCommand.CommandText = commandText;
				sqlCommand.CommandType = CommandType.Text;
				SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
				if (sqlDataReader != null)
				{
					CommandXMLWriter val = new CommandXMLWriter();
					val.CreateDocument();
					XmlElement xmlElement = val.CreateCommandResult("GetCurrencyList");
					List<List<XmlElement>> list = new List<List<XmlElement>>();
					while (sqlDataReader.Read())
					{
						List<XmlElement> list2 = new List<XmlElement>();
						list2.Add(val.CreateStringItemValue("Name", sqlDataReader.GetValue(0).ToString().TrimEnd()));
						list2.Add(val.CreateStringItemValue("Symbol", sqlDataReader.GetValue(1).ToString().TrimEnd()));
						list.Add(list2);
					}
					val.AddSetListParameter(xmlElement, "Currencies", list);
					base.ResultXml = val.GetXml();
				}
				return true;
			}
			catch (Exception exception)
			{
				if (base.Messages != null)
				{
					base.Messages.SetException(exception);
				}
				return false;
			}
		}
	}

	protected class CmdGetStaffList : CmdBase
	{
		public override bool Execute()
		{
			//IL_0038: Unknown result type (might be due to invalid IL or missing references)
			//IL_003e: Expected O, but got Unknown
			if (!base.Execute())
			{
				return false;
			}
			try
			{
				string commandText = "SELECT AccountId,EmployeeName FROM vwCmsStaffCodeName";
				SqlCommand sqlCommand = base.Connection.CreateCommand();
				sqlCommand.CommandText = commandText;
				sqlCommand.CommandType = CommandType.Text;
				SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
				if (sqlDataReader != null)
				{
					CommandXMLWriter val = new CommandXMLWriter();
					val.CreateDocument();
					XmlElement xmlElement = val.CreateCommandResult("GetStaffList");
					List<List<XmlElement>> list = new List<List<XmlElement>>();
					while (sqlDataReader.Read())
					{
						List<XmlElement> list2 = new List<XmlElement>();
						list2.Add(val.CreateStringItemValue("Code", sqlDataReader.GetValue(0).ToString().TrimEnd()));
						list2.Add(val.CreateStringItemValue("FullName", sqlDataReader.GetValue(1).ToString().TrimEnd()));
						list.Add(list2);
					}
					val.AddSetListParameter(xmlElement, "Staff", list);
					base.ResultXml = val.GetXml();
				}
				return true;
			}
			catch (Exception exception)
			{
				if (base.Messages != null)
				{
					base.Messages.SetException(exception);
				}
				return false;
			}
		}
	}

	private string _commandsXml;

	private string _resultsXml;

	private CommandXMLReader _commandReader;

	private SqlConnection _connection;

	private PrefMessages _messages;

	public string CommandsXml
	{
		set
		{
			_commandsXml = value;
		}
	}

	public SqlConnection Connection
	{
		get
		{
			return _connection;
		}
		set
		{
			_connection = value;
		}
	}

	public string ResultsXml => _resultsXml;

	public PrefMessages Messages
	{
		get
		{
			return _messages;
		}
		set
		{
			_messages = value;
		}
	}

	public ExpensesCommands()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Expected O, but got Unknown
		_resultsXml = string.Empty;
		_commandReader = new CommandXMLReader();
	}

	public bool Execute()
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Expected O, but got Unknown
		if (_messages == null)
		{
			_messages = new PrefMessages();
		}
		if (!CheckPrecondition())
		{
			return false;
		}
		if (!ExecuteCommands())
		{
			return false;
		}
		if (_connection != null)
		{
			_connection.Close();
		}
		return true;
	}

	private bool CheckPrecondition()
	{
		if (_connection == null)
		{
			return false;
		}
		if (_connection.State == ConnectionState.Closed)
		{
			_connection.Open();
		}
		return true;
	}

	private bool ExecuteCommands()
	{
		try
		{
			if (!_commandReader.CreateDocument(_commandsXml))
			{
				return false;
			}
			XmlNodeList commands = _commandReader.GetCommands();
			foreach (XmlElement item in commands)
			{
				string commandName = _commandReader.GetCommandName(item);
				switch (commandName)
				{
				case "GetCurrencyList":
				{
					CmdGetCurrencyList cmdGetCurrencyList = new CmdGetCurrencyList();
					cmdGetCurrencyList.Connection = _connection;
					cmdGetCurrencyList.Messages = _messages;
					if (!cmdGetCurrencyList.Execute())
					{
						return false;
					}
					_resultsXml = cmdGetCurrencyList.ResultXml;
					break;
				}
				case "GetDocument":
				{
					CmdGetDocument cmdGetDocument = new CmdGetDocument();
					cmdGetDocument.Number = _commandReader.GetLongParameter(item, "Number");
					cmdGetDocument.Connection = _connection;
					cmdGetDocument.Messages = _messages;
					if (!cmdGetDocument.Execute())
					{
						return false;
					}
					_resultsXml = cmdGetDocument.ResultXml;
					break;
				}
				case "GetExpensesDocumentItemTypes":
				{
					CmdGetExpensesDocumentItemTypes cmdGetExpensesDocumentItemTypes = new CmdGetExpensesDocumentItemTypes();
					cmdGetExpensesDocumentItemTypes.Connection = _connection;
					cmdGetExpensesDocumentItemTypes.Messages = _messages;
					if (!cmdGetExpensesDocumentItemTypes.Execute())
					{
						return false;
					}
					_resultsXml = cmdGetExpensesDocumentItemTypes.ResultXml;
					break;
				}
				case "GetGlobalVariable":
				{
					CmdGetGlobalvariable cmdGetGlobalvariable = new CmdGetGlobalvariable();
					cmdGetGlobalvariable.GlobalVariableName = _commandReader.GetStringParameter(item, "Name");
					cmdGetGlobalvariable.Connection = _connection;
					cmdGetGlobalvariable.Messages = _messages;
					if (!cmdGetGlobalvariable.Execute())
					{
						return false;
					}
					_resultsXml = cmdGetGlobalvariable.ResultXml;
					break;
				}
				case "GetStaffList":
				{
					CmdGetStaffList cmdGetStaffList = new CmdGetStaffList();
					cmdGetStaffList.Connection = _connection;
					cmdGetStaffList.Messages = _messages;
					if (!cmdGetStaffList.Execute())
					{
						return false;
					}
					_resultsXml = cmdGetStaffList.ResultXml;
					break;
				}
				case "SaveDocument":
				{
					CmdSaveDocument cmdSaveDocument = new CmdSaveDocument();
					XmlDocument xMLParameter = _commandReader.GetXMLParameter(item, "DocumentXml");
					if (xMLParameter != null)
					{
						cmdSaveDocument.NodeDocument = xMLParameter.DocumentElement;
					}
					cmdSaveDocument.Connection = _connection;
					cmdSaveDocument.Messages = _messages;
					if (!cmdSaveDocument.Execute())
					{
						return false;
					}
					break;
				}
				case "GetNextAvailableDocumentNumber":
				{
					CmdGetNextAvailableDocumentNumber cmdGetNextAvailableDocumentNumber = new CmdGetNextAvailableDocumentNumber();
					cmdGetNextAvailableDocumentNumber.Connection = _connection;
					cmdGetNextAvailableDocumentNumber.Messages = _messages;
					if (!cmdGetNextAvailableDocumentNumber.Execute())
					{
						return false;
					}
					_resultsXml = cmdGetNextAvailableDocumentNumber.ResultXml;
					break;
				}
				case "InsertNewDocument":
				{
					CmdInsertNewDocument cmdInsertNewDocument = new CmdInsertNewDocument();
					cmdInsertNewDocument.Title = _commandReader.GetStringParameter(item, "Title");
					cmdInsertNewDocument.Connection = _connection;
					cmdInsertNewDocument.Messages = _messages;
					if (!cmdInsertNewDocument.Execute())
					{
						return false;
					}
					_resultsXml = cmdInsertNewDocument.ResultXml;
					break;
				}
				default:
					_messages.AddWarning($"Unknown command {commandName}");
					break;
				}
			}
		}
		catch (Exception exception)
		{
			Messages.SetException(exception);
			return false;
		}
		return true;
	}
}
