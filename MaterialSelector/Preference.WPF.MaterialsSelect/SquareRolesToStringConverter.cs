using System;
using System.Globalization;
using System.Windows.Data;
using Preference.WPF.MaterialsSelector.Models;
using Preference.WPF.MaterialsSelector.Properties;

namespace Preference.WPF.MaterialsSelector.Core.Converters;

public class SquareRolesToStringConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (value == null)
		{
			return null;
		}
		return (SquareRoles)value switch
		{
			SquareRoles.Frame => Resources.Frame, 
			SquareRoles.GlazingStop => Resources.GlazingStop, 
			SquareRoles.Sash => Resources.Sash, 
			_ => null, 
		};
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		return null;
	}
}
