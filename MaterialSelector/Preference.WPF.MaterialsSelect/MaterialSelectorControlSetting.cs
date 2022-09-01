using System;
using Microsoft.Win32;
using Preference.WPF.MaterialsSelector.Core;

namespace Preference.WPF.MaterialsSelector.Settings;

public class MaterialSelectorControlSettings
{
	private const string REGISTRY_FILTER_BOM_NAME_DOCK_LAYOUT = "DockLayout";

	public string GetDockLayout()
	{
		object value = RegistryHelper.GetValue("SOFTWARE\\Preference\\PrefGest\\PrefFilterBOM", "DockLayout");
		if (value != null)
		{
			return Convert.ToString(value);
		}
		return null;
	}

	public void SetDockLayout(string dockLayout)
	{
		RegistryHelper.SetValue("SOFTWARE\\Preference\\PrefGest\\PrefFilterBOM", "DockLayout", dockLayout, RegistryValueKind.String);
	}
}
