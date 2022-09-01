using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[CoClass(typeof(NewDocumentWizardClass))]
[Guid("729C4D51-1742-47D8-89E4-169C1E42ECB2")]
public interface NewDocumentWizard : INewDocumentWizard
{
}
