using System;

namespace Preference.Wpf.Controls;

public class TreeEventArgs : EventArgs
{
	private TreeItem _item;

	public TreeItem Item
	{
		get
		{
			return _item;
		}
		set
		{
			_item = value;
		}
	}
}
