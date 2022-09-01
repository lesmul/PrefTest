using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Preference.Wpf.Controls;

public class BowModelCreator : UserControl, IComponentConnector
{
	private double _dModelHeight;

	private double _dWidth;

	private double _dDepth;

	private double _dAngle;

	private double _dSide;

	private int _nSideCount;

	private string _strErrorCode;

	internal StackPanel BackgroundDrawing;

	internal Viewbox drawingViewbox;

	internal StackPanel PolylineDrawing;

	internal RadioButton StyleDepth;

	internal RadioButton StyleAngle;

	internal TextBlock WidthTextBlock;

	internal NumericTextBox WidthTextBox;

	internal TextBlock DepthTextBlock;

	internal NumericTextBox DepthTextBox;

	internal TextBlock AngleTextBlock;

	internal NumericTextBox AngleTextBox;

	internal TextBlock SideTextBlock;

	internal NumericTextBox SideTextBox;

	internal TextBlock SideCountTextBlock;

	internal NumericTextBox SideCountTextBox;

	internal TextBlock ErrorTextBlock;

	internal TextBlock HeightTextBlock;

	internal NumericTextBox HeightTextBox;

	private bool _contentLoaded;

	public double ModelHeight
	{
		get
		{
			return _dModelHeight;
		}
		set
		{
			_dModelHeight = value;
			HeightTextBox.Text = _dModelHeight.ToString("", NumberFormatInfo.InvariantInfo);
		}
	}

	public double ModelWidth
	{
		get
		{
			return _dWidth;
		}
		set
		{
			_dWidth = value;
			WidthTextBox.Text = _dWidth.ToString("", NumberFormatInfo.InvariantInfo);
		}
	}

	public double ModelDepth
	{
		get
		{
			return _dDepth;
		}
		set
		{
			_dDepth = value;
			DepthTextBox.Text = _dDepth.ToString("", NumberFormatInfo.InvariantInfo);
		}
	}

	public double ModelAngle
	{
		get
		{
			return _dAngle;
		}
		set
		{
			_dAngle = value;
			AngleTextBox.Text = _dAngle.ToString("", NumberFormatInfo.InvariantInfo);
		}
	}

	public string XmlCode
	{
		get
		{
			string result = "";
			if (_dWidth > 0.0 && _dDepth > 0.0)
			{
				result = BayAndBowHelpers.GetBowLogoCode(_dAngle, _dSide, _nSideCount);
			}
			return result;
		}
	}

	public BowModelCreator()
	{
		InitializeComponent();
		StyleDepth.IsChecked = true;
		_dWidth = 0.0;
		_dDepth = 0.0;
		_dAngle = 0.0;
		_dSide = 0.0;
		_nSideCount = 5;
		SideCountTextBox.Text = _nSideCount.ToString();
		UpdateData();
	}

	private void CalculateData()
	{
		if (StyleAngle != null && StyleAngle.IsChecked == true)
		{
			_strErrorCode = BayAndBowHelpers.GetDepthAndSideDimension(_dWidth, _dAngle, _nSideCount, ref _dDepth, ref _dSide);
			UpdateData();
		}
		else if (StyleDepth != null && StyleDepth.IsChecked == true)
		{
			_strErrorCode = BayAndBowHelpers.GetAngleAndSideDimension(_dWidth, _dDepth, _nSideCount, ref _dAngle, ref _dSide);
			UpdateData();
		}
	}

	private void UpdateData()
	{
		WidthTextBox.Text = _dWidth.ToString("", NumberFormatInfo.InvariantInfo);
		DepthTextBox.Text = _dDepth.ToString("", NumberFormatInfo.InvariantInfo);
		AngleTextBox.Text = _dAngle.ToString("", NumberFormatInfo.InvariantInfo);
		SideTextBox.Text = _dSide.ToString("", NumberFormatInfo.InvariantInfo);
		ErrorTextBlock.Text = _strErrorCode;
		string strPolylineCode = "";
		if (_dWidth > 0.0 && _dDepth > 0.0)
		{
			strPolylineCode = BayAndBowHelpers.GetBowLogoCode(_dAngle, _dSide, _nSideCount);
		}
		DrawingHelpers.DrawPolyline(PolylineDrawing, drawingViewbox, strPolylineCode, 1, bWidthDimension: true);
	}

	private void AngleTextBox_TextChanged(object sender, TextChangedEventArgs e)
	{
		double num = 0.0;
		if (AngleTextBox.Text != "")
		{
			num = Convert.ToDouble(AngleTextBox.Text, NumberFormatInfo.InvariantInfo);
		}
		if (_dAngle != num)
		{
			_dAngle = num;
			CalculateData();
		}
	}

	private void DepthTextBox_TextChanged(object sender, TextChangedEventArgs e)
	{
		double num = 0.0;
		if (DepthTextBox.Text != "")
		{
			num = Convert.ToDouble(DepthTextBox.Text, NumberFormatInfo.InvariantInfo);
		}
		if (_dDepth != num)
		{
			_dDepth = num;
			CalculateData();
		}
	}

	private void WidthTextBox_TextChanged(object sender, TextChangedEventArgs e)
	{
		double num = 0.0;
		if (WidthTextBox.Text != "")
		{
			num = Convert.ToDouble(WidthTextBox.Text, NumberFormatInfo.InvariantInfo);
		}
		if (num != _dWidth)
		{
			_dWidth = num;
			CalculateData();
		}
	}

	private void SideTextBox_TextChanged(object sender, TextChangedEventArgs e)
	{
		double num = 0.0;
		if (SideTextBox.Text != "")
		{
			num = Convert.ToDouble(SideTextBox.Text, NumberFormatInfo.InvariantInfo);
		}
		if (num != _dSide)
		{
			_dSide = num;
			CalculateData();
		}
	}

	private void SideCountTextBox_TextChanged(object sender, TextChangedEventArgs e)
	{
		int num = 0;
		if (SideCountTextBox.Text != "")
		{
			num = Convert.ToInt16(SideCountTextBox.Text, NumberFormatInfo.InvariantInfo);
		}
		if (num != _nSideCount)
		{
			_nSideCount = num;
			CalculateData();
		}
	}

	private void HeightTextBox_TextChanged(object sender, TextChangedEventArgs e)
	{
		_dModelHeight = Convert.ToDouble(HeightTextBox.Text, NumberFormatInfo.InvariantInfo);
	}

	private void drawingViewbox_SizeChanged(object sender, SizeChangedEventArgs e)
	{
		string strPolylineCode = "";
		if (_dWidth > 0.0 && _dDepth > 0.0)
		{
			strPolylineCode = BayAndBowHelpers.GetBowLogoCode(_dAngle, _dSide, _nSideCount);
		}
		DrawingHelpers.DrawPolyline(PolylineDrawing, drawingViewbox, strPolylineCode, 1, bWidthDimension: true);
	}

	private void Style_Checked(object sender, RoutedEventArgs e)
	{
		if (StyleDepth != null && StyleDepth.IsChecked == true)
		{
			if (AngleTextBox != null)
			{
				AngleTextBox.IsEnabled = false;
			}
			if (DepthTextBox != null)
			{
				DepthTextBox.IsEnabled = true;
			}
			if (SideTextBox != null)
			{
				SideTextBox.IsEnabled = false;
			}
		}
		else if (StyleAngle != null && StyleAngle.IsChecked == true)
		{
			if (AngleTextBox != null)
			{
				AngleTextBox.IsEnabled = true;
			}
			if (DepthTextBox != null)
			{
				DepthTextBox.IsEnabled = false;
			}
			if (SideTextBox != null)
			{
				SideTextBox.IsEnabled = false;
			}
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/Preference.Wpf.Controls;component/bowmodelcreator.xaml", UriKind.Relative);
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
			BackgroundDrawing = (StackPanel)target;
			break;
		case 2:
			drawingViewbox = (Viewbox)target;
			drawingViewbox.SizeChanged += drawingViewbox_SizeChanged;
			break;
		case 3:
			PolylineDrawing = (StackPanel)target;
			break;
		case 4:
			StyleDepth = (RadioButton)target;
			StyleDepth.Checked += Style_Checked;
			break;
		case 5:
			StyleAngle = (RadioButton)target;
			StyleAngle.Checked += Style_Checked;
			break;
		case 6:
			WidthTextBlock = (TextBlock)target;
			break;
		case 7:
			WidthTextBox = (NumericTextBox)target;
			break;
		case 8:
			DepthTextBlock = (TextBlock)target;
			break;
		case 9:
			DepthTextBox = (NumericTextBox)target;
			break;
		case 10:
			AngleTextBlock = (TextBlock)target;
			break;
		case 11:
			AngleTextBox = (NumericTextBox)target;
			break;
		case 12:
			SideTextBlock = (TextBlock)target;
			break;
		case 13:
			SideTextBox = (NumericTextBox)target;
			break;
		case 14:
			SideCountTextBlock = (TextBlock)target;
			break;
		case 15:
			SideCountTextBox = (NumericTextBox)target;
			break;
		case 16:
			ErrorTextBlock = (TextBlock)target;
			break;
		case 17:
			HeightTextBlock = (TextBlock)target;
			break;
		case 18:
			HeightTextBox = (NumericTextBox)target;
			break;
		default:
			_contentLoaded = true;
			break;
		}
	}
}
