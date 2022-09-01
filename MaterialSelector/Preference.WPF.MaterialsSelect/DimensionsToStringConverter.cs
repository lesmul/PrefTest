using System;
using System.Globalization;
using System.Windows.Data;
using Preference.WPF.MaterialsSelector.Models;
using Preference.WPF.MaterialsSelector.Properties;

namespace Preference.WPF.MaterialsSelector.Core.Converters;

public class DimensionsToStringConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (value == null)
		{
			return string.Empty;
		}
		GeneratedMaterial generatedMaterial = (GeneratedMaterial)value;
		if (generatedMaterial.MaterialType == MaterialTypes.Surface)
		{
			return $"{generatedMaterial.Width} x {generatedMaterial.Height}";
		}
		if (generatedMaterial.MaterialType == MaterialTypes.Rod || generatedMaterial.MaterialType == MaterialTypes.Meter)
		{
			return $"{Resources.Width} : {generatedMaterial.Width}";
		}
		return string.Empty;
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		return null;
	}
}
