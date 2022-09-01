using System;
using System.Globalization;
using System.Windows.Data;
using ConvertMetricImperial;

namespace Preference.Wpf.Controls.Units;

public class UnitsModeMatchesConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		if (parameter == null || value == null)
		{
			return false;
		}
		UnitsMode val = (UnitsMode)System.Convert.ToInt32(value);
		UnitsMode val2 = (UnitsMode)System.Convert.ToInt32(parameter);
		double num = System.Convert.ToDouble(value);
		bool flag = val == val2;
		return flag;
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		return Binding.DoNothing;
	}
}
