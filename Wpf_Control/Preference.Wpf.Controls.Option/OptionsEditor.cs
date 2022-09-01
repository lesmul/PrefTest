using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Xml;
using Preference.PrefItems.Clients.Model;
using Preference.Wpf.Controls.Properties;

namespace Preference.Wpf.Controls.Options;

public class OptionsEditor : UserControl, IComponentConnector
{
	private XmlDocument _commandResult;

	internal TabControl OptionsTabControl;

	internal TextBlock OptionsTabHeader;

	internal OptionsDesigner OptionsDesigner;

	internal TextBlock ColorsTabHeader;

	internal ColorsDesigner ColorsDesigner;

	internal TextBlock GlassTabHeader;

	internal GlassDesigner GlassDesigner;

	internal Button btnOk;

	internal Button btnCancel;

	private bool _contentLoaded;

	public bool HasPendingChanges
	{
		get
		{
			if (OptionsDesigner.HasPendingChanges || ColorsDesigner.HasPendingChanges || GlassDesigner.HasPendingChanges)
			{
				return true;
			}
			return false;
		}
	}

	public Collection<TreeItem> OptionsTreeItems
	{
		get
		{
			return OptionsDesigner.Options;
		}
		set
		{
			OptionsDesigner.Options = value;
		}
	}

	public Collection<ColorGalleryItem> Colors
	{
		get
		{
			return ColorsDesigner.Colors;
		}
		set
		{
			ColorsDesigner.Colors = value;
		}
	}

	public Collection<string> ColorsFamilies
	{
		get
		{
			return ColorsDesigner.Families;
		}
		set
		{
			ColorsDesigner.Families = value;
		}
	}

	public GalleryControl ColorsGallery => ColorsDesigner.ColorGallery;

	public XmlDocument CommandResult
	{
		get
		{
			return _commandResult;
		}
		set
		{
			_commandResult = value;
		}
	}

	public OptionsDesigner OptionsControl => OptionsDesigner;

	public ColorsDesigner ColorsControl => ColorsDesigner;

	public GlassDesigner GlassControl => GlassDesigner;

	public Collection<TreeItem> GlassTreeItems
	{
		get
		{
			return GlassDesigner.Glass;
		}
		set
		{
			GlassDesigner.Glass = value;
		}
	}

	public event EventHandler<EventArgs> OkButtonClick;

	public event EventHandler<EventArgs> CancelButtonClick;

	public event EventHandler<TabEventArgs> TabSelectedChanged;

	private void OnOkButtonClick(EventArgs e)
	{
		this.OkButtonClick?.Invoke(this, e);
	}

	private void OnCancelButtonClick(EventArgs e)
	{
		this.CancelButtonClick?.Invoke(this, e);
	}

	private void OnTabSelectedChanged(TabEventArgs e)
	{
		this.TabSelectedChanged?.Invoke(this, e);
	}

	public OptionsEditor()
	{
		InitializeComponent();
		Translate();
		base.Loaded += OptionsEditorLoaded;
	}

	private void Translate()
	{
		btnOk.Content = Preference.Wpf.Controls.Properties.Resources.StringButtonOk;
		btnCancel.Content = Preference.Wpf.Controls.Properties.Resources.StringButtonCancel;
		OptionsTabHeader.Text = Preference.Wpf.Controls.Properties.Resources.StringOptions;
		GlassTabHeader.Text = Preference.Wpf.Controls.Properties.Resources.StringGlass;
		ColorsTabHeader.Text = Preference.Wpf.Controls.Properties.Resources.StringColors;
	}

	private void OkClick(object sender, RoutedEventArgs e)
	{
		XmlDocument xmlDocument = null;
		XmlDocument xmlDocument2 = null;
		XmlDocument xmlDocument3 = null;
		if (OptionsDesigner.OptionsResult != null && OptionsDesigner.OptionsResult.Count > 0)
		{
			xmlDocument = ModelCommandXmlWriter.SetOptions((IDictionary<string, string>)OptionsDesigner.OptionsResult, (IEnumerable<string>)null, true);
		}
		if (ColorsDesigner.SelectedItem != null)
		{
			string itemValue = ColorsDesigner.SelectedItem.ItemValue;
			if (!string.IsNullOrEmpty(itemValue))
			{
				xmlDocument2 = ModelCommandXmlWriter.SetColor(itemValue);
			}
		}
		string selectedGlass = GlassDesigner.SelectedGlass;
		if (!string.IsNullOrEmpty(selectedGlass))
		{
			xmlDocument3 = ModelCommandXmlWriter.SetGlassMaterial(selectedGlass, (IEnumerable<string>)null);
		}
		if (xmlDocument != null || xmlDocument2 != null || xmlDocument3 != null)
		{
			CommandResult = MergeAllCommands(xmlDocument, xmlDocument2, xmlDocument3);
		}
		OnOkButtonClick(EventArgs.Empty);
	}

	private XmlDocument MergeAllCommands(XmlDocument optionsCommnad, XmlDocument colorsCommnad, XmlDocument glassCommand)
	{
		XmlDocument xmlDocument = null;
		XmlNode xmlNode = null;
		XmlNode xmlNode2 = null;
		if (optionsCommnad != null)
		{
			xmlDocument = optionsCommnad;
			if (colorsCommnad != null)
			{
				xmlNode = GetCommandNode(colorsCommnad);
				xmlDocument.FirstChild.AppendChild(xmlDocument.ImportNode(xmlNode, deep: true));
			}
			if (glassCommand != null)
			{
				xmlNode2 = GetCommandNode(glassCommand);
				xmlDocument.FirstChild.AppendChild(xmlDocument.ImportNode(xmlNode2, deep: true));
			}
		}
		else if (colorsCommnad != null)
		{
			xmlDocument = colorsCommnad;
			if (glassCommand != null)
			{
				xmlNode2 = GetCommandNode(glassCommand);
				xmlDocument.FirstChild.AppendChild(xmlDocument.ImportNode(xmlNode2, deep: true));
			}
		}
		else if (glassCommand != null)
		{
			xmlDocument = glassCommand;
		}
		XmlDocument xmlCommands = ModelCommandXmlWriter.Regenerate();
		XmlNode commandNode = GetCommandNode(xmlCommands);
		if (commandNode != null)
		{
			xmlDocument.FirstChild.AppendChild(xmlDocument.ImportNode(commandNode, deep: true));
		}
		return xmlDocument;
	}

	private XmlNode GetCommandNode(XmlDocument xmlCommands)
	{
		XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlCommands.NameTable);
		xmlNamespaceManager.AddNamespace("cmd", "http://www.preference.com/XMLSchemas/2006/PrefCAD.Command");
		return xmlCommands.SelectSingleNode("/cmd:Commands/cmd:Command", xmlNamespaceManager);
	}

	private void CancelClick(object sender, RoutedEventArgs e)
	{
		OnCancelButtonClick(EventArgs.Empty);
	}

	private void TabControlSelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		TabControl tabControl = sender as TabControl;
		TabEventArgs e2 = new TabEventArgs(sender, tabControl.SelectedIndex);
		OnTabSelectedChanged(e2);
		e.Handled = true;
	}

	private void OptionsEditorLoaded(object sender, RoutedEventArgs e)
	{
		OptionsTabControl.SelectedIndex = 0;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/Preference.Wpf.Controls;component/options%20editor/optionseditor.xaml", UriKind.Relative);
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
			OptionsTabControl = (TabControl)target;
			OptionsTabControl.SelectionChanged += TabControlSelectionChanged;
			break;
		case 2:
			OptionsTabHeader = (TextBlock)target;
			break;
		case 3:
			OptionsDesigner = (OptionsDesigner)target;
			break;
		case 4:
			ColorsTabHeader = (TextBlock)target;
			break;
		case 5:
			ColorsDesigner = (ColorsDesigner)target;
			break;
		case 6:
			GlassTabHeader = (TextBlock)target;
			break;
		case 7:
			GlassDesigner = (GlassDesigner)target;
			break;
		case 8:
			btnOk = (Button)target;
			btnOk.Click += OkClick;
			break;
		case 9:
			btnCancel = (Button)target;
			btnCancel.Click += CancelClick;
			break;
		default:
			_contentLoaded = true;
			break;
		}
	}
}
