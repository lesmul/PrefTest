using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[TypeLibType(TypeLibTypeFlags.FHidden)]
[ComVisible(false)]
public delegate void _ISalesDocEvents_BlockDocumentEventHandler([MarshalAs(UnmanagedType.Interface)] SalesDoc SalesDoc);
