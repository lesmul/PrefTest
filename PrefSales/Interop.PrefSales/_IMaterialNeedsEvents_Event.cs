using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComEventInterface(typeof(_IMaterialNeedsEvents), typeof(_IMaterialNeedsEvents_EventProvider))]
[ComVisible(false)]
[TypeLibType(TypeLibTypeFlags.FHidden)]
public interface _IMaterialNeedsEvents_Event
{
	event _IMaterialNeedsEvents_BeforeCalculateMaterialNeedsEventHandler BeforeCalculateMaterialNeeds;

	event _IMaterialNeedsEvents_AfterCalculateMaterialNeedsEventHandler AfterCalculateMaterialNeeds;

	event _IMaterialNeedsEvents_BeforeRemoveMaterialNeedsEventHandler BeforeRemoveMaterialNeeds;
}
