using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[Guid("0C733A30-2A1C-11CE-ADE5-00AA0044773D")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface ISequentialStream
{
	[MethodImpl(MethodImplOptions.InternalCall)]
	void RemoteRead(out byte pv, [In] uint cb, out uint pcbRead);

	[MethodImpl(MethodImplOptions.InternalCall)]
	void RemoteWrite([In] ref byte pv, [In] uint cb, out uint pcbWritten);
}
