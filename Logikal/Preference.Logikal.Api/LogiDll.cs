using System;
using System.Globalization;
using System.Runtime.InteropServices;
using Preference.Exceptions;

namespace Preference.Logikal.Api;

internal static class LogiDll
{
	private delegate void FreeMemFromCharDelegate(IntPtr pChar);

	private delegate bool IsLoggedInDelegate();

	private delegate bool LogInLogikalDelegate(ref IntPtr pInfo, [MarshalAs(UnmanagedType.LPWStr)] string strPassword);

	private delegate bool LogOutDelegate();

	private delegate void SetApplicationDelegate(IntPtr nHandle);

	private delegate void SetPathDelegate([MarshalAs(UnmanagedType.LPWStr)] string strPath);

	private delegate void IoeDoneDelegate(int nHandle);

	private delegate int IoeGetReturnDelegate(int nHandle, byte[] buffer);

	private delegate int IoeGetReturnFDelegate(int nHandle, [MarshalAs(UnmanagedType.LPWStr)] string stFileName, [MarshalAs(UnmanagedType.LPWStr)] string strExportFileName);

	private delegate int IoeGetReturnPLDelegate(int nHandle, byte[] buffer);

	private delegate bool IoeExecuteDelegate(int nHandle, InputOfElementsExecuteMode mode);

	private delegate int IoeInitDelegate();

	private delegate void IoeNewPositionDelegate(int nHandle, ElementType elementType);

	private delegate void IoeSetPositionDelegate(int nHandle, byte[] buffer, int nSize);

	private delegate void IOESetPositionFDelegate(int handle, [MarshalAs(UnmanagedType.LPWStr)] string filename);

	private delegate LanguageId GetActualLanguageIdDelegate();

	private delegate void SetLanguageDelegate(LanguageId language);

	private delegate bool ExportExecuteDelegate(ExportType type, ExportFormat format, [MarshalAs(UnmanagedType.LPWStr)] string strFileName);

	private delegate bool ExportExecuteExDelegate(int nHandle, ExportType type, ExportFormat format, [MarshalAs(UnmanagedType.LPWStr)] string strFileName);

	private delegate int POGetTypeCountDelegate();

	private delegate void POGetTypeNameDelegate(int nPrintType, ref IntPtr pName);

	private delegate int POGetTypeDelegate(int nAIndex);

	private delegate int POBeginDelegate(int nAPrintType);

	private delegate void POSetObjDataFDelegate(int nHandle, [MarshalAs(UnmanagedType.LPWStr)] string strName);

	private delegate void POSetAdressDelegate(int nHandle, [MarshalAs(UnmanagedType.LPWStr)] string strHead, [MarshalAs(UnmanagedType.LPWStr)] string strName1, [MarshalAs(UnmanagedType.LPWStr)] string strName2, [MarshalAs(UnmanagedType.LPWStr)] string strStreet, [MarshalAs(UnmanagedType.LPWStr)] string strZipCode, [MarshalAs(UnmanagedType.LPWStr)] string strPlace, [MarshalAs(UnmanagedType.LPWStr)] string strCountry, [MarshalAs(UnmanagedType.LPWStr)] string strTlf, [MarshalAs(UnmanagedType.LPWStr)] string strFax);

	private delegate bool POAddPosExDelegate(int AHandle, byte[] APosition, int ASize, [MarshalAs(UnmanagedType.LPWStr)] string Name, [MarshalAs(UnmanagedType.LPWStr)] string Beschr, int AID, int AAnz);

	private delegate bool POExecuteDelegate(int nAHandle, int nAModus);

	private delegate bool POGetPosFDelegate(int nHandle, int nID, [MarshalAs(UnmanagedType.LPWStr)] string strfileName);

	private delegate int POGetReturnXMLFDelegate(int nHandle, [MarshalAs(UnmanagedType.LPWStr)] string strFileName);

	private delegate void POGetObjDataFDelegate(int nHandle, [MarshalAs(UnmanagedType.LPWStr)] string strFileName);

	private delegate void POEndDelegate(int nHandle);

	private delegate void POSetStrParamDelegate(int nHandle, [MarshalAs(UnmanagedType.LPWStr)] string strParam, [MarshalAs(UnmanagedType.LPWStr)] string strValue);

	private delegate bool POExecuteCNCDelegate(int nHandle, int nModus);

	private delegate void POSetJobNumberDelegate(int nHandle, [MarshalAs(UnmanagedType.LPWStr)] string strOfferNr, [MarshalAs(UnmanagedType.LPWStr)] string strJobNr);

	private delegate bool POAddPosFexDelegate(int nHandle, [MarshalAs(UnmanagedType.LPWStr)] string strBlobElevationFile, [MarshalAs(UnmanagedType.LPWStr)] string strElevationName, [MarshalAs(UnmanagedType.LPWStr)] string strElevationDescription, int nElevationIndex, int nElevationAmount);

	private delegate int POGetReturnFDelegate(int nHandle, [MarshalAs(UnmanagedType.LPWStr)] string strFileName);

	private delegate int POGetTypeInterfaceDelegate();

	private delegate int ProgramsGetTypeCountDelegate();

	private delegate int ProgramsGetTypeDelegate(int nIndex);

	private delegate int ProgramsGetTypeNameAndKindDelegate(int nPrintType, ref IntPtr pName, ref int nKind);

	private delegate bool ProgramsExecuteDelegate(int nProgramType);

	private delegate bool ProgramsExecuteObjDelegate(int nHandle, int nProgramType);

	private delegate bool ProjectBlobHasChangedDelegate(int nHandle);

	private static FreeMemFromCharDelegate _freeMemFromChar;

	private static IsLoggedInDelegate _isLoggedInDelegate;

	private static LogInLogikalDelegate _logInLogikalDelegate;

	private static LogOutDelegate _logOutDelegate;

	private static SetApplicationDelegate _setApplicationDelegate;

	private static SetPathDelegate _setPathDelegate;

	private static IoeDoneDelegate _ioeDoneDelegate;

	private static IoeExecuteDelegate _ioeExecuteDelegate;

	private static IoeGetReturnDelegate _ioeGetReturnDelegate;

	private static IoeGetReturnFDelegate _ioeGetReturnFDelegate;

	private static IoeGetReturnPLDelegate _ioeGetReturnPLDelegate;

	private static IoeInitDelegate _ioeInitDelegate;

	private static IoeNewPositionDelegate _ioeNewPositionDelegate;

	private static IoeSetPositionDelegate _ioeSetPositionDelegate;

	private static IOESetPositionFDelegate _ioeSetPositionFDelegate;

	private static GetActualLanguageIdDelegate _getActualLanguageIdDelegate;

	private static SetLanguageDelegate _setLanguageDelegate;

	private static ExportExecuteDelegate _exportExecuteDelegate;

	private static ExportExecuteExDelegate _exportExecuteExDelegate;

	private static POGetTypeCountDelegate _poGetTypeCount;

	private static POGetTypeNameDelegate _poGetTypeName;

	private static POGetTypeDelegate _poGetType;

	private static POBeginDelegate _poBegin;

	private static POSetObjDataFDelegate _poSetObjDataF;

	private static POSetAdressDelegate _poSetAddress;

	private static POAddPosExDelegate _poAddPosEx;

	private static POExecuteDelegate _poExecute;

	private static POGetPosFDelegate _poGetPosF;

	private static POGetReturnXMLFDelegate _poGetReturnXMLF;

	private static POGetObjDataFDelegate _poGetObjDataF;

	private static POEndDelegate _poEnd;

	private static POSetStrParamDelegate _poSetStrParam;

	private static POExecuteCNCDelegate _poExecuteCNC;

	private static POSetJobNumberDelegate _poSetJobNr;

	private static POAddPosFexDelegate _poAddPosFex;

	private static POGetReturnFDelegate _poGetReturnF;

	private static POGetTypeInterfaceDelegate _poGetTypeInterface;

	private static ProgramsGetTypeCountDelegate _programsGetTypeCount;

	private static ProgramsGetTypeDelegate _programsGetType;

	private static ProgramsGetTypeNameAndKindDelegate _programsGetTypeNameAndKind;

	private static ProgramsExecuteDelegate _programsExecute;

	private static ProgramsExecuteObjDelegate _programsExecuteObj;

	private static ProjectBlobHasChangedDelegate _projectBlobHasChanged;

	private static SafeDllHandle DllModule { get; set; }

	public static void LoadDll(string strFileName)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			SafeDllHandle safeDllHandle = NativeMethods.LoadLibrary(strFileName);
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (safeDllHandle.IsInvalid)
			{
				throw new PrefException(string.Format(CultureInfo.InvariantCulture, "Load library '{0}' failed with code {1}).", strFileName, lastWin32Error));
			}
			DllModule = safeDllHandle;
			LoadFuntions();
		}
		catch
		{
			if (!DllModule.IsInvalid)
			{
				DllModule.Dispose();
			}
			throw;
		}
	}

	private static void FreeMemFromChar(IntPtr pChar)
	{
		_freeMemFromChar(pChar);
	}

	public static bool IsLoggedIn()
	{
		return _isLoggedInDelegate();
	}

	public static bool LogInLogikal(ref string strInfo, string strPassword)
	{
		strInfo = null;
		IntPtr pInfo = IntPtr.Zero;
		bool result = _logInLogikalDelegate(ref pInfo, strPassword);
		if (pInfo != IntPtr.Zero)
		{
			strInfo = Marshal.PtrToStringUni(pInfo);
			FreeMemFromChar(pInfo);
		}
		return result;
	}

	public static bool LogOut()
	{
		return _logOutDelegate();
	}

	public static void SetApplication(IntPtr handle)
	{
		_setApplicationDelegate(handle);
	}

	public static void SetPath(string strPath)
	{
		_setPathDelegate(strPath);
	}

	public static void IoeDone(int nHandle)
	{
		_ioeDoneDelegate(nHandle);
	}

	public static int IoeGetReturn(int nHandle, byte[] buffer)
	{
		return _ioeGetReturnDelegate(nHandle, buffer);
	}

	public static int IoeGetReturnF(int nHandle, string strFileName, string strExportFileName)
	{
		return _ioeGetReturnFDelegate(nHandle, strFileName, strExportFileName);
	}

	public static int IoeGetReturnPL(int nHandle, byte[] buffer)
	{
		return _ioeGetReturnPLDelegate(nHandle, buffer);
	}

	public static bool IoeExecute(int nHandle, InputOfElementsExecuteMode mode)
	{
		return _ioeExecuteDelegate(nHandle, mode);
	}

	public static int IoeInit()
	{
		return _ioeInitDelegate();
	}

	public static void IoeNewPosition(int nHandle, ElementType elementType)
	{
		_ioeNewPositionDelegate(nHandle, elementType);
	}

	public static void IoeSetPosition(int nHandle, byte[] buffer, int nSize)
	{
		_ioeSetPositionDelegate(nHandle, buffer, nSize);
	}

	public static void IoeSetPositionF(int nHandle, string strElevationBlob)
	{
		_ioeSetPositionFDelegate(nHandle, strElevationBlob);
	}

	public static LanguageId GetActualLanguageId()
	{
		return _getActualLanguageIdDelegate();
	}

	public static void SetLanguage(LanguageId language)
	{
		_setLanguageDelegate(language);
	}

	public static bool ExportExecute(ExportType type, ExportFormat format, string strFileName)
	{
		return _exportExecuteDelegate(type, format, strFileName);
	}

	public static bool ExportExecuteEx(int nHandle, ExportType type, ExportFormat format, string strFileName)
	{
		return _exportExecuteExDelegate(nHandle, type, format, strFileName);
	}

	public static void POSetStrParam(int nHandle, string strParam, string strValue)
	{
		_poSetStrParam(nHandle, strParam, strValue);
	}

	public static bool POExecuteCNC(int nAHandle, int nAModus)
	{
		return _poExecuteCNC(nAHandle, nAModus);
	}

	public static int POGetReturnF(int nHandle, string strMDBFileName)
	{
		return _poGetReturnF(nHandle, strMDBFileName);
	}

	public static int POGetTypeInterface()
	{
		return _poGetTypeInterface();
	}

	public static void POSetJobNumber(int nHandle, string strOrderNumber, string strJobNumber)
	{
		_poSetJobNr(nHandle, strOrderNumber, strJobNumber);
	}

	public static void POAddPosFEx(int nHandle, string strBlobFile, string strElevationName, string strDescription, int nID, int nQuantity)
	{
		_poAddPosFex(nHandle, strBlobFile, strElevationName, strDescription, nID, nQuantity);
	}

	public static int ProgramsGetTypeCount()
	{
		return _programsGetTypeCount();
	}

	public static int ProgramsGetType(int nIndex)
	{
		return _programsGetType(nIndex);
	}

	public static void GetProgramName(int nType, ref string strName)
	{
		int nKind = 0;
		strName = null;
		IntPtr pName = IntPtr.Zero;
		_programsGetTypeNameAndKind(nType, ref pName, ref nKind);
		if (pName != IntPtr.Zero)
		{
			strName = Marshal.PtrToStringUni(pName);
			FreeMemFromChar(pName);
		}
	}

	public static void GetProgramKind(int nType, ref int nKind)
	{
		nKind = 0;
		IntPtr pName = IntPtr.Zero;
		_programsGetTypeNameAndKind(nType, ref pName, ref nKind);
		if (pName != IntPtr.Zero)
		{
			Marshal.PtrToStringUni(pName);
			FreeMemFromChar(pName);
		}
	}

	public static bool ProgramsExecute(int nProgramType)
	{
		return _programsExecute(nProgramType);
	}

	public static bool ProgramsExecuteObj(int nHandle, int nProgramType)
	{
		return _programsExecuteObj(nHandle, nProgramType);
	}

	public static bool ProjectBlobHasChanged(int nHandle)
	{
		return _projectBlobHasChanged(nHandle);
	}

	public static int GetPOTypeCount()
	{
		return _poGetTypeCount();
	}

	public static void GetPOTypeName(int nPrintType, ref string strName)
	{
		strName = null;
		IntPtr pName = IntPtr.Zero;
		_poGetTypeName(nPrintType, ref pName);
		if (pName != IntPtr.Zero)
		{
			strName = Marshal.PtrToStringUni(pName);
			FreeMemFromChar(pName);
		}
	}

	public static int GetPOType(int AIndex)
	{
		return _poGetType(AIndex);
	}

	public static int POBegin(int APrintType)
	{
		return _poBegin(APrintType);
	}

	public static void POSetObjDataF(int nHandle, [MarshalAs(UnmanagedType.LPWStr)] string strName)
	{
		_poSetObjDataF(nHandle, strName);
	}

	public static void POSetAddress(int nHandle, [MarshalAs(UnmanagedType.LPWStr)] string strHead, [MarshalAs(UnmanagedType.LPWStr)] string strName1, [MarshalAs(UnmanagedType.LPWStr)] string strName2, [MarshalAs(UnmanagedType.LPWStr)] string strStreet, [MarshalAs(UnmanagedType.LPWStr)] string strZipCode, [MarshalAs(UnmanagedType.LPWStr)] string strPlace, [MarshalAs(UnmanagedType.LPWStr)] string strCountry, [MarshalAs(UnmanagedType.LPWStr)] string strTlf, [MarshalAs(UnmanagedType.LPWStr)] string strFax)
	{
		_poSetAddress(nHandle, strHead, strName1, strName2, strStreet, strZipCode, strPlace, strCountry, strTlf, strFax);
	}

	public static void POAddPosEx(int nHandle, byte[] bPosition, int nSize, [MarshalAs(UnmanagedType.LPWStr)] string strName, [MarshalAs(UnmanagedType.LPWStr)] string strDescription, int nID, int nAnz)
	{
		_poAddPosEx(nHandle, bPosition, nSize, strName, strDescription, nID, nAnz);
	}

	public static bool POExecute(int nAHandle, int nAModus)
	{
		return _poExecute(nAHandle, nAModus);
	}

	public static bool POGetPosF(int nHandle, int nId, [MarshalAs(UnmanagedType.LPWStr)] string strFileName)
	{
		return _poGetPosF(nHandle, nId, strFileName);
	}

	public static void POGetReturnXMLF(int nHandle, [MarshalAs(UnmanagedType.LPWStr)] string strFileName)
	{
		_poGetReturnXMLF(nHandle, strFileName);
	}

	public static void POGetObjDataF(int nHandle, [MarshalAs(UnmanagedType.LPWStr)] string strFileName)
	{
		_poGetObjDataF(nHandle, strFileName);
	}

	public static void POEnd(int nHandle)
	{
		_poEnd(nHandle);
	}

	private static void LoadFuntions()
	{
		_freeMemFromChar = LoadFunction<FreeMemFromCharDelegate>("logi_FreeMemFromCharW");
		_isLoggedInDelegate = LoadFunction<IsLoggedInDelegate>("logi_IsLoggedIn");
		_logInLogikalDelegate = LoadFunction<LogInLogikalDelegate>("logi_LogInLogikalW");
		_logOutDelegate = LoadFunction<LogOutDelegate>("logi_LogOut");
		_setApplicationDelegate = LoadFunction<SetApplicationDelegate>("logi_SetApplication");
		_setPathDelegate = LoadFunction<SetPathDelegate>("logi_SetPathW");
		_ioeDoneDelegate = LoadFunction<IoeDoneDelegate>("logi_IOEDone");
		_ioeExecuteDelegate = LoadFunction<IoeExecuteDelegate>("logi_IOEExecute");
		_ioeGetReturnDelegate = LoadFunction<IoeGetReturnDelegate>("logi_IOEGetReturn");
		_ioeGetReturnFDelegate = LoadFunction<IoeGetReturnFDelegate>("logi_IOEGetReturnFW");
		_ioeGetReturnPLDelegate = LoadFunction<IoeGetReturnPLDelegate>("logi_IOEGetReturnPL");
		_ioeInitDelegate = LoadFunction<IoeInitDelegate>("logi_IOEInit");
		_ioeNewPositionDelegate = LoadFunction<IoeNewPositionDelegate>("logi_IOENewPosition");
		_ioeSetPositionDelegate = LoadFunction<IoeSetPositionDelegate>("logi_IOESetPosition");
		_ioeSetPositionFDelegate = LoadFunction<IOESetPositionFDelegate>("logi_IOESetPositionFW");
		_getActualLanguageIdDelegate = LoadFunction<GetActualLanguageIdDelegate>("logi_GetActualLanguageId");
		_setLanguageDelegate = LoadFunction<SetLanguageDelegate>("logi_SetLanguage");
		_exportExecuteDelegate = LoadFunction<ExportExecuteDelegate>("logi_ExportExecuteW");
		_exportExecuteExDelegate = LoadFunction<ExportExecuteExDelegate>("logi_ExportExecuteExW");
		_poGetTypeCount = LoadFunction<POGetTypeCountDelegate>("logi_POGetTypeCount");
		_poGetTypeName = LoadFunction<POGetTypeNameDelegate>("logi_POGetTypeNameW");
		_poGetType = LoadFunction<POGetTypeDelegate>("logi_POGetType");
		_poBegin = LoadFunction<POBeginDelegate>("logi_POBegin");
		_poSetObjDataF = LoadFunction<POSetObjDataFDelegate>("logi_POSetObjDataFW");
		_poSetAddress = LoadFunction<POSetAdressDelegate>("logi_POSetAdressW");
		_poAddPosEx = LoadFunction<POAddPosExDelegate>("logi_POAddPosEx");
		_poExecute = LoadFunction<POExecuteDelegate>("logi_POExecute");
		_poGetPosF = LoadFunction<POGetPosFDelegate>("logi_POGetPosFW");
		_poGetReturnXMLF = LoadFunction<POGetReturnXMLFDelegate>("logi_POGetReturnXMLFW");
		_poGetObjDataF = LoadFunction<POGetObjDataFDelegate>("logi_POGetObjDataFW");
		_poEnd = LoadFunction<POEndDelegate>("logi_POEnd");
		_poSetStrParam = LoadFunction<POSetStrParamDelegate>("logi_POSetStrParamW");
		_poExecuteCNC = LoadFunction<POExecuteCNCDelegate>("logi_POExecuteCNC");
		_poSetJobNr = LoadFunction<POSetJobNumberDelegate>("logi_POSetJobNrW");
		_poAddPosFex = LoadFunction<POAddPosFexDelegate>("logi_POAddPosFEx");
		_poGetReturnF = LoadFunction<POGetReturnFDelegate>("logi_POGetReturnFW");
		_poGetTypeInterface = LoadFunction<POGetTypeInterfaceDelegate>("logi_POGetTypeInterface");
		_programsGetTypeCount = LoadFunction<ProgramsGetTypeCountDelegate>("logi_ProgramsGetTypeCount");
		_programsGetType = LoadFunction<ProgramsGetTypeDelegate>("logi_ProgramsGetType");
		_programsGetTypeNameAndKind = LoadFunction<ProgramsGetTypeNameAndKindDelegate>("logi_ProgramsGetTypeNameAndKindW");
		_programsExecute = LoadFunction<ProgramsExecuteDelegate>("logi_ProgramsExecute");
		_programsExecuteObj = LoadFunction<ProgramsExecuteObjDelegate>("logi_ProgramsExecuteObj");
		_projectBlobHasChanged = LoadFunction<ProjectBlobHasChangedDelegate>("logi_ProjectBlobHasChanged");
	}

	private static T LoadFunction<T>(string strFunctionName) where T : class
	{
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		IntPtr procAddress = NativeMethods.GetProcAddress(DllModule, strFunctionName);
		if (procAddress != IntPtr.Zero)
		{
			Delegate delegateForFunctionPointer = Marshal.GetDelegateForFunctionPointer(procAddress, typeof(T));
			if ((object)delegateForFunctionPointer != null && delegateForFunctionPointer is T result)
			{
				return result;
			}
		}
		throw new PrefException(string.Format(CultureInfo.InvariantCulture, "Entry point '{0}' not found in LogiDll.dll. This version of LogiKal is not compatible with the current version of PrefSuite.", strFunctionName));
	}
}
