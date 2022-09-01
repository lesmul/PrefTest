using System;
using System.Runtime.InteropServices;

namespace Preference.Logikal;

internal static class NativeMethods
{
	[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
	public static extern SafeDllHandle LoadLibrary(string strFileName);

	[DllImport("kernel32.dll")]
	public static extern int FreeLibrary(IntPtr module);

	[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Ansi)]
	public static extern IntPtr GetProcAddress(SafeDllHandle module, string strProcName);
}
