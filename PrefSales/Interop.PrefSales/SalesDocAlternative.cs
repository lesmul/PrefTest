using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[CoClass(typeof(SalesDocAlternativeClass))]
[Guid("047F921E-5FA3-49F2-AA9C-AE8D3DC39880")]
public interface SalesDocAlternative : ISalesDocAlternative
{
}
