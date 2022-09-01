using System;
using System.Globalization;
using System.Windows;
using Preference.Wpf.Controls.ControlsForDoubles;

namespace Preference.Wpf.Controls.ControlsForIntegers;

public class PositiveIntegerTextBox : GenericTextBox<int>
{
	static PositiveIntegerTextBox()
	{
		GenericTextBox<int>.SetDefaultValue(0);
		FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(PositiveIntegerTextBox), new FrameworkPropertyMetadata(typeof(PositiveIntegerTextBox)));
	}

	protected override int GetValueFromText(string strText)
	{
		return Convert.ToInt32(strText, CultureInfo.CurrentUICulture);
	}

	protected override string GetTextFromValue(int valueData)
	{
		return valueData.ToString(CultureInfo.CurrentUICulture);
	}
}
