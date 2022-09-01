using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[TypeLibType(TypeLibTypeFlags.FHidden)]
[ComVisible(false)]
public delegate void _IPrefSchedulerEvents_PODoubleClickEventHandler(int Number, int Numeration);
