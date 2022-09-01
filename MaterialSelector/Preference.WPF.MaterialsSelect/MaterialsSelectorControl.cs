using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
using System.Xml;
using Preference.Diagnostics;
using Preference.WPF.MaterialsSelector.Core;
using Preference.WPF.MaterialsSelector.Presenters;
using Preference.WPF.MaterialsSelector.Properties;
using Preference.WPF.MaterialsSelector.Settings;
using Preference.WPF.MaterialsSelector.UsersControls;
using Telerik.Windows.Controls;

namespace Preference.WPF.MaterialsSelector;

public class MaterialsSelectorControl : UserControl, IMaterialsSelectorControl, IComponentConnector
{
	private Window _Window;

	private Binding _headerBinding;

	private DataTemplate _dt;

	private Binding _MaterialHeaderBinding;

	private DataTemplate _dtMaterialHeaderBinding;

	internal RadDocking radDocking;

	internal RadPaneGroup GroupPane;

	internal RadPane GeneralPane;

	internal RadPane ItemsPane;

	internal ItemsPane itemsPane;

	internal RadPane ModelPane;

	internal RadPane SelectedItemPane;

	internal RadPane MaterialsPane;

	internal MaterialsPane materialsPane;

	internal RadPane MaterialDetailPane;

	private bool _contentLoaded;

	public Window window
	{
		set
		{
			_Window = value;
		}
	}

	public int UnitsMode
	{
		get
		{
			return Convert.ToInt32((base.DataContext as MaterialSelectorPresenter).ServiceAgent.UnitsMode);
		}
		set
		{
			if (base.DataContext is MaterialSelectorPresenter)
			{
				(base.DataContext as MaterialSelectorPresenter).ServiceAgent.UnitsMode = (UnitsMode)value;
			}
		}
	}

	public string ConnectionString
	{
		get
		{
			return (base.DataContext as MaterialSelectorPresenter).ServiceAgent.ConnectionString;
		}
		set
		{
			(base.DataContext as MaterialSelectorPresenter).ServiceAgent.ConnectionString = value;
		}
	}

	public bool ReadOnly
	{
		get
		{
			return (base.DataContext as MaterialSelectorPresenter).ReadOnly;
		}
		set
		{
			(base.DataContext as MaterialSelectorPresenter).ReadOnly = value;
		}
	}

	public string XmlDescriptive
	{
		get
		{
			return (base.DataContext as MaterialSelectorPresenter).ServiceAgent.XmlDescriptive;
		}
		set
		{
			if (base.DataContext is MaterialSelectorPresenter)
			{
				MaterialSelectorPresenter obj = base.DataContext as MaterialSelectorPresenter;
				obj.ServiceAgent.XmlDescriptive = value;
				obj.Refresh.Execute(null);
			}
		}
	}

	public string DescriptiveXmlStructureNodeDocumentXml => (base.DataContext as MaterialSelectorPresenter).DescriptiveXmlStructureNodeDocumentXml;

	public string FilteredXmlDescriptive => (base.DataContext as MaterialSelectorPresenter).GetXmlDescriptiveFiltered();

	public string FilterXml
	{
		get
		{
			return (base.DataContext as MaterialSelectorPresenter).GetXmlFilter();
		}
		set
		{
			(base.DataContext as MaterialSelectorPresenter).SetXmlFilter(value);
		}
	}

	public Collection<string> SelectedMaterials
	{
		get
		{
			if (base.DataContext is MaterialSelectorPresenter)
			{
				return (base.DataContext as MaterialSelectorPresenter).SelectedMaterials;
			}
			return null;
		}
	}

	public MaterialsSelectorControl()
	{
		InitializeComponent();
		base.Language = XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag);
		base.Unloaded += MaterialsSelectorControl_Unloaded;
		base.DataContext = new MaterialSelectorPresenter(new ServiceAgent(), base.Dispatcher, this);
		LoadSettings();
	}

	private void MaterialsSelectorControl_Unloaded(object sender, RoutedEventArgs e)
	{
		SaveSettings();
	}

	public void ResetSettings()
	{
		try
		{
			string text = "<?xml version='1.0' encoding='utf-8'?>";
			text += "<RadDocking>";
			text += "\t<DocumentHost>";
			text += "\t\t<RadSplitContainer>";
			text += "\t\t\t<Items>";
			text += "\t\t\t\t<RadPaneGroup SerializationTag='PaneGroup1' SelectedIndex='0'>";
			text += "\t\t\t\t\t<Items>";
			text += string.Format("\t\t\t\t\t\t<RadPane SerializationTag='MaterialsPane' IsDockable='True' Title='{0}' Header='{0}' />", Preference.WPF.MaterialsSelector.Properties.Resources.Materials);
			text += "\t\t\t\t\t</Items>";
			text += "\t\t\t\t</RadPaneGroup>";
			text += "\t\t\t</Items>";
			text += "\t\t</RadSplitContainer>";
			text += "\t</DocumentHost>";
			text += "\t<SplitContainers>";
			text += "\t\t<RadSplitContainer Dock='DockedRight' Width='453' RelativeWidth='100' RelativeHeight='100' IsAutoGenerated='True' Orientation='Vertical'>";
			text += "\t\t\t<Items>";
			text += "\t\t\t\t<RadPaneGroup SerializationTag='PaneGroup2' RelativeWidth='100' RelativeHeight='100' IsAutoGenerated='True' SelectedIndex='0'>";
			text += "\t\t\t\t\t<Items>";
			text += string.Format("\t\t\t\t\t\t<RadPane SerializationTag='ItemsPane'  IsDockable='True' Title='{0}' Header='{0}' />", Preference.WPF.MaterialsSelector.Properties.Resources.Items);
			text += "\t\t\t\t\t</Items>";
			text += "\t\t\t\t</RadPaneGroup>";
			text += "\t\t\t\t<RadPaneGroup SerializationTag='PaneGroup3' RelativeWidth='100' RelativeHeight='100' IsAutoGenerated='True' SelectedIndex='0'>";
			text += "\t\t\t\t\t<Items>";
			text += string.Format("\t\t\t\t\t\t<RadPane SerializationTag='ModelPane'  IsDockable='True' Title='{0}' Header='{0}' />", Preference.WPF.MaterialsSelector.Properties.Resources.Model);
			text += "\t\t\t\t\t</Items>";
			text += "\t\t\t\t</RadPaneGroup>";
			text += "\t\t\t\t<RadPaneGroup SerializationTag='PaneGroup4' RelativeWidth='100' RelativeHeight='100' IsAutoGenerated='True' SelectedIndex='0'>";
			text += "\t\t\t\t\t<Items>";
			text += "\t\t\t\t\t\t<RadPane SerializationTag='SelectedItemPane' IsDockable='True' />";
			text += "\t\t\t\t\t</Items>";
			text += "\t\t\t\t</RadPaneGroup>";
			text += "\t\t\t</Items>";
			text += "\t\t</RadSplitContainer>";
			text += "\t\t<RadSplitContainer Dock='DockedLeft' Width='240' RelativeWidth='100' RelativeHeight='100' IsAutoGenerated='True' Orientation='Vertical'>";
			text += "\t\t\t<Items>";
			text += "\t\t\t\t<RadPaneGroup SerializationTag='PaneGroup5' RelativeWidth='100' RelativeHeight='100' IsAutoGenerated='True' SelectedIndex='0'>";
			text += "\t\t\t\t\t<Items>";
			text += string.Format("\t\t\t\t\t\t<RadPane SerializationTag='GeneralPane' IsDockable='True' Title='{0}' Header='{0}' />", Preference.WPF.MaterialsSelector.Properties.Resources.General);
			text += "\t\t\t\t\t</Items>";
			text += "\t\t\t\t</RadPaneGroup>";
			text += "\t\t\t\t<RadPaneGroup SerializationTag='PaneGroup6' RelativeWidth='100' RelativeHeight='100' IsAutoGenerated='True' SelectedIndex='0'>";
			text += "\t\t\t\t\t<Items>";
			text += string.Format("\t\t\t\t\t\t<RadPane SerializationTag='MaterialDetailPane' IsDockable='True' Title='{0}' Header='{0}' />", Preference.WPF.MaterialsSelector.Properties.Resources.Material);
			text += "\t\t\t\t\t</Items>";
			text += "\t\t\t\t</RadPaneGroup>";
			text += "\t\t\t</Items>";
			text += "\t\t</RadSplitContainer>";
			text += "\t</SplitContainers>";
			text += "</RadDocking>";
			MemoryStream memoryStream = new MemoryStream();
			StreamWriter streamWriter = new StreamWriter(memoryStream);
			streamWriter.Write(text);
			streamWriter.Flush();
			memoryStream.Seek(0L, SeekOrigin.Begin);
			radDocking.LoadLayout((Stream)memoryStream);
			SetBindings();
		}
		catch
		{
		}
	}

	private void ChangeRadPaneHeader(XmlDocument xmldoc, string serializationTag, string header)
	{
		string xpath = $"/descendant::RadPane[@SerializationTag = '{serializationTag}']";
		XmlNode xmlNode = xmldoc.SelectSingleNode(xpath);
		if (xmlNode.Attributes.GetNamedItem("Title") != null)
		{
			xmlNode.Attributes.GetNamedItem("Title").Value = header;
		}
		if (xmlNode.Attributes.GetNamedItem("Header") != null)
		{
			xmlNode.Attributes.GetNamedItem("Header").Value = header;
		}
	}

	public void LoadSettings()
	{
		try
		{
			string dockLayout = new MaterialSelectorControlSettings().GetDockLayout();
			if (!string.IsNullOrEmpty(dockLayout))
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.LoadXml(dockLayout);
				ChangeRadPaneHeader(xmlDocument, "MaterialsPane", Preference.WPF.MaterialsSelector.Properties.Resources.Materials);
				ChangeRadPaneHeader(xmlDocument, "ItemsPane", Preference.WPF.MaterialsSelector.Properties.Resources.Items);
				ChangeRadPaneHeader(xmlDocument, "ModelPane", Preference.WPF.MaterialsSelector.Properties.Resources.Model);
				ChangeRadPaneHeader(xmlDocument, "GeneralPane", Preference.WPF.MaterialsSelector.Properties.Resources.General);
				ChangeRadPaneHeader(xmlDocument, "MaterialDetailPane", Preference.WPF.MaterialsSelector.Properties.Resources.Material);
				dockLayout = xmlDocument.OuterXml;
				MemoryStream memoryStream = new MemoryStream();
				StreamWriter streamWriter = new StreamWriter(memoryStream);
				streamWriter.Write(dockLayout);
				streamWriter.Flush();
				memoryStream.Seek(0L, SeekOrigin.Begin);
				radDocking.LoadLayout((Stream)memoryStream);
				SetBindings();
			}
			else
			{
				ResetSettings();
			}
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefGest");
			ResetSettings();
		}
	}

	private void SetBindings()
	{
		_headerBinding = new Binding("SelectedItem");
		((FrameworkElement)(object)SelectedItemPane).SetBinding(HeaderedContentControl.HeaderProperty, (BindingBase)_headerBinding);
		((FrameworkElement)(object)SelectedItemPane).SetBinding(RadPane.TitleProperty, (BindingBase)_headerBinding);
		_dt = (DataTemplate)TryFindResource("SelectedItemPaneTemplate");
		SelectedItemPane.set_TitleTemplate(_dt);
		((HeaderedContentControl)(object)SelectedItemPane).HeaderTemplate = _dt;
		_MaterialHeaderBinding = new Binding("SelectedMaterial");
		((FrameworkElement)(object)MaterialDetailPane).SetBinding(HeaderedContentControl.HeaderProperty, (BindingBase)_MaterialHeaderBinding);
		((FrameworkElement)(object)MaterialDetailPane).SetBinding(RadPane.TitleProperty, (BindingBase)_MaterialHeaderBinding);
		_dtMaterialHeaderBinding = (DataTemplate)TryFindResource("SelectedMaterialPaneTemplate");
		MaterialDetailPane.set_TitleTemplate(_dtMaterialHeaderBinding);
		((HeaderedContentControl)(object)MaterialDetailPane).HeaderTemplate = _dtMaterialHeaderBinding;
	}

	public void SaveSettings()
	{
		try
		{
			MemoryStream memoryStream = new MemoryStream();
			radDocking.SaveLayout((Stream)memoryStream);
			memoryStream.Position = 0L;
			string xml = new StreamReader(memoryStream).ReadToEnd();
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(xml);
			string xpath = "descendant::RadPaneGroup";
			XmlNodeList xmlNodeList = xmlDocument.SelectNodes(xpath);
			int num = 1;
			foreach (XmlNode item in xmlNodeList)
			{
				if (item.Attributes.GetNamedItem("SerializationTag") == null)
				{
					XmlAttribute xmlAttribute = xmlDocument.CreateAttribute("SerializationTag");
					xmlAttribute.Value = $"PaneGroup{num}";
					item.Attributes.SetNamedItem(xmlAttribute);
				}
				else
				{
					item.Attributes.GetNamedItem("SerializationTag").Value = $"PaneGroup{num}";
				}
				num++;
			}
			xml = xmlDocument.OuterXml;
			new MaterialSelectorControlSettings().SetDockLayout(xml);
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefGest");
			ResetSettings();
		}
	}

	public void RebindMaterials()
	{
		materialsPane.Rebind();
	}

	public void ScrollIntoViewItem(object item)
	{
		itemsPane.ScrollIntoViewItem(item);
	}

	public void ResetItems()
	{
		itemsPane.SubscribeDataLoadedEvent();
	}

	public void ShowCursorWait(bool show)
	{
		if (show)
		{
			Mouse.OverrideCursor = Cursors.Wait;
		}
		else
		{
			Mouse.OverrideCursor = null;
		}
	}

	public void ViewGeneralPanel()
	{
		GeneralPane.set_IsHidden(false);
		GeneralPane.MakeDockable();
		GeneralPane.set_IsPinned(true);
	}

	public void ViewItemsPanel()
	{
		ItemsPane.set_IsHidden(false);
		ItemsPane.MakeDockable();
		ItemsPane.set_IsPinned(true);
	}

	public void ViewModelPanel()
	{
		ModelPane.set_IsHidden(false);
		ModelPane.MakeDockable();
		ModelPane.set_IsPinned(true);
	}

	public void ViewItemPanel()
	{
		SelectedItemPane.set_IsHidden(false);
		SelectedItemPane.MakeDockable();
		SelectedItemPane.set_IsPinned(true);
	}

	public void ViewMaterialsPanel()
	{
		MaterialsPane.set_IsHidden(false);
		MaterialsPane.MakeDockable();
		MaterialsPane.set_IsPinned(true);
	}

	public void ViewMaterialPanel()
	{
		MaterialDetailPane.set_IsHidden(false);
		MaterialDetailPane.MakeDockable();
		MaterialDetailPane.set_IsPinned(true);
	}

	public void ResetWindowLayout()
	{
		try
		{
			if (MessageBox.Show(Preference.WPF.MaterialsSelector.Properties.Resources.ConfirmResetWindowLayout, Preference.WPF.MaterialsSelector.Properties.Resources.Confirm, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) != MessageBoxResult.No)
			{
				ResetSettings();
			}
		}
		catch
		{
		}
	}

	public void InformAboutWrongFilter(EventHandler<WindowClosedEventArgs> closed)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Expected O, but got Unknown
		DialogParameters val = new DialogParameters();
		val.set_Closed(closed);
		val.set_Owner((ContentControl)_Window);
		val.set_DialogStartupLocation(WindowStartupLocation.CenterOwner);
		val.set_Content((object)Preference.WPF.MaterialsSelector.Properties.Resources.WrongFilter);
		RadWindow.Alert(val);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/Preference.WPF.MaterialsSelector;component/materialsselectorcontrol.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	void IComponentConnector.Connect(int connectionId, object target)
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Expected O, but got Unknown
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Expected O, but got Unknown
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Expected O, but got Unknown
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Expected O, but got Unknown
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Expected O, but got Unknown
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Expected O, but got Unknown
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Expected O, but got Unknown
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Expected O, but got Unknown
		switch (connectionId)
		{
		case 1:
			radDocking = (RadDocking)target;
			break;
		case 2:
			GroupPane = (RadPaneGroup)target;
			break;
		case 3:
			GeneralPane = (RadPane)target;
			break;
		case 4:
			ItemsPane = (RadPane)target;
			break;
		case 5:
			itemsPane = (ItemsPane)target;
			break;
		case 6:
			ModelPane = (RadPane)target;
			break;
		case 7:
			SelectedItemPane = (RadPane)target;
			break;
		case 8:
			MaterialsPane = (RadPane)target;
			break;
		case 9:
			materialsPane = (MaterialsPane)target;
			break;
		case 10:
			MaterialDetailPane = (RadPane)target;
			break;
		default:
			_contentLoaded = true;
			break;
		}
	}
}
