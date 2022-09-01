using System;
using System.Reflection;

namespace Preference.WPF.MaterialsSelector.Core;

internal static class LateBinding
{
	public static object CreateObject(string progID)
	{
		return Activator.CreateInstance(Type.GetTypeFromProgID(progID));
	}

	public static object CreateObjectFromCLSID(Guid clsid)
	{
		return Activator.CreateInstance(Type.GetTypeFromCLSID(clsid));
	}

	public static void SetProperty(object obj, string sProperty, object oValue)
	{
		object[] args = new object[1] { oValue };
		obj.GetType().InvokeMember(sProperty, BindingFlags.SetProperty, null, obj, args);
	}

	public static object GetProperty(object obj, string sProperty, object oValue)
	{
		object[] args = new object[1] { oValue };
		return obj.GetType().InvokeMember(sProperty, BindingFlags.GetProperty, null, obj, args);
	}

	public static object GetProperty(object obj, string sProperty, object oValue1, object oValue2)
	{
		object[] args = new object[2] { oValue1, oValue2 };
		return obj.GetType().InvokeMember(sProperty, BindingFlags.GetProperty, null, obj, args);
	}

	public static object GetProperty(object obj, string sProperty)
	{
		return obj.GetType().InvokeMember(sProperty, BindingFlags.GetProperty, null, obj, null);
	}

	public static object InvokeMethod(object obj, string sProperty)
	{
		return obj.GetType().InvokeMember(sProperty, BindingFlags.InvokeMethod, null, obj, null);
	}

	public static object InvokeMethod(object obj, string sProperty, object[] oParam)
	{
		return obj.GetType().InvokeMember(sProperty, BindingFlags.InvokeMethod, null, obj, oParam);
	}

	public static object InvokeMethod(object obj, string sProperty, object[] oParam, out object[] oReturnParam)
	{
		object result = obj.GetType().InvokeMember(sProperty, BindingFlags.InvokeMethod, null, obj, oParam);
		oReturnParam = new object[oParam.Length];
		for (int i = 0; i < oParam.Length; i++)
		{
			oReturnParam[i] = oParam[i];
		}
		return result;
	}

	public static object InvokeMethod(object obj, string sProperty, object oValue)
	{
		object[] args = new object[1] { oValue };
		return obj.GetType().InvokeMember(sProperty, BindingFlags.InvokeMethod, null, obj, args);
	}
}
