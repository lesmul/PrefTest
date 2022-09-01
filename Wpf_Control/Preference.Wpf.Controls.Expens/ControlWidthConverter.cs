using System;
using System.Globalization;
using System.Windows.Data;

namespace Preference.Wpf.Controls.Expenses;

public class ControlWidthConverter : IValueConverter
{
	private const double c_IndentSize = 10.0;

	public object Convert(object o, Type type, object parameter, CultureInfo culture)
	{
		return (double)o - 10.0;
	}

	public object ConvertBack(object o, Type type, object parameter, CultureInfo culture)
	{
		throw new NotSupportedException();
	}
}
