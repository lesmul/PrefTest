using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[CoClass(typeof(SalesDocItemClass))]
[Guid("D2177AF4-B00E-47F4-A336-B950E36BCB9D")]
public interface SalesDocItem : ISalesDocItem
{
}
