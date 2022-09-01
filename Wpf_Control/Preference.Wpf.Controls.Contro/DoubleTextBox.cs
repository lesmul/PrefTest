using System;
using System.Globalization;
using System.Windows;

namespace Preference.Wpf.Controls.ControlsForDoubles;

public class DoubleTextBox : GenericTextBox<double>
{
	static DoubleTextBox()
	{
		GenericTextBox<double>.SetDefaultValue(0.0);
		FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(DoubleTextBox), new FrameworkPropertyMetadata(typeof(DoubleTextBox)));
	}

	protected override double GetValueFromText(string strText)
	{
		return Convert.ToDouble(strText, CultureInfo.CurrentUICulture);
	}

	protected override string GetTextFromValue(double valueData)
	{
		return valueData.ToString(CultureInfo.CurrentUICulture);
	}
}
