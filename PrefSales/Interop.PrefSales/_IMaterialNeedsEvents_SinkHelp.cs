using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[TypeLibType(TypeLibTypeFlags.FHidden)]
[ClassInterface(ClassInterfaceType.None)]
public sealed class _IMaterialNeedsEvents_SinkHelper : _IMaterialNeedsEvents
{
	public _IMaterialNeedsEvents_BeforeCalculateMaterialNeedsEventHandler m_BeforeCalculateMaterialNeedsDelegate;

	public _IMaterialNeedsEvents_AfterCalculateMaterialNeedsEventHandler m_AfterCalculateMaterialNeedsDelegate;

	public _IMaterialNeedsEvents_BeforeRemoveMaterialNeedsEventHandler m_BeforeRemoveMaterialNeedsDelegate;

	public int m_dwCookie;

	public override void BeforeCalculateMaterialNeeds(MaterialNeeds P_0, object P_1, ref bool P_2)
	{
		//Error decoding local variables: Signature type sequence must have at least one element.
		if (m_BeforeCalculateMaterialNeedsDelegate != null)
		{
			m_BeforeCalculateMaterialNeedsDelegate(P_0, P_1, ref P_2);
		}
	}

	public override void AfterCalculateMaterialNeeds(MaterialNeeds P_0, object P_1)
	{
		//Error decoding local variables: Signature type sequence must have at least one element.
		if (m_AfterCalculateMaterialNeedsDelegate != null)
		{
			m_AfterCalculateMaterialNeedsDelegate(P_0, P_1);
		}
	}

	public override void BeforeRemoveMaterialNeeds(MaterialNeeds P_0, object P_1, ref bool P_2)
	{
		//Error decoding local variables: Signature type sequence must have at least one element.
		if (m_BeforeRemoveMaterialNeedsDelegate != null)
		{
			m_BeforeRemoveMaterialNeedsDelegate(P_0, P_1, ref P_2);
		}
	}

	internal _IMaterialNeedsEvents_SinkHelper()
	{
		//Error decoding local variables: Signature type sequence must have at least one element.
		m_dwCookie = 0;
		m_BeforeCalculateMaterialNeedsDelegate = null;
		m_AfterCalculateMaterialNeedsDelegate = null;
		m_BeforeRemoveMaterialNeedsDelegate = null;
	}
}
