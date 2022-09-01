#define TRACE
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using Microsoft.Win32;
using Preference.Wpf.Controls.Properties;

namespace Preference.Wpf.Controls;

public class QuestionWindow : Window, IComponentConnector
{
	private Cursor _overrideCursor;

	private string _strRegistrySubKey;

	private string _strRegistryVariable;

	internal TextBlock QuestionTextBlock;

	internal CheckBox DontAskCheckbox;

	internal Button ButtonYes;

	internal Button ButtonNo;

	private bool _contentLoaded;

	public string QuestionText
	{
		get
		{
			return QuestionTextBlock.Text;
		}
		set
		{
			QuestionTextBlock.Text = value;
		}
	}

	public string RegistrySubKey
	{
		get
		{
			return _strRegistrySubKey;
		}
		set
		{
			_strRegistrySubKey = value;
		}
	}

	public string RegistryVariable
	{
		get
		{
			return _strRegistryVariable;
		}
		set
		{
			_strRegistryVariable = value;
		}
	}

	private bool DoNotAsk => DontAskCheckbox.IsChecked.GetValueOrDefault(false);

	public QuestionWindow()
	{
		InitializeComponent();
		InitTranslation();
		_overrideCursor = Mouse.OverrideCursor;
		Mouse.OverrideCursor = null;
	}

	public new bool? ShowDialog()
	{
		if (GetAllowShowDialog())
		{
			return base.ShowDialog();
		}
		return true;
	}

	private bool GetAllowShowDialog()
	{
		if (string.IsNullOrEmpty(RegistrySubKey) || string.IsNullOrEmpty(RegistryVariable))
		{
			return true;
		}
		try
		{
			RegistryKey registryKey = Registry.CurrentUser.CreateSubKey(RegistrySubKey);
			object value = registryKey.GetValue(RegistryVariable);
			if (value == null)
			{
				return true;
			}
			return value.ToString() == "1";
		}
		catch (SecurityException ex)
		{
			Trace.WriteLine(ex.Message);
		}
		catch (ArgumentException ex2)
		{
			Trace.WriteLine(ex2.Message);
		}
		return true;
	}

	private void InitTranslation()
	{
		DontAskCheckbox.Content = Preference.Wpf.Controls.Properties.Resources.StringDontAsk;
		ButtonYes.Content = Preference.Wpf.Controls.Properties.Resources.StringButtonYes;
		ButtonNo.Content = Preference.Wpf.Controls.Properties.Resources.StringButtonNo;
	}

	private void OnButtonYesClick(object sender, RoutedEventArgs e)
	{
		if (DoNotAsk)
		{
			SetAllowShowDialog(bAllow: false);
		}
		base.DialogResult = true;
		Close();
	}

	private void SetAllowShowDialog(bool bAllow)
	{
		try
		{
			RegistryKey registryKey = Registry.CurrentUser.CreateSubKey(RegistrySubKey);
			registryKey.SetValue(RegistryVariable, bAllow ? "1" : "0");
		}
		catch (SecurityException ex)
		{
			Trace.WriteLine(ex.Message);
		}
		catch (ArgumentException ex2)
		{
			Trace.WriteLine(ex2.Message);
		}
	}

	protected override void OnClosed(EventArgs e)
	{
		Mouse.OverrideCursor = _overrideCursor;
		base.OnClosed(e);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/Preference.Wpf.Controls;component/questionwindow.xaml", UriKind.Relative);
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
			QuestionTextBlock = (TextBlock)target;
			break;
		case 2:
			DontAskCheckbox = (CheckBox)target;
			break;
		case 3:
			ButtonYes = (Button)target;
			ButtonYes.Click += OnButtonYesClick;
			break;
		case 4:
			ButtonNo = (Button)target;
			break;
		default:
			_contentLoaded = true;
			break;
		}
	}
}
