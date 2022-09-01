using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[Guid("2C1F24C5-EE0E-44C6-8CA6-DE3A913097CB")]
[TypeLibType(TypeLibTypeFlags.FDual | TypeLibTypeFlags.FNonExtensible | TypeLibTypeFlags.FDispatchable)]
public interface ISalesDocItemAddWork
{
	[DispId(1610743808)]
	short Code
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743808)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743808)]
		[param: In]
		set;
	}

	[DispId(1610743810)]
	string Name
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
	string Description
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
	string Formula
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743814)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743814)]
		[param: In]
		[param: MarshalAs(UnmanagedType.BStr)]
		set;
	}

	[DispId(1610743816)]
	double Price
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
	short PriceDocGroup
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
	string Level1
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743820)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743820)]
		[param: In]
		[param: MarshalAs(UnmanagedType.BStr)]
		set;
	}

	[DispId(1610743822)]
	string Level2
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743822)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743822)]
		[param: In]
		[param: MarshalAs(UnmanagedType.BStr)]
		set;
	}

	[DispId(1610743824)]
	string Level3
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

	[DispId(1610743826)]
	string Level4
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743826)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743826)]
		[param: In]
		[param: MarshalAs(UnmanagedType.BStr)]
		set;
	}

	[DispId(1610743828)]
	string Level5
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743828)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743828)]
		[param: In]
		[param: MarshalAs(UnmanagedType.BStr)]
		set;
	}

	[DispId(1610743830)]
	bool Automatic
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743830)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743830)]
		[param: In]
		set;
	}

	[DispId(1610743832)]
	double Value
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743832)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743832)]
		[param: In]
		set;
	}

	[DispId(1610743834)]
	bool IsUserValue
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743834)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743834)]
		[param: In]
		set;
	}

	[DispId(1610743836)]
	string XmlSerialization
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743836)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743836)]
		[param: In]
		[param: MarshalAs(UnmanagedType.BStr)]
		set;
	}
}
