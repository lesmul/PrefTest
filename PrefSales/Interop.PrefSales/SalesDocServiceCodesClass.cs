using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[Guid("11FD4579-7A80-413E-BA34-285B482D841C")]
[ClassInterface(ClassInterfaceType.None)]
[TypeLibType(TypeLibTypeFlags.FCanCreate)]
public class SalesDocServiceCodesClass : ISalesDocServiceCodes, SalesDocServiceCodes, IEnumerable
{
	[DispId(1610743808)]
	public virtual extern int Count
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743808)]
		get;
	}

	[DispId(0)]
	public virtual extern SalesDocServiceCode this[[In][MarshalAs(UnmanagedType.Struct)] object Index]
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(0)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern SalesDocServiceCodesClass();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(-4)]
	[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "System.Runtime.InteropServices.CustomMarshalers.EnumeratorToEnumVariantMarshaler, CustomMarshalers, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public virtual extern IEnumerator GetEnumerator();

	IEnumerator ISalesDocServiceCodes.GetEnumerator()
	{
		//ILSpy generated this explicit interface implementation from .override directive in GetEnumerator
		return this.GetEnumerator();
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743811)]
	[return: MarshalAs(UnmanagedType.Interface)]
	public virtual extern SalesDocServiceCode Add([In] int ServiceCode);

	SalesDocServiceCode ISalesDocServiceCodes.Add([In] int ServiceCode)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Add
		return this.Add(ServiceCode);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743812)]
	[return: MarshalAs(UnmanagedType.Interface)]
	public virtual extern SalesDocServiceCode Insert([In] int ServiceCode, [In] int Position);

	SalesDocServiceCode ISalesDocServiceCodes.Insert([In] int ServiceCode, [In] int Position)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Insert
		return this.Insert(ServiceCode, Position);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743813)]
	public virtual extern void Remove([In][MarshalAs(UnmanagedType.Struct)] object Index);

	void ISalesDocServiceCodes.Remove([In][MarshalAs(UnmanagedType.Struct)] object Index)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Remove
		this.Remove(Index);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743814)]
	public virtual extern void Reset();

	void ISalesDocServiceCodes.Reset()
	{
		//ILSpy generated this explicit interface implementation from .override directive in Reset
		this.Reset();
	}
}
