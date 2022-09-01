using System.Runtime.InteropServices;

namespace Interop.PrefSales;

[ClassInterface(ClassInterfaceType.None)]
[TypeLibType(TypeLibTypeFlags.FHidden)]
public sealed class _ISalesDocFinderEvents_SinkHelper : _ISalesDocFinderEvents
{
	public int m_dwCookie;

	internal _ISalesDocFinderEvents_SinkHelper()
	{
		//Error decoding local variables: Signature type sequence must have at least one element.
		m_dwCookie = 0;
	}
}
