using System.Runtime.InteropServices;
using Interop.MSXML2;

namespace Interop.PrefSales;

[TypeLibType(TypeLibTypeFlags.FHidden)]
[ComVisible(false)]
public delegate void _IPrefSchedulerEvents_BeforeLoadSchedulerEventHandler([MarshalAs(UnmanagedType.Interface)] PrefScheduler Scheduler, [MarshalAs(UnmanagedType.Interface)] IXMLDOMDocument2 pXML);
[ComVisible(false)]
[TypeLibType(TypeLibTypeFlags.FHidden)]
public delegate void _IPrefSchedulerEvents_BeforeLoadSelectionDetailEventHandler([MarshalAs(UnmanagedType.Interface)] PrefScheduler Scheduler, [MarshalAs(UnmanagedType.Interface)] IXMLDOMDocument2 pXML);
