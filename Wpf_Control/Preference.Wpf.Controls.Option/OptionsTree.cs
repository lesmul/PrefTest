using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace Preference.Wpf.Controls.Options;

public class OptionsTree : TreeListView
{
	private string _strInitDecisionOptionValue;

	public event EventHandler<TreeEventArgs> TreeItemChecked;

	private void OnTreeItemChecked(TreeEventArgs e)
	{
		this.TreeItemChecked?.Invoke(this, e);
	}

	public OptionsTree()
	{
		ResourceDictionary item = new ResourceDictionary
		{
			Source = new Uri("pack://application:,,,/Preference.WPF.Controls;component/Resources/OptionsTreeIcons.xaml", UriKind.Absolute)
		};
		if (Application.Current != null)
		{
			Application.Current.Resources.MergedDictionaries.Add(item);
		}
		else
		{
			base.Resources.MergedDictionaries.Add(item);
		}
		base.SelectedItemChanged += OptionsTreeSelectedItemChanged;
		base.TreeItemCollapsed += OptionsTreeTreeItemCollapsed;
		base.TreeItemExpanded += OptionsTreeTreeItemExpanded;
		base.TreeItemEndEdit += OptionsTreeTreeItemEndEdit;
		base.TreeItemBeginEdit += OptionsTreeTreeItemBeginEdit;
		base.KeyDown += OptionsTreeKeyDown;
		base.MouseDoubleClick += OptionsTreeMouseDoubleClick;
	}

	public new void Filter(string strToFilter)
	{
		Predicate<object> predicateFilterMethod = delegate(object sender)
		{
			if (sender is TreeItem treeItem)
			{
				bool result = HasFilteredChild(treeItem);
				if (treeItem.Contains(strToFilter))
				{
					if (treeItem.Type == OptionTreeItemType.Folder.ToString() || treeItem.Type == OptionTreeItemType.Option.ToString())
					{
						return result;
					}
					return true;
				}
				return result;
			}
			return false;
		};
		Filter(predicateFilterMethod);
	}

	private bool HasFilteredChild(TreeItem item)
	{
		ICollectionView defaultView = CollectionViewSource.GetDefaultView(item.Children);
		if (defaultView != null && defaultView.MoveCurrentToFirst())
		{
			return true;
		}
		return false;
	}

	private void OptionsTreeTreeItemCollapsed(object sender, TreeEventArgs e)
	{
		TreeItem item = e.Item;
		ImageSource image = ((!item.IsSelected) ? GetImage(e.Item.Type, OptionTreeItemState.None) : GetImage(e.Item.Type, OptionTreeItemState.Selected));
		e.Item.Image = image;
	}

	private void OptionsTreeTreeItemExpanded(object sender, TreeEventArgs e)
	{
		TreeItem item = e.Item;
		ImageSource imageSource = ((!item.IsSelected) ? GetImage(e.Item.Type, OptionTreeItemState.Expanded) : GetImage(e.Item.Type, OptionTreeItemState.ExpandedSelected));
		if (imageSource == null)
		{
			imageSource = ((!item.IsSelected) ? GetImage(e.Item.Type, OptionTreeItemState.None) : GetImage(e.Item.Type, OptionTreeItemState.Selected));
		}
		e.Item.Image = imageSource;
	}

	private void OptionsTreeSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
	{
		TreeItem treeItem = e.NewValue as TreeItem;
		TreeItem treeItem2 = e.OldValue as TreeItem;
		ImageSource imageSource;
		if (treeItem != null)
		{
			if (!treeItem.IsExpanded)
			{
				imageSource = ((!treeItem.IsChecked) ? GetImage(treeItem.Type, OptionTreeItemState.Selected) : GetImage(treeItem.Type, OptionTreeItemState.CheckedSelected));
			}
			else
			{
				imageSource = GetImage(treeItem.Type, OptionTreeItemState.ExpandedSelected);
				if (imageSource == null)
				{
					imageSource = GetImage(treeItem.Type, OptionTreeItemState.Selected);
				}
			}
			treeItem.Image = imageSource;
		}
		if (treeItem2 == null)
		{
			return;
		}
		if (!treeItem2.IsExpanded)
		{
			imageSource = ((!treeItem2.IsChecked) ? GetImage(treeItem2.Type, OptionTreeItemState.None) : GetImage(treeItem2.Type, OptionTreeItemState.Checked));
		}
		else
		{
			imageSource = GetImage(treeItem2.Type, OptionTreeItemState.Expanded);
			if (imageSource == null)
			{
				imageSource = GetImage(treeItem2.Type, OptionTreeItemState.None);
			}
		}
		treeItem2.Image = imageSource;
	}

	private void OptionsTreeTreeItemBeginEdit(object sender, TreeEventArgs e)
	{
		_strInitDecisionOptionValue = e.Item.Value;
	}

	private void OptionsTreeTreeItemEndEdit(object sender, TreeEventArgs e)
	{
		TreeItem item = e.Item;
		if (item.Value != _strInitDecisionOptionValue)
		{
			TreeEventArgs treeEventArgs = new TreeEventArgs();
			treeEventArgs.Item = item;
			OnTreeItemChecked(treeEventArgs);
		}
	}

	private ImageSource GetImage(string strItemType, OptionTreeItemState optionTreeItemState)
	{
		DrawingImage result = new DrawingImage();
		string resourceKey = $"icon{strItemType}{optionTreeItemState.ToString()}";
		if (!string.IsNullOrEmpty(strItemType))
		{
			result = (DrawingImage)TryFindResource(resourceKey);
		}
		return result;
	}

	private void OptionsTreeKeyDown(object sender, KeyEventArgs e)
	{
		if (base.SelectedItem is TreeItem currentItem)
		{
			Key key = e.Key;
			if (key == Key.Space)
			{
				CheckItem(currentItem);
			}
		}
	}

	private void OptionsTreeMouseDoubleClick(object sender, MouseButtonEventArgs e)
	{
		if (base.SelectedItem is TreeItem currentItem)
		{
			CheckItem(currentItem);
		}
	}

	private void CheckItem(TreeItem currentItem)
	{
		if (currentItem.Type == OptionTreeItemType.AlphaNumeric.ToString() || currentItem.Type == OptionTreeItemType.Numeric.ToString() || currentItem.Type == OptionTreeItemType.Folder.ToString() || currentItem.Type == OptionTreeItemType.Option.ToString())
		{
			return;
		}
		if (currentItem.Type == OptionTreeItemType.Decision.ToString())
		{
			if (currentItem.IsChecked)
			{
				currentItem.Image = GetImage(currentItem.Type, OptionTreeItemState.Selected);
				currentItem.Value = "No";
				currentItem.IsChecked = false;
			}
			else
			{
				currentItem.Image = GetImage(currentItem.Type, OptionTreeItemState.CheckedSelected);
				currentItem.Value = "Sí";
				currentItem.IsChecked = true;
			}
		}
		else if (!currentItem.IsChecked)
		{
			currentItem.Image = GetImage(currentItem.Type, OptionTreeItemState.CheckedSelected);
			currentItem.IsChecked = true;
			foreach (TreeItem child in currentItem.Parent.Children)
			{
				if (currentItem != child && currentItem.IsChecked)
				{
					child.Image = GetImage(child.Type, OptionTreeItemState.None);
					child.IsChecked = false;
				}
			}
		}
		TreeEventArgs treeEventArgs = new TreeEventArgs();
		treeEventArgs.Item = currentItem;
		OnTreeItemChecked(treeEventArgs);
	}
}
