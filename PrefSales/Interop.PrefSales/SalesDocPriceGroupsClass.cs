using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[Guid("4E59DD3E-2E94-46DD-8A8F-A3E6B498C10F")]
[TypeLibType(TypeLibTypeFlags.FCanCreate)]
[ClassInterface(ClassInterfaceType.None)]
public class SalesDocPriceGroupsClass : ISalesDocPriceGroups, SalesDocPriceGroups, IEnumerable
{
	[DispId(1610743808)]
	public virtual extern int Count
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743808)]
		get;
	}

	[DispId(0)]
	public virtual extern SalesDocPriceGroup this[[In] int n]
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(0)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern SalesDocPriceGroupsClass();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(-4)]
	[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "System.Runtime.InteropServices.CustomMarshalers.EnumeratorToEnumVariantMarshaler, CustomMarshalers, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public virtual extern IEnumerator GetEnumerator();

	IEnumerator ISalesDocPriceGroups.GetEnumerator()
	{
		//ILSpy generated this explicit interface implementation from .override directive in GetEnumerator
		return this.GetEnumerator();
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743811)]
	public virtual extern bool Exists([In] int lPriceGroupId);

	bool ISalesDocPriceGroups.Exists([In] int lPriceGroupId)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Exists
		return this.Exists(lPriceGroupId);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743812)]
	[return: MarshalAs(UnmanagedType.Interface)]
	public virtual extern SalesDocPriceGroup Add([In] int lPriceGroupId);

	SalesDocPriceGroup ISalesDocPriceGroups.Add([In] int lPriceGroupId)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Add
		return this.Add(lPriceGroupId);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743813)]
	public virtual extern void Reset();

	void ISalesDocPriceGroups.Reset()
	{
		//ILSpy generated this explicit interface implementation from .override directive in Reset
		this.Reset();
	}
}
