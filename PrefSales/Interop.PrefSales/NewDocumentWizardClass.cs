using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[Guid("EB8BD143-F56A-43F0-8441-523767F030BD")]
[TypeLibType(TypeLibTypeFlags.FCanCreate)]
[ClassInterface(ClassInterfaceType.None)]
public class NewDocumentWizardClass : INewDocumentWizard, NewDocumentWizard
{
	[DispId(1610743809)]
	public virtual extern string ConnectionString
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743809)]
		[param: In]
		[param: MarshalAs(UnmanagedType.BStr)]
		set;
	}

	[DispId(1610743810)]
	public virtual extern bool ReadFromFile
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743810)]
		[param: In]
		set;
	}

	[DispId(1610743811)]
	public virtual extern int UserCode
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743811)]
		[param: In]
		set;
	}

	[DispId(1610743812)]
	public virtual extern int DefaultCustomerCode
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743812)]
		[param: In]
		set;
	}

	[DispId(1610743813)]
	[ComAliasName("Interop.PrefSales.EnumLanguagesId")]
	public virtual extern EnumLanguagesId LanguageId
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743813)]
		[param: In]
		[param: ComAliasName("Interop.PrefSales.EnumLanguagesId")]
		set;
	}

	[ComAliasName("Interop.PrefSales.EnumLanguagesId")]
	[DispId(1610743814)]
	public virtual extern EnumLanguagesId TransLanguageId
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743814)]
		[param: In]
		[param: ComAliasName("Interop.PrefSales.EnumLanguagesId")]
		set;
	}

	[DispId(1610743815)]
	[ComAliasName("Interop.PrefSales.TranslationModeType")]
	public virtual extern TranslationModeType TranslationMode
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743815)]
		[param: In]
		[param: ComAliasName("Interop.PrefSales.TranslationModeType")]
		set;
	}

	[DispId(1610743816)]
	public virtual extern int DefaultNumeration
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743816)]
		[param: In]
		set;
	}

	[DispId(1610743817)]
	public virtual extern SalesDoc FromSalesDoc
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743817)]
		[param: In]
		[param: MarshalAs(UnmanagedType.Interface)]
		set;
	}

	[DispId(1610743818)]
	public virtual extern bool CopyHeader
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743818)]
		get;
	}

	[DispId(1610743819)]
	public virtual extern string XMLDocument
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743819)]
		[param: In]
		[param: MarshalAs(UnmanagedType.BStr)]
		set;
	}

	[DispId(1610743820)]
	public virtual extern int NewDocumentNumber
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743820)]
		get;
	}

	[DispId(1610743821)]
	public virtual extern bool ResellerVersion
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743821)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743821)]
		[param: In]
		set;
	}

	[DispId(1610743823)]
	public virtual extern bool AllowChangeNumber
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743823)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743823)]
		[param: In]
		set;
	}

	[DispId(1610743825)]
	public virtual extern object UserRights
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743825)]
		[param: In]
		[param: MarshalAs(UnmanagedType.IUnknown)]
		set;
	}

	[DispId(1610743826)]
	public virtual extern int UnitsMode
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743826)]
		[param: In]
		set;
	}

	[DispId(1610743827)]
	public virtual extern bool UseRemoteFactory
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743827)]
		[param: In]
		set;
	}

	[DispId(1610743828)]
	public virtual extern object PlUnknown
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743828)]
		[TypeLibFunc(TypeLibFuncFlags.FHidden)]
		[param: In]
		[param: MarshalAs(UnmanagedType.IUnknown)]
		set;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern NewDocumentWizardClass();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743808)]
	[return: ComAliasName("Interop.PrefSales.ShowWizardReturnValue")]
	public virtual extern ShowWizardReturnValue ShowWizard();

	ShowWizardReturnValue INewDocumentWizard.ShowWizard()
	{
		//ILSpy generated this explicit interface implementation from .override directive in ShowWizard
		return this.ShowWizard();
	}
}
