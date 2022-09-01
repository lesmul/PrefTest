using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Interop.PrefCAD;

[ComImport]
[CompilerGenerated]
[Guid("23AF50E0-3CBB-11D2-AEE9-00A0C92F860F")]
[TypeIdentifier]
public interface IDualModelo
{
	void _VtblGap1_124();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(59)]
	bool CargaModelo([In][MarshalAs(UnmanagedType.BStr)] string Nombre, [In] bool Pattern = false);

	void _VtblGap2_36();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(105)]
	[return: MarshalAs(UnmanagedType.BStr)]
	string GetXMLCode([In] XMLOptionEnum Options = XMLOptionEnum.xmlDefault);
}
