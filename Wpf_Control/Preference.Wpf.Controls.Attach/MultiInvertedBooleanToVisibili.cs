using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Preference.Wpf.Controls.Attachments.Converters;

public class MultiInvertedBooleanToVisibilityConverter : IMultiValueConverter
{
	public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
	{
		bool flag = false;
		foreach (object obj in values)
		{
			if (obj is bool)
			{
				flag = flag || (bool)obj;
			}
		}
		if (flag)
		{
			return Visibility.Hidden;
		}
		return Visibility.Visible;
	}

	public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}
