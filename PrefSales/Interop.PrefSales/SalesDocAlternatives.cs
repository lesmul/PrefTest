using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[CoClass(typeof(SalesDocAlternativesClass))]
[Guid("CDE8D2E1-1ABE-4DB9-8707-6C7DD3D12B68")]
public interface SalesDocAlternatives : ISalesDocAlternatives
{
}
