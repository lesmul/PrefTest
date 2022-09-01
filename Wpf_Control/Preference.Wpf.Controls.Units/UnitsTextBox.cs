using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ConvertMetricImperial;

namespace Preference.Wpf.Controls.Units;

public class UnitsTextBox : TextBox
{
	private static RoutedCommand _commandChangeUnits;

	private UnitsValueData _data;

	public static readonly DependencyProperty UnitsValueProperty;

	public static readonly DependencyProperty UnitsModeProperty;

	public static readonly DependencyProperty UnitsDataStoredKindProperty;

	public static readonly DependencyProperty UnitsTextValueProperty;

	private bool _bUpdatingText;

	public static RoutedCommand ChangeUnitsCmd => _commandChangeUnits;

	private UnitsValueData ValueData
	{
		get
		{
			if (_data == null)
			{
				_data = new UnitsValueData();
				_data.PropertyChanged += _data_PropertyChanged;
			}
			return _data;
		}
	}

	public double UnitsValue
	{
		get
		{
			return (double)GetValue(UnitsValueProperty);
		}
		set
		{
			SetValue(UnitsValueProperty, value);
		}
	}

	public UnitsMode UnitsMode
	{
		get
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			return (UnitsMode)GetValue(UnitsModeProperty);
		}
		set
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			SetValue(UnitsModeProperty, value);
		}
	}

	public DataStoredKind UnitsDataStoredKind
	{
		get
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			return (DataStoredKind)GetValue(UnitsDataStoredKindProperty);
		}
		set
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			SetValue(UnitsDataStoredKindProperty, value);
		}
	}

	public string UnitsTextValue
	{
		get
		{
			return (string)GetValue(UnitsTextValueProperty);
		}
		private set
		{
			SetValue(UnitsTextValueProperty, value);
		}
	}

	public string UpdatedUnitsTextValue
	{
		get
		{
			return ValueData.Text;
		}
		set
		{
			_bUpdatingText = true;
			ValueData.Text = value;
			UnitsTextValue = value;
			_bUpdatingText = false;
		}
	}

	static UnitsTextBox()
	{
		UnitsValueProperty = DependencyProperty.Register("UnitsValue", typeof(double), typeof(UnitsTextBox), new PropertyMetadata(0.0, OnUnitsValueChanged));
		UnitsModeProperty = DependencyProperty.Register("UnitsMode", typeof(UnitsMode), typeof(UnitsTextBox), new PropertyMetadata((object)(UnitsMode)0, OnUnitsModeChanged));
		UnitsDataStoredKindProperty = DependencyProperty.Register("UnitsDataStoredKind", typeof(DataStoredKind), typeof(UnitsTextBox), new PropertyMetadata((object)(DataStoredKind)0, OnUnitsDataStoredKindChanged));
		UnitsTextValueProperty = DependencyProperty.Register("UnitsTextValue", typeof(string), typeof(UnitsTextBox), new PropertyMetadata("0"));
		FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(UnitsTextBox), new FrameworkPropertyMetadata(typeof(UnitsTextBox)));
		InitializeCommands();
	}

	private static void InitializeCommands()
	{
		_commandChangeUnits = new RoutedCommand("ChangeUnitsCmd", typeof(UnitsTextBox));
		CommandManager.RegisterClassCommandBinding(typeof(UnitsTextBox), new CommandBinding(_commandChangeUnits, ChangeUnitsCmdExecuted));
	}

	private void UpdateTextInInterface()
	{
		UnitsTextValue = ValueData.Text;
	}

	protected override void OnLostFocus(RoutedEventArgs e)
	{
		base.OnLostFocus(e);
		UpdateTextInInterface();
	}

	private void _data_PropertyChanged(object sender, PropertyChangedEventArgs e)
	{
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		string propertyName = e.PropertyName;
		switch (propertyName)
		{
		default:
			_ = propertyName == "Text";
			break;
		case "Value":
			UnitsValue = ValueData.Value;
			if (!_bUpdatingText)
			{
				UpdateTextInInterface();
			}
			break;
		case "UnitsMode":
			UnitsMode = ValueData.UnitsMode;
			UpdateTextInInterface();
			break;
		case "Dsk":
			UnitsDataStoredKind = ValueData.Dsk;
			UpdateTextInInterface();
			break;
		}
	}

	private static void OnUnitsValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is UnitsTextBox unitsTextBox)
		{
			unitsTextBox.ValueData.Value = (double)e.NewValue;
		}
	}

	private static void OnUnitsModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		if (d is UnitsTextBox unitsTextBox)
		{
			unitsTextBox.ValueData.UnitsMode = (UnitsMode)e.NewValue;
		}
	}

	private static void OnUnitsDataStoredKindChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		if (d is UnitsTextBox unitsTextBox)
		{
			unitsTextBox.ValueData.Dsk = (DataStoredKind)e.NewValue;
		}
	}

	private static void ChangeUnitsCmdExecuted(object sender, ExecutedRoutedEventArgs e)
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Invalid comparison between Unknown and I4
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		if (sender is UnitsTextBox unitsTextBox)
		{
			unitsTextBox.ValueData.Text = unitsTextBox.Text;
			UnitsMode val = (UnitsMode)Convert.ToInt32(e.Parameter, CultureInfo.InvariantCulture);
			if ((int)val <= 2)
			{
				unitsTextBox.UnitsMode = val;
			}
		}
	}
}
