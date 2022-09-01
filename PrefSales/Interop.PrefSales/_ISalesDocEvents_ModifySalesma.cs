using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComVisible(false)]
[TypeLibType(TypeLibTypeFlags.FHidden)]
public delegate void _ISalesDocEvents_ModifySalesmanSurchargeEventHandler([MarshalAs(UnmanagedType.Interface)] SalesDoc SalesDoc, ref double dblSalesmanSurcharge);
