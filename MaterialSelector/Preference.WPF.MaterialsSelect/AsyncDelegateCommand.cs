using System;
using System.ComponentModel;
using System.Windows.Input;

namespace Preference.WPF.MaterialsSelector.Core.Commands;

public class AsyncDelegateCommand : ICommand
{
	private BackgroundWorker _worker = new BackgroundWorker();

	private Func<bool> _canExecute;

	public event EventHandler CanExecuteChanged
	{
		add
		{
			CommandManager.RequerySuggested += value;
		}
		remove
		{
			CommandManager.RequerySuggested -= value;
		}
	}

	public AsyncDelegateCommand(Action action, Func<bool> canExecute = null, Action<object> completed = null, Action<Exception> error = null)
	{
		_worker.DoWork += delegate
		{
			CommandManager.InvalidateRequerySuggested();
			action();
		};
		_worker.RunWorkerCompleted += delegate(object s, RunWorkerCompletedEventArgs e)
		{
			if (completed != null && e.Error == null)
			{
				completed(e.Result);
			}
			if (error != null && e.Error != null)
			{
				error(e.Error);
			}
			CommandManager.InvalidateRequerySuggested();
		};
		_canExecute = canExecute;
	}

	public void Cancel()
	{
		if (_worker.IsBusy)
		{
			_worker.CancelAsync();
		}
	}

	public bool CanExecute(object parameter)
	{
		if (_canExecute != null)
		{
			if (!_worker.IsBusy)
			{
				return _canExecute();
			}
			return false;
		}
		return !_worker.IsBusy;
	}

	public void Execute(object parameter)
	{
		if (!_worker.IsBusy)
		{
			_worker.RunWorkerAsync();
		}
	}
}
