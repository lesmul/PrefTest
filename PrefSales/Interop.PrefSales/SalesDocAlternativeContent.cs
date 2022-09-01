using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[CoClass(typeof(SalesDocAlternativeContentClass))]
[Guid("E2488898-35B7-48D6-9BDF-E9E5D575C794")]
public interface SalesDocAlternativeContent : ISalesDocAlternativeContent
{
}
