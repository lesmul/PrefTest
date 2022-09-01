using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using Preference.Wpf.Controls.Properties;

namespace Preference.Wpf.Controls.Options;

[Serializable]
public class ColorsDesigner : Grid, IComponentConnector
{
	private Collection<ColorGalleryItem> _colors;

	private bool _bHasPendingChanges;

	private Collection<string> _colorFamilies;

	internal Label FamilyLabel;

	internal ComboBox ColorsFamiliesCombo;

	internal Label SearchLabel;

	internal SearchFilterUIControl SearchFilterControl;

	internal GalleryControl ColorGallery;

	internal Label InnerColorLabel;

	internal Label OuterColorLabel;

	internal TextBlock InnerColorText;

	internal TextBlock OuterColorText;

	private bool _contentLoaded;

	private CollectionView ColorsView => (CollectionView)CollectionViewSource.GetDefaultView(ColorGallery.Items);

	public Collection<ColorGalleryItem> Colors
	{
		get
		{
			if (_colors == null)
			{
				_colors = new Collection<ColorGalleryItem>();
			}
			return _colors;
		}
		set
		{
			_colors = value;
		}
	}

	public Collection<string> Families
	{
		get
		{
			if (_colorFamilies == null)
			{
				_colorFamilies = new Collection<string>();
			}
			return _colorFamilies;
		}
		set
		{
			_colorFamilies = value;
		}
	}

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

	public GalleryItem SelectedItem => ColorGallery.SelectedItem as GalleryItem;

	public ColorsDesigner()
	{
		InitializeComponent();
		Translate();
		SearchFilterControl.Filter += SearchFilterControlFilter;
		SearchFilterControl.Search += SearchFilterControlSearch;
		SearchFilterControl.ClearFilter += SearchFilterControlClearFilter;
		ColorsFamiliesCombo.SelectionChanged += ColorsFamiliesComboSelectionChanged;
		ColorGallery.SelectionChanged += ColorGallerySelectionChanged;
	}

	public void LoadData()
	{
		if (ColorsFamiliesCombo.Items.Count == 0)
		{
			FillFamiliesCombo();
		}
		if (ColorGallery.Items.Count == 0)
		{
			FillGalleryControl();
		}
	}

	private void Translate()
	{
		FamilyLabel.Content = Preference.Wpf.Controls.Properties.Resources.StringColorFamily;
		FamilyLabel.ToolTip = Preference.Wpf.Controls.Properties.Resources.StringColorFamilyTooltip;
		SearchLabel.Content = Preference.Wpf.Controls.Properties.Resources.StringSearchColor;
		SearchLabel.ToolTip = Preference.Wpf.Controls.Properties.Resources.StringSearchColorTooltip;
		SearchFilterControl.SearchButton.ToolTip = Preference.Wpf.Controls.Properties.Resources.StringSearchColorButtonTooltip;
		SearchFilterControl.FilterButton.ToolTip = Preference.Wpf.Controls.Properties.Resources.StringFilterColorTootlip;
		SearchFilterControl.ClearButton.ToolTip = Preference.Wpf.Controls.Properties.Resources.StringClearFilterColorTootlip;
		InnerColorLabel.Content = Preference.Wpf.Controls.Properties.Resources.StringInnerColor;
		OuterColorLabel.Content = Preference.Wpf.Controls.Properties.Resources.StringOuterColor;
	}

	private void FillGalleryControl()
	{
		Collection<GalleryItem> collection = new Collection<GalleryItem>();
		foreach (ColorGalleryItem color in Colors)
		{
			GalleryItem item = color;
			collection.Add(item);
		}
		ColorGallery.Fill(collection);
	}

	private void FillFamiliesCombo()
	{
		ColorsFamiliesCombo.Items.Clear();
		ComboBoxItem comboBoxItem = new ComboBoxItem();
		comboBoxItem.Content = Preference.Wpf.Controls.Properties.Resources.StringAllFamilies;
		ColorsFamiliesCombo.Items.Add(comboBoxItem);
		foreach (string family in Families)
		{
			ComboBoxItem comboBoxItem2 = new ComboBoxItem();
			comboBoxItem2.Content = family;
			ColorsFamiliesCombo.Items.Add(comboBoxItem2);
		}
		ColorsFamiliesCombo.SelectedIndex = 0;
	}

	private void SearchFilterControlClearFilter(object sender, EventArgs e)
	{
		if (ColorsFamiliesCombo.SelectedIndex == 0)
		{
			ColorGallery.ClearFilter();
		}
		else
		{
			FilterByFamily();
		}
	}

	private void SearchFilterControlSearch(object sender, EventArgs e)
	{
		if (!ColorGallery.Search(SearchFilterControl.SearchCombo.Text))
		{
			MessageBox.Show(Preference.Wpf.Controls.Properties.Resources.WarningNotFound, Preference.Wpf.Controls.Properties.Resources.WarningNotFoundTitle, MessageBoxButton.OK, MessageBoxImage.Exclamation);
			SearchFilterControl.SearchCombo.Focus();
		}
	}

	private void SearchFilterControlFilter(object sender, EventArgs e)
	{
		Filter();
	}

	private void ColorsFamiliesComboSelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		Filter();
		e.Handled = true;
	}

	private void Filter()
	{
		if (ColorsFamiliesCombo.SelectedIndex == 0)
		{
			if (string.IsNullOrEmpty(SearchFilterControl.SearchCombo.Text) || SearchFilterControl.SearchCombo.Text == Preference.Wpf.Controls.Properties.Resources.StringSearchDefaultText)
			{
				ClearFilter();
			}
			else
			{
				ColorGallery.Filter(SearchFilterControl.SearchCombo.Text);
			}
		}
		else if (string.IsNullOrEmpty(SearchFilterControl.SearchCombo.Text) || SearchFilterControl.SearchCombo.Text == Preference.Wpf.Controls.Properties.Resources.StringSearchDefaultText)
		{
			FilterByFamily();
		}
		else
		{
			FilterByFamilyAndText(SearchFilterControl.SearchCombo.Text);
		}
	}

	private void FilterByFamilyAndText(string strToFilter)
	{
		ColorGallery.Items.Filter = delegate(object sender)
		{
			if (!(sender is ColorGalleryItem colorGalleryItem))
			{
				return false;
			}
			return (ColorsFamiliesCombo.SelectedItem is ComboBoxItem comboBoxItem && colorGalleryItem.Family == comboBoxItem.Content.ToString() && colorGalleryItem.Contains(strToFilter)) ? true : false;
		};
	}

	private void FilterByFamily()
	{
		ColorGallery.Items.Filter = delegate(object sender)
		{
			if (!(sender is ColorGalleryItem colorGalleryItem))
			{
				return false;
			}
			return (ColorsFamiliesCombo.SelectedItem is ComboBoxItem comboBoxItem && colorGalleryItem.Family == comboBoxItem.Content.ToString()) ? true : false;
		};
	}

	private void ClearFilter()
	{
		ColorsView.Filter = null;
	}

	private void ColorGallerySelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		HasPendingChanges = true;
		if (ColorGallery.SelectedItem is ColorGalleryItem colorGalleryItem)
		{
			InnerColorText.Text = colorGalleryItem.InnerColor;
			OuterColorText.Text = colorGalleryItem.OuterColor;
		}
		e.Handled = true;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/Preference.Wpf.Controls;component/options%20editor/colorsdesigner.xaml", UriKind.Relative);
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
			FamilyLabel = (Label)target;
			break;
		case 2:
			ColorsFamiliesCombo = (ComboBox)target;
			break;
		case 3:
			SearchLabel = (Label)target;
			break;
		case 4:
			SearchFilterControl = (SearchFilterUIControl)target;
			break;
		case 5:
			ColorGallery = (GalleryControl)target;
			break;
		case 6:
			InnerColorLabel = (Label)target;
			break;
		case 7:
			OuterColorLabel = (Label)target;
			break;
		case 8:
			InnerColorText = (TextBlock)target;
			break;
		case 9:
			OuterColorText = (TextBlock)target;
			break;
		default:
			_contentLoaded = true;
			break;
		}
	}
}
