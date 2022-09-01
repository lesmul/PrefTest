using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[Guid("75536F24-8A8E-4717-8BD2-65B7A5B95355")]
[CoClass(typeof(SalesDocItemsClass))]
public interface SalesDocItems : ISalesDocItems
{
}
