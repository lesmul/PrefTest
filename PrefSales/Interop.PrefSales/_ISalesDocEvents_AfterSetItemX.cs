using System.Runtime.InteropServices;
using Interop.MSXML2;

namespace Interop.PrefSales;

[TypeLibType(TypeLibTypeFlags.FHidden)]
[ComVisible(false)]
public delegate void _ISalesDocEvents_AfterSetItemXmlEventHandler([MarshalAs(UnmanagedType.Interface)] SalesDocItem SalesDocItem, [MarshalAs(UnmanagedType.Interface)] IXMLDOMDocument2 ItemXml);
