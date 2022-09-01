using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[Guid("A42576D3-C72A-4B67-8322-755DCF31E81D")]
[ClassInterface(ClassInterfaceType.None)]
[TypeLibType(TypeLibTypeFlags.FCanCreate)]
public class SalesDocPropertiesClass : ISalesDocProperties, SalesDocProperties, IEnumerable
{
	[DispId(1610743808)]
	public virtual extern int Count
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743808)]
		get;
	}

	[DispId(0)]
	public virtual extern SalesDocProperty this[[In][MarshalAs(UnmanagedType.Struct)] object Index]
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(0)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern SalesDocPropertiesClass();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(-4)]
	[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "System.Runtime.InteropServices.CustomMarshalers.EnumeratorToEnumVariantMarshaler, CustomMarshalers, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public virtual extern IEnumerator GetEnumerator();

	IEnumerator ISalesDocProperties.GetEnumerator()
	{
		//ILSpy generated this explicit interface implementation from .override directive in GetEnumerator
		return this.GetEnumerator();
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743811)]
	public virtual extern bool Exists([In][MarshalAs(UnmanagedType.Struct)] object Index);

	bool ISalesDocProperties.Exists([In][MarshalAs(UnmanagedType.Struct)] object Index)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Exists
		return this.Exists(Index);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743812)]
	[return: MarshalAs(UnmanagedType.Interface)]
	public virtual extern SalesDocProperty Add([In][MarshalAs(UnmanagedType.BStr)] string Name);

	SalesDocProperty ISalesDocProperties.Add([In][MarshalAs(UnmanagedType.BStr)] string Name)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Add
		return this.Add(Name);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743813)]
	public virtual extern void Remove([In][MarshalAs(UnmanagedType.BStr)] string Name);

	void ISalesDocProperties.Remove([In][MarshalAs(UnmanagedType.BStr)] string Name)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Remove
		this.Remove(Name);
	}
}
