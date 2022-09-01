using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[TypeLibType(TypeLibTypeFlags.FHidden)]
[ComVisible(false)]
public delegate void _ISalesDocEvents_AfterNewVersionEventHandler([MarshalAs(UnmanagedType.Interface)] SalesDoc srcSalesDoc, int lDestNumber, int lDestVersion);
