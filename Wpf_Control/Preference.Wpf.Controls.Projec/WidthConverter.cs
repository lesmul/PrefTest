using System;
using System.Globalization;
using System.Windows.Data;

namespace Preference.Wpf.Controls.Projects.AppLogic;

public class WidthConverter : IValueConverter
{
	private const double c_IndentSize = 12.0;

	public object Convert(object o, Type type, object parameter, CultureInfo culture)
	{
		return (double)o - 12.0;
	}

	public object ConvertBack(object o, Type type, object parameter, CultureInfo culture)
	{
		throw new NotSupportedException();
	}
}
