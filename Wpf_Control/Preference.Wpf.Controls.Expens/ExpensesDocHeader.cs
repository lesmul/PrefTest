using System;
using System.ComponentModel;

namespace Preference.Wpf.Controls.Expenses.Models;

public class ExpensesDocHeader : INotifyPropertyChanged
{
	private long _number;

	private DateTime _documentDate = DateTime.Now;

	private Guid _documentId = Guid.Empty;

	private double _totalAmount;

	private double _taxAmount;

	private string _title = string.Empty;

	private string _currency;

	private long _costDriverCode;

	public long CostDriverCode
	{
		get
		{
			return _costDriverCode;
		}
		set
		{
			_costDriverCode = value;
			OnPropertyChanged("CostDriverCode");
		}
	}

	public string Currency
	{
		get
		{
			return _currency;
		}
		set
		{
			_currency = value;
			OnPropertyChanged("Currency");
		}
	}

	public string Title
	{
		get
		{
			return _title;
		}
		set
		{
			_title = value;
			OnPropertyChanged("Title");
		}
	}

	public double TaxAmount
	{
		get
		{
			return _taxAmount;
		}
		set
		{
			_taxAmount = value;
			OnPropertyChanged("TaxAmount");
		}
	}

	public double TotalAmount
	{
		get
		{
			return _totalAmount;
		}
		set
		{
			_totalAmount = value;
			OnPropertyChanged("TotalAmount");
		}
	}

	public long Number
	{
		get
		{
			return _number;
		}
		set
		{
			_number = value;
			OnPropertyChanged("Number");
		}
	}

	public DateTime DocumentDate
	{
		get
		{
			return _documentDate;
		}
		set
		{
			_documentDate = value;
			OnPropertyChanged("DocumentDate");
		}
	}

	public Guid DocumentId
	{
		get
		{
			return _documentId;
		}
		set
		{
			_documentId = value;
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	protected void OnPropertyChanged(string propName)
	{
		if (this.PropertyChanged != null)
		{
			this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
		}
	}

	internal void Clear()
	{
		Title = string.Empty;
		Currency = null;
		CostDriverCode = 0L;
	}
}
