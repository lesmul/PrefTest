using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[Guid("370C7894-74C0-4D49-BF90-D0E146719167")]
[CoClass(typeof(SalesDocPriceGroupClass))]
public interface SalesDocPriceGroup : ISalesDocPriceGroup
{
}
