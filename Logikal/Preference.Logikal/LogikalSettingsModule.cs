using Preference.Logikal.Api;

namespace Preference.Logikal;

public static class LogikalSettingsModule
{
	public static int SettingsBegin(int nType)
	{
		return LogiDll.POBegin(nType);
	}

	public static void SettingsSetObjData(int nHandle, string strName)
	{
		LogiDll.POSetObjDataF(nHandle, strName);
	}

	public static void SettingsSetAddress(int nHandle, string strHead, string strName1, string strName2, string strStreet, string strZipCode, string strPlace, string strCountry, string strTlf, string strFax)
	{
		LogiDll.POSetAddress(nHandle, strHead, strName1, strName2, strStreet, strZipCode, strPlace, strCountry, strTlf, strFax);
	}

	public static void SettingsAddPos(int nHandle, byte[] bPosition, int nSize, string strName, string strDescription, int nId, int nAnz)
	{
		LogiDll.POAddPosEx(nHandle, bPosition, nSize, strName, strDescription, nId, nAnz);
	}

	public static bool SettingsGetPosF(int nHandle, int nId, string strFile)
	{
		return LogiDll.POGetPosF(nHandle, nId, strFile);
	}

	public static void SettingsGetObjectDataF(int nHandle, string strFile)
	{
		LogiDll.POGetObjDataF(nHandle, strFile);
	}

	public static void SettingsEnd(int nHandle)
	{
		LogiDll.POEnd(nHandle);
	}

	public static int ProgramsGetTypeCount()
	{
		return LogiDll.ProgramsGetTypeCount();
	}

	public static int ProgramsGetType(int nIndex)
	{
		return LogiDll.ProgramsGetType(nIndex);
	}

	public static string GetSettingName(int nType)
	{
		string strName = null;
		LogiDll.GetProgramName(nType, ref strName);
		return strName;
	}

	public static int GetSettingKind(int nType)
	{
		int nKind = 0;
		LogiDll.GetProgramKind(nType, ref nKind);
		return nKind;
	}

	public static bool ExecuteProgram(int nProgramType)
	{
		return LogiDll.ProgramsExecute(nProgramType);
	}

	public static bool ExecuteProgramObj(int nHandle, int nProgramType)
	{
		return LogiDll.ProgramsExecuteObj(nHandle, nProgramType);
	}

	public static bool ProjectBlobHasChanged(int nHandle)
	{
		return LogiDll.ProjectBlobHasChanged(nHandle);
	}
}
