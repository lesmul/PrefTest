namespace Preference.Wpf.Controls.Expenses.Models;

public class PrefStaff
{
	private string _fullName = string.Empty;

	private long _code;

	public long Code
	{
		get
		{
			return _code;
		}
		set
		{
			_code = value;
		}
	}

	public string FullName
	{
		get
		{
			return _fullName;
		}
		set
		{
			_fullName = value;
		}
	}

	public override string ToString()
	{
		return _fullName;
	}
}
