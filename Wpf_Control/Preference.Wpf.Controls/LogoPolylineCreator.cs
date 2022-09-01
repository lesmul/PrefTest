using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
using System.Xml;
using Preference.Wpf.Controls.Properties;

namespace Preference.Wpf.Controls;

public class LogoPolylineCreator : UserControl, IComponentConnector, IStyleConnector
{
	private string _strXmlCode;

	private Dictionary<string, string> _stringMap;

	internal Grid LayoutRoot;

	internal Viewbox drawingViewbox;

	internal StackPanel PolylineDrawing;

	internal ListView LogoOrderList;

	internal GridView LogoOrderGrid;

	private bool _contentLoaded;

	public string XmlCode
	{
		get
		{
			return _strXmlCode;
		}
		set
		{
			_strXmlCode = value;
			DrawingHelpers.DrawPolyline(PolylineDrawing, drawingViewbox, _strXmlCode, 0, bWidthDimension: false);
			drawingViewbox.UpdateLayout();
			FillLogoOrderList(_strXmlCode);
		}
	}

	public LogoPolylineCreator()
	{
		_stringMap = new Dictionary<string, string>();
		_strXmlCode = "";
		InitializeComponent();
		_stringMap["LogoOrder"] = Preference.Wpf.Controls.Properties.Resources.ResourceManager.GetString("LogoOrder");
		_stringMap["Parameter"] = Preference.Wpf.Controls.Properties.Resources.ResourceManager.GetString("Parameter");
		UpdateGridHeader();
		List<LogoOrder> itemsSource = new List<LogoOrder>();
		LogoOrderList.ItemsSource = itemsSource;
		AddRow();
	}

	public void SetResourceString(string strResource, string strStringResource)
	{
		_stringMap[strResource] = strStringResource;
		UpdateGridHeader();
	}

	private void UpdateGridHeader()
	{
		LogoOrderGrid.Columns[0].Header = _stringMap["LogoOrder"];
		LogoOrderGrid.Columns[1].Header = _stringMap["Parameter"];
	}

	private void FillLogoOrderList(string strContent)
	{
		List<LogoOrder> list = new List<LogoOrder>();
		XmlDocument xmlDocument = new XmlDocument();
		if (strContent != "")
		{
			xmlDocument.LoadXml(strContent);
		}
		XmlNode xmlNode = xmlDocument.SelectSingleNode(XMLHelpers.LOGOXML_ELEMENT_POLYLINE2D);
		if (xmlNode != null)
		{
			XmlNode xmlNode2 = xmlNode.SelectSingleNode(XMLHelpers.LOGOXML_ELEMENT_LOGOCODE);
			if (xmlNode2 != null)
			{
				foreach (XmlNode childNode in xmlNode2.ChildNodes)
				{
					if (childNode != null)
					{
						XmlNode namedItem = childNode.Attributes.GetNamedItem(XMLHelpers.LOGOXML_ATTR_PARAMETER);
						if (namedItem != null)
						{
							LogoOrder logoOrder = new LogoOrder();
							logoOrder.m_strOrder = childNode.Name;
							logoOrder.m_dParameter = Convert.ToDouble(namedItem.Value, NumberFormatInfo.InvariantInfo);
							list.Add(logoOrder);
						}
					}
				}
			}
		}
		LogoOrderList.ItemsSource = null;
		LogoOrderList.SelectedIndex = -1;
		LogoOrderList.ItemsSource = list;
	}

	private void LogoChanged()
	{
		XmlDocument xmlDocument = new XmlDocument();
		XmlElement xmlElement = xmlDocument.CreateElement(XMLHelpers.LOGOXML_ELEMENT_POLYLINE2D);
		xmlDocument.AppendChild(xmlElement);
		XmlElement xmlElement2 = xmlDocument.CreateElement(XMLHelpers.LOGOXML_ELEMENT_LOGOCODE);
		xmlElement.AppendChild(xmlElement2);
		List<LogoOrder> list = (List<LogoOrder>)LogoOrderList.ItemsSource;
		foreach (LogoOrder item in list)
		{
			string text = "";
			if (item.m_strOrder == "BK")
			{
				text = XMLHelpers.LOGOXML_ELEMENT_BK;
			}
			if (item.m_strOrder == "FD")
			{
				text = XMLHelpers.LOGOXML_ELEMENT_FD;
			}
			if (item.m_strOrder == "LT")
			{
				text = XMLHelpers.LOGOXML_ELEMENT_LT;
			}
			if (item.m_strOrder == "RT")
			{
				text = XMLHelpers.LOGOXML_ELEMENT_RT;
			}
			if (text != "")
			{
				XmlElement xmlElement3 = xmlDocument.CreateElement(text);
				xmlElement2.AppendChild(xmlElement3);
				string value = Convert.ToString(item.m_dParameter, NumberFormatInfo.InvariantInfo);
				xmlElement3.SetAttribute(XMLHelpers.LOGOXML_ATTR_PARAMETER, value);
			}
		}
		_strXmlCode = xmlDocument.InnerXml;
		DrawingHelpers.DrawPolyline(PolylineDrawing, drawingViewbox, xmlDocument.InnerXml, 0, bWidthDimension: false);
	}

	private void AddRow()
	{
		List<LogoOrder> list = (List<LogoOrder>)LogoOrderList.ItemsSource;
		list.Add(new LogoOrder());
		CollectionView collectionView = (CollectionView)CollectionViewSource.GetDefaultView(LogoOrderList.ItemsSource);
		collectionView.MoveCurrentToPosition(LogoOrderList.Items.Count - 1);
		collectionView.Refresh();
		SelectItem(LogoOrderList.Items.Count - 1);
	}

	private void DeleteRow()
	{
		if (LogoOrderList.SelectedIndex != -1)
		{
			int selectedIndex = LogoOrderList.SelectedIndex;
			List<LogoOrder> list = (List<LogoOrder>)LogoOrderList.ItemsSource;
			list.RemoveAt(selectedIndex);
			CollectionView collectionView = (CollectionView)CollectionViewSource.GetDefaultView(LogoOrderList.ItemsSource);
			collectionView.MoveCurrentToPosition(selectedIndex);
			collectionView.Refresh();
			SelectItem(selectedIndex);
			LogoChanged();
		}
	}

	private void InsertRow()
	{
		List<LogoOrder> list = (List<LogoOrder>)LogoOrderList.ItemsSource;
		int selectedIndex = LogoOrderList.SelectedIndex;
		if (LogoOrderList.SelectedIndex == -1)
		{
			AddRow();
			return;
		}
		list.Insert(LogoOrderList.SelectedIndex, new LogoOrder());
		CollectionView collectionView = (CollectionView)CollectionViewSource.GetDefaultView(LogoOrderList.ItemsSource);
		collectionView.MoveCurrentToPosition(selectedIndex);
		collectionView.Refresh();
		SelectItem(selectedIndex);
	}

	private void SelectItem(int nSelectedItem)
	{
		if (nSelectedItem >= LogoOrderList.Items.Count)
		{
			nSelectedItem = LogoOrderList.Items.Count - 1;
		}
		if (nSelectedItem != -1)
		{
			ListViewItem listViewItem = (ListViewItem)LogoOrderList.ItemContainerGenerator.ContainerFromItem(LogoOrderList.Items[nSelectedItem]);
			if (listViewItem == null)
			{
				LogoOrderList.ScrollIntoView(LogoOrderList.Items[nSelectedItem]);
				listViewItem = (ListViewItem)LogoOrderList.ItemContainerGenerator.ContainerFromItem(LogoOrderList.Items[nSelectedItem]);
			}
			listViewItem?.Focus();
		}
	}

	private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		LogoChanged();
	}

	private void NumericTextBox_TextChanged(object sender, TextChangedEventArgs e)
	{
		LogoChanged();
	}

	private void LogoOrderList_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.Key == Key.Insert)
		{
			InsertRow();
		}
		if (e.Key == Key.Delete)
		{
			DeleteRow();
		}
		if (e.Key == Key.Down && (LogoOrderList.SelectedIndex == -1 || LogoOrderList.SelectedIndex == LogoOrderList.Items.Count - 1))
		{
			AddRow();
		}
	}

	private void drawingViewbox_SizeChanged(object sender, SizeChangedEventArgs e)
	{
		DrawingHelpers.DrawPolyline(PolylineDrawing, drawingViewbox, _strXmlCode, 0, bWidthDimension: false);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/Preference.Wpf.Controls;component/logopolylinecreator.xaml", UriKind.Relative);
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
		case 2:
			LayoutRoot = (Grid)target;
			break;
		case 3:
			drawingViewbox = (Viewbox)target;
			drawingViewbox.SizeChanged += drawingViewbox_SizeChanged;
			break;
		case 4:
			PolylineDrawing = (StackPanel)target;
			break;
		case 5:
			LogoOrderList = (ListView)target;
			LogoOrderList.KeyDown += LogoOrderList_KeyDown;
			break;
		case 6:
			LogoOrderGrid = (GridView)target;
			break;
		default:
			_contentLoaded = true;
			break;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	void IStyleConnector.Connect(int connectionId, object target)
	{
		if (connectionId == 1)
		{
			((ComboBox)target).SelectionChanged += ComboBox_SelectionChanged;
		}
	}
}
