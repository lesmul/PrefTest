using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[Guid("0F192B88-D07C-4873-8C63-927173362063")]
[CoClass(typeof(SalesDocTariffsClass))]
public interface SalesDocTariffs : ISalesDocTariffs
{
}
