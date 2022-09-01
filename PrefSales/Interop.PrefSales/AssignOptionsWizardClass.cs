using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[Guid("AC3322EB-4116-47F8-B0EC-E438A8CAACE8")]
[TypeLibType(TypeLibTypeFlags.FCanCreate)]
[ClassInterface(ClassInterfaceType.None)]
public class AssignOptionsWizardClass : IAssignOptionsWizard, AssignOptionsWizard
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
	public virtual extern SalesDoc SalesDoc
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(6)]
		[param: In]
		[param: MarshalAs(UnmanagedType.Interface)]
		set;
	}

	[DispId(7)]
	public virtual extern string DataVersion
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(7)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(7)]
		[param: In]
		[param: MarshalAs(UnmanagedType.BStr)]
		set;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern AssignOptionsWizardClass();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1)]
	[return: ComAliasName("Interop.PrefSales.ShowWizardReturnValue")]
	public virtual extern ShowWizardReturnValue ShowWizard();

	ShowWizardReturnValue IAssignOptionsWizard.ShowWizard()
	{
		//ILSpy generated this explicit interface implementation from .override directive in ShowWizard
		return this.ShowWizard();
	}
}
