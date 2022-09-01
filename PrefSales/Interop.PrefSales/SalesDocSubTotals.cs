using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[CoClass(typeof(SalesDocSubTotalsClass))]
[Guid("FDE52030-F6BE-47A2-B172-AE4A36FC6DE6")]
public interface SalesDocSubTotals : ISalesDocSubTotals
{
}
