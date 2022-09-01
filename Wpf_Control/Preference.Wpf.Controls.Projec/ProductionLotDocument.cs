namespace Preference.Wpf.Controls.Projects.AppLogic;

public class ProductionLotDocument : Document
{
	private int _iNumber;

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
}
