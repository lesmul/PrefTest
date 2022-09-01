using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Preference.Wpf.Controls.Sales;

public class PlausibilityIconConverter : IValueConverter
{
	public Style Plausibility0 { get; set; }

	public Style Plausibility1 { get; set; }

	public Style Plausibility2 { get; set; }

	public Style Plausibility3 { get; set; }

	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (value is short || value is int)
		{
			switch (System.Convert.ToInt32(value))
			{
			case 0:
				return Plausibility0;
			case 1:
				return Plausibility1;
			case 2:
				return Plausibility2;
			case 3:
				return Plausibility3;
			}
		}
		return Plausibility0;
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}
