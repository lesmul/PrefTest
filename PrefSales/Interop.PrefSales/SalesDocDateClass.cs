using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[Guid("7ED10296-A6F3-423A-B275-CE4B71AB39DB")]
[TypeLibType(TypeLibTypeFlags.FCanCreate)]
[ClassInterface(ClassInterfaceType.None)]
public class SalesDocDateClass : ISalesDocDate, SalesDocDate
{
	[DispId(1610743808)]
	public virtual extern string RowId
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743808)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
	}

	[DispId(1610743809)]
	public virtual extern int Position
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743809)]
		get;
	}

	[DispId(1610743810)]
	public virtual extern string TaskName
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
	public virtual extern string TaskDescription
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
	public virtual extern int TaskId
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
	public virtual extern DateTime TaskDate
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
	public virtual extern DateTime TaskBookedDate
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
	public virtual extern DateTime TaskRealDate
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
	public virtual extern int TaskDuration
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
	public virtual extern string FiringEvent
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

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern SalesDocDateClass();
}
