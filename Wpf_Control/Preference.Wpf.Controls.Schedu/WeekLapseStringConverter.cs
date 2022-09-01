using System;
using System.Globalization;
using System.Windows.Data;

namespace Preference.Wpf.Controls.Scheduler;

public class WeekLapseStringConverter : IValueConverter
{
	public CultureInfo CultureInfoUI { get; set; }

	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		CultureInfo cultureInfo = ((CultureInfoUI != null) ? CultureInfoUI : culture);
		string monthDayPattern = cultureInfo.DateTimeFormat.MonthDayPattern;
		monthDayPattern = monthDayPattern.Replace("MMMM", "MMM");
		DateTime dateTime = (DateTime)value;
		string text = dateTime.ToString(monthDayPattern);
		string text2 = dateTime.AddDays(6.0).ToString(monthDayPattern);
		return text + " - " + text2;
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		return Binding.DoNothing;
	}
}
