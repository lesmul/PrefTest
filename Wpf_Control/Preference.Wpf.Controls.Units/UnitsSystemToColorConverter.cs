using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using ConvertMetricImperial;

namespace Preference.Wpf.Controls.Units;

public class UnitsSystemToColorConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Invalid comparison between Unknown and I4
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Invalid comparison between Unknown and I4
		byte result = byte.MaxValue;
		if (parameter != null && !byte.TryParse(parameter.ToString(), out result))
		{
			result = byte.MaxValue;
		}
		UnitsMode val = (UnitsMode)value;
		if ((int)val != 1)
		{
			if ((int)val == 2)
			{
				return new SolidColorBrush(Color.FromArgb(result, 175, 215, 225));
			}
			return new SolidColorBrush(Color.FromArgb(result, SystemColors.WindowColor.R, SystemColors.WindowColor.G, SystemColors.WindowColor.B));
		}
		return new SolidColorBrush(Color.FromArgb(result, 199, 178, 208));
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}
