using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[CoClass(typeof(SalesDocFieldClass))]
[Guid("CDFF6E60-B3DA-4285-AF75-30DC9E5ADA28")]
public interface SalesDocField : ISalesDocField
{
}
