using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using Microsoft.Win32;
using Preference.Exceptions;
using Preference.Logikal.Api;

namespace Preference.Logikal;

public static class LogikalModule
{
	private const string LoginName = "preference";

	public static bool IsLoggedIn => LogiDll.IsLoggedIn();

	private static string LogikalDllPath
	{
		get
		{
			string result = "C:\\Logikal\\LogiDll.dll";
			using RegistryKey registryKey = Registry.LocalMachine;
			using RegistryKey registryKey2 = registryKey.OpenSubKey("Software\\Orgadata");
			if (registryKey2 != null)
			{
				object value = registryKey2.GetValue("LogiDll");
				if (value != null)
				{
					return Convert.ToString(value, CultureInfo.InvariantCulture);
				}
				return result;
			}
			return result;
		}
	}

	public static string LogikalFolder => Path.GetDirectoryName(LogikalDllPath);

	public static int LanguageId
	{
		get
		{
			LanguageId actualLanguageId = LogiDll.GetActualLanguageId();
			string name = "en";
			switch (actualLanguageId)
			{
			case Preference.Logikal.Api.LanguageId.German:
				name = "de";
				break;
			case Preference.Logikal.Api.LanguageId.English:
				name = "en";
				break;
			case Preference.Logikal.Api.LanguageId.French:
				name = "fr";
				break;
			case Preference.Logikal.Api.LanguageId.Polish:
				name = "pl";
				break;
			case Preference.Logikal.Api.LanguageId.Danish:
				name = "da";
				break;
			case Preference.Logikal.Api.LanguageId.Russian:
				name = "ru";
				break;
			case Preference.Logikal.Api.LanguageId.Czech:
				name = "cs";
				break;
			case Preference.Logikal.Api.LanguageId.Swedish:
				name = "sv";
				break;
			case Preference.Logikal.Api.LanguageId.Greek:
				name = "el";
				break;
			case Preference.Logikal.Api.LanguageId.Italian:
				name = "it";
				break;
			case Preference.Logikal.Api.LanguageId.Croatian:
				name = "hr";
				break;
			case Preference.Logikal.Api.LanguageId.Dutch:
				name = "nl";
				break;
			case Preference.Logikal.Api.LanguageId.Turkish:
				name = "tr";
				break;
			case Preference.Logikal.Api.LanguageId.Spanish:
				name = "es";
				break;
			case Preference.Logikal.Api.LanguageId.Finnish:
				name = "fi";
				break;
			case Preference.Logikal.Api.LanguageId.American:
				name = "en-US";
				break;
			case Preference.Logikal.Api.LanguageId.Icelandic:
				name = "is";
				break;
			case Preference.Logikal.Api.LanguageId.Norwegian:
				name = "no";
				break;
			case Preference.Logikal.Api.LanguageId.Slovak:
				name = "sk";
				break;
			case Preference.Logikal.Api.LanguageId.Chinese:
				name = "zh";
				break;
			case Preference.Logikal.Api.LanguageId.Hungarian:
				name = "hu";
				break;
			case Preference.Logikal.Api.LanguageId.Farsi:
				name = "fa";
				break;
			case Preference.Logikal.Api.LanguageId.Slovenian:
				name = "sl";
				break;
			case Preference.Logikal.Api.LanguageId.Bulgarian:
				name = "bg";
				break;
			case Preference.Logikal.Api.LanguageId.Flemish:
				name = "nl-BE";
				break;
			case Preference.Logikal.Api.LanguageId.Estonian:
				name = "et";
				break;
			case Preference.Logikal.Api.LanguageId.Ukrainian:
				name = "uk";
				break;
			case Preference.Logikal.Api.LanguageId.Lithuanian:
				name = "lt";
				break;
			case Preference.Logikal.Api.LanguageId.Romanian:
				name = "ro";
				break;
			case Preference.Logikal.Api.LanguageId.Hebrew:
				name = "he";
				break;
			case Preference.Logikal.Api.LanguageId.Portuguese:
				name = "pt";
				break;
			}
			return new CultureInfo(name).LCID;
		}
		set
		{
			CultureInfo cultureInfo = new CultureInfo(value);
			LanguageId languageId = Preference.Logikal.Api.LanguageId.English;
			string name = cultureInfo.Name;
			if (!(name == "en-us"))
			{
				if (name == "nl-BE")
				{
					languageId = Preference.Logikal.Api.LanguageId.Flemish;
				}
				else
				{
					switch (cultureInfo.TwoLetterISOLanguageName)
					{
					case "de":
						languageId = Preference.Logikal.Api.LanguageId.German;
						break;
					case "en":
						languageId = Preference.Logikal.Api.LanguageId.English;
						break;
					case "fr":
						languageId = Preference.Logikal.Api.LanguageId.French;
						break;
					case "pl":
						languageId = Preference.Logikal.Api.LanguageId.Polish;
						break;
					case "da":
						languageId = Preference.Logikal.Api.LanguageId.Danish;
						break;
					case "ru":
						languageId = Preference.Logikal.Api.LanguageId.Russian;
						break;
					case "cs":
						languageId = Preference.Logikal.Api.LanguageId.Czech;
						break;
					case "sv":
						languageId = Preference.Logikal.Api.LanguageId.Swedish;
						break;
					case "el":
						languageId = Preference.Logikal.Api.LanguageId.Greek;
						break;
					case "it":
						languageId = Preference.Logikal.Api.LanguageId.Italian;
						break;
					case "hr":
						languageId = Preference.Logikal.Api.LanguageId.Croatian;
						break;
					case "nl":
						languageId = Preference.Logikal.Api.LanguageId.Dutch;
						break;
					case "tr":
						languageId = Preference.Logikal.Api.LanguageId.Turkish;
						break;
					case "es":
						languageId = Preference.Logikal.Api.LanguageId.Spanish;
						break;
					case "fi":
						languageId = Preference.Logikal.Api.LanguageId.Finnish;
						break;
					case "is":
						languageId = Preference.Logikal.Api.LanguageId.Icelandic;
						break;
					case "no":
						languageId = Preference.Logikal.Api.LanguageId.Norwegian;
						break;
					case "sk":
						languageId = Preference.Logikal.Api.LanguageId.Slovak;
						break;
					case "zh":
						languageId = Preference.Logikal.Api.LanguageId.Chinese;
						break;
					case "hu":
						languageId = Preference.Logikal.Api.LanguageId.Hungarian;
						break;
					case "fa":
						languageId = Preference.Logikal.Api.LanguageId.Farsi;
						break;
					case "sl":
						languageId = Preference.Logikal.Api.LanguageId.Slovenian;
						break;
					case "bg":
						languageId = Preference.Logikal.Api.LanguageId.Bulgarian;
						break;
					case "et":
						languageId = Preference.Logikal.Api.LanguageId.Estonian;
						break;
					case "uk":
						languageId = Preference.Logikal.Api.LanguageId.Ukrainian;
						break;
					case "lt":
						languageId = Preference.Logikal.Api.LanguageId.Lithuanian;
						break;
					case "ro":
						languageId = Preference.Logikal.Api.LanguageId.Romanian;
						break;
					case "he":
						languageId = Preference.Logikal.Api.LanguageId.Hebrew;
						break;
					case "pt":
						languageId = Preference.Logikal.Api.LanguageId.Portuguese;
						break;
					}
				}
			}
			else
			{
				languageId = Preference.Logikal.Api.LanguageId.American;
			}
			if (LogiDll.GetActualLanguageId() != languageId)
			{
				LogiDll.SetLanguage(languageId);
			}
		}
	}

	static LogikalModule()
	{
		try
		{
			LogiDll.LoadDll(LogikalDllPath);
		}
		catch (Exception ex)
		{
			throw PrefException.New("Failed loading LogiKal module.", ex);
		}
		AppDomain.CurrentDomain.ProcessExit += AppDomainProcessExit;
	}

	public static void EnsureLoggedIn()
	{
		if (!IsLoggedIn)
		{
			Login();
		}
	}

	public static void Login()
	{
		string strInfo = null;
		try
		{
			LogiDll.SetApplication(Process.GetCurrentProcess().MainWindowHandle);
			LogiDll.SetPath(LogikalFolder);
			if (!LogiDll.LogInLogikal(ref strInfo, "preference"))
			{
				throw PrefException.New(strInfo);
			}
		}
		catch (Exception ex)
		{
			throw PrefException.New("Failed login into LogiKal.", ex);
		}
	}

	public static void Logout()
	{
		LogiDll.LogOut();
	}

	private static void AppDomainProcessExit(object sender, EventArgs e)
	{
		try
		{
			AppDomain.CurrentDomain.ProcessExit -= AppDomainProcessExit;
			Logout();
		}
		catch (Exception ex)
		{
			EventLog.AddError(PrefException.GetFormattedMessage("LogiKal logout failed.", ex));
		}
	}
}
