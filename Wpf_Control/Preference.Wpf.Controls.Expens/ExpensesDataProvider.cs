using System;
using System.Data.SqlClient;
using System.Xml;
using Preference.Commands;
using Preference.Messages;

namespace Preference.Wpf.Controls.Expenses.Models;

public static class ExpensesDataProvider
{
	private static SqlConnection _connection;

	private static string _serviceEndpoint;

	private static PrefMessages _messages;

	private static bool _isWebService;

	private static string _serviceEndpointRemoteAddress;

	public static string ServiceEndpointRemoteAddress
	{
		get
		{
			return _serviceEndpointRemoteAddress;
		}
		set
		{
			_serviceEndpointRemoteAddress = value;
		}
	}

	public static SqlConnection Connection
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

	public static string ServiceEndpoint
	{
		get
		{
			return _serviceEndpoint;
		}
		set
		{
			_serviceEndpoint = value;
		}
	}

	public static PrefMessages Messages
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

	public static bool IsWebService
	{
		get
		{
			return _isWebService;
		}
		set
		{
			_isWebService = value;
		}
	}

	public static string GetGlobalVariableValue(string globalVariableName)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Expected O, but got Unknown
		CommandXMLWriter val = new CommandXMLWriter();
		val.CreateDocument();
		if (Connection != null)
		{
			val.CreateConnection(Connection.ConnectionString);
		}
		XmlElement xmlElement = val.CreateCommand("GetGlobalVariable");
		val.AddStringParameter(xmlElement, "Name", globalVariableName);
		string ResultsXml = "";
		string MessagesXml = "";
		if (IsWebService)
		{
			ServiceAgent serviceAgent = new ServiceAgent();
			serviceAgent.Execute(val.GetXml(), ref ResultsXml, ref MessagesXml);
		}
		else
		{
			ExpensesCommands expensesCommands = new ExpensesCommands();
			expensesCommands.CommandsXml = val.GetXml();
			expensesCommands.Connection = Connection;
			expensesCommands.Execute();
			ResultsXml = expensesCommands.ResultsXml;
			MessagesXml = ((object)expensesCommands.Messages).ToString();
		}
		if (string.IsNullOrEmpty(ResultsXml))
		{
			return string.Empty;
		}
		try
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(ResultsXml);
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
			xmlNamespaceManager.AddNamespace("ped", Defines.ExpensesXmlNamespace);
			XmlNodeList xmlNodeList = xmlDocument.SelectNodes("descendant::ped:GlobalVariable/*", xmlNamespaceManager);
			foreach (XmlNode item in xmlNodeList)
			{
				string localName = item.LocalName;
				if (localName == "Value")
				{
					return item.Attributes["Value"].Value.ToString();
				}
			}
		}
		catch (Exception)
		{
		}
		return string.Empty;
	}

	static ExpensesDataProvider()
	{
		_connection = null;
		_serviceEndpoint = "";
		_messages = null;
		_isWebService = false;
		_serviceEndpointRemoteAddress = "";
	}
}
