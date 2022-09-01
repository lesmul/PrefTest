using System.Windows.Threading;

namespace Preference.Wpf.Controls;

internal static class DispatcherHelper
{
	internal static void WaitForPriority(DispatcherPriority priority)
	{
		DispatcherFrame dispatcherFrame = new DispatcherFrame();
		DispatcherOperation dispatcherOperation = Dispatcher.CurrentDispatcher.BeginInvoke(priority, new DispatcherOperationCallback(ExitFrameOperation), dispatcherFrame);
		Dispatcher.PushFrame(dispatcherFrame);
		if (dispatcherOperation.Status != DispatcherOperationStatus.Completed)
		{
			dispatcherOperation.Abort();
		}
	}

	private static object ExitFrameOperation(object obj)
	{
		((DispatcherFrame)obj).Continue = false;
		return null;
	}
}
