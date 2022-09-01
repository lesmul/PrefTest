using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComVisible(false)]
[TypeLibType(TypeLibTypeFlags.FHidden)]
public delegate void _ISalesDocEvents_ValidateEventHandler([MarshalAs(UnmanagedType.Interface)] SalesDoc SalesDoc, ref bool Result);
