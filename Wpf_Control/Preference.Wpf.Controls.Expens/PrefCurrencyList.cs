using System;
using System.Collections.ObjectModel;
using System.Xml;
using Preference.Commands;

namespace Preference.Wpf.Controls.Expenses.Models;

public class PrefCurrencyList : ObservableCollection<PrefCurrency>
{
	public PrefCurrencyList()
	{
		Initialize();
	}

	public void Initialize()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Expected O, but got Unknown
		Clear();
		CommandXMLWriter val = new CommandXMLWriter();
		val.CreateDocument();
		if (ExpensesDataProvider.Connection != null)
		{
			val.CreateConnection(ExpensesDataProvider.Connection.ConnectionString);
		}
		XmlElement xmlElement = val.CreateCommand("GetCurrencyList");
		string ResultsXml = "";
		string MessagesXml = "";
		if (ExpensesDataProvider.IsWebService)
		{
			ServiceAgent serviceAgent = new ServiceAgent();
			serviceAgent.Execute(val.GetXml(), ref ResultsXml, ref MessagesXml);
		}
		else
		{
			ExpensesCommands expensesCommands = new ExpensesCommands();
			expensesCommands.CommandsXml = val.GetXml();
			expensesCommands.Connection = ExpensesDataProvider.Connection;
			expensesCommands.Execute();
			ResultsXml = expensesCommands.ResultsXml;
			MessagesXml = ((object)expensesCommands.Messages).ToString();
		}
		if (string.IsNullOrEmpty(ResultsXml))
		{
			return;
		}
		try
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(ResultsXml);
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
			xmlNamespaceManager.AddNamespace(CommandsXML.COMMANDSXML_PREFIX, CommandsXML.COMMANDSXML_NAMESPACE);
			XmlNodeList xmlNodeList = xmlDocument.SelectNodes("descendant::cmd:CommandResult[@name=\"GetCurrencyList\"]/descendant::cmd:Item", xmlNamespaceManager);
			if (xmlNodeList == null)
			{
				return;
			}
			foreach (XmlNode item in xmlNodeList)
			{
				PrefCurrency prefCurrency = new PrefCurrency();
				prefCurrency.Name = item.ChildNodes[0].Attributes["value"].Value.ToString().Trim();
				prefCurrency.Symbol = item.ChildNodes[1].Attributes["value"].Value.ToString().Trim();
				Add(prefCurrency);
			}
		}
		catch (Exception)
		{
		}
	}
}
