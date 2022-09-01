using System;
using System.Windows;
using System.Windows.Media;

namespace Preference.Wpf.Controls.Options;

public class OptionTreeItem : TreeItem
{
	public OptionTreeItem(string strHeader, string strValue, string strDescription, TreeItem parent, OptionTreeItemType type)
	{
		base.Header = strHeader;
		base.Value = strValue;
		base.Description = strDescription;
		base.Parent = parent;
		base.Type = type.ToString();
		if (type == OptionTreeItemType.AlphaNumeric || type == OptionTreeItemType.Numeric)
		{
			base.IsEnableEdit = true;
		}
		if (type == OptionTreeItemType.Decision)
		{
			base.Value = base.Header;
		}
		base.Image = (DrawingImage)new ResourceDictionary
		{
			Source = new Uri("pack://application:,,,/Preference.WPF.Controls;component/Resources/OptionsTreeIcons.xaml", UriKind.Absolute)
		}[$"icon{type.ToString()}None"];
	}

	public OptionTreeItem(string strHeader, string strValue, string strDescription, TreeItem parent, OptionTreeItemType type, bool bIsChecked)
	{
		base.Header = strHeader;
		base.Value = strValue;
		base.Description = strDescription;
		base.Parent = parent;
		base.Type = type.ToString();
		base.IsChecked = bIsChecked;
		if (type == OptionTreeItemType.AlphaNumeric || type == OptionTreeItemType.Numeric)
		{
			base.IsEnableEdit = true;
		}
		if (type == OptionTreeItemType.Decision)
		{
			base.Value = base.Header;
		}
		ResourceDictionary resourceDictionary = new ResourceDictionary
		{
			Source = new Uri("pack://application:,,,/Preference.WPF.Controls;component/Resources/OptionsTreeIcons.xaml", UriKind.Absolute)
		};
		if (base.IsChecked)
		{
			base.Image = (DrawingImage)resourceDictionary[$"icon{type.ToString()}Checked"];
		}
		else
		{
			base.Image = (DrawingImage)resourceDictionary[$"icon{type.ToString()}None"];
		}
	}
}
