using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Preference.Wpf.Controls.Projects.AppLogic;

public class LevelToIndentConverter : IValueConverter
{
	private const double c_IndentSize = 19.0;

	public object Convert(object o, Type type, object parameter, CultureInfo culture)
	{
		return new Thickness((double)(int)o * 19.0, 0.0, 0.0, 0.0);
	}

	public object ConvertBack(object o, Type type, object parameter, CultureInfo culture)
	{
		throw new NotSupportedException();
	}
}
