using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[Guid("FBCA0A51-95C3-4EA8-B88F-73C4313812CD")]
[CoClass(typeof(AssignOptionsWizardClass))]
public interface AssignOptionsWizard : IAssignOptionsWizard
{
}
