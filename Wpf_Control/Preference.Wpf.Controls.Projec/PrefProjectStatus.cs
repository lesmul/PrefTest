using System;
using System.ComponentModel;
using System.Globalization;

namespace Preference.Wpf.Controls.Projects.AppLogic;

public class PrefProjectStatus : INotifyPropertyChanged
{
	private int m_nAccepted;

	private int m_nEstimated;

	private int m_nOrdered;

	private int m_nPurchased;

	private int m_nStarted;

	private int m_nFinished;

	private int m_nSent;

	private int m_nDelivered;

	private int m_nMounted;

	private int _invoiced;

	private int m_nTotal;

	private int totalInstances;

	private double m_dTotalSales;

	private double m_dTotalPurchases;

	private double m_dTotalProduction;

	private double m_dTotalWorkforce;

	public double TotalWorkforce
	{
		get
		{
			return m_dTotalWorkforce;
		}
		set
		{
			m_dTotalWorkforce = value;
			OnPropertyChanged("TotalWorkforce");
		}
	}

	public double TotalProduction
	{
		get
		{
			return m_dTotalProduction;
		}
		set
		{
			m_dTotalProduction = value;
			OnPropertyChanged("TotalProduction");
		}
	}

	public double TotalPurchases
	{
		get
		{
			return m_dTotalPurchases;
		}
		set
		{
			m_dTotalPurchases = value;
			OnPropertyChanged("TotalPurchases");
		}
	}

	public double TotalSales
	{
		get
		{
			return m_dTotalSales;
		}
		set
		{
			m_dTotalSales = value;
			OnPropertyChanged("TotalSales");
		}
	}

	public int Accepted
	{
		get
		{
			return m_nAccepted;
		}
		set
		{
			m_nAccepted = value;
			OnPropertyChanged("Accepted");
		}
	}

	public string AcceptedAsPercentage => ConvertToStarLength(m_nAccepted);

	public int Estimated
	{
		get
		{
			return m_nEstimated;
		}
		set
		{
			m_nEstimated = value;
			OnPropertyChanged("Estimated");
		}
	}

	public string EstimatedAsPercentage => ConvertToStarLength(m_nEstimated);

	public int Ordered
	{
		get
		{
			return m_nOrdered;
		}
		set
		{
			m_nOrdered = value;
			OnPropertyChanged("Ordered");
		}
	}

	public string OrderedAsPercentage => ConvertToStarLength(m_nOrdered);

	public int Purchased
	{
		get
		{
			return m_nPurchased;
		}
		set
		{
			m_nPurchased = value;
			OnPropertyChanged("Purchased");
		}
	}

	public string PurchasedAsPercentage => ConvertToStarLength(m_nPurchased);

	public int Started
	{
		get
		{
			return m_nStarted;
		}
		set
		{
			m_nStarted = value;
			OnPropertyChanged("Started");
		}
	}

	public string StartedAsPercentage => ConvertToStarLength(m_nStarted);

	public int Finished
	{
		get
		{
			return m_nFinished;
		}
		set
		{
			m_nFinished = value;
			OnPropertyChanged("Finished");
		}
	}

	public string FinishedAsPercentage => ConvertToStarLength(m_nFinished);

	public int Sent
	{
		get
		{
			return m_nSent;
		}
		set
		{
			m_nSent = value;
			OnPropertyChanged("Sent");
		}
	}

	public string SentAsPercentage => ConvertToStarLength(m_nSent);

	public int Delivered
	{
		get
		{
			return m_nDelivered;
		}
		set
		{
			m_nDelivered = value;
			OnPropertyChanged("Delivered");
		}
	}

	public string DeliveredAsPercentage => ConvertToStarLength(m_nDelivered);

	public int Mounted
	{
		get
		{
			return m_nMounted;
		}
		set
		{
			m_nMounted = value;
			OnPropertyChanged("Mounted");
		}
	}

	public string MountedAsPercentage => ConvertToStarLength(m_nMounted);

	public int Invoiced
	{
		get
		{
			return _invoiced;
		}
		set
		{
			_invoiced = value;
		}
	}

	public int TotalInstances
	{
		get
		{
			return totalInstances;
		}
		set
		{
			totalInstances = value;
			OnPropertyChanged("TotalInstances");
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	private string ConvertToStarLength(int nQuantity)
	{
		double num = 0.0;
		if (m_nTotal > 0)
		{
			num = (double)nQuantity / (double)m_nTotal;
		}
		return Convert.ToString(num, NumberFormatInfo.InvariantInfo) + "*";
	}

	protected void OnPropertyChanged(string propName)
	{
		switch (propName)
		{
		case "Accepted":
		case "Estimated":
		case "Purchased":
		case "Started":
		case "Finished":
		case "Sent":
		case "Delivered":
		case "Mounted":
			m_nTotal = m_nAccepted + m_nEstimated + m_nPurchased + m_nStarted + m_nFinished + m_nSent + m_nDelivered + m_nMounted;
			OnPropertyChanged("AcceptedAsPercentage");
			OnPropertyChanged("EstimatedAsPercentage");
			OnPropertyChanged("PurchasedAsPercentage");
			OnPropertyChanged("StartedAsPercentage");
			OnPropertyChanged("FinishedAsPercentage");
			OnPropertyChanged("SentAsPercentage");
			OnPropertyChanged("DeliveredAsPercentage");
			OnPropertyChanged("MountedAsPercentage");
			break;
		}
		if (this.PropertyChanged != null)
		{
			this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
		}
	}
}
