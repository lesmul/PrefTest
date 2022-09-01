using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Preference.AddWorks.Views;

[StructLayout(LayoutKind.Sequential, Size = 48)]
[NativeCppClass]
internal struct CSalesDocItemAddWorksViewerWrapper
{
	public class delegate_proxy_type
	{
		private unsafe CSalesDocItemAddWorksViewerWrapper* m_p_native_target;

		public unsafe delegate_proxy_type(CSalesDocItemAddWorksViewerWrapper* pNativeTarget)
		{
			m_p_native_target = pNativeTarget;
			base._002Ector();
		}

		public unsafe void detach()
		{
			//IL_0008: Expected I, but got I8
			m_p_native_target = null;
		}

		public unsafe void OnAddWorksCollectionChanged(object arg0, SalesDocItemArgs arg1)
		{
			if (m_p_native_target == null)
			{
				throw new ArgumentNullException("Delegate call failed: Native sink was not attached or has already detached from the managed proxy (m_p_native_target == NULL). Hint: see if native sink was destructed or not constructed properly");
			}
			_003CModule_003E.CSalesDocItemAddWorksViewerWrapper_002EOnAddWorksCollectionChanged(m_p_native_target, arg0, arg1);
			GC.KeepAlive(this);
		}
	}

	private long _003Calignment_0020member_003E;
}
