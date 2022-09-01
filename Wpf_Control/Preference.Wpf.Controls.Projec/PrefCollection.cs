using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Preference.Wpf.Controls.Projects.AppLogic;

public class PrefCollection<T> : ObservableCollection<T>
{
	private enStatus _CollectionStatus;

	public enStatus CollectionStatus
	{
		get
		{
			return _CollectionStatus;
		}
		set
		{
			_CollectionStatus = value;
		}
	}

	public event NotifyCollectionChangedEventHandler CollectionItemsChanged;

	protected virtual void OnCollectionItemsChanged(NotifyCollectionChangedEventArgs e)
	{
		if (this.CollectionItemsChanged != null)
		{
			this.CollectionItemsChanged(this, e);
		}
	}

	public void Refresh()
	{
		OnCollectionItemsChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
	}
}
