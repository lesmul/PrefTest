using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;

namespace Interop.PrefSales;

[ComImport]
[TypeLibType(TypeLibTypeFlags.FDispatchable)]
[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
[Guid("6215C308-8AC4-49DD-8C6D-A90B4B3B47ED")]
public interface _IGeneratePurchaseOrdersFromMaterialNeedsEvents
{
}
[ComVisible(false)]
[TypeLibType(TypeLibTypeFlags.FHidden)]
[ComEventInterface(typeof(_IGeneratePurchaseOrdersFromMaterialNeedsEvents), typeof(_IGeneratePurchaseOrdersFromMaterialNeedsEvents_EventProvider))]
public interface _IGeneratePurchaseOrdersFromMaterialNeedsEvents_Event
{
}
[ClassInterface(ClassInterfaceType.None)]
[TypeLibType(TypeLibTypeFlags.FHidden)]
public sealed class _IGeneratePurchaseOrdersFromMaterialNeedsEvents_SinkHelper : _IGeneratePurchaseOrdersFromMaterialNeedsEvents
{
	public int m_dwCookie;

	internal _IGeneratePurchaseOrdersFromMaterialNeedsEvents_SinkHelper()
	{
		//Error decoding local variables: Signature type sequence must have at least one element.
		m_dwCookie = 0;
	}
}
internal sealed class _IGeneratePurchaseOrdersFromMaterialNeedsEvents_EventProvider : _IGeneratePurchaseOrdersFromMaterialNeedsEvents_Event, IDisposable
{
	private WeakReference m_wkConnectionPointContainer;

	private ArrayList m_aEventSinkHelpers;

	private IConnectionPoint m_ConnectionPoint;

	private void Init()
	{
		IConnectionPoint ppCP = null;
		Guid riid = new Guid(new byte[16]
		{
			8, 195, 21, 98, 196, 138, 221, 73, 140, 109,
			169, 11, 75, 59, 71, 237
		});
		((IConnectionPointContainer)m_wkConnectionPointContainer.Target).FindConnectionPoint(ref riid, out ppCP);
		m_ConnectionPoint = ppCP;
		m_aEventSinkHelpers = new ArrayList();
	}

	public _IGeneratePurchaseOrdersFromMaterialNeedsEvents_EventProvider(object P_0)
	{
		//Error decoding local variables: Signature type sequence must have at least one element.
		m_wkConnectionPointContainer = new WeakReference((IConnectionPointContainer)P_0, trackResurrection: false);
	}

	public override void Finalize()
	{
		bool lockTaken = default(bool);
		try
		{
			Monitor.Enter(this, ref lockTaken);
			if (m_ConnectionPoint == null)
			{
				return;
			}
			int count = m_aEventSinkHelpers.Count;
			int num = 0;
			if (0 < count)
			{
				do
				{
					_IGeneratePurchaseOrdersFromMaterialNeedsEvents_SinkHelper iGeneratePurchaseOrdersFromMaterialNeedsEvents_SinkHelper = (_IGeneratePurchaseOrdersFromMaterialNeedsEvents_SinkHelper)m_aEventSinkHelpers[num];
					m_ConnectionPoint.Unadvise(iGeneratePurchaseOrdersFromMaterialNeedsEvents_SinkHelper.m_dwCookie);
					num++;
				}
				while (num < count);
			}
			Marshal.ReleaseComObject(m_ConnectionPoint);
		}
		catch (Exception)
		{
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(this);
			}
		}
	}

	public override void Dispose()
	{
		//Error decoding local variables: Signature type sequence must have at least one element.
		Finalize();
		GC.SuppressFinalize(this);
	}
}
