using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[CoClass(typeof(SalesDocServiceCodesClass))]
[Guid("70CF40DD-D2B9-4B47-9697-5480C2917209")]
public interface SalesDocServiceCodes : ISalesDocServiceCodes
{
}
