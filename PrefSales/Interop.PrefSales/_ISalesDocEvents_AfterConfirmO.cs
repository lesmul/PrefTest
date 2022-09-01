using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[TypeLibType(TypeLibTypeFlags.FHidden)]
[ComVisible(false)]
public delegate void _ISalesDocEvents_AfterConfirmOrderEventHandler([MarshalAs(UnmanagedType.Interface)] SalesDoc SalesDoc);
