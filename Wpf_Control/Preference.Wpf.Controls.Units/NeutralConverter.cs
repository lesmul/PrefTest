using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace Preference.Wpf.Controls.Units;

public class NeutralConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		MenuItem menuItem = value as MenuItem;
		if (parameter == null)
		{
			return value;
		}
		int num = System.Convert.ToInt32(parameter);
		if (num == 2 || num == 3)
		{
			return Binding.DoNothing;
		}
		return value;
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (parameter == null)
		{
			return value;
		}
		int num = System.Convert.ToInt32(parameter);
		if (num == 1 || num == 3)
		{
			return Binding.DoNothing;
		}
		return value;
	}
}
