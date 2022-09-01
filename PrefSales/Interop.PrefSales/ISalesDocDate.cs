using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[TypeLibType(TypeLibTypeFlags.FDual | TypeLibTypeFlags.FNonExtensible | TypeLibTypeFlags.FDispatchable)]
[Guid("A7F96EFF-1295-4C97-BA59-F081DF1B53E3")]
public interface ISalesDocDate
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
	}

	[DispId(1610743810)]
	string TaskName
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743810)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743810)]
		[param: In]
		[param: MarshalAs(UnmanagedType.BStr)]
		set;
	}

	[DispId(1610743812)]
	string TaskDescription
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743812)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743812)]
		[param: In]
		[param: MarshalAs(UnmanagedType.BStr)]
		set;
	}

	[DispId(1610743814)]
	int TaskId
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743814)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743814)]
		[param: In]
		set;
	}

	[DispId(1610743816)]
	DateTime TaskDate
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743816)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743816)]
		[param: In]
		set;
	}

	[DispId(1610743818)]
	DateTime TaskBookedDate
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743818)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743818)]
		[param: In]
		set;
	}

	[DispId(1610743820)]
	DateTime TaskRealDate
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743820)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743820)]
		[param: In]
		set;
	}

	[DispId(1610743822)]
	int TaskDuration
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743822)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743822)]
		[param: In]
		set;
	}

	[DispId(1610743824)]
	string FiringEvent
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743824)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743824)]
		[param: In]
		[param: MarshalAs(UnmanagedType.BStr)]
		set;
	}
}
