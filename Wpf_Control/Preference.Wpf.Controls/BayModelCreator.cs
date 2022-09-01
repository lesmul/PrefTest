using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Preference.Wpf.Controls;

public class BayModelCreator : UserControl, IComponentConnector
{
	private double _dModelHeight;

	private double _dWidth;

	private double _dDepth;

	private double _dAngle;

	private double _dFront;

	private double _dFlanker;

	private string _strErrorCode;

	internal StackPanel BackgroundDrawing;

	internal Viewbox drawingViewbox;

	internal StackPanel PolylineDrawing;

	internal RadioButton StyleFlankers;

	internal RadioButton StyleAngle;

	internal RadioButton StyleFront;

	internal TextBlock WidthTextBlock;

	internal NumericTextBox WidthTextBox;

	internal TextBlock DepthTextBlock;

	internal NumericTextBox DepthTextBox;

	internal TextBlock AngleTextBlock;

	internal NumericTextBox AngleTextBox;

	internal TextBlock FlankerTextBlock;

	internal NumericTextBox FlankerTextBox;

	internal TextBlock FrontTextBlock;

	internal NumericTextBox FrontTextBox;

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
			CalculateData();
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
			CalculateData();
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
			CalculateData();
		}
	}

	public string XmlCode => BayAndBowHelpers.GetBayLogoCode(_dAngle, _dFront, _dFlanker);

	public BayModelCreator()
	{
		InitializeComponent();
		StyleFlankers.IsChecked = true;
		_dWidth = 0.0;
		_dDepth = 0.0;
		_dAngle = 0.0;
		_dFront = 0.0;
		_dFlanker = 0.0;
		UpdateData();
	}

	private void CalculateData()
	{
		if (StyleAngle != null && StyleAngle.IsChecked == true)
		{
			_strErrorCode = BayAndBowHelpers.GetFrontAndFlankerDimension(_dWidth, _dDepth, _dAngle, ref _dFront, ref _dFlanker);
			UpdateData();
		}
		else if (StyleFlankers != null && StyleFlankers.IsChecked == true)
		{
			_strErrorCode = BayAndBowHelpers.GetAngleAndFrontDimension(_dWidth, _dDepth, _dFlanker, ref _dAngle, ref _dFront);
			UpdateData();
		}
		else if (StyleFront != null && StyleFront.IsChecked == true)
		{
			_strErrorCode = BayAndBowHelpers.GetAngleAndFlankerDimension(_dWidth, _dDepth, _dFront, ref _dAngle, ref _dFlanker);
			UpdateData();
		}
	}

	private void UpdateData()
	{
		WidthTextBox.Text = _dWidth.ToString("", NumberFormatInfo.InvariantInfo);
		DepthTextBox.Text = _dDepth.ToString("", NumberFormatInfo.InvariantInfo);
		AngleTextBox.Text = _dAngle.ToString("", NumberFormatInfo.InvariantInfo);
		FrontTextBox.Text = _dFront.ToString("", NumberFormatInfo.InvariantInfo);
		FlankerTextBox.Text = _dFlanker.ToString("", NumberFormatInfo.InvariantInfo);
		ErrorTextBlock.Text = _strErrorCode;
		string bayLogoCode = BayAndBowHelpers.GetBayLogoCode(_dAngle, _dFront, _dFlanker);
		DrawingHelpers.DrawPolyline(PolylineDrawing, drawingViewbox, bayLogoCode, 1, bWidthDimension: true);
	}

	private void Style_Checked(object sender, RoutedEventArgs e)
	{
		if (StyleAngle != null && StyleAngle.IsChecked == true)
		{
			if (AngleTextBox != null)
			{
				AngleTextBox.IsEnabled = true;
			}
			if (FlankerTextBox != null)
			{
				FlankerTextBox.IsEnabled = false;
			}
			if (FrontTextBox != null)
			{
				FrontTextBox.IsEnabled = false;
			}
		}
		else if (StyleFlankers != null && StyleFlankers.IsChecked == true)
		{
			if (AngleTextBox != null)
			{
				AngleTextBox.IsEnabled = false;
			}
			if (FlankerTextBox != null)
			{
				FlankerTextBox.IsEnabled = true;
			}
			if (FrontTextBox != null)
			{
				FrontTextBox.IsEnabled = false;
			}
		}
		else if (StyleFront != null && StyleFront.IsChecked == true)
		{
			if (AngleTextBox != null)
			{
				AngleTextBox.IsEnabled = false;
			}
			if (FlankerTextBox != null)
			{
				FlankerTextBox.IsEnabled = false;
			}
			if (FrontTextBox != null)
			{
				FrontTextBox.IsEnabled = true;
			}
		}
	}

	private void WidthTextBox_TextChanged(object sender, TextChangedEventArgs e)
	{
		double num = Convert.ToDouble(WidthTextBox.Text, NumberFormatInfo.InvariantInfo);
		if (num != _dWidth)
		{
			_dWidth = num;
			CalculateData();
		}
	}

	private void DepthTextBox_TextChanged(object sender, TextChangedEventArgs e)
	{
		double num = Convert.ToDouble(DepthTextBox.Text, NumberFormatInfo.InvariantInfo);
		if (_dDepth != num)
		{
			_dDepth = num;
			CalculateData();
		}
	}

	private void AngleTextBox_TextChanged(object sender, TextChangedEventArgs e)
	{
		double num = Convert.ToDouble(AngleTextBox.Text, NumberFormatInfo.InvariantInfo);
		if (_dAngle != num)
		{
			_dAngle = num;
			CalculateData();
		}
	}

	private void FlankerTextBox_TextChanged(object sender, TextChangedEventArgs e)
	{
		double num = Convert.ToDouble(FlankerTextBox.Text, NumberFormatInfo.InvariantInfo);
		if (_dFlanker != num)
		{
			_dFlanker = num;
			CalculateData();
		}
	}

	private void FrontTextBox_TextChanged(object sender, TextChangedEventArgs e)
	{
		double num = Convert.ToDouble(FrontTextBox.Text, NumberFormatInfo.InvariantInfo);
		if (_dFront != num)
		{
			_dFront = num;
			CalculateData();
		}
	}

	private void HeightTextBox_TextChanged(object sender, TextChangedEventArgs e)
	{
		_dModelHeight = Convert.ToDouble(HeightTextBox.Text, NumberFormatInfo.InvariantInfo);
	}

	private void drawingViewbox_SizeChanged(object sender, SizeChangedEventArgs e)
	{
		string bayLogoCode = BayAndBowHelpers.GetBayLogoCode(_dAngle, _dFront, _dFlanker);
		DrawingHelpers.DrawPolyline(PolylineDrawing, drawingViewbox, bayLogoCode, 1, bWidthDimension: true);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/Preference.Wpf.Controls;component/baymodelcreator.xaml", UriKind.Relative);
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
			StyleFlankers = (RadioButton)target;
			StyleFlankers.Checked += Style_Checked;
			break;
		case 5:
			StyleAngle = (RadioButton)target;
			StyleAngle.Checked += Style_Checked;
			break;
		case 6:
			StyleFront = (RadioButton)target;
			StyleFront.Checked += Style_Checked;
			break;
		case 7:
			WidthTextBlock = (TextBlock)target;
			break;
		case 8:
			WidthTextBox = (NumericTextBox)target;
			break;
		case 9:
			DepthTextBlock = (TextBlock)target;
			break;
		case 10:
			DepthTextBox = (NumericTextBox)target;
			break;
		case 11:
			AngleTextBlock = (TextBlock)target;
			break;
		case 12:
			AngleTextBox = (NumericTextBox)target;
			break;
		case 13:
			FlankerTextBlock = (TextBlock)target;
			break;
		case 14:
			FlankerTextBox = (NumericTextBox)target;
			break;
		case 15:
			FrontTextBlock = (TextBlock)target;
			break;
		case 16:
			FrontTextBox = (NumericTextBox)target;
			break;
		case 17:
			ErrorTextBlock = (TextBlock)target;
			break;
		case 18:
			HeightTextBlock = (TextBlock)target;
			break;
		case 19:
			HeightTextBox = (NumericTextBox)target;
			break;
		default:
			_contentLoaded = true;
			break;
		}
	}
}
