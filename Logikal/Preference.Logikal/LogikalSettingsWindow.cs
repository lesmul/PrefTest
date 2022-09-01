using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using Preference.Exceptions;

namespace Preference.Logikal;

public class LogikalSettingsWindow : Window, IComponentConnector
{
	internal StackPanel ButtonsPannel;

	private bool _contentLoaded;

	public CultureInfo Culture { get; set; }

	public LogikalSettingsWindow()
	{
		InitializeComponent();
	}

	private void Window_Loaded(object sender, RoutedEventArgs e)
	{
		try
		{
			LogikalModule.EnsureLoggedIn();
			LogikalModule.LanguageId = Culture.LCID;
			LoadButtons();
		}
		catch (Exception ex)
		{
			MessageBox.Show(PrefException.GetFormattedMessage("Error loading LogiKal settings.", ex), base.Title, MessageBoxButton.OK, MessageBoxImage.Hand);
		}
	}

	private void LoadButtons()
	{
		string text = "";
		int num = LogikalSettingsModule.ProgramsGetTypeCount();
		for (int i = 0; i <= num; i++)
		{
			int nType = LogikalSettingsModule.ProgramsGetType(i);
			text = LogikalSettingsModule.GetSettingName(nType);
			int settingKind = LogikalSettingsModule.GetSettingKind(nType);
			if (!string.IsNullOrEmpty(text) && (settingKind == 0 || settingKind == 2))
			{
				AddButton(text, nType);
			}
		}
	}

	private void AddButton(string strContent, int nType)
	{
		Button button = new Button();
		button.Content = strContent;
		button.Margin = new Thickness(0.0, 2.0, 0.0, 2.0);
		button.Tag = nType;
		button.Click += newButtonSettings_Click;
		ButtonsPannel.Children.Add(button);
	}

	private void newButtonSettings_Click(object sender, RoutedEventArgs e)
	{
		if (sender is Button button)
		{
			try
			{
				LogikalSettingsModule.ExecuteProgram(Convert.ToInt32(button.Tag));
			}
			catch (Exception ex)
			{
				MessageBox.Show(PrefException.GetFormattedMessage("Error loading LogiKal setting: " + button.Content?.ToString() + " ID:" + button.Tag, ex), "", MessageBoxButton.OK, MessageBoxImage.Hand);
			}
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/Preference.Logikal;component/logikalsettingswindow.xaml", UriKind.Relative);
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
			((LogikalSettingsWindow)target).Loaded += Window_Loaded;
			break;
		case 2:
			ButtonsPannel = (StackPanel)target;
			break;
		default:
			_contentLoaded = true;
			break;
		}
	}
}
