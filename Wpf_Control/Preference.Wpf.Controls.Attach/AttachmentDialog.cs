using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Preference.Wpf.Controls.Attachments.Dialog;

public class AttachmentDialog : Window, IComponentConnector
{
	internal TextBox attachmentName;

	internal CheckBox serializeContent;

	internal Button ok;

	private bool _contentLoaded;

	public string AttachmentName
	{
		get
		{
			return attachmentName.Text;
		}
		set
		{
			attachmentName.Text = value;
		}
	}

	public bool Saved
	{
		get
		{
			if (serializeContent.IsChecked != true)
			{
				return false;
			}
			return true;
		}
		set
		{
			serializeContent.IsChecked = value;
		}
	}

	public AttachmentDialog()
	{
		InitializeComponent();
	}

	private void ok_Click(object sender, RoutedEventArgs e)
	{
		if (Validate())
		{
			base.DialogResult = true;
			Close();
		}
	}

	private bool Validate()
	{
		return !string.IsNullOrWhiteSpace(attachmentName.Text);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/Preference.Wpf.Controls;component/attachments/dialog/attachmentdialog.xaml", UriKind.Relative);
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
			attachmentName = (TextBox)target;
			break;
		case 2:
			serializeContent = (CheckBox)target;
			break;
		case 3:
			ok = (Button)target;
			ok.Click += ok_Click;
			break;
		default:
			_contentLoaded = true;
			break;
		}
	}
}
