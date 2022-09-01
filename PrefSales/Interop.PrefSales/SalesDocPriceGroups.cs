using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[CoClass(typeof(SalesDocPriceGroupsClass))]
[Guid("BE895971-3A2A-4652-9FDC-3AF67EDCBC90")]
public interface SalesDocPriceGroups : ISalesDocPriceGroups
{
}
