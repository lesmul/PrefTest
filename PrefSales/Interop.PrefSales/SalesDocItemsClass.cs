using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[TypeLibType(TypeLibTypeFlags.FCanCreate)]
[ClassInterface(ClassInterfaceType.None)]
[Guid("40BB029E-EA4E-4E74-B073-2AC45E92B460")]
public class SalesDocItemsClass : ISalesDocItems, SalesDocItems, IEnumerable
{
	[DispId(1610743808)]
	public virtual extern int Count
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743808)]
		get;
	}

	[DispId(0)]
	public virtual extern SalesDocItem this[[In][MarshalAs(UnmanagedType.Struct)] object Index]
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(0)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	[DispId(8)]
	public virtual extern bool Changed
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(8)]
		get;
	}

	[DispId(9)]
	public virtual extern bool Blocked
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(9)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(9)]
		[param: In]
		set;
	}

	[DispId(10)]
	public virtual extern SalesDocItem ItemFromPosition
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(10)]
		[return: MarshalAs(UnmanagedType.Interface)]
		get;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern SalesDocItemsClass();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(-4)]
	[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalType = "System.Runtime.InteropServices.CustomMarshalers.EnumeratorToEnumVariantMarshaler, CustomMarshalers, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public virtual extern IEnumerator GetEnumerator();

	IEnumerator ISalesDocItems.GetEnumerator()
	{
		//ILSpy generated this explicit interface implementation from .override directive in GetEnumerator
		return this.GetEnumerator();
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1)]
	[return: MarshalAs(UnmanagedType.Interface)]
	public virtual extern SalesDocItem Add([In][MarshalAs(UnmanagedType.BStr)] string IdPos);

	SalesDocItem ISalesDocItems.Add([In][MarshalAs(UnmanagedType.BStr)] string IdPos)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Add
		return this.Add(IdPos);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(2)]
	[return: MarshalAs(UnmanagedType.Interface)]
	public virtual extern SalesDocItem Insert([In][MarshalAs(UnmanagedType.BStr)] string IdPos, [In] int SortOrder);

	SalesDocItem ISalesDocItems.Insert([In][MarshalAs(UnmanagedType.BStr)] string IdPos, [In] int SortOrder)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Insert
		return this.Insert(IdPos, SortOrder);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(3)]
	[return: MarshalAs(UnmanagedType.Interface)]
	public virtual extern SalesDocItem AddFromXML([In][MarshalAs(UnmanagedType.BStr)] string IdPos, [In][MarshalAs(UnmanagedType.BStr)] string XMLItem);

	SalesDocItem ISalesDocItems.AddFromXML([In][MarshalAs(UnmanagedType.BStr)] string IdPos, [In][MarshalAs(UnmanagedType.BStr)] string XMLItem)
	{
		//ILSpy generated this explicit interface implementation from .override directive in AddFromXML
		return this.AddFromXML(IdPos, XMLItem);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(4)]
	[return: MarshalAs(UnmanagedType.Interface)]
	public virtual extern SalesDocItem InsertFromXML([In][MarshalAs(UnmanagedType.BStr)] string IdPos, [In] int SortOrder, [In][MarshalAs(UnmanagedType.BStr)] string XMLItem);

	SalesDocItem ISalesDocItems.InsertFromXML([In][MarshalAs(UnmanagedType.BStr)] string IdPos, [In] int SortOrder, [In][MarshalAs(UnmanagedType.BStr)] string XMLItem)
	{
		//ILSpy generated this explicit interface implementation from .override directive in InsertFromXML
		return this.InsertFromXML(IdPos, SortOrder, XMLItem);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(5)]
	[return: MarshalAs(UnmanagedType.Interface)]
	public virtual extern SalesDocItem AddWebItem([In][MarshalAs(UnmanagedType.BStr)] string IdPos);

	SalesDocItem ISalesDocItems.AddWebItem([In][MarshalAs(UnmanagedType.BStr)] string IdPos)
	{
		//ILSpy generated this explicit interface implementation from .override directive in AddWebItem
		return this.AddWebItem(IdPos);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(6)]
	[return: MarshalAs(UnmanagedType.Interface)]
	public virtual extern SalesDocItem InsertWebItem([In][MarshalAs(UnmanagedType.BStr)] string IdPos, [In] int SortOrder);

	SalesDocItem ISalesDocItems.InsertWebItem([In][MarshalAs(UnmanagedType.BStr)] string IdPos, [In] int SortOrder)
	{
		//ILSpy generated this explicit interface implementation from .override directive in InsertWebItem
		return this.InsertWebItem(IdPos, SortOrder);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(7)]
	public virtual extern bool Exists([In][MarshalAs(UnmanagedType.Struct)] object Index);

	bool ISalesDocItems.Exists([In][MarshalAs(UnmanagedType.Struct)] object Index)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Exists
		return this.Exists(Index);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(11)]
	public virtual extern void Remove([In][MarshalAs(UnmanagedType.Struct)] object Index);

	void ISalesDocItems.Remove([In][MarshalAs(UnmanagedType.Struct)] object Index)
	{
		//ILSpy generated this explicit interface implementation from .override directive in Remove
		this.Remove(Index);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(12)]
	[return: MarshalAs(UnmanagedType.Interface)]
	public virtual extern SalesDocItem AddTypedItem([In][MarshalAs(UnmanagedType.BStr)] string IdPos, [In][ComAliasName("Interop.PrefSales.SalesDocItemType")] SalesDocItemType Type);

	SalesDocItem ISalesDocItems.AddTypedItem([In][MarshalAs(UnmanagedType.BStr)] string IdPos, [In][ComAliasName("Interop.PrefSales.SalesDocItemType")] SalesDocItemType Type)
	{
		//ILSpy generated this explicit interface implementation from .override directive in AddTypedItem
		return this.AddTypedItem(IdPos, Type);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(13)]
	[return: MarshalAs(UnmanagedType.Interface)]
	public virtual extern SalesDocItem InsertTypedItem([In][MarshalAs(UnmanagedType.BStr)] string IdPos, [In] int SortOrder, [In][ComAliasName("Interop.PrefSales.SalesDocItemType")] SalesDocItemType Type);

	SalesDocItem ISalesDocItems.InsertTypedItem([In][MarshalAs(UnmanagedType.BStr)] string IdPos, [In] int SortOrder, [In][ComAliasName("Interop.PrefSales.SalesDocItemType")] SalesDocItemType Type)
	{
		//ILSpy generated this explicit interface implementation from .override directive in InsertTypedItem
		return this.InsertTypedItem(IdPos, SortOrder, Type);
	}
}
