using System.Xml;

namespace Preference.Wpf.Controls.Projects.AppLogic;

public static class PrefCurrencyList
{
	public static PrefCollection<PrefCurrency> Currencies;

	static PrefCurrencyList()
	{
		Initialize();
	}

	public static void Initialize()
	{
		Currencies = new PrefCollection<PrefCurrency>();
		ServiceAgent serviceAgent = new ServiceAgent(Globals.ConnectionString);
		string strResults = "";
		string strErrors = "";
		string strCommandXml = Globals.CommandsHeaderXML + "\r\n\t\t<cmd:Command name=\"GetCurrencies\"/> \r\n</cmd:Commands>";
		if (!serviceAgent.ExecuteCommand(strCommandXml, ref strResults, ref strErrors))
		{
			return;
		}
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(strResults);
		XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
		xmlNamespaceManager.AddNamespace("cmd", Globals.PrefCADCommandNamespaceUri);
		xmlNamespaceManager.AddNamespace("pmsg", Globals.MessageNamespaceUri);
		XmlNodeList xmlNodeList = xmlDocument.SelectNodes("descendant::cmd:CommandResult[@name=\"GetCurrencies\"]/descendant::cmd:Item[@name=\"" + Globals.ItemNameCurrency + "\"]", xmlNamespaceManager);
		if (xmlNodeList == null)
		{
			return;
		}
		foreach (XmlNode item in xmlNodeList)
		{
			PrefCurrency prefCurrency = new PrefCurrency();
			prefCurrency.Name = item.ChildNodes[0].Attributes["value"].Value.ToString().Trim();
			prefCurrency.Symbol = item.ChildNodes[1].Attributes["value"].Value.ToString().Trim();
			Currencies.Add(prefCurrency);
		}
	}
}
