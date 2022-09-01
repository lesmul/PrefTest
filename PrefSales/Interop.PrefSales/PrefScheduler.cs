using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[Guid("5648E59D-5EDE-4D53-9BD0-3B1962CF1A29")]
[CoClass(typeof(PrefSchedulerClass))]
public interface PrefScheduler : IPrefScheduler, _IPrefSchedulerEvents_Event
{
}
