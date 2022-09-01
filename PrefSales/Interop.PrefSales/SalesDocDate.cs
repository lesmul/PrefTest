using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[Guid("A7F96EFF-1295-4C97-BA59-F081DF1B53E3")]
[CoClass(typeof(SalesDocDateClass))]
public interface SalesDocDate : ISalesDocDate
{
}
