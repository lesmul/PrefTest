using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;

namespace Interop.PrefSales;

internal sealed class _ISalesDocEvents_EventProvider : _ISalesDocEvents_Event, IDisposable
{
	private WeakReference m_wkConnectionPointContainer;

	private ArrayList m_aEventSinkHelpers;

	private IConnectionPoint m_ConnectionPoint;

	private void Init()
	{
		IConnectionPoint ppCP = null;
		Guid riid = new Guid(new byte[16]
		{
			35, 138, 186, 128, 43, 87, 18, 76, 173, 75,
			10, 231, 211, 19, 208, 216
		});
		((IConnectionPointContainer)m_wkConnectionPointContainer.Target).FindConnectionPoint(ref riid, out ppCP);
		m_ConnectionPoint = ppCP;
		m_aEventSinkHelpers = new ArrayList();
	}

	public override void add_IsModel(_ISalesDocEvents_IsModelEventHandler P_0)
	{
		bool lockTaken = default(bool);
		try
		{
			Monitor.Enter(this, ref lockTaken);
			if (m_ConnectionPoint == null)
			{
				Init();
			}
			_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = new _ISalesDocEvents_SinkHelper();
			int pdwCookie = 0;
			m_ConnectionPoint.Advise(iSalesDocEvents_SinkHelper, out pdwCookie);
			iSalesDocEvents_SinkHelper.m_dwCookie = pdwCookie;
			iSalesDocEvents_SinkHelper.m_IsModelDelegate = P_0;
			m_aEventSinkHelpers.Add(iSalesDocEvents_SinkHelper);
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(this);
			}
		}
	}

	public override void remove_IsModel(_ISalesDocEvents_IsModelEventHandler P_0)
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
				_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = (_ISalesDocEvents_SinkHelper)m_aEventSinkHelpers[num];
				if (iSalesDocEvents_SinkHelper.m_IsModelDelegate != null && ((iSalesDocEvents_SinkHelper.m_IsModelDelegate.Equals(P_0) ? 1u : 0u) & 0xFFu) != 0)
				{
					m_aEventSinkHelpers.RemoveAt(num);
					m_ConnectionPoint.Unadvise(iSalesDocEvents_SinkHelper.m_dwCookie);
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

	public override void add_IsMaterial(_ISalesDocEvents_IsMaterialEventHandler P_0)
	{
		bool lockTaken = default(bool);
		try
		{
			Monitor.Enter(this, ref lockTaken);
			if (m_ConnectionPoint == null)
			{
				Init();
			}
			_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = new _ISalesDocEvents_SinkHelper();
			int pdwCookie = 0;
			m_ConnectionPoint.Advise(iSalesDocEvents_SinkHelper, out pdwCookie);
			iSalesDocEvents_SinkHelper.m_dwCookie = pdwCookie;
			iSalesDocEvents_SinkHelper.m_IsMaterialDelegate = P_0;
			m_aEventSinkHelpers.Add(iSalesDocEvents_SinkHelper);
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(this);
			}
		}
	}

	public override void remove_IsMaterial(_ISalesDocEvents_IsMaterialEventHandler P_0)
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
				_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = (_ISalesDocEvents_SinkHelper)m_aEventSinkHelpers[num];
				if (iSalesDocEvents_SinkHelper.m_IsMaterialDelegate != null && ((iSalesDocEvents_SinkHelper.m_IsMaterialDelegate.Equals(P_0) ? 1u : 0u) & 0xFFu) != 0)
				{
					m_aEventSinkHelpers.RemoveAt(num);
					m_ConnectionPoint.Unadvise(iSalesDocEvents_SinkHelper.m_dwCookie);
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

	public override void add_IsScript(_ISalesDocEvents_IsScriptEventHandler P_0)
	{
		bool lockTaken = default(bool);
		try
		{
			Monitor.Enter(this, ref lockTaken);
			if (m_ConnectionPoint == null)
			{
				Init();
			}
			_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = new _ISalesDocEvents_SinkHelper();
			int pdwCookie = 0;
			m_ConnectionPoint.Advise(iSalesDocEvents_SinkHelper, out pdwCookie);
			iSalesDocEvents_SinkHelper.m_dwCookie = pdwCookie;
			iSalesDocEvents_SinkHelper.m_IsScriptDelegate = P_0;
			m_aEventSinkHelpers.Add(iSalesDocEvents_SinkHelper);
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(this);
			}
		}
	}

	public override void remove_IsScript(_ISalesDocEvents_IsScriptEventHandler P_0)
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
				_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = (_ISalesDocEvents_SinkHelper)m_aEventSinkHelpers[num];
				if (iSalesDocEvents_SinkHelper.m_IsScriptDelegate != null && ((iSalesDocEvents_SinkHelper.m_IsScriptDelegate.Equals(P_0) ? 1u : 0u) & 0xFFu) != 0)
				{
					m_aEventSinkHelpers.RemoveAt(num);
					m_ConnectionPoint.Unadvise(iSalesDocEvents_SinkHelper.m_dwCookie);
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

	public override void add_BeforeSave(_ISalesDocEvents_BeforeSaveEventHandler P_0)
	{
		bool lockTaken = default(bool);
		try
		{
			Monitor.Enter(this, ref lockTaken);
			if (m_ConnectionPoint == null)
			{
				Init();
			}
			_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = new _ISalesDocEvents_SinkHelper();
			int pdwCookie = 0;
			m_ConnectionPoint.Advise(iSalesDocEvents_SinkHelper, out pdwCookie);
			iSalesDocEvents_SinkHelper.m_dwCookie = pdwCookie;
			iSalesDocEvents_SinkHelper.m_BeforeSaveDelegate = P_0;
			m_aEventSinkHelpers.Add(iSalesDocEvents_SinkHelper);
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(this);
			}
		}
	}

	public override void remove_BeforeSave(_ISalesDocEvents_BeforeSaveEventHandler P_0)
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
				_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = (_ISalesDocEvents_SinkHelper)m_aEventSinkHelpers[num];
				if (iSalesDocEvents_SinkHelper.m_BeforeSaveDelegate != null && ((iSalesDocEvents_SinkHelper.m_BeforeSaveDelegate.Equals(P_0) ? 1u : 0u) & 0xFFu) != 0)
				{
					m_aEventSinkHelpers.RemoveAt(num);
					m_ConnectionPoint.Unadvise(iSalesDocEvents_SinkHelper.m_dwCookie);
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

	public override void add_AfterSave(_ISalesDocEvents_AfterSaveEventHandler P_0)
	{
		bool lockTaken = default(bool);
		try
		{
			Monitor.Enter(this, ref lockTaken);
			if (m_ConnectionPoint == null)
			{
				Init();
			}
			_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = new _ISalesDocEvents_SinkHelper();
			int pdwCookie = 0;
			m_ConnectionPoint.Advise(iSalesDocEvents_SinkHelper, out pdwCookie);
			iSalesDocEvents_SinkHelper.m_dwCookie = pdwCookie;
			iSalesDocEvents_SinkHelper.m_AfterSaveDelegate = P_0;
			m_aEventSinkHelpers.Add(iSalesDocEvents_SinkHelper);
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(this);
			}
		}
	}

	public override void remove_AfterSave(_ISalesDocEvents_AfterSaveEventHandler P_0)
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
				_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = (_ISalesDocEvents_SinkHelper)m_aEventSinkHelpers[num];
				if (iSalesDocEvents_SinkHelper.m_AfterSaveDelegate != null && ((iSalesDocEvents_SinkHelper.m_AfterSaveDelegate.Equals(P_0) ? 1u : 0u) & 0xFFu) != 0)
				{
					m_aEventSinkHelpers.RemoveAt(num);
					m_ConnectionPoint.Unadvise(iSalesDocEvents_SinkHelper.m_dwCookie);
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

	public override void add_AfterLoad(_ISalesDocEvents_AfterLoadEventHandler P_0)
	{
		bool lockTaken = default(bool);
		try
		{
			Monitor.Enter(this, ref lockTaken);
			if (m_ConnectionPoint == null)
			{
				Init();
			}
			_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = new _ISalesDocEvents_SinkHelper();
			int pdwCookie = 0;
			m_ConnectionPoint.Advise(iSalesDocEvents_SinkHelper, out pdwCookie);
			iSalesDocEvents_SinkHelper.m_dwCookie = pdwCookie;
			iSalesDocEvents_SinkHelper.m_AfterLoadDelegate = P_0;
			m_aEventSinkHelpers.Add(iSalesDocEvents_SinkHelper);
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(this);
			}
		}
	}

	public override void remove_AfterLoad(_ISalesDocEvents_AfterLoadEventHandler P_0)
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
				_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = (_ISalesDocEvents_SinkHelper)m_aEventSinkHelpers[num];
				if (iSalesDocEvents_SinkHelper.m_AfterLoadDelegate != null && ((iSalesDocEvents_SinkHelper.m_AfterLoadDelegate.Equals(P_0) ? 1u : 0u) & 0xFFu) != 0)
				{
					m_aEventSinkHelpers.RemoveAt(num);
					m_ConnectionPoint.Unadvise(iSalesDocEvents_SinkHelper.m_dwCookie);
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

	public override void add_BeforeConfirmOrder(_ISalesDocEvents_BeforeConfirmOrderEventHandler P_0)
	{
		bool lockTaken = default(bool);
		try
		{
			Monitor.Enter(this, ref lockTaken);
			if (m_ConnectionPoint == null)
			{
				Init();
			}
			_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = new _ISalesDocEvents_SinkHelper();
			int pdwCookie = 0;
			m_ConnectionPoint.Advise(iSalesDocEvents_SinkHelper, out pdwCookie);
			iSalesDocEvents_SinkHelper.m_dwCookie = pdwCookie;
			iSalesDocEvents_SinkHelper.m_BeforeConfirmOrderDelegate = P_0;
			m_aEventSinkHelpers.Add(iSalesDocEvents_SinkHelper);
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(this);
			}
		}
	}

	public override void remove_BeforeConfirmOrder(_ISalesDocEvents_BeforeConfirmOrderEventHandler P_0)
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
				_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = (_ISalesDocEvents_SinkHelper)m_aEventSinkHelpers[num];
				if (iSalesDocEvents_SinkHelper.m_BeforeConfirmOrderDelegate != null && ((iSalesDocEvents_SinkHelper.m_BeforeConfirmOrderDelegate.Equals(P_0) ? 1u : 0u) & 0xFFu) != 0)
				{
					m_aEventSinkHelpers.RemoveAt(num);
					m_ConnectionPoint.Unadvise(iSalesDocEvents_SinkHelper.m_dwCookie);
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

	public override void add_AfterConfirmOrder(_ISalesDocEvents_AfterConfirmOrderEventHandler P_0)
	{
		bool lockTaken = default(bool);
		try
		{
			Monitor.Enter(this, ref lockTaken);
			if (m_ConnectionPoint == null)
			{
				Init();
			}
			_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = new _ISalesDocEvents_SinkHelper();
			int pdwCookie = 0;
			m_ConnectionPoint.Advise(iSalesDocEvents_SinkHelper, out pdwCookie);
			iSalesDocEvents_SinkHelper.m_dwCookie = pdwCookie;
			iSalesDocEvents_SinkHelper.m_AfterConfirmOrderDelegate = P_0;
			m_aEventSinkHelpers.Add(iSalesDocEvents_SinkHelper);
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(this);
			}
		}
	}

	public override void remove_AfterConfirmOrder(_ISalesDocEvents_AfterConfirmOrderEventHandler P_0)
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
				_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = (_ISalesDocEvents_SinkHelper)m_aEventSinkHelpers[num];
				if (iSalesDocEvents_SinkHelper.m_AfterConfirmOrderDelegate != null && ((iSalesDocEvents_SinkHelper.m_AfterConfirmOrderDelegate.Equals(P_0) ? 1u : 0u) & 0xFFu) != 0)
				{
					m_aEventSinkHelpers.RemoveAt(num);
					m_ConnectionPoint.Unadvise(iSalesDocEvents_SinkHelper.m_dwCookie);
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

	public override void add_BeforeRemoveDocument(_ISalesDocEvents_BeforeRemoveDocumentEventHandler P_0)
	{
		bool lockTaken = default(bool);
		try
		{
			Monitor.Enter(this, ref lockTaken);
			if (m_ConnectionPoint == null)
			{
				Init();
			}
			_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = new _ISalesDocEvents_SinkHelper();
			int pdwCookie = 0;
			m_ConnectionPoint.Advise(iSalesDocEvents_SinkHelper, out pdwCookie);
			iSalesDocEvents_SinkHelper.m_dwCookie = pdwCookie;
			iSalesDocEvents_SinkHelper.m_BeforeRemoveDocumentDelegate = P_0;
			m_aEventSinkHelpers.Add(iSalesDocEvents_SinkHelper);
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(this);
			}
		}
	}

	public override void remove_BeforeRemoveDocument(_ISalesDocEvents_BeforeRemoveDocumentEventHandler P_0)
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
				_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = (_ISalesDocEvents_SinkHelper)m_aEventSinkHelpers[num];
				if (iSalesDocEvents_SinkHelper.m_BeforeRemoveDocumentDelegate != null && ((iSalesDocEvents_SinkHelper.m_BeforeRemoveDocumentDelegate.Equals(P_0) ? 1u : 0u) & 0xFFu) != 0)
				{
					m_aEventSinkHelpers.RemoveAt(num);
					m_ConnectionPoint.Unadvise(iSalesDocEvents_SinkHelper.m_dwCookie);
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

	public override void add_AfterRemoveDocument(_ISalesDocEvents_AfterRemoveDocumentEventHandler P_0)
	{
		bool lockTaken = default(bool);
		try
		{
			Monitor.Enter(this, ref lockTaken);
			if (m_ConnectionPoint == null)
			{
				Init();
			}
			_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = new _ISalesDocEvents_SinkHelper();
			int pdwCookie = 0;
			m_ConnectionPoint.Advise(iSalesDocEvents_SinkHelper, out pdwCookie);
			iSalesDocEvents_SinkHelper.m_dwCookie = pdwCookie;
			iSalesDocEvents_SinkHelper.m_AfterRemoveDocumentDelegate = P_0;
			m_aEventSinkHelpers.Add(iSalesDocEvents_SinkHelper);
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(this);
			}
		}
	}

	public override void remove_AfterRemoveDocument(_ISalesDocEvents_AfterRemoveDocumentEventHandler P_0)
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
				_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = (_ISalesDocEvents_SinkHelper)m_aEventSinkHelpers[num];
				if (iSalesDocEvents_SinkHelper.m_AfterRemoveDocumentDelegate != null && ((iSalesDocEvents_SinkHelper.m_AfterRemoveDocumentDelegate.Equals(P_0) ? 1u : 0u) & 0xFFu) != 0)
				{
					m_aEventSinkHelpers.RemoveAt(num);
					m_ConnectionPoint.Unadvise(iSalesDocEvents_SinkHelper.m_dwCookie);
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

	public override void add_BeforeRemoveOffer(_ISalesDocEvents_BeforeRemoveOfferEventHandler P_0)
	{
		bool lockTaken = default(bool);
		try
		{
			Monitor.Enter(this, ref lockTaken);
			if (m_ConnectionPoint == null)
			{
				Init();
			}
			_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = new _ISalesDocEvents_SinkHelper();
			int pdwCookie = 0;
			m_ConnectionPoint.Advise(iSalesDocEvents_SinkHelper, out pdwCookie);
			iSalesDocEvents_SinkHelper.m_dwCookie = pdwCookie;
			iSalesDocEvents_SinkHelper.m_BeforeRemoveOfferDelegate = P_0;
			m_aEventSinkHelpers.Add(iSalesDocEvents_SinkHelper);
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(this);
			}
		}
	}

	public override void remove_BeforeRemoveOffer(_ISalesDocEvents_BeforeRemoveOfferEventHandler P_0)
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
				_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = (_ISalesDocEvents_SinkHelper)m_aEventSinkHelpers[num];
				if (iSalesDocEvents_SinkHelper.m_BeforeRemoveOfferDelegate != null && ((iSalesDocEvents_SinkHelper.m_BeforeRemoveOfferDelegate.Equals(P_0) ? 1u : 0u) & 0xFFu) != 0)
				{
					m_aEventSinkHelpers.RemoveAt(num);
					m_ConnectionPoint.Unadvise(iSalesDocEvents_SinkHelper.m_dwCookie);
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

	public override void add_BeforeRemoveOrder(_ISalesDocEvents_BeforeRemoveOrderEventHandler P_0)
	{
		bool lockTaken = default(bool);
		try
		{
			Monitor.Enter(this, ref lockTaken);
			if (m_ConnectionPoint == null)
			{
				Init();
			}
			_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = new _ISalesDocEvents_SinkHelper();
			int pdwCookie = 0;
			m_ConnectionPoint.Advise(iSalesDocEvents_SinkHelper, out pdwCookie);
			iSalesDocEvents_SinkHelper.m_dwCookie = pdwCookie;
			iSalesDocEvents_SinkHelper.m_BeforeRemoveOrderDelegate = P_0;
			m_aEventSinkHelpers.Add(iSalesDocEvents_SinkHelper);
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(this);
			}
		}
	}

	public override void remove_BeforeRemoveOrder(_ISalesDocEvents_BeforeRemoveOrderEventHandler P_0)
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
				_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = (_ISalesDocEvents_SinkHelper)m_aEventSinkHelpers[num];
				if (iSalesDocEvents_SinkHelper.m_BeforeRemoveOrderDelegate != null && ((iSalesDocEvents_SinkHelper.m_BeforeRemoveOrderDelegate.Equals(P_0) ? 1u : 0u) & 0xFFu) != 0)
				{
					m_aEventSinkHelpers.RemoveAt(num);
					m_ConnectionPoint.Unadvise(iSalesDocEvents_SinkHelper.m_dwCookie);
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

	public override void add_BeforeRemoveDeliveryNote(_ISalesDocEvents_BeforeRemoveDeliveryNoteEventHandler P_0)
	{
		bool lockTaken = default(bool);
		try
		{
			Monitor.Enter(this, ref lockTaken);
			if (m_ConnectionPoint == null)
			{
				Init();
			}
			_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = new _ISalesDocEvents_SinkHelper();
			int pdwCookie = 0;
			m_ConnectionPoint.Advise(iSalesDocEvents_SinkHelper, out pdwCookie);
			iSalesDocEvents_SinkHelper.m_dwCookie = pdwCookie;
			iSalesDocEvents_SinkHelper.m_BeforeRemoveDeliveryNoteDelegate = P_0;
			m_aEventSinkHelpers.Add(iSalesDocEvents_SinkHelper);
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(this);
			}
		}
	}

	public override void remove_BeforeRemoveDeliveryNote(_ISalesDocEvents_BeforeRemoveDeliveryNoteEventHandler P_0)
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
				_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = (_ISalesDocEvents_SinkHelper)m_aEventSinkHelpers[num];
				if (iSalesDocEvents_SinkHelper.m_BeforeRemoveDeliveryNoteDelegate != null && ((iSalesDocEvents_SinkHelper.m_BeforeRemoveDeliveryNoteDelegate.Equals(P_0) ? 1u : 0u) & 0xFFu) != 0)
				{
					m_aEventSinkHelpers.RemoveAt(num);
					m_ConnectionPoint.Unadvise(iSalesDocEvents_SinkHelper.m_dwCookie);
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

	public override void add_BeforeRemoveInvoice(_ISalesDocEvents_BeforeRemoveInvoiceEventHandler P_0)
	{
		bool lockTaken = default(bool);
		try
		{
			Monitor.Enter(this, ref lockTaken);
			if (m_ConnectionPoint == null)
			{
				Init();
			}
			_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = new _ISalesDocEvents_SinkHelper();
			int pdwCookie = 0;
			m_ConnectionPoint.Advise(iSalesDocEvents_SinkHelper, out pdwCookie);
			iSalesDocEvents_SinkHelper.m_dwCookie = pdwCookie;
			iSalesDocEvents_SinkHelper.m_BeforeRemoveInvoiceDelegate = P_0;
			m_aEventSinkHelpers.Add(iSalesDocEvents_SinkHelper);
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(this);
			}
		}
	}

	public override void remove_BeforeRemoveInvoice(_ISalesDocEvents_BeforeRemoveInvoiceEventHandler P_0)
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
				_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = (_ISalesDocEvents_SinkHelper)m_aEventSinkHelpers[num];
				if (iSalesDocEvents_SinkHelper.m_BeforeRemoveInvoiceDelegate != null && ((iSalesDocEvents_SinkHelper.m_BeforeRemoveInvoiceDelegate.Equals(P_0) ? 1u : 0u) & 0xFFu) != 0)
				{
					m_aEventSinkHelpers.RemoveAt(num);
					m_ConnectionPoint.Unadvise(iSalesDocEvents_SinkHelper.m_dwCookie);
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

	public override void add_BeforeRemoveSubOrder(_ISalesDocEvents_BeforeRemoveSubOrderEventHandler P_0)
	{
		bool lockTaken = default(bool);
		try
		{
			Monitor.Enter(this, ref lockTaken);
			if (m_ConnectionPoint == null)
			{
				Init();
			}
			_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = new _ISalesDocEvents_SinkHelper();
			int pdwCookie = 0;
			m_ConnectionPoint.Advise(iSalesDocEvents_SinkHelper, out pdwCookie);
			iSalesDocEvents_SinkHelper.m_dwCookie = pdwCookie;
			iSalesDocEvents_SinkHelper.m_BeforeRemoveSubOrderDelegate = P_0;
			m_aEventSinkHelpers.Add(iSalesDocEvents_SinkHelper);
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(this);
			}
		}
	}

	public override void remove_BeforeRemoveSubOrder(_ISalesDocEvents_BeforeRemoveSubOrderEventHandler P_0)
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
				_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = (_ISalesDocEvents_SinkHelper)m_aEventSinkHelpers[num];
				if (iSalesDocEvents_SinkHelper.m_BeforeRemoveSubOrderDelegate != null && ((iSalesDocEvents_SinkHelper.m_BeforeRemoveSubOrderDelegate.Equals(P_0) ? 1u : 0u) & 0xFFu) != 0)
				{
					m_aEventSinkHelpers.RemoveAt(num);
					m_ConnectionPoint.Unadvise(iSalesDocEvents_SinkHelper.m_dwCookie);
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

	public override void add_BeforeRemoveNotActiveOffers(_ISalesDocEvents_BeforeRemoveNotActiveOffersEventHandler P_0)
	{
		bool lockTaken = default(bool);
		try
		{
			Monitor.Enter(this, ref lockTaken);
			if (m_ConnectionPoint == null)
			{
				Init();
			}
			_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = new _ISalesDocEvents_SinkHelper();
			int pdwCookie = 0;
			m_ConnectionPoint.Advise(iSalesDocEvents_SinkHelper, out pdwCookie);
			iSalesDocEvents_SinkHelper.m_dwCookie = pdwCookie;
			iSalesDocEvents_SinkHelper.m_BeforeRemoveNotActiveOffersDelegate = P_0;
			m_aEventSinkHelpers.Add(iSalesDocEvents_SinkHelper);
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(this);
			}
		}
	}

	public override void remove_BeforeRemoveNotActiveOffers(_ISalesDocEvents_BeforeRemoveNotActiveOffersEventHandler P_0)
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
				_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = (_ISalesDocEvents_SinkHelper)m_aEventSinkHelpers[num];
				if (iSalesDocEvents_SinkHelper.m_BeforeRemoveNotActiveOffersDelegate != null && ((iSalesDocEvents_SinkHelper.m_BeforeRemoveNotActiveOffersDelegate.Equals(P_0) ? 1u : 0u) & 0xFFu) != 0)
				{
					m_aEventSinkHelpers.RemoveAt(num);
					m_ConnectionPoint.Unadvise(iSalesDocEvents_SinkHelper.m_dwCookie);
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

	public override void add_RecalculatePerVolume(_ISalesDocEvents_RecalculatePerVolumeEventHandler P_0)
	{
		bool lockTaken = default(bool);
		try
		{
			Monitor.Enter(this, ref lockTaken);
			if (m_ConnectionPoint == null)
			{
				Init();
			}
			_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = new _ISalesDocEvents_SinkHelper();
			int pdwCookie = 0;
			m_ConnectionPoint.Advise(iSalesDocEvents_SinkHelper, out pdwCookie);
			iSalesDocEvents_SinkHelper.m_dwCookie = pdwCookie;
			iSalesDocEvents_SinkHelper.m_RecalculatePerVolumeDelegate = P_0;
			m_aEventSinkHelpers.Add(iSalesDocEvents_SinkHelper);
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(this);
			}
		}
	}

	public override void remove_RecalculatePerVolume(_ISalesDocEvents_RecalculatePerVolumeEventHandler P_0)
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
				_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = (_ISalesDocEvents_SinkHelper)m_aEventSinkHelpers[num];
				if (iSalesDocEvents_SinkHelper.m_RecalculatePerVolumeDelegate != null && ((iSalesDocEvents_SinkHelper.m_RecalculatePerVolumeDelegate.Equals(P_0) ? 1u : 0u) & 0xFFu) != 0)
				{
					m_aEventSinkHelpers.RemoveAt(num);
					m_ConnectionPoint.Unadvise(iSalesDocEvents_SinkHelper.m_dwCookie);
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

	public override void add_AfterItemChanged(_ISalesDocEvents_AfterItemChangedEventHandler P_0)
	{
		bool lockTaken = default(bool);
		try
		{
			Monitor.Enter(this, ref lockTaken);
			if (m_ConnectionPoint == null)
			{
				Init();
			}
			_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = new _ISalesDocEvents_SinkHelper();
			int pdwCookie = 0;
			m_ConnectionPoint.Advise(iSalesDocEvents_SinkHelper, out pdwCookie);
			iSalesDocEvents_SinkHelper.m_dwCookie = pdwCookie;
			iSalesDocEvents_SinkHelper.m_AfterItemChangedDelegate = P_0;
			m_aEventSinkHelpers.Add(iSalesDocEvents_SinkHelper);
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(this);
			}
		}
	}

	public override void remove_AfterItemChanged(_ISalesDocEvents_AfterItemChangedEventHandler P_0)
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
				_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = (_ISalesDocEvents_SinkHelper)m_aEventSinkHelpers[num];
				if (iSalesDocEvents_SinkHelper.m_AfterItemChangedDelegate != null && ((iSalesDocEvents_SinkHelper.m_AfterItemChangedDelegate.Equals(P_0) ? 1u : 0u) & 0xFFu) != 0)
				{
					m_aEventSinkHelpers.RemoveAt(num);
					m_ConnectionPoint.Unadvise(iSalesDocEvents_SinkHelper.m_dwCookie);
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

	public override void add_AfterItemInserted(_ISalesDocEvents_AfterItemInsertedEventHandler P_0)
	{
		bool lockTaken = default(bool);
		try
		{
			Monitor.Enter(this, ref lockTaken);
			if (m_ConnectionPoint == null)
			{
				Init();
			}
			_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = new _ISalesDocEvents_SinkHelper();
			int pdwCookie = 0;
			m_ConnectionPoint.Advise(iSalesDocEvents_SinkHelper, out pdwCookie);
			iSalesDocEvents_SinkHelper.m_dwCookie = pdwCookie;
			iSalesDocEvents_SinkHelper.m_AfterItemInsertedDelegate = P_0;
			m_aEventSinkHelpers.Add(iSalesDocEvents_SinkHelper);
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(this);
			}
		}
	}

	public override void remove_AfterItemInserted(_ISalesDocEvents_AfterItemInsertedEventHandler P_0)
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
				_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = (_ISalesDocEvents_SinkHelper)m_aEventSinkHelpers[num];
				if (iSalesDocEvents_SinkHelper.m_AfterItemInsertedDelegate != null && ((iSalesDocEvents_SinkHelper.m_AfterItemInsertedDelegate.Equals(P_0) ? 1u : 0u) & 0xFFu) != 0)
				{
					m_aEventSinkHelpers.RemoveAt(num);
					m_ConnectionPoint.Unadvise(iSalesDocEvents_SinkHelper.m_dwCookie);
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

	public override void add_BeforeItemDeleted(_ISalesDocEvents_BeforeItemDeletedEventHandler P_0)
	{
		bool lockTaken = default(bool);
		try
		{
			Monitor.Enter(this, ref lockTaken);
			if (m_ConnectionPoint == null)
			{
				Init();
			}
			_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = new _ISalesDocEvents_SinkHelper();
			int pdwCookie = 0;
			m_ConnectionPoint.Advise(iSalesDocEvents_SinkHelper, out pdwCookie);
			iSalesDocEvents_SinkHelper.m_dwCookie = pdwCookie;
			iSalesDocEvents_SinkHelper.m_BeforeItemDeletedDelegate = P_0;
			m_aEventSinkHelpers.Add(iSalesDocEvents_SinkHelper);
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(this);
			}
		}
	}

	public override void remove_BeforeItemDeleted(_ISalesDocEvents_BeforeItemDeletedEventHandler P_0)
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
				_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = (_ISalesDocEvents_SinkHelper)m_aEventSinkHelpers[num];
				if (iSalesDocEvents_SinkHelper.m_BeforeItemDeletedDelegate != null && ((iSalesDocEvents_SinkHelper.m_BeforeItemDeletedDelegate.Equals(P_0) ? 1u : 0u) & 0xFFu) != 0)
				{
					m_aEventSinkHelpers.RemoveAt(num);
					m_ConnectionPoint.Unadvise(iSalesDocEvents_SinkHelper.m_dwCookie);
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

	public override void add_AfterItemDeleted(_ISalesDocEvents_AfterItemDeletedEventHandler P_0)
	{
		bool lockTaken = default(bool);
		try
		{
			Monitor.Enter(this, ref lockTaken);
			if (m_ConnectionPoint == null)
			{
				Init();
			}
			_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = new _ISalesDocEvents_SinkHelper();
			int pdwCookie = 0;
			m_ConnectionPoint.Advise(iSalesDocEvents_SinkHelper, out pdwCookie);
			iSalesDocEvents_SinkHelper.m_dwCookie = pdwCookie;
			iSalesDocEvents_SinkHelper.m_AfterItemDeletedDelegate = P_0;
			m_aEventSinkHelpers.Add(iSalesDocEvents_SinkHelper);
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(this);
			}
		}
	}

	public override void remove_AfterItemDeleted(_ISalesDocEvents_AfterItemDeletedEventHandler P_0)
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
				_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = (_ISalesDocEvents_SinkHelper)m_aEventSinkHelpers[num];
				if (iSalesDocEvents_SinkHelper.m_AfterItemDeletedDelegate != null && ((iSalesDocEvents_SinkHelper.m_AfterItemDeletedDelegate.Equals(P_0) ? 1u : 0u) & 0xFFu) != 0)
				{
					m_aEventSinkHelpers.RemoveAt(num);
					m_ConnectionPoint.Unadvise(iSalesDocEvents_SinkHelper.m_dwCookie);
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

	public override void add_BeforeSelectCustomer(_ISalesDocEvents_BeforeSelectCustomerEventHandler P_0)
	{
		bool lockTaken = default(bool);
		try
		{
			Monitor.Enter(this, ref lockTaken);
			if (m_ConnectionPoint == null)
			{
				Init();
			}
			_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = new _ISalesDocEvents_SinkHelper();
			int pdwCookie = 0;
			m_ConnectionPoint.Advise(iSalesDocEvents_SinkHelper, out pdwCookie);
			iSalesDocEvents_SinkHelper.m_dwCookie = pdwCookie;
			iSalesDocEvents_SinkHelper.m_BeforeSelectCustomerDelegate = P_0;
			m_aEventSinkHelpers.Add(iSalesDocEvents_SinkHelper);
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(this);
			}
		}
	}

	public override void remove_BeforeSelectCustomer(_ISalesDocEvents_BeforeSelectCustomerEventHandler P_0)
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
				_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = (_ISalesDocEvents_SinkHelper)m_aEventSinkHelpers[num];
				if (iSalesDocEvents_SinkHelper.m_BeforeSelectCustomerDelegate != null && ((iSalesDocEvents_SinkHelper.m_BeforeSelectCustomerDelegate.Equals(P_0) ? 1u : 0u) & 0xFFu) != 0)
				{
					m_aEventSinkHelpers.RemoveAt(num);
					m_ConnectionPoint.Unadvise(iSalesDocEvents_SinkHelper.m_dwCookie);
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

	public override void add_AfterCalculateCommission(_ISalesDocEvents_AfterCalculateCommissionEventHandler P_0)
	{
		bool lockTaken = default(bool);
		try
		{
			Monitor.Enter(this, ref lockTaken);
			if (m_ConnectionPoint == null)
			{
				Init();
			}
			_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = new _ISalesDocEvents_SinkHelper();
			int pdwCookie = 0;
			m_ConnectionPoint.Advise(iSalesDocEvents_SinkHelper, out pdwCookie);
			iSalesDocEvents_SinkHelper.m_dwCookie = pdwCookie;
			iSalesDocEvents_SinkHelper.m_AfterCalculateCommissionDelegate = P_0;
			m_aEventSinkHelpers.Add(iSalesDocEvents_SinkHelper);
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(this);
			}
		}
	}

	public override void remove_AfterCalculateCommission(_ISalesDocEvents_AfterCalculateCommissionEventHandler P_0)
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
				_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = (_ISalesDocEvents_SinkHelper)m_aEventSinkHelpers[num];
				if (iSalesDocEvents_SinkHelper.m_AfterCalculateCommissionDelegate != null && ((iSalesDocEvents_SinkHelper.m_AfterCalculateCommissionDelegate.Equals(P_0) ? 1u : 0u) & 0xFFu) != 0)
				{
					m_aEventSinkHelpers.RemoveAt(num);
					m_ConnectionPoint.Unadvise(iSalesDocEvents_SinkHelper.m_dwCookie);
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

	public override void add_AfterFieldChanged(_ISalesDocEvents_AfterFieldChangedEventHandler P_0)
	{
		bool lockTaken = default(bool);
		try
		{
			Monitor.Enter(this, ref lockTaken);
			if (m_ConnectionPoint == null)
			{
				Init();
			}
			_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = new _ISalesDocEvents_SinkHelper();
			int pdwCookie = 0;
			m_ConnectionPoint.Advise(iSalesDocEvents_SinkHelper, out pdwCookie);
			iSalesDocEvents_SinkHelper.m_dwCookie = pdwCookie;
			iSalesDocEvents_SinkHelper.m_AfterFieldChangedDelegate = P_0;
			m_aEventSinkHelpers.Add(iSalesDocEvents_SinkHelper);
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(this);
			}
		}
	}

	public override void remove_AfterFieldChanged(_ISalesDocEvents_AfterFieldChangedEventHandler P_0)
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
				_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = (_ISalesDocEvents_SinkHelper)m_aEventSinkHelpers[num];
				if (iSalesDocEvents_SinkHelper.m_AfterFieldChangedDelegate != null && ((iSalesDocEvents_SinkHelper.m_AfterFieldChangedDelegate.Equals(P_0) ? 1u : 0u) & 0xFFu) != 0)
				{
					m_aEventSinkHelpers.RemoveAt(num);
					m_ConnectionPoint.Unadvise(iSalesDocEvents_SinkHelper.m_dwCookie);
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

	public override void add_FormatAlias(_ISalesDocEvents_FormatAliasEventHandler P_0)
	{
		bool lockTaken = default(bool);
		try
		{
			Monitor.Enter(this, ref lockTaken);
			if (m_ConnectionPoint == null)
			{
				Init();
			}
			_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = new _ISalesDocEvents_SinkHelper();
			int pdwCookie = 0;
			m_ConnectionPoint.Advise(iSalesDocEvents_SinkHelper, out pdwCookie);
			iSalesDocEvents_SinkHelper.m_dwCookie = pdwCookie;
			iSalesDocEvents_SinkHelper.m_FormatAliasDelegate = P_0;
			m_aEventSinkHelpers.Add(iSalesDocEvents_SinkHelper);
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(this);
			}
		}
	}

	public override void remove_FormatAlias(_ISalesDocEvents_FormatAliasEventHandler P_0)
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
				_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = (_ISalesDocEvents_SinkHelper)m_aEventSinkHelpers[num];
				if (iSalesDocEvents_SinkHelper.m_FormatAliasDelegate != null && ((iSalesDocEvents_SinkHelper.m_FormatAliasDelegate.Equals(P_0) ? 1u : 0u) & 0xFFu) != 0)
				{
					m_aEventSinkHelpers.RemoveAt(num);
					m_ConnectionPoint.Unadvise(iSalesDocEvents_SinkHelper.m_dwCookie);
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

	public override void add_ModifySalesmanSurcharge(_ISalesDocEvents_ModifySalesmanSurchargeEventHandler P_0)
	{
		bool lockTaken = default(bool);
		try
		{
			Monitor.Enter(this, ref lockTaken);
			if (m_ConnectionPoint == null)
			{
				Init();
			}
			_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = new _ISalesDocEvents_SinkHelper();
			int pdwCookie = 0;
			m_ConnectionPoint.Advise(iSalesDocEvents_SinkHelper, out pdwCookie);
			iSalesDocEvents_SinkHelper.m_dwCookie = pdwCookie;
			iSalesDocEvents_SinkHelper.m_ModifySalesmanSurchargeDelegate = P_0;
			m_aEventSinkHelpers.Add(iSalesDocEvents_SinkHelper);
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(this);
			}
		}
	}

	public override void remove_ModifySalesmanSurcharge(_ISalesDocEvents_ModifySalesmanSurchargeEventHandler P_0)
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
				_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = (_ISalesDocEvents_SinkHelper)m_aEventSinkHelpers[num];
				if (iSalesDocEvents_SinkHelper.m_ModifySalesmanSurchargeDelegate != null && ((iSalesDocEvents_SinkHelper.m_ModifySalesmanSurchargeDelegate.Equals(P_0) ? 1u : 0u) & 0xFFu) != 0)
				{
					m_aEventSinkHelpers.RemoveAt(num);
					m_ConnectionPoint.Unadvise(iSalesDocEvents_SinkHelper.m_dwCookie);
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

	public override void add_BlockDocument(_ISalesDocEvents_BlockDocumentEventHandler P_0)
	{
		bool lockTaken = default(bool);
		try
		{
			Monitor.Enter(this, ref lockTaken);
			if (m_ConnectionPoint == null)
			{
				Init();
			}
			_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = new _ISalesDocEvents_SinkHelper();
			int pdwCookie = 0;
			m_ConnectionPoint.Advise(iSalesDocEvents_SinkHelper, out pdwCookie);
			iSalesDocEvents_SinkHelper.m_dwCookie = pdwCookie;
			iSalesDocEvents_SinkHelper.m_BlockDocumentDelegate = P_0;
			m_aEventSinkHelpers.Add(iSalesDocEvents_SinkHelper);
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(this);
			}
		}
	}

	public override void remove_BlockDocument(_ISalesDocEvents_BlockDocumentEventHandler P_0)
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
				_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = (_ISalesDocEvents_SinkHelper)m_aEventSinkHelpers[num];
				if (iSalesDocEvents_SinkHelper.m_BlockDocumentDelegate != null && ((iSalesDocEvents_SinkHelper.m_BlockDocumentDelegate.Equals(P_0) ? 1u : 0u) & 0xFFu) != 0)
				{
					m_aEventSinkHelpers.RemoveAt(num);
					m_ConnectionPoint.Unadvise(iSalesDocEvents_SinkHelper.m_dwCookie);
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

	public override void add_BeforeCancelDocument(_ISalesDocEvents_BeforeCancelDocumentEventHandler P_0)
	{
		bool lockTaken = default(bool);
		try
		{
			Monitor.Enter(this, ref lockTaken);
			if (m_ConnectionPoint == null)
			{
				Init();
			}
			_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = new _ISalesDocEvents_SinkHelper();
			int pdwCookie = 0;
			m_ConnectionPoint.Advise(iSalesDocEvents_SinkHelper, out pdwCookie);
			iSalesDocEvents_SinkHelper.m_dwCookie = pdwCookie;
			iSalesDocEvents_SinkHelper.m_BeforeCancelDocumentDelegate = P_0;
			m_aEventSinkHelpers.Add(iSalesDocEvents_SinkHelper);
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(this);
			}
		}
	}

	public override void remove_BeforeCancelDocument(_ISalesDocEvents_BeforeCancelDocumentEventHandler P_0)
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
				_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = (_ISalesDocEvents_SinkHelper)m_aEventSinkHelpers[num];
				if (iSalesDocEvents_SinkHelper.m_BeforeCancelDocumentDelegate != null && ((iSalesDocEvents_SinkHelper.m_BeforeCancelDocumentDelegate.Equals(P_0) ? 1u : 0u) & 0xFFu) != 0)
				{
					m_aEventSinkHelpers.RemoveAt(num);
					m_ConnectionPoint.Unadvise(iSalesDocEvents_SinkHelper.m_dwCookie);
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

	public override void add_AfterCancelDocument(_ISalesDocEvents_AfterCancelDocumentEventHandler P_0)
	{
		bool lockTaken = default(bool);
		try
		{
			Monitor.Enter(this, ref lockTaken);
			if (m_ConnectionPoint == null)
			{
				Init();
			}
			_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = new _ISalesDocEvents_SinkHelper();
			int pdwCookie = 0;
			m_ConnectionPoint.Advise(iSalesDocEvents_SinkHelper, out pdwCookie);
			iSalesDocEvents_SinkHelper.m_dwCookie = pdwCookie;
			iSalesDocEvents_SinkHelper.m_AfterCancelDocumentDelegate = P_0;
			m_aEventSinkHelpers.Add(iSalesDocEvents_SinkHelper);
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(this);
			}
		}
	}

	public override void remove_AfterCancelDocument(_ISalesDocEvents_AfterCancelDocumentEventHandler P_0)
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
				_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = (_ISalesDocEvents_SinkHelper)m_aEventSinkHelpers[num];
				if (iSalesDocEvents_SinkHelper.m_AfterCancelDocumentDelegate != null && ((iSalesDocEvents_SinkHelper.m_AfterCancelDocumentDelegate.Equals(P_0) ? 1u : 0u) & 0xFFu) != 0)
				{
					m_aEventSinkHelpers.RemoveAt(num);
					m_ConnectionPoint.Unadvise(iSalesDocEvents_SinkHelper.m_dwCookie);
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

	public override void add_Validate(_ISalesDocEvents_ValidateEventHandler P_0)
	{
		bool lockTaken = default(bool);
		try
		{
			Monitor.Enter(this, ref lockTaken);
			if (m_ConnectionPoint == null)
			{
				Init();
			}
			_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = new _ISalesDocEvents_SinkHelper();
			int pdwCookie = 0;
			m_ConnectionPoint.Advise(iSalesDocEvents_SinkHelper, out pdwCookie);
			iSalesDocEvents_SinkHelper.m_dwCookie = pdwCookie;
			iSalesDocEvents_SinkHelper.m_ValidateDelegate = P_0;
			m_aEventSinkHelpers.Add(iSalesDocEvents_SinkHelper);
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(this);
			}
		}
	}

	public override void remove_Validate(_ISalesDocEvents_ValidateEventHandler P_0)
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
				_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = (_ISalesDocEvents_SinkHelper)m_aEventSinkHelpers[num];
				if (iSalesDocEvents_SinkHelper.m_ValidateDelegate != null && ((iSalesDocEvents_SinkHelper.m_ValidateDelegate.Equals(P_0) ? 1u : 0u) & 0xFFu) != 0)
				{
					m_aEventSinkHelpers.RemoveAt(num);
					m_ConnectionPoint.Unadvise(iSalesDocEvents_SinkHelper.m_dwCookie);
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

	public override void add_BeforeRecalculateItem(_ISalesDocEvents_BeforeRecalculateItemEventHandler P_0)
	{
		bool lockTaken = default(bool);
		try
		{
			Monitor.Enter(this, ref lockTaken);
			if (m_ConnectionPoint == null)
			{
				Init();
			}
			_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = new _ISalesDocEvents_SinkHelper();
			int pdwCookie = 0;
			m_ConnectionPoint.Advise(iSalesDocEvents_SinkHelper, out pdwCookie);
			iSalesDocEvents_SinkHelper.m_dwCookie = pdwCookie;
			iSalesDocEvents_SinkHelper.m_BeforeRecalculateItemDelegate = P_0;
			m_aEventSinkHelpers.Add(iSalesDocEvents_SinkHelper);
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(this);
			}
		}
	}

	public override void remove_BeforeRecalculateItem(_ISalesDocEvents_BeforeRecalculateItemEventHandler P_0)
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
				_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = (_ISalesDocEvents_SinkHelper)m_aEventSinkHelpers[num];
				if (iSalesDocEvents_SinkHelper.m_BeforeRecalculateItemDelegate != null && ((iSalesDocEvents_SinkHelper.m_BeforeRecalculateItemDelegate.Equals(P_0) ? 1u : 0u) & 0xFFu) != 0)
				{
					m_aEventSinkHelpers.RemoveAt(num);
					m_ConnectionPoint.Unadvise(iSalesDocEvents_SinkHelper.m_dwCookie);
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

	public override void add_AfterRecalculateItem(_ISalesDocEvents_AfterRecalculateItemEventHandler P_0)
	{
		bool lockTaken = default(bool);
		try
		{
			Monitor.Enter(this, ref lockTaken);
			if (m_ConnectionPoint == null)
			{
				Init();
			}
			_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = new _ISalesDocEvents_SinkHelper();
			int pdwCookie = 0;
			m_ConnectionPoint.Advise(iSalesDocEvents_SinkHelper, out pdwCookie);
			iSalesDocEvents_SinkHelper.m_dwCookie = pdwCookie;
			iSalesDocEvents_SinkHelper.m_AfterRecalculateItemDelegate = P_0;
			m_aEventSinkHelpers.Add(iSalesDocEvents_SinkHelper);
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(this);
			}
		}
	}

	public override void remove_AfterRecalculateItem(_ISalesDocEvents_AfterRecalculateItemEventHandler P_0)
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
				_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = (_ISalesDocEvents_SinkHelper)m_aEventSinkHelpers[num];
				if (iSalesDocEvents_SinkHelper.m_AfterRecalculateItemDelegate != null && ((iSalesDocEvents_SinkHelper.m_AfterRecalculateItemDelegate.Equals(P_0) ? 1u : 0u) & 0xFFu) != 0)
				{
					m_aEventSinkHelpers.RemoveAt(num);
					m_ConnectionPoint.Unadvise(iSalesDocEvents_SinkHelper.m_dwCookie);
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

	public override void add_AfterSetItemCode(_ISalesDocEvents_AfterSetItemCodeEventHandler P_0)
	{
		bool lockTaken = default(bool);
		try
		{
			Monitor.Enter(this, ref lockTaken);
			if (m_ConnectionPoint == null)
			{
				Init();
			}
			_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = new _ISalesDocEvents_SinkHelper();
			int pdwCookie = 0;
			m_ConnectionPoint.Advise(iSalesDocEvents_SinkHelper, out pdwCookie);
			iSalesDocEvents_SinkHelper.m_dwCookie = pdwCookie;
			iSalesDocEvents_SinkHelper.m_AfterSetItemCodeDelegate = P_0;
			m_aEventSinkHelpers.Add(iSalesDocEvents_SinkHelper);
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(this);
			}
		}
	}

	public override void remove_AfterSetItemCode(_ISalesDocEvents_AfterSetItemCodeEventHandler P_0)
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
				_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = (_ISalesDocEvents_SinkHelper)m_aEventSinkHelpers[num];
				if (iSalesDocEvents_SinkHelper.m_AfterSetItemCodeDelegate != null && ((iSalesDocEvents_SinkHelper.m_AfterSetItemCodeDelegate.Equals(P_0) ? 1u : 0u) & 0xFFu) != 0)
				{
					m_aEventSinkHelpers.RemoveAt(num);
					m_ConnectionPoint.Unadvise(iSalesDocEvents_SinkHelper.m_dwCookie);
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

	public override void add_BeforeSetItemContext(_ISalesDocEvents_BeforeSetItemContextEventHandler P_0)
	{
		bool lockTaken = default(bool);
		try
		{
			Monitor.Enter(this, ref lockTaken);
			if (m_ConnectionPoint == null)
			{
				Init();
			}
			_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = new _ISalesDocEvents_SinkHelper();
			int pdwCookie = 0;
			m_ConnectionPoint.Advise(iSalesDocEvents_SinkHelper, out pdwCookie);
			iSalesDocEvents_SinkHelper.m_dwCookie = pdwCookie;
			iSalesDocEvents_SinkHelper.m_BeforeSetItemContextDelegate = P_0;
			m_aEventSinkHelpers.Add(iSalesDocEvents_SinkHelper);
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(this);
			}
		}
	}

	public override void remove_BeforeSetItemContext(_ISalesDocEvents_BeforeSetItemContextEventHandler P_0)
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
				_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = (_ISalesDocEvents_SinkHelper)m_aEventSinkHelpers[num];
				if (iSalesDocEvents_SinkHelper.m_BeforeSetItemContextDelegate != null && ((iSalesDocEvents_SinkHelper.m_BeforeSetItemContextDelegate.Equals(P_0) ? 1u : 0u) & 0xFFu) != 0)
				{
					m_aEventSinkHelpers.RemoveAt(num);
					m_ConnectionPoint.Unadvise(iSalesDocEvents_SinkHelper.m_dwCookie);
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

	public override void add_BeforeCreateSalesDoc(_ISalesDocEvents_BeforeCreateSalesDocEventHandler P_0)
	{
		bool lockTaken = default(bool);
		try
		{
			Monitor.Enter(this, ref lockTaken);
			if (m_ConnectionPoint == null)
			{
				Init();
			}
			_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = new _ISalesDocEvents_SinkHelper();
			int pdwCookie = 0;
			m_ConnectionPoint.Advise(iSalesDocEvents_SinkHelper, out pdwCookie);
			iSalesDocEvents_SinkHelper.m_dwCookie = pdwCookie;
			iSalesDocEvents_SinkHelper.m_BeforeCreateSalesDocDelegate = P_0;
			m_aEventSinkHelpers.Add(iSalesDocEvents_SinkHelper);
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(this);
			}
		}
	}

	public override void remove_BeforeCreateSalesDoc(_ISalesDocEvents_BeforeCreateSalesDocEventHandler P_0)
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
				_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = (_ISalesDocEvents_SinkHelper)m_aEventSinkHelpers[num];
				if (iSalesDocEvents_SinkHelper.m_BeforeCreateSalesDocDelegate != null && ((iSalesDocEvents_SinkHelper.m_BeforeCreateSalesDocDelegate.Equals(P_0) ? 1u : 0u) & 0xFFu) != 0)
				{
					m_aEventSinkHelpers.RemoveAt(num);
					m_ConnectionPoint.Unadvise(iSalesDocEvents_SinkHelper.m_dwCookie);
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

	public override void add_AfterCreateSalesDoc(_ISalesDocEvents_AfterCreateSalesDocEventHandler P_0)
	{
		bool lockTaken = default(bool);
		try
		{
			Monitor.Enter(this, ref lockTaken);
			if (m_ConnectionPoint == null)
			{
				Init();
			}
			_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = new _ISalesDocEvents_SinkHelper();
			int pdwCookie = 0;
			m_ConnectionPoint.Advise(iSalesDocEvents_SinkHelper, out pdwCookie);
			iSalesDocEvents_SinkHelper.m_dwCookie = pdwCookie;
			iSalesDocEvents_SinkHelper.m_AfterCreateSalesDocDelegate = P_0;
			m_aEventSinkHelpers.Add(iSalesDocEvents_SinkHelper);
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(this);
			}
		}
	}

	public override void remove_AfterCreateSalesDoc(_ISalesDocEvents_AfterCreateSalesDocEventHandler P_0)
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
				_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = (_ISalesDocEvents_SinkHelper)m_aEventSinkHelpers[num];
				if (iSalesDocEvents_SinkHelper.m_AfterCreateSalesDocDelegate != null && ((iSalesDocEvents_SinkHelper.m_AfterCreateSalesDocDelegate.Equals(P_0) ? 1u : 0u) & 0xFFu) != 0)
				{
					m_aEventSinkHelpers.RemoveAt(num);
					m_ConnectionPoint.Unadvise(iSalesDocEvents_SinkHelper.m_dwCookie);
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

	public override void add_BeforeSetItemXml(_ISalesDocEvents_BeforeSetItemXmlEventHandler P_0)
	{
		bool lockTaken = default(bool);
		try
		{
			Monitor.Enter(this, ref lockTaken);
			if (m_ConnectionPoint == null)
			{
				Init();
			}
			_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = new _ISalesDocEvents_SinkHelper();
			int pdwCookie = 0;
			m_ConnectionPoint.Advise(iSalesDocEvents_SinkHelper, out pdwCookie);
			iSalesDocEvents_SinkHelper.m_dwCookie = pdwCookie;
			iSalesDocEvents_SinkHelper.m_BeforeSetItemXmlDelegate = P_0;
			m_aEventSinkHelpers.Add(iSalesDocEvents_SinkHelper);
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(this);
			}
		}
	}

	public override void remove_BeforeSetItemXml(_ISalesDocEvents_BeforeSetItemXmlEventHandler P_0)
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
				_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = (_ISalesDocEvents_SinkHelper)m_aEventSinkHelpers[num];
				if (iSalesDocEvents_SinkHelper.m_BeforeSetItemXmlDelegate != null && ((iSalesDocEvents_SinkHelper.m_BeforeSetItemXmlDelegate.Equals(P_0) ? 1u : 0u) & 0xFFu) != 0)
				{
					m_aEventSinkHelpers.RemoveAt(num);
					m_ConnectionPoint.Unadvise(iSalesDocEvents_SinkHelper.m_dwCookie);
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

	public override void add_AfterSetItemXml(_ISalesDocEvents_AfterSetItemXmlEventHandler P_0)
	{
		bool lockTaken = default(bool);
		try
		{
			Monitor.Enter(this, ref lockTaken);
			if (m_ConnectionPoint == null)
			{
				Init();
			}
			_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = new _ISalesDocEvents_SinkHelper();
			int pdwCookie = 0;
			m_ConnectionPoint.Advise(iSalesDocEvents_SinkHelper, out pdwCookie);
			iSalesDocEvents_SinkHelper.m_dwCookie = pdwCookie;
			iSalesDocEvents_SinkHelper.m_AfterSetItemXmlDelegate = P_0;
			m_aEventSinkHelpers.Add(iSalesDocEvents_SinkHelper);
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(this);
			}
		}
	}

	public override void remove_AfterSetItemXml(_ISalesDocEvents_AfterSetItemXmlEventHandler P_0)
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
				_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = (_ISalesDocEvents_SinkHelper)m_aEventSinkHelpers[num];
				if (iSalesDocEvents_SinkHelper.m_AfterSetItemXmlDelegate != null && ((iSalesDocEvents_SinkHelper.m_AfterSetItemXmlDelegate.Equals(P_0) ? 1u : 0u) & 0xFFu) != 0)
				{
					m_aEventSinkHelpers.RemoveAt(num);
					m_ConnectionPoint.Unadvise(iSalesDocEvents_SinkHelper.m_dwCookie);
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

	public override void add_BeforeRecalculateItemPrices(_ISalesDocEvents_BeforeRecalculateItemPricesEventHandler P_0)
	{
		bool lockTaken = default(bool);
		try
		{
			Monitor.Enter(this, ref lockTaken);
			if (m_ConnectionPoint == null)
			{
				Init();
			}
			_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = new _ISalesDocEvents_SinkHelper();
			int pdwCookie = 0;
			m_ConnectionPoint.Advise(iSalesDocEvents_SinkHelper, out pdwCookie);
			iSalesDocEvents_SinkHelper.m_dwCookie = pdwCookie;
			iSalesDocEvents_SinkHelper.m_BeforeRecalculateItemPricesDelegate = P_0;
			m_aEventSinkHelpers.Add(iSalesDocEvents_SinkHelper);
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(this);
			}
		}
	}

	public override void remove_BeforeRecalculateItemPrices(_ISalesDocEvents_BeforeRecalculateItemPricesEventHandler P_0)
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
				_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = (_ISalesDocEvents_SinkHelper)m_aEventSinkHelpers[num];
				if (iSalesDocEvents_SinkHelper.m_BeforeRecalculateItemPricesDelegate != null && ((iSalesDocEvents_SinkHelper.m_BeforeRecalculateItemPricesDelegate.Equals(P_0) ? 1u : 0u) & 0xFFu) != 0)
				{
					m_aEventSinkHelpers.RemoveAt(num);
					m_ConnectionPoint.Unadvise(iSalesDocEvents_SinkHelper.m_dwCookie);
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

	public override void add_AfterRecalculateItemPrices(_ISalesDocEvents_AfterRecalculateItemPricesEventHandler P_0)
	{
		bool lockTaken = default(bool);
		try
		{
			Monitor.Enter(this, ref lockTaken);
			if (m_ConnectionPoint == null)
			{
				Init();
			}
			_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = new _ISalesDocEvents_SinkHelper();
			int pdwCookie = 0;
			m_ConnectionPoint.Advise(iSalesDocEvents_SinkHelper, out pdwCookie);
			iSalesDocEvents_SinkHelper.m_dwCookie = pdwCookie;
			iSalesDocEvents_SinkHelper.m_AfterRecalculateItemPricesDelegate = P_0;
			m_aEventSinkHelpers.Add(iSalesDocEvents_SinkHelper);
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(this);
			}
		}
	}

	public override void remove_AfterRecalculateItemPrices(_ISalesDocEvents_AfterRecalculateItemPricesEventHandler P_0)
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
				_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = (_ISalesDocEvents_SinkHelper)m_aEventSinkHelpers[num];
				if (iSalesDocEvents_SinkHelper.m_AfterRecalculateItemPricesDelegate != null && ((iSalesDocEvents_SinkHelper.m_AfterRecalculateItemPricesDelegate.Equals(P_0) ? 1u : 0u) & 0xFFu) != 0)
				{
					m_aEventSinkHelpers.RemoveAt(num);
					m_ConnectionPoint.Unadvise(iSalesDocEvents_SinkHelper.m_dwCookie);
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

	public override void add_BeforeCalculateScheduling(_ISalesDocEvents_BeforeCalculateSchedulingEventHandler P_0)
	{
		bool lockTaken = default(bool);
		try
		{
			Monitor.Enter(this, ref lockTaken);
			if (m_ConnectionPoint == null)
			{
				Init();
			}
			_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = new _ISalesDocEvents_SinkHelper();
			int pdwCookie = 0;
			m_ConnectionPoint.Advise(iSalesDocEvents_SinkHelper, out pdwCookie);
			iSalesDocEvents_SinkHelper.m_dwCookie = pdwCookie;
			iSalesDocEvents_SinkHelper.m_BeforeCalculateSchedulingDelegate = P_0;
			m_aEventSinkHelpers.Add(iSalesDocEvents_SinkHelper);
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(this);
			}
		}
	}

	public override void remove_BeforeCalculateScheduling(_ISalesDocEvents_BeforeCalculateSchedulingEventHandler P_0)
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
				_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = (_ISalesDocEvents_SinkHelper)m_aEventSinkHelpers[num];
				if (iSalesDocEvents_SinkHelper.m_BeforeCalculateSchedulingDelegate != null && ((iSalesDocEvents_SinkHelper.m_BeforeCalculateSchedulingDelegate.Equals(P_0) ? 1u : 0u) & 0xFFu) != 0)
				{
					m_aEventSinkHelpers.RemoveAt(num);
					m_ConnectionPoint.Unadvise(iSalesDocEvents_SinkHelper.m_dwCookie);
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

	public override void add_AfterCalculateScheduling(_ISalesDocEvents_AfterCalculateSchedulingEventHandler P_0)
	{
		bool lockTaken = default(bool);
		try
		{
			Monitor.Enter(this, ref lockTaken);
			if (m_ConnectionPoint == null)
			{
				Init();
			}
			_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = new _ISalesDocEvents_SinkHelper();
			int pdwCookie = 0;
			m_ConnectionPoint.Advise(iSalesDocEvents_SinkHelper, out pdwCookie);
			iSalesDocEvents_SinkHelper.m_dwCookie = pdwCookie;
			iSalesDocEvents_SinkHelper.m_AfterCalculateSchedulingDelegate = P_0;
			m_aEventSinkHelpers.Add(iSalesDocEvents_SinkHelper);
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(this);
			}
		}
	}

	public override void remove_AfterCalculateScheduling(_ISalesDocEvents_AfterCalculateSchedulingEventHandler P_0)
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
				_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = (_ISalesDocEvents_SinkHelper)m_aEventSinkHelpers[num];
				if (iSalesDocEvents_SinkHelper.m_AfterCalculateSchedulingDelegate != null && ((iSalesDocEvents_SinkHelper.m_AfterCalculateSchedulingDelegate.Equals(P_0) ? 1u : 0u) & 0xFFu) != 0)
				{
					m_aEventSinkHelpers.RemoveAt(num);
					m_ConnectionPoint.Unadvise(iSalesDocEvents_SinkHelper.m_dwCookie);
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

	public override void add_NewWizardSelectNumeration(_ISalesDocEvents_NewWizardSelectNumerationEventHandler P_0)
	{
		bool lockTaken = default(bool);
		try
		{
			Monitor.Enter(this, ref lockTaken);
			if (m_ConnectionPoint == null)
			{
				Init();
			}
			_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = new _ISalesDocEvents_SinkHelper();
			int pdwCookie = 0;
			m_ConnectionPoint.Advise(iSalesDocEvents_SinkHelper, out pdwCookie);
			iSalesDocEvents_SinkHelper.m_dwCookie = pdwCookie;
			iSalesDocEvents_SinkHelper.m_NewWizardSelectNumerationDelegate = P_0;
			m_aEventSinkHelpers.Add(iSalesDocEvents_SinkHelper);
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(this);
			}
		}
	}

	public override void remove_NewWizardSelectNumeration(_ISalesDocEvents_NewWizardSelectNumerationEventHandler P_0)
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
				_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = (_ISalesDocEvents_SinkHelper)m_aEventSinkHelpers[num];
				if (iSalesDocEvents_SinkHelper.m_NewWizardSelectNumerationDelegate != null && ((iSalesDocEvents_SinkHelper.m_NewWizardSelectNumerationDelegate.Equals(P_0) ? 1u : 0u) & 0xFFu) != 0)
				{
					m_aEventSinkHelpers.RemoveAt(num);
					m_ConnectionPoint.Unadvise(iSalesDocEvents_SinkHelper.m_dwCookie);
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

	public override void add_AfterNewVersion(_ISalesDocEvents_AfterNewVersionEventHandler P_0)
	{
		bool lockTaken = default(bool);
		try
		{
			Monitor.Enter(this, ref lockTaken);
			if (m_ConnectionPoint == null)
			{
				Init();
			}
			_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = new _ISalesDocEvents_SinkHelper();
			int pdwCookie = 0;
			m_ConnectionPoint.Advise(iSalesDocEvents_SinkHelper, out pdwCookie);
			iSalesDocEvents_SinkHelper.m_dwCookie = pdwCookie;
			iSalesDocEvents_SinkHelper.m_AfterNewVersionDelegate = P_0;
			m_aEventSinkHelpers.Add(iSalesDocEvents_SinkHelper);
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(this);
			}
		}
	}

	public override void remove_AfterNewVersion(_ISalesDocEvents_AfterNewVersionEventHandler P_0)
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
				_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = (_ISalesDocEvents_SinkHelper)m_aEventSinkHelpers[num];
				if (iSalesDocEvents_SinkHelper.m_AfterNewVersionDelegate != null && ((iSalesDocEvents_SinkHelper.m_AfterNewVersionDelegate.Equals(P_0) ? 1u : 0u) & 0xFFu) != 0)
				{
					m_aEventSinkHelpers.RemoveAt(num);
					m_ConnectionPoint.Unadvise(iSalesDocEvents_SinkHelper.m_dwCookie);
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

	public override void add_BeforeSetDataVersion(_ISalesDocEvents_BeforeSetDataVersionEventHandler P_0)
	{
		bool lockTaken = default(bool);
		try
		{
			Monitor.Enter(this, ref lockTaken);
			if (m_ConnectionPoint == null)
			{
				Init();
			}
			_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = new _ISalesDocEvents_SinkHelper();
			int pdwCookie = 0;
			m_ConnectionPoint.Advise(iSalesDocEvents_SinkHelper, out pdwCookie);
			iSalesDocEvents_SinkHelper.m_dwCookie = pdwCookie;
			iSalesDocEvents_SinkHelper.m_BeforeSetDataVersionDelegate = P_0;
			m_aEventSinkHelpers.Add(iSalesDocEvents_SinkHelper);
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(this);
			}
		}
	}

	public override void remove_BeforeSetDataVersion(_ISalesDocEvents_BeforeSetDataVersionEventHandler P_0)
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
				_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = (_ISalesDocEvents_SinkHelper)m_aEventSinkHelpers[num];
				if (iSalesDocEvents_SinkHelper.m_BeforeSetDataVersionDelegate != null && ((iSalesDocEvents_SinkHelper.m_BeforeSetDataVersionDelegate.Equals(P_0) ? 1u : 0u) & 0xFFu) != 0)
				{
					m_aEventSinkHelpers.RemoveAt(num);
					m_ConnectionPoint.Unadvise(iSalesDocEvents_SinkHelper.m_dwCookie);
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

	public override void add_SearchConsolidationRiskPonderation(_ISalesDocEvents_SearchConsolidationRiskPonderationEventHandler P_0)
	{
		bool lockTaken = default(bool);
		try
		{
			Monitor.Enter(this, ref lockTaken);
			if (m_ConnectionPoint == null)
			{
				Init();
			}
			_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = new _ISalesDocEvents_SinkHelper();
			int pdwCookie = 0;
			m_ConnectionPoint.Advise(iSalesDocEvents_SinkHelper, out pdwCookie);
			iSalesDocEvents_SinkHelper.m_dwCookie = pdwCookie;
			iSalesDocEvents_SinkHelper.m_SearchConsolidationRiskPonderationDelegate = P_0;
			m_aEventSinkHelpers.Add(iSalesDocEvents_SinkHelper);
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(this);
			}
		}
	}

	public override void remove_SearchConsolidationRiskPonderation(_ISalesDocEvents_SearchConsolidationRiskPonderationEventHandler P_0)
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
				_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = (_ISalesDocEvents_SinkHelper)m_aEventSinkHelpers[num];
				if (iSalesDocEvents_SinkHelper.m_SearchConsolidationRiskPonderationDelegate != null && ((iSalesDocEvents_SinkHelper.m_SearchConsolidationRiskPonderationDelegate.Equals(P_0) ? 1u : 0u) & 0xFFu) != 0)
				{
					m_aEventSinkHelpers.RemoveAt(num);
					m_ConnectionPoint.Unadvise(iSalesDocEvents_SinkHelper.m_dwCookie);
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

	public override void add_ReplaceItemCode(_ISalesDocEvents_ReplaceItemCodeEventHandler P_0)
	{
		bool lockTaken = default(bool);
		try
		{
			Monitor.Enter(this, ref lockTaken);
			if (m_ConnectionPoint == null)
			{
				Init();
			}
			_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = new _ISalesDocEvents_SinkHelper();
			int pdwCookie = 0;
			m_ConnectionPoint.Advise(iSalesDocEvents_SinkHelper, out pdwCookie);
			iSalesDocEvents_SinkHelper.m_dwCookie = pdwCookie;
			iSalesDocEvents_SinkHelper.m_ReplaceItemCodeDelegate = P_0;
			m_aEventSinkHelpers.Add(iSalesDocEvents_SinkHelper);
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(this);
			}
		}
	}

	public override void remove_ReplaceItemCode(_ISalesDocEvents_ReplaceItemCodeEventHandler P_0)
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
				_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = (_ISalesDocEvents_SinkHelper)m_aEventSinkHelpers[num];
				if (iSalesDocEvents_SinkHelper.m_ReplaceItemCodeDelegate != null && ((iSalesDocEvents_SinkHelper.m_ReplaceItemCodeDelegate.Equals(P_0) ? 1u : 0u) & 0xFFu) != 0)
				{
					m_aEventSinkHelpers.RemoveAt(num);
					m_ConnectionPoint.Unadvise(iSalesDocEvents_SinkHelper.m_dwCookie);
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

	public override void add_AfterReplaceItemCode(_ISalesDocEvents_AfterReplaceItemCodeEventHandler P_0)
	{
		bool lockTaken = default(bool);
		try
		{
			Monitor.Enter(this, ref lockTaken);
			if (m_ConnectionPoint == null)
			{
				Init();
			}
			_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = new _ISalesDocEvents_SinkHelper();
			int pdwCookie = 0;
			m_ConnectionPoint.Advise(iSalesDocEvents_SinkHelper, out pdwCookie);
			iSalesDocEvents_SinkHelper.m_dwCookie = pdwCookie;
			iSalesDocEvents_SinkHelper.m_AfterReplaceItemCodeDelegate = P_0;
			m_aEventSinkHelpers.Add(iSalesDocEvents_SinkHelper);
		}
		finally
		{
			if (lockTaken)
			{
				Monitor.Exit(this);
			}
		}
	}

	public override void remove_AfterReplaceItemCode(_ISalesDocEvents_AfterReplaceItemCodeEventHandler P_0)
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
				_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = (_ISalesDocEvents_SinkHelper)m_aEventSinkHelpers[num];
				if (iSalesDocEvents_SinkHelper.m_AfterReplaceItemCodeDelegate != null && ((iSalesDocEvents_SinkHelper.m_AfterReplaceItemCodeDelegate.Equals(P_0) ? 1u : 0u) & 0xFFu) != 0)
				{
					m_aEventSinkHelpers.RemoveAt(num);
					m_ConnectionPoint.Unadvise(iSalesDocEvents_SinkHelper.m_dwCookie);
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

	public _ISalesDocEvents_EventProvider(object P_0)
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
					_ISalesDocEvents_SinkHelper iSalesDocEvents_SinkHelper = (_ISalesDocEvents_SinkHelper)m_aEventSinkHelpers[num];
					m_ConnectionPoint.Unadvise(iSalesDocEvents_SinkHelper.m_dwCookie);
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
