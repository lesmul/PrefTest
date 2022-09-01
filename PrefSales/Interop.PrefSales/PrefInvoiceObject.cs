using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[CoClass(typeof(PrefInvoiceObjectClass))]
[Guid("F96C79B9-293B-44E0-8242-DAB05886FD12")]
public interface PrefInvoiceObject : IPrefInvoiceObject
{
}
