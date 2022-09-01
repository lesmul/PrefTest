using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComVisible(false)]
[TypeLibType(TypeLibTypeFlags.FHidden)]
public delegate void _ISalesDocEvents_AfterSaveEventHandler([MarshalAs(UnmanagedType.Interface)] SalesDoc SalesDoc);
