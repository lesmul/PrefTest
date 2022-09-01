using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[Guid("6C38816B-A7ED-43AC-8C87-455251584BF5")]
[ClassInterface(ClassInterfaceType.None)]
[TypeLibType(TypeLibTypeFlags.FCanCreate)]
public class PrefInvoiceObjectClass : IPrefInvoiceObject, PrefInvoiceObject
{
	[DispId(1)]
	public virtual extern int SrcDocumentNumber
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
	public virtual extern int SrcDocumentVersion
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
	public virtual extern string ConnectionString
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
	public virtual extern SalesDocType DestType
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
	[ComAliasName("Interop.PrefSales.TranslationModeType")]
	public virtual extern TranslationModeType TranslationMode
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
	public virtual extern EnumLanguagesId LanguageId
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

	[DispId(10)]
	[ComAliasName("Interop.PrefSales.EnumLanguagesId")]
	public virtual extern EnumLanguagesId TransLanguageId
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
	public virtual extern int UserCode
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
	public virtual extern bool ResellerVersion
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
	public virtual extern bool AllowChangeNumber
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
	public virtual extern object PlUnknown
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(14)]
		[TypeLibFunc(TypeLibFuncFlags.FHidden)]
		[param: In]
		[param: MarshalAs(UnmanagedType.IUnknown)]
		set;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern PrefInvoiceObjectClass();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(4)]
	[return: ComAliasName("Interop.PrefSales.ShowWizardReturnValue")]
	public virtual extern ShowWizardReturnValue ShowWizard();

	ShowWizardReturnValue IPrefInvoiceObject.ShowWizard()
	{
		//ILSpy generated this explicit interface implementation from .override directive in ShowWizard
		return this.ShowWizard();
	}
}
