using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[TypeLibType(TypeLibTypeFlags.FHidden)]
[ComVisible(false)]
public delegate void _ISalesDocEvents_ReplaceItemCodeEventHandler([MarshalAs(UnmanagedType.Interface)] SalesDocItem SalesDocItem, [ComAliasName("Interop.PrefSales.SalesDocItemType")] SalesDocItemType lItemType, [MarshalAs(UnmanagedType.BStr)] ref string Code);
