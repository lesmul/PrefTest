using System;
using System.Globalization;
using System.Windows.Data;
using Preference.Wpf.Controls.Projects.Views;

namespace Preference.Wpf.Controls.Projects.AppLogic;

[ValueConversion(typeof(double), typeof(string))]
public class PriceAsDoubleConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		double num = (double)value;
		string result = num.ToString("N");
		if (!string.IsNullOrEmpty(ProjectView.CurrencySymbol))
		{
			result = num.ToString("N") + " " + ProjectView.CurrencySymbol;
		}
		return result;
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		double num = 0.0;
		try
		{
			string value2 = value.ToString();
			if (!string.IsNullOrEmpty(ProjectView.CurrencySymbol))
			{
				value2 = value.ToString().Replace(ProjectView.CurrencySymbol, "");
				value2 = value2.Trim();
			}
			num = System.Convert.ToDouble(value2);
		}
		catch (Exception)
		{
			num = 0.0;
		}
		return num;
	}
}
