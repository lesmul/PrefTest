using System.Runtime.InteropServices;
using Interop.MSXML2;

namespace Interop.PrefSales;

[TypeLibType(TypeLibTypeFlags.FHidden)]
[ComVisible(false)]
public delegate void _ISalesDocEvents_BeforeCreateSalesDocEventHandler([MarshalAs(UnmanagedType.Interface)] SalesDoc SalesDoc, ref bool Cancel, [MarshalAs(UnmanagedType.Interface)] IXMLDOMDocument2 XMLSalesDoc);
