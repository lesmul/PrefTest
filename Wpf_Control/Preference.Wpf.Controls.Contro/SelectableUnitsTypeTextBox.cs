using System;
using System.Globalization;
using System.Windows;

namespace Preference.Wpf.Controls.ControlsForDoubles;

public class SelectableUnitsTypeTextBox : GenericTextBox<double>
{
	public static readonly DependencyProperty UnitsTypeProperty;

	public short UnitsType
	{
		get
		{
			return (short)GetValue(UnitsTypeProperty);
		}
		set
		{
			SetValue(UnitsTypeProperty, value);
		}
	}

	static SelectableUnitsTypeTextBox()
	{
		UnitsTypeProperty = DependencyProperty.Register("UnitsType", typeof(short), typeof(SelectableUnitsTypeTextBox), new PropertyMetadata((short)0, OnUnitsTypeChanged));
		GenericTextBox<double>.SetDefaultValue(0.0);
		FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(SelectableUnitsTypeTextBox), new FrameworkPropertyMetadata(typeof(SelectableUnitsTypeTextBox)));
	}

	protected override double GetValueFromText(string strText)
	{
		double num = Convert.ToDouble(strText, CultureInfo.CurrentUICulture);
		return UnitsType switch
		{
			1 => num, 
			2 => num * 1000.0, 
			3 => num * 25.4, 
			4 => num * 304.8, 
			_ => Math.Round(num), 
		};
	}

	protected override string GetTextFromValue(double valueData)
	{
		double num = valueData;
		switch (UnitsType)
		{
		case 2:
			num /= 1000.0;
			break;
		case 3:
			num /= 25.4;
			break;
		case 4:
			num /= 304.8;
			break;
		default:
			num = Math.Round(num);
			break;
		case 1:
			break;
		}
		return num.ToString(CultureInfo.CurrentUICulture);
	}

	private static void OnUnitsTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is SelectableUnitsTypeTextBox selectableUnitsTypeTextBox)
		{
			selectableUnitsTypeTextBox.RefreshingTextFromData = true;
			selectableUnitsTypeTextBox.SetValue(GenericTextBox<double>.TextValueProperty, selectableUnitsTypeTextBox.GetTextFromValue((double)selectableUnitsTypeTextBox.GetValue(GenericTextBox<double>.ValueDataProperty)));
			selectableUnitsTypeTextBox.RefreshingTextFromData = false;
		}
	}
}
