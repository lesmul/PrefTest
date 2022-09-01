using System.Windows;
using System.Windows.Controls;

namespace Preference.Wpf.Controls.Projects.AppLogic;

public class TreeListView : TreeView
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
}
