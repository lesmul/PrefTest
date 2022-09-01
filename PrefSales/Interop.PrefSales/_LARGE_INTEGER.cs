using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[StructLayout(LayoutKind.Sequential, Pack = 8)]
public struct _LARGE_INTEGER
{
	public long QuadPart;
}
