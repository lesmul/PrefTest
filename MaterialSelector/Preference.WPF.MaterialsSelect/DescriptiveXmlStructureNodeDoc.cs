using System.Xml;

namespace Preference.WPF.MaterialsSelector.Core;

internal class DescriptiveXmlStructureNodeDocumentXmlBuilder
{
	private XmlDocument _xmldoc;

	private XmlNode _node;

	public string OuterXml => _xmldoc.OuterXml;

	public DescriptiveXmlStructureNodeDocumentXmlBuilder()
	{
		_xmldoc = new XmlDocument();
		_node = _xmldoc.CreateElement("DescriptiveXmlStructureNodeDocument");
		_xmldoc.AppendChild(_node);
	}

	public DescriptiveXmlStructureNodeXmlBuilder AddDescriptiveXmlStructureNode(int allMaterialsCount, string generationMethod, string id, int materialsCount, string name, string squareId, string type)
	{
		return new DescriptiveXmlStructureNodeXmlBuilder(_xmldoc, _node, allMaterialsCount, generationMethod, id, materialsCount, name, squareId, type);
	}
}
