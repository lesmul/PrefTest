using System.ComponentModel;
using System.Windows;

namespace Preference.Wpf.Controls.Sales;

public class SalesDocHeaderInfoProperties : INotifyPropertyChanged
{
	private string _documentType;

	private short _numericdocumentType;

	private string _orderNumber;

	private string _numberVersion;

	private string _suborderNumber;

	private string _internalOrderCode;

	private string _dataVersion;

	private string _price;

	private Visibility _visibilityExpanderPrices;

	private string _finalPriceHeading;

	private string _sumModelsPriceHeading;

	private string _sumModelsAfterDiscountsPriceHeading;

	private string _sumModelsAfterTotalDiscountsPriceHeading;

	private string _sumAddWorksPriceHeading;

	private string _sumModelsAndAddWorksPriceHeading;

	private string _priceAfterSubTotalsHeading;

	private string _finalPrice;

	private string _sumModelsPrice;

	private string _sumModelsAfterDiscountsPrice;

	private string _sumModelsAfterTotalDiscountsPrice;

	private string _sumAddWorksPrice;

	private string _sumModelsAndAddWorksPrice;

	private string _priceAfterSubTotals;

	private bool _isModified;

	private short _severity;

	private string _projectCode;

	private short _pdeLight;

	public string DocumentType
	{
		get
		{
			return _documentType;
		}
		set
		{
			if (_documentType != value)
			{
				_documentType = value;
				NotifyPropertyChanged("DocumentType");
			}
		}
	}

	public short NumericDocumentType
	{
		get
		{
			return _numericdocumentType;
		}
		set
		{
			if (_numericdocumentType != value)
			{
				_numericdocumentType = value;
				NotifyPropertyChanged("NumericDocumentType");
			}
		}
	}

	public string NumberVersion
	{
		get
		{
			return _numberVersion;
		}
		set
		{
			if (_numberVersion != value)
			{
				_numberVersion = value;
				NotifyPropertyChanged("NumberVersion");
			}
		}
	}

	public string OrderNumber
	{
		get
		{
			return _orderNumber;
		}
		set
		{
			if (_orderNumber != value)
			{
				_orderNumber = value;
				NotifyPropertyChanged("OrderNumber");
			}
		}
	}

	public string SuborderNumber
	{
		get
		{
			return _suborderNumber;
		}
		set
		{
			if (_suborderNumber != value)
			{
				_suborderNumber = value;
				NotifyPropertyChanged("SuborderNumber");
			}
		}
	}

	public string InternalOrderCode
	{
		get
		{
			return _internalOrderCode;
		}
		set
		{
			if (_internalOrderCode != value)
			{
				_internalOrderCode = value;
				NotifyPropertyChanged("InternalOrderCode");
			}
		}
	}

	public string DataVersion
	{
		get
		{
			return _dataVersion;
		}
		set
		{
			if (_dataVersion != value)
			{
				_dataVersion = value;
				NotifyPropertyChanged("DataVersion");
			}
		}
	}

	public string Price
	{
		get
		{
			return _price;
		}
		set
		{
			if (_price != value)
			{
				_price = value;
				NotifyPropertyChanged("Price");
			}
		}
	}

	public Visibility ExpanderVisibility
	{
		get
		{
			return _visibilityExpanderPrices;
		}
		set
		{
			if (_visibilityExpanderPrices != value)
			{
				_visibilityExpanderPrices = value;
				NotifyPropertyChanged("ExpanderVisibility");
			}
		}
	}

	public string FinalPriceHeading
	{
		get
		{
			return _finalPriceHeading;
		}
		set
		{
			if (_finalPriceHeading != value)
			{
				_finalPriceHeading = value;
				NotifyPropertyChanged("FinalPriceHeading");
				NotifyPropertyChanged("CompositeFinalPrice");
			}
		}
	}

	public string FinalPrice
	{
		get
		{
			return _finalPrice;
		}
		set
		{
			if (_finalPrice != value)
			{
				_finalPrice = value;
				NotifyPropertyChanged("FinalPrice");
				NotifyPropertyChanged("CompositeFinalPrice");
			}
		}
	}

	public string CompositeFinalPrice => FinalPriceHeading + " " + FinalPrice;

	public string SumModelsPriceHeading
	{
		get
		{
			return _sumModelsPriceHeading;
		}
		set
		{
			if (_sumModelsPriceHeading != value)
			{
				_sumModelsPriceHeading = value;
				NotifyPropertyChanged("SumModelsPriceHeading");
			}
		}
	}

	public string SumModelsPrice
	{
		get
		{
			return _sumModelsPrice;
		}
		set
		{
			if (_sumModelsPrice != value)
			{
				_sumModelsPrice = value;
				NotifyPropertyChanged("SumModelsPrice");
			}
		}
	}

	public string SumModelsAfterDiscountsPriceHeading
	{
		get
		{
			return _sumModelsAfterDiscountsPriceHeading;
		}
		set
		{
			if (_sumModelsAfterDiscountsPriceHeading != value)
			{
				_sumModelsAfterDiscountsPriceHeading = value;
				NotifyPropertyChanged("SumModelsAfterDiscountsPriceHeading");
			}
		}
	}

	public string SumModelsAfterDiscountsPrice
	{
		get
		{
			return _sumModelsAfterDiscountsPrice;
		}
		set
		{
			if (_sumModelsAfterDiscountsPrice != value)
			{
				_sumModelsAfterDiscountsPrice = value;
				NotifyPropertyChanged("SumModelsAfterDiscountsPrice");
			}
		}
	}

	public string SumModelsAfterTotalDiscountsPriceHeading
	{
		get
		{
			return _sumModelsAfterTotalDiscountsPriceHeading;
		}
		set
		{
			if (_sumModelsAfterTotalDiscountsPriceHeading != value)
			{
				_sumModelsAfterTotalDiscountsPriceHeading = value;
				NotifyPropertyChanged("SumModelsAfterTotalDiscountsPriceHeading");
			}
		}
	}

	public string SumModelsAfterTotalDiscountsPrice
	{
		get
		{
			return _sumModelsAfterTotalDiscountsPrice;
		}
		set
		{
			if (_sumModelsAfterTotalDiscountsPrice != value)
			{
				_sumModelsAfterTotalDiscountsPrice = value;
				NotifyPropertyChanged("SumModelsAfterTotalDiscountsPrice");
			}
		}
	}

	public string SumAddWorksPriceHeading
	{
		get
		{
			return _sumAddWorksPriceHeading;
		}
		set
		{
			if (_sumAddWorksPriceHeading != value)
			{
				_sumAddWorksPriceHeading = value;
				NotifyPropertyChanged("SumAddWorksPriceHeading");
			}
		}
	}

	public string SumAddWorksPrice
	{
		get
		{
			return _sumAddWorksPrice;
		}
		set
		{
			if (_sumAddWorksPrice != value)
			{
				_sumAddWorksPrice = value;
				NotifyPropertyChanged("SumAddWorksPrice");
			}
		}
	}

	public string SumModelsAndAddWorksPriceHeading
	{
		get
		{
			return _sumModelsAndAddWorksPriceHeading;
		}
		set
		{
			if (_sumModelsAndAddWorksPriceHeading != value)
			{
				_sumModelsAndAddWorksPriceHeading = value;
				NotifyPropertyChanged("SumModelsAndAddWorksPriceHeading");
			}
		}
	}

	public string SumModelsAndAddWorksPrice
	{
		get
		{
			return _sumModelsAndAddWorksPrice;
		}
		set
		{
			if (_sumModelsAndAddWorksPrice != value)
			{
				_sumModelsAndAddWorksPrice = value;
				NotifyPropertyChanged("SumModelsAndAddWorksPrice");
			}
		}
	}

	public string PriceAfterSubTotalsHeading
	{
		get
		{
			return _priceAfterSubTotalsHeading;
		}
		set
		{
			if (_priceAfterSubTotalsHeading != value)
			{
				_priceAfterSubTotalsHeading = value;
				NotifyPropertyChanged("PriceAfterSubTotalsHeading");
			}
		}
	}

	public string PriceAfterSubTotals
	{
		get
		{
			return _priceAfterSubTotals;
		}
		set
		{
			if (_priceAfterSubTotals != value)
			{
				_priceAfterSubTotals = value;
				NotifyPropertyChanged("PriceAfterSubTotals");
			}
		}
	}

	public bool IsModified
	{
		get
		{
			return _isModified;
		}
		set
		{
			if (_isModified != value)
			{
				_isModified = value;
				NotifyPropertyChanged("IsModified");
			}
		}
	}

	public short Severity
	{
		get
		{
			return _severity;
		}
		set
		{
			if (_severity != value)
			{
				_severity = value;
				NotifyPropertyChanged("Severity");
			}
		}
	}

	public string ProjectCode
	{
		get
		{
			return _projectCode;
		}
		set
		{
			if (_projectCode != value)
			{
				_projectCode = value;
				NotifyPropertyChanged("ProjectCode");
			}
		}
	}

	public short PDELight
	{
		get
		{
			return _pdeLight;
		}
		set
		{
			if (_pdeLight != value)
			{
				_pdeLight = value;
				NotifyPropertyChanged("PDELight");
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	protected virtual void NotifyPropertyChanged(string strPropertyName)
	{
		PropertyChangedEventArgs args = new PropertyChangedEventArgs(strPropertyName);
		OnPropertyChanged(args);
	}

	protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
	{
		this.PropertyChanged?.Invoke(this, args);
	}
}
