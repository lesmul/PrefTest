using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[TypeLibType(TypeLibTypeFlags.FCanCreate)]
[Guid("4A5B129B-E488-4119-87F8-A8D77A6B8431")]
[ClassInterface(ClassInterfaceType.None)]
public class SalesDocDatesClass : ISalesDocDates, SalesDocDates, IEnumerable
{
	[DispId(1610743808)]
	public virtual extern int Count
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743808)]
		get;
	}

	[DispId(0)]
	public virtual extern SalesDocDate this[[In] int Index]
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(0)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	[DispId(1610743811)]
	public virtual extern bool IsDirty
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743811)]
		get;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern SalesDocDatesClass();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(-4)]
	[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "System.Runtime.InteropServices.CustomMarshalers.EnumeratorToEnumVariantMarshaler, CustomMarshalers, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public virtual extern IEnumerator GetEnumerator();

	IEnumerator ISalesDocDates.GetEnumerator()
	{
		//ILSpy generated this explicit interface implementation from .override directive in GetEnumerator
		return this.GetEnumerator();
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743812)]
	public virtual extern void Remove([In] int TaskId);

	void ISalesDocDates.Remove([In] int TaskId)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Remove
		this.Remove(TaskId);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743813)]
	public virtual extern void ApplyBooking();

	void ISalesDocDates.ApplyBooking()
	{
		//ILSpy generated this explicit interface implementation from .override directive in ApplyBooking
		this.ApplyBooking();
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743814)]
	public virtual extern void CancelBooking();

	void ISalesDocDates.CancelBooking()
	{
		//ILSpy generated this explicit interface implementation from .override directive in CancelBooking
		this.CancelBooking();
	}
}
