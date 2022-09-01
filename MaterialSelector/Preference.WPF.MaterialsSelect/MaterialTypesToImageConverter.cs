using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using Preference.WPF.MaterialsSelector.Models;

namespace Preference.WPF.MaterialsSelector.Core.Converters;

public class MaterialTypesToImageConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (value == null)
		{
			return null;
		}
		if (value is MaterialTypes materialTypes)
		{
			ResourceDictionary resourceDictionary = new ResourceDictionary();
			resourceDictionary.Source = new Uri("pack://application:,,,/Preference.WPF.MaterialsSelector;component/Resources/Images.xaml", UriKind.RelativeOrAbsolute);
			switch (materialTypes)
			{
			case MaterialTypes.Rod:
				return resourceDictionary["icon4009"] as DrawingImage;
			case MaterialTypes.Meter:
				return resourceDictionary["icon4019"] as DrawingImage;
			case MaterialTypes.Piece:
				return resourceDictionary["icon4017"] as DrawingImage;
			case MaterialTypes.Surface:
				return resourceDictionary["icon4007"] as DrawingImage;
			}
		}
		else if (value is string)
		{
			return value;
		}
		return null;
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}
