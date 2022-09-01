using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using Preference.WPF.MaterialsSelector.Models;

namespace Preference.WPF.MaterialsSelector.Core.Converters;

public class ItemToImageConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (value is Item)
		{
			Item item = value as Item;
			new ResourceDictionary().Source = new Uri("pack://application:,,,/Preference.WPF.MaterialsSelector;component/Resources/Images.xaml", UriKind.RelativeOrAbsolute);
			if (item.ItemType == ItemTypes.AluClip)
			{
				return new BitmapImage(new Uri("pack://application:,,,/Preference.WPF.MaterialsSelector;component/Resources/4133_16.png"));
			}
			if (item.ItemType == ItemTypes.Clones)
			{
				return new BitmapImage(new Uri("pack://application:,,,/Preference.WPF.MaterialsSelector;component/Resources/4067_16.bmp"));
			}
			if (item.ItemType == ItemTypes.Contour)
			{
				return new BitmapImage(new Uri("pack://application:,,,/Preference.WPF.MaterialsSelector;component/Resources/4055_16.bmp"));
			}
			if (item.ItemType == ItemTypes.Delimiter)
			{
				return new BitmapImage(new Uri("pack://application:,,,/Preference.WPF.MaterialsSelector;component/Resources/4061_16.bmp"));
			}
			if (item.ItemType == ItemTypes.FrameHardware)
			{
				return new BitmapImage(new Uri("pack://application:,,,/Preference.WPF.MaterialsSelector;component/Resources/4095_16.bmp"));
			}
			if (item.ItemType == ItemTypes.Glass)
			{
				return new BitmapImage(new Uri("pack://application:,,,/Preference.WPF.MaterialsSelector;component/Resources/4063_16.png"));
			}
			if (item.ItemType == ItemTypes.GlazingLedge)
			{
				return new BitmapImage(new Uri("pack://application:,,,/Preference.WPF.MaterialsSelector;component/Resources/4081_16.bmp"));
			}
			if (item.ItemType == ItemTypes.GlobalScript)
			{
				return new BitmapImage(new Uri("pack://application:,,,/Preference.WPF.MaterialsSelector;component/Resources/4013_16.bmp"));
			}
			if (item.ItemType == ItemTypes.Hole)
			{
				return new BitmapImage(new Uri("pack://application:,,,/Preference.WPF.MaterialsSelector;component/Resources/4057_16.bmp"));
			}
			if (item.ItemType == ItemTypes.Model)
			{
				return new BitmapImage(new Uri("pack://application:,,,/Preference.WPF.MaterialsSelector;component/Resources/4003_16.png"));
			}
			if (item.ItemType == ItemTypes.OuterRod)
			{
				return new BitmapImage(new Uri("pack://application:,,,/Preference.WPF.MaterialsSelector;component/Resources/4065_16.bmp"));
			}
			if (item.ItemType == ItemTypes.ProfilePiece)
			{
				ProfilePiece profilePiece = item as ProfilePiece;
				if (profilePiece.Angle == 360.0)
				{
					return new BitmapImage(new Uri("pack://application:,,,/Preference.WPF.MaterialsSelector;component/Resources/4071_16.bmp"));
				}
				if (profilePiece.Angle == 90.0)
				{
					return new BitmapImage(new Uri("pack://application:,,,/Preference.WPF.MaterialsSelector;component/Resources/4073_16.bmp"));
				}
				if (profilePiece.Angle == 180.0)
				{
					return new BitmapImage(new Uri("pack://application:,,,/Preference.WPF.MaterialsSelector;component/Resources/4075_16.bmp"));
				}
				if (profilePiece.Angle == 270.0)
				{
					return new BitmapImage(new Uri("pack://application:,,,/Preference.WPF.MaterialsSelector;component/Resources/4069_16.bmp"));
				}
				return new BitmapImage(new Uri("pack://application:,,,/Preference.WPF.MaterialsSelector;component/Resources/4077_16.bmp"));
			}
			if (item.ItemType == ItemTypes.Rod)
			{
				return new BitmapImage(new Uri("pack://application:,,,/Preference.WPF.MaterialsSelector;component/Resources/4081_16.bmp"));
			}
			if (item.ItemType == ItemTypes.Roller)
			{
				return new BitmapImage(new Uri("pack://application:,,,/Preference.WPF.MaterialsSelector;component/Resources/4099_16.bmp"));
			}
			if (item.ItemType == ItemTypes.SashHardware)
			{
				return new BitmapImage(new Uri("pack://application:,,,/Preference.WPF.MaterialsSelector;component/Resources/4097_16.bmp"));
			}
			if (item.ItemType == ItemTypes.Submodel)
			{
				return new BitmapImage(new Uri("pack://application:,,,/Preference.WPF.MaterialsSelector;component/Resources/4003_16.png"));
			}
		}
		return null;
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}
