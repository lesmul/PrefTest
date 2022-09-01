using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Interop.PrefView;

[ComImport]
[CompilerGenerated]
[Guid("D863F3A0-7360-4B71-BC4A-FD168941003C")]
[TypeIdentifier]
public interface IPrefModelRenderer
{
	[DispId(5)]
	string ConnectionString
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(5)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(5)]
		[param: In]
		[param: MarshalAs(UnmanagedType.BStr)]
		set;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1)]
	void SetXMLDraw([In][MarshalAs(UnmanagedType.BStr)] string strCode);

	void _VtblGap1_4();

	void _VtblGap2_38();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(45)]
	[return: MarshalAs(UnmanagedType.BStr)]
	string GetWPF([In] WPFKind Kind);

	void _VtblGap3_13();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(55)]
	[return: MarshalAs(UnmanagedType.BStr)]
	string GetWPFMaterial([In] WPFKind Kind, [In][MarshalAs(UnmanagedType.BStr)] string strMaterial);
}
