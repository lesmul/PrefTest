using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Preference.Wpf.Controls;

public class CustomProgressDialog : Window, IComponentConnector
{
	internal TextBlock ProgressText;

	internal ProgressBar ProgressBar;

	private bool _contentLoaded;

	public ICustomProgress CustomProgress { get; set; }

	private BackgroundWorker BackgroundWorker { get; set; }

	public CustomProgressDialog(string title, ICustomProgress customProgress)
	{
		InitializeComponent();
		base.Title = title;
		Init(customProgress);
	}

	public void Start()
	{
		base.Dispatcher.Invoke(delegate
		{
			BackgroundWorker.RunWorkerAsync();
			ShowDialog();
		});
	}

	protected override void OnClosing(CancelEventArgs e)
	{
		if (CustomProgress.Result == CustomProgressResult.InProgress)
		{
			CustomProgress.Cancel();
			e.Cancel = true;
		}
	}

	private void Init(ICustomProgress customProgress)
	{
		CustomProgress = customProgress;
		CustomProgress.ProgressChanged += OnCustomProgressChanged;
		CustomProgress.Canceled += OnCustomProgressCanceled;
		ProgressBar.Minimum = 0.0;
		ProgressBar.Maximum = CustomProgress.Maximum;
		BackgroundWorker = new BackgroundWorker
		{
			WorkerReportsProgress = true,
			WorkerSupportsCancellation = true
		};
		BackgroundWorker.DoWork += OnWorkerDoWork;
		BackgroundWorker.ProgressChanged += OnWorkerProgressChanged;
		BackgroundWorker.RunWorkerCompleted += OnWorkerRunWorkerCompleted;
	}

	private void OnCancelButtonClick(object sender, RoutedEventArgs e)
	{
		CustomProgress.Cancel();
	}

	private void OnCustomProgressCanceled(object sender, EventArgs e)
	{
		BackgroundWorker.CancelAsync();
	}

	private void OnCustomProgressChanged(object sender, ProgressChangedEventArgs e)
	{
		BackgroundWorker.ReportProgress(e.ProgressPercentage);
	}

	private void OnWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
	{
		Close();
		switch (CustomProgress.Result)
		{
		case CustomProgressResult.Success:
			PrefMessageBox.Inform(CustomProgress.OutputMessage);
			break;
		case CustomProgressResult.Cancel:
			PrefMessageBox.Exclaim(CustomProgress.OutputMessage);
			break;
		case CustomProgressResult.Error:
			PrefMessageBox.Error(CustomProgress.OutputMessage);
			break;
		case CustomProgressResult.InProgress:
			break;
		}
	}

	private void OnWorkerProgressChanged(object sender, ProgressChangedEventArgs e)
	{
		ProgressText.Text = CustomProgress.CurrentText;
		ProgressBar.Value = e.ProgressPercentage;
	}

	private void OnWorkerDoWork(object sender, DoWorkEventArgs e)
	{
		try
		{
			CustomProgress.Run();
		}
		catch (Exception ex)
		{
			CustomProgress.Result = CustomProgressResult.Error;
			CustomProgress.OutputMessage = ex.Message;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/Preference.Wpf.Controls;component/customprogressdialog.xaml", UriKind.Relative);
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
			ProgressText = (TextBlock)target;
			break;
		case 2:
			ProgressBar = (ProgressBar)target;
			break;
		case 3:
			((Button)target).Click += OnCancelButtonClick;
			break;
		default:
			_contentLoaded = true;
			break;
		}
	}
}
