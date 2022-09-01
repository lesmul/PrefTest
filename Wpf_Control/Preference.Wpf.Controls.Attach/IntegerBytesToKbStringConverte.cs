using System;
using System.Globalization;
using System.Windows.Data;

namespace Preference.Wpf.Controls.Attachments.Converters;

public class IntegerBytesToKbStringConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (value is int)
		{
			double num = (int)value;
			return string.Format("{0} KB", (num / 1024.0).ToString("N"));
		}
		return null;
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		throw new NotSupportedException();
	}
}
