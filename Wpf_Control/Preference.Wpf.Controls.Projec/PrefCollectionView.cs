using System.Collections;
using System.Collections.Specialized;
using System.Windows.Data;

namespace Preference.Wpf.Controls.Projects.AppLogic;

public class PrefCollectionView : CollectionView
{
	public PrefCollectionView(IEnumerable col)
		: base(col)
	{
	}

	protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
	{
		base.OnCollectionChanged(args);
	}
}
