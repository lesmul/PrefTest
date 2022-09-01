using System;
using System.Globalization;
using System.Windows.Data;

namespace Preference.Wpf.Controls.Scheduler;

public class DateTimeWithFormatStringConverter : IValueConverter
{
	public CultureInfo CultureInfoUI { get; set; }

	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		CultureInfo cultureInfo = ((CultureInfoUI != null) ? CultureInfoUI : culture);
		string text = parameter as string;
		DateTime dateTime = (DateTime)value;
		if (text != null)
		{
			return dateTime.ToString(text, cultureInfo);
		}
		return dateTime.ToString(cultureInfo);
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		return Binding.DoNothing;
	}
}
