using System.ComponentModel;

namespace Preference.Wpf.Controls.Expenses.Models;

public class ExpensesDocItemType : INotifyPropertyChanged
{
	private long _code;

	private string _name;

	private string _description;

	public string Name
	{
		get
		{
			return _name;
		}
		set
		{
			_name = value;
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
		}
	}

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

	public event PropertyChangedEventHandler PropertyChanged;

	public override string ToString()
	{
		return _name;
	}

	public override bool Equals(object obj)
	{
		if (obj is ExpensesDocItemType)
		{
			ExpensesDocItemType expensesDocItemType = obj as ExpensesDocItemType;
			return Code == expensesDocItemType.Code;
		}
		return base.Equals(obj);
	}

	public override int GetHashCode()
	{
		return base.GetHashCode();
	}

	protected void OnPropertyChanged(string propName)
	{
		if (this.PropertyChanged != null)
		{
			this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
		}
	}
}
