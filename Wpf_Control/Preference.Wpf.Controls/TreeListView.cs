using System;
using System.Windows;
using System.Windows.Controls;

namespace Preference.Wpf.Controls;

public class TreeListView : TreeControl
{
	private GridViewColumnCollection _columns;

	public GridViewColumnCollection Columns
	{
		get
		{
			if (_columns == null)
			{
				_columns = new GridViewColumnCollection();
			}
			return _columns;
		}
	}

	protected override DependencyObject GetContainerForItemOverride()
	{
		return new TreeListViewItem();
	}

	protected override bool IsItemItsOwnContainerOverride(object item)
	{
		return item is TreeListViewItem;
	}

	public TreeListView()
	{
		if (Application.Current != null)
		{
			ResourceDictionary item = new ResourceDictionary
			{
				Source = new Uri("pack://application:,,,/Preference.WPF.Controls;component/Resources/TreeStyles.xaml", UriKind.Absolute)
			};
			Application.Current.Resources.MergedDictionaries.Add(item);
		}
	}
}
