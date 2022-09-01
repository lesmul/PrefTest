using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Preference.Wpf.Controls.Expenses.Models;

public class PrefCollection<T> : ObservableCollection<T>
{
	public event NotifyCollectionChangedEventHandler CollectionRefresh;

	protected virtual void OnCollectionRefresh(NotifyCollectionChangedEventArgs e)
	{
		if (this.CollectionRefresh != null)
		{
			this.CollectionRefresh(this, e);
		}
	}

	public void Refresh()
	{
		OnCollectionRefresh(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
	}
}
