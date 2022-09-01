using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;

namespace Interop.PrefSales;

internal sealed class _IMaterialNeedsEvents_EventProvider : _IMaterialNeedsEvents_Event, IDisposable
{
	private WeakReference m_wkConnectionPointContainer;

	private ArrayList m_aEventSinkHelpers;

	private IConnectionPoint m_ConnectionPoint;

	private void Init()
	{
		IConnectionPoint ppCP = null;
		Guid riid = new Guid(new byte[16]
		{
			200, 25, 109, 122, 192, 234, 136, 78, 149, 237,
			180, 34, 158, 64, 243, 31
		});
		((IConnectionPointContainer)m_wkConnectionPointContainer.Target).FindConnectionPoint(ref riid, out ppCP);
		m_ConnectionPoint = ppCP;
		m_aEventSinkHelpers = new ArrayList();
	}

	public override void add_BeforeCalculateMaterialNeeds(_IMaterialNeedsEvents_BeforeCalculateMaterialNeedsEventHandler P_0)
	{
		bool lockTaken = default(bool);
		try
		{
			Monitor.Enter(this, ref lockTaken);
			if (m_ConnectionPoint == null)
			{
				Init();
			}
			_IMaterialNeedsEvents_SinkHelper iMaterialNeedsEvents_SinkHelper = new _IMaterialNeedsEvents_SinkHelper();
			int pdwCookie = 0;
			m_ConnectionPoint.Advise(iMaterialNeedsEvents_SinkHelper, out pdwCookie);
			iMaterialNeedsEvents_SinkHelper.m_dwCookie = pdwCookie;
			iMaterialNeedsEvents_SinkHelper.m_BeforeCalculateMaterialNeedsDelegate = P_0;
			m_aEventSinkHelpers.Add(iMaterialNeedsEvents_SinkHelper);
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(this);
			}
		}
	}

	public override void remove_BeforeCalculateMaterialNeeds(_IMaterialNeedsEvents_BeforeCalculateMaterialNeedsEventHandler P_0)
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
				_IMaterialNeedsEvents_SinkHelper iMaterialNeedsEvents_SinkHelper = (_IMaterialNeedsEvents_SinkHelper)m_aEventSinkHelpers[num];
				if (iMaterialNeedsEvents_SinkHelper.m_BeforeCalculateMaterialNeedsDelegate != null && ((iMaterialNeedsEvents_SinkHelper.m_BeforeCalculateMaterialNeedsDelegate.Equals(P_0) ? 1u : 0u) & 0xFFu) != 0)
				{
					m_aEventSinkHelpers.RemoveAt(num);
					m_ConnectionPoint.Unadvise(iMaterialNeedsEvents_SinkHelper.m_dwCookie);
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

	public override void add_AfterCalculateMaterialNeeds(_IMaterialNeedsEvents_AfterCalculateMaterialNeedsEventHandler P_0)
	{
		bool lockTaken = default(bool);
		try
		{
			Monitor.Enter(this, ref lockTaken);
			if (m_ConnectionPoint == null)
			{
				Init();
			}
			_IMaterialNeedsEvents_SinkHelper iMaterialNeedsEvents_SinkHelper = new _IMaterialNeedsEvents_SinkHelper();
			int pdwCookie = 0;
			m_ConnectionPoint.Advise(iMaterialNeedsEvents_SinkHelper, out pdwCookie);
			iMaterialNeedsEvents_SinkHelper.m_dwCookie = pdwCookie;
			iMaterialNeedsEvents_SinkHelper.m_AfterCalculateMaterialNeedsDelegate = P_0;
			m_aEventSinkHelpers.Add(iMaterialNeedsEvents_SinkHelper);
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(this);
			}
		}
	}

	public override void remove_AfterCalculateMaterialNeeds(_IMaterialNeedsEvents_AfterCalculateMaterialNeedsEventHandler P_0)
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
				_IMaterialNeedsEvents_SinkHelper iMaterialNeedsEvents_SinkHelper = (_IMaterialNeedsEvents_SinkHelper)m_aEventSinkHelpers[num];
				if (iMaterialNeedsEvents_SinkHelper.m_AfterCalculateMaterialNeedsDelegate != null && ((iMaterialNeedsEvents_SinkHelper.m_AfterCalculateMaterialNeedsDelegate.Equals(P_0) ? 1u : 0u) & 0xFFu) != 0)
				{
					m_aEventSinkHelpers.RemoveAt(num);
					m_ConnectionPoint.Unadvise(iMaterialNeedsEvents_SinkHelper.m_dwCookie);
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

	public override void add_BeforeRemoveMaterialNeeds(_IMaterialNeedsEvents_BeforeRemoveMaterialNeedsEventHandler P_0)
	{
		bool lockTaken = default(bool);
		try
		{
			Monitor.Enter(this, ref lockTaken);
			if (m_ConnectionPoint == null)
			{
				Init();
			}
			_IMaterialNeedsEvents_SinkHelper iMaterialNeedsEvents_SinkHelper = new _IMaterialNeedsEvents_SinkHelper();
			int pdwCookie = 0;
			m_ConnectionPoint.Advise(iMaterialNeedsEvents_SinkHelper, out pdwCookie);
			iMaterialNeedsEvents_SinkHelper.m_dwCookie = pdwCookie;
			iMaterialNeedsEvents_SinkHelper.m_BeforeRemoveMaterialNeedsDelegate = P_0;
			m_aEventSinkHelpers.Add(iMaterialNeedsEvents_SinkHelper);
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(this);
			}
		}
	}

	public override void remove_BeforeRemoveMaterialNeeds(_IMaterialNeedsEvents_BeforeRemoveMaterialNeedsEventHandler P_0)
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
				_IMaterialNeedsEvents_SinkHelper iMaterialNeedsEvents_SinkHelper = (_IMaterialNeedsEvents_SinkHelper)m_aEventSinkHelpers[num];
				if (iMaterialNeedsEvents_SinkHelper.m_BeforeRemoveMaterialNeedsDelegate != null && ((iMaterialNeedsEvents_SinkHelper.m_BeforeRemoveMaterialNeedsDelegate.Equals(P_0) ? 1u : 0u) & 0xFFu) != 0)
				{
					m_aEventSinkHelpers.RemoveAt(num);
					m_ConnectionPoint.Unadvise(iMaterialNeedsEvents_SinkHelper.m_dwCookie);
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

	public _IMaterialNeedsEvents_EventProvider(object P_0)
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
					_IMaterialNeedsEvents_SinkHelper iMaterialNeedsEvents_SinkHelper = (_IMaterialNeedsEvents_SinkHelper)m_aEventSinkHelpers[num];
					m_ConnectionPoint.Unadvise(iMaterialNeedsEvents_SinkHelper.m_dwCookie);
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
