using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[ClassInterface(ClassInterfaceType.None)]
[Guid("CF1D89EA-B886-479F-8AA9-311921DD4C64")]
[TypeLibType(TypeLibTypeFlags.FCanCreate)]
public class VersionIndirectionWizardClass : IVersionIndirectionWizard, VersionIndirectionWizard
{
	[DispId(2)]
	public virtual extern string ConnectionString
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
	public virtual extern TranslationModeType TranslationMode
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
	public virtual extern EnumLanguagesId LanguageId
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

	[DispId(5)]
	[ComAliasName("Interop.PrefSales.EnumLanguagesId")]
	public virtual extern EnumLanguagesId TransLanguageId
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
	public virtual extern SalesDoc SalesDoc
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(6)]
		[param: In]
		[param: MarshalAs(UnmanagedType.Interface)]
		set;
	}

	[DispId(7)]
	public virtual extern int DestNumber
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(7)]
		get;
	}

	[DispId(8)]
	public virtual extern int DestVersion
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(8)]
		get;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern VersionIndirectionWizardClass();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1)]
	[return: ComAliasName("Interop.PrefSales.ShowWizardReturnValue")]
	public virtual extern ShowWizardReturnValue ShowWizard();

	ShowWizardReturnValue IVersionIndirectionWizard.ShowWizard()
	{
		//ILSpy generated this explicit interface implementation from .override directive in ShowWizard
		return this.ShowWizard();
	}
}
