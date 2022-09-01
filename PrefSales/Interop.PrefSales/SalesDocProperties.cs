using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[Guid("F6F1F0C6-3A4B-479A-9037-5334161D56B7")]
[CoClass(typeof(SalesDocPropertiesClass))]
public interface SalesDocProperties : ISalesDocProperties
{
}
