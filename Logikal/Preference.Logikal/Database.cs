using Preference.Exceptions;
using Preference.Logikal.Api;

namespace Preference.Logikal;

public static class Database
{
	public static void Export(string strFileName)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		LogikalModule.EnsureLoggedIn();
		if (!LogiDll.ExportExecute(ExportType.AllEconomicData, ExportFormat.Mdb, strFileName))
		{
			throw new PrefException("Failed exporting data from LogiKal.");
		}
	}

	public static void ExportGlassData(string strFileName)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		LogikalModule.EnsureLoggedIn();
		if (!LogiDll.ExportExecute(ExportType.Glass, ExportFormat.Xml, strFileName))
		{
			throw new PrefException("Failed exporting glass data from LogiKal.");
		}
	}

	public static void ExportDrawingsData(int nHandle, string strFileName)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		LogikalModule.EnsureLoggedIn();
		if (!LogiDll.ExportExecuteEx(nHandle, ExportType.AllEconomicData, ExportFormat.Mdb, strFileName))
		{
			throw new PrefException("Failed exporting glass data from LogiKal.");
		}
	}
}
