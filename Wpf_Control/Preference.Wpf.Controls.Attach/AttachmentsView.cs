using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Markup;
using Preference.Diagnostics;
using Preference.Wpf.Controls.Attachments.Dialog;
using Preference.Wpf.Controls.Attachments.ViewModels;

namespace Preference.Wpf.Controls.Attachments.Views;

public class AttachmentsView : System.Windows.Controls.UserControl, IAttachmentsView, IComponentConnector
{
	private AttachmentsViewModel _ViewModel;

	public static readonly DependencyProperty DocumentIDProperty = DependencyProperty.Register("DocumentID", typeof(Guid), typeof(AttachmentsView), new PropertyMetadata(Guid.Empty, DocumentIDChanged));

	public static readonly DependencyProperty ObjectTypeCodeProperty = DependencyProperty.Register("ObjectTypeCode", typeof(int), typeof(AttachmentsView), new PropertyMetadata(2, ObjectTypeCodeChanged));

	internal ColumnDefinition columnDefinition1;

	internal System.Windows.Controls.TreeView treeView;

	internal GridSplitter gridSplitter;

	internal System.Windows.Controls.ToolBar toolBar;

	internal System.Windows.Controls.DataGrid datagrid;

	internal System.Windows.Controls.MenuItem setIsPublic;

	internal DataGridTemplateColumn colName;

	internal DataGridTemplateColumn colSize;

	internal DataGridTemplateColumn colSaved;

	internal DataGridTemplateColumn colPath;

	internal DataGridTemplateColumn colIsPublic;

	internal DataGridTextColumn colCreationDate;

	internal Grid descriptionPanel;

	private bool _contentLoaded;

	public Guid DocumentID
	{
		get
		{
			return (Guid)GetValue(DocumentIDProperty);
		}
		set
		{
			SetValue(DocumentIDProperty, value);
		}
	}

	public int ObjectTypeCode
	{
		get
		{
			return (int)GetValue(ObjectTypeCodeProperty);
		}
		set
		{
			SetValue(ObjectTypeCodeProperty, value);
		}
	}

	public string ConnectionString
	{
		get
		{
			return (base.DataContext as AttachmentsViewModel).ServiceAgent.ConnectionString;
		}
		set
		{
			(base.DataContext as AttachmentsViewModel).ServiceAgent.ConnectionString = value;
		}
	}

	public int SizeLimitInKb
	{
		get
		{
			return (base.DataContext as AttachmentsViewModel).SizeLimitInKb;
		}
		set
		{
			(base.DataContext as AttachmentsViewModel).SizeLimitInKb = value;
		}
	}

	public AttachmentsView()
	{
		InitializeComponent();
		base.Dispatcher.ShutdownStarted += Dispatcher_ShutdownStarted;
		LoadSettings();
		_ViewModel = new AttachmentsViewModel(new ServiceAgent(), this);
		base.DataContext = _ViewModel;
		treeView.SelectedItemChanged += treeView_SelectedItemChanged;
	}

	private static void DocumentIDChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
	{
		AttachmentsView attachmentsView = sender as AttachmentsView;
		Guid documentID = (Guid)e.NewValue;
		if (attachmentsView != null && !string.IsNullOrEmpty(attachmentsView.ConnectionString))
		{
			attachmentsView.LoadAttachments(documentID, attachmentsView.ObjectTypeCode);
			attachmentsView.SetColumnIsPublicVisibleOrNot();
		}
	}

	private static void ObjectTypeCodeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
	{
		AttachmentsView attachmentsView = sender as AttachmentsView;
		int objectType = (int)e.NewValue;
		if (attachmentsView != null && !string.IsNullOrEmpty(attachmentsView.ConnectionString))
		{
			attachmentsView.LoadAttachments(attachmentsView.DocumentID, objectType);
			attachmentsView.SetColumnIsPublicVisibleOrNot();
		}
	}

	private void LoadSettings()
	{
		try
		{
			AttachmentsViewSettings attachmentsViewSettings = new AttachmentsViewSettings();
			attachmentsViewSettings.Reload();
			columnDefinition1.Width = new GridLength(attachmentsViewSettings.ListViewWidth, GridUnitType.Pixel);
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefGest");
		}
	}

	public void SaveSettings()
	{
		try
		{
			AttachmentsViewSettings attachmentsViewSettings = new AttachmentsViewSettings();
			attachmentsViewSettings.ListViewWidth = columnDefinition1.Width.Value;
			attachmentsViewSettings.Save();
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefGest");
		}
	}

	public void LoadAttachments(Guid documentID, int objectType)
	{
		if (base.DataContext is AttachmentsViewModel)
		{
			(base.DataContext as AttachmentsViewModel).LoadAttachments(documentID, objectType);
		}
		SetColumnIsPublicVisibleOrNot();
	}

	public void HideAttachmentsAndShowText(string message)
	{
		if (base.DataContext is AttachmentsViewModel)
		{
			(base.DataContext as AttachmentsViewModel).HideAttachmentsAndShowText(message);
		}
	}

	private void Dispatcher_ShutdownStarted(object sender, EventArgs e)
	{
		SaveSettings();
	}

	private void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
	{
		if (base.DataContext is AttachmentsViewModel && treeView.SelectedItem is ExternalFile)
		{
			(base.DataContext as AttachmentsViewModel).SelectedFolder = (ExternalFile)treeView.SelectedItem;
		}
	}

	private void ToolBar_Loaded(object sender, RoutedEventArgs e)
	{
		System.Windows.Controls.ToolBar toolBar = sender as System.Windows.Controls.ToolBar;
		if (toolBar.Template.FindName("OverflowGrid", toolBar) is FrameworkElement frameworkElement)
		{
			frameworkElement.Visibility = Visibility.Collapsed;
		}
	}

	public void SetColumnIsPublicVisibleOrNot()
	{
		if (!(datagrid.DataContext is AttachmentsViewModel))
		{
			return;
		}
		AttachmentsViewModel attachmentsViewModel = datagrid.DataContext as AttachmentsViewModel;
		setIsPublic.Visibility = Visibility.Collapsed;
		colIsPublic.Visibility = Visibility.Collapsed;
		descriptionPanel.Visibility = Visibility.Collapsed;
		if (attachmentsViewModel.IsPDEActived)
		{
			setIsPublic.Visibility = Visibility.Visible;
			colIsPublic.Visibility = Visibility.Visible;
			if (attachmentsViewModel.IsSalesDocumentPublic)
			{
				descriptionPanel.Visibility = Visibility.Visible;
			}
		}
	}

	public bool AskForFilePath(out string path)
	{
		path = string.Empty;
		OpenFileDialog openFileDialog = new OpenFileDialog();
		if (openFileDialog.ShowDialog() == DialogResult.OK)
		{
			path = openFileDialog.FileName;
			return true;
		}
		return false;
	}

	public bool AskForAttachmentNameAndTypeOfSerialization(ref string attachmentName, out bool saved)
	{
		saved = false;
		AttachmentDialog attachmentDialog = new AttachmentDialog();
		attachmentDialog.AttachmentName = attachmentName;
		if (attachmentDialog.ShowDialog() == true)
		{
			attachmentName = attachmentDialog.AttachmentName;
			saved = attachmentDialog.Saved;
			return true;
		}
		return false;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/Preference.Wpf.Controls;component/attachments/views/attachmentsview.xaml", UriKind.Relative);
			System.Windows.Application.LoadComponent(this, resourceLocator);
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
			columnDefinition1 = (ColumnDefinition)target;
			break;
		case 2:
			((System.Windows.Controls.ToolBar)target).Loaded += ToolBar_Loaded;
			break;
		case 3:
			treeView = (System.Windows.Controls.TreeView)target;
			break;
		case 4:
			gridSplitter = (GridSplitter)target;
			break;
		case 5:
			toolBar = (System.Windows.Controls.ToolBar)target;
			toolBar.Loaded += ToolBar_Loaded;
			break;
		case 6:
			datagrid = (System.Windows.Controls.DataGrid)target;
			break;
		case 7:
			setIsPublic = (System.Windows.Controls.MenuItem)target;
			break;
		case 8:
			colName = (DataGridTemplateColumn)target;
			break;
		case 9:
			colSize = (DataGridTemplateColumn)target;
			break;
		case 10:
			colSaved = (DataGridTemplateColumn)target;
			break;
		case 11:
			colPath = (DataGridTemplateColumn)target;
			break;
		case 12:
			colIsPublic = (DataGridTemplateColumn)target;
			break;
		case 13:
			colCreationDate = (DataGridTextColumn)target;
			break;
		case 14:
			descriptionPanel = (Grid)target;
			break;
		default:
			_contentLoaded = true;
			break;
		}
	}
}
