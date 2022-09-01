using System;
using System.Globalization;
using System.Windows.Data;
using Preference.Wpf.Controls.Properties;

namespace Preference.Wpf.Controls.Projects.AppLogic;

public class DocumentSubTypeToStringConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		return (DocumentSubTypes)value switch
		{
			DocumentSubTypes.PurchasesDeliveryNote => Resources.IDS_DELIVERYNOTE, 
			DocumentSubTypes.PurchasesInvoice => Resources.IDS_INVOICE, 
			DocumentSubTypes.PurchasesOffer => Resources.IDS_OFFER, 
			DocumentSubTypes.PurchasesOrder => Resources.IDS_ORDER, 
			DocumentSubTypes.PurchasesPhase => Resources.IDS_PHASE, 
			DocumentSubTypes.SalesDeliveryNote => Resources.IDS_DELIVERYNOTE, 
			DocumentSubTypes.SalesInvoice => Resources.IDS_INVOICE, 
			DocumentSubTypes.SalesOffer => Resources.IDS_OFFER, 
			DocumentSubTypes.SalesOrder => Resources.IDS_ORDER, 
			DocumentSubTypes.SalesPhase => Resources.IDS_PHASE, 
			_ => null, 
		};
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}
