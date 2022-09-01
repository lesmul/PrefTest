using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[TypeLibType(TypeLibTypeFlags.FHidden)]
[ComVisible(false)]
public delegate void _ISalesDocEvents_AfterRecalculateItemEventHandler([MarshalAs(UnmanagedType.Interface)] SalesDocItem SalesDocItem);
[TypeLibType(TypeLibTypeFlags.FHidden)]
[ComVisible(false)]
public delegate void _ISalesDocEvents_AfterRecalculateItemPricesEventHandler([MarshalAs(UnmanagedType.Interface)] SalesDocItem SalesDocItem);
