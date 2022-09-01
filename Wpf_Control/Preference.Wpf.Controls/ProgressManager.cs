using System.ComponentModel;

namespace Preference.Wpf.Controls;

public class ProgressManager : INotifyPropertyChanged
{
	private bool? _success;

	public bool? Success
	{
		get
		{
			return _success;
		}
		set
		{
			if (value != _success)
			{
				_success = value;
				NotifyPropertyChanged("Success");
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	public ProgressManager()
	{
		Success = null;
	}

	private void NotifyPropertyChanged(string strProperty)
	{
		if (this.PropertyChanged != null)
		{
			this.PropertyChanged(this, new PropertyChangedEventArgs(strProperty));
		}
	}
}
