using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;

namespace Interop.PrefSales;

internal sealed class _IPrefSchedulerEvents_EventProvider : _IPrefSchedulerEvents_Event, IDisposable
{
	private WeakReference m_wkConnectionPointContainer;

	private ArrayList m_aEventSinkHelpers;

	private IConnectionPoint m_ConnectionPoint;

	private void Init()
	{
		IConnectionPoint ppCP = null;
		Guid riid = new Guid(new byte[16]
		{
			248, 195, 10, 133, 148, 182, 18, 69, 166, 36,
			198, 86, 217, 228, 224, 19
		});
		((IConnectionPointContainer)m_wkConnectionPointContainer.Target).FindConnectionPoint(ref riid, out ppCP);
		m_ConnectionPoint = ppCP;
		m_aEventSinkHelpers = new ArrayList();
	}

	public override void add_BeforeLoadScheduler(_IPrefSchedulerEvents_BeforeLoadSchedulerEventHandler P_0)
	{
		bool lockTaken = default(bool);
		try
		{
			Monitor.Enter(this, ref lockTaken);
			if (m_ConnectionPoint == null)
			{
				Init();
			}
			_IPrefSchedulerEvents_SinkHelper iPrefSchedulerEvents_SinkHelper = new _IPrefSchedulerEvents_SinkHelper();
			int pdwCookie = 0;
			m_ConnectionPoint.Advise(iPrefSchedulerEvents_SinkHelper, out pdwCookie);
			iPrefSchedulerEvents_SinkHelper.m_dwCookie = pdwCookie;
			iPrefSchedulerEvents_SinkHelper.m_BeforeLoadSchedulerDelegate = P_0;
			m_aEventSinkHelpers.Add(iPrefSchedulerEvents_SinkHelper);
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(this);
			}
		}
	}

	public override void remove_BeforeLoadScheduler(_IPrefSchedulerEvents_BeforeLoadSchedulerEventHandler P_0)
	{
		bool lockTaken = default(bool);
		try
		{
			Monitor.Enter(this, ref lockTaken);
			if (m_aEventSinkHelpers == null)
			{
				return;
			}
			int count = m_aEventSinkHelpers.Count;
			int num = 0;
			if (0 >= count)
			{
				return;
			}
			do
			{
				_IPrefSchedulerEvents_SinkHelper iPrefSchedulerEvents_SinkHelper = (_IPrefSchedulerEvents_SinkHelper)m_aEventSinkHelpers[num];
				if (iPrefSchedulerEvents_SinkHelper.m_BeforeLoadSchedulerDelegate != null && ((iPrefSchedulerEvents_SinkHelper.m_BeforeLoadSchedulerDelegate.Equals(P_0) ? 1u : 0u) & 0xFFu) != 0)
				{
					m_aEventSinkHelpers.RemoveAt(num);
					m_ConnectionPoint.Unadvise(iPrefSchedulerEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						Marshal.ReleaseComObject(m_ConnectionPoint);
						m_ConnectionPoint = null;
						m_aEventSinkHelpers = null;
					}
					break;
				}
				num++;
			}
			while (num < count);
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(this);
			}
		}
	}

	public override void add_BeforeLoadSelectionDetail(_IPrefSchedulerEvents_BeforeLoadSelectionDetailEventHandler P_0)
	{
		bool lockTaken = default(bool);
		try
		{
			Monitor.Enter(this, ref lockTaken);
			if (m_ConnectionPoint == null)
			{
				Init();
			}
			_IPrefSchedulerEvents_SinkHelper iPrefSchedulerEvents_SinkHelper = new _IPrefSchedulerEvents_SinkHelper();
			int pdwCookie = 0;
			m_ConnectionPoint.Advise(iPrefSchedulerEvents_SinkHelper, out pdwCookie);
			iPrefSchedulerEvents_SinkHelper.m_dwCookie = pdwCookie;
			iPrefSchedulerEvents_SinkHelper.m_BeforeLoadSelectionDetailDelegate = P_0;
			m_aEventSinkHelpers.Add(iPrefSchedulerEvents_SinkHelper);
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(this);
			}
		}
	}

	public override void remove_BeforeLoadSelectionDetail(_IPrefSchedulerEvents_BeforeLoadSelectionDetailEventHandler P_0)
	{
		bool lockTaken = default(bool);
		try
		{
			Monitor.Enter(this, ref lockTaken);
			if (m_aEventSinkHelpers == null)
			{
				return;
			}
			int count = m_aEventSinkHelpers.Count;
			int num = 0;
			if (0 >= count)
			{
				return;
			}
			do
			{
				_IPrefSchedulerEvents_SinkHelper iPrefSchedulerEvents_SinkHelper = (_IPrefSchedulerEvents_SinkHelper)m_aEventSinkHelpers[num];
				if (iPrefSchedulerEvents_SinkHelper.m_BeforeLoadSelectionDetailDelegate != null && ((iPrefSchedulerEvents_SinkHelper.m_BeforeLoadSelectionDetailDelegate.Equals(P_0) ? 1u : 0u) & 0xFFu) != 0)
				{
					m_aEventSinkHelpers.RemoveAt(num);
					m_ConnectionPoint.Unadvise(iPrefSchedulerEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						Marshal.ReleaseComObject(m_ConnectionPoint);
						m_ConnectionPoint = null;
						m_aEventSinkHelpers = null;
					}
					break;
				}
				num++;
			}
			while (num < count);
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(this);
			}
		}
	}

	public override void add_OrderDoubleClick(_IPrefSchedulerEvents_OrderDoubleClickEventHandler P_0)
	{
		bool lockTaken = default(bool);
		try
		{
			Monitor.Enter(this, ref lockTaken);
			if (m_ConnectionPoint == null)
			{
				Init();
			}
			_IPrefSchedulerEvents_SinkHelper iPrefSchedulerEvents_SinkHelper = new _IPrefSchedulerEvents_SinkHelper();
			int pdwCookie = 0;
			m_ConnectionPoint.Advise(iPrefSchedulerEvents_SinkHelper, out pdwCookie);
			iPrefSchedulerEvents_SinkHelper.m_dwCookie = pdwCookie;
			iPrefSchedulerEvents_SinkHelper.m_OrderDoubleClickDelegate = P_0;
			m_aEventSinkHelpers.Add(iPrefSchedulerEvents_SinkHelper);
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(this);
			}
		}
	}

	public override void remove_OrderDoubleClick(_IPrefSchedulerEvents_OrderDoubleClickEventHandler P_0)
	{
		bool lockTaken = default(bool);
		try
		{
			Monitor.Enter(this, ref lockTaken);
			if (m_aEventSinkHelpers == null)
			{
				return;
			}
			int count = m_aEventSinkHelpers.Count;
			int num = 0;
			if (0 >= count)
			{
				return;
			}
			do
			{
				_IPrefSchedulerEvents_SinkHelper iPrefSchedulerEvents_SinkHelper = (_IPrefSchedulerEvents_SinkHelper)m_aEventSinkHelpers[num];
				if (iPrefSchedulerEvents_SinkHelper.m_OrderDoubleClickDelegate != null && ((iPrefSchedulerEvents_SinkHelper.m_OrderDoubleClickDelegate.Equals(P_0) ? 1u : 0u) & 0xFFu) != 0)
				{
					m_aEventSinkHelpers.RemoveAt(num);
					m_ConnectionPoint.Unadvise(iPrefSchedulerEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						Marshal.ReleaseComObject(m_ConnectionPoint);
						m_ConnectionPoint = null;
						m_aEventSinkHelpers = null;
					}
					break;
				}
				num++;
			}
			while (num < count);
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(this);
			}
		}
	}

	public override void add_PODoubleClick(_IPrefSchedulerEvents_PODoubleClickEventHandler P_0)
	{
		bool lockTaken = default(bool);
		try
		{
			Monitor.Enter(this, ref lockTaken);
			if (m_ConnectionPoint == null)
			{
				Init();
			}
			_IPrefSchedulerEvents_SinkHelper iPrefSchedulerEvents_SinkHelper = new _IPrefSchedulerEvents_SinkHelper();
			int pdwCookie = 0;
			m_ConnectionPoint.Advise(iPrefSchedulerEvents_SinkHelper, out pdwCookie);
			iPrefSchedulerEvents_SinkHelper.m_dwCookie = pdwCookie;
			iPrefSchedulerEvents_SinkHelper.m_PODoubleClickDelegate = P_0;
			m_aEventSinkHelpers.Add(iPrefSchedulerEvents_SinkHelper);
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(this);
			}
		}
	}

	public override void remove_PODoubleClick(_IPrefSchedulerEvents_PODoubleClickEventHandler P_0)
	{
		bool lockTaken = default(bool);
		try
		{
			Monitor.Enter(this, ref lockTaken);
			if (m_aEventSinkHelpers == null)
			{
				return;
			}
			int count = m_aEventSinkHelpers.Count;
			int num = 0;
			if (0 >= count)
			{
				return;
			}
			do
			{
				_IPrefSchedulerEvents_SinkHelper iPrefSchedulerEvents_SinkHelper = (_IPrefSchedulerEvents_SinkHelper)m_aEventSinkHelpers[num];
				if (iPrefSchedulerEvents_SinkHelper.m_PODoubleClickDelegate != null && ((iPrefSchedulerEvents_SinkHelper.m_PODoubleClickDelegate.Equals(P_0) ? 1u : 0u) & 0xFFu) != 0)
				{
					m_aEventSinkHelpers.RemoveAt(num);
					m_ConnectionPoint.Unadvise(iPrefSchedulerEvents_SinkHelper.m_dwCookie);
					if (count <= 1)
					{
						Marshal.ReleaseComObject(m_ConnectionPoint);
						m_ConnectionPoint = null;
						m_aEventSinkHelpers = null;
					}
					break;
				}
				num++;
			}
			while (num < count);
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(this);
			}
		}
	}

	public _IPrefSchedulerEvents_EventProvider(object P_0)
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
					_IPrefSchedulerEvents_SinkHelper iPrefSchedulerEvents_SinkHelper = (_IPrefSchedulerEvents_SinkHelper)m_aEventSinkHelpers[num];
					m_ConnectionPoint.Unadvise(iPrefSchedulerEvents_SinkHelper.m_dwCookie);
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
