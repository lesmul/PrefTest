using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace Preference.WPF.MaterialsSelector.Core.Commands;

public class DelegateCommand : ICommand
{
	private string _strCommandName;

	private readonly Action _executeMethod;

	private readonly Func<bool> _canExecuteMethod;

	private bool _isAutomaticRequeryDisabled;

	private List<WeakReference> _canExecuteChangedHandlers;

	public bool IsAutomaticRequeryDisabled
	{
		get
		{
			return _isAutomaticRequeryDisabled;
		}
		set
		{
			if (_isAutomaticRequeryDisabled != value)
			{
				if (value)
				{
					CommandManagerHelper.RemoveHandlersFromRequerySuggested(_canExecuteChangedHandlers);
				}
				else
				{
					CommandManagerHelper.AddHandlersToRequerySuggested(_canExecuteChangedHandlers);
				}
				_isAutomaticRequeryDisabled = value;
			}
		}
	}

	public event EventHandler CanExecuteChanged
	{
		add
		{
			if (!_isAutomaticRequeryDisabled)
			{
				CommandManager.RequerySuggested += value;
			}
			CommandManagerHelper.AddWeakReferenceHandler(ref _canExecuteChangedHandlers, value, 2);
		}
		remove
		{
			if (!_isAutomaticRequeryDisabled)
			{
				CommandManager.RequerySuggested -= value;
			}
			CommandManagerHelper.RemoveWeakReferenceHandler(_canExecuteChangedHandlers, value);
		}
	}

	public DelegateCommand(string commandName, Action executeMethod)
		: this(commandName, executeMethod, null, isAutomaticRequeryDisabled: false)
	{
	}

	public DelegateCommand(string commandName, Action executeMethod, Func<bool> canExecuteMethod)
		: this(commandName, executeMethod, canExecuteMethod, isAutomaticRequeryDisabled: false)
	{
	}

	public DelegateCommand(string commandName, Action executeMethod, Func<bool> canExecuteMethod, bool isAutomaticRequeryDisabled)
	{
		if (executeMethod == null)
		{
			throw new ArgumentNullException("executeMethod");
		}
		_executeMethod = executeMethod;
		_canExecuteMethod = canExecuteMethod;
		_isAutomaticRequeryDisabled = isAutomaticRequeryDisabled;
		_strCommandName = commandName;
	}

	public bool CanExecute()
	{
		if (_canExecuteMethod != null)
		{
			return _canExecuteMethod();
		}
		return true;
	}

	public void Execute()
	{
		if (_executeMethod != null)
		{
			_executeMethod();
		}
	}

	public void RaiseCanExecuteChanged()
	{
		OnCanExecuteChanged();
	}

	public string GetCommandName()
	{
		return _strCommandName;
	}

	protected virtual void OnCanExecuteChanged()
	{
		CommandManagerHelper.CallWeakReferenceHandlers(_canExecuteChangedHandlers);
	}

	bool ICommand.CanExecute(object parameter)
	{
		return CanExecute();
	}

	void ICommand.Execute(object parameter)
	{
		Execute();
	}
}
public class DelegateCommand<T> : ICommand
{
	private readonly Action<T> _executeMethod;

	private readonly Func<T, bool> _canExecuteMethod;

	private bool _isAutomaticRequeryDisabled;

	private List<WeakReference> _canExecuteChangedHandlers;

	public bool IsAutomaticRequeryDisabled
	{
		get
		{
			return _isAutomaticRequeryDisabled;
		}
		set
		{
			if (_isAutomaticRequeryDisabled != value)
			{
				if (value)
				{
					CommandManagerHelper.RemoveHandlersFromRequerySuggested(_canExecuteChangedHandlers);
				}
				else
				{
					CommandManagerHelper.AddHandlersToRequerySuggested(_canExecuteChangedHandlers);
				}
				_isAutomaticRequeryDisabled = value;
			}
		}
	}

	public event EventHandler CanExecuteChanged
	{
		add
		{
			if (!_isAutomaticRequeryDisabled)
			{
				CommandManager.RequerySuggested += value;
			}
			CommandManagerHelper.AddWeakReferenceHandler(ref _canExecuteChangedHandlers, value, 2);
		}
		remove
		{
			if (!_isAutomaticRequeryDisabled)
			{
				CommandManager.RequerySuggested -= value;
			}
			CommandManagerHelper.RemoveWeakReferenceHandler(_canExecuteChangedHandlers, value);
		}
	}

	public DelegateCommand(Action<T> executeMethod)
		: this(executeMethod, (Func<T, bool>)null, isAutomaticRequeryDisabled: false)
	{
	}

	public DelegateCommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod)
		: this(executeMethod, canExecuteMethod, isAutomaticRequeryDisabled: false)
	{
	}

	public DelegateCommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod, bool isAutomaticRequeryDisabled)
	{
		if (executeMethod == null)
		{
			throw new ArgumentNullException("executeMethod");
		}
		_executeMethod = executeMethod;
		_canExecuteMethod = canExecuteMethod;
		_isAutomaticRequeryDisabled = isAutomaticRequeryDisabled;
	}

	public bool CanExecute(T parameter)
	{
		if (_canExecuteMethod != null)
		{
			return _canExecuteMethod(parameter);
		}
		return true;
	}

	public void Execute(T parameter)
	{
		if (_executeMethod != null)
		{
			_executeMethod(parameter);
		}
	}

	public void RaiseCanExecuteChanged()
	{
		OnCanExecuteChanged();
	}

	protected virtual void OnCanExecuteChanged()
	{
		CommandManagerHelper.CallWeakReferenceHandlers(_canExecuteChangedHandlers);
	}

	bool ICommand.CanExecute(object parameter)
	{
		if (parameter == null && typeof(T).IsValueType)
		{
			return _canExecuteMethod == null;
		}
		return CanExecute((T)parameter);
	}

	void ICommand.Execute(object parameter)
	{
		Execute((T)parameter);
	}
}
