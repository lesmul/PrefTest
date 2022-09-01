using System;
using System.Globalization;
using System.Windows.Data;

namespace Preference.Wpf.Controls.Attachments.Converters;

public class IntegerToPowOfBytesStringConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (value is int)
		{
			string[] array = new string[6] { "bytes", "KB", "MB", "GB", "TB", "PB" };
			double num = (int)value;
			double num2 = num;
			int num3 = 0;
			while ((int)num2 > 0 && num3 < 6)
			{
				num = num2;
				num2 /= 1024.0;
				if ((int)num2 > 0)
				{
					num3++;
				}
			}
			return string.Format("{0} {1}", num.ToString("N"), array[num3]);
		}
		return null;
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		throw new NotSupportedException();
	}
}
