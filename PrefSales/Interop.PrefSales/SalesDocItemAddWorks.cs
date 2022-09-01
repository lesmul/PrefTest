using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[CoClass(typeof(SalesDocItemAddWorksClass))]
[Guid("63F89207-A3C7-473E-8C43-6FBB6A2B7B56")]
public interface SalesDocItemAddWorks : ISalesDocItemAddWorks
{
}
