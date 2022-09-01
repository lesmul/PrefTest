using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Preference.Wpf.Controls.Sales;

public class PDELightConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (value == null)
		{
			return Visibility.Collapsed;
		}
		if (value is short || value is int)
		{
			if (System.Convert.ToInt32(value) == 0)
			{
				return Visibility.Collapsed;
			}
			return Visibility.Visible;
		}
		return Visibility.Collapsed;
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}
