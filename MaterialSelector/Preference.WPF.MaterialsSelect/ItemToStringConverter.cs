using System;
using System.Globalization;
using System.Windows.Data;
using Preference.WPF.MaterialsSelector.Models;
using Preference.WPF.MaterialsSelector.Properties;

namespace Preference.WPF.MaterialsSelector.Core.Converters;

public class ItemToStringConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (value is Item)
		{
			Item item = value as Item;
			if (item.ItemType == ItemTypes.AluClip)
			{
				return Resources.AluClip;
			}
			if (item.ItemType == ItemTypes.Clones)
			{
				return GetString(item.ElementName, Resources.Clones);
			}
			if (item.ItemType == ItemTypes.Contour)
			{
				return GetString(item.ElementName, Resources.Contour);
			}
			if (item.ItemType == ItemTypes.Delimiter)
			{
				return GetString(item.ElementName, Resources.Delimiter);
			}
			if (item.ItemType == ItemTypes.FrameHardware)
			{
				return Resources.FrameHardware;
			}
			if (item.ItemType == ItemTypes.Glass)
			{
				return GetString(item.ElementName, Resources.Glass);
			}
			if (item.ItemType == ItemTypes.GlobalScript)
			{
				return GetString(item.ElementName, Resources.GlobalAttachments);
			}
			if (item.ItemType == ItemTypes.GlazingLedge)
			{
				return Resources.GlazingLedge;
			}
			if (item.ItemType == ItemTypes.Hole)
			{
				return GetString(item.ElementName, Resources.Hole);
			}
			if (item.ItemType == ItemTypes.Model)
			{
				return GetString(item.ElementName, Resources.Model);
			}
			if (item.ItemType == ItemTypes.OuterRod)
			{
				return GetString(item.ElementName, Resources.OuterBar);
			}
			if (item.ItemType == ItemTypes.ProfilePiece)
			{
				ProfilePiece profilePiece = item as ProfilePiece;
				return string.Format(Resources.ProfilePiece, profilePiece.Order);
			}
			if (item.ItemType == ItemTypes.Rod)
			{
				return Resources.ProfilePieces;
			}
			if (item.ItemType == ItemTypes.Roller)
			{
				return GetString(item.ElementName, Resources.Roller);
			}
			if (item.ItemType == ItemTypes.SashHardware)
			{
				return Resources.SashHardware;
			}
			if (item.ItemType == ItemTypes.Submodel)
			{
				return GetString(item.ElementName, Resources.Submodel);
			}
		}
		return null;
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}

	private string GetString(string s1, string s2)
	{
		if (!string.IsNullOrEmpty(s1))
		{
			return s1;
		}
		return s2;
	}
}
