using System;
using System.Globalization;
using System.Windows.Data;
using Preference.Wpf.Controls.Properties;

namespace Preference.Wpf.Controls.Attachments.Converters;

public class BooleanToStringConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (value is bool)
		{
			if (!(bool)value)
			{
				return Resources.No;
			}
			return Resources.Yes;
		}
		return null;
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		throw new NotSupportedException();
	}
}
