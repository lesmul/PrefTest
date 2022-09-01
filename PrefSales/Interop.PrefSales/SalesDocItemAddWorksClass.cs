using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[Guid("B0E8CEFE-5C6E-4B03-BF92-E56EAE8FEEBF")]
[ClassInterface(ClassInterfaceType.None)]
[TypeLibType(TypeLibTypeFlags.FCanCreate)]
public class SalesDocItemAddWorksClass : ISalesDocItemAddWorks, SalesDocItemAddWorks, IEnumerable
{
	[DispId(1610743808)]
	public virtual extern int Count
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743808)]
		get;
	}

	[DispId(0)]
	public virtual extern SalesDocItemAddWork this[[In] short Code]
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(0)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	[DispId(1610743815)]
	public virtual extern double AddWorksTotalValue
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743815)]
		get;
	}

	[DispId(1610743816)]
	public virtual extern string XmlSerialization
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743816)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743816)]
		[param: In]
		[param: MarshalAs(UnmanagedType.BStr)]
		set;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern SalesDocItemAddWorksClass();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(-4)]
	[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "System.Runtime.InteropServices.CustomMarshalers.EnumeratorToEnumVariantMarshaler, CustomMarshalers, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public virtual extern IEnumerator GetEnumerator();

	IEnumerator ISalesDocItemAddWorks.GetEnumerator()
	{
		//ILSpy generated this explicit interface implementation from .override directive in GetEnumerator
		return this.GetEnumerator();
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743811)]
	[return: MarshalAs(UnmanagedType.Interface)]
	public virtual extern SalesDocItemAddWork Add([In] short AddWorkCode);

	SalesDocItemAddWork ISalesDocItemAddWorks.Add([In] short AddWorkCode)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Add
		return this.Add(AddWorkCode);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743812)]
	public virtual extern void Remove([In] short Code);

	void ISalesDocItemAddWorks.Remove([In] short Code)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Remove
		this.Remove(Code);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743813)]
	public virtual extern void Reset();

	void ISalesDocItemAddWorks.Reset()
	{
		//ILSpy generated this explicit interface implementation from .override directive in Reset
		this.Reset();
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743814)]
	public virtual extern bool Exists([In] short AddWorkCode);

	bool ISalesDocItemAddWorks.Exists([In] short AddWorkCode)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Exists
		return this.Exists(AddWorkCode);
	}
}
