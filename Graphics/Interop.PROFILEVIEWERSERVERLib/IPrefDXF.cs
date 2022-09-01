using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Interop.PROFILEVIEWERSERVERLib;

[ComImport]
[CompilerGenerated]
[Guid("CAF43ADE-5186-4538-9FD5-7CC56F27C41E")]
[TypeIdentifier]
public interface IPrefDXF
{
	[DispId(1)]
	string ConnectionString
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1)]
		[param: MarshalAs(UnmanagedType.BStr)]
		set;
	}

	[DispId(2)]
	string WhereSentence
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(2)]
		[param: MarshalAs(UnmanagedType.BStr)]
		set;
	}

	void _VtblGap1_3();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(6)]
	void UpdateSilverlightXamls();
}
