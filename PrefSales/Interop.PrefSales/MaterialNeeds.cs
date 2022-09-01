using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[CoClass(typeof(MaterialNeedsClass))]
[Guid("91D00E8C-6B7E-4CC0-BB24-CC833F7500B6")]
public interface MaterialNeeds : IMaterialNeeds, _IMaterialNeedsEvents_Event
{
}
