using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[TypeLibType(TypeLibTypeFlags.FHidden)]
[ComVisible(false)]
public delegate void _ISalesDocEvents_AfterItemChangedEventHandler([MarshalAs(UnmanagedType.Interface)] SalesDocItem SalesDocItem, [MarshalAs(UnmanagedType.BStr)] string FieldName);
