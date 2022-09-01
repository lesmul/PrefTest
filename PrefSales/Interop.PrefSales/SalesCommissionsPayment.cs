using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[Guid("33435A51-9E73-4230-BBD6-01AE7CB9B952")]
[CoClass(typeof(SalesCommissionsPaymentClass))]
public interface SalesCommissionsPayment : ISalesCommissionsPayment
{
}
