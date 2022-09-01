using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[Guid("B6696F78-EB27-4CD2-981B-51026213E5BF")]
[CoClass(typeof(PrefSalesLocationsClass))]
public interface PrefSalesLocations : IPrefSalesLocations
{
}
