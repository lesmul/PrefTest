using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
[Guid("7A6D19C8-EAC0-4E88-95ED-B4229E40F31F")]
[TypeLibType(TypeLibTypeFlags.FDispatchable)]
public interface _IMaterialNeedsEvents
{
	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
	[DispId(1)]
	void BeforeCalculateMaterialNeeds([MarshalAs(UnmanagedType.Interface)] MaterialNeeds MaterialNeeds, [MarshalAs(UnmanagedType.IUnknown)] object Connection, ref bool Cancel);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
	[DispId(2)]
	void AfterCalculateMaterialNeeds([MarshalAs(UnmanagedType.Interface)] MaterialNeeds MaterialNeeds, [MarshalAs(UnmanagedType.IUnknown)] object Connection);

	[MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall)]
	[DispId(3)]
	void BeforeRemoveMaterialNeeds([MarshalAs(UnmanagedType.Interface)] MaterialNeeds MaterialNeeds, [MarshalAs(UnmanagedType.IUnknown)] object Connection, ref bool Cancel);
}
