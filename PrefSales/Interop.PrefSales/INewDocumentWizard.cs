using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[Guid("729C4D51-1742-47D8-89E4-169C1E42ECB2")]
[TypeLibType(TypeLibTypeFlags.FDual | TypeLibTypeFlags.FDispatchable)]
public interface INewDocumentWizard
{
	[DispId(1610743809)]
	string ConnectionString
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743809)]
		[param: In]
		[param: MarshalAs(UnmanagedType.BStr)]
		set;
	}

	[DispId(1610743810)]
	bool ReadFromFile
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743810)]
		[param: In]
		set;
	}

	[DispId(1610743811)]
	int UserCode
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743811)]
		[param: In]
		set;
	}

	[DispId(1610743812)]
	int DefaultCustomerCode
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743812)]
		[param: In]
		set;
	}

	[ComAliasName("Interop.PrefSales.EnumLanguagesId")]
	[DispId(1610743813)]
	EnumLanguagesId LanguageId
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743813)]
		[param: In]
		[param: ComAliasName("Interop.PrefSales.EnumLanguagesId")]
		set;
	}

	[ComAliasName("Interop.PrefSales.EnumLanguagesId")]
	[DispId(1610743814)]
	EnumLanguagesId TransLanguageId
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743814)]
		[param: In]
		[param: ComAliasName("Interop.PrefSales.EnumLanguagesId")]
		set;
	}

	[ComAliasName("Interop.PrefSales.TranslationModeType")]
	[DispId(1610743815)]
	TranslationModeType TranslationMode
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743815)]
		[param: In]
		[param: ComAliasName("Interop.PrefSales.TranslationModeType")]
		set;
	}

	[DispId(1610743816)]
	int DefaultNumeration
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743816)]
		[param: In]
		set;
	}

	[DispId(1610743817)]
	SalesDoc FromSalesDoc
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743817)]
		[param: In]
		[param: MarshalAs(UnmanagedType.Interface)]
		set;
	}

	[DispId(1610743818)]
	bool CopyHeader
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743818)]
		get;
	}

	[DispId(1610743819)]
	string XMLDocument
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743819)]
		[param: In]
		[param: MarshalAs(UnmanagedType.BStr)]
		set;
	}

	[DispId(1610743820)]
	int NewDocumentNumber
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743820)]
		get;
	}

	[DispId(1610743821)]
	bool ResellerVersion
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
	bool AllowChangeNumber
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
	object UserRights
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743825)]
		[param: In]
		[param: MarshalAs(UnmanagedType.IUnknown)]
		set;
	}

	[DispId(1610743826)]
	int UnitsMode
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743826)]
		[param: In]
		set;
	}

	[DispId(1610743827)]
	bool UseRemoteFactory
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743827)]
		[param: In]
		set;
	}

	[DispId(1610743828)]
	object PlUnknown
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743828)]
		[TypeLibFunc(TypeLibFuncFlags.FHidden)]
		[param: In]
		[param: MarshalAs(UnmanagedType.IUnknown)]
		set;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743808)]
	[return: ComAliasName("Interop.PrefSales.ShowWizardReturnValue")]
	ShowWizardReturnValue ShowWizard();
}
