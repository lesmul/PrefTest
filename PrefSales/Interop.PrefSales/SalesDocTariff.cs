using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[CoClass(typeof(SalesDocTariffClass))]
[Guid("413BF231-5203-4B88-8164-E97EE2C6F9C3")]
public interface SalesDocTariff : ISalesDocTariff
{
}
