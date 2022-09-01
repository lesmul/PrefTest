using System;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Globalization;
using System.Xml;
using Preference.Commands;
using Preference.Messages;

namespace Preference.Wpf.Controls.Expenses.Models;

public class ExpensesDoc
{
	private PrefMessages _messages;

	private ExpensesDocHeader _header;

	private ExpensesDocItemCollection _items;

	private SqlConnection _connection;

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

	public ExpensesDocHeader Header
	{
		get
		{
			return _header;
		}
		set
		{
			_header = value;
		}
	}

	public ExpensesDocItemCollection Items => _items;

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

	protected ExpensesDoc()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Expected O, but got Unknown
		_messages = new PrefMessages();
		_header = new ExpensesDocHeader();
		_items = new ExpensesDocItemCollection();
		_items.CollectionRefresh += _items_CollectionItemsChanged;
		_items.CollectionChanged += _items_CollectionItemsChanged;
	}

	private void _items_CollectionItemsChanged(object sender, NotifyCollectionChangedEventArgs e)
	{
		_header.TaxAmount = 0.0;
		_header.TotalAmount = 0.0;
		foreach (ExpensesDocItem item in _items)
		{
			_header.TaxAmount += (item.TaxFactor - 1.0) * item.UnitPrice * item.Quantity;
			_header.TotalAmount += item.Amount;
		}
	}

	public bool LoadFromXml(string documentXml)
	{
		return FillExpensesDoc(documentXml);
	}

	public virtual bool Load(long number)
	{
		return false;
	}

	public virtual bool Save()
	{
		return false;
	}

	public virtual bool BuildNewDocument()
	{
		return false;
	}

	public virtual int AddNewExpensesDoc(string name)
	{
		return 0;
	}

	protected XmlDocument GetXmlFromExpensesDoc()
	{
		try
		{
			XmlDocument xmlDocument = new XmlDocument();
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
			xmlNamespaceManager.AddNamespace("ped", Defines.ExpensesXmlNamespace);
			XmlElement xmlElement = xmlDocument.CreateElement("ped:ExpensesDocument", Defines.ExpensesXmlNamespace);
			xmlDocument.AppendChild(xmlElement);
			XmlElement xmlElement2 = xmlDocument.CreateElement("ped:Header", Defines.ExpensesXmlNamespace);
			xmlElement.AppendChild(xmlElement2);
			XmlElement xmlElement3 = xmlDocument.CreateElement("ped:Detail", Defines.ExpensesXmlNamespace);
			xmlElement.AppendChild(xmlElement3);
			XmlElement xmlElement4 = xmlDocument.CreateElement("ped:Number", Defines.ExpensesXmlNamespace);
			xmlElement4.SetAttribute("Value", Header.Number.ToString());
			xmlElement2.AppendChild(xmlElement4);
			xmlElement4 = xmlDocument.CreateElement("ped:DocumentId", Defines.ExpensesXmlNamespace);
			xmlElement4.SetAttribute("Value", Header.DocumentId.ToString("D").ToUpper());
			xmlElement2.AppendChild(xmlElement4);
			xmlElement4 = xmlDocument.CreateElement("ped:DocumentDate", Defines.ExpensesXmlNamespace);
			xmlElement4.SetAttribute("Value", Header.DocumentDate.ToString("s", DateTimeFormatInfo.InvariantInfo));
			xmlElement2.AppendChild(xmlElement4);
			xmlElement4 = xmlDocument.CreateElement("ped:TaxAmount", Defines.ExpensesXmlNamespace);
			xmlElement4.SetAttribute("Value", Header.TaxAmount.ToString(NumberFormatInfo.InvariantInfo));
			xmlElement2.AppendChild(xmlElement4);
			xmlElement4 = xmlDocument.CreateElement("ped:TotalAmount", Defines.ExpensesXmlNamespace);
			xmlElement4.SetAttribute("Value", Header.TotalAmount.ToString(NumberFormatInfo.InvariantInfo));
			xmlElement2.AppendChild(xmlElement4);
			xmlElement4 = xmlDocument.CreateElement("ped:Title", Defines.ExpensesXmlNamespace);
			xmlElement4.SetAttribute("Value", Header.Title);
			xmlElement2.AppendChild(xmlElement4);
			xmlElement4 = xmlDocument.CreateElement("ped:Currency", Defines.ExpensesXmlNamespace);
			xmlElement4.SetAttribute("Value", Header.Currency);
			xmlElement2.AppendChild(xmlElement4);
			xmlElement4 = xmlDocument.CreateElement("ped:CostDriverCode", Defines.ExpensesXmlNamespace);
			xmlElement4.SetAttribute("Value", Header.CostDriverCode.ToString(NumberFormatInfo.InvariantInfo));
			xmlElement2.AppendChild(xmlElement4);
			foreach (ExpensesDocItem item in Items)
			{
				XmlElement xmlElement5 = xmlDocument.CreateElement("ped:Item", Defines.ExpensesXmlNamespace);
				xmlElement3.AppendChild(xmlElement5);
				XmlElement xmlElement6 = xmlDocument.CreateElement("ped:LineCode", Defines.ExpensesXmlNamespace);
				xmlElement6.SetAttribute("Value", item.LineCode.ToString());
				xmlElement5.AppendChild(xmlElement6);
				xmlElement6 = xmlDocument.CreateElement("ped:SortOrder", Defines.ExpensesXmlNamespace);
				xmlElement6.SetAttribute("Value", item.SortOrder.ToString());
				xmlElement5.AppendChild(xmlElement6);
				xmlElement6 = xmlDocument.CreateElement("ped:DetailId", Defines.ExpensesXmlNamespace);
				xmlElement6.SetAttribute("Value", item.DetailId.ToString("D").ToUpper());
				xmlElement5.AppendChild(xmlElement6);
				xmlElement6 = xmlDocument.CreateElement("ped:Concept", Defines.ExpensesXmlNamespace);
				xmlElement6.SetAttribute("Value", item.Concept);
				xmlElement5.AppendChild(xmlElement6);
				xmlElement6 = xmlDocument.CreateElement("ped:Description", Defines.ExpensesXmlNamespace);
				xmlElement6.SetAttribute("Value", item.Description);
				xmlElement5.AppendChild(xmlElement6);
				xmlElement6 = xmlDocument.CreateElement("ped:Type", Defines.ExpensesXmlNamespace);
				xmlElement6.SetAttribute("Value", item.Type);
				xmlElement5.AppendChild(xmlElement6);
				xmlElement6 = xmlDocument.CreateElement("ped:UnitPrice", Defines.ExpensesXmlNamespace);
				xmlElement6.SetAttribute("Value", item.UnitPrice.ToString(NumberFormatInfo.InvariantInfo));
				xmlElement5.AppendChild(xmlElement6);
				xmlElement6 = xmlDocument.CreateElement("ped:Tax", Defines.ExpensesXmlNamespace);
				xmlElement6.SetAttribute("Value", item.TaxFactor.ToString(NumberFormatInfo.InvariantInfo));
				xmlElement5.AppendChild(xmlElement6);
				xmlElement6 = xmlDocument.CreateElement("ped:Quantity", Defines.ExpensesXmlNamespace);
				xmlElement6.SetAttribute("Value", item.Quantity.ToString(NumberFormatInfo.InvariantInfo));
				xmlElement5.AppendChild(xmlElement6);
				xmlElement6 = xmlDocument.CreateElement("ped:Amount", Defines.ExpensesXmlNamespace);
				xmlElement6.SetAttribute("Value", item.Amount.ToString(NumberFormatInfo.InvariantInfo));
				xmlElement5.AppendChild(xmlElement6);
			}
			return xmlDocument;
		}
		catch (Exception exception)
		{
			_messages.SetException(exception);
			return null;
		}
	}

	private bool FillExpensesDoc(string documentXml)
	{
		if (string.IsNullOrEmpty(documentXml))
		{
			return false;
		}
		try
		{
			XmlDocument xmlDocument = new XmlDocument();
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
			xmlNamespaceManager.AddNamespace("cmd", CommandsXML.COMMANDSXML_NAMESPACE);
			xmlNamespaceManager.AddNamespace("pmsg", "http://www.preference.com/XMLSchemas/2007/PrefSuite.Messages");
			xmlNamespaceManager.AddNamespace("ped", Defines.ExpensesXmlNamespace);
			xmlDocument.LoadXml(documentXml);
			XmlNode xmlNode = xmlDocument.SelectSingleNode("descendant::ped:Header", xmlNamespaceManager);
			if (xmlNode != null)
			{
				foreach (XmlNode childNode in xmlNode.ChildNodes)
				{
					switch (childNode.LocalName)
					{
					case "CostDriverCode":
						_header.CostDriverCode = Convert.ToInt64(childNode.Attributes["Value"].Value.ToString().TrimEnd());
						break;
					case "Currency":
						_header.Currency = childNode.Attributes["Value"].Value.ToString().TrimEnd();
						break;
					case "DocumentDate":
						_header.DocumentDate = DateTime.Parse(childNode.Attributes["Value"].Value, DateTimeFormatInfo.InvariantInfo);
						break;
					case "DocumentId":
						_header.DocumentId = new Guid(childNode.Attributes["Value"].Value);
						break;
					case "Number":
						_header.Number = Convert.ToInt64(childNode.Attributes["Value"].Value);
						break;
					case "TaxAmount":
						_header.TaxAmount = Convert.ToDouble(childNode.Attributes["Value"].Value, NumberFormatInfo.InvariantInfo);
						break;
					case "Title":
						_header.Title = childNode.Attributes["Value"].Value.ToString().TrimEnd();
						break;
					case "TotalAmount":
						_header.TotalAmount = Convert.ToDouble(childNode.Attributes["Value"].Value, NumberFormatInfo.InvariantInfo);
						break;
					}
				}
			}
			XmlNodeList xmlNodeList = xmlDocument.SelectNodes("descendant::ped:Item", xmlNamespaceManager);
			foreach (XmlNode item in xmlNodeList)
			{
				ExpensesDocItem expensesDocItem = new ExpensesDocItem();
				foreach (XmlNode childNode2 in item.ChildNodes)
				{
					switch (childNode2.LocalName)
					{
					case "Concept":
						expensesDocItem.Concept = childNode2.Attributes["Value"].Value;
						break;
					case "Description":
						expensesDocItem.Description = childNode2.Attributes["Value"].Value;
						break;
					case "DetailId":
						expensesDocItem.DetailId = new Guid(childNode2.Attributes["Value"].Value);
						break;
					case "LineCode":
						expensesDocItem.LineCode = Convert.ToInt64(childNode2.Attributes["Value"].Value, NumberFormatInfo.InvariantInfo);
						break;
					case "Quantity":
						expensesDocItem.Quantity = Convert.ToDouble(childNode2.Attributes["Value"].Value, NumberFormatInfo.InvariantInfo);
						break;
					case "SortOrder":
						expensesDocItem.SortOrder = Convert.ToInt64(childNode2.Attributes["Value"].Value, NumberFormatInfo.InvariantInfo);
						break;
					case "Tax":
						expensesDocItem.TaxFactor = Convert.ToDouble(childNode2.Attributes["Value"].Value, NumberFormatInfo.InvariantInfo);
						break;
					case "Type":
						expensesDocItem.Type = childNode2.Attributes["Value"].Value;
						break;
					case "UnitPrice":
						expensesDocItem.UnitPrice = Convert.ToDouble(childNode2.Attributes["Value"].Value, NumberFormatInfo.InvariantInfo);
						break;
					}
				}
				_items.Add(expensesDocItem);
			}
			return true;
		}
		catch (Exception exception)
		{
			_messages.SetException(exception);
			return false;
		}
	}

	protected void Clear()
	{
		_header.Clear();
		_items.Clear();
	}

	protected bool FormatNewDocumentFromXml(string documentXml)
	{
		if (string.IsNullOrEmpty(documentXml))
		{
			return false;
		}
		Clear();
		_header.DocumentDate = DateTime.Now;
		_header.DocumentId = Guid.NewGuid();
		_header.Number = 0L;
		try
		{
			XmlDocument xmlDocument = new XmlDocument();
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
			xmlNamespaceManager.AddNamespace("cmd", CommandsXML.COMMANDSXML_NAMESPACE);
			xmlNamespaceManager.AddNamespace("pmsg", "http://www.preference.com/XMLSchemas/2007/PrefSuite.Messages");
			xmlNamespaceManager.AddNamespace("ped", Defines.ExpensesXmlNamespace);
			xmlDocument.LoadXml(documentXml);
			XmlNode xmlNode = xmlDocument.SelectSingleNode("descendant::ped:NextNumber", xmlNamespaceManager);
			if (xmlNode != null)
			{
				_header.Number = Convert.ToInt64(xmlNode.Attributes["Value"].Value);
			}
			return true;
		}
		catch (Exception exception)
		{
			_messages.SetException(exception);
			return false;
		}
	}
}
