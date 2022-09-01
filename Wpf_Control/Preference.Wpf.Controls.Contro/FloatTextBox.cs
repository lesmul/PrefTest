using System;
using System.Globalization;
using System.Windows;

namespace Preference.Wpf.Controls.ControlsForDoubles;

public class FloatTextBox : GenericTextBox<float>
{
	static FloatTextBox()
	{
		GenericTextBox<float>.SetDefaultValue(0f);
		FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(FloatTextBox), new FrameworkPropertyMetadata(typeof(FloatTextBox)));
	}

	protected override float GetValueFromText(string strText)
	{
		return Convert.ToSingle(strText, CultureInfo.CurrentUICulture);
	}

	protected override string GetTextFromValue(float valueData)
	{
		return valueData.ToString(CultureInfo.CurrentUICulture);
	}
}
