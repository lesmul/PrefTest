using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComEventInterface(typeof(_IPrefSchedulerEvents), typeof(_IPrefSchedulerEvents_EventProvider))]
[ComVisible(false)]
[TypeLibType(TypeLibTypeFlags.FHidden)]
public interface _IPrefSchedulerEvents_Event
{
	event _IPrefSchedulerEvents_BeforeLoadSchedulerEventHandler BeforeLoadScheduler;

	event _IPrefSchedulerEvents_BeforeLoadSelectionDetailEventHandler BeforeLoadSelectionDetail;

	event _IPrefSchedulerEvents_OrderDoubleClickEventHandler OrderDoubleClick;

	event _IPrefSchedulerEvents_PODoubleClickEventHandler PODoubleClick;
}
