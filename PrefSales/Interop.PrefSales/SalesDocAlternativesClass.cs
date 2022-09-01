using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[ClassInterface(ClassInterfaceType.None)]
[Guid("371900FC-E9F9-4AC7-A983-F184E924D257")]
[TypeLibType(TypeLibTypeFlags.FCanCreate)]
public class SalesDocAlternativesClass : ISalesDocAlternatives, SalesDocAlternatives, IEnumerable
{
	[DispId(1610743808)]
	public virtual extern int Count
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743808)]
		get;
	}

	[DispId(0)]
	public virtual extern SalesDocAlternative this[[In][MarshalAs(UnmanagedType.Struct)] object Index]
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(0)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern SalesDocAlternativesClass();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(-4)]
	[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "System.Runtime.InteropServices.CustomMarshalers.EnumeratorToEnumVariantMarshaler, CustomMarshalers, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public virtual extern IEnumerator GetEnumerator();

	IEnumerator ISalesDocAlternatives.GetEnumerator()
	{
		//ILSpy generated this explicit interface implementation from .override directive in GetEnumerator
		return this.GetEnumerator();
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743811)]
	[return: MarshalAs(UnmanagedType.Interface)]
	public virtual extern SalesDocAlternative Add();

	SalesDocAlternative ISalesDocAlternatives.Add()
	{
		//ILSpy generated this explicit interface implementation from .override directive in Add
		return this.Add();
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743812)]
	[return: MarshalAs(UnmanagedType.Interface)]
	public virtual extern SalesDocAlternative Insert([In] int Position);

	SalesDocAlternative ISalesDocAlternatives.Insert([In] int Position)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Insert
		return this.Insert(Position);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743813)]
	public virtual extern void Remove([In][MarshalAs(UnmanagedType.Struct)] object Index);

	void ISalesDocAlternatives.Remove([In][MarshalAs(UnmanagedType.Struct)] object Index)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Remove
		this.Remove(Index);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743814)]
	public virtual extern void Reset();

	void ISalesDocAlternatives.Reset()
	{
		//ILSpy generated this explicit interface implementation from .override directive in Reset
		this.Reset();
	}
}
