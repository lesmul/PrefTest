using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Preference.Wpf.Controls.Options;

public class GlassTree : TreeListView
{
	private TreeItem _selectedGlass;

	public TreeItem SelectedGlass
	{
		get
		{
			return _selectedGlass;
		}
		set
		{
			_selectedGlass = value;
		}
	}

	public event EventHandler<TreeEventArgs> TreeItemChecked;

	private void OnTreeItemChecked(TreeEventArgs e)
	{
		this.TreeItemChecked?.Invoke(this, e);
	}

	public GlassTree()
	{
		if (Application.Current != null)
		{
			ResourceDictionary item = new ResourceDictionary
			{
				Source = new Uri("pack://application:,,,/Preference.WPF.Controls;component/Resources/OptionsTreeIcons.xaml", UriKind.Absolute)
			};
			Application.Current.Resources.MergedDictionaries.Add(item);
		}
		base.SelectedItemChanged += GlassTreeSelectedItemChanged;
		base.TreeItemCollapsed += GlassTreeTreeItemCollapsed;
		base.TreeItemExpanded += GlassTreeTreeItemExpanded;
		base.KeyDown += GlassTreeKeyDown;
		base.MouseDoubleClick += GlassTreeMouseDoubleClick;
	}

	private void GlassTreeMouseDoubleClick(object sender, MouseButtonEventArgs e)
	{
		if (base.SelectedItem is TreeItem treeItem && treeItem.Children.Count == 0)
		{
			CheckItem(treeItem);
		}
	}

	private void GlassTreeKeyDown(object sender, KeyEventArgs e)
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

	private void GlassTreeTreeItemExpanded(object sender, TreeEventArgs e)
	{
		TreeItem item = e.Item;
		ImageSource imageSource = ((!item.IsSelected) ? GetImage(e.Item.Type, OptionTreeItemState.Expanded) : GetImage(e.Item.Type, OptionTreeItemState.ExpandedSelected));
		if (imageSource == null)
		{
			imageSource = ((!item.IsSelected) ? GetImage(e.Item.Type, OptionTreeItemState.None) : GetImage(e.Item.Type, OptionTreeItemState.Selected));
		}
		e.Item.Image = imageSource;
	}

	private void GlassTreeTreeItemCollapsed(object sender, TreeEventArgs e)
	{
		TreeItem item = e.Item;
		ImageSource image = ((!item.IsSelected) ? GetImage(e.Item.Type, OptionTreeItemState.None) : GetImage(e.Item.Type, OptionTreeItemState.Selected));
		e.Item.Image = image;
	}

	private void GlassTreeSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
	{
		TreeItem treeItem = e.NewValue as TreeItem;
		TreeItem treeItem2 = e.OldValue as TreeItem;
		ImageSource image;
		if (treeItem != null)
		{
			if (treeItem.IsExpanded)
			{
				image = GetImage(treeItem.Type, OptionTreeItemState.ExpandedSelected);
				if (image == null)
				{
					image = GetImage(treeItem.Type, OptionTreeItemState.Selected);
				}
			}
			else
			{
				image = GetImage(treeItem.Type, OptionTreeItemState.Selected);
			}
			treeItem.Image = image;
		}
		if (treeItem2 == null)
		{
			return;
		}
		if (treeItem2.IsExpanded)
		{
			image = GetImage(treeItem2.Type, OptionTreeItemState.Expanded);
			if (image == null)
			{
				image = GetImage(treeItem2.Type, OptionTreeItemState.None);
			}
		}
		else
		{
			image = GetImage(treeItem2.Type, OptionTreeItemState.None);
		}
		treeItem2.Image = image;
	}

	private ImageSource GetImage(string strItemType, OptionTreeItemState optionTreeItemState)
	{
		DrawingImage result = new DrawingImage();
		string resourceKey = $"icon{strItemType}{optionTreeItemState.ToString()}";
		if (!string.IsNullOrEmpty(strItemType))
		{
			result = (DrawingImage)Application.Current.TryFindResource(resourceKey);
		}
		return result;
	}

	private void CheckItem(TreeItem currentItem)
	{
		SelectedGlass = currentItem;
		TreeEventArgs treeEventArgs = new TreeEventArgs();
		treeEventArgs.Item = currentItem;
		OnTreeItemChecked(treeEventArgs);
	}
}
