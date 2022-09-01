using System;
using System.ComponentModel;

namespace Preference.Wpf.Controls.Expenses.Models;

public class ExpensesDocItem : INotifyPropertyChanged
{
	private double _quantity = 1.0;

	private double _amount;

	private double _tax = 1.0;

	private double _unitPrice;

	private long _lineCode;

	private long _sortOrder;

	private Guid _detailId = Guid.Empty;

	private string _concept = string.Empty;

	private string _description = string.Empty;

	private string _type = string.Empty;

	private ExpensesDocItemCollection _parentCollection;

	public ExpensesDocItemCollection ParentCollection
	{
		get
		{
			return _parentCollection;
		}
		set
		{
			_parentCollection = value;
		}
	}

	public double UnitPrice
	{
		get
		{
			return _unitPrice;
		}
		set
		{
			_unitPrice = value;
			OnPropertyChanged("UnitPrice");
		}
	}

	public double Tax
	{
		get
		{
			return (_tax - 1.0) * 100.0;
		}
		set
		{
			_tax = value / 100.0 + 1.0;
			OnPropertyChanged("Tax");
		}
	}

	public double TaxFactor
	{
		get
		{
			return _tax;
		}
		set
		{
			_tax = value;
			OnPropertyChanged("Tax");
		}
	}

	public double Amount
	{
		get
		{
			return _amount;
		}
		set
		{
			_amount = value;
			OnPropertyChanged("Amount");
		}
	}

	public double Quantity
	{
		get
		{
			return _quantity;
		}
		set
		{
			_quantity = value;
			OnPropertyChanged("Quantity");
		}
	}

	public long LineCode
	{
		get
		{
			return _lineCode;
		}
		set
		{
			_lineCode = value;
		}
	}

	public long SortOrder
	{
		get
		{
			return _sortOrder;
		}
		set
		{
			_sortOrder = value;
		}
	}

	public Guid DetailId
	{
		get
		{
			return _detailId;
		}
		set
		{
			_detailId = value;
		}
	}

	public string Concept
	{
		get
		{
			return _concept;
		}
		set
		{
			_concept = value;
			OnPropertyChanged("Concept");
		}
	}

	public string Description
	{
		get
		{
			return _description;
		}
		set
		{
			_description = value;
			OnPropertyChanged("Description");
		}
	}

	public string Type
	{
		get
		{
			return _type;
		}
		set
		{
			_type = value;
			OnPropertyChanged("Type");
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	protected void OnPropertyChanged(string propName)
	{
		switch (propName)
		{
		case "UnitPrice":
		case "Quantity":
		case "Tax":
			Amount = UnitPrice * Quantity * TaxFactor;
			break;
		}
		if (this.PropertyChanged != null)
		{
			this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
		}
		if (_parentCollection != null)
		{
			_parentCollection.Refresh();
		}
	}
}
