using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[CoClass(typeof(SalesDocItemAddWorkClass))]
[Guid("2C1F24C5-EE0E-44C6-8CA6-DE3A913097CB")]
public interface SalesDocItemAddWork : ISalesDocItemAddWork
{
}
