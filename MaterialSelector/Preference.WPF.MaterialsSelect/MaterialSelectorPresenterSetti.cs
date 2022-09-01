using Microsoft.Win32;
using Preference.WPF.MaterialsSelector.Core;

namespace Preference.WPF.MaterialsSelector.Settings;

public class MaterialSelectorPresenterSettings
{
	private const string REGISTRY_FILTER_BOM_NAME_SHOW_COMPONENTS = "ShowComponents";

	private const string REGISTRY_FILTER_BOM_NAME_MODE_RECURSIVE = "ModeRecursive";

	public bool GetShowComponents()
	{
		object value = RegistryHelper.GetValue("SOFTWARE\\Preference\\PrefGest\\PrefFilterBOM", "ShowComponents");
		if (value != null)
		{
			int result = 0;
			if (int.TryParse(value.ToString(), out result))
			{
				return result == 1;
			}
		}
		return false;
	}

	public void SetShowComponents(bool showComponents)
	{
		int num = (showComponents ? 1 : 0);
		RegistryHelper.SetValue("SOFTWARE\\Preference\\PrefGest\\PrefFilterBOM", "ShowComponents", num, RegistryValueKind.DWord);
	}

	public bool GetIsModeRecursive()
	{
		object value = RegistryHelper.GetValue("SOFTWARE\\Preference\\PrefGest\\PrefFilterBOM", "ModeRecursive");
		if (value != null)
		{
			int result = 0;
			if (int.TryParse(value.ToString(), out result))
			{
				return result == 1;
			}
		}
		return false;
	}

	public void SetIsModeRecursive(bool isModeRecursive)
	{
		int num = (isModeRecursive ? 1 : 0);
		RegistryHelper.SetValue("SOFTWARE\\Preference\\PrefGest\\PrefFilterBOM", "ModeRecursive", num, RegistryValueKind.DWord);
	}
}
