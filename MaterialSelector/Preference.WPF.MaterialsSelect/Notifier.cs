using System;
using System.ComponentModel;
using System.Windows.Threading;

namespace Preference.WPF.MaterialsSelector.Core;

[Serializable]
public abstract class Notifier : INotifyPropertyChanged
{
	private Dispatcher _Dispatcher;

	[field: NonSerialized]
	public event PropertyChangedEventHandler PropertyChanged;

	public Notifier()
	{
		_Dispatcher = null;
	}

	public Notifier(Dispatcher dispatcher)
	{
		_Dispatcher = dispatcher;
	}

	protected virtual void OnPropertyChanged(string propertyName)
	{
		if (this.PropertyChanged != null)
		{
			this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
	}

	public void InvokeOnUI(Action action)
	{
		if (_Dispatcher.CheckAccess())
		{
			action();
		}
		else
		{
			_Dispatcher.BeginInvoke(action);
		}
	}
}
