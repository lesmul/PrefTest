using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[Guid("F0F8E620-F911-416E-AC1F-A14426471598")]
[CoClass(typeof(SalesDocSubTotalClass))]
public interface SalesDocSubTotal : ISalesDocSubTotal
{
}
