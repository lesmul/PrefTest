using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[TypeLibType(TypeLibTypeFlags.FHidden)]
[ComVisible(false)]
public delegate void _ISalesDocEvents_BeforeRemoveInvoiceEventHandler([MarshalAs(UnmanagedType.Interface)] SalesDoc SalesDoc, ref bool Cancel);
