using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using Preference.WPF.MaterialsSelector.Models;

namespace Preference.WPF.MaterialsSelector.Core.Converters;

public class SquareRolesToImageConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (value == null)
		{
			return null;
		}
		SquareRoles squareRoles = (SquareRoles)value;
		Uri uri = null;
		switch (squareRoles)
		{
		case SquareRoles.Frame:
			uri = new Uri("pack://application:,,,/Preference.WPF.MaterialsSelector;component/Resources/3331_16.png");
			break;
		case SquareRoles.GlazingStop:
			uri = new Uri("pack://application:,,,/Preference.WPF.MaterialsSelector;component/Resources/4063_16.png");
			break;
		case SquareRoles.Sash:
			uri = new Uri("pack://application:,,,/Preference.WPF.MaterialsSelector;component/Resources/3362_16.png");
			break;
		}
		if (uri != null)
		{
			return new BitmapImage(uri);
		}
		return null;
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		return null;
	}
}
