using System;
using System.Globalization;
using System.Windows.Data;
using Preference.Wpf.Controls.Properties;

namespace Preference.Wpf.Controls.Projects.AppLogic;

public class DocumentTypeToStringConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		return (DocumentTypes)value switch
		{
			DocumentTypes.Customer => Resources.IDS_CONTACTS, 
			DocumentTypes.ExpenseDocument => Resources.IDS_EXPENSES, 
			DocumentTypes.ProductionLot => Resources.IDS_PRODUCTION, 
			DocumentTypes.Project => Resources.IDS_PROJECT, 
			DocumentTypes.PurchasesDocument => Resources.IDS_PURCHASES, 
			DocumentTypes.SalesDocument => Resources.IDS_SALES, 
			DocumentTypes.ShippingLot => Resources.IDS_SHIPPING, 
			DocumentTypes.Warehouse => Resources.IDS_WAREHOUSE, 
			_ => null, 
		};
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}
