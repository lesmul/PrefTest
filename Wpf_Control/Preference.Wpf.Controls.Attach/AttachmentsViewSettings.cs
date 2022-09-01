using System;
using System.Globalization;
using Microsoft.Win32;
using Preference.Diagnostics;

namespace Preference.Wpf.Controls.Attachments.Views;

internal sealed class AttachmentsViewSettings
{
	private const string REGISTRY_ATTACHMENTS_KEY = "SOFTWARE\\Preference\\PrefGest\\PrefAttachments";

	private double _dListViewWidth = 123.0;

	public double ListViewWidth
	{
		get
		{
			return _dListViewWidth;
		}
		set
		{
			_dListViewWidth = value;
		}
	}

	public void Reload()
	{
		try
		{
			using RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Preference\\PrefGest\\PrefAttachments", writable: false);
			if (registryKey != null)
			{
				_dListViewWidth = Convert.ToDouble(registryKey.GetValue("ListViewWidth"), CultureInfo.InvariantCulture);
			}
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefGest");
		}
	}

	public void Save()
	{
		try
		{
			using RegistryKey registryKey = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Preference\\PrefGest\\PrefAttachments");
			registryKey?.SetValue("ListViewWidth", Convert.ToString(_dListViewWidth, CultureInfo.InvariantCulture));
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefGest");
		}
	}
}
