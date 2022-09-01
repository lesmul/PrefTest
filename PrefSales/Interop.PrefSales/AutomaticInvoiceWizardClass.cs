using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[TypeLibType(TypeLibTypeFlags.FCanCreate)]
[Guid("7F9CA09F-2E1F-47BA-8E7D-F20330692E62")]
[ClassInterface(ClassInterfaceType.None)]
public class AutomaticInvoiceWizardClass : IAutomaticInvoiceWizard, AutomaticInvoiceWizard
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

	[ComAliasName("Interop.PrefSales.TranslationModeType")]
	[DispId(3)]
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
	public virtual extern int DestDocumentNumber
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(6)]
		get;
	}

	[DispId(7)]
	public virtual extern int DestDocumentVersion
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(7)]
		get;
	}

	[DispId(8)]
	public virtual extern int UserCode
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
	public virtual extern bool AllowChangeNumber
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
	public extern AutomaticInvoiceWizardClass();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1)]
	[return: ComAliasName("Interop.PrefSales.ShowWizardReturnValue")]
	public virtual extern ShowWizardReturnValue ShowWizard();

	ShowWizardReturnValue IAutomaticInvoiceWizard.ShowWizard()
	{
		//ILSpy generated this explicit interface implementation from .override directive in ShowWizard
		return this.ShowWizard();
	}
}
