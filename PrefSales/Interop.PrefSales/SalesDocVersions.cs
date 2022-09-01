using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[CoClass(typeof(SalesDocVersionsClass))]
[Guid("2C73CFDC-5C2C-485E-840D-96D9FD474176")]
public interface SalesDocVersions : ISalesDocVersions
{
}
