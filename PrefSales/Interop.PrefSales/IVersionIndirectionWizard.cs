using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[Guid("8C535848-02BD-4C0E-95F0-E6F0B9D5CB8F")]
[TypeLibType(TypeLibTypeFlags.FDual | TypeLibTypeFlags.FNonExtensible | TypeLibTypeFlags.FDispatchable)]
public interface IVersionIndirectionWizard
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

	[ComAliasName("Interop.PrefSales.TranslationModeType")]
	[DispId(3)]
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

	[ComAliasName("Interop.PrefSales.EnumLanguagesId")]
	[DispId(4)]
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
	SalesDoc SalesDoc
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(6)]
		[param: In]
		[param: MarshalAs(UnmanagedType.Interface)]
		set;
	}

	[DispId(7)]
	int DestNumber
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(7)]
		get;
	}

	[DispId(8)]
	int DestVersion
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(8)]
		get;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1)]
	[return: ComAliasName("Interop.PrefSales.ShowWizardReturnValue")]
	ShowWizardReturnValue ShowWizard();
}
