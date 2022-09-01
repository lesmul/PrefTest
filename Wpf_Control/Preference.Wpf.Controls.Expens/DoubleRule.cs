using System;
using System.Globalization;
using System.Windows.Controls;

namespace Preference.Wpf.Controls.Expenses;

public class DoubleRule : ValidationRule
{
	public override ValidationResult Validate(object value, CultureInfo cultureInfo)
	{
		double num = 0.0;
		try
		{
			if (((string)value).Length > 0)
			{
				num = double.Parse((string)value);
			}
		}
		catch (Exception ex)
		{
			return new ValidationResult(isValid: false, ex.Message);
		}
		return new ValidationResult(isValid: true, null);
	}
}
