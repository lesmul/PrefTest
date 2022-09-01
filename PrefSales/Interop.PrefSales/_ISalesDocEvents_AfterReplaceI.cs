using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[TypeLibType(TypeLibTypeFlags.FHidden)]
[ComVisible(false)]
public delegate void _ISalesDocEvents_AfterReplaceItemCodeEventHandler([MarshalAs(UnmanagedType.Interface)] SalesDocItem SalesDocItem, [ComAliasName("Interop.PrefSales.SalesDocItemType")] SalesDocItemType lOldItemType, [MarshalAs(UnmanagedType.BStr)] string OOldCode, [ComAliasName("Interop.PrefSales.SalesDocItemType")] SalesDocItemType lNewItemType, [MarshalAs(UnmanagedType.BStr)] string NewCode);
