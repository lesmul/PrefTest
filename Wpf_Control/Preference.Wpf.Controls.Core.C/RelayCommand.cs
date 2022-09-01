using System;
using System.Windows.Input;

namespace Preference.Wpf.Controls.Core.Commands;

public class RelayCommand : DelegateCommand
{
	public override event EventHandler CanExecuteChanged
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

	public RelayCommand(Action<object> execute)
		: base(execute, null)
	{
	}

	public RelayCommand(Action<object> execute, Predicate<object> canExecute)
		: base(execute, canExecute)
	{
	}
}
