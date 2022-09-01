using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Shapes;
using System.Xml;
using Preference.Diagnostics;

namespace Preference.WPF.MaterialsSelector.UsersControls;

public class DrawingViewer : UserControl, IComponentConnector
{
	private Canvas _Canvas;

	private Canvas _currentSelectedCanvas1;

	private Canvas _currentSelectedCanvas2;

	public static readonly DependencyProperty DrawingProperty = DependencyProperty.Register("Drawing", typeof(string), typeof(DrawingViewer), new PropertyMetadata("", DrawingChanged));

	public static readonly DependencyProperty SelectedIdentifierProperty = DependencyProperty.Register("SelectedIdentifier", typeof(string), typeof(DrawingViewer));

	public static readonly DependencyProperty SelectedIdentifiers1Property = DependencyProperty.Register("SelectedIdentifiers1", typeof(ObservableCollection<string>), typeof(DrawingViewer), new PropertyMetadata(null, SelectedIdentifiers1Changed));

	public static readonly DependencyProperty SelectionColor1Property = DependencyProperty.Register("SelectionColor1", typeof(string), typeof(DrawingViewer), new PropertyMetadata("#1111FF33"));

	public static readonly DependencyProperty SelectedIdentifiers2Property = DependencyProperty.Register("SelectedIdentifiers2", typeof(ObservableCollection<string>), typeof(DrawingViewer), new PropertyMetadata(null, SelectedIdentifiers2Changed));

	public static readonly DependencyProperty SelectionColor2Property = DependencyProperty.Register("SelectionColor2", typeof(string), typeof(DrawingViewer), new PropertyMetadata("#11FF8800"));

	internal Grid LayoutRoot;

	internal Canvas DrawingContent;

	private bool _contentLoaded;

	public string Drawing
	{
		get
		{
			return (string)GetValue(DrawingProperty);
		}
		set
		{
			SetValue(DrawingProperty, value);
		}
	}

	public string SelectedIdentifier
	{
		get
		{
			return (string)GetValue(SelectedIdentifierProperty);
		}
		set
		{
			SetValue(SelectedIdentifierProperty, value);
		}
	}

	public ObservableCollection<string> SelectedIdentifiers1
	{
		get
		{
			return (ObservableCollection<string>)GetValue(SelectedIdentifiers1Property);
		}
		set
		{
			SetValue(SelectedIdentifiers1Property, value);
		}
	}

	public string SelectionColor1
	{
		get
		{
			return (string)GetValue(SelectionColor1Property);
		}
		set
		{
			SetValue(SelectionColor1Property, value);
		}
	}

	public ObservableCollection<string> SelectedIdentifiers2
	{
		get
		{
			return (ObservableCollection<string>)GetValue(SelectedIdentifiers2Property);
		}
		set
		{
			SetValue(SelectedIdentifiers2Property, value);
		}
	}

	public string SelectionColor2
	{
		get
		{
			return (string)GetValue(SelectionColor2Property);
		}
		set
		{
			SetValue(SelectionColor2Property, value);
		}
	}

	public DrawingViewer()
	{
		InitializeComponent();
	}

	private static void DrawingChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
	{
		if (sender is DrawingViewer drawingViewer)
		{
			object newValue = e.NewValue;
			if (newValue != null && newValue is string)
			{
				drawingViewer.RenderModel(newValue as string);
			}
		}
	}

	private static void SelectedIdentifiers1Changed(DependencyObject sender, DependencyPropertyChangedEventArgs e)
	{
		if (sender is DrawingViewer @object)
		{
			if (e.OldValue is ObservableCollection<string> observableCollection)
			{
				observableCollection.CollectionChanged -= @object.OnSelectedIdentifiers1Changed;
			}
			if (e.NewValue is ObservableCollection<string> observableCollection2)
			{
				observableCollection2.CollectionChanged += @object.OnSelectedIdentifiers1Changed;
			}
			_ = e.NewValue;
		}
	}

	private static void SelectedIdentifiers2Changed(DependencyObject sender, DependencyPropertyChangedEventArgs e)
	{
		if (sender is DrawingViewer @object)
		{
			if (e.OldValue is ObservableCollection<string> observableCollection)
			{
				observableCollection.CollectionChanged -= @object.OnSelectedIdentifiers2Changed;
			}
			if (e.NewValue is ObservableCollection<string> observableCollection2)
			{
				observableCollection2.CollectionChanged += @object.OnSelectedIdentifiers2Changed;
			}
			_ = e.NewValue;
		}
	}

	private void OnSelectedIdentifiers1Changed(object sender, NotifyCollectionChangedEventArgs e)
	{
		if (e.Action == NotifyCollectionChangedAction.Reset)
		{
			if (_currentSelectedCanvas1 != null)
			{
				(_Canvas.Children[0] as Canvas).Children.Remove(_currentSelectedCanvas1);
			}
			_currentSelectedCanvas1 = null;
		}
		if (e.NewItems != null)
		{
			RenderSelectedIdentifiers(sender as ObservableCollection<string>, SelectionColor1, ref _currentSelectedCanvas1);
		}
		_ = e.OldItems;
	}

	private void OnSelectedIdentifiers2Changed(object sender, NotifyCollectionChangedEventArgs e)
	{
		if (e.Action == NotifyCollectionChangedAction.Reset)
		{
			if (_currentSelectedCanvas2 != null)
			{
				(_Canvas.Children[0] as Canvas).Children.Remove(_currentSelectedCanvas2);
			}
			_currentSelectedCanvas2 = null;
		}
		if (e.NewItems != null)
		{
			RenderSelectedIdentifiers(sender as ObservableCollection<string>, SelectionColor2, ref _currentSelectedCanvas2);
		}
		_ = e.OldItems;
	}

	private void RenderModel(string xaml)
	{
		try
		{
			DrawingContent.Children.Clear();
			if (string.IsNullOrEmpty(xaml))
			{
				return;
			}
			xaml = GetXamlSynchronous(xaml);
			object obj = XamlReader.Parse(xaml);
			_Canvas = obj as Canvas;
			DrawingContent.Children.Add(_Canvas);
			if (_Canvas.Children[0] is Canvas canvas)
			{
				foreach (UIElement child in canvas.Children)
				{
					if (child is Canvas)
					{
						Canvas canvas2 = child as Canvas;
						if (canvas2.Tag != null && canvas2.Children.Count > 0)
						{
							canvas2.MouseLeftButtonUp += canvas_MouseLeftButtonUp;
						}
					}
				}
			}
			DrawingContent.Width = _Canvas.Width;
			DrawingContent.Height = _Canvas.Height;
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefGest");
			throw;
		}
	}

	private void RenderSelectedIdentifiers(ObservableCollection<string> items, string selectionColor, ref Canvas selectedCanvas)
	{
		try
		{
			if (_Canvas == null)
			{
				return;
			}
			if (selectedCanvas != null)
			{
				(_Canvas.Children[0] as Canvas).Children.Remove(selectedCanvas);
			}
			selectedCanvas = null;
			if (items == null || items.Count == 0)
			{
				return;
			}
			selectedCanvas = new Canvas();
			selectedCanvas.MouseLeftButtonUp += canvas_MouseLeftButtonUp;
			(_Canvas.Children[0] as Canvas).Children.Add(selectedCanvas);
			foreach (Canvas canvase in GetCanvases(items))
			{
				SelectCanvas(canvase, selectionColor, selectedCanvas);
			}
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefGest");
			throw;
		}
	}

	private Collection<Canvas> GetCanvases(ObservableCollection<string> items)
	{
		if (items == null)
		{
			return null;
		}
		Collection<Canvas> collection = new Collection<Canvas>();
		if (DrawingContent.Children.Count > 0)
		{
			Canvas canvas = _Canvas.Children[0] as Canvas;
			for (int i = 0; i < canvas.Children.Count; i++)
			{
				if (!(canvas.Children[i] is Canvas canvas2))
				{
					continue;
				}
				foreach (string item in items)
				{
					if (GetCanvasId(canvas2) == item)
					{
						collection.Add(canvas2);
						break;
					}
				}
			}
		}
		return collection;
	}

	private void canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
	{
		if (sender is Canvas)
		{
			SelectedIdentifier = GetCanvasId(sender as Canvas);
		}
	}

	private static string GetCanvasId(Canvas canvas)
	{
		if (canvas == null)
		{
			return string.Empty;
		}
		if (canvas.Tag == null)
		{
			return string.Empty;
		}
		if (!(canvas.Tag is XmlDataProvider xmlDataProvider))
		{
			return string.Empty;
		}
		XmlDocument document = xmlDataProvider.Document;
		if (document == null)
		{
			return string.Empty;
		}
		XmlNode firstChild = document.FirstChild;
		if (firstChild == null)
		{
			return string.Empty;
		}
		return firstChild.Attributes["Id"].Value;
	}

	public static string GetXamlSynchronous(string xaml)
	{
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(xaml);
		XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
		xmlNamespaceManager.AddNamespace("xaml", "http://schemas.microsoft.com/winfx/2006/xaml/presentation");
		xmlNamespaceManager.AddNamespace("x", "http://schemas.microsoft.com/winfx/2006/xaml");
		xmlNamespaceManager.AddNamespace("i", "xmlns=''");
		string text = "/descendant::xaml:Canvas";
		text += "/child::xaml:Canvas.Tag";
		text += "/child::xaml:XmlDataProvider";
		foreach (XmlNode item in xmlDocument.SelectNodes(text, xmlNamespaceManager))
		{
			XmlAttribute xmlAttribute = xmlDocument.CreateAttribute("IsAsynchronous");
			xmlAttribute.Value = "False";
			item.Attributes.SetNamedItem(xmlAttribute);
		}
		return xmlDocument.OuterXml;
	}

	public static void GetXamlDocumentSynchronous(string xaml, out XmlDocument xmldoc, out XmlNamespaceManager nsmgr)
	{
		xmldoc = new XmlDocument();
		xmldoc.LoadXml(xaml);
		nsmgr = new XmlNamespaceManager(xmldoc.NameTable);
		nsmgr.AddNamespace("xaml", "http://schemas.microsoft.com/winfx/2006/xaml/presentation");
		nsmgr.AddNamespace("x", "http://schemas.microsoft.com/winfx/2006/xaml");
		nsmgr.AddNamespace("i", "xmlns=''");
		string text = "/descendant::xaml:Canvas";
		text += "/child::xaml:Canvas.Tag";
		text += "/child::xaml:XmlDataProvider";
		foreach (XmlNode item in xmldoc.SelectNodes(text, nsmgr))
		{
			XmlAttribute xmlAttribute = xmldoc.CreateAttribute("IsAsynchronous");
			xmlAttribute.Value = "False";
			item.Attributes.SetNamedItem(xmlAttribute);
		}
	}

	private void SelectCanvas(Canvas parentCanvas, string selectionColor, Canvas selectedCanvas)
	{
		try
		{
			for (int i = 0; i < parentCanvas.Children.Count; i++)
			{
				UIElement uIElement = parentCanvas.Children[i];
				if (uIElement is System.Windows.Shapes.Path)
				{
					System.Windows.Shapes.Path path = uIElement as System.Windows.Shapes.Path;
					if (string.IsNullOrEmpty(path.Name))
					{
						HighlightPath(parentCanvas, path, selectionColor, selectedCanvas);
					}
				}
				else if (uIElement is Canvas)
				{
					Canvas parentCanvas2 = uIElement as Canvas;
					SelectCanvas(parentCanvas2, selectionColor, selectedCanvas);
				}
			}
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefCIM");
			throw;
		}
	}

	private void HighlightPath(Canvas parentCanvas, System.Windows.Shapes.Path childPath, string selectionColor, Canvas selectedCanvas)
	{
		try
		{
			string text = string.Format("<Path xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation' Data='{0}' Stroke='#ff000000' >", childPath.RenderedGeometry.ToString(CultureInfo.InvariantCulture).Replace(";", ","), childPath.Name);
			text += "<Path.Fill>";
			text += $"<SolidColorBrush Color='{selectionColor}'/>";
			text += "</Path.Fill>";
			text += "</Path>";
			System.Windows.Shapes.Path element = XamlReader.Load(new MemoryStream(Encoding.UTF8.GetBytes(text))) as System.Windows.Shapes.Path;
			Panel.SetZIndex(element, 100);
			selectedCanvas.Children.Add(element);
			Panel.SetZIndex(selectedCanvas, 100);
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefCIM");
			throw;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/Preference.WPF.MaterialsSelector;component/userscontrols/drawingviewer.xaml", UriKind.Relative);
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
			LayoutRoot = (Grid)target;
			break;
		case 2:
			DrawingContent = (Canvas)target;
			break;
		default:
			_contentLoaded = true;
			break;
		}
	}
}
