using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComVisible(false)]
[TypeLibType(TypeLibTypeFlags.FHidden)]
public delegate void _ISalesDocEvents_BeforeRecalculateItemEventHandler([MarshalAs(UnmanagedType.Interface)] SalesDocItem SalesDocItem, ref bool Cancel);
[TypeLibType(TypeLibTypeFlags.FHidden)]
[ComVisible(false)]
public delegate void _ISalesDocEvents_BeforeRecalculateItemPricesEventHandler([MarshalAs(UnmanagedType.Interface)] SalesDocItem SalesDocItem, ref bool Cancel);
