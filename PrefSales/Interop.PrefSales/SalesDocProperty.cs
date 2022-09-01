using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[Guid("7B0D2092-9F73-4219-80F9-647EF53EC23D")]
[CoClass(typeof(SalesDocPropertyClass))]
public interface SalesDocProperty : ISalesDocProperty
{
}
