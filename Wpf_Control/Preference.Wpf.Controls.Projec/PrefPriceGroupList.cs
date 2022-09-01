using System;
using System.Xml;
using Preference.Wpf.Controls.Properties;

namespace Preference.Wpf.Controls.Projects.AppLogic;

public static class PrefPriceGroupList
{
	public static PrefCollection<PrefGroup> PriceGroups;

	static PrefPriceGroupList()
	{
		Initialize();
	}

	public static void Initialize()
	{
		PriceGroups = new PrefCollection<PrefGroup>();
		PrefGroup prefGroup = null;
		try
		{
			ServiceAgent serviceAgent = new ServiceAgent(Globals.ConnectionString);
			string strResults = "";
			string strErrors = "";
			string strCommandXml = Globals.CommandsHeaderXML + "\r\n\t\t<cmd:Command name=\"GetAllGroupsOfType\"> \r\n\t\t\t<cmd:Parameter name=\"GroupType\" value=\"2\"/> \r\n\t\t</cmd:Command> \r\n</cmd:Commands>";
			if (!serviceAgent.ExecuteCommand(strCommandXml, ref strResults, ref strErrors))
			{
				return;
			}
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(strResults);
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
			xmlNamespaceManager.AddNamespace("cmd", Globals.PrefCADCommandNamespaceUri);
			xmlNamespaceManager.AddNamespace("pmsg", Globals.MessageNamespaceUri);
			XmlNodeList xmlNodeList = xmlDocument.SelectNodes("descendant::cmd:CommandResult[@name=\"GetAllGroupsOfType\"]/descendant::cmd:Item[@name=\"" + Globals.ItemNameGroup + "\"]", xmlNamespaceManager);
			if (xmlNodeList == null)
			{
				return;
			}
			foreach (XmlNode item in xmlNodeList)
			{
				prefGroup = new PrefGroup();
				prefGroup.RowId = new Guid(item.ChildNodes[0].Attributes["value"].Value.ToString().Trim());
				prefGroup.Code = Convert.ToInt32(item.ChildNodes[1].Attributes["value"].Value.ToString().Trim());
				prefGroup.Name = item.ChildNodes[2].Attributes["value"].Value.ToString().Trim();
				prefGroup.Type = enGroupType.Offering;
				prefGroup.Supplier = Resources.IDS_PRICEGROUP;
				PriceGroups.Add(prefGroup);
			}
		}
		catch (Exception)
		{
		}
	}
}
