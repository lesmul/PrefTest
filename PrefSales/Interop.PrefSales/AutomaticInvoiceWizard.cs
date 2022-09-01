using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[CoClass(typeof(AutomaticInvoiceWizardClass))]
[Guid("77F9E153-5E44-4A60-9650-941D8033542E")]
public interface AutomaticInvoiceWizard : IAutomaticInvoiceWizard
{
}
