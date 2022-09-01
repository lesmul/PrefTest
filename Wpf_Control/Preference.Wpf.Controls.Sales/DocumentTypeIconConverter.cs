using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Preference.Wpf.Controls.Sales;

public class DocumentTypeIconConverter : IValueConverter
{
	public Style Order { get; set; }

	public Style Offer { get; set; }

	public Style Installation { get; set; }

	public Style Measurement { get; set; }

	public Style Production { get; set; }

	public Style DeliveryNote { get; set; }

	public Style Invoice { get; set; }

	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (value is short || value is int)
		{
			switch (System.Convert.ToInt32(value))
			{
			case 1:
				return Offer;
			case 2:
				return Order;
			case 3:
				return Production;
			case 4:
				return Measurement;
			case 5:
				return Installation;
			case 6:
				return DeliveryNote;
			case 7:
				return Invoice;
			}
		}
		return Offer;
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}
