using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Interop.PrefCAD;

namespace Interop.PrefSales;

[ComImport]
[TypeLibType(TypeLibTypeFlags.FDual | TypeLibTypeFlags.FNonExtensible | TypeLibTypeFlags.FDispatchable)]
[Guid("047F921E-5FA3-49F2-AA9C-AE8D3DC39880")]
public interface ISalesDocAlternative
{
	[DispId(1610743808)]
	string RowId
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743808)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
	}

	[DispId(1610743809)]
	int Position
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743809)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743809)]
		[param: In]
		set;
	}

	[DispId(1610743811)]
	bool Enabled
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743811)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743811)]
		[param: In]
		set;
	}

	[DispId(1610743813)]
	string Name
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743813)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743813)]
		[param: In]
		[param: MarshalAs(UnmanagedType.BStr)]
		set;
	}

	[DispId(1610743815)]
	string PrintDescription
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743815)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743815)]
		[param: In]
		[param: MarshalAs(UnmanagedType.BStr)]
		set;
	}

	[DispId(1610743817)]
	int Type
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743817)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743817)]
		[param: In]
		set;
	}

	[DispId(1610743819)]
	string XMLProperties
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743819)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743819)]
		[param: In]
		[param: MarshalAs(UnmanagedType.BStr)]
		set;
	}

	[DispId(1610743821)]
	string XMLGlass
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743821)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743821)]
		[param: In]
		[param: MarshalAs(UnmanagedType.BStr)]
		set;
	}

	[DispId(1610743823)]
	Options Options
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743823)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743823)]
		[param: In]
		[param: MarshalAs(UnmanagedType.Interface)]
		set;
	}

	[DispId(1610743825)]
	string XMLVariant
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743825)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743825)]
		[param: In]
		[param: MarshalAs(UnmanagedType.BStr)]
		set;
	}
}
