using System.Collections.Generic;
using System.Windows.Media;

namespace Preference.Wpf.Controls;

public abstract class WPFWin32Interop<T> where T : Visual
{
	private static Dictionary<string, T> _visuals = new Dictionary<string, T>();

	public static T GetVisual(string strId)
	{
		T value = null;
		_visuals.TryGetValue(strId, out value);
		return value;
	}

	public static bool Remove(string strId)
	{
		return _visuals.Remove(strId);
	}

	public static void SetVisual(string strId, T viewer)
	{
		if (_visuals.ContainsKey(strId))
		{
			_visuals.Remove(strId);
		}
		_visuals.Add(strId, viewer);
	}
}
