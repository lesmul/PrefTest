using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[CoClass(typeof(SalesDocFinderClass))]
[Guid("E2714B61-A3EA-4166-B4DC-A59275B33D24")]
public interface SalesDocFinder : ISalesDocFinder, _ISalesDocFinderEvents_Event
{
}
