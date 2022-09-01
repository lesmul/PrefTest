using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Preference.Wpf.Controls.Sales;

public class PDELightIconConverter : IValueConverter
{
	public Style GreenLight { get; set; }

	public Style YellowLight { get; set; }

	public Style RedLight { get; set; }

	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (value is short || value is int)
		{
			switch (System.Convert.ToInt32(value))
			{
			case 1:
				return GreenLight;
			case 2:
				return YellowLight;
			case 3:
				return RedLight;
			}
		}
		return GreenLight;
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}
