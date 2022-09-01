using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComVisible(false)]
[TypeLibType(TypeLibTypeFlags.FHidden)]
public delegate void _IPrefSchedulerEvents_OrderDoubleClickEventHandler(int Number, int Version);
