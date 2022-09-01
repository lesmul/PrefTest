using System;
using System.Collections.ObjectModel;
using System.Xml;
using Preference.Commands;

namespace Preference.Wpf.Controls.Expenses.Models;

public class ExpensesDocItemTypeList : ObservableCollection<ExpensesDocItemType>
{
	public ExpensesDocItemTypeList()
	{
		Clear();
		Populate();
	}

	public void Populate()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Expected O, but got Unknown
		CommandXMLWriter val = new CommandXMLWriter();
		val.CreateDocument();
		if (ExpensesDataProvider.Connection != null)
		{
			val.CreateConnection(ExpensesDataProvider.Connection.ConnectionString);
		}
		XmlElement xmlElement = val.CreateCommand("GetExpensesDocumentItemTypes");
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
			xmlNamespaceManager.AddNamespace("ped", Defines.ExpensesXmlNamespace);
			XmlNodeList xmlNodeList = xmlDocument.SelectNodes("descendant::ped:Type", xmlNamespaceManager);
			foreach (XmlNode item in xmlNodeList)
			{
				ExpensesDocItemType expensesDocItemType = new ExpensesDocItemType();
				expensesDocItemType.Code = Convert.ToInt64(item.SelectSingleNode("ped:Code", xmlNamespaceManager).Attributes["Value"].Value);
				expensesDocItemType.Description = item.SelectSingleNode("ped:Description", xmlNamespaceManager).Attributes["Value"].Value.ToString();
				expensesDocItemType.Name = item.SelectSingleNode("ped:Name", xmlNamespaceManager).Attributes["Value"].Value.ToString();
				if (!Contains(expensesDocItemType))
				{
					Add(expensesDocItemType);
				}
			}
		}
		catch (Exception)
		{
		}
	}
}
