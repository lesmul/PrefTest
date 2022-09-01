using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Preference.Wpf.Controls.Attachments.Converters;

public sealed class BooleanToVisibilityConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		bool flag = false;
		if (value is bool)
		{
			flag = (bool)value;
		}
		else if (value is bool? flag2)
		{
			flag = flag2.GetValueOrDefault();
		}
		if (parameter != null && bool.Parse((string)parameter))
		{
			flag = !flag;
		}
		if (flag)
		{
			return Visibility.Visible;
		}
		return Visibility.Collapsed;
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		bool flag = value is Visibility && (Visibility)value == Visibility.Visible;
		if (parameter != null && (bool)parameter)
		{
			flag = !flag;
		}
		return flag;
	}
}
