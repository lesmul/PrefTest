using System;
using System.Globalization;
using System.Windows.Data;

namespace Preference.Wpf.Controls.Attachments.Converters;

public class IntegerToBytesStringConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (value is int num)
		{
			return string.Format("{0} bytes", num.ToString("N"));
		}
		return null;
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		throw new NotSupportedException();
	}
}
