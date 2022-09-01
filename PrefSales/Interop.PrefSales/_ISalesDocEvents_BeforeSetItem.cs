using System.Runtime.InteropServices;
using Interop.MSXML2;

namespace Interop.PrefSales;

[TypeLibType(TypeLibTypeFlags.FHidden)]
[ComVisible(false)]
public delegate void _ISalesDocEvents_BeforeSetItemContextEventHandler([MarshalAs(UnmanagedType.Interface)] SalesDocItem SalesDocItem, [MarshalAs(UnmanagedType.Interface)] IXMLDOMDocument2 XMLContext);
[TypeLibType(TypeLibTypeFlags.FHidden)]
[ComVisible(false)]
public delegate void _ISalesDocEvents_BeforeSetItemXmlEventHandler([MarshalAs(UnmanagedType.Interface)] SalesDocItem SalesDocItem, [MarshalAs(UnmanagedType.Interface)] IXMLDOMDocument2 ItemXml);
