using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComVisible(false)]
[TypeLibType(TypeLibTypeFlags.FHidden)]
public delegate void _ISalesDocEvents_BeforeSelectCustomerEventHandler([MarshalAs(UnmanagedType.Interface)] SalesDoc SalesDoc, int lCustomerCode, ref bool vbCancel);
