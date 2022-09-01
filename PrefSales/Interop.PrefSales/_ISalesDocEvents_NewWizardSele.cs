using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComVisible(false)]
[TypeLibType(TypeLibTypeFlags.FHidden)]
public delegate void _ISalesDocEvents_NewWizardSelectNumerationEventHandler([MarshalAs(UnmanagedType.Interface)] SalesDoc SalesDoc, int lDestDocumentType, ref int lNumerationId);
