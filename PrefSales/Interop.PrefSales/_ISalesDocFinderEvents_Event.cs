using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComVisible(false)]
[TypeLibType(TypeLibTypeFlags.FHidden)]
[ComEventInterface(typeof(_ISalesDocFinderEvents), typeof(_ISalesDocFinderEvents_EventProvider))]
public interface _ISalesDocFinderEvents_Event
{
}
