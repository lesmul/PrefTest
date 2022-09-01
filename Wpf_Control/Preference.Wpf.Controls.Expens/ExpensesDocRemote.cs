using System.Xml;
using Preference.Commands;

namespace Preference.Wpf.Controls.Expenses.Models;

public class ExpensesDocRemote : ExpensesDoc
{
	private string _serviceEndpoint = "";

	private string _serviceEndpointRemoteAdress = "";

	public string ServiceEndpoint
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

	public string ServiceEndpointRemoteAdress
	{
		get
		{
			return _serviceEndpointRemoteAdress;
		}
		set
		{
			_serviceEndpointRemoteAdress = value;
		}
	}

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
		ServiceAgent serviceAgent = new ServiceAgent();
		string ResultsXml = "";
		string MessagesXml = "";
		if (!serviceAgent.Execute(val.GetXml(), ref ResultsXml, ref MessagesXml))
		{
			return false;
		}
		string documentXml = ResultsXml;
		ExpensesDataProvider.ServiceEndpoint = _serviceEndpoint;
		ExpensesDataProvider.IsWebService = true;
		return LoadFromXml(documentXml);
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
		ServiceAgent serviceAgent = new ServiceAgent();
		string ResultsXml = "";
		string MessagesXml = "";
		if (!serviceAgent.Execute(val.GetXml(), ref ResultsXml, ref MessagesXml))
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
		ServiceAgent serviceAgent = new ServiceAgent();
		string ResultsXml = "";
		string MessagesXml = "";
		if (!serviceAgent.Execute(val.GetXml(), ref ResultsXml, ref MessagesXml))
		{
			return false;
		}
		return FormatNewDocumentFromXml(ResultsXml);
	}
}
