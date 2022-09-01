using System;
using System.Diagnostics;
using System.Windows.Input;

namespace Preference.Wpf.Controls.Core.Commands;

public class DelegateCommand : ICommand
{
	private readonly Action<object> _execute;

	private readonly Predicate<object> _canExecute;

	public virtual event EventHandler CanExecuteChanged;

	public DelegateCommand(Action<object> execute)
		: this(execute, null)
	{
	}

	public DelegateCommand(Action<object> execute, Predicate<object> canExecute)
	{
		if (execute == null)
		{
			throw new ArgumentNullException("execute");
		}
		_execute = execute;
		_canExecute = canExecute;
	}

	[DebuggerStepThrough]
	public bool CanExecute(object parameter)
	{
		if (_canExecute != null)
		{
			return _canExecute(parameter);
		}
		return true;
	}

	public void RaiseCanExecuteChanged()
	{
		if (this.CanExecuteChanged != null)
		{
			this.CanExecuteChanged(this, EventArgs.Empty);
		}
	}

	public void Execute(object parameter)
	{
		_execute(parameter);
	}
}
