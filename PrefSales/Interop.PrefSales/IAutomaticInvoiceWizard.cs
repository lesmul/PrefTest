using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[TypeLibType(TypeLibTypeFlags.FDual | TypeLibTypeFlags.FDispatchable)]
[Guid("77F9E153-5E44-4A60-9650-941D8033542E")]
public interface IAutomaticInvoiceWizard
{
	[DispId(2)]
	string ConnectionString
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(2)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(2)]
		[param: In]
		[param: MarshalAs(UnmanagedType.BStr)]
		set;
	}

	[DispId(3)]
	[ComAliasName("Interop.PrefSales.TranslationModeType")]
	TranslationModeType TranslationMode
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(3)]
		[return: ComAliasName("Interop.PrefSales.TranslationModeType")]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(3)]
		[param: In]
		[param: ComAliasName("Interop.PrefSales.TranslationModeType")]
		set;
	}

	[DispId(4)]
	[ComAliasName("Interop.PrefSales.EnumLanguagesId")]
	EnumLanguagesId LanguageId
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(4)]
		[return: ComAliasName("Interop.PrefSales.EnumLanguagesId")]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(4)]
		[param: In]
		[param: ComAliasName("Interop.PrefSales.EnumLanguagesId")]
		set;
	}

	[ComAliasName("Interop.PrefSales.EnumLanguagesId")]
	[DispId(5)]
	EnumLanguagesId TransLanguageId
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(5)]
		[return: ComAliasName("Interop.PrefSales.EnumLanguagesId")]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(5)]
		[param: In]
		[param: ComAliasName("Interop.PrefSales.EnumLanguagesId")]
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
	int UserCode
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(8)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(8)]
		[param: In]
		set;
	}

	[DispId(9)]
	bool AllowChangeNumber
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(9)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(9)]
		[param: In]
		set;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1)]
	[return: ComAliasName("Interop.PrefSales.ShowWizardReturnValue")]
	ShowWizardReturnValue ShowWizard();
}
