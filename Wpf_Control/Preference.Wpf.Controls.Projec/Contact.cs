namespace Preference.Wpf.Controls.Projects.AppLogic;

public class Contact : Document
{
	private long _ContactCode;

	private string _CompanyName = string.Empty;

	private string _FullName = string.Empty;

	private string _Telephone = string.Empty;

	private string _Fax = string.Empty;

	private string _Email = string.Empty;

	private string _Address = string.Empty;

	private string _PostalCode = string.Empty;

	private string _City = string.Empty;

	private string _Province = string.Empty;

	private string _Country = string.Empty;

	public long ContactCode
	{
		get
		{
			return _ContactCode;
		}
		set
		{
			bool flag = _ContactCode != value;
			_ContactCode = value;
			if (flag)
			{
				OnPropertyChanged("ContactCode");
			}
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

	public string FullName
	{
		get
		{
			return _FullName;
		}
		set
		{
			_FullName = value;
			OnPropertyChanged("FullName");
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

	public string Address
	{
		get
		{
			return _Address;
		}
		set
		{
			_Address = value;
			OnPropertyChanged("Address");
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

	public override bool Equals(object obj)
	{
		if (obj is Contact)
		{
			if ((obj as Contact).ContactCode == ContactCode)
			{
				return true;
			}
			return false;
		}
		return base.Equals(obj);
	}

	public override int GetHashCode()
	{
		return base.GetHashCode();
	}

	public override string ToString()
	{
		return FullName;
	}
}
