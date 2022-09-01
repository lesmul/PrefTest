using System;
using System.Collections.Generic;
using System.ComponentModel;
using Preference.Wpf.Controls.Properties;

namespace Preference.Wpf.Controls.Projects.AppLogic;

public class PrefProjectPerGroupExpenditure : INotifyPropertyChanged
{
	private string m_strColor = string.Empty;

	private PrefGroup m_pGroup;

	private enStatus m_eStatusProviderDiscount;

	private enStatus m_eStatusSalesIncrement;

	private double m_dOriginCost;

	private double m_dEffectiveCost;

	private double purchasePrice;

	private double m_dSale;

	private double m_dCoefficientFactor = 1.0;

	private double m_dProviderDiscountFactor = 1.0;

	private double m_dOriginalCoefficient = 1.0;

	private double m_dOriginalProviderDiscount = 1.0;

	private bool m_bIsCoefficientOverriden;

	private bool m_bIsProviderDiscountOverriden;

	private bool m_bIsProviderDiscountEditable;

	private bool m_bIsCostIncrementEditable;

	private bool m_bIsSalesIncrementEditable;

	private bool m_bIsAggregate;

	private double _Threshold = 0.0001;

	private PrefCollection<PrefProjectPerGroupExpenditure> m_pParentCollection;

	private List<PrefProjectPerGroupExpenditure> _listColorsRedefined;

	private double _PreviousCoefficient = 1.0;

	private double _PreviousProviderDiscount = 1.0;

	private bool _IsCoefficientModified;

	private bool _IsProviderDiscountModified;

	private bool _IsSaleAmountFixed;

	private enStatus statusRemnantIncrement;

	private double remnantIncrementFactor = 1.0;

	private double originalRemnantIncrement = 1.0;

	private bool isRemnantIncrementOverriden;

	private double previousRemnantIncrement;

	public double PreviousRemnantIncrement
	{
		get
		{
			return previousRemnantIncrement;
		}
		set
		{
			previousRemnantIncrement = value;
		}
	}

	public bool IsRemnantIncrementOverriden => isRemnantIncrementOverriden;

	public enStatus StatusRemnantIncrement
	{
		get
		{
			return statusRemnantIncrement;
		}
		set
		{
			statusRemnantIncrement = value;
			if (m_pParentCollection != null && (value == enStatus.Modified || value == enStatus.Deleted))
			{
				m_pParentCollection.CollectionStatus = enStatus.Modified;
			}
		}
	}

	public double RemnantIncrementAsFactor
	{
		get
		{
			return remnantIncrementFactor;
		}
		set
		{
			if (remnantIncrementFactor == value)
			{
				return;
			}
			remnantIncrementFactor = value;
			if (Math.Abs(remnantIncrementFactor - previousRemnantIncrement) > _Threshold)
			{
				StatusRemnantIncrement = ((remnantIncrementFactor != originalRemnantIncrement) ? enStatus.Modified : enStatus.Deleted);
			}
			isRemnantIncrementOverriden = Math.Abs(remnantIncrementFactor - originalRemnantIncrement) > _Threshold;
			if (_listColorsRedefined != null && Color == "" && m_pParentCollection != null)
			{
				foreach (PrefProjectPerGroupExpenditure item in m_pParentCollection)
				{
					if (!(item.Color == "") && item.Group == Group && !item.IsCostIncrementEditable)
					{
						item.RemnantIncrementAsFactor = remnantIncrementFactor;
					}
				}
			}
			OnPropertyChanged("RemnantIncrementAsPercentage");
		}
	}

	public double OriginalRemnantIncrement
	{
		get
		{
			return originalRemnantIncrement;
		}
		set
		{
			originalRemnantIncrement = value;
			isRemnantIncrementOverriden = Math.Abs(remnantIncrementFactor - originalRemnantIncrement) > _Threshold;
		}
	}

	public double RemnantIncrementAsPercentage
	{
		get
		{
			return (remnantIncrementFactor - 1.0) * 100.0;
		}
		set
		{
			RemnantIncrementAsFactor = value / 100.0 + 1.0;
		}
	}

	public bool IsSaleAmountFixed
	{
		get
		{
			return _IsSaleAmountFixed;
		}
		set
		{
			_IsSaleAmountFixed = value;
		}
	}

	public double PreviousCoefficient
	{
		get
		{
			return _PreviousCoefficient;
		}
		set
		{
			_PreviousCoefficient = value;
		}
	}

	public double PreviousProviderDiscount
	{
		get
		{
			return _PreviousProviderDiscount;
		}
		set
		{
			_PreviousProviderDiscount = value;
		}
	}

	public bool IsLabourCost => m_pGroup.Supplier == Resources.IDS_WORKFORCE;

	public bool IsWithoutGroup => m_pGroup.Supplier == Resources.IDS_WITHOUTGROUP;

	public bool IsAggregate
	{
		get
		{
			return m_bIsAggregate;
		}
		set
		{
			m_bIsAggregate = value;
		}
	}

	public PrefCollection<PrefProjectPerGroupExpenditure> ParentCollection
	{
		get
		{
			return m_pParentCollection;
		}
		set
		{
			m_pParentCollection = value;
		}
	}

	public bool IsCoefficientOverriden => m_bIsCoefficientOverriden;

	public bool IsProviderDiscountOverriden => m_bIsProviderDiscountOverriden;

	public bool IsCoefficientModified => _IsCoefficientModified;

	public bool IsProviderDiscountModified => _IsProviderDiscountModified;

	public bool IsProviderDiscountEditable
	{
		get
		{
			return m_bIsProviderDiscountEditable;
		}
		set
		{
			m_bIsProviderDiscountEditable = value;
		}
	}

	public bool IsCostIncrementEditable
	{
		get
		{
			return m_bIsCostIncrementEditable;
		}
		set
		{
			m_bIsCostIncrementEditable = value;
		}
	}

	public bool IsSalesIncrementEditable
	{
		get
		{
			return m_bIsSalesIncrementEditable;
		}
		set
		{
			m_bIsSalesIncrementEditable = value;
		}
	}

	public double OriginalCoefficient
	{
		get
		{
			return m_dOriginalCoefficient;
		}
		set
		{
			m_dOriginalCoefficient = value;
			m_bIsCoefficientOverriden = Math.Abs(m_dCoefficientFactor - m_dOriginalCoefficient) > _Threshold;
		}
	}

	public double OriginalProviderDiscount
	{
		get
		{
			return m_dOriginalProviderDiscount;
		}
		set
		{
			m_dOriginalProviderDiscount = value;
			m_bIsProviderDiscountOverriden = Math.Abs(m_dProviderDiscountFactor - m_dOriginalProviderDiscount) > _Threshold;
		}
	}

	public double ProviderDiscountAsPercentage
	{
		get
		{
			return (m_dProviderDiscountFactor - 1.0) * -100.0;
		}
		set
		{
			ProviderDiscountAsFactor = (0.0 - value) / 100.0 + 1.0;
		}
	}

	public double ProviderDiscountAsFactor
	{
		get
		{
			return m_dProviderDiscountFactor;
		}
		set
		{
			m_dProviderDiscountFactor = value;
			_IsProviderDiscountModified = Math.Abs(m_dProviderDiscountFactor - _PreviousProviderDiscount) > _Threshold;
			if (_IsProviderDiscountModified)
			{
				StatusProviderDiscount = ((m_dProviderDiscountFactor != m_dOriginalProviderDiscount) ? enStatus.Modified : enStatus.Deleted);
			}
			m_bIsProviderDiscountOverriden = Math.Abs(m_dProviderDiscountFactor - m_dOriginalProviderDiscount) > _Threshold;
			if (_listColorsRedefined != null && Color == "" && m_pParentCollection != null)
			{
				foreach (PrefProjectPerGroupExpenditure item in m_pParentCollection)
				{
					if (!(item.Color == "") && item.Group == Group && !item.IsProviderDiscountEditable)
					{
						item.ProviderDiscountAsFactor = m_dProviderDiscountFactor;
					}
				}
			}
			OnPropertyChanged("ProviderDiscountAsPercentage");
		}
	}

	public double EffectiveCost
	{
		get
		{
			return m_dEffectiveCost;
		}
		set
		{
			m_dEffectiveCost = value;
			OnPropertyChanged("EffectiveCost");
		}
	}

	public double PurchasePrice
	{
		get
		{
			return purchasePrice;
		}
		set
		{
			purchasePrice = value;
			OnPropertyChanged("PurchasePrice");
		}
	}

	public double Sale => m_dSale;

	public double OriginCost
	{
		get
		{
			return m_dOriginCost;
		}
		set
		{
			m_dOriginCost = value;
			OnPropertyChanged("OriginCost");
		}
	}

	public enStatus StatusProviderDiscount
	{
		get
		{
			return m_eStatusProviderDiscount;
		}
		set
		{
			m_eStatusProviderDiscount = value;
			if (m_pParentCollection != null && (value == enStatus.Modified || value == enStatus.Deleted))
			{
				m_pParentCollection.CollectionStatus = enStatus.Modified;
			}
		}
	}

	public enStatus StatusSalesIncrement
	{
		get
		{
			return m_eStatusSalesIncrement;
		}
		set
		{
			m_eStatusSalesIncrement = value;
			if (m_pParentCollection != null && (value == enStatus.Modified || value == enStatus.Deleted))
			{
				m_pParentCollection.CollectionStatus = enStatus.Modified;
			}
		}
	}

	public List<PrefProjectPerGroupExpenditure> ListColorsRedefined
	{
		get
		{
			return _listColorsRedefined;
		}
		set
		{
			_listColorsRedefined = value;
		}
	}

	public string Name
	{
		get
		{
			if (m_pGroup == null)
			{
				return string.Empty;
			}
			return m_pGroup.Name;
		}
	}

	public string Color
	{
		get
		{
			return m_strColor;
		}
		set
		{
			m_strColor = value;
		}
	}

	public PrefGroup Group
	{
		get
		{
			return m_pGroup;
		}
		set
		{
			m_pGroup = value;
		}
	}

	public double CoefficientAsFactor
	{
		get
		{
			return m_dCoefficientFactor;
		}
		set
		{
			m_dCoefficientFactor = value;
			_IsCoefficientModified = Math.Abs(m_dCoefficientFactor - _PreviousCoefficient) > _Threshold;
			if (_IsCoefficientModified)
			{
				StatusSalesIncrement = ((m_dCoefficientFactor != m_dOriginalCoefficient || !_IsCoefficientModified) ? enStatus.Modified : enStatus.Deleted);
			}
			m_bIsCoefficientOverriden = Math.Abs(m_dCoefficientFactor - m_dOriginalCoefficient) > _Threshold;
			if (_listColorsRedefined != null && Color == "" && m_pParentCollection != null)
			{
				foreach (PrefProjectPerGroupExpenditure item in _listColorsRedefined)
				{
					if (!(item.Color == "") && item.Group == Group && !item.IsSalesIncrementEditable)
					{
						item.CoefficientAsFactor = m_dCoefficientFactor;
					}
				}
			}
			OnPropertyChanged("CoefficientAsPercentage");
		}
	}

	public double CoefficientAsPercentage
	{
		get
		{
			return (m_dCoefficientFactor - 1.0) * 100.0;
		}
		set
		{
			CoefficientAsFactor = value / 100.0 + 1.0;
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	protected void OnPropertyChanged(string propName)
	{
		if (m_bIsAggregate || m_dProviderDiscountFactor == 0.0 || remnantIncrementFactor == 0.0)
		{
			return;
		}
		if (propName == "EffectiveCost")
		{
			m_dOriginCost = m_dEffectiveCost / (m_dProviderDiscountFactor * remnantIncrementFactor);
		}
		else
		{
			m_dEffectiveCost = m_dOriginCost * m_dProviderDiscountFactor * remnantIncrementFactor;
		}
		if (propName == "PurchasePrice")
		{
			m_dOriginCost = m_dEffectiveCost / m_dProviderDiscountFactor;
		}
		else
		{
			purchasePrice = m_dOriginCost * m_dProviderDiscountFactor;
		}
		if ((propName == "RemnantIncrementAsPercentage" || propName == "ProviderDiscountAsPercentage") && _IsSaleAmountFixed)
		{
			double num = previousRemnantIncrement * _PreviousProviderDiscount * m_dOriginCost * _PreviousCoefficient;
			if (m_dEffectiveCost != 0.0)
			{
				CoefficientAsFactor = num / m_dEffectiveCost;
			}
		}
		m_dSale = m_dEffectiveCost * m_dCoefficientFactor;
		if (this.PropertyChanged != null)
		{
			this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
			this.PropertyChanged(this, new PropertyChangedEventArgs("EffectiveCost"));
			this.PropertyChanged(this, new PropertyChangedEventArgs("PurchasePrice"));
			this.PropertyChanged(this, new PropertyChangedEventArgs("OriginCost"));
			this.PropertyChanged(this, new PropertyChangedEventArgs("Sale"));
			this.PropertyChanged(this, new PropertyChangedEventArgs("IsProviderDiscountOverriden"));
			this.PropertyChanged(this, new PropertyChangedEventArgs("IsCoefficientOverriden"));
			this.PropertyChanged(this, new PropertyChangedEventArgs("IsRemnantIncrementOverriden"));
		}
		if (ParentCollection != null)
		{
			ParentCollection.Refresh();
		}
	}
}
