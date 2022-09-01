using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Xml;
using Microsoft.Win32;
using Preference.Diagnostics;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;

namespace Preference.WPF.MaterialsSelector.Core;

public static class RadGridViewSerializationHelper
{
	public static void SerializeColumnWidth(string key, string uniqueName, GridViewLength gridViewLength)
	{
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			XmlDocument xmlDocument = new XmlDocument();
			string empty = string.Empty;
			object value = RegistryHelper.GetValue("SOFTWARE\\Preference\\PrefGest\\PrefFilterBOM", key);
			if (value != null)
			{
				empty = value.ToString();
				if (!string.IsNullOrEmpty(empty))
				{
					xmlDocument.LoadXml(empty);
				}
			}
			string xpath = "/RadGridView";
			XmlNode xmlNode = xmlDocument.SelectSingleNode(xpath);
			if (xmlNode == null)
			{
				xmlNode = xmlDocument.CreateElement("RadGridView");
				xmlDocument.AppendChild(xmlNode);
			}
			xpath = "child::ColumnWidths";
			XmlNode xmlNode2 = xmlNode.SelectSingleNode(xpath);
			if (xmlNode2 == null)
			{
				xmlNode2 = xmlDocument.CreateElement("ColumnWidths");
				xmlNode.AppendChild(xmlNode2);
			}
			xpath = $"child::ColumnWidth[@uniqueName = '{uniqueName}']";
			XmlNode xmlNode3 = xmlNode2.SelectSingleNode(xpath);
			if (xmlNode3 != null)
			{
				xmlNode3.Attributes.GetNamedItem("value").Value = ((GridViewLength)(ref gridViewLength)).get_Value().ToString(CultureInfo.InvariantCulture);
				xmlNode3.Attributes.GetNamedItem("type").Value = Convert.ToInt32(((GridViewLength)(ref gridViewLength)).get_UnitType()).ToString();
				xmlNode3.Attributes.GetNamedItem("desiredValue").Value = ((GridViewLength)(ref gridViewLength)).get_DesiredValue().ToString(CultureInfo.InvariantCulture);
				xmlNode3.Attributes.GetNamedItem("displayValue").Value = ((GridViewLength)(ref gridViewLength)).get_DisplayValue().ToString(CultureInfo.InvariantCulture);
			}
			else
			{
				XmlNode xmlNode4 = xmlDocument.CreateElement("ColumnWidth");
				XmlAttribute xmlAttribute = xmlDocument.CreateAttribute("uniqueName");
				xmlAttribute.Value = uniqueName;
				xmlNode4.Attributes.SetNamedItem(xmlAttribute);
				xmlAttribute = xmlDocument.CreateAttribute("value");
				xmlAttribute.Value = ((GridViewLength)(ref gridViewLength)).get_Value().ToString(CultureInfo.InvariantCulture);
				xmlNode4.Attributes.SetNamedItem(xmlAttribute);
				xmlAttribute = xmlDocument.CreateAttribute("type");
				xmlAttribute.Value = Convert.ToInt32(((GridViewLength)(ref gridViewLength)).get_UnitType()).ToString();
				xmlNode4.Attributes.SetNamedItem(xmlAttribute);
				xmlAttribute = xmlDocument.CreateAttribute("desiredValue");
				xmlAttribute.Value = ((GridViewLength)(ref gridViewLength)).get_DesiredValue().ToString(CultureInfo.InvariantCulture);
				xmlNode4.Attributes.SetNamedItem(xmlAttribute);
				xmlAttribute = xmlDocument.CreateAttribute("displayValue");
				xmlAttribute.Value = ((GridViewLength)(ref gridViewLength)).get_DisplayValue().ToString(CultureInfo.InvariantCulture);
				xmlNode4.Attributes.SetNamedItem(xmlAttribute);
				xmlNode2.AppendChild(xmlNode4);
			}
			empty = xmlDocument.OuterXml;
			RegistryHelper.SetValue("SOFTWARE\\Preference\\PrefGest\\PrefFilterBOM", key, empty, RegistryValueKind.String);
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefGest");
		}
	}

	public static void ApplySerializedColumnWidths(string key, RadGridView grid)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			foreach (GridViewColumn item in (Collection<GridViewColumn>)(object)((GridViewDataControl)grid).get_Columns())
			{
				GridViewLength gridViewLength = GridViewLength.get_Auto();
				if (TryGetColumnWidth(key, item.get_UniqueName(), out gridViewLength))
				{
					((GridViewDataControl)grid).get_Columns().get_Item(item.get_UniqueName()).set_Width(gridViewLength);
				}
			}
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefGest");
		}
	}

	public static void ApplySerializedColumnWidths(string key, RadTreeListView treelist)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			foreach (GridViewColumn item in (Collection<GridViewColumn>)(object)((GridViewDataControl)treelist).get_Columns())
			{
				GridViewLength gridViewLength = GridViewLength.get_Auto();
				if (TryGetColumnWidth(key, item.get_UniqueName(), out gridViewLength))
				{
					((GridViewDataControl)treelist).get_Columns().get_Item(item.get_UniqueName()).set_Width(gridViewLength);
				}
			}
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefGest");
		}
	}

	private static bool TryGetColumnWidth(string key, string uniqueName, out GridViewLength gridViewLength)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		gridViewLength = GridViewLength.get_Auto();
		try
		{
			object value = RegistryHelper.GetValue("SOFTWARE\\Preference\\PrefGest\\PrefFilterBOM", key);
			if (value != null)
			{
				string text = value.ToString();
				if (!string.IsNullOrEmpty(text))
				{
					XmlDocument xmlDocument = new XmlDocument();
					xmlDocument.LoadXml(text);
					string xpath = $"/RadGridView/ColumnWidths/ColumnWidth[@uniqueName = '{uniqueName}']";
					XmlNode xmlNode = xmlDocument.SelectSingleNode(xpath);
					if (xmlNode != null)
					{
						double num = Convert.ToDouble(xmlNode.Attributes.GetNamedItem("value").Value, CultureInfo.InvariantCulture);
						GridViewLengthUnitType val = (GridViewLengthUnitType)Convert.ToInt32(xmlNode.Attributes.GetNamedItem("type").Value);
						double num2 = Convert.ToDouble(xmlNode.Attributes.GetNamedItem("desiredValue").Value, CultureInfo.InvariantCulture);
						double num3 = Convert.ToDouble(xmlNode.Attributes.GetNamedItem("displayValue").Value, CultureInfo.InvariantCulture);
						gridViewLength = new GridViewLength(num, val, num2, num3);
						return true;
					}
				}
			}
			return false;
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefGest");
			return false;
		}
	}
}
