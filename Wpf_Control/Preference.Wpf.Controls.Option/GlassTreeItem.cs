using System;
using System.Windows;
using System.Windows.Media;

namespace Preference.Wpf.Controls.Options;

public class GlassTreeItem : TreeItem
{
	public GlassTreeItem(string strHeader, string strValue, string strDescription, TreeItem parent, GlassTreeItemType type)
	{
		base.Header = strHeader;
		base.Value = strValue;
		base.Description = strDescription;
		base.Parent = parent;
		base.Type = type.ToString();
		base.Image = (DrawingImage)new ResourceDictionary
		{
			Source = new Uri("pack://application:,,,/Preference.WPF.Controls;component/Resources/OptionsTreeIcons.xaml", UriKind.Absolute)
		}[$"icon{type.ToString()}None"];
	}
}
