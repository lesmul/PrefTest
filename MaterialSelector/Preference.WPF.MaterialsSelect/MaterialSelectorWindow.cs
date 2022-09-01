using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;
using Preference.Sql;
using Preference.WPF.MaterialsSelector.Settings;
using Telerik.Windows.Controls;

namespace Preference.WPF.MaterialsSelector;

public class MaterialSelectorWindow : Window, IComponentConnector
{
	internal MaterialsSelectorControl MaterialSelector;

	internal RadButton OK;

	internal RadButton Cancel;

	private bool _contentLoaded;

	public int Number { get; set; }

	public int Version { get; set; }

	public int Position { get; set; }

	public string Filter { get; set; }

	public string ConnectionString { get; set; }

	public int UnitsMode { get; set; }

	public bool ReadOnly { get; set; }

	public MaterialSelectorWindow()
	{
		InitializeComponent();
		base.Loaded += MaterialSelectorWindow_Loaded;
	}

	private void MaterialSelectorWindow_Loaded(object sender, RoutedEventArgs e)
	{
		if (new MaterialSelectorWindowSettings().TryGetWindowSettings(out var top, out var left, out var height, out var width, out var windowState))
		{
			base.Width = width;
			base.Height = height;
			base.Left = left;
			base.Top = top;
			base.WindowState = windowState;
		}
		MaterialSelector.LoadSettings();
		MaterialSelector.window = this;
	}

	protected override void OnClosed(EventArgs e)
	{
		new MaterialSelectorWindowSettings().SetWindowSettings(base.Top, base.Left, base.Height, base.Width, base.WindowState);
		MaterialSelector.SaveSettings();
		base.OnClosed(e);
	}

	public void Load(string descriptiveXML)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Expected O, but got Unknown
		ConnectionString val = new ConnectionString(ConnectionString);
		MaterialSelector.ConnectionString = val.get_AdoNet();
		MaterialSelector.UnitsMode = UnitsMode;
		MaterialSelector.XmlDescriptive = descriptiveXML;
		if (!string.IsNullOrWhiteSpace(Filter))
		{
			MaterialSelector.FilterXml = Filter;
		}
		MaterialSelector.ReadOnly = ReadOnly;
		((UIElement)(object)OK).IsEnabled = !ReadOnly;
	}

	private void OK_Click(object sender, RoutedEventArgs e)
	{
		Filter = MaterialSelector.FilterXml;
		base.DialogResult = true;
		Close();
	}

	private void Cancel_Click(object sender, RoutedEventArgs e)
	{
		base.DialogResult = false;
		Close();
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/Preference.WPF.MaterialsSelector;component/materialselectorwindow.xaml", UriKind.Relative);
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
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Expected O, but got Unknown
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Expected O, but got Unknown
		switch (connectionId)
		{
		case 1:
			MaterialSelector = (MaterialsSelectorControl)target;
			break;
		case 2:
			OK = (RadButton)target;
			((ButtonBase)(object)OK).Click += OK_Click;
			break;
		case 3:
			Cancel = (RadButton)target;
			((ButtonBase)(object)Cancel).Click += Cancel_Click;
			break;
		default:
			_contentLoaded = true;
			break;
		}
	}
}
