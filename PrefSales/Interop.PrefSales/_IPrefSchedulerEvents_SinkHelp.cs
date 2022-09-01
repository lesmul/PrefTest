using System.Runtime.InteropServices;
using Interop.MSXML2;

namespace Interop.PrefSales;

[TypeLibType(TypeLibTypeFlags.FHidden)]
[ClassInterface(ClassInterfaceType.None)]
public sealed class _IPrefSchedulerEvents_SinkHelper : _IPrefSchedulerEvents
{
	public _IPrefSchedulerEvents_BeforeLoadSchedulerEventHandler m_BeforeLoadSchedulerDelegate;

	public _IPrefSchedulerEvents_BeforeLoadSelectionDetailEventHandler m_BeforeLoadSelectionDetailDelegate;

	public _IPrefSchedulerEvents_OrderDoubleClickEventHandler m_OrderDoubleClickDelegate;

	public _IPrefSchedulerEvents_PODoubleClickEventHandler m_PODoubleClickDelegate;

	public int m_dwCookie;

	public override void BeforeLoadScheduler(PrefScheduler P_0, IXMLDOMDocument2 P_1)
	{
		//Error decoding local variables: Signature type sequence must have at least one element.
		if (m_BeforeLoadSchedulerDelegate != null)
		{
			m_BeforeLoadSchedulerDelegate(P_0, P_1);
		}
	}

	public override void BeforeLoadSelectionDetail(PrefScheduler P_0, IXMLDOMDocument2 P_1)
	{
		//Error decoding local variables: Signature type sequence must have at least one element.
		if (m_BeforeLoadSelectionDetailDelegate != null)
		{
			m_BeforeLoadSelectionDetailDelegate(P_0, P_1);
		}
	}

	public override void OrderDoubleClick(int P_0, int P_1)
	{
		//Error decoding local variables: Signature type sequence must have at least one element.
		if (m_OrderDoubleClickDelegate != null)
		{
			m_OrderDoubleClickDelegate(P_0, P_1);
		}
	}

	public override void PODoubleClick(int P_0, int P_1)
	{
		//Error decoding local variables: Signature type sequence must have at least one element.
		if (m_PODoubleClickDelegate != null)
		{
			m_PODoubleClickDelegate(P_0, P_1);
		}
	}

	internal _IPrefSchedulerEvents_SinkHelper()
	{
		//Error decoding local variables: Signature type sequence must have at least one element.
		m_dwCookie = 0;
		m_BeforeLoadSchedulerDelegate = null;
		m_BeforeLoadSelectionDetailDelegate = null;
		m_OrderDoubleClickDelegate = null;
		m_PODoubleClickDelegate = null;
	}
}
