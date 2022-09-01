namespace Preference.Wpf.Controls.Projects.AppLogic;

public class Customer : Contact
{
	private Address _ShippingAddress;

	private Address _InvoicingAddress;

	private long _CustomerCode;

	public long CustomerCode
	{
		get
		{
			return _CustomerCode;
		}
		set
		{
			_CustomerCode = value;
		}
	}

	public Address ShippingAddress => _ShippingAddress;

	public Address InvoicingAddress => _InvoicingAddress;

	public Customer()
	{
		_ShippingAddress = new Address();
		_InvoicingAddress = new Address();
	}

	public override bool Equals(object obj)
	{
		if (obj is Customer)
		{
			if ((obj as Customer).CustomerCode == CustomerCode)
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
		return base.FullName;
	}
}
