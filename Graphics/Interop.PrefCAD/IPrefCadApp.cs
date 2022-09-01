using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Interop.PrefCAD;

[ComImport]
[CompilerGenerated]
[Guid("369FC323-D0BA-11D2-86A2-0060081FCD9C")]
[TypeIdentifier]
public interface IPrefCadApp
{
	[DispId(4)]
	string ConnectionString
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(4)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(4)]
		[param: In]
		[param: MarshalAs(UnmanagedType.BStr)]
		set;
	}

	void _VtblGap1_3();

	void _VtblGap2_25();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(13)]
	void Init();
}
