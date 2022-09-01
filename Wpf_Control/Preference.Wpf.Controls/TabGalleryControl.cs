using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using Preference.Wpf.Controls.Properties;

namespace Preference.Wpf.Controls;

public class TabGalleryControl : TabControl, IComponentConnector
{
	private bool _contentLoaded;

	public TabGalleryItem CurrentTab => base.SelectedItem as TabGalleryItem;

	public TabGalleryControl()
	{
		InitializeComponent();
	}

	public TabGalleryControl(Collection<TabGalleryItem> collectionTabItems)
	{
		InitializeComponent();
		Fill(collectionTabItems);
	}

	public void Fill(Collection<TabGalleryItem> collectionTabItems)
	{
		if (collectionTabItems == null)
		{
			return;
		}
		foreach (TabGalleryItem collectionTabItem in collectionTabItems)
		{
			AddTab(collectionTabItem);
		}
	}

	public void AddNewTab(string strTabName)
	{
		GalleryControl gallery = new GalleryControl();
		TabGalleryItem tabItem = new TabGalleryItem(strTabName, Brushes.White, gallery);
		AddTab(tabItem);
	}

	public void AddNewTab(string strTabName, Brush brushColor, GalleryControl gallery)
	{
		TabGalleryItem tabItem = new TabGalleryItem(strTabName, brushColor, gallery);
		AddTab(tabItem);
	}

	public void AddNewTab(string strTabName, Brush brushColor, Collection<GalleryItem> collectionGalleryItems)
	{
		GalleryControl gallery = new GalleryControl(collectionGalleryItems);
		TabGalleryItem tabItem = new TabGalleryItem(strTabName, brushColor, gallery);
		AddTab(tabItem);
	}

	public static void AddItemToGallery(TabGalleryItem tabItem, GalleryItem newGalleryItem)
	{
		if (tabItem != null && newGalleryItem != null)
		{
			tabItem.Gallery.AddItem(newGalleryItem);
		}
	}

	public void AddItemToGallery(int nTabIndex, GalleryItem newGalleryItem)
	{
		if (nTabIndex >= 0 && newGalleryItem != null && base.Items[nTabIndex] is TabGalleryItem tabItem)
		{
			AddItemToGallery(tabItem, newGalleryItem);
		}
	}

	private void TabGalleryLoaded(object sender, RoutedEventArgs e)
	{
		InitializeTabGallery();
	}

	private void AddTab(TabGalleryItem tabItem)
	{
		tabItem.Style = FindResource(Preference.Wpf.Controls.Properties.Resources.StringTabItemsStyle) as Style;
		base.Items.Add(tabItem);
	}

	private void InitializeTabGallery()
	{
		base.Template = FindResource(Preference.Wpf.Controls.Properties.Resources.StringTabControlTemplate) as ControlTemplate;
	}

	protected override void OnSelectionChanged(SelectionChangedEventArgs e)
	{
		if (base.SelectedItem is TabGalleryItem tabGalleryItem)
		{
			base.Background = tabGalleryItem.Background;
		}
		base.OnSelectionChanged(e);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/Preference.Wpf.Controls;component/gallery/tabgallerycontrol.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	void IComponentConnector.Connect(int connectionId, object target)
	{
		if (connectionId == 1)
		{
			((TabGalleryControl)target).Loaded += TabGalleryLoaded;
		}
		else
		{
			_contentLoaded = true;
		}
	}
}
