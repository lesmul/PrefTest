using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[Guid("9229AB6D-933A-457A-A9D4-3830352711E8")]
[CoClass(typeof(SalesDocVersionClass))]
public interface SalesDocVersion : ISalesDocVersion
{
}
