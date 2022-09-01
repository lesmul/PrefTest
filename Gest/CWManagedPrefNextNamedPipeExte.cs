using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Preference.Next.ExternalConnection;

[StructLayout(LayoutKind.Sequential, Size = 48)]
[NativeCppClass]
internal struct CWManagedPrefNextNamedPipeExternalClient
{
	public class delegate_proxy_type
	{
		private unsafe CWManagedPrefNextNamedPipeExternalClient* m_p_native_target;

		public unsafe delegate_proxy_type(CWManagedPrefNextNamedPipeExternalClient* pNativeTarget)
		{
			m_p_native_target = pNativeTarget;
			base._002Ector();
		}

		public unsafe void detach()
		{
			//IL_0008: Expected I, but got I8
			m_p_native_target = null;
		}

		public unsafe void OnItemModified(object arg0, ClientItemArgs arg1)
		{
			if (m_p_native_target == null)
			{
				throw new ArgumentNullException("Delegate call failed: Native sink was not attached or has already detached from the managed proxy (m_p_native_target == NULL). Hint: see if native sink was destructed or not constructed properly");
			}
			_003CModule_003E.CWManagedPrefNextNamedPipeExternalClient_002EOnItemModified(m_p_native_target, arg0, arg1);
			GC.KeepAlive(this);
		}

		public unsafe void OnChangeSelectedItem(object arg0, ClientItemOffsetArgs arg1)
		{
			if (m_p_native_target == null)
			{
				throw new ArgumentNullException("Delegate call failed: Native sink was not attached or has already detached from the managed proxy (m_p_native_target == NULL). Hint: see if native sink was destructed or not constructed properly");
			}
			_003CModule_003E.CWManagedPrefNextNamedPipeExternalClient_002EOnChangeSelectedItem(m_p_native_target, arg0, arg1);
			GC.KeepAlive(this);
		}

		public unsafe void OnPdfInjectionCompleted(object arg0, PdfDocumentArgs arg1)
		{
			if (m_p_native_target == null)
			{
				throw new ArgumentNullException("Delegate call failed: Native sink was not attached or has already detached from the managed proxy (m_p_native_target == NULL). Hint: see if native sink was destructed or not constructed properly");
			}
			_003CModule_003E.CWManagedPrefNextNamedPipeExternalClient_002EOnPdfInjectionCompleted(m_p_native_target, arg0, arg1);
			GC.KeepAlive(this);
		}

		public unsafe void OnPdfInjectionFailed(object arg0, PdfFailedDocumentArgs arg1)
		{
			if (m_p_native_target == null)
			{
				throw new ArgumentNullException("Delegate call failed: Native sink was not attached or has already detached from the managed proxy (m_p_native_target == NULL). Hint: see if native sink was destructed or not constructed properly");
			}
			_003CModule_003E.CWManagedPrefNextNamedPipeExternalClient_002EOnPdfInjectionFailed(m_p_native_target, arg0, arg1);
			GC.KeepAlive(this);
		}

		public unsafe void OnEditSessionStarting(object arg0, ClientItemArgs arg1)
		{
			if (m_p_native_target == null)
			{
				throw new ArgumentNullException("Delegate call failed: Native sink was not attached or has already detached from the managed proxy (m_p_native_target == NULL). Hint: see if native sink was destructed or not constructed properly");
			}
			_003CModule_003E.CWManagedPrefNextNamedPipeExternalClient_002EOnEditSessionStarting(m_p_native_target, arg0, arg1);
			GC.KeepAlive(this);
		}

		public unsafe void OnEditSessionTerminated(object arg0, ClientItemArgs arg1)
		{
			if (m_p_native_target == null)
			{
				throw new ArgumentNullException("Delegate call failed: Native sink was not attached or has already detached from the managed proxy (m_p_native_target == NULL). Hint: see if native sink was destructed or not constructed properly");
			}
			_003CModule_003E.CWManagedPrefNextNamedPipeExternalClient_002EOnEditSessionTerminated(m_p_native_target, arg0, arg1);
			GC.KeepAlive(this);
		}

		public unsafe void OnGoBack(object arg0, EventArgs arg1)
		{
			if (m_p_native_target == null)
			{
				throw new ArgumentNullException("Delegate call failed: Native sink was not attached or has already detached from the managed proxy (m_p_native_target == NULL). Hint: see if native sink was destructed or not constructed properly");
			}
			_003CModule_003E.CWManagedPrefNextNamedPipeExternalClient_002EOnGoBack(m_p_native_target, arg0, arg1);
			GC.KeepAlive(this);
		}

		public unsafe void OnExecuteCommands(object arg0, ExecuteCommandsArgs arg1)
		{
			if (m_p_native_target == null)
			{
				throw new ArgumentNullException("Delegate call failed: Native sink was not attached or has already detached from the managed proxy (m_p_native_target == NULL). Hint: see if native sink was destructed or not constructed properly");
			}
			_003CModule_003E.CWManagedPrefNextNamedPipeExternalClient_002EOnExecuteCommands(m_p_native_target, arg0, arg1);
			GC.KeepAlive(this);
		}
	}

	private long _003Calignment_0020member_003E;
}
