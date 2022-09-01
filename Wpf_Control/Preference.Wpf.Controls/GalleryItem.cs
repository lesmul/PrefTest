using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Preference.Wpf.Controls;

public class GalleryItem : INotifyPropertyChanged
{
	private string _strItemName;

	private string _strItemDescription;

	private string _strItemTooltip;

	private ImageSource _isItemImage;

	private object _objItemXamlImage;

	private string _strItemValue;

	private static ImageSource _imageSourceEmpty;

	public string ItemName
	{
		get
		{
			return _strItemName;
		}
		set
		{
			if (_strItemName != value)
			{
				_strItemName = value;
				OnNotifyPropertyChanged(new PropertyChangedEventArgs("ItemName"));
			}
		}
	}

	public string ItemValue
	{
		get
		{
			return _strItemValue;
		}
		set
		{
			string text = _strItemName;
			if (value != null)
			{
				text = value;
			}
			if (text != _strItemValue)
			{
				_strItemValue = text;
				OnNotifyPropertyChanged(new PropertyChangedEventArgs("ItemValue"));
			}
		}
	}

	public string ItemDescription
	{
		get
		{
			return _strItemDescription;
		}
		set
		{
			if (_strItemDescription != value)
			{
				_strItemDescription = value;
				OnNotifyPropertyChanged(new PropertyChangedEventArgs("ItemDescription"));
			}
		}
	}

	public string ItemToolTip
	{
		get
		{
			return _strItemTooltip;
		}
		set
		{
			string text = _strItemDescription;
			if (!string.IsNullOrEmpty(value))
			{
				text = value;
			}
			if (text != _strItemTooltip)
			{
				_strItemTooltip = text;
				OnNotifyPropertyChanged(new PropertyChangedEventArgs("ItemToolTip"));
			}
		}
	}

	public ImageSource ItemImage
	{
		get
		{
			return _isItemImage;
		}
		set
		{
			if (value != null)
			{
				_isItemImage = value;
			}
			else
			{
				if (_imageSourceEmpty == null)
				{
					_imageSourceEmpty = BitmapSource.Create(2, 2, 96.0, 96.0, PixelFormats.Indexed1, new BitmapPalette(new List<Color> { Colors.Transparent }), new byte[4], 1);
				}
				_isItemImage = _imageSourceEmpty;
			}
			OnNotifyPropertyChanged(new PropertyChangedEventArgs("ItemImage"));
		}
	}

	public object ItemXamlImage
	{
		get
		{
			return _objItemXamlImage;
		}
		set
		{
			_objItemXamlImage = value;
			OnNotifyPropertyChanged(new PropertyChangedEventArgs("ItemXamlImage"));
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	public GalleryItem()
	{
	}

	public GalleryItem(ImageSource isImage, string strName)
	{
		ItemName = strName;
		ItemDescription = "";
		ItemToolTip = "";
		ItemImage = isImage;
	}

	public GalleryItem(ImageSource isImage, string strName, string strDescription)
	{
		ItemName = strName;
		ItemDescription = strDescription;
		ItemToolTip = "";
		ItemImage = isImage;
	}

	public GalleryItem(ImageSource isImage, string strName, string strDescription, string strTooltip)
	{
		ItemName = strName;
		ItemDescription = strDescription;
		ItemToolTip = strTooltip;
		ItemImage = isImage;
	}

	public GalleryItem(ImageSource isImage, string strName, string strDescription, string strTooltip, string strValue)
	{
		ItemName = strName;
		ItemDescription = strDescription;
		ItemToolTip = strTooltip;
		ItemImage = isImage;
		ItemValue = strValue;
	}

	public GalleryItem(object objImage, string strName, string strDescription, string strTooltip, string strValue)
	{
		ItemName = strName;
		ItemDescription = strDescription;
		ItemToolTip = strTooltip;
		ItemXamlImage = objImage;
		ItemValue = strValue;
	}

	public bool Contains(string strToSearch)
	{
		string value = strToSearch.ToUpper();
		if (ItemName.ToUpper().Contains(value) || ItemDescription.ToUpper().Contains(value) || ItemValue.ToUpper().Contains(value))
		{
			return true;
		}
		return false;
	}

	protected virtual void OnNotifyPropertyChanged(PropertyChangedEventArgs e)
	{
		this.PropertyChanged?.Invoke(this, e);
	}
}
