using System.Runtime.InteropServices;
using Interop.PrefCAD;
using Interop.PrefPrices;

namespace Interop.PrefSales;

[TypeLibType(TypeLibTypeFlags.FHidden)]
[ComVisible(false)]
public delegate void _ISalesDocEvents_RecalculatePerVolumeEventHandler([MarshalAs(UnmanagedType.Interface)] SalesDoc SalesDoc, [MarshalAs(UnmanagedType.Interface)] TariffExceptions TariffExceptions, [MarshalAs(UnmanagedType.Interface)] RawMaterialVolumeItems RawMaterial);
