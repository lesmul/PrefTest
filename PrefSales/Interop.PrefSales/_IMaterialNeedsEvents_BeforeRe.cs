using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[TypeLibType(TypeLibTypeFlags.FHidden)]
[ComVisible(false)]
public delegate void _IMaterialNeedsEvents_BeforeRemoveMaterialNeedsEventHandler([MarshalAs(UnmanagedType.Interface)] MaterialNeeds MaterialNeeds, [MarshalAs(UnmanagedType.IUnknown)] object Connection, ref bool Cancel);
