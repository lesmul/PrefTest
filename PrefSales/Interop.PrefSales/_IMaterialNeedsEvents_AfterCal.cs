using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComVisible(false)]
[TypeLibType(TypeLibTypeFlags.FHidden)]
public delegate void _IMaterialNeedsEvents_AfterCalculateMaterialNeedsEventHandler([MarshalAs(UnmanagedType.Interface)] MaterialNeeds MaterialNeeds, [MarshalAs(UnmanagedType.IUnknown)] object Connection);
