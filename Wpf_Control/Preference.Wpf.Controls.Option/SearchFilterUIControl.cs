using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;
using Preference.Wpf.Controls.Properties;

namespace Preference.Wpf.Controls.Options;

public class SearchFilterUIControl : UserControl, IComponentConnector
{
	internal ComboBox SearchCombo;

	internal ToolBar toolBar;

	internal Button SearchButton;

	internal ToggleButton FilterButton;

	internal Button ClearButton;

	private bool _contentLoaded;

	public double ButtonsSize
	{
		get
		{
			return SearchButton.Width;
		}
		set
		{
			_ = 0.0;
		}
	}

	public event EventHandler<EventArgs> Filter;

	public event EventHandler<EventArgs> Search;

	public event EventHandler<EventArgs> ClearFilter;

	public SearchFilterUIControl()
	{
		InitializeComponent();
		InitializeCombo();
		ClearButton.Click += ClearButtonClick;
		FilterButton.Click += FilterButtonClick;
		SearchButton.Click += SearchButtonClick;
		SearchCombo.PreviewKeyDown += SearchComboPreviewKeyDown;
		SearchCombo.PreviewGotKeyboardFocus += SearchComboPreviewGotKeyboardFocus;
		SearchCombo.PreviewLostKeyboardFocus += SearchComboPreviewLostKeyboardFocus;
		SearchCombo.DropDownOpened += SearchComboDropDownOpened;
		SearchCombo.DropDownClosed += SearchComboDropDownClosed;
	}

	private void SearchComboDropDownClosed(object sender, EventArgs e)
	{
		if (SearchCombo.SelectedIndex == 0)
		{
			OnClearFilter(EventArgs.Empty);
		}
		else if (!string.IsNullOrEmpty(SearchCombo.Text) && SearchCombo.Text != Preference.Wpf.Controls.Properties.Resources.StringSearchDefaultText)
		{
			SearchButton.IsEnabled = true;
			FilterButton.IsEnabled = true;
		}
	}

	private void SearchComboDropDownOpened(object sender, EventArgs e)
	{
		if (SearchCombo.Text == Preference.Wpf.Controls.Properties.Resources.StringSearchDefaultText)
		{
			SearchCombo.Text = string.Empty;
		}
	}

	private void SearchComboPreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
	{
		if (string.IsNullOrEmpty(SearchCombo.Text))
		{
			SearchCombo.Text = Preference.Wpf.Controls.Properties.Resources.StringSearchDefaultText;
		}
	}

	private void SearchComboPreviewGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
	{
		if (SearchCombo.Text == Preference.Wpf.Controls.Properties.Resources.StringSearchDefaultText)
		{
			SearchCombo.Text = string.Empty;
		}
	}

	private void SearchComboPreviewKeyDown(object sender, KeyEventArgs e)
	{
		switch (e.Key)
		{
		case Key.Return:
			if (string.IsNullOrEmpty(SearchCombo.Text))
			{
				OnClearFilter(EventArgs.Empty);
			}
			else if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
			{
				FilterButton.IsChecked = true;
				OnFilter(EventArgs.Empty);
			}
			else
			{
				OnSearch(EventArgs.Empty);
			}
			break;
		case Key.Down:
			SearchCombo.IsDropDownOpen = true;
			break;
		default:
			SearchButton.IsEnabled = true;
			FilterButton.IsEnabled = true;
			break;
		}
	}

	private void InitializeCombo()
	{
		SearchCombo.Text = Preference.Wpf.Controls.Properties.Resources.StringSearchDefaultText;
		ComboBoxItem comboBoxItem = new ComboBoxItem();
		comboBoxItem.Content = Preference.Wpf.Controls.Properties.Resources.StringClearFilterText;
		SearchCombo.Items.Add(comboBoxItem);
	}

	private void SearchButtonClick(object sender, RoutedEventArgs e)
	{
		OnSearch(EventArgs.Empty);
	}

	private void ClearButtonClick(object sender, RoutedEventArgs e)
	{
		FilterButton.IsChecked = false;
		OnClearFilter(EventArgs.Empty);
	}

	private void FilterButtonClick(object sender, RoutedEventArgs e)
	{
		FilterButton.IsChecked = true;
		OnFilter(EventArgs.Empty);
	}

	private void OnFilter(EventArgs e)
	{
		ClearButton.IsEnabled = true;
		this.Filter?.Invoke(this, e);
	}

	private void OnSearch(EventArgs e)
	{
		this.Search?.Invoke(this, e);
	}

	private void OnClearFilter(EventArgs e)
	{
		SearchButton.IsEnabled = false;
		FilterButton.IsEnabled = false;
		ClearButton.IsEnabled = false;
		SearchCombo.Text = string.Empty;
		this.ClearFilter?.Invoke(this, e);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/Preference.Wpf.Controls;component/options%20editor/searchfilteruicontrol.xaml", UriKind.Relative);
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
			SearchCombo = (ComboBox)target;
			break;
		case 2:
			toolBar = (ToolBar)target;
			break;
		case 3:
			SearchButton = (Button)target;
			break;
		case 4:
			FilterButton = (ToggleButton)target;
			break;
		case 5:
			ClearButton = (Button)target;
			break;
		default:
			_contentLoaded = true;
			break;
		}
	}
}
