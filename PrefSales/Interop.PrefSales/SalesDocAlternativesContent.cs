using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[Guid("B0826A15-A889-4551-85DA-9E1AF2F9D8A0")]
[CoClass(typeof(SalesDocAlternativesContentClass))]
public interface SalesDocAlternativesContent : ISalesDocAlternativesContent
{
}
