using System;

namespace Preference.Wpf.Controls.Options;

public class TabEventArgs : EventArgs
{
	private object _sender;

	private int _nSelectedIndex;

	public object Sender
	{
		get
		{
			return _sender;
		}
		set
		{
			_sender = value;
		}
	}

	public int SelectedIndex
	{
		get
		{
			return _nSelectedIndex;
		}
		set
		{
			_nSelectedIndex = value;
		}
	}

	public TabEventArgs()
	{
	}

	public TabEventArgs(object sender, int nIndex)
	{
		Sender = sender;
		SelectedIndex = nIndex;
	}
}
