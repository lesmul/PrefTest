using System;
using System.Xml;
using Preference.Commands;

namespace Preference.Wpf.Controls.Expenses.Models;

public class ExpensesDocLocal : ExpensesDoc
{
	public override bool Load(long number)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Expected O, but got Unknown
		Clear();
		CommandXMLWriter val = new CommandXMLWriter();
		val.CreateDocument();
		if (base.Connection != null)
		{
			val.CreateConnection(base.Connection.ConnectionString);
		}
		XmlElement xmlElement = val.CreateCommand("GetDocument");
		val.AddLongParameter(xmlElement, "Number", number);
		ExpensesCommands expensesCommands = new ExpensesCommands();
		expensesCommands.CommandsXml = val.GetXml();
		expensesCommands.Connection = base.Connection;
		expensesCommands.Messages = base.Messages;
		if (!expensesCommands.Execute())
		{
			return false;
		}
		ExpensesDataProvider.Connection = base.Connection;
		ExpensesDataProvider.IsWebService = false;
		return LoadFromXml(expensesCommands.ResultsXml);
	}

	public override bool Save()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Expected O, but got Unknown
		CommandXMLWriter val = new CommandXMLWriter();
		val.CreateDocument();
		if (base.Connection != null)
		{
			val.CreateConnection(base.Connection.ConnectionString);
		}
		XmlElement xmlElement = val.CreateCommand("SaveDocument");
		XmlElement documentElement = GetXmlFromExpensesDoc().DocumentElement;
		if (documentElement != null)
		{
			val.AddXmlParameter(xmlElement, "DocumentXml", documentElement);
		}
		ExpensesCommands expensesCommands = new ExpensesCommands();
		expensesCommands.CommandsXml = val.GetXml();
		expensesCommands.Connection = base.Connection;
		expensesCommands.Messages = base.Messages;
		if (!expensesCommands.Execute())
		{
			return false;
		}
		return true;
	}

	public override bool BuildNewDocument()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Expected O, but got Unknown
		CommandXMLWriter val = new CommandXMLWriter();
		val.CreateDocument();
		if (base.Connection != null)
		{
			val.CreateConnection(base.Connection.ConnectionString);
		}
		XmlElement xmlElement = val.CreateCommand("GetNextAvailableDocumentNumber");
		ExpensesCommands expensesCommands = new ExpensesCommands();
		expensesCommands.CommandsXml = val.GetXml();
		expensesCommands.Connection = base.Connection;
		expensesCommands.Messages = base.Messages;
		if (!expensesCommands.Execute())
		{
			return false;
		}
		return FormatNewDocumentFromXml(expensesCommands.ResultsXml);
	}

	public override int AddNewExpensesDoc(string name)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Expected O, but got Unknown
		CommandXMLWriter val = new CommandXMLWriter();
		val.CreateDocument();
		if (base.Connection != null)
		{
			val.CreateConnection(base.Connection.ConnectionString);
		}
		XmlElement xmlElement = val.CreateCommand("InsertNewDocument");
		val.AddStringParameter(xmlElement, "Title", name);
		ExpensesCommands expensesCommands = new ExpensesCommands();
		expensesCommands.CommandsXml = val.GetXml();
		expensesCommands.Connection = base.Connection;
		expensesCommands.Messages = base.Messages;
		if (!expensesCommands.Execute())
		{
			return 0;
		}
		NameTable nameTable = new NameTable();
		XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(nameTable);
		xmlNamespaceManager.AddNamespace("cmd", "http://www.preference.com/XMLSchemas/2006/PrefCAD.Command");
		XmlDocument xmlDocument = new XmlDocument(nameTable);
		xmlDocument.LoadXml(expensesCommands.ResultsXml);
		string xpath = "/cmd:Commands/cmd:CommandResult/cmd:Parameter[@name = 'Number']";
		XmlNode xmlNode = xmlDocument.SelectSingleNode(xpath, xmlNamespaceManager);
		return Convert.ToInt32(xmlNode.Attributes.GetNamedItem("value").Value);
	}
}
