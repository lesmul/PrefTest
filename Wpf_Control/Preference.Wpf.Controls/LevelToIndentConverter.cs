using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Preference.Wpf.Controls;

public class LevelToIndentConverter : IValueConverter
{
	private const double c_IndentSize = 19.0;

	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		return new Thickness((double)(int)value * 19.0, 0.0, 0.0, 0.0);
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		throw new NotSupportedException();
	}
}
