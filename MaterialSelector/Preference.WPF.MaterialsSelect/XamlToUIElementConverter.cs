using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using System.Xml;

namespace Preference.WPF.MaterialsSelector.Core.Converters;

public class XamlToUIElementConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (value == null)
		{
			return null;
		}
		if (!(value is string))
		{
			return null;
		}
		string text = value as string;
		if (string.IsNullOrEmpty(text))
		{
			return null;
		}
		try
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(text);
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
			xmlNamespaceManager.AddNamespace("xaml", "http://schemas.microsoft.com/winfx/2006/xaml/presentation");
			xmlNamespaceManager.AddNamespace("x", "http://schemas.microsoft.com/winfx/2006/xaml");
			xmlNamespaceManager.AddNamespace("i", "xmlns=''");
			foreach (XmlNode item in xmlDocument.SelectNodes("//xaml:TextBlock", xmlNamespaceManager))
			{
				if (item.Attributes.GetNamedItem("FontSize") != null && double.Parse(item.Attributes.GetNamedItem("FontSize").Value, CultureInfo.InvariantCulture) < 1.0)
				{
					item.Attributes.GetNamedItem("FontSize").Value = "1";
				}
			}
			return XamlReader.Parse(xmlDocument.OuterXml);
		}
		catch
		{
			return null;
		}
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		return null;
	}
}
