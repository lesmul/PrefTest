using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using Preference.Wpf.Controls.Projects.Views;

namespace Preference.Wpf.Controls.Projects.AppLogic;

public class DocumentSubTypeToImageConverter : IValueConverter
{
	private static DrawingImage _iconDocuments;

	private static DrawingImage _iconSales;

	private static DrawingImage _iconContacts;

	private static DrawingImage _iconPurchases;

	private static DrawingImage _iconProject;

	private static DrawingImage _iconExpenses;

	private static DrawingImage _iconWarehouseDocumentInOut;

	private static DrawingImage _iconProductionStatus;

	private static DrawingImage _iconShipping;

	private static DrawingImage IconDocuments
	{
		get
		{
			if (_iconDocuments == null)
			{
				_iconDocuments = new ProjectView().TryFindResource("IconDocuments") as DrawingImage;
			}
			return _iconDocuments;
		}
	}

	private static DrawingImage IconSales
	{
		get
		{
			if (_iconSales == null)
			{
				_iconSales = new ProjectView().TryFindResource("IconSales") as DrawingImage;
			}
			return _iconSales;
		}
	}

	private static DrawingImage IconContacts
	{
		get
		{
			if (_iconContacts == null)
			{
				_iconContacts = new ProjectView().TryFindResource("IconContacts") as DrawingImage;
			}
			return _iconContacts;
		}
	}

	private static DrawingImage IconPurchases
	{
		get
		{
			if (_iconPurchases == null)
			{
				_iconPurchases = new ProjectView().TryFindResource("IconPurchases") as DrawingImage;
			}
			return _iconPurchases;
		}
	}

	private static DrawingImage IconProject
	{
		get
		{
			if (_iconProject == null)
			{
				_iconProject = new ProjectView().TryFindResource("IconProject") as DrawingImage;
			}
			return _iconProject;
		}
	}

	private static DrawingImage IconExpenses
	{
		get
		{
			if (_iconExpenses == null)
			{
				_iconExpenses = new ProjectView().TryFindResource("IconExpenses") as DrawingImage;
			}
			return _iconExpenses;
		}
	}

	private static DrawingImage IconWarehouseDocumentInOut
	{
		get
		{
			if (_iconWarehouseDocumentInOut == null)
			{
				_iconWarehouseDocumentInOut = new ProjectView().TryFindResource("IconWarehouseDocumentInOut") as DrawingImage;
			}
			return _iconWarehouseDocumentInOut;
		}
	}

	private static DrawingImage IconProductionStatus
	{
		get
		{
			if (_iconProductionStatus == null)
			{
				_iconProductionStatus = new ProjectView().TryFindResource("IconProductionStatus") as DrawingImage;
			}
			return _iconProductionStatus;
		}
	}

	private static DrawingImage IconShipping
	{
		get
		{
			if (_iconShipping == null)
			{
				_iconShipping = new ProjectView().TryFindResource("IconShipping") as DrawingImage;
			}
			return _iconShipping;
		}
	}

	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		return (DocumentSubTypes)value switch
		{
			DocumentSubTypes.Expense => IconExpenses, 
			DocumentSubTypes.PurchasesDeliveryNote => IconPurchases, 
			DocumentSubTypes.PurchasesInvoice => IconPurchases, 
			DocumentSubTypes.PurchasesOffer => IconPurchases, 
			DocumentSubTypes.PurchasesOrder => IconPurchases, 
			DocumentSubTypes.PurchasesPhase => IconPurchases, 
			DocumentSubTypes.SalesDeliveryNote => IconSales, 
			DocumentSubTypes.SalesInvoice => IconSales, 
			DocumentSubTypes.SalesOffer => IconSales, 
			DocumentSubTypes.SalesOrder => IconSales, 
			DocumentSubTypes.SalesPhase => IconSales, 
			_ => null, 
		};
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}
