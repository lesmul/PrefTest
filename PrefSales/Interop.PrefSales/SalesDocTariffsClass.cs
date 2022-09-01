using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[TypeLibType(TypeLibTypeFlags.FCanCreate)]
[Guid("AA04E44E-833E-460A-A600-EC0CFCDF527A")]
[ClassInterface(ClassInterfaceType.None)]
public class SalesDocTariffsClass : ISalesDocTariffs, SalesDocTariffs, IEnumerable
{
	[DispId(1610743808)]
	public virtual extern int Count
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743808)]
		get;
	}

	[DispId(0)]
	public virtual extern SalesDocTariff this[[In][MarshalAs(UnmanagedType.Struct)] object Index]
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(0)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern SalesDocTariffsClass();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(-4)]
	[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "System.Runtime.InteropServices.CustomMarshalers.EnumeratorToEnumVariantMarshaler, CustomMarshalers, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public virtual extern IEnumerator GetEnumerator();

	IEnumerator ISalesDocTariffs.GetEnumerator()
	{
		//ILSpy generated this explicit interface implementation from .override directive in GetEnumerator
		return this.GetEnumerator();
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743811)]
	public virtual extern bool Exists([In][MarshalAs(UnmanagedType.BStr)] string Name);

	bool ISalesDocTariffs.Exists([In][MarshalAs(UnmanagedType.BStr)] string Name)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Exists
		return this.Exists(Name);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743812)]
	[return: MarshalAs(UnmanagedType.Interface)]
	public virtual extern SalesDocTariff Add([In][MarshalAs(UnmanagedType.BStr)] string Name);

	SalesDocTariff ISalesDocTariffs.Add([In][MarshalAs(UnmanagedType.BStr)] string Name)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Add
		return this.Add(Name);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743813)]
	[return: MarshalAs(UnmanagedType.Interface)]
	public virtual extern SalesDocTariff Insert([In] int Position, [In][MarshalAs(UnmanagedType.BStr)] string Name);

	SalesDocTariff ISalesDocTariffs.Insert([In] int Position, [In][MarshalAs(UnmanagedType.BStr)] string Name)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Insert
		return this.Insert(Position, Name);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743814)]
	public virtual extern void Remove([In][MarshalAs(UnmanagedType.Struct)] object Index);

	void ISalesDocTariffs.Remove([In][MarshalAs(UnmanagedType.Struct)] object Index)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Remove
		this.Remove(Index);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743815)]
	public virtual extern void Reset();

	void ISalesDocTariffs.Reset()
	{
		//ILSpy generated this explicit interface implementation from .override directive in Reset
		this.Reset();
	}
}
