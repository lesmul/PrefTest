using Preference.Logikal.Api;

namespace Preference.Logikal;

public static class LogikalCNCModule
{
	public static int CNCBegin(int nCNCType)
	{
		return LogiDll.POBegin(nCNCType);
	}

	public static void CNCSetObjData(int nHandle, string strName)
	{
		LogiDll.POSetObjDataF(nHandle, strName);
	}

	public static void CNCAddPos(int nHandle, byte[] bPosition, int nSize, string strName, string strDescription, int nId, int nAnz)
	{
		LogiDll.POAddPosEx(nHandle, bPosition, nSize, strName, strDescription, nId, nAnz);
	}

	public static void CNCAddPosFEx(int nHandle, string strBlobFile, string strName, string strDescription, int nId, int nAnz)
	{
		LogiDll.POAddPosFEx(nHandle, strBlobFile, strName, strDescription, nId, nAnz);
	}

	public static void CNCSetJobNumber(int nHandle, string strOrderNumber, string strJobNumber)
	{
		LogiDll.POSetJobNumber(nHandle, strOrderNumber, strJobNumber);
	}

	public static bool CNCExecute(int nHandle, int nModus)
	{
		return LogiDll.POExecuteCNC(nHandle, nModus);
	}

	public static bool MDBExecute(int nHandle, int nModus)
	{
		return LogiDll.POExecute(nHandle, nModus);
	}

	public static int MDBGetReturnF(int nHandle, string strMDBFileName)
	{
		return LogiDll.POGetReturnF(nHandle, strMDBFileName);
	}

	public static void CNCEnd(int nHandle)
	{
		LogiDll.POEnd(nHandle);
	}

	public static void SetParamValue(int nHandle, string strParameter, string strValue)
	{
		LogiDll.POSetStrParam(nHandle, strParameter, strValue);
	}

	public static int MDBPOTGetTypeInterface()
	{
		return LogiDll.POGetTypeInterface();
	}

	public static void MDBPOAddPosEx(int nHandle, byte[] bPosition, int nSize, string strName, string strDescription, int nId, int nAnz)
	{
		LogiDll.POAddPosEx(nHandle, bPosition, nSize, strName, strDescription, nId, nAnz);
	}

	public static bool MDBPOGetPosF(int nHandle, int nId, string strFile)
	{
		return LogiDll.POGetPosF(nHandle, nId, strFile);
	}

	public static void MDBGetObjectDataF(int nHandle, string strFile)
	{
		LogiDll.POGetObjDataF(nHandle, strFile);
	}

	public static void IOESetPositionF(int nHandle, string strElevationBlobFile)
	{
		LogiDll.IoeSetPositionF(nHandle, strElevationBlobFile);
	}

	public static bool IOEExecute(int nHandle, InputOfElementsExecuteMode mode)
	{
		return LogiDll.IoeExecute(nHandle, mode);
	}

	public static void IOEDone(int nHandle)
	{
		LogiDll.IoeDone(nHandle);
	}
}
