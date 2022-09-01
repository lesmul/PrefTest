using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[Guid("A40B43D3-E818-405E-A6C8-E6B2314570F3")]
[CoClass(typeof(SalesDocServiceCodeClass))]
public interface SalesDocServiceCode : ISalesDocServiceCode
{
}
