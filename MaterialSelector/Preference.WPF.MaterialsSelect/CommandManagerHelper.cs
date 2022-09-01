using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace Preference.WPF.MaterialsSelector.Core.Commands;

internal class CommandManagerHelper
{
	internal static void CallWeakReferenceHandlers(List<WeakReference> handlers)
	{
		if (handlers == null)
		{
			return;
		}
		EventHandler[] array = new EventHandler[handlers.Count];
		int num = 0;
		for (int num2 = handlers.Count - 1; num2 >= 0; num2--)
		{
			if (!(handlers[num2].Target is EventHandler eventHandler))
			{
				handlers.RemoveAt(num2);
			}
			else
			{
				array[num] = eventHandler;
				num++;
			}
		}
		for (int i = 0; i < num; i++)
		{
			array[i](null, EventArgs.Empty);
		}
	}

	internal static void AddHandlersToRequerySuggested(List<WeakReference> handlers)
	{
		if (handlers == null)
		{
			return;
		}
		foreach (WeakReference handler in handlers)
		{
			if (handler.Target is EventHandler value)
			{
				CommandManager.RequerySuggested += value;
			}
		}
	}

	internal static void RemoveHandlersFromRequerySuggested(List<WeakReference> handlers)
	{
		if (handlers == null)
		{
			return;
		}
		foreach (WeakReference handler in handlers)
		{
			if (handler.Target is EventHandler value)
			{
				CommandManager.RequerySuggested -= value;
			}
		}
	}

	internal static void AddWeakReferenceHandler(ref List<WeakReference> handlers, EventHandler handler)
	{
		AddWeakReferenceHandler(ref handlers, handler, -1);
	}

	internal static void AddWeakReferenceHandler(ref List<WeakReference> handlers, EventHandler handler, int defaultListSize)
	{
		if (handlers == null)
		{
			handlers = ((defaultListSize > 0) ? new List<WeakReference>(defaultListSize) : new List<WeakReference>());
		}
		handlers.Add(new WeakReference(handler));
	}

	internal static void RemoveWeakReferenceHandler(List<WeakReference> handlers, EventHandler handler)
	{
		if (handlers == null)
		{
			return;
		}
		for (int num = handlers.Count - 1; num >= 0; num--)
		{
			if (!(handlers[num].Target is EventHandler eventHandler) || eventHandler == handler)
			{
				handlers.RemoveAt(num);
			}
		}
	}
}
