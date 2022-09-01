using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[TypeLibType(TypeLibTypeFlags.FDual | TypeLibTypeFlags.FDispatchable)]
[Guid("F96C79B9-293B-44E0-8242-DAB05886FD12")]
public interface IPrefInvoiceObject
{
	[DispId(1)]
	int SrcDocumentNumber
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1)]
		[param: In]
		set;
	}

	[DispId(2)]
	int SrcDocumentVersion
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(2)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(2)]
		[param: In]
		set;
	}

	[DispId(3)]
	string ConnectionString
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(3)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(3)]
		[param: In]
		[param: MarshalAs(UnmanagedType.BStr)]
		set;
	}

	[ComAliasName("Interop.PrefSales.SalesDocType")]
	[DispId(5)]
	SalesDocType DestType
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(5)]
		[return: ComAliasName("Interop.PrefSales.SalesDocType")]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(5)]
		[param: In]
		[param: ComAliasName("Interop.PrefSales.SalesDocType")]
		set;
	}

	[DispId(6)]
	int DestDocumentNumber
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(6)]
		get;
	}

	[DispId(7)]
	int DestDocumentVersion
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(7)]
		get;
	}

	[DispId(8)]
	[ComAliasName("Interop.PrefSales.TranslationModeType")]
	TranslationModeType TranslationMode
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(8)]
		[return: ComAliasName("Interop.PrefSales.TranslationModeType")]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(8)]
		[param: In]
		[param: ComAliasName("Interop.PrefSales.TranslationModeType")]
		set;
	}

	[ComAliasName("Interop.PrefSales.EnumLanguagesId")]
	[DispId(9)]
	EnumLanguagesId LanguageId
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(9)]
		[return: ComAliasName("Interop.PrefSales.EnumLanguagesId")]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(9)]
		[param: In]
		[param: ComAliasName("Interop.PrefSales.EnumLanguagesId")]
		set;
	}

	[ComAliasName("Interop.PrefSales.EnumLanguagesId")]
	[DispId(10)]
	EnumLanguagesId TransLanguageId
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(10)]
		[return: ComAliasName("Interop.PrefSales.EnumLanguagesId")]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(10)]
		[param: In]
		[param: ComAliasName("Interop.PrefSales.EnumLanguagesId")]
		set;
	}

	[DispId(11)]
	int UserCode
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(11)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(11)]
		[param: In]
		set;
	}

	[DispId(12)]
	bool ResellerVersion
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(12)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(12)]
		[param: In]
		set;
	}

	[DispId(13)]
	bool AllowChangeNumber
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(13)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(13)]
		[param: In]
		set;
	}

	[DispId(14)]
	object PlUnknown
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[TypeLibFunc(TypeLibFuncFlags.FHidden)]
		[DispId(14)]
		[param: In]
		[param: MarshalAs(UnmanagedType.IUnknown)]
		set;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(4)]
	[return: ComAliasName("Interop.PrefSales.ShowWizardReturnValue")]
	ShowWizardReturnValue ShowWizard();
}
