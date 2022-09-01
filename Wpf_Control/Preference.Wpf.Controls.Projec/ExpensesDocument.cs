namespace Preference.Wpf.Controls.Projects.AppLogic;

public class ExpensesDocument : Document
{
	private int _iNumber;

	private string _strTitle = string.Empty;

	public int Number
	{
		get
		{
			return _iNumber;
		}
		set
		{
			if (_iNumber != value)
			{
				_iNumber = value;
				OnPropertyChanged("Number");
			}
		}
	}

	public string Title
	{
		get
		{
			return _strTitle;
		}
		set
		{
			if (_strTitle != value)
			{
				_strTitle = value;
				OnPropertyChanged("Title");
			}
		}
	}
}
