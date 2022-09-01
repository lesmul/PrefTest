using System.ComponentModel;

namespace Preference.Wpf.Controls.Projects.AppLogic;

public class PrefProjectExpenditure : INotifyPropertyChanged
{
	private string m_strName = string.Empty;

	private string m_strKey = string.Empty;

	private double m_dCoefficientFactor;

	private double m_dResult;

	private double m_dSource;

	private enStatus m_eStatus;

	private PrefCollection<PrefProjectExpenditure> m_pParentCollection;

	public PrefCollection<PrefProjectExpenditure> ParentCollection
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

	public double Source
	{
		get
		{
			return m_dSource;
		}
		set
		{
			m_dSource = value;
			OnPropertyChanged("Source");
		}
	}

	public double Result => m_dResult;

	public enStatus Status
	{
		get
		{
			return m_eStatus;
		}
		set
		{
			m_eStatus = value;
		}
	}

	public string Key
	{
		get
		{
			return m_strKey;
		}
		set
		{
			m_strKey = value;
		}
	}

	public string Name
	{
		get
		{
			return m_strName;
		}
		set
		{
			m_strName = value;
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
			OnPropertyChanged("CoefficientAsPercentage");
		}
	}

	public double CoefficientAsPercentage
	{
		get
		{
			return m_dCoefficientFactor * 100.0;
		}
		set
		{
			m_dCoefficientFactor = value / 100.0;
			OnPropertyChanged("CoefficientAsPercentage");
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	protected void OnPropertyChanged(string propName)
	{
		m_dResult = m_dSource * m_dCoefficientFactor;
		if (this.PropertyChanged != null)
		{
			this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
			this.PropertyChanged(this, new PropertyChangedEventArgs("Result"));
		}
		if (ParentCollection != null && Key != "Total")
		{
			ParentCollection.Refresh();
		}
	}
}
