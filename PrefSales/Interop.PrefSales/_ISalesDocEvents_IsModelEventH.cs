using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[TypeLibType(TypeLibTypeFlags.FHidden)]
[ComVisible(false)]
public delegate void _ISalesDocEvents_IsModelEventHandler([MarshalAs(UnmanagedType.Interface)] SalesDoc SalesDoc, [MarshalAs(UnmanagedType.BStr)] string Name, ref bool IsModel);
