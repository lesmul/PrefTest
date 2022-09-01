using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;
using Telerik.Windows.Controls;

namespace Preference.Wpf.Controls.Core;

public class ErrorDialogLog : Window, IComponentConnector
{
	private IEnumerable<string> _errorLogs;

	internal TextBlock ErrorMessage;

	private bool _contentLoaded;

	public ErrorDialogLog(string errorMessage, string errorlog)
	{
		InitializeComponent();
		ErrorMessage.Text = errorMessage;
		_errorLogs = new List<string> { errorlog };
	}

	public ErrorDialogLog(string errorMessage, IEnumerable<string> errorlogs)
	{
		InitializeComponent();
		ErrorMessage.Text = errorMessage;
		_errorLogs = errorlogs;
	}

	private void OnShowLog(object sender, RoutedEventArgs e)
	{
		string text = FormatErrorLogList();
		NotepadHelper.ShowMessage(text, "Errors");
	}

	private void OnAccept(object sender, RoutedEventArgs e)
	{
		Close();
	}

	private string FormatErrorLogList()
	{
		StringBuilder stringBuilder = new StringBuilder();
		foreach (string errorLog in _errorLogs)
		{
			stringBuilder.AppendLine(errorLog);
		}
		return stringBuilder.ToString();
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/Preference.Wpf.Controls;component/core/errordialoglog.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	void IComponentConnector.Connect(int connectionId, object target)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Expected O, but got Unknown
		switch (connectionId)
		{
		case 1:
			ErrorMessage = (TextBlock)target;
			break;
		case 2:
			((ButtonBase)(RadButton)target).Click += OnShowLog;
			break;
		case 3:
			((Button)target).Click += OnAccept;
			break;
		default:
			_contentLoaded = true;
			break;
		}
	}
}
