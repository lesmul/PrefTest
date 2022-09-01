using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Xml;
using DevComponents.WpfRibbon;
using Preference.Wpf.Controls.Properties;

namespace Preference.Wpf.Controls;

public class JitmvViewer : Grid, IComponentConnector
{
	private enum ZoomMode
	{
		MoreZoom,
		LessZoom,
		ResetZoom
	}

	private static double dScaleIncrement = 1.2;

	private string _strXamlJitmv;

	private Point _scrollOrigin;

	private Point _panStart;

	private bool _bPanModeActivate;

	internal Grid HideMenuPanel;

	internal ButtonPanel ButtonsPanel;

	internal ButtonDropDown PanModeButton;

	internal ButtonDropDown MoreZoomButton;

	internal ButtonDropDown LessZoomButton;

	internal ButtonDropDown ResetZoomButton;

	internal ScrollViewer ScrollView;

	internal Grid JitmvViewerContent;

	internal ScaleTransform Scale;

	private bool _contentLoaded;

	public string XamlJitmv
	{
		get
		{
			return _strXamlJitmv;
		}
		set
		{
			_strXamlJitmv = value;
		}
	}

	public bool PanModeActivate
	{
		get
		{
			return _bPanModeActivate;
		}
		set
		{
			_bPanModeActivate = value;
			if (_bPanModeActivate)
			{
				Mouse.OverrideCursor = new Cursor(GetType().Module.Assembly.GetManifestResourceStream("Preference.Wpf.Controls.Resources.HandCursor.cur"));
			}
			else
			{
				Mouse.OverrideCursor = null;
			}
		}
	}

	public JitmvViewer()
	{
		InitializeComponent();
		InitTranslation();
	}

	public JitmvViewer(string strXamlJitmv)
	{
		InitializeComponent();
		XamlJitmv = strXamlJitmv;
		InitTranslation();
	}

	public void Reset()
	{
		JitmvViewerContent.Children.Clear();
		HideMenuPanel.MouseEnter -= HideMenuPanelMouseEnter;
		((UIElement)(object)ButtonsPanel).MouseLeave -= ButtonsPanelMouseLeave;
		ScrollView.PreviewMouseWheel -= ScrollViewPreviewMouseWheel;
		ScrollView.PreviewMouseMove -= ScrollViewPreviewMouseMove;
		ScrollView.PreviewMouseDown -= ScrollViewPreviewMouseDown;
	}

	public void UpdateViewer(string strXamlJitmv)
	{
		XamlJitmv = strXamlJitmv;
		UpdateViewer();
	}

	public void UpdateViewer()
	{
		try
		{
			Reset();
			if (!string.IsNullOrEmpty(XamlJitmv))
			{
				StringReader input = new StringReader(XamlJitmv);
				XmlReader reader = XmlReader.Create(input);
				if (XamlReader.Load(reader) is UIElement element)
				{
					JitmvViewerContent.Children.Add(element);
					HideMenuPanel.MouseEnter += HideMenuPanelMouseEnter;
					((UIElement)(object)ButtonsPanel).MouseLeave += ButtonsPanelMouseLeave;
					ScrollView.PreviewMouseWheel += ScrollViewPreviewMouseWheel;
					ScrollView.PreviewMouseMove += ScrollViewPreviewMouseMove;
					ScrollView.PreviewMouseDown += ScrollViewPreviewMouseDown;
				}
			}
		}
		catch (XamlParseException ex)
		{
			StackPanel stackPanel = new StackPanel();
			TextBlock textBlock = new TextBlock();
			textBlock.Text = Preference.Wpf.Controls.Properties.Resources.ErrorJitmvViewer;
			textBlock.FontSize = 12.0;
			textBlock.FontWeight = FontWeights.Bold;
			textBlock.TextDecorations = TextDecorations.Underline;
			textBlock.Foreground = Brushes.Red;
			textBlock.HorizontalAlignment = HorizontalAlignment.Center;
			textBlock.VerticalAlignment = VerticalAlignment.Center;
			textBlock.Margin = new Thickness(0.0, 0.0, 0.0, 5.0);
			TextBlock textBlock2 = new TextBlock();
			textBlock2.Text = Preference.Wpf.Controls.Properties.Resources.ErrorJitmvViewer + ex.ToString();
			textBlock2.HorizontalAlignment = HorizontalAlignment.Center;
			textBlock2.VerticalAlignment = VerticalAlignment.Center;
			textBlock2.TextWrapping = TextWrapping.Wrap;
			textBlock2.Width = JitmvViewerContent.ActualWidth;
			stackPanel.Children.Add(textBlock);
			stackPanel.Children.Add(textBlock2);
			JitmvViewerContent.Children.Clear();
			JitmvViewerContent.Children.Add(stackPanel);
		}
	}

	public void ResetZoom()
	{
		Zoom(ZoomMode.ResetZoom);
	}

	private void ScrollViewPreviewMouseDown(object sender, MouseButtonEventArgs e)
	{
		_panStart = e.GetPosition(ScrollView);
		_scrollOrigin = new Point(ScrollView.HorizontalOffset, ScrollView.VerticalOffset);
	}

	private void ScrollViewPreviewMouseMove(object sender, MouseEventArgs e)
	{
		if ((PanModeActivate && e.LeftButton == MouseButtonState.Pressed) || e.MiddleButton == MouseButtonState.Pressed)
		{
			if (JitmvViewerContent.IsMouseOver)
			{
				Mouse.OverrideCursor = new Cursor(GetType().Module.Assembly.GetManifestResourceStream("Preference.Wpf.Controls.Resources.HandCursor.cur"));
				Point point = _scrollOrigin - (e.GetPosition(ScrollView) - _panStart);
				if (ScrollView.ComputedHorizontalScrollBarVisibility == Visibility.Visible)
				{
					ScrollView.ScrollToHorizontalOffset(point.X);
				}
				if (ScrollView.ComputedVerticalScrollBarVisibility == Visibility.Visible)
				{
					ScrollView.ScrollToVerticalOffset(point.Y);
				}
			}
		}
		else if (!PanModeActivate)
		{
			Mouse.OverrideCursor = null;
		}
	}

	private void ScrollViewPreviewMouseWheel(object sender, MouseWheelEventArgs e)
	{
		if (e.MiddleButton != MouseButtonState.Pressed && (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)))
		{
			if (e.Delta > 0)
			{
				Zoom(ZoomMode.MoreZoom);
			}
			else
			{
				Zoom(ZoomMode.LessZoom);
			}
		}
	}

	private void InitTranslation()
	{
		((FrameworkElement)(object)PanModeButton).ToolTip = Preference.Wpf.Controls.Properties.Resources.ToolTipJitmvViewerPanMode;
		((FrameworkElement)(object)MoreZoomButton).ToolTip = Preference.Wpf.Controls.Properties.Resources.ToolTipJitmvViewerMoreZoom;
		((FrameworkElement)(object)LessZoomButton).ToolTip = Preference.Wpf.Controls.Properties.Resources.ToolTipJitmvViewerLessZoom;
		((FrameworkElement)(object)ResetZoomButton).ToolTip = Preference.Wpf.Controls.Properties.Resources.ToolTipJitmvViewerResetZoom;
	}

	private void Zoom(ZoomMode mode)
	{
		switch (mode)
		{
		case ZoomMode.MoreZoom:
			Scale.ScaleX *= dScaleIncrement;
			Scale.ScaleY *= dScaleIncrement;
			break;
		case ZoomMode.LessZoom:
			Scale.ScaleX /= dScaleIncrement;
			Scale.ScaleY /= dScaleIncrement;
			break;
		case ZoomMode.ResetZoom:
			Scale.ScaleX = 1.0;
			Scale.ScaleY = 1.0;
			break;
		}
	}

	private void ResetZoomClick(object sender, RoutedEventArgs e)
	{
		Zoom(ZoomMode.ResetZoom);
	}

	private void ButtonsPanelMouseLeave(object sender, MouseEventArgs e)
	{
		((UIElement)(object)ButtonsPanel).Visibility = Visibility.Collapsed;
		HideMenuPanel.Width = 20.0;
		HideMenuPanel.Height = 20.0;
		if (PanModeActivate)
		{
			PanModeActivate = true;
		}
	}

	private void HideMenuPanelMouseEnter(object sender, MouseEventArgs e)
	{
		((UIElement)(object)ButtonsPanel).Visibility = Visibility.Visible;
		HideMenuPanel.Width = 200.0;
		HideMenuPanel.Height = 100.0;
		Mouse.OverrideCursor = null;
	}

	private void ActivePanModeClick(object sender, RoutedEventArgs e)
	{
		if (PanModeActivate)
		{
			PanModeActivate = false;
		}
		else
		{
			PanModeActivate = true;
		}
	}

	private void MoreZoomClick(object sender, RoutedEventArgs e)
	{
		Zoom(ZoomMode.MoreZoom);
	}

	private void LessZoomClick(object sender, RoutedEventArgs e)
	{
		Zoom(ZoomMode.LessZoom);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/Preference.Wpf.Controls;component/jitmvviewer.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	void IComponentConnector.Connect(int connectionId, object target)
	{
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Expected O, but got Unknown
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Expected O, but got Unknown
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Expected O, but got Unknown
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Expected O, but got Unknown
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Expected O, but got Unknown
		switch (connectionId)
		{
		case 1:
			HideMenuPanel = (Grid)target;
			break;
		case 2:
			ButtonsPanel = (ButtonPanel)target;
			break;
		case 3:
			PanModeButton = (ButtonDropDown)target;
			PanModeButton.add_Click((RoutedEventHandler)ActivePanModeClick);
			break;
		case 4:
			MoreZoomButton = (ButtonDropDown)target;
			MoreZoomButton.add_Click((RoutedEventHandler)MoreZoomClick);
			break;
		case 5:
			LessZoomButton = (ButtonDropDown)target;
			LessZoomButton.add_Click((RoutedEventHandler)LessZoomClick);
			break;
		case 6:
			ResetZoomButton = (ButtonDropDown)target;
			ResetZoomButton.add_Click((RoutedEventHandler)ResetZoomClick);
			break;
		case 7:
			ScrollView = (ScrollViewer)target;
			break;
		case 8:
			JitmvViewerContent = (Grid)target;
			break;
		case 9:
			Scale = (ScaleTransform)target;
			break;
		default:
			_contentLoaded = true;
			break;
		}
	}
}
