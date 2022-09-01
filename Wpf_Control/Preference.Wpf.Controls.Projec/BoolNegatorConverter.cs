using System;
using System.Globalization;
using System.Windows.Data;

namespace Preference.Wpf.Controls.Projects.AppLogic;

[ValueConversion(typeof(bool), typeof(bool))]
public class BoolNegatorConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		bool flag = System.Convert.ToBoolean(value);
		return !flag;
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		bool flag = System.Convert.ToBoolean(value);
		return !flag;
	}
}
