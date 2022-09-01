using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[CoClass(typeof(SalesDocDatesClass))]
[Guid("997AF85E-A32E-4B6B-B786-CD8F84BFDD08")]
public interface SalesDocDates : ISalesDocDates
{
}
