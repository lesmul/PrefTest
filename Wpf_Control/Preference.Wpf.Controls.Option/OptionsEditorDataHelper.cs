using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml;

namespace Preference.Wpf.Controls.Options;

public static class OptionsEditorDataHelper
{
	public static Collection<TreeItem> GetOptionFromServiceXml(XmlNode xml)
	{
		Collection<TreeItem> collection = new Collection<TreeItem>();
		XmlNodeList xmlNodeList = xml.SelectNodes("descendant::Row");
		foreach (XmlNode item in xmlNodeList)
		{
			string attributeValue = GetAttributeValue(item, "Level1");
			string attributeValue2 = GetAttributeValue(item, "Level2");
			string attributeValue3 = GetAttributeValue(item, "Level3");
			string attributeValue4 = GetAttributeValue(item, "Level4");
			string attributeValue5 = GetAttributeValue(item, "Level5");
			string attributeValue6 = GetAttributeValue(item, "Opcion");
			string attributeValue7 = GetAttributeValue(item, "TranslatedOption");
			string attributeValue8 = GetAttributeValue(item, "LevelDescription");
			string attributeValue9 = GetAttributeValue(item, "TranslatedLevelDescripcion");
			string attributeValue10 = GetAttributeValue(item, "Type");
			string attributeValue11 = GetAttributeValue(item, "Value");
			string attributeValue12 = GetAttributeValue(item, "TranslatedValue");
			string attributeValue13 = GetAttributeValue(item, "OptionDescription");
			string attributeValue14 = GetAttributeValue(item, "TranslatedOptionDescription");
			string attributeValue15 = GetAttributeValue(item, "Orden");
			InsertOption(attributeValue, attributeValue2, attributeValue3, attributeValue4, attributeValue5, attributeValue6, attributeValue7, attributeValue9, attributeValue10, attributeValue11, attributeValue12, attributeValue14, collection);
		}
		FilterNodesWithoutChildren(collection);
		return collection;
	}

	public static Collection<TreeItem> GetOptionFromServiceXml(XmlNode xmlOptions, XmlNode xmlOptionsValues)
	{
		Collection<TreeItem> collection = new Collection<TreeItem>();
		XmlNodeList xmlNodeList = xmlOptions.SelectNodes("descendant::Row");
		foreach (XmlNode item in xmlNodeList)
		{
			string attributeValue = GetAttributeValue(item, "Level1");
			string attributeValue2 = GetAttributeValue(item, "Level2");
			string attributeValue3 = GetAttributeValue(item, "Level3");
			string attributeValue4 = GetAttributeValue(item, "Level4");
			string attributeValue5 = GetAttributeValue(item, "Level5");
			string attributeValue6 = GetAttributeValue(item, "Opcion");
			string attributeValue7 = GetAttributeValue(item, "TranslatedOption");
			string attributeValue8 = GetAttributeValue(item, "TranslatedOptionDescription");
			string attributeValue9 = GetAttributeValue(item, "Type");
			XmlNodeList xmlNodeList2 = xmlOptionsValues.SelectNodes($"descendant::Row[Opcion = '{attributeValue6}']");
			if (xmlNodeList2 != null && xmlNodeList2.Count > 0)
			{
				foreach (XmlNode item2 in xmlNodeList2)
				{
					string attributeValue10 = GetAttributeValue(item2, "Value");
					string attributeValue11 = GetAttributeValue(item2, "TranslatedValue");
					string attributeValue12 = GetAttributeValue(item2, "OptionDescription");
					string attributeValue13 = GetAttributeValue(item2, "TranslatedOptionDescription");
					string attributeValue14 = GetAttributeValue(item2, "Orden");
					InsertOption(attributeValue, attributeValue2, attributeValue3, attributeValue4, attributeValue5, attributeValue6, attributeValue7, attributeValue8, attributeValue9, attributeValue10, attributeValue11, attributeValue13, collection);
				}
			}
			else
			{
				InsertOption(attributeValue, attributeValue2, attributeValue3, attributeValue4, attributeValue5, attributeValue6, attributeValue7, attributeValue8, attributeValue9, string.Empty, string.Empty, string.Empty, collection);
			}
		}
		FilterNodesWithoutChildren(collection);
		return collection;
	}

	public static Collection<TreeItem> GetGlassFromServiceXml(XmlNode xml)
	{
		Collection<TreeItem> collection = new Collection<TreeItem>();
		XmlNodeList xmlNodeList = xml.SelectNodes("descendant::Row");
		foreach (XmlNode item in xmlNodeList)
		{
			string attributeValue = GetAttributeValue(item, "Level1");
			string attributeValue2 = GetAttributeValue(item, "Level2");
			string attributeValue3 = GetAttributeValue(item, "Level3");
			string attributeValue4 = GetAttributeValue(item, "Level4");
			string attributeValue5 = GetAttributeValue(item, "Level5");
			string attributeValue6 = GetAttributeValue(item, "Reference");
			string attributeValue7 = GetAttributeValue(item, "Description");
			string attributeValue8 = GetAttributeValue(item, "Type");
			string attributeValue9 = GetAttributeValue(item, "Composite");
			GlassTreeItemType type = GlassTreeItemType.Glass;
			if (attributeValue8 == "2")
			{
				type = GlassTreeItemType.Panel;
			}
			else if (attributeValue8 == "0" && attributeValue9 == "1")
			{
				type = GlassTreeItemType.CompositeGlass;
			}
			InsertGlass(attributeValue, attributeValue2, attributeValue3, attributeValue4, attributeValue5, attributeValue6, attributeValue7, type, collection);
		}
		return collection;
	}

	private static void InsertOption(string strLevel1, string strLevel2, string strLevel3, string strLevel4, string strLevel5, string strOption, string strTranslatedOption, string strDescription, string strType, string strValue, string strTranslatedValue, string strOptionDescription, Collection<TreeItem> collectionOptions)
	{
		TreeItem treeItem = null;
		OptionTreeItemType optionTreeItemType = OptionTreeItemType.Folder;
		switch (strType)
		{
		case "0":
			optionTreeItemType = OptionTreeItemType.Selection;
			break;
		case "1":
			optionTreeItemType = OptionTreeItemType.Decision;
			break;
		case "2":
			optionTreeItemType = OptionTreeItemType.Material;
			break;
		case "3":
			optionTreeItemType = OptionTreeItemType.Color;
			break;
		case "6":
			optionTreeItemType = OptionTreeItemType.Numeric;
			break;
		case "7":
			optionTreeItemType = OptionTreeItemType.AlphaNumeric;
			break;
		}
		if (!string.IsNullOrEmpty(strLevel1))
		{
			TreeItem treeItem2 = FindNodeInTree(collectionOptions, strLevel1);
			if (treeItem2 == null)
			{
				treeItem2 = new OptionTreeItem(strLevel1, strLevel1, string.Empty, null, OptionTreeItemType.Folder);
				collectionOptions.Add(treeItem2);
			}
			treeItem = treeItem2;
			if (!string.IsNullOrEmpty(strLevel2))
			{
				TreeItem treeItem3 = FindNodeInTree(treeItem.Children, strLevel2);
				if (treeItem3 == null)
				{
					treeItem3 = new OptionTreeItem(strLevel2, strLevel2, string.Empty, treeItem, OptionTreeItemType.Folder);
					treeItem.Children.Add(treeItem3);
				}
				treeItem = treeItem3;
				if (!string.IsNullOrEmpty(strLevel3))
				{
					TreeItem treeItem4 = FindNodeInTree(treeItem.Children, strLevel3);
					if (treeItem4 == null)
					{
						treeItem4 = new OptionTreeItem(strLevel3, strLevel3, string.Empty, treeItem, OptionTreeItemType.Folder);
						treeItem.Children.Add(treeItem4);
					}
					treeItem = treeItem4;
					if (!string.IsNullOrEmpty(strLevel4))
					{
						TreeItem treeItem5 = FindNodeInTree(treeItem.Children, strLevel4);
						if (treeItem5 == null)
						{
							treeItem5 = new OptionTreeItem(strLevel4, strLevel4, string.Empty, treeItem, OptionTreeItemType.Folder);
							treeItem.Children.Add(treeItem5);
						}
						treeItem = treeItem5;
						if (!string.IsNullOrEmpty(strLevel5))
						{
							TreeItem treeItem6 = FindNodeInTree(treeItem.Children, strLevel5);
							if (treeItem6 == null)
							{
								treeItem6 = new OptionTreeItem(strLevel5, strLevel5, string.Empty, treeItem, OptionTreeItemType.Folder);
								treeItem.Children.Add(treeItem6);
							}
							treeItem = treeItem6;
						}
					}
				}
			}
		}
		if (string.IsNullOrEmpty(strOption))
		{
			return;
		}
		TreeItem treeItem7 = FindNodeInTree(treeItem.Children, strOption);
		if (treeItem7 == null)
		{
			treeItem7 = new OptionTreeItem(strTranslatedOption, strOption, strDescription, treeItem, OptionTreeItemType.Option);
			treeItem.Children.Add(treeItem7);
		}
		treeItem = treeItem7;
		if (!string.IsNullOrEmpty(strValue))
		{
			TreeItem treeItem8 = FindNodeInTree(treeItem.Children, strValue);
			if (treeItem8 == null)
			{
				treeItem7 = new OptionTreeItem(strTranslatedValue, strValue, strOptionDescription, treeItem, optionTreeItemType);
				treeItem.Children.Add(treeItem7);
			}
			return;
		}
		string text = "0";
		if (optionTreeItemType == OptionTreeItemType.Decision)
		{
			text = treeItem.Header;
		}
		TreeItem treeItem9 = new OptionTreeItem(text, text, string.Empty, treeItem, optionTreeItemType);
		if (optionTreeItemType == OptionTreeItemType.Numeric || optionTreeItemType == OptionTreeItemType.AlphaNumeric)
		{
			treeItem9.IsEnableEdit = true;
		}
		treeItem.Children.Add(treeItem9);
	}

	private static TreeItem FindNodeInTree(Collection<TreeItem> collection, string strHeader)
	{
		if (collection != null)
		{
			foreach (TreeItem item in collection)
			{
				if (item.Header.ToString() == strHeader)
				{
					return item;
				}
			}
		}
		return null;
	}

	private static ImageSource ConvertByteArrayToImageSource(byte[] bdIconeArray)
	{
		ImageSource imageSource = null;
		MemoryStream memoryStream = new MemoryStream();
		BitmapImage bitmapImage = new BitmapImage();
		int num = 78;
		memoryStream.Write(bdIconeArray, num, bdIconeArray.Length - num);
		bitmapImage.BeginInit();
		bitmapImage.StreamSource = memoryStream;
		bitmapImage.EndInit();
		return bitmapImage;
	}

	private static string GetAttributeValue(XmlNode node, string strName)
	{
		if (node == null)
		{
			return string.Empty;
		}
		XmlNode xmlNode = node.SelectSingleNode(strName);
		if (xmlNode == null)
		{
			return string.Empty;
		}
		return xmlNode.InnerText;
	}

	private static void InsertGlass(string strLevel1, string strLevel2, string strLevel3, string strLevel4, string strLevel5, string strReference, string strDescription, GlassTreeItemType type, Collection<TreeItem> collectionGlass)
	{
		TreeItem treeItem = null;
		if (!string.IsNullOrEmpty(strLevel1))
		{
			TreeItem treeItem2 = FindNodeInTree(collectionGlass, strLevel1);
			if (treeItem2 == null)
			{
				treeItem2 = new GlassTreeItem(strLevel1, strLevel1, string.Empty, null, GlassTreeItemType.Folder);
				collectionGlass.Add(treeItem2);
			}
			treeItem = treeItem2;
			if (!string.IsNullOrEmpty(strLevel2))
			{
				TreeItem treeItem3 = FindNodeInTree(treeItem.Children, strLevel2);
				if (treeItem3 == null)
				{
					treeItem3 = new GlassTreeItem(strLevel2, strLevel2, string.Empty, treeItem, GlassTreeItemType.Folder);
					treeItem.Children.Add(treeItem3);
				}
				treeItem = treeItem3;
				if (!string.IsNullOrEmpty(strLevel3))
				{
					TreeItem treeItem4 = FindNodeInTree(treeItem.Children, strLevel3);
					if (treeItem4 == null)
					{
						treeItem4 = new GlassTreeItem(strLevel3, strLevel3, string.Empty, treeItem, GlassTreeItemType.Folder);
						treeItem.Children.Add(treeItem4);
					}
					treeItem = treeItem4;
					if (!string.IsNullOrEmpty(strLevel4))
					{
						TreeItem treeItem5 = FindNodeInTree(treeItem.Children, strLevel4);
						if (treeItem5 == null)
						{
							treeItem5 = new GlassTreeItem(strLevel4, strLevel4, string.Empty, treeItem, GlassTreeItemType.Folder);
							treeItem.Children.Add(treeItem5);
						}
						treeItem = treeItem5;
						if (!string.IsNullOrEmpty(strLevel5))
						{
							TreeItem treeItem6 = FindNodeInTree(treeItem.Children, strLevel5);
							if (treeItem6 == null)
							{
								treeItem6 = new GlassTreeItem(strLevel5, strLevel5, string.Empty, treeItem, GlassTreeItemType.Folder);
								treeItem.Children.Add(treeItem6);
							}
							treeItem = treeItem6;
						}
					}
				}
			}
		}
		if (!string.IsNullOrEmpty(strReference))
		{
			TreeItem treeItem7 = FindNodeInTree(treeItem.Children, strReference);
			if (treeItem7 == null)
			{
				treeItem7 = new GlassTreeItem(strReference, strReference, strDescription, treeItem, type);
				treeItem.Children.Add(treeItem7);
			}
			treeItem = treeItem7;
		}
	}

	private static void FilterNodesWithoutChildren(Collection<TreeItem> collectionTreeItems)
	{
		if (collectionTreeItems == null)
		{
			return;
		}
		Collection<TreeItem> collection = new Collection<TreeItem>();
		foreach (TreeItem collectionTreeItem in collectionTreeItems)
		{
			if (!(collectionTreeItem.Type == OptionTreeItemType.Folder.ToString()) && !(collectionTreeItem.Type == OptionTreeItemType.Option.ToString()))
			{
				continue;
			}
			if (collectionTreeItem.Children.Count == 0)
			{
				collection.Add(collectionTreeItem);
				continue;
			}
			FilterNodesWithoutChildren(collectionTreeItem.Children);
			if (collectionTreeItem.Children.Count == 0)
			{
				collection.Add(collectionTreeItem);
			}
		}
		foreach (TreeItem item in collection)
		{
			collectionTreeItems.Remove(item);
		}
		collection.Clear();
	}
}
