using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Threading;
using Preference.Logikal.Api;

namespace Preference.Logikal;

internal class PrefItemPropertiesWindow : Window, IComponentConnector
{
	private delegate void EditPositionDelegate();

	internal TextBlock textBlock1;

	private bool _contentLoaded;

	public InputOfElements InputOfElements { get; set; }

	public InputOfElementsExecuteMode ExecuteMode { get; set; }

	public PrefItemPropertiesWindow()
	{
		ExecuteMode = InputOfElementsExecuteMode.Edit;
		InitializeComponent();
	}

	private void WindowLoaded(object sender, RoutedEventArgs e)
	{
		if (InputOfElements == null)
		{
			throw new InvalidOperationException("InputOfElements cannot be null.");
		}
		base.Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new EditPositionDelegate(EditPosition));
	}

	private void EditPosition()
	{
		bool value = false;
		try
		{
			value = InputOfElements.EditPosition(ExecuteMode);
		}
		finally
		{
			Activate();
			base.DialogResult = value;
		}
	}

	protected override void OnClosing(CancelEventArgs e)
	{
		base.OnClosing(e);
		if (!base.DialogResult.HasValue)
		{
			e.Cancel = true;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/Preference.Logikal;component/prefitempropertieswindow.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	void IComponentConnector.Connect(int connectionId, object target)
	{
		switch (connectionId)
		{
		case 1:
			((PrefItemPropertiesWindow)target).Loaded += WindowLoaded;
			break;
		case 2:
			textBlock1 = (TextBlock)target;
			break;
		default:
			_contentLoaded = true;
			break;
		}
	}
}
