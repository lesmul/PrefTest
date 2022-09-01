using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Preference.Wpf.Controls.Properties;

namespace Preference.Wpf.Controls;

public class GalleryControl : ListView, IComponentConnector
{
	private string _strCurrentView;

	private Collection<string> _searchResults;

	internal MenuItem ViewsContextMenu;

	internal MenuItem ListViewMode;

	internal MenuItem TileViewMode;

	internal MenuItem GalleryViewMode;

	internal MenuItem GridViewMode;

	private bool _contentLoaded;

	public string CurrentView
	{
		get
		{
			return _strCurrentView;
		}
		set
		{
			_strCurrentView = value;
			ChangeView(_strCurrentView);
		}
	}

	private Collection<string> SearchResults
	{
		get
		{
			if (_searchResults == null)
			{
				_searchResults = new Collection<string>();
			}
			return _searchResults;
		}
	}

	public event EventHandler<EventArgs> GalleryViewChanged;

	private void OnGalleryViewChanged(EventArgs e)
	{
		this.GalleryViewChanged?.Invoke(this, e);
	}

	public GalleryControl()
	{
		InitializeComponent();
	}

	public GalleryControl(Collection<GalleryItem> collectionItems)
	{
		InitializeComponent();
		Fill(collectionItems);
	}

	public void Fill(Collection<GalleryItem> collectionItems)
	{
		Clear();
		if (collectionItems == null)
		{
			return;
		}
		foreach (GalleryItem collectionItem in collectionItems)
		{
			base.Items.Add(collectionItem);
		}
	}

	public void Clear()
	{
		if (base.Items.Count > 0)
		{
			base.Items.Clear();
		}
	}

	public void AddItem(int nIndex, GalleryItem newItem)
	{
		if (nIndex > -1)
		{
			base.Items.Insert(nIndex, newItem);
		}
		else
		{
			base.Items.Insert(0, newItem);
		}
		ScrollIntoView(base.Items[nIndex]);
	}

	public void AddItem(GalleryItem newItem)
	{
		AddItem(base.Items.Count, newItem);
	}

	public bool RemoveItem(int nIndex)
	{
		if (nIndex < 0)
		{
			return false;
		}
		if (base.Items.Count == 0)
		{
			return false;
		}
		base.Items.RemoveAt(nIndex);
		return true;
	}

	public bool RemoveItem(GalleryItem galleryItem)
	{
		if (galleryItem == null)
		{
			return false;
		}
		if (base.Items.Count == 0)
		{
			return false;
		}
		base.Items.Remove(galleryItem);
		return true;
	}

	public bool RemoveItem(string strName)
	{
		if (string.IsNullOrEmpty(strName))
		{
			return false;
		}
		if (base.Items.Count == 0)
		{
			return false;
		}
		GalleryItem galleryItem = FindItem(strName);
		if (galleryItem == null)
		{
			return false;
		}
		base.Items.Remove(galleryItem);
		return true;
	}

	public bool UpdateItem(GalleryItem oldItem, GalleryItem newItem)
	{
		if (oldItem == null || newItem == null)
		{
			return false;
		}
		if (oldItem == newItem)
		{
			return false;
		}
		if (base.Items.Count == 0)
		{
			return false;
		}
		int insertIndex = base.Items.IndexOf(oldItem);
		base.Items.Remove(oldItem);
		base.Items.Insert(insertIndex, newItem);
		return true;
	}

	public bool UpdateItem(int nIndex, GalleryItem newItem)
	{
		if (nIndex < 0)
		{
			return false;
		}
		if (newItem == null)
		{
			return false;
		}
		if (base.Items.Count == 0)
		{
			return false;
		}
		GalleryItem galleryItem = base.Items[nIndex] as GalleryItem;
		if (galleryItem == newItem)
		{
			return false;
		}
		base.Items.RemoveAt(nIndex);
		base.Items.Insert(nIndex, newItem);
		base.Items.Refresh();
		return true;
	}

	public bool UpdateItem(string strName, GalleryItem newItem)
	{
		if (string.IsNullOrEmpty(strName))
		{
			return false;
		}
		if (newItem == null)
		{
			return false;
		}
		if (base.Items.Count == 0)
		{
			return false;
		}
		GalleryItem galleryItem = FindItem(strName);
		if (galleryItem == null)
		{
			return false;
		}
		int insertIndex = base.Items.IndexOf(galleryItem);
		if (galleryItem == newItem)
		{
			return false;
		}
		base.Items.Remove(galleryItem);
		base.Items.Insert(insertIndex, newItem);
		base.Items.Refresh();
		return true;
	}

	public bool UpdateItem(int nIndex, ImageSource isNewImage)
	{
		if (nIndex < 0)
		{
			return false;
		}
		if (base.Items.Count == 0)
		{
			return false;
		}
		if (!(base.Items[nIndex] is GalleryItem galleryItem))
		{
			return false;
		}
		galleryItem.ItemImage = isNewImage;
		base.Items.Refresh();
		return true;
	}

	public bool UpdateItem(int nIndex, string strNewName)
	{
		if (nIndex < 0)
		{
			return false;
		}
		if (base.Items.Count == 0)
		{
			return false;
		}
		if (!(base.Items[nIndex] is GalleryItem galleryItem))
		{
			return false;
		}
		galleryItem.ItemName = strNewName;
		base.Items.Refresh();
		return true;
	}

	public void Filter(string strToFilter)
	{
		base.Items.Filter = (object sender) => (sender is GalleryItem galleryItem && galleryItem.Contains(strToFilter)) ? true : false;
	}

	public void Filter(Predicate<object> predicateMethod)
	{
		if (predicateMethod != null)
		{
			base.Items.Filter = predicateMethod;
		}
	}

	public void ClearFilter()
	{
		base.Items.Filter = null;
	}

	public int FindNextItem(GalleryItem itemToFind, int nIndexToStart)
	{
		if (itemToFind == null)
		{
			return -1;
		}
		if (nIndexToStart < 0)
		{
			nIndexToStart = 0;
		}
		for (int i = nIndexToStart; i < base.Items.Count - 1; i++)
		{
			GalleryItem galleryItem = base.Items[i] as GalleryItem;
			if (galleryItem == itemToFind)
			{
				base.SelectedIndex = i;
				base.SelectedItem = galleryItem;
				return i;
			}
		}
		return -1;
	}

	public int FindNextItem(string strToFind, int nIndexToStart)
	{
		if (string.IsNullOrEmpty(strToFind))
		{
			return -1;
		}
		if (nIndexToStart < 0)
		{
			nIndexToStart = 0;
		}
		for (int i = nIndexToStart + 1; i < base.Items.Count; i++)
		{
			if (base.Items[i] is GalleryItem galleryItem)
			{
				string value = strToFind.ToUpper();
				if (galleryItem.ItemName.ToUpper().Contains(value) || galleryItem.ItemDescription.ToUpper().Contains(value) || galleryItem.ItemValue.ToUpper().Contains(value))
				{
					base.SelectedIndex = i;
					base.SelectedItem = galleryItem;
					return i;
				}
			}
		}
		return -1;
	}

	public bool Search(string strToSearch)
	{
		if (!SearchItem(strToSearch))
		{
			SearchResults.Clear();
			return SearchItem(strToSearch);
		}
		return true;
	}

	private bool SearchItem(string strToSearch)
	{
		for (int i = 0; i < base.Items.Count; i++)
		{
			if (base.Items[i] is GalleryItem galleryItem && galleryItem.Contains(strToSearch) && !SearchResults.Contains(galleryItem.ItemName))
			{
				base.SelectedIndex = i;
				base.SelectedItem = galleryItem;
				SearchResults.Add(galleryItem.ItemName);
				return true;
			}
		}
		return false;
	}

	private void GalleryListViewLoaded(object sender, RoutedEventArgs e)
	{
		InitializeGallery();
	}

	private void InitializeGallery()
	{
		if (string.IsNullOrEmpty(CurrentView))
		{
			base.ItemContainerStyle = FindResource("GalleryViewMode") as Style;
		}
		else
		{
			base.ItemContainerStyle = FindResource(CurrentView) as Style;
		}
		base.SelectionChanged += GalleryControlSelectionChanged;
	}

	private void GalleryControlSelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		ScrollIntoView(base.SelectedItem);
	}

	private void ChangeView(string strViewMode)
	{
		if (string.IsNullOrEmpty(strViewMode))
		{
			return;
		}
		try
		{
			if (strViewMode == Preference.Wpf.Controls.Properties.Resources.StringGridViewMode)
			{
				base.Style = FindResource(strViewMode) as Style;
				base.ItemContainerStyle = null;
			}
			else
			{
				base.Style = null;
				base.ItemContainerStyle = FindResource(strViewMode) as Style;
			}
			OnGalleryViewChanged(EventArgs.Empty);
		}
		catch (ResourceReferenceKeyNotFoundException resourceKey)
		{
			throw new ResourceReferenceKeyNotFoundException(Preference.Wpf.Controls.Properties.Resources.StringErrorGalleryView, resourceKey);
		}
	}

	private GalleryItem FindItem(string strItemName)
	{
		if (base.Items.Count <= 0)
		{
			return null;
		}
		foreach (GalleryItem item in (IEnumerable)base.Items)
		{
			if (item.ItemName == strItemName)
			{
				return item;
			}
		}
		return null;
	}

	private void ContextMenuClick(object sender, RoutedEventArgs e)
	{
		if (!(sender is MenuItem menuItem))
		{
			return;
		}
		CurrentView = menuItem.Name;
		foreach (MenuItem item in (IEnumerable)ViewsContextMenu.Items)
		{
			if (item == menuItem)
			{
				item.IsChecked = true;
			}
			else
			{
				item.IsChecked = false;
			}
		}
	}

	public static ImageSource TransformXamlToImage(string strXaml)
	{
		int num = 100;
		int num2 = 100;
		if (string.IsNullOrEmpty(strXaml))
		{
			return null;
		}
		try
		{
			byte[] bytes = Encoding.UTF32.GetBytes(strXaml);
			Stream stream = new MemoryStream(bytes);
			Canvas canvas = XamlReader.Load(stream) as Canvas;
			canvas.Background = Brushes.White;
			Size availableSize = new Size(num, num2);
			canvas.Measure(availableSize);
			canvas.Arrange(new Rect(0.0, 0.0, num, num2));
			canvas.UpdateLayout();
			canvas.LayoutTransform = new ScaleTransform(canvas.ActualWidth / (double)num, canvas.ActualHeight / (double)num2);
			double dpiX = 96.0 * (double)num / canvas.ActualWidth;
			double dpiY = 96.0 * (double)num2 / canvas.ActualHeight;
			RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap(num, num2, dpiX, dpiY, PixelFormats.Pbgra32);
			renderTargetBitmap.Render(canvas);
			MemoryStream memoryStream = new MemoryStream();
			PngBitmapEncoder pngBitmapEncoder = new PngBitmapEncoder();
			pngBitmapEncoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));
			pngBitmapEncoder.Save(memoryStream);
			BitmapImage bitmapImage = new BitmapImage();
			bitmapImage.BeginInit();
			bitmapImage.StreamSource = memoryStream;
			bitmapImage.EndInit();
			return bitmapImage;
		}
		catch (ArgumentOutOfRangeException)
		{
			return null;
		}
		catch (InvalidOperationException)
		{
			return null;
		}
		catch (NotSupportedException)
		{
			return null;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/Preference.Wpf.Controls;component/gallery/gallerycontrol.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	void IComponentConnector.Connect(int connectionId, object target)
	{
		switch (connectionId)
		{
		case 1:
			((GalleryControl)target).Loaded += GalleryListViewLoaded;
			break;
		case 2:
			ViewsContextMenu = (MenuItem)target;
			break;
		case 3:
			ListViewMode = (MenuItem)target;
			ListViewMode.Click += ContextMenuClick;
			break;
		case 4:
			TileViewMode = (MenuItem)target;
			TileViewMode.Click += ContextMenuClick;
			break;
		case 5:
			GalleryViewMode = (MenuItem)target;
			GalleryViewMode.Click += ContextMenuClick;
			break;
		case 6:
			GridViewMode = (MenuItem)target;
			GridViewMode.Click += ContextMenuClick;
			break;
		default:
			_contentLoaded = true;
			break;
		}
	}
}
