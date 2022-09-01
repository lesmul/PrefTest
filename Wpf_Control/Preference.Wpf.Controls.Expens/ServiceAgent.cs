using System;
using System.Data.SqlClient;
using System.Xml;
using Preference.Commands;
using Preference.Messages;

namespace Preference.Wpf.Controls.Expenses.Models;

public class ServiceAgent : IServiceAgent
{
	private ExpensesCommands _prefExpensesCommands;

	private PrefMessages _messages;

	private string _connectionString = "";

	public ServiceAgent()
	{
		_prefExpensesCommands = new ExpensesCommands();
		_connectionString = Globals.ConnectionString;
	}

	public bool Execute(string CommandsXml, ref string ResultsXml, ref string MessagesXml)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Expected O, but got Unknown
		if (_messages == null)
		{
			_messages = new PrefMessages();
		}
		bool flag = true;
		try
		{
			if (CommandsXml.Contains(CommandsXML.COMMANDSXML_ELEMENT_CONNECTION))
			{
				try
				{
					XmlDocument xmlDocument = new XmlDocument();
					xmlDocument.LoadXml(CommandsXml);
					XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
					xmlNamespaceManager.AddNamespace(CommandsXML.COMMANDSXML_PREFIX, CommandsXML.COMMANDSXML_NAMESPACE);
					XmlNode xmlNode = xmlDocument.SelectSingleNode("descendant::" + CommandsXML.COMMANDSXML_ELEMENT_CONNECTION, xmlNamespaceManager);
					if (xmlNode != null)
					{
						SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder(_connectionString);
						sqlConnectionStringBuilder.DataSource = xmlNode.Attributes["server"].Value;
						sqlConnectionStringBuilder.InitialCatalog = xmlNode.Attributes["database"].Value;
						_connectionString = sqlConnectionStringBuilder.ConnectionString;
					}
				}
				catch (Exception exception)
				{
					_messages.SetException(exception);
				}
			}
			SqlConnection sqlConnection = new SqlConnection();
			sqlConnection.ConnectionString = _connectionString;
			sqlConnection.Open();
			_prefExpensesCommands.CommandsXml = CommandsXml;
			_prefExpensesCommands.Messages = _messages;
			_prefExpensesCommands.Connection = sqlConnection;
			flag = _prefExpensesCommands.Execute();
			ResultsXml = _prefExpensesCommands.ResultsXml;
			sqlConnection.Close();
		}
		catch (Exception exception2)
		{
			_messages.SetException(exception2);
			flag = false;
		}
		XmlDocument xmlDocument2 = new XmlDocument();
		_messages.GetXml(xmlDocument2);
		MessagesXml = xmlDocument2.OuterXml;
		return flag;
	}
}
