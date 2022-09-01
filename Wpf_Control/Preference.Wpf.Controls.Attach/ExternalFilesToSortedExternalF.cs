using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;
using Preference.Wpf.Controls.Attachments.ViewModels;

namespace Preference.Wpf.Controls.Attachments.Converters;

public class ExternalFilesToSortedExternalFilesConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (value is ExternalFileList)
		{
			ExternalFileList list = value as ExternalFileList;
			ListCollectionView listCollectionView = new ListCollectionView(list);
			SortDescription item = new SortDescription("Name", ListSortDirection.Ascending);
			listCollectionView.SortDescriptions.Add(item);
			return listCollectionView;
		}
		return null;
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		throw new NotSupportedException();
	}
}
