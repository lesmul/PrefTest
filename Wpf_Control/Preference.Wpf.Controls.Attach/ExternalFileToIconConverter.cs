using System;
using System.Drawing;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Preference.Wpf.Controls.Attachments.ViewModels;

namespace Preference.Wpf.Controls.Attachments.Converters;

public class ExternalFileToIconConverter : IValueConverter
{
	internal static class SharedDictionaryManager
	{
		private static ResourceDictionary _sharedDictionary;

		internal static ResourceDictionary IconsDictionary
		{
			get
			{
				if (_sharedDictionary == null)
				{
					Uri resourceLocator = new Uri("/Preference.WPF.Controls;component/Resources/Icons.xaml", UriKind.Relative);
					_sharedDictionary = (ResourceDictionary)Application.LoadComponent(resourceLocator);
				}
				return _sharedDictionary;
			}
		}
	}

	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (value is ExternalFile)
		{
			ExternalFile externalFile = value as ExternalFile;
			if (externalFile.IsFolder)
			{
				return (DrawingImage)SharedDictionaryManager.IconsDictionary["iconFolderClosed"];
			}
			string extension = IconHelper.GetExtension(externalFile.FilePath);
			if (string.IsNullOrEmpty(extension))
			{
				extension = IconHelper.GetExtension(externalFile.FilePath);
			}
			return GetIconFromExtension(extension);
		}
		return null;
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		throw new NotSupportedException();
	}

	private ImageSource GetIconFromExtension(string extension)
	{
		Icon ico = IconHelper.ExtractIconForExtension(extension, IconHelper.IconSize.Small);
		using Bitmap bitmap = IconHelper.IconToAlphaBitmap(ico);
		return Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
	}
}
