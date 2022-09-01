using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Preference.Vfo.Controls;

[StructLayout(LayoutKind.Sequential, Size = 136)]
[NativeCppClass]
internal struct CWManagedVfo
{
	public class delegate_proxy_type
	{
		private unsafe CWManagedVfo* m_p_native_target;

		public unsafe delegate_proxy_type(CWManagedVfo* pNativeTarget)
		{
			m_p_native_target = pNativeTarget;
			base._002Ector();
		}

		public unsafe void detach()
		{
			//IL_0008: Expected I, but got I8
			m_p_native_target = null;
		}

		public unsafe void CommandBoxKeyDown(object arg0, KeyEventArgs arg1)
		{
			if (m_p_native_target == null)
			{
				throw new ArgumentNullException("Delegate call failed: Native sink was not attached or has already detached from the managed proxy (m_p_native_target == NULL). Hint: see if native sink was destructed or not constructed properly");
			}
			_003CModule_003E.CWManagedVfo_002ECommandBoxKeyDown(m_p_native_target, arg0, arg1);
			GC.KeepAlive(this);
		}

		public unsafe void CommandBoxTextChanged(object arg0, TextChangedEventArgs arg1)
		{
			if (m_p_native_target == null)
			{
				throw new ArgumentNullException("Delegate call failed: Native sink was not attached or has already detached from the managed proxy (m_p_native_target == NULL). Hint: see if native sink was destructed or not constructed properly");
			}
			_003CModule_003E.CWManagedVfo_002ECommandBoxTextChanged(m_p_native_target, arg0, arg1);
			GC.KeepAlive(this);
		}

		public unsafe void CommandButtonExecute(object arg0, RoutedEventArgs arg1)
		{
			if (m_p_native_target == null)
			{
				throw new ArgumentNullException("Delegate call failed: Native sink was not attached or has already detached from the managed proxy (m_p_native_target == NULL). Hint: see if native sink was destructed or not constructed properly");
			}
			_003CModule_003E.CWManagedVfo_002ECommandButtonExecute(m_p_native_target, arg0, arg1);
			GC.KeepAlive(this);
		}

		public unsafe void AllowResetCommand(object arg0, CancelEventArgs arg1)
		{
			if (m_p_native_target == null)
			{
				throw new ArgumentNullException("Delegate call failed: Native sink was not attached or has already detached from the managed proxy (m_p_native_target == NULL). Hint: see if native sink was destructed or not constructed properly");
			}
			_003CModule_003E.CWManagedVfo_002EAllowResetCommand(m_p_native_target, arg0, arg1);
			GC.KeepAlive(this);
		}

		public unsafe void GridItemChanged(object arg0, GridItemEventArgs arg1)
		{
			if (m_p_native_target == null)
			{
				throw new ArgumentNullException("Delegate call failed: Native sink was not attached or has already detached from the managed proxy (m_p_native_target == NULL). Hint: see if native sink was destructed or not constructed properly");
			}
			_003CModule_003E.CWManagedVfo_002EGridItemChanged(m_p_native_target, arg0, arg1);
			GC.KeepAlive(this);
		}

		public unsafe void ItemRecalculated(object arg0, EventArgs arg1)
		{
			if (m_p_native_target == null)
			{
				throw new ArgumentNullException("Delegate call failed: Native sink was not attached or has already detached from the managed proxy (m_p_native_target == NULL). Hint: see if native sink was destructed or not constructed properly");
			}
			_003CModule_003E.CWManagedVfo_002EItemRecalculated(m_p_native_target, arg0, arg1);
			GC.KeepAlive(this);
		}

		public unsafe void ItemPropertyChanged(object arg0, PropertyChangedEventArgs arg1)
		{
			if (m_p_native_target == null)
			{
				throw new ArgumentNullException("Delegate call failed: Native sink was not attached or has already detached from the managed proxy (m_p_native_target == NULL). Hint: see if native sink was destructed or not constructed properly");
			}
			_003CModule_003E.CWManagedVfo_002EItemPropertyChanged(m_p_native_target, arg0, arg1);
			GC.KeepAlive(this);
		}

		public unsafe void ItemsCollectionChanged(object arg0, NotifyCollectionChangedEventArgs arg1)
		{
			if (m_p_native_target == null)
			{
				throw new ArgumentNullException("Delegate call failed: Native sink was not attached or has already detached from the managed proxy (m_p_native_target == NULL). Hint: see if native sink was destructed or not constructed properly");
			}
			_003CModule_003E.CWManagedVfo_002EItemsCollectionChanged(m_p_native_target, arg0, arg1);
			GC.KeepAlive(this);
		}

		public unsafe void LoadCompleted(object arg0, EventArgs arg1)
		{
			if (m_p_native_target == null)
			{
				throw new ArgumentNullException("Delegate call failed: Native sink was not attached or has already detached from the managed proxy (m_p_native_target == NULL). Hint: see if native sink was destructed or not constructed properly");
			}
			_003CModule_003E.CWManagedVfo_002ELoadCompleted(m_p_native_target, arg0, arg1);
			GC.KeepAlive(this);
		}

		public unsafe void SelectedItemsChanged(object arg0, NotifyCollectionChangedEventArgs arg1)
		{
			if (m_p_native_target == null)
			{
				throw new ArgumentNullException("Delegate call failed: Native sink was not attached or has already detached from the managed proxy (m_p_native_target == NULL). Hint: see if native sink was destructed or not constructed properly");
			}
			_003CModule_003E.CWManagedVfo_002ESelectedItemsChanged(m_p_native_target, arg0, arg1);
			GC.KeepAlive(this);
		}

		public unsafe void InvalidSortOrderDetected(object arg0, EventArgs arg1)
		{
			if (m_p_native_target == null)
			{
				throw new ArgumentNullException("Delegate call failed: Native sink was not attached or has already detached from the managed proxy (m_p_native_target == NULL). Hint: see if native sink was destructed or not constructed properly");
			}
			_003CModule_003E.CWManagedVfo_002EInvalidSortOrderDetected(m_p_native_target, arg0, arg1);
			GC.KeepAlive(this);
		}
	}

	private long _003Calignment_0020member_003E;
}
