using System;
using System.Globalization;
using System.Windows;
using Microsoft.Win32;
using Preference.Diagnostics;

namespace Preference.WPF.MaterialsSelector.Settings;

public class MaterialSelectorWindowSettings
{
	public bool TryGetWindowSettings(out double top, out double left, out double height, out double width, out WindowState windowState)
	{
		top = 0.0;
		left = 0.0;
		height = 0.0;
		width = 0.0;
		windowState = WindowState.Normal;
		try
		{
			using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(string.Format("{0}\\Window", "SOFTWARE\\Preference\\PrefGest\\PrefFilterBOM"), writable: false))
			{
				if (registryKey != null)
				{
					top = Convert.ToDouble(registryKey.GetValue("WindowTop"), CultureInfo.InvariantCulture);
					left = Convert.ToDouble(registryKey.GetValue("WindowLeft"), CultureInfo.InvariantCulture);
					height = Convert.ToDouble(registryKey.GetValue("WindowHeight"), CultureInfo.InvariantCulture);
					width = Convert.ToDouble(registryKey.GetValue("WindowWidth"), CultureInfo.InvariantCulture);
					windowState = (WindowState)Convert.ToInt32(registryKey.GetValue("WindowState"), CultureInfo.InvariantCulture);
					return true;
				}
			}
			return false;
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefGest");
			return false;
		}
	}

	public void SetWindowSettings(double top, double left, double height, double width, WindowState windowState)
	{
		try
		{
			using RegistryKey registryKey = Registry.CurrentUser.CreateSubKey(string.Format("{0}\\Window", "SOFTWARE\\Preference\\PrefGest\\PrefFilterBOM"));
			if (registryKey != null)
			{
				registryKey.SetValue("WindowTop", Convert.ToString(top, CultureInfo.InvariantCulture));
				registryKey.SetValue("WindowLeft", Convert.ToString(left, CultureInfo.InvariantCulture));
				registryKey.SetValue("WindowHeight", Convert.ToString(height, CultureInfo.InvariantCulture));
				registryKey.SetValue("WindowWidth", Convert.ToString(width, CultureInfo.InvariantCulture));
				registryKey.SetValue("WindowState", Convert.ToString(Convert.ToInt32(windowState), CultureInfo.InvariantCulture));
			}
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefGest");
		}
	}
}
