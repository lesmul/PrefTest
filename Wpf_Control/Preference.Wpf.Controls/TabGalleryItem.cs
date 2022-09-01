using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Preference.Wpf.Controls;

public class TabGalleryItem : TabItem
{
	private string _strTabName;

	private Brush _brushTabColor;

	private object _objTabContent;

	public GalleryControl Gallery
	{
		get
		{
			return _objTabContent as GalleryControl;
		}
		set
		{
			_objTabContent = value;
		}
	}

	public string TabName
	{
		get
		{
			return _strTabName;
		}
		set
		{
			_strTabName = value;
			base.Header = _strTabName;
		}
	}

	public Brush TabColor
	{
		get
		{
			return _brushTabColor;
		}
		set
		{
			_brushTabColor = value;
			base.Background = _brushTabColor;
		}
	}

	public TabGalleryItem(string strName, Brush brushColor, GalleryControl gallery)
	{
		_strTabName = strName;
		_brushTabColor = brushColor;
		if (string.IsNullOrEmpty(_strTabName))
		{
			base.Visibility = Visibility.Collapsed;
		}
		if (gallery != null)
		{
			gallery.Margin = new Thickness(5.0);
			_objTabContent = gallery;
		}
	}

	public TabGalleryItem(string strName, Brush brushColor, Collection<GalleryItem> collectionItem)
	{
		_strTabName = strName;
		_brushTabColor = brushColor;
		if (string.IsNullOrEmpty(_strTabName))
		{
			base.Visibility = Visibility.Collapsed;
		}
		if (collectionItem != null)
		{
			_objTabContent = new GalleryControl(collectionItem)
			{
				Margin = new Thickness(5.0)
			};
		}
	}

	protected override void OnInitialized(EventArgs e)
	{
		base.Header = _strTabName;
		base.Background = _brushTabColor;
		Color color = (Color)_brushTabColor.GetValue(SolidColorBrush.ColorProperty);
		if (IsDark(color))
		{
			base.Foreground = Brushes.White;
		}
		base.Content = _objTabContent;
		base.OnInitialized(e);
	}

	private bool IsDark(Color color)
	{
		byte b = 130;
		double num = ConvertRgbToGrayScale(color);
		if (num < (double)(int)b)
		{
			return true;
		}
		return false;
	}

	private double ConvertRgbToGrayScale(Color color)
	{
		return 0.3 * (double)(int)color.R + 0.59 * (double)(int)color.G + 0.11 * (double)(int)color.B;
	}
}
