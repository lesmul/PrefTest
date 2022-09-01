using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Interop.MSXML2;

namespace Interop.PrefSales;

[ComImport]
[TypeLibType(TypeLibTypeFlags.FDispatchable)]
[Guid("850AC3F8-B694-4512-A624-C656D9E4E013")]
[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
public interface _IPrefSchedulerEvents
{
	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
	[DispId(1)]
	void BeforeLoadScheduler([MarshalAs(UnmanagedType.Interface)] PrefScheduler Scheduler, [MarshalAs(UnmanagedType.Interface)] IXMLDOMDocument2 pXML);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
	[DispId(2)]
	void BeforeLoadSelectionDetail([MarshalAs(UnmanagedType.Interface)] PrefScheduler Scheduler, [MarshalAs(UnmanagedType.Interface)] IXMLDOMDocument2 pXML);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
	[DispId(3)]
	void OrderDoubleClick(int Number, int Version);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
	[DispId(4)]
	void PODoubleClick(int Number, int Numeration);
}
