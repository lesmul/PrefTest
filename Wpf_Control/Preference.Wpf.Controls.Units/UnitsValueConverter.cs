using System;
using System.Globalization;
using System.Windows.Data;

namespace Preference.Wpf.Controls.Units;

public class UnitsValueConverter : IMultiValueConverter
{
	public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
	{
		return values[0];
	}

	public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
	{
		return new object[2]
		{
			Binding.DoNothing,
			value
		};
	}
}
