using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComVisible(false)]
[TypeLibType(TypeLibTypeFlags.FHidden)]
public delegate void _ISalesDocEvents_AfterFieldChangedEventHandler([MarshalAs(UnmanagedType.Interface)] SalesDoc SalesDoc, [MarshalAs(UnmanagedType.Interface)] SalesDocField SalesDocField);
