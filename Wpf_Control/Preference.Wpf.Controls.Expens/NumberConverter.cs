using System;
using System.Globalization;
using System.Windows.Data;

namespace Preference.Wpf.Controls.Expenses;

[ValueConversion(typeof(double), typeof(string))]
public class NumberConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		return ((double)value).ToString("N");
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		double num = 0.0;
		try
		{
			num = System.Convert.ToDouble(value);
		}
		catch (Exception)
		{
			num = 0.0;
		}
		return num;
	}
}
