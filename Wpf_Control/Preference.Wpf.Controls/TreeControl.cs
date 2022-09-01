using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Preference.Wpf.Controls.Properties;

namespace Preference.Wpf.Controls;

public class TreeControl : TreeView
{
	private bool _bIsContextMenuEnabled;

	private string _initOptionValue;

	private bool _bIsEscapePressed;

	private bool _bIsEditing;

	private TreeItem _lastItemFound;

	private string _strFilterValue;

	private Collection<TreeItem> _searchResults;

	public bool IsContextMenuEnabled
	{
		get
		{
			return _bIsContextMenuEnabled;
		}
		set
		{
			_bIsContextMenuEnabled = value;
		}
	}

	private string InitOptionValue
	{
		get
		{
			return _initOptionValue;
		}
		set
		{
			_initOptionValue = value;
		}
	}

	private bool IsEscapePressed
	{
		get
		{
			return _bIsEscapePressed;
		}
		set
		{
			_bIsEscapePressed = value;
		}
	}

	private bool IsEditing
	{
		get
		{
			return _bIsEditing;
		}
		set
		{
			_bIsEditing = value;
		}
	}

	public TreeItem LastItemFound
	{
		get
		{
			return _lastItemFound;
		}
		set
		{
			_lastItemFound = value;
		}
	}

	private string FilterValue
	{
		get
		{
			return _strFilterValue;
		}
		set
		{
			_strFilterValue = value;
		}
	}

	private Collection<TreeItem> SearchResults
	{
		get
		{
			if (_searchResults == null)
			{
				_searchResults = new Collection<TreeItem>();
			}
			return _searchResults;
		}
	}

	public event EventHandler<TreeEventArgs> TreeItemExpanded;

	public event EventHandler<TreeEventArgs> TreeItemCollapsed;

	public event EventHandler<TreeEventArgs> TreeItemBeginEdit;

	public event EventHandler<TreeEventArgs> TreeItemEndEdit;

	private void OnTreeItemExpanded(TreeEventArgs e)
	{
		this.TreeItemExpanded?.Invoke(this, e);
	}

	private void OnTreeItemCollapsed(TreeEventArgs e)
	{
		this.TreeItemCollapsed?.Invoke(this, e);
	}

	private void OnTreeItemBeginEdit(TreeEventArgs e)
	{
		this.TreeItemBeginEdit?.Invoke(this, e);
	}

	private void OnTreeItemEndEdit(TreeEventArgs e)
	{
		this.TreeItemEndEdit?.Invoke(this, e);
	}

	public TreeControl()
	{
	}

	public TreeControl(bool bContextMenuEnabled)
	{
		IsContextMenuEnabled = bContextMenuEnabled;
	}

	public void Fill(Collection<TreeItem> collectionTreeItems)
	{
		if (collectionTreeItems != null)
		{
			base.ItemsSource = collectionTreeItems;
			AddExpansionControlToNodes(collectionTreeItems);
		}
	}

	public void Clean()
	{
		base.ItemsSource = null;
	}

	public void Filter(string strFilter)
	{
		if (string.IsNullOrEmpty(strFilter))
		{
			ClearFilter();
		}
		FilterValue = strFilter;
		foreach (TreeItem item in (IEnumerable)base.Items)
		{
			FilterBranch(item, strFilter);
		}
		ICollectionView defaultView = CollectionViewSource.GetDefaultView(base.Items);
		if (defaultView != null)
		{
			defaultView.Filter = (object sender) => (sender is TreeItem treeItem && treeItem.Contains(strFilter)) ? true : false;
		}
		ExpandAllNodes();
	}

	public void Filter(Predicate<object> predicateFilterMethod)
	{
		if (predicateFilterMethod == null)
		{
			return;
		}
		foreach (TreeItem item in (IEnumerable)base.Items)
		{
			ApplyCustomFilterToBranch(item, predicateFilterMethod);
		}
		ICollectionView defaultView = CollectionViewSource.GetDefaultView(base.Items);
		if (defaultView != null)
		{
			defaultView.Filter = predicateFilterMethod;
		}
		ExpandAllNodes();
	}

	public void ClearFilter()
	{
		ICollectionView defaultView = CollectionViewSource.GetDefaultView(base.Items);
		defaultView.Filter = null;
		foreach (TreeItem item in (IEnumerable)base.Items)
		{
			RemoveBranchFilter(item);
		}
		CollapseAllNodes();
		Collection<TreeItem> collectionBranch = base.ItemsSource as Collection<TreeItem>;
		AddExpansionControlToNodes(collectionBranch);
	}

	public bool Search(string strToSearch)
	{
		if (!(base.ItemsSource is Collection<TreeItem>))
		{
			return false;
		}
		if (!ExpandAndSelectItem(this, strToSearch))
		{
			SearchResults.Clear();
			return ExpandAndSelectItem(this, strToSearch);
		}
		return true;
	}

	public void ExpandAllNodes()
	{
		ExpandSubContainers(this);
		FocusFirstItem();
	}

	public void ExpandSubBranch(TreeViewItem treeItem)
	{
		if (treeItem != null)
		{
			ExpandSubContainers(treeItem);
		}
	}

	public void CollapseAllNodes()
	{
		CollapseSubContainers(this);
	}

	public void CollapseSubBranch(TreeViewItem treeItem)
	{
		if (treeItem != null)
		{
			CollapseSubContainers(treeItem);
		}
	}

	public TreeViewItem GetContainer(object item)
	{
		if (item == null)
		{
			return null;
		}
		return GetContainerFromTreeItem(this, item);
	}

	public bool FocusFirstItem()
	{
		if (base.Items.Count > 0 && base.ItemContainerGenerator.ContainerFromIndex(0) is TreeListViewItem treeListViewItem)
		{
			treeListViewItem.Focus();
			return true;
		}
		return false;
	}

	public void FocusItem(TreeItem currentItem)
	{
		if (currentItem != null)
		{
			GetContainer(currentItem)?.Focus();
		}
	}

	protected override void OnInitialized(EventArgs e)
	{
		CreateContextMenu();
		base.OnInitialized(e);
	}

	protected override void OnSelectedItemChanged(RoutedPropertyChangedEventArgs<object> e)
	{
		TreeItem treeItem = e.NewValue as TreeItem;
		TreeItem treeItem2 = e.OldValue as TreeItem;
		if (treeItem != null)
		{
			treeItem.IsSelected = true;
		}
		if (treeItem2 != null)
		{
			treeItem2.IsSelected = false;
		}
		base.OnSelectedItemChanged(e);
	}

	protected override void OnPreviewMouseDoubleClick(MouseButtonEventArgs e)
	{
		TreeItem treeItem = base.SelectedItem as TreeItem;
		TreeViewItem container = GetContainer(treeItem);
		if (treeItem != null && container != null && treeItem.IsEnableEdit)
		{
			e.Handled = true;
			EnterEdit(container);
		}
		base.OnPreviewMouseDoubleClick(e);
	}

	protected override void OnPreviewKeyDown(KeyEventArgs e)
	{
		if (!(base.SelectedItem is TreeItem treeItem))
		{
			return;
		}
		TreeViewItem container = GetContainer(treeItem);
		switch (e.Key)
		{
		case Key.Space:
			if (IsEditing)
			{
				break;
			}
			if (treeItem.IsEnableEdit)
			{
				e.Handled = true;
				EnterEdit(container);
				return;
			}
			if (treeItem.Children.Count > 0 && container != null)
			{
				if (container.IsExpanded)
				{
					container.IsExpanded = false;
				}
				else
				{
					container.IsExpanded = true;
				}
			}
			break;
		case Key.Right:
		case Key.F2:
			if (!IsEditing && treeItem.IsEnableEdit)
			{
				e.Handled = true;
				EnterEdit(container);
			}
			break;
		case Key.Multiply:
			if (container != null)
			{
				container.IsExpanded = true;
				ExpandSubBranch(container);
				DispatcherHelper.WaitForPriority(DispatcherPriority.Render);
				container.Focus();
			}
			e.Handled = true;
			break;
		}
		base.OnPreviewKeyDown(e);
	}

	protected override void OnContextMenuOpening(ContextMenuEventArgs e)
	{
		if (base.Items == null || base.Items.Count == 0)
		{
			base.ContextMenu.IsEnabled = false;
		}
		base.OnContextMenuOpening(e);
	}

	private void EnterEdit(TreeViewItem tvItem)
	{
		if (tvItem != null)
		{
			object visualRecursive = GetVisualRecursive(tvItem, "templatedTextboxHeader");
			if (visualRecursive is TextBox textBox)
			{
				IsEditing = true;
				TreeEventArgs treeEventArgs = new TreeEventArgs();
				treeEventArgs.Item = base.SelectedItem as TreeItem;
				OnTreeItemBeginEdit(treeEventArgs);
				InitOptionValue = textBox.Text;
				textBox.SelectionStart = 0;
				textBox.SelectionLength = textBox.Text.Length;
				textBox.Focus();
				textBox.PreviewKeyDown += TemplateTextBoxHeaderPreviewKeyDown;
				textBox.PreviewLostKeyboardFocus += TemplateTextBoxHeaderPreviewLostKeyboardFocus;
			}
		}
	}

	private void FilterBranch(TreeItem currentItem, string strFilter)
	{
		if (currentItem == null || currentItem.Children.Count <= 0)
		{
			return;
		}
		foreach (TreeItem child in currentItem.Children)
		{
			FilterBranch(child, strFilter);
		}
		ICollectionView defaultView = CollectionViewSource.GetDefaultView(currentItem.Children);
		if (defaultView != null)
		{
			defaultView.Filter = (object sender) => (sender is TreeItem treeItem && treeItem.Contains(strFilter)) ? true : false;
		}
	}

	private void ApplyCustomFilterToBranch(TreeItem currentItem, Predicate<object> predicateFilterMethod)
	{
		if (currentItem == null || currentItem.Children.Count <= 0)
		{
			return;
		}
		foreach (TreeItem child in currentItem.Children)
		{
			ApplyCustomFilterToBranch(child, predicateFilterMethod);
		}
		ICollectionView defaultView = CollectionViewSource.GetDefaultView(currentItem.Children);
		if (defaultView != null)
		{
			defaultView.Filter = predicateFilterMethod;
		}
	}

	private void RemoveBranchFilter(TreeItem item)
	{
		if (item == null || item.Children.Count <= 0)
		{
			return;
		}
		foreach (TreeItem child in item.Children)
		{
			RemoveBranchFilter(child);
		}
		ICollectionView defaultView = CollectionViewSource.GetDefaultView(item.Children);
		defaultView.Filter = null;
	}

	private void CreateContextMenu()
	{
		ContextMenu contextMenu = new ContextMenu();
		MenuItem menuItem = new MenuItem();
		menuItem.Header = Preference.Wpf.Controls.Properties.Resources.StringMenuItemExpandAll;
		menuItem.Click += MenuItemExpandAllClick;
		MenuItem menuItem2 = new MenuItem();
		menuItem2.Header = Preference.Wpf.Controls.Properties.Resources.StringMenuItemCollapseAll;
		menuItem2.Click += MenuItemCollapseAllClick;
		MenuItem menuItem3 = new MenuItem();
		menuItem3.Header = Preference.Wpf.Controls.Properties.Resources.StringMenuItemCollapseCurrent;
		menuItem3.Click += MenuItemCollapseCurrentClick;
		MenuItem menuItem4 = new MenuItem();
		menuItem4.Header = Preference.Wpf.Controls.Properties.Resources.StringMenuItemExpandCurrent;
		menuItem4.Click += MenuItemExpandCurrentClick;
		contextMenu.Items.Add(menuItem4);
		contextMenu.Items.Add(menuItem3);
		contextMenu.Items.Add(new Separator());
		contextMenu.Items.Add(menuItem);
		contextMenu.Items.Add(menuItem2);
		base.ContextMenu = contextMenu;
	}

	private void MenuItemExpandAllClick(object sender, RoutedEventArgs e)
	{
		ExpandAllNodes();
	}

	private void MenuItemCollapseAllClick(object sender, RoutedEventArgs e)
	{
		CollapseAllNodes();
	}

	private void MenuItemCollapseCurrentClick(object sender, RoutedEventArgs e)
	{
		if (base.SelectedItem is TreeItem item)
		{
			TreeViewItem container = GetContainer(item);
			if (container != null)
			{
				CollapseSubBranch(container);
				container.IsExpanded = false;
			}
		}
	}

	private void MenuItemExpandCurrentClick(object sender, RoutedEventArgs e)
	{
		if (base.SelectedItem is TreeItem item)
		{
			TreeViewItem container = GetContainer(item);
			if (container != null)
			{
				container.IsExpanded = true;
				ExpandSubBranch(container);
			}
		}
	}

	public TreeViewItem GetContainerFromTreeItem(ItemsControl parentContainer, object itemToSelect)
	{
		if (parentContainer == null)
		{
			return null;
		}
		if (itemToSelect == null)
		{
			return null;
		}
		TreeViewItem treeViewItem = null;
		foreach (object item in (IEnumerable)parentContainer.Items)
		{
			if (treeViewItem != null)
			{
				return treeViewItem;
			}
			TreeViewItem treeViewItem2 = parentContainer.ItemContainerGenerator.ContainerFromItem(item) as TreeViewItem;
			if (item == itemToSelect && treeViewItem2 != null)
			{
				treeViewItem = treeViewItem2;
			}
			else if (treeViewItem2 != null && treeViewItem2.Items.Count > 0)
			{
				treeViewItem = GetContainerFromTreeItem(treeViewItem2, itemToSelect);
			}
		}
		return treeViewItem;
	}

	private void ExpandSubContainers(ItemsControl parentContainer)
	{
		if (parentContainer == null)
		{
			return;
		}
		if (parentContainer.ItemContainerGenerator.Status != GeneratorStatus.ContainersGenerated)
		{
			EventHandler eventHandler = null;
			eventHandler = delegate
			{
				if (parentContainer.ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated)
				{
					ExpandSubContainers(parentContainer);
					parentContainer.ItemContainerGenerator.StatusChanged -= eventHandler;
				}
			};
			parentContainer.ItemContainerGenerator.StatusChanged += eventHandler;
			return;
		}
		foreach (object item in (IEnumerable)parentContainer.Items)
		{
			TreeViewItem currentContainer = parentContainer.ItemContainerGenerator.ContainerFromItem(item) as TreeViewItem;
			if (currentContainer == null || currentContainer.Items.Count <= 0)
			{
				continue;
			}
			currentContainer.IsExpanded = true;
			if (currentContainer.ItemContainerGenerator.Status != GeneratorStatus.ContainersGenerated)
			{
				EventHandler eh = null;
				eh = delegate
				{
					if (currentContainer.ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated)
					{
						ExpandSubContainers(currentContainer);
						currentContainer.ItemContainerGenerator.StatusChanged -= eh;
					}
				};
				currentContainer.ItemContainerGenerator.StatusChanged += eh;
			}
			else
			{
				ExpandSubContainers(currentContainer);
			}
		}
	}

	private void CollapseSubContainers(ItemsControl parentContainer)
	{
		if (parentContainer == null)
		{
			return;
		}
		foreach (object item in (IEnumerable)parentContainer.Items)
		{
			if (parentContainer.ItemContainerGenerator.ContainerFromItem(item) is TreeViewItem treeViewItem)
			{
				if (treeViewItem.Items.Count > 0)
				{
					CollapseSubContainers(treeViewItem);
				}
				treeViewItem.IsExpanded = false;
			}
		}
	}

	private bool ExpandAndSelectItem(ItemsControl container, string strToSearch)
	{
		foreach (object item in (IEnumerable)container.Items)
		{
			TreeViewItem treeViewItem = container.ItemContainerGenerator.ContainerFromItem(item) as TreeViewItem;
			if (treeViewItem == null)
			{
				DispatcherHelper.WaitForPriority(DispatcherPriority.Render);
				treeViewItem = container.ItemContainerGenerator.ContainerFromItem(item) as TreeViewItem;
				if (treeViewItem == null)
				{
					continue;
				}
			}
			TreeItem treeItem = item as TreeItem;
			if (treeItem.Contains(strToSearch) && !SearchResults.Contains(treeItem))
			{
				treeViewItem.IsSelected = true;
				treeViewItem.BringIntoView();
				treeViewItem.Focus();
				SearchResults.Add(treeItem);
				return true;
			}
			if (treeViewItem.Items.Count > 0)
			{
				bool isExpanded = treeViewItem.IsExpanded;
				treeViewItem.IsExpanded = true;
				if (ExpandAndSelectItem(treeViewItem, strToSearch))
				{
					return true;
				}
				treeViewItem.IsExpanded = isExpanded;
			}
		}
		return false;
	}

	private void AddExpansionControlToNodes(Collection<TreeItem> collectionBranch)
	{
		foreach (TreeItem item in collectionBranch)
		{
			TreeViewItem container = GetContainer(item);
			if (container != null)
			{
				container.Expanded += TreeViewItemExpanded;
				container.Collapsed += TreeListViewItemCollapsed;
			}
			if (item.Children.Count > 0)
			{
				AddExpansionControlToNodes(item.Children);
			}
		}
	}

	private void TreeViewItemExpanded(object sender, RoutedEventArgs e)
	{
		if (e.OriginalSource is TreeViewItem treeViewItem)
		{
			treeViewItem.Focus();
			TreeItem treeItem = base.SelectedItem as TreeItem;
			if (treeItem != null && treeItem.Children.Count > 0)
			{
				treeItem.IsExpanded = true;
			}
			TreeEventArgs treeEventArgs = new TreeEventArgs();
			treeEventArgs.Item = treeItem;
			OnTreeItemExpanded(treeEventArgs);
		}
	}

	private void TreeListViewItemCollapsed(object sender, RoutedEventArgs e)
	{
		if (e.OriginalSource is TreeViewItem treeViewItem)
		{
			treeViewItem.Focus();
			TreeItem treeItem = base.SelectedItem as TreeItem;
			if (treeItem != null)
			{
				treeItem.IsExpanded = false;
			}
			TreeEventArgs treeEventArgs = new TreeEventArgs();
			treeEventArgs.Item = treeItem;
			OnTreeItemCollapsed(treeEventArgs);
		}
	}

	private object GetVisualRecursive(DependencyObject sender, string strName)
	{
		FrameworkElement frameworkElement = sender as FrameworkElement;
		object obj = null;
		if (frameworkElement == null)
		{
			return null;
		}
		if (frameworkElement.Name == strName)
		{
			return frameworkElement;
		}
		for (int i = 0; i < VisualTreeHelper.GetChildrenCount(sender); i++)
		{
			obj = GetVisualRecursive(VisualTreeHelper.GetChild(sender, i), strName);
			if (obj != null)
			{
				break;
			}
		}
		return obj;
	}

	private TreeItem GetItemFromContainer(TreeViewItem treeViewItem)
	{
		if (treeViewItem == null)
		{
			return null;
		}
		return treeViewItem.DataContext as TreeItem;
	}

	private void TemplateTextBoxHeaderPreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
	{
		TextBox textBox = sender as TextBox;
		TreeItem treeItem = base.SelectedItem as TreeItem;
		if (IsEscapePressed)
		{
			textBox.Text = InitOptionValue;
			IsEscapePressed = false;
			e.Handled = false;
		}
		else
		{
			treeItem.Value = textBox.Text;
		}
		IsEditing = false;
		TreeEventArgs treeEventArgs = new TreeEventArgs();
		treeEventArgs.Item = treeItem;
		OnTreeItemEndEdit(treeEventArgs);
	}

	private void TemplateTextBoxHeaderPreviewKeyDown(object sender, KeyEventArgs e)
	{
		TextBox textBox = sender as TextBox;
		TreeViewItem container = GetContainer(base.SelectedItem);
		if (textBox == null || container == null)
		{
			return;
		}
		switch (e.Key)
		{
		case Key.Right:
			if (textBox.CaretIndex == textBox.Text.Length)
			{
				container.Focus();
				e.Handled = true;
			}
			break;
		case Key.Left:
			if (textBox.CaretIndex == 0)
			{
				container.Focus();
			}
			break;
		case Key.Return:
			container.Focus();
			e.Handled = true;
			break;
		case Key.Escape:
			IsEscapePressed = true;
			container.Focus();
			e.Handled = true;
			break;
		}
	}
}
