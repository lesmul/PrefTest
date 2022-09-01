using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ComImport]
[Guid("957BB74F-7BFB-46AC-9579-4DF6950C4DAE")]
[ComSourceInterfaces("Interop.PrefSales._IPrefSchedulerEvents")]
[TypeLibType(TypeLibTypeFlags.FCanCreate)]
[ClassInterface(ClassInterfaceType.None)]
public class PrefSchedulerClass : IPrefScheduler, PrefScheduler, _IPrefSchedulerEvents_Event
{
	[DispId(1610743808)]
	public virtual extern string ConnectionString
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

	[DispId(1610743810)]
	[ComAliasName("Interop.PrefSales.TranslationModeType")]
	public virtual extern TranslationModeType TranslationMode
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743810)]
		[param: In]
		[param: ComAliasName("Interop.PrefSales.TranslationModeType")]
		set;
	}

	[DispId(1610743811)]
	[ComAliasName("Interop.PrefSales.EnumLanguagesId")]
	public virtual extern EnumLanguagesId TransLanguageId
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743811)]
		[param: In]
		[param: ComAliasName("Interop.PrefSales.EnumLanguagesId")]
		set;
	}

	[ComAliasName("Interop.PrefSales.EnumLanguagesId")]
	[DispId(1610743812)]
	public virtual extern EnumLanguagesId LanguageId
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[DispId(1610743812)]
		[param: In]
		[param: ComAliasName("Interop.PrefSales.EnumLanguagesId")]
		set;
	}

	[DispId(1610743813)]
	public virtual extern object PrefUserLink
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
	public virtual extern int UserCode
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
	public virtual extern AuditType AuditMode
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

	[ComAliasName("Interop.PrefSales.AuditSaveType")]
	[DispId(1610743819)]
	public virtual extern AuditSaveType AuditSaveMode
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

	[ComAliasName("Interop.PrefSales.TaskNames")]
	[DispId(1610743821)]
	public virtual extern TaskNames Application
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

	public virtual extern event _IPrefSchedulerEvents_BeforeLoadSchedulerEventHandler BeforeLoadScheduler;

	public virtual extern event _IPrefSchedulerEvents_BeforeLoadSelectionDetailEventHandler BeforeLoadSelectionDetail;

	public virtual extern event _IPrefSchedulerEvents_OrderDoubleClickEventHandler OrderDoubleClick;

	public virtual extern event _IPrefSchedulerEvents_PODoubleClickEventHandler PODoubleClick;

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern PrefSchedulerClass();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743823)]
	public virtual extern void InitControl();

	void IPrefScheduler.InitControl()
	{
		//ILSpy generated this explicit interface implementation from .override directive in InitControl
		this.InitControl();
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743824)]
	public virtual extern void RefreshControl();

	void IPrefScheduler.RefreshControl()
	{
		//ILSpy generated this explicit interface implementation from .override directive in RefreshControl
		this.RefreshControl();
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743825)]
	public virtual extern void ShowSearchDialog();

	void IPrefScheduler.ShowSearchDialog()
	{
		//ILSpy generated this explicit interface implementation from .override directive in ShowSearchDialog
		this.ShowSearchDialog();
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[DispId(1610743826)]
	public virtual extern void ExportToExcel();

	void IPrefScheduler.ExportToExcel()
	{
		//ILSpy generated this explicit interface implementation from .override directive in ExportToExcel
		this.ExportToExcel();
	}
}
