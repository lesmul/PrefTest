using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[Guid("2FEF9D8F-D928-48C2-8663-F66B7CEBF56E")]
[CoClass(typeof(SalesDocFieldsClass))]
public interface SalesDocFields : ISalesDocFields
{
}
