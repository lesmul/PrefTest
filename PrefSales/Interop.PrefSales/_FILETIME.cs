using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[StructLayout(LayoutKind.Sequential, Pack = 4)]
public struct _FILETIME
{
	public uint dwLowDateTime;

	public uint dwHighDateTime;
}
