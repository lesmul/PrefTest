using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using Microsoft.Win32;
using Preference.Wpf.Controls.Properties;

namespace Preference.Wpf.Controls.Options;

public class GlassDesigner : Grid, IComponentConnector
{
	private const int MAX_RECENT_SEARCHS = 10;

	private bool _bHasPendingChanges;

	private Collection<TreeItem> _glass;

	private int _nNumberRecentSearchs = -1;

	internal SearchFilterUIControl SearchFilterControl;

	internal Grid TreeGridContainer;

	internal GlassTree GlassTree;

	internal GridViewColumn HeaderGlass;

	internal GridViewColumn HeaderDescriptions;

	private bool _contentLoaded;

	public bool HasPendingChanges
	{
		get
		{
			return _bHasPendingChanges;
		}
		set
		{
			_bHasPendingChanges = value;
		}
	}

	public Collection<TreeItem> Glass
	{
		get
		{
			if (_glass == null)
			{
				_glass = new Collection<TreeItem>();
			}
			return _glass;
		}
		set
		{
			_glass = value;
		}
	}

	public int NumberOfRecentSearchs
	{
		get
		{
			if (_nNumberRecentSearchs < 0)
			{
				_nNumberRecentSearchs = 10;
			}
			return _nNumberRecentSearchs;
		}
		set
		{
			if (value < 0)
			{
				_nNumberRecentSearchs = 10;
			}
			else
			{
				_nNumberRecentSearchs = value;
			}
		}
	}

	public string SelectedGlass
	{
		get
		{
			if (GlassTree.SelectedGlass != null)
			{
				return GlassTree.SelectedGlass.Value;
			}
			return string.Empty;
		}
	}

	public GlassDesigner()
	{
		InitializeComponent();
		Translate();
		SearchFilterControl.Loaded += SearchFilterControlLoaded;
		SearchFilterControl.Filter += SearchFilterControlFilter;
		SearchFilterControl.Search += SearchFilterControlSearch;
		SearchFilterControl.ClearFilter += SearchFilterControlClearFilter;
	}

	public void LoadData()
	{
		PopulateTree(Glass);
	}

	private void Translate()
	{
		SearchFilterControl.SearchCombo.ToolTip = Preference.Wpf.Controls.Properties.Resources.StringTooltipFilterText;
		SearchFilterControl.FilterButton.ToolTip = Preference.Wpf.Controls.Properties.Resources.StringTooltipFilterButton;
		SearchFilterControl.SearchButton.ToolTip = Preference.Wpf.Controls.Properties.Resources.StringTooltipSearchButton;
		SearchFilterControl.ClearButton.ToolTip = Preference.Wpf.Controls.Properties.Resources.StringTooltipClearFilterButton;
		HeaderGlass.Header = Preference.Wpf.Controls.Properties.Resources.StringGlass;
		HeaderDescriptions.Header = Preference.Wpf.Controls.Properties.Resources.StringDescriptionHeader;
		GlassTree.TreeItemChecked += GlassTreeTreeItemChecked;
	}

	private void GlassTreeTreeItemChecked(object sender, TreeEventArgs e)
	{
		HasPendingChanges = true;
	}

	private void SaveRecentSearch()
	{
		if (SearchFilterControl.SearchCombo.Items.Count > 2)
		{
			string text = "FilterRecentUsed";
			string empty = string.Empty;
			for (int i = 1; i < SearchFilterControl.SearchCombo.Items.Count; i++)
			{
				ComboBoxItem comboBoxItem = SearchFilterControl.SearchCombo.Items[i] as ComboBoxItem;
				empty = comboBoxItem.Content.ToString();
				AddKeyToRegistry(text + i.ToString(CultureInfo.CurrentCulture), empty);
			}
		}
	}

	private void SearchFilterControlLoaded(object sender, RoutedEventArgs e)
	{
		PopulateSearchCombo();
	}

	private void PopulateSearchCombo()
	{
		RegistryKey registryKey = Registry.CurrentUser.CreateSubKey("Software\\Preference\\WPFControls\\OptionsEditor");
		string[] valueNames = registryKey.GetValueNames();
		string[] array = valueNames;
		foreach (string strKey in array)
		{
			string text = ReadKeyFromRegistry(strKey);
			if (!string.IsNullOrEmpty(text))
			{
				ComboBoxItem comboBoxItem = new ComboBoxItem();
				comboBoxItem.Content = text;
				SearchFilterControl.SearchCombo.Items.Add(comboBoxItem);
			}
		}
	}

	private void InsertInComboRecentUsed(string strRecentSearch)
	{
		if (string.IsNullOrEmpty(strRecentSearch))
		{
			return;
		}
		ComboBoxItem comboBoxItem = null;
		foreach (ComboBoxItem item in (IEnumerable)SearchFilterControl.SearchCombo.Items)
		{
			if (item.Content.ToString() == strRecentSearch)
			{
				comboBoxItem = item;
				break;
			}
		}
		if (comboBoxItem != null)
		{
			SearchFilterControl.SearchCombo.Items.Remove(comboBoxItem);
		}
		if (SearchFilterControl.SearchCombo.Items.Count >= NumberOfRecentSearchs + 1)
		{
			SearchFilterControl.SearchCombo.Items.RemoveAt(SearchFilterControl.SearchCombo.Items.Count - 1);
		}
		ComboBoxItem comboBoxItem3 = new ComboBoxItem();
		comboBoxItem3.Content = strRecentSearch;
		SearchFilterControl.SearchCombo.Items.Insert(1, comboBoxItem3);
		SearchFilterControl.SearchCombo.SelectedIndex = 1;
	}

	private void SearchFilterControlFilter(object sender, EventArgs e)
	{
		FilterOrSearch(bIsSearch: false);
	}

	private void SearchFilterControlSearch(object sender, EventArgs e)
	{
		FilterOrSearch(bIsSearch: true);
	}

	private void SearchFilterControlClearFilter(object sender, EventArgs e)
	{
		ClearFilter();
	}

	private void FilterOrSearch(bool bIsSearch)
	{
		if (!string.IsNullOrEmpty(SearchFilterControl.SearchCombo.Text))
		{
			if (bIsSearch)
			{
				Search();
			}
			else
			{
				Filter();
			}
			InsertInComboRecentUsed(SearchFilterControl.SearchCombo.Text);
			SaveRecentSearch();
		}
	}

	private void OptionsTreeTreeItemChecked(object sender, TreeEventArgs e)
	{
		HasPendingChanges = true;
	}

	private void PopulateTree(Collection<TreeItem> Glass)
	{
		if (Glass != null)
		{
			GlassTree.Fill(Glass);
			GlassTree.FocusFirstItem();
		}
	}

	private void Filter()
	{
		if (Glass != null || Glass.Count != 0)
		{
			if (string.IsNullOrEmpty(SearchFilterControl.SearchCombo.Text))
			{
				ClearFilter();
				return;
			}
			TreeGridContainer.Background = (Brush)base.Resources["FilterBackgroundBrush"];
			GlassTree.Filter(SearchFilterControl.SearchCombo.Text);
		}
	}

	private void Search()
	{
		if (!string.IsNullOrEmpty(SearchFilterControl.SearchCombo.Text) && !GlassTree.Search(SearchFilterControl.SearchCombo.Text))
		{
			MessageBox.Show(Preference.Wpf.Controls.Properties.Resources.WarningNotFound, Preference.Wpf.Controls.Properties.Resources.WarningNotFoundTitle, MessageBoxButton.OK, MessageBoxImage.Exclamation);
			SearchFilterControl.SearchCombo.Focus();
		}
	}

	private void ClearFilter()
	{
		TreeGridContainer.Background = Brushes.White;
		GlassTree.ClearFilter();
	}

	private static void AddKeyToRegistry(string strKey, string strKeyValue)
	{
		try
		{
			RegistryKey registryKey = Registry.CurrentUser.CreateSubKey("Software\\Preference\\WPFControls\\OptionsEditor");
			registryKey.SetValue(strKey, strKeyValue);
		}
		catch (SecurityException inner)
		{
			throw new SecurityException("Error accesing registry", inner);
		}
		catch (UnauthorizedAccessException inner2)
		{
			throw new UnauthorizedAccessException("Unauthorized Registry Access", inner2);
		}
	}

	private static string ReadKeyFromRegistry(string strKey)
	{
		try
		{
			RegistryKey registryKey = Registry.CurrentUser.CreateSubKey("Software\\Preference\\WPFControls\\OptionsEditor");
			return registryKey.GetValue(strKey) as string;
		}
		catch (SecurityException inner)
		{
			throw new SecurityException("Error accesing registry", inner);
		}
		catch (UnauthorizedAccessException inner2)
		{
			throw new UnauthorizedAccessException("Unauthorized Registry Access", inner2);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/Preference.Wpf.Controls;component/options%20editor/glassdesigner.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	internal Delegate _CreateDelegate(Type delegateType, string handler)
	{
		return Delegate.CreateDelegate(delegateType, this, handler);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	void IComponentConnector.Connect(int connectionId, object target)
	{
		switch (connectionId)
		{
		case 1:
			SearchFilterControl = (SearchFilterUIControl)target;
			break;
		case 2:
			TreeGridContainer = (Grid)target;
			break;
		case 3:
			GlassTree = (GlassTree)target;
			break;
		case 4:
			HeaderGlass = (GridViewColumn)target;
			break;
		case 5:
			HeaderDescriptions = (GridViewColumn)target;
			break;
		default:
			_contentLoaded = true;
			break;
		}
	}
}
