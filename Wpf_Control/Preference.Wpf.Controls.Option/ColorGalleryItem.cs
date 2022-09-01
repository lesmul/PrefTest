using System;

namespace Preference.Wpf.Controls.Options;

[Serializable]
public class ColorGalleryItem : GalleryItem
{
	private string _strFamily;

	private string _strInnerColor;

	private string _strOuterColor;

	public string Family
	{
		get
		{
			return _strFamily;
		}
		set
		{
			_strFamily = value;
		}
	}

	public string InnerColor
	{
		get
		{
			return _strInnerColor;
		}
		set
		{
			_strInnerColor = value;
		}
	}

	public string OuterColor
	{
		get
		{
			return _strOuterColor;
		}
		set
		{
			_strOuterColor = value;
		}
	}

	public ColorGalleryItem()
	{
	}

	public ColorGalleryItem(string strName, string strDescription, string strValue, string strFamily, string strInnerColor, string strOuterColor)
	{
		base.ItemName = strName;
		base.ItemDescription = strDescription;
		base.ItemValue = strValue;
		base.ItemToolTip = $"{strDescription} ({strFamily})";
		Family = strFamily;
		InnerColor = strInnerColor;
		OuterColor = strOuterColor;
	}
}
