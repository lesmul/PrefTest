using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[TypeLibType(TypeLibTypeFlags.FHidden)]
[ComVisible(false)]
public delegate void _ISalesDocEvents_BeforeRemoveOfferEventHandler([MarshalAs(UnmanagedType.Interface)] SalesDoc SalesDoc, ref bool Cancel);
[ComVisible(false)]
[TypeLibType(TypeLibTypeFlags.FHidden)]
public delegate void _ISalesDocEvents_BeforeRemoveOrderEventHandler([MarshalAs(UnmanagedType.Interface)] SalesDoc SalesDoc, ref bool Cancel);
