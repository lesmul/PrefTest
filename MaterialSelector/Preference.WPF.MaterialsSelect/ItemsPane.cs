using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using Preference.WPF.MaterialsSelector.Core;
using Preference.WPF.MaterialsSelector.Models;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;

namespace Preference.WPF.MaterialsSelector.UsersControls;

public class ItemsPane : UserControl, IComponentConnector
{
	private const string REGISTRY_FILTER_BOM_NAME_RADTREELISTVIEW = "ItemsPaneGrid";

	internal RadTreeListView treelistview;

	private bool _contentLoaded;

	public ItemsPane()
	{
		InitializeComponent();
		RadGridViewSerializationHelper.ApplySerializedColumnWidths("ItemsPaneGrid", treelistview);
	}

	private void radTreeListView_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		string uniqueName = e.get_Column().get_UniqueName();
		GridViewLength newWidth = e.get_NewWidth();
		RadGridViewSerializationHelper.SerializeColumnWidth("ItemsPaneGrid", uniqueName, newWidth);
	}

	private void treelistview_DataLoaded(object sender, EventArgs e)
	{
		((GridViewDataControl)treelistview).remove_DataLoaded((EventHandler<EventArgs>)treelistview_DataLoaded);
		((GridViewDataControl)treelistview).ExpandAllHierarchyItems();
	}

	public void SubscribeDataLoadedEvent()
	{
		((GridViewDataControl)treelistview).add_DataLoaded((EventHandler<EventArgs>)treelistview_DataLoaded);
	}

	public void ScrollIntoViewItem(object item)
	{
		if (item != null)
		{
			SyncronizeExpandedItems();
			((GridViewDataControl)treelistview).ScrollIntoView(item);
			((DataControl)treelistview).set_SelectedItem(item);
		}
	}

	private void SyncronizeExpandedItems()
	{
		foreach (object item2 in ((DataControl)treelistview).get_Items())
		{
			if (item2 is Item)
			{
				Item item = item2 as Item;
				SyncronizeExpandedItem(item);
			}
		}
	}

	private void SyncronizeExpandedItem(Item item)
	{
		if (!item.IsExpanded)
		{
			return;
		}
		((GridViewDataControl)treelistview).ExpandHierarchyItem((object)item);
		foreach (Item item2 in item.Items)
		{
			SyncronizeExpandedItem(item2);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/Preference.WPF.MaterialsSelector;component/userscontrols/itemspane.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	void IComponentConnector.Connect(int connectionId, object target)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Expected O, but got Unknown
		if (connectionId == 1)
		{
			treelistview = (RadTreeListView)target;
			((GridViewDataControl)treelistview).add_ColumnWidthChanged((EventHandler<ColumnWidthChangedEventArgs>)radTreeListView_ColumnWidthChanged);
		}
		else
		{
			_contentLoaded = true;
		}
	}
}
