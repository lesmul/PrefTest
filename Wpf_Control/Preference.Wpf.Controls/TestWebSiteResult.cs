using System.ComponentModel;

namespace Preference.Wpf.Controls;

public class TestWebSiteResult : INotifyPropertyChanged
{
	private bool? _areCertificatesValid;

	private bool? _arePortsOpen;

	private bool? _areSitesReachables;

	private bool? _canResolveUris;

	private bool? _isConnectedToInternet;

	private bool? _isTimeSynchronized;

	private bool _areCertificatesValidTestRunning;

	private bool _arePortsOpenTestRunning;

	private bool _areSitesReachablesTestRunning;

	private bool _canResolveUrisTestRunning;

	private bool _isConnectedToInternetTestRunning;

	private bool _isTestRunning;

	private bool _isTimeSynchronizedTestRunning;

	private string _extendedInfo;

	public bool? AreCertificatesValid
	{
		get
		{
			return _areCertificatesValid;
		}
		set
		{
			if (value != _areCertificatesValid)
			{
				_areCertificatesValid = value;
				NotifyPropertyChanged("AreCertificatesValid");
			}
		}
	}

	public bool AreCertificatesValidTestRunning
	{
		get
		{
			return _areCertificatesValidTestRunning;
		}
		set
		{
			if (value != AreCertificatesValidTestRunning)
			{
				_areCertificatesValidTestRunning = value;
				NotifyPropertyChanged("AreCertificatesValidTestRunning");
			}
		}
	}

	public bool? ArePortsOpen
	{
		get
		{
			return _arePortsOpen;
		}
		set
		{
			if (value != _arePortsOpen)
			{
				_arePortsOpen = value;
				NotifyPropertyChanged("ArePortsOpen");
			}
		}
	}

	public bool ArePortsOpenTestRunning
	{
		get
		{
			return _arePortsOpenTestRunning;
		}
		set
		{
			if (value != _arePortsOpenTestRunning)
			{
				_arePortsOpenTestRunning = value;
				NotifyPropertyChanged("ArePortsOpenTestRunning");
			}
		}
	}

	public bool? AreSitesReachables
	{
		get
		{
			return _areSitesReachables;
		}
		set
		{
			if (value != _areSitesReachables)
			{
				_areSitesReachables = value;
				NotifyPropertyChanged("AreSitesReachables");
			}
		}
	}

	public bool AreSitesReachablesTestRunning
	{
		get
		{
			return _areSitesReachablesTestRunning;
		}
		set
		{
			if (value != _areSitesReachablesTestRunning)
			{
				_areSitesReachablesTestRunning = value;
				NotifyPropertyChanged("AreSitesReachablesTestRunning");
			}
		}
	}

	public bool? CanResolveUris
	{
		get
		{
			return _canResolveUris;
		}
		set
		{
			if (value != _canResolveUris)
			{
				_canResolveUris = value;
				NotifyPropertyChanged("CanResolveUris");
			}
		}
	}

	public bool CanResolveUrisTestRunning
	{
		get
		{
			return _canResolveUrisTestRunning;
		}
		set
		{
			if (value != _canResolveUrisTestRunning)
			{
				_canResolveUrisTestRunning = value;
				NotifyPropertyChanged("CanResolveUrisTestRunning");
			}
		}
	}

	public string ExtendedInfo
	{
		get
		{
			return _extendedInfo;
		}
		set
		{
			if (value != _extendedInfo)
			{
				_extendedInfo = value;
				NotifyPropertyChanged("ExtendedInfo");
			}
		}
	}

	public bool? IsConnectedToInternet
	{
		get
		{
			return _isConnectedToInternet;
		}
		set
		{
			if (value != _isConnectedToInternet)
			{
				_isConnectedToInternet = value;
				NotifyPropertyChanged("IsConnectedToInternet");
			}
		}
	}

	public bool IsConnectedToInternetTestRunning
	{
		get
		{
			return _isConnectedToInternetTestRunning;
		}
		set
		{
			if (value != IsConnectedToInternetTestRunning)
			{
				_isConnectedToInternetTestRunning = value;
				NotifyPropertyChanged("IsConnectedToInternetTestRunning");
			}
		}
	}

	public bool IsTestFinished => !IsTestRunning;

	public bool IsTestRunning
	{
		get
		{
			return _isTestRunning;
		}
		set
		{
			if (value != IsTestRunning)
			{
				_isTestRunning = value;
				NotifyPropertyChanged("IsTestRunning");
				NotifyPropertyChanged("IsTestFinished");
			}
		}
	}

	public bool? IsTimeSynchronized
	{
		get
		{
			return _isTimeSynchronized;
		}
		set
		{
			if (value != IsTimeSynchronized)
			{
				_isTimeSynchronized = value;
				NotifyPropertyChanged("IsTimeSynchronized");
			}
		}
	}

	public bool IsTimeSynchronizedTestRunning
	{
		get
		{
			return _isTimeSynchronizedTestRunning;
		}
		set
		{
			if (value != IsTimeSynchronizedTestRunning)
			{
				_isTimeSynchronizedTestRunning = value;
				NotifyPropertyChanged("IsTimeSynchronizedTestRunning");
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	public TestWebSiteResult()
	{
		Reset();
	}

	public void Reset()
	{
		AreCertificatesValid = null;
		AreCertificatesValidTestRunning = true;
		ArePortsOpen = null;
		ArePortsOpenTestRunning = true;
		AreSitesReachables = null;
		AreSitesReachablesTestRunning = true;
		CanResolveUris = null;
		CanResolveUrisTestRunning = true;
		IsConnectedToInternet = null;
		IsConnectedToInternetTestRunning = true;
		IsTimeSynchronized = null;
		IsTimeSynchronizedTestRunning = true;
		IsTestRunning = true;
		ExtendedInfo = "";
	}

	private void NotifyPropertyChanged(string strProperty)
	{
		if (this.PropertyChanged != null)
		{
			this.PropertyChanged(this, new PropertyChangedEventArgs(strProperty));
		}
	}
}
