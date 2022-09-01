using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[Guid("8C535848-02BD-4C0E-95F0-E6F0B9D5CB8F")]
[CoClass(typeof(VersionIndirectionWizardClass))]
public interface VersionIndirectionWizard : IVersionIndirectionWizard
{
}
