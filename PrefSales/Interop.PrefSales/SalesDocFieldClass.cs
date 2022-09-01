using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[Guid("A31F7072-ED68-4D1E-BCF1-1E81AF6CFA8B")]
[ClassInterface(ClassInterfaceType.None)]
[TypeLibType(TypeLibTypeFlags.FCanCreate)]
[DefaultMember("Value")]
public class SalesDocFieldClass : ISalesDocField, SalesDocField
{
	[DispId(0)]
	public virtual extern object Value
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(0)]
		[return: MarshalAs(UnmanagedType.Struct)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(0)]
		[param: In]
		[param: MarshalAs(UnmanagedType.Struct)]
		set;
	}

	[DispId(1610743810)]
	public virtual extern string Name
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743810)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
	}

	[DispId(1610743811)]
	public virtual extern int DataType
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743811)]
		get;
	}

	[DispId(1610743812)]
	public virtual extern bool IsDirty
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743812)]
		get;
	}

	[DispId(1610743813)]
	public virtual extern bool Blocked
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743813)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743813)]
		[param: In]
		set;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern SalesDocFieldClass();
}
