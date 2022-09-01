using System.Collections.Generic;

namespace Preference.Wpf.Controls;

public abstract class JitmvViewerWin32Interop
{
	private static Dictionary<string, JitmvViewer> _viewers = new Dictionary<string, JitmvViewer>();

	public static JitmvViewer GetViewer(string strId)
	{
		JitmvViewer value = null;
		_viewers.TryGetValue(strId, out value);
		return value;
	}

	public static void SetViewer(string strId, JitmvViewer viewer)
	{
		if (_viewers.ContainsKey(strId))
		{
			_viewers.Remove(strId);
		}
		_viewers.Add(strId, viewer);
	}
}
