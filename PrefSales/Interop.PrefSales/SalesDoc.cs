using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[Guid("0B0E5F00-BC94-4E20-80E2-1CA3A10E9E03")]
[CoClass(typeof(SalesDocClass))]
public interface SalesDoc : ISalesDoc, _ISalesDocEvents_Event
{
}
