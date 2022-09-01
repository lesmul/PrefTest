using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComVisible(false)]
[TypeLibType(TypeLibTypeFlags.FHidden)]
public delegate void _ISalesDocEvents_IsMaterialEventHandler([MarshalAs(UnmanagedType.Interface)] SalesDoc SalesDoc, [MarshalAs(UnmanagedType.BStr)] ref string Code, ref bool IsMaterial);
