using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[Guid("5648E59D-5EDE-4D53-9BD0-3B1962CF1A29")]
[TypeLibType(TypeLibTypeFlags.FDual | TypeLibTypeFlags.FNonExtensible | TypeLibTypeFlags.FDispatchable)]
public interface IPrefScheduler
{
	[DispId(1610743808)]
	string ConnectionString
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743808)]
		[return: MarshalAs(UnmanagedType.BStr)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743808)]
		[param: In]
		[param: MarshalAs(UnmanagedType.BStr)]
		set;
	}

	[ComAliasName("Interop.PrefSales.TranslationModeType")]
	[DispId(1610743810)]
	TranslationModeType TranslationMode
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743810)]
		[param: In]
		[param: ComAliasName("Interop.PrefSales.TranslationModeType")]
		set;
	}

	[ComAliasName("Interop.PrefSales.EnumLanguagesId")]
	[DispId(1610743811)]
	EnumLanguagesId TransLanguageId
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743811)]
		[param: In]
		[param: ComAliasName("Interop.PrefSales.EnumLanguagesId")]
		set;
	}

	[ComAliasName("Interop.PrefSales.EnumLanguagesId")]
	[DispId(1610743812)]
	EnumLanguagesId LanguageId
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743812)]
		[param: In]
		[param: ComAliasName("Interop.PrefSales.EnumLanguagesId")]
		set;
	}

	[DispId(1610743813)]
	object PrefUserLink
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743813)]
		[return: MarshalAs(UnmanagedType.IUnknown)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743813)]
		[param: In]
		[param: MarshalAs(UnmanagedType.IUnknown)]
		set;
	}

	[DispId(1610743815)]
	int UserCode
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743815)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743815)]
		[param: In]
		set;
	}

	[DispId(1610743817)]
	[ComAliasName("Interop.PrefSales.AuditType")]
	AuditType AuditMode
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743817)]
		[return: ComAliasName("Interop.PrefSales.AuditType")]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743817)]
		[param: In]
		[param: ComAliasName("Interop.PrefSales.AuditType")]
		set;
	}

	[DispId(1610743819)]
	[ComAliasName("Interop.PrefSales.AuditSaveType")]
	AuditSaveType AuditSaveMode
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743819)]
		[return: ComAliasName("Interop.PrefSales.AuditSaveType")]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743819)]
		[param: In]
		[param: ComAliasName("Interop.PrefSales.AuditSaveType")]
		set;
	}

	[DispId(1610743821)]
	[ComAliasName("Interop.PrefSales.TaskNames")]
	TaskNames Application
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743821)]
		[return: ComAliasName("Interop.PrefSales.TaskNames")]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743821)]
		[param: In]
		[param: ComAliasName("Interop.PrefSales.TaskNames")]
		set;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743823)]
	void InitControl();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743824)]
	void RefreshControl();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743825)]
	void ShowSearchDialog();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743826)]
	void ExportToExcel();
}
