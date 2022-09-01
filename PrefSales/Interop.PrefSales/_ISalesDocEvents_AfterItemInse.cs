using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComVisible(false)]
[TypeLibType(TypeLibTypeFlags.FHidden)]
public delegate void _ISalesDocEvents_AfterItemInsertedEventHandler([MarshalAs(UnmanagedType.Interface)] SalesDocItem SalesDocItem);
