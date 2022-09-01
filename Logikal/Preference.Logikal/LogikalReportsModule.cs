using Preference.Logikal.Api;

namespace Preference.Logikal;

public static class LogikalReportsModule
{
	public static int GetReportsTypeCount()
	{
		return LogiDll.GetPOTypeCount();
	}

	public static int GetReportType(int nIndex)
	{
		return LogiDll.GetPOType(nIndex);
	}

	public static string GetReportTypeName(int nType)
	{
		string strName = null;
		LogiDll.GetPOTypeName(nType, ref strName);
		return strName;
	}

	public static int ReportBegin(int nPrintType)
	{
		return LogiDll.POBegin(nPrintType);
	}

	public static void ReportSetObjData(int nHandle, string strName)
	{
		LogiDll.POSetObjDataF(nHandle, strName);
	}

	public static void ReportSetAddress(int nHandle, string strHead, string strName1, string strName2, string strStreet, string strZipCode, string strPlace, string strCountry, string strTlf, string strFax)
	{
		LogiDll.POSetAddress(nHandle, strHead, strName1, strName2, strStreet, strZipCode, strPlace, strCountry, strTlf, strFax);
	}

	public static void ReportAddPos(int nHandle, byte[] bPosition, int nSize, string strName, string strDescription, int nId, int nAnz)
	{
		LogiDll.POAddPosEx(nHandle, bPosition, nSize, strName, strDescription, nId, nAnz);
	}

	public static bool ReportExecute(int nHandle, int nModus)
	{
		return LogiDll.POExecute(nHandle, nModus);
	}

	public static bool ReportGetPosF(int nHandle, int nId, string strFile)
	{
		return LogiDll.POGetPosF(nHandle, nId, strFile);
	}

	public static void ReportGetReturnXmlF(int nHandle, string strFile)
	{
		LogiDll.POGetReturnXMLF(nHandle, strFile);
	}

	public static void ReportGetObjectDataF(int nHandle, string strFile)
	{
		LogiDll.POGetObjDataF(nHandle, strFile);
	}

	public static void ReportEnd(int nHandle)
	{
		LogiDll.POEnd(nHandle);
	}

	public static void SetParamValue(int nHandle, string strParameter, string strValue)
	{
		LogiDll.POSetStrParam(nHandle, strParameter, strValue);
	}
}
