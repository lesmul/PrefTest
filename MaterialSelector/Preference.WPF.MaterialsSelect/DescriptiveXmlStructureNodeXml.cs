using System;
using System.Xml;

namespace Preference.WPF.MaterialsSelector.Core;

internal class DescriptiveXmlStructureNodeXmlBuilder
{
	private XmlDocument _xmldoc;

	private XmlNode _node;

	public DescriptiveXmlStructureNodeXmlBuilder(XmlDocument xmldoc, XmlNode parentNode, int allMaterialsCount, string generationMethod, string id, int materialsCount, string name, string squareId, string type)
	{
		_xmldoc = xmldoc;
		_node = _xmldoc.CreateElement("DescriptiveXmlStructureNode");
		parentNode.AppendChild(_node);
		XmlAttribute xmlAttribute = _xmldoc.CreateAttribute("allMaterialsCount");
		xmlAttribute.Value = Convert.ToString(allMaterialsCount);
		_node.Attributes.SetNamedItem(xmlAttribute);
		xmlAttribute = _xmldoc.CreateAttribute("generationMethod");
		xmlAttribute.Value = generationMethod;
		_node.Attributes.SetNamedItem(xmlAttribute);
		xmlAttribute = _xmldoc.CreateAttribute("id");
		xmlAttribute.Value = id;
		_node.Attributes.SetNamedItem(xmlAttribute);
		xmlAttribute = _xmldoc.CreateAttribute("materialsCount");
		xmlAttribute.Value = Convert.ToString(materialsCount);
		_node.Attributes.SetNamedItem(xmlAttribute);
		xmlAttribute = _xmldoc.CreateAttribute("name");
		xmlAttribute.Value = name;
		_node.Attributes.SetNamedItem(xmlAttribute);
		xmlAttribute = _xmldoc.CreateAttribute("squareId");
		xmlAttribute.Value = name;
		_node.Attributes.SetNamedItem(xmlAttribute);
		xmlAttribute = _xmldoc.CreateAttribute("type");
		xmlAttribute.Value = Convert.ToString(type);
		_node.Attributes.SetNamedItem(xmlAttribute);
	}

	public DescriptiveXmlStructureNodeXmlBuilder AddDescriptiveXmlStructureNode(int allMaterialsCount, string generationMethod, string id, int materialsCount, string name, string squareId, string type)
	{
		return new DescriptiveXmlStructureNodeXmlBuilder(_xmldoc, _node, allMaterialsCount, generationMethod, id, materialsCount, name, squareId, type);
	}
}
