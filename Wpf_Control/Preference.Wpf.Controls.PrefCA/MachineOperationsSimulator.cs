using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;

namespace Preference.Wpf.Controls.PrefCAM;

public class MachineOperationsSimulator : UserControl, IComponentConnector
{
	private static double dZoomingValue = 1.2;

	private TranslateTransform _tTranslation;

	private ScaleTransform _tScale;

	private double _dCurrentOffset;

	private Vector _vCurrentTranslateApplied;

	private Vector _vCurrentScaleApplied = new Vector(1.0, 1.0);

	private Point _ptPanStart;

	private bool _bIsMenuOptionsEnabled;

	private double _dAnimationSpeed;

	private MachineItem _siMachine;

	private OperationItem _siOperation;

	private ProfileItem _siProfile;

	internal DockPanel DrawingPanel;

	internal Grid StatusBar;

	internal TextBlock MousePositionX;

	internal TextBlock MousePositionY;

	internal Expander ExpanderOptions;

	internal Slider AnimationSlider;

	internal Slider ZoomSlider;

	internal TextBox AnimationSpeedTextBox;

	internal CheckBox HideStatusBarCheck;

	internal Canvas DrawCanvas;

	internal ScaleTransform DrawCanvasScale;

	internal TranslateTransform DrawCanvasTranslation;

	private bool _contentLoaded;

	public MachineItem Machine
	{
		get
		{
			return _siMachine;
		}
		set
		{
			_siMachine = value;
		}
	}

	public ProfileItem Profile
	{
		get
		{
			return _siProfile;
		}
		set
		{
			_siProfile = value;
		}
	}

	public OperationItem Operation
	{
		get
		{
			return _siOperation;
		}
		set
		{
			_siOperation = value;
		}
	}

	public double AnimationSpeed
	{
		get
		{
			return _dAnimationSpeed;
		}
		set
		{
			_dAnimationSpeed = value;
		}
	}

	public double CurrentOffset
	{
		get
		{
			return _dCurrentOffset;
		}
		set
		{
			_dCurrentOffset = value;
		}
	}

	public bool IsMenuOptionsEnabled
	{
		get
		{
			return _bIsMenuOptionsEnabled;
		}
		set
		{
			_bIsMenuOptionsEnabled = value;
			if (!value)
			{
				ExpanderOptions.Visibility = Visibility.Collapsed;
			}
			else
			{
				ExpanderOptions.Visibility = Visibility.Visible;
			}
		}
	}

	public MachineOperationsSimulator()
	{
		InitializeComponent();
		_tTranslation = new TranslateTransform();
		_tScale = new ScaleTransform();
		TransformGroup transformGroup = new TransformGroup
		{
			Children = 
			{
				(Transform)_tTranslation,
				(Transform)_tScale
			}
		};
		AnimationSpeed = 5.0;
	}

	public void UpdateSimulator()
	{
		Operation.Background = new SolidColorBrush(Color.FromArgb(50, 141, 253, 166));
		Profile.Background = new SolidColorBrush(Color.FromArgb(50, 243, 144, 144));
		Machine.Background = new SolidColorBrush(Color.FromArgb(50, 27, 19, 143));
		Profile.Children.Add(Operation);
		Machine.Children.Add(Profile);
		DrawCanvas.Children.Add(Machine);
		AdjustCanvasToDraw();
	}

	public void AdjustImageToScreen()
	{
		ScaleImage(1.0, 1.0);
		_vCurrentScaleApplied = new Vector(1.0, 1.0);
		CenterOnScreen();
	}

	public void CenterOnScreen()
	{
		TranslateImage(0.0, 0.0);
		_vCurrentTranslateApplied = default(Vector);
	}

	public void ZoomImage(bool bZoomIn)
	{
		if (bZoomIn)
		{
			_vCurrentScaleApplied.X *= dZoomingValue;
			_vCurrentScaleApplied.Y *= dZoomingValue;
		}
		else
		{
			_vCurrentScaleApplied.X /= dZoomingValue;
			_vCurrentScaleApplied.Y /= dZoomingValue;
		}
		ScaleImage(_vCurrentScaleApplied.X, _vCurrentScaleApplied.Y);
	}

	private void Animate(bool bInOut, double dSpeed)
	{
		if (Operation == null)
		{
			return;
		}
		if (bInOut)
		{
			if (_dCurrentOffset < 0.0)
			{
				Operation.Translation.X = Operation.Translation.X - Operation.OrientationPoint.X * dSpeed;
				Operation.Translation.Y = Operation.Translation.Y - Operation.OrientationPoint.Y * dSpeed;
				_dCurrentOffset += dSpeed;
			}
		}
		else if (_dCurrentOffset > 0.0 - Operation.AnimationOffset)
		{
			Operation.Translation.X = Operation.Translation.X + Operation.OrientationPoint.X * dSpeed;
			Operation.Translation.Y = Operation.Translation.Y + Operation.OrientationPoint.Y * dSpeed;
			_dCurrentOffset -= dSpeed;
		}
	}

	private void UserControlLoaded(object sender, RoutedEventArgs e)
	{
		HideStatusBarCheck.Checked += HideStatusBarCheckChecked;
		HideStatusBarCheck.Unchecked += HideStatusBarCheckUnchecked;
		DrawingPanel.PreviewMouseMove += DrawingPanelPreviewMouseMove;
		DrawingPanel.PreviewMouseWheel += DrawingPanelPreviewMouseWheel;
		DrawingPanel.PreviewMouseDown += DrawingPanelPreviewMouseDown;
		AnimationSpeedTextBox.TextChanged += AnimationSpeedTextBoxTextChanged;
		AnimationSlider.Maximum = Operation.AnimationOffset;
		AnimationSlider.Minimum = 0.0;
		AnimationSlider.ValueChanged += AnimationSliderValueChanged;
	}

	private void DrawingPanelPreviewMouseDown(object sender, MouseButtonEventArgs e)
	{
		Point position = e.GetPosition(this);
		_ptPanStart = position - _vCurrentTranslateApplied;
	}

	private void DrawingPanelPreviewMouseWheel(object sender, MouseWheelEventArgs e)
	{
		if (Operation != null)
		{
			if (e.Delta > 0)
			{
				Animate(bInOut: true, AnimationSpeed);
			}
			else
			{
				Animate(bInOut: false, AnimationSpeed);
			}
		}
	}

	private void DrawingPanelPreviewMouseMove(object sender, MouseEventArgs e)
	{
		Point position = e.GetPosition(DrawingPanel);
		if (e.MiddleButton == MouseButtonState.Pressed)
		{
			_vCurrentTranslateApplied = position - _ptPanStart;
			TranslateImage(_vCurrentTranslateApplied.X, _vCurrentTranslateApplied.Y);
		}
		string format = "{0:0.##}";
		position = e.GetPosition(DrawCanvas);
		MousePositionX.Text = string.Format(CultureInfo.CurrentCulture, format, position.X);
		MousePositionY.Text = string.Format(CultureInfo.CurrentCulture, format, DrawingPanel.ActualHeight - position.Y);
	}

	private void HideStatusBarCheckUnchecked(object sender, RoutedEventArgs e)
	{
		StatusBar.Visibility = Visibility.Collapsed;
	}

	private void HideStatusBarCheckChecked(object sender, RoutedEventArgs e)
	{
		StatusBar.Visibility = Visibility.Visible;
	}

	private void AnimationSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
	{
		if (e.NewValue > e.OldValue)
		{
			Animate(bInOut: false, AnimationSpeed);
		}
		else
		{
			Animate(bInOut: true, AnimationSpeed);
		}
	}

	private void AnimationSpeedTextBoxTextChanged(object sender, TextChangedEventArgs e)
	{
		if (string.IsNullOrEmpty(AnimationSpeedTextBox.Text))
		{
			AnimationSpeed = 0.0;
		}
		else
		{
			AnimationSpeed = Convert.ToDouble(AnimationSpeedTextBox.Text, CultureInfo.CurrentCulture);
		}
	}

	private void AdjustCanvasToDraw()
	{
		DrawCanvas.Width = 700.0;
		DrawCanvas.Height = 1100.0;
	}

	private void TranslateImage(double dTranslationX, double dTranslationY)
	{
		DrawCanvasTranslation.X = dTranslationX;
		DrawCanvasTranslation.Y = dTranslationY;
	}

	private void ScaleImage(double dScaleX, double dScaleY)
	{
		DrawCanvasScale.ScaleX = dScaleX;
		DrawCanvasScale.ScaleY = dScaleY;
	}

	private void OnCenterOnScreen(object sender, RoutedEventArgs e)
	{
		CenterOnScreen();
	}

	private void OnAdjustToScreen(object sender, RoutedEventArgs e)
	{
		AdjustImageToScreen();
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/Preference.Wpf.Controls;component/prefcam/machineoperationssimulator.xaml", UriKind.Relative);
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
			((MachineOperationsSimulator)target).Loaded += UserControlLoaded;
			break;
		case 2:
			DrawingPanel = (DockPanel)target;
			break;
		case 3:
			StatusBar = (Grid)target;
			break;
		case 4:
			MousePositionX = (TextBlock)target;
			break;
		case 5:
			MousePositionY = (TextBlock)target;
			break;
		case 6:
			ExpanderOptions = (Expander)target;
			break;
		case 7:
			AnimationSlider = (Slider)target;
			break;
		case 8:
			ZoomSlider = (Slider)target;
			break;
		case 9:
			AnimationSpeedTextBox = (TextBox)target;
			break;
		case 10:
			((Button)target).Click += OnCenterOnScreen;
			break;
		case 11:
			((Button)target).Click += OnAdjustToScreen;
			break;
		case 12:
			HideStatusBarCheck = (CheckBox)target;
			break;
		case 13:
			DrawCanvas = (Canvas)target;
			break;
		case 14:
			DrawCanvasScale = (ScaleTransform)target;
			break;
		case 15:
			DrawCanvasTranslation = (TranslateTransform)target;
			break;
		default:
			_contentLoaded = true;
			break;
		}
	}
}
