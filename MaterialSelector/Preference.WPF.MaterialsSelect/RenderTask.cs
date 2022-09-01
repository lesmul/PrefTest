#define TRACE
using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Windows.Threading;
using Preference.Diagnostics;
using Preference.WPF.MaterialsSelector.Models;

namespace Preference.WPF.MaterialsSelector.Core;

public class RenderTask
{
	private IServiceAgent _ServiceAgent;

	private object _item;

	private string _strKey;

	private string _strXAML = string.Empty;

	private ManualResetEvent _doneEvent;

	private static bool _bIsCanceled;

	public static bool IsCanceled
	{
		get
		{
			return _bIsCanceled;
		}
		set
		{
			_bIsCanceled = value;
		}
	}

	public ManualResetEvent Done => _doneEvent;

	public RenderTask(ManualResetEvent doneEvent, object value, IServiceAgent serviceAgent)
	{
		_ServiceAgent = serviceAgent;
		_doneEvent = doneEvent;
		_item = value;
		if (value is Square)
		{
			_strKey = (value as Square).Id;
		}
	}

	public void ThreadPoolCallback(object threadContext)
	{
		try
		{
			Trace.WriteLine($"INICIO TAREA SECUNDARIA: {Thread.CurrentThread.ManagedThreadId}");
			if (IsCanceled)
			{
				return;
			}
			Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
			_ = Thread.CurrentThread.CurrentCulture.Name;
			Thread.CurrentThread.GetApartmentState();
			if (!string.IsNullOrEmpty(_strKey))
			{
				_strXAML = _ServiceAgent.GetWpfDrawing(_strKey);
			}
			else
			{
				_strXAML = _ServiceAgent.GetWpfDrawing(_strKey);
			}
			Dispatcher.CurrentDispatcher.Invoke((ThreadStart)delegate
			{
				Trace.WriteLine($"ESTABLECER VALORES DE LA TAREA PRINCIPAL: {Thread.CurrentThread.ManagedThreadId}");
				if (string.IsNullOrEmpty(_strXAML))
				{
					_strXAML = "NONE";
				}
				if (_item != null)
				{
					if (_item is Model)
					{
						(_item as Model).WpfDrawing = _strXAML;
					}
					else if (_item is Square)
					{
						(_item as Square).WpfDrawing = _strXAML;
					}
				}
			});
			Done.Set();
			Trace.WriteLine($"FIN TAREA SECUNDARIA: {Thread.CurrentThread.ManagedThreadId}");
		}
		catch (ThreadAbortException ex)
		{
			_strXAML = "NONE";
			Logger.Instance.WriteError((Exception)ex, "PrefCIM");
		}
		catch (OutOfMemoryException ex2)
		{
			_strXAML = "NONE";
			Logger.Instance.WriteError((Exception)ex2, "PrefCIM");
		}
		catch (Exception ex3)
		{
			_strXAML = "NONE";
			Logger.Instance.WriteError(ex3, "PrefCIM");
		}
	}
}
