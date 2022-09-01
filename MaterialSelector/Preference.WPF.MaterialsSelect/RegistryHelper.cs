using System;
using System.Globalization;
using Microsoft.Win32;
using Preference.Diagnostics;

namespace Preference.WPF.MaterialsSelector.Core;

public static class RegistryHelper
{
	public const string REGISTRY_FILTER_BOM_KEY = "SOFTWARE\\Preference\\PrefGest\\PrefFilterBOM";

	public static object GetValue(string subKey, string name)
	{
		try
		{
			using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(subKey, writable: false))
			{
				if (registryKey != null)
				{
					return registryKey.GetValue(name, CultureInfo.InvariantCulture);
				}
			}
			return null;
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefGest");
			return null;
		}
	}

	public static void SetValue(string subKey, string name, object value, RegistryValueKind valueKind)
	{
		try
		{
			using RegistryKey registryKey = Registry.CurrentUser.CreateSubKey(subKey);
			registryKey?.SetValue(name, value, valueKind);
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefGest");
		}
	}
}
