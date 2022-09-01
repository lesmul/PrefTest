using System;
using System.Globalization;
using System.Windows.Data;

namespace Preference.WPF.MaterialsSelector.Core.Converters;

public class TimeSpanToStringConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (value == null)
		{
			return null;
		}
		TimeSpan timeSpan = (TimeSpan)value;
		return $"{timeSpan.Hours:D2}:{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		return null;
	}
}
