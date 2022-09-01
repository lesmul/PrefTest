using System.Collections.Generic;

namespace Preference.Import.Data;

internal static class Extensions
{
	public static void AddRange<TKey, TValue>(this Dictionary<TKey, TValue> thisDictionary, IDictionary<TKey, TValue> dictionary)
	{
		foreach (KeyValuePair<TKey, TValue> item in dictionary)
		{
			thisDictionary.Add(item.Key, item.Value);
		}
	}
}
