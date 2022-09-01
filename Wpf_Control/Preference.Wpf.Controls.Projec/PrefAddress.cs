using System.ComponentModel;

namespace Preference.Wpf.Controls.Projects.AppLogic;

public class PrefAddress : INotifyPropertyChanged
{
	private enAddressPurpose _Purpose;

	private string _CompanyName = string.Empty;

	private string _ContactPerson = string.Empty;

	private string _Telephone = string.Empty;

	private string _Fax = string.Empty;

	private string _Email = string.Empty;

	private string _Address1 = string.Empty;

	private string _Address2 = string.Empty;

	private string _PostalCode = string.Empty;

	private string _City = string.Empty;

	private string _Province = string.Empty;

	private string _Country = string.Empty;

	public enAddressPurpose Purpose
	{
		get
		{
			return _Purpose;
		}
		set
		{
			_Purpose = value;
			OnPropertyChanged("Purpose");
		}
	}

	public string CompanyName
	{
		get
		{
			return _CompanyName;
		}
		set
		{
			_CompanyName = value;
			OnPropertyChanged("CompanyName");
		}
	}

	public string ContactPerson
	{
		get
		{
			return _ContactPerson;
		}
		set
		{
			_ContactPerson = value;
			OnPropertyChanged("ContactPerson");
		}
	}

	public string Telephone
	{
		get
		{
			return _Telephone;
		}
		set
		{
			_Telephone = value;
			OnPropertyChanged("Telephone");
		}
	}

	public string Fax
	{
		get
		{
			return _Fax;
		}
		set
		{
			_Fax = value;
			OnPropertyChanged("Fax");
		}
	}

	public string Email
	{
		get
		{
			return _Email;
		}
		set
		{
			_Email = value;
			OnPropertyChanged("Email");
		}
	}

	public string Address1
	{
		get
		{
			return _Address1;
		}
		set
		{
			_Address1 = value;
			OnPropertyChanged("Address1");
		}
	}

	public string Address2
	{
		get
		{
			return _Address2;
		}
		set
		{
			_Address2 = value;
			OnPropertyChanged("Address2");
		}
	}

	public string PostalCode
	{
		get
		{
			return _PostalCode;
		}
		set
		{
			_PostalCode = value;
			OnPropertyChanged("PostalCode");
		}
	}

	public string City
	{
		get
		{
			return _City;
		}
		set
		{
			_City = value;
			OnPropertyChanged("City");
		}
	}

	public string Province
	{
		get
		{
			return _Province;
		}
		set
		{
			_Province = value;
			OnPropertyChanged("Province");
		}
	}

	public string Country
	{
		get
		{
			return _Country;
		}
		set
		{
			_Country = value;
			OnPropertyChanged("Country");
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

	public PrefAddress()
	{
	}

	public PrefAddress(enAddressPurpose purpose)
	{
		_Purpose = purpose;
	}
}
