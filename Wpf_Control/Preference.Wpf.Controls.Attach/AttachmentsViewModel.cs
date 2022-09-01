using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Input;
using System.Xml;
using Preference.Diagnostics;
using Preference.Wpf.Controls.Attachments.Views;
using Preference.Wpf.Controls.Core;
using Preference.Wpf.Controls.Core.Commands;
using Preference.Wpf.Controls.Properties;

namespace Preference.Wpf.Controls.Attachments.ViewModels;

public class AttachmentsViewModel : INotifyPropertyChanged
{
	private const string GLOBAL_VARIABLE_PREFERENCE_PDE = "Preference.PDE";

	private const string GLOBAL_VARIABLE_PDE_ATTACHMENT_PUBLIC_TOTAL_SIZE = "PDE.DocLinkPubTotalKB";

	private const string GLOBAL_VARIABLE_PDE_SW_ATTACHMENT_PUBLIC_TOTAL_SIZE = "PDE.SWDocLinkPubTotalKB";

	private Guid _guidDocumentID = Guid.Empty;

	private int _iObjectType = -1;

	private IServiceAgent _ServiceAgent;

	private IAttachmentsView _View;

	private ICommand _AddFolderCommand;

	private ICommand _RenameFolderCommand;

	private ICommand _DeleteFolderCommand;

	private ICommand _AddFileCommand;

	private ICommand _RenameFileCommand;

	private ICommand _RemoveFileCommand;

	private ICommand _ViewFileCommand;

	private ICommand _DownloadFileCommand;

	private ICommand _SyncronizeFileCommand;

	private ICommand _SetIsPublicCommand;

	private ExternalFileList _ExternalFiles;

	private ExternalFileList _Folders;

	private ExternalFileList _CurrentFiles;

	private ExternalFile _SelectedFolder;

	private ExternalFile _SelectedFile;

	private SortedList<string, Guid> _filesOpened;

	private string _strMessage = string.Empty;

	private bool _bAreAttachmentsHidden;

	private int _iSizeLimitInKb = -1;

	private bool _bIsPDEActive;

	private bool _bIsSalesDocumentPublic;

	private bool _bIsSalesDocumentPublicOwned;

	private bool _bCanEdit;

	private int _iTotalSizePublicDocumentsUsedInBytes;

	private int _iTotalSizePublicDocumentsAvailableInBytes;

	private int _iTotalSizePublicDocumentsTotalInBytes;

	public ServiceAgent ServiceAgent
	{
		get
		{
			return (ServiceAgent)_ServiceAgent;
		}
		set
		{
			_ServiceAgent = value;
		}
	}

	public ExternalFile SelectedFolder
	{
		get
		{
			return _SelectedFolder;
		}
		set
		{
			if (_SelectedFolder != value)
			{
				_SelectedFolder = value;
				OnPropertyChanged("SelectedFolder");
				RefreshCurrentFiles();
			}
		}
	}

	public ExternalFile SelectedFile
	{
		get
		{
			return _SelectedFile;
		}
		set
		{
			if (_SelectedFile != value)
			{
				_SelectedFile = value;
				OnPropertyChanged("SelectedFile");
			}
		}
	}

	public CollectionViewSource CurrentFilesView { get; set; }

	public ExternalFileList CurrentFiles
	{
		get
		{
			return _CurrentFiles;
		}
		set
		{
			if (_CurrentFiles != value)
			{
				_CurrentFiles = value;
				OnPropertyChanged("CurrentFiles");
			}
		}
	}

	public ExternalFileList Folders
	{
		get
		{
			return _Folders;
		}
		set
		{
			if (_Folders != value)
			{
				_Folders = value;
				OnPropertyChanged("Folders");
			}
		}
	}

	public ExternalFileList ExternalFiles
	{
		get
		{
			return _ExternalFiles;
		}
		set
		{
			if (_ExternalFiles != value)
			{
				_ExternalFiles = value;
				OnPropertyChanged("ExternalFiles");
			}
		}
	}

	public int SizeLimitInKb
	{
		get
		{
			return _iSizeLimitInKb;
		}
		set
		{
			_iSizeLimitInKb = value;
		}
	}

	public bool CanEdit
	{
		get
		{
			return _bCanEdit;
		}
		set
		{
			if (_bCanEdit != value)
			{
				_bCanEdit = value;
				OnPropertyChanged("CanEdit");
			}
		}
	}

	public bool IsSalesDocumentPublicOwned
	{
		get
		{
			return _bIsSalesDocumentPublicOwned;
		}
		set
		{
			if (_bIsSalesDocumentPublicOwned != value)
			{
				_bIsSalesDocumentPublicOwned = value;
				OnPropertyChanged("IsSalesDocumentPublicOwned");
			}
		}
	}

	public bool IsSalesDocumentPublic
	{
		get
		{
			return _bIsSalesDocumentPublic;
		}
		set
		{
			if (_bIsSalesDocumentPublic != value)
			{
				_bIsSalesDocumentPublic = value;
				OnPropertyChanged("IsSalesDocumentPublic");
			}
		}
	}

	public int TotalSizePublicDocumentsUsedInBytes
	{
		get
		{
			return _iTotalSizePublicDocumentsUsedInBytes;
		}
		set
		{
			if (_iTotalSizePublicDocumentsUsedInBytes != value)
			{
				_iTotalSizePublicDocumentsUsedInBytes = value;
				OnPropertyChanged("TotalSizePublicDocumentsUsedInBytes");
			}
		}
	}

	public int TotalSizePublicDocumentsAvailableInBytes
	{
		get
		{
			return _iTotalSizePublicDocumentsAvailableInBytes;
		}
		set
		{
			if (_iTotalSizePublicDocumentsAvailableInBytes != value)
			{
				_iTotalSizePublicDocumentsAvailableInBytes = value;
				OnPropertyChanged("TotalSizePublicDocumentsAvailableInBytes");
			}
		}
	}

	public int TotalSizePublicDocumentsTotalInBytes
	{
		get
		{
			return _iTotalSizePublicDocumentsTotalInBytes;
		}
		set
		{
			if (_iTotalSizePublicDocumentsTotalInBytes != value)
			{
				_iTotalSizePublicDocumentsTotalInBytes = value;
				OnPropertyChanged("TotalSizePublicDocumentsTotalInBytes");
			}
		}
	}

	public string Message
	{
		get
		{
			return _strMessage;
		}
		set
		{
			if (_strMessage != value)
			{
				_strMessage = value;
				OnPropertyChanged("Message");
			}
		}
	}

	public bool AreAttachmentsHidden
	{
		get
		{
			return _bAreAttachmentsHidden;
		}
		set
		{
			if (_bAreAttachmentsHidden != value)
			{
				_bAreAttachmentsHidden = value;
				OnPropertyChanged("AreAttachmentsHidden");
			}
		}
	}

	public bool IsPDEActived => _bIsPDEActive;

	public ICommand AddFolderCommand
	{
		get
		{
			if (_AddFolderCommand == null)
			{
				_AddFolderCommand = new RelayCommand(ExecuteAddFolderCommand, CanExecuteAddFolderCommand);
			}
			return _AddFolderCommand;
		}
	}

	public ICommand RenameFolderCommand
	{
		get
		{
			if (_RenameFolderCommand == null)
			{
				_RenameFolderCommand = new RelayCommand(ExecuteRenameFolderCommand, CanExecuteRenameFolderCommand);
			}
			return _RenameFolderCommand;
		}
	}

	public ICommand DeleteFolderCommand
	{
		get
		{
			if (_DeleteFolderCommand == null)
			{
				_DeleteFolderCommand = new RelayCommand(ExecuteDeleteFolderCommand, CanExecuteDeleteFolderCommand);
			}
			return _DeleteFolderCommand;
		}
	}

	public ICommand AddFileCommand
	{
		get
		{
			if (_AddFileCommand == null)
			{
				_AddFileCommand = new RelayCommand(ExecuteAddFileCommand, CanExecuteAddFileCommand);
			}
			return _AddFileCommand;
		}
	}

	public ICommand RenameFileCommand
	{
		get
		{
			if (_RenameFileCommand == null)
			{
				_RenameFileCommand = new RelayCommand(ExecuteRenameFileCommand, CanExecuteRenameFileCommand);
			}
			return _RenameFileCommand;
		}
	}

	public ICommand RemoveFileCommand
	{
		get
		{
			if (_RemoveFileCommand == null)
			{
				_RemoveFileCommand = new RelayCommand(ExecuteRemoveFileCommand, CanExecuteRemoveFileCommand);
			}
			return _RemoveFileCommand;
		}
	}

	public ICommand ViewFileCommand
	{
		get
		{
			if (_ViewFileCommand == null)
			{
				_ViewFileCommand = new RelayCommand(ExecuteViewFileCommand, CanExecuteViewFileCommand);
			}
			return _ViewFileCommand;
		}
	}

	public ICommand DownloadFileCommand
	{
		get
		{
			if (_DownloadFileCommand == null)
			{
				_DownloadFileCommand = new RelayCommand(ExecuteDownloadFileCommand, CanExecuteDownloadFileCommand);
			}
			return _DownloadFileCommand;
		}
	}

	public ICommand SyncronizeFileCommand
	{
		get
		{
			if (_SyncronizeFileCommand == null)
			{
				_SyncronizeFileCommand = new RelayCommand(ExecuteSyncronizeFileCommand, CanExecuteSyncronizeFileCommand);
			}
			return _SyncronizeFileCommand;
		}
	}

	public ICommand SetIsPublicCommand
	{
		get
		{
			if (_SetIsPublicCommand == null)
			{
				_SetIsPublicCommand = new RelayCommand(SetIsPublicCommandExecute, CanSetIsPublicCommandExecute);
			}
			return _SetIsPublicCommand;
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	public AttachmentsViewModel(IServiceAgent serviceAgent, IAttachmentsView view)
	{
		_ServiceAgent = serviceAgent;
		_View = view;
		ExternalFiles = new ExternalFileList();
		Folders = new ExternalFileList();
		CurrentFiles = new ExternalFileList();
		CurrentFilesView = new CollectionViewSource();
		CurrentFilesView.Source = CurrentFiles;
		CurrentFilesView.SortDescriptions.Add(new SortDescription("IsFolder", ListSortDirection.Descending));
		CurrentFilesView.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
		_filesOpened = new SortedList<string, Guid>();
	}

	public void RefreshCurrentFiles()
	{
		CurrentFiles = null;
		if (_SelectedFolder != null)
		{
			ExternalFile selectedFolderInExternalFiles = GetSelectedFolderInExternalFiles(ExternalFiles);
			if (selectedFolderInExternalFiles != null)
			{
				CurrentFiles = selectedFolderInExternalFiles.ExternalFiles;
				CurrentFilesView.Source = CurrentFiles;
				CurrentFilesView.SortDescriptions.Add(new SortDescription("IsFolder", ListSortDirection.Descending));
				CurrentFilesView.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
			}
		}
	}

	public void RefreshFileSizeLimitsSummary()
	{
		TotalSizePublicDocumentsAvailableInBytes = 0;
		TotalSizePublicDocumentsUsedInBytes = 0;
		foreach (ExternalFile externalFile in ExternalFiles)
		{
			RefreshFileSizeLimitsSummary(externalFile);
		}
		TotalSizePublicDocumentsAvailableInBytes = TotalSizePublicDocumentsTotalInBytes - TotalSizePublicDocumentsUsedInBytes;
	}

	public void RefreshFileSizeLimitsSummary(ExternalFile item)
	{
		if (item.IsPublic && !item.IsFolder)
		{
			TotalSizePublicDocumentsUsedInBytes += item.FileSize;
		}
		foreach (ExternalFile externalFile in item.ExternalFiles)
		{
			RefreshFileSizeLimitsSummary(externalFile);
		}
	}

	public ExternalFile GetSelectedFolderInExternalFiles(ExternalFileList files)
	{
		ExternalFile externalFile = null;
		foreach (ExternalFile file in files)
		{
			if (file.RowId == SelectedFolder.RowId)
			{
				return file;
			}
			externalFile = GetSelectedFolderInExternalFiles(file.ExternalFiles);
			if (externalFile != null)
			{
				return externalFile;
			}
		}
		return externalFile;
	}

	public void LoadAttachments(Guid documentID, int objectType)
	{
		AreAttachmentsHidden = false;
		if (_ServiceAgent != null)
		{
			_guidDocumentID = documentID;
			_iObjectType = objectType;
			LoadAttachments(documentID);
		}
	}

	public void HideAttachmentsAndShowText(string message)
	{
		AreAttachmentsHidden = true;
		Message = message;
	}

	private void LoadAttachments(Guid guidDocID)
	{
		string xml = string.Empty;
		_ServiceAgent.GetAttachments(guidDocID, ref xml);
		CanEdit = true;
		TotalSizePublicDocumentsTotalInBytes = int.MaxValue;
		_bIsPDEActive = false;
		object globalVariableValue = _ServiceAgent.GetGlobalVariableValue(1, "Preference.PDE");
		if (globalVariableValue != null)
		{
			int result = 0;
			if (int.TryParse(Convert.ToString(globalVariableValue), out result))
			{
				_bIsPDEActive = result == 1;
			}
		}
		if (_bIsPDEActive)
		{
			IsSalesDocumentPublic = _ServiceAgent.SalesIsPublic(_guidDocumentID, _iObjectType);
			if (IsSalesDocumentPublic)
			{
				IsSalesDocumentPublicOwned = _ServiceAgent.SalesIsPublicDocumentOwned(_guidDocumentID, _iObjectType);
				if (!IsSalesDocumentPublicOwned)
				{
					CanEdit = false;
				}
			}
			if (IsSalesDocumentPublic)
			{
				int number = 0;
				int version = 0;
				if (_ServiceAgent.GetNumberAndVersion(guidDocID, _iObjectType, out number, out version))
				{
					WarrantyTypes warrantyType = _ServiceAgent.GetWarrantyType(number, version);
					if (warrantyType == WarrantyTypes.Service || warrantyType == WarrantyTypes.Warranty)
					{
						TotalSizePublicDocumentsTotalInBytes = 6291456;
						object globalVariableValue2 = _ServiceAgent.GetGlobalVariableValue(1, "PDE.SWDocLinkPubTotalKB");
						if (globalVariableValue2 != null)
						{
							string s = Convert.ToString(globalVariableValue2);
							int result2 = 0;
							if (int.TryParse(s, out result2))
							{
								TotalSizePublicDocumentsTotalInBytes = result2 * 1024;
							}
						}
					}
					else
					{
						TotalSizePublicDocumentsTotalInBytes = 3145728;
						object globalVariableValue3 = _ServiceAgent.GetGlobalVariableValue(1, "PDE.DocLinkPubTotalKB");
						if (globalVariableValue3 != null)
						{
							string s2 = Convert.ToString(globalVariableValue3);
							int result3 = 0;
							if (int.TryParse(s2, out result3))
							{
								TotalSizePublicDocumentsTotalInBytes = result3 * 1024;
							}
						}
					}
				}
			}
		}
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(xml);
		XmlNode documentElement = xmlDocument.DocumentElement;
		ExternalFiles = new ExternalFileList();
		ExternalFile externalFile = new ExternalFile();
		externalFile.Name = _ServiceAgent.GetDocumentName(_guidDocumentID, _iObjectType);
		externalFile.IsFolder = true;
		externalFile.IsRootDocument = true;
		ExternalFiles.Add(externalFile);
		LoadAttachments(documentElement, externalFile.ExternalFiles);
		OnPropertyChanged("ExternalFiles");
		Folders.Clear();
		LoadFolders(ExternalFiles, Folders);
		if (Folders.Count > 0)
		{
			Folders[0].IsSelected = true;
			SelectedFolder = Folders[0];
		}
		RefreshCurrentFiles();
		RefreshFileSizeLimitsSummary();
		CommandManager.InvalidateRequerySuggested();
		Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;
	}

	private void LoadAttachments(XmlNode node, ExternalFileList list)
	{
		string xpath = "./child::Attachment";
		XmlNodeList xmlNodeList = node.SelectNodes(xpath);
		if (xmlNodeList.Count <= 0)
		{
			return;
		}
		foreach (XmlNode item in xmlNodeList)
		{
			ExternalFile externalFile = new ExternalFile();
			externalFile.RowId = new Guid(item.Attributes["RowId"].Value);
			if (node.Attributes.GetNamedItem("RowId") != null)
			{
				externalFile.ParentRowId = new Guid(node.Attributes["RowId"].Value);
			}
			externalFile.DocId = new Guid(item.Attributes["DocId"].Value);
			externalFile.Position = Convert.ToInt32(item.Attributes["Position"].Value);
			externalFile.Name = item.Attributes["Name"].Value;
			externalFile.FilePath = item.Attributes["FilePath"].Value;
			externalFile.Saved = item.Attributes["Saved"].Value == "1";
			externalFile.CreationDate = Convert.ToDateTime(item.Attributes["CreationDate"].Value);
			externalFile.IsFolder = item.Attributes["NodeType"].Value == "Folder";
			externalFile.IsPublic = item.Attributes["IsPublic"].Value == "1";
			if (item.Attributes["FileSize"] != null)
			{
				externalFile.FileSize = Convert.ToInt32(item.Attributes["FileSize"].Value);
			}
			list.Add(externalFile);
			LoadAttachments(item, externalFile.ExternalFiles);
		}
	}

	private void LoadFolders(ExternalFileList files, ExternalFileList folders)
	{
		foreach (ExternalFile file in files)
		{
			if (file.IsFolder)
			{
				ExternalFile externalFile = (ExternalFile)file.Clone();
				folders.Add(externalFile);
				LoadFolders(file.ExternalFiles, externalFile.ExternalFiles);
			}
		}
	}

	private void LoadFolders(ExternalFile parent, ExternalFileList children)
	{
		foreach (ExternalFile child in children)
		{
			if (child.IsFolder)
			{
				ExternalFile externalFile = (ExternalFile)child.Clone();
				Folders.Add(externalFile);
				LoadFolders(externalFile, child.ExternalFiles);
			}
		}
	}

	protected virtual void OnPropertyChanged(string propertyName)
	{
		if (this.PropertyChanged != null)
		{
			this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
	}

	private void ExecuteAddFolderCommand(object o)
	{
		try
		{
			if (!(_guidDocumentID != Guid.Empty) || SelectedFolder == null)
			{
				return;
			}
			string text = Dialogs.InputBox(Resources.StringNewFolder, Resources.StringNewFolder, Resources.StringNewFolder, 0, 0);
			if (!(text != string.Empty))
			{
				return;
			}
			if (_ServiceAgent.ExistsAttachment(text, _guidDocumentID, SelectedFolder.RowId))
			{
				Dialogs.Error(Resources.StringFolderExists);
				return;
			}
			Guid newRowID = Guid.Empty;
			if (_ServiceAgent.AddAttachmentFolder(text, SelectedFolder.RowId, _guidDocumentID, ref newRowID) == 0)
			{
				ExternalFile externalFile = new ExternalFile();
				externalFile.RowId = newRowID;
				externalFile.Name = text;
				externalFile.IsFolder = true;
				SelectedFolder.ExternalFiles.Add(externalFile);
				OnPropertyChanged("Folders");
				ExternalFile selectedFolderInExternalFiles = GetSelectedFolderInExternalFiles(ExternalFiles);
				ExternalFile item = (ExternalFile)externalFile.Clone();
				selectedFolderInExternalFiles.ExternalFiles.Add(item);
				RefreshCurrentFiles();
			}
		}
		catch (Exception ex)
		{
			Dialogs.Error(ex.Message);
		}
	}

	private bool CanExecuteAddFolderCommand(object o)
	{
		if (CanEdit)
		{
			return SelectedFolder != null;
		}
		return false;
	}

	private void ExecuteRenameFolderCommand(object o)
	{
		try
		{
			string text = Dialogs.InputBox(Resources.StringRename, Resources.StringRename, SelectedFolder.Name, 0, 0);
			if (text != string.Empty && text != SelectedFolder.Name)
			{
				_ServiceAgent.RenameAttachment(SelectedFolder.RowId, text);
				SelectedFolder.Name = text;
				ExternalFile selectedFolderInExternalFiles = GetSelectedFolderInExternalFiles(ExternalFiles);
				selectedFolderInExternalFiles.Name = text;
			}
		}
		catch (Exception ex)
		{
			Dialogs.Error(ex.Message);
		}
	}

	private bool CanExecuteRenameFolderCommand(object o)
	{
		if (CanEdit && SelectedFolder != null)
		{
			return SelectedFolder.RowId != Guid.Empty;
		}
		return false;
	}

	private void ExecuteDeleteFolderCommand(object o)
	{
		try
		{
			Guid rowId = SelectedFolder.RowId;
			string messageBoxText = string.Format(Resources.StringConfirmDeleteFolderQuestion, SelectedFolder.Name);
			string stringConfirmDeleteFolder = Resources.StringConfirmDeleteFolder;
			if (Dialogs.Confirm(messageBoxText, stringConfirmDeleteFolder) == MessageBoxResult.Yes)
			{
				_ServiceAgent.RemoveAttachment(SelectedFolder.RowId);
				ExternalFile selectedFolderInExternalFiles = GetSelectedFolderInExternalFiles(ExternalFiles);
				ExternalFile parentFolder = GetParentFolder(selectedFolderInExternalFiles, ExternalFiles);
				parentFolder.ExternalFiles.Remove(selectedFolderInExternalFiles);
				RefreshCurrentFiles();
				RefreshFileSizeLimitsSummary();
				parentFolder = GetParentFolder(SelectedFolder, Folders);
				parentFolder.ExternalFiles.Remove(SelectedFolder);
			}
		}
		catch (Exception ex)
		{
			Dialogs.Error(ex.Message);
		}
	}

	private ExternalFile GetParentFolder(ExternalFile child, ExternalFileList folders)
	{
		ExternalFile externalFile = null;
		foreach (ExternalFile folder in folders)
		{
			if (child.ParentRowId == folder.RowId)
			{
				return folder;
			}
			externalFile = GetParentFolder(child, folder.ExternalFiles);
			if (externalFile != null)
			{
				return externalFile;
			}
		}
		return externalFile;
	}

	private bool CanExecuteDeleteFolderCommand(object o)
	{
		if (CanEdit && SelectedFolder != null)
		{
			return SelectedFolder.RowId != Guid.Empty;
		}
		return false;
	}

	private void ExecuteAddFileCommand(object o)
	{
		try
		{
			string path = string.Empty;
			if (!_View.AskForFilePath(out path))
			{
				return;
			}
			string fileName = Path.GetFileName(path);
			string extension = Path.GetExtension(fileName);
			string attachmentName = fileName.Substring(0, fileName.Length - extension.Length);
			bool saved = false;
			if (!_View.AskForAttachmentNameAndTypeOfSerialization(ref attachmentName, out saved))
			{
				return;
			}
			if (_ServiceAgent.ExistsAttachment(attachmentName, _guidDocumentID, SelectedFolder.RowId))
			{
				Dialogs.Error(Resources.StringDocumentExists);
			}
			else if (saved)
			{
				byte[] buffer = FileTools.GetBuffer(path);
				byte[] zippedBuffer = FileTools.GetZippedBuffer(buffer);
				int length = zippedBuffer.GetLength(0);
				if (_iSizeLimitInKb == -1 && IsSalesDocumentPublic)
				{
					_iSizeLimitInKb = 3145728;
				}
				if (_iSizeLimitInKb > -1 && length > _iSizeLimitInKb)
				{
					Dialogs.Error(Resources.ErrorSizeLimitInKbMaximun3MB);
					return;
				}
				Guid newRowID = Guid.Empty;
				_ServiceAgent.AddAttachmentBlob(attachmentName, path, extension, SelectedFolder.RowId, _guidDocumentID, _iObjectType, buffer, ref newRowID);
				ExternalFile externalFile = new ExternalFile();
				externalFile.RowId = newRowID;
				externalFile.ParentRowId = SelectedFolder.RowId;
				externalFile.IsFolder = false;
				externalFile.CreationDate = DateTime.Now;
				externalFile.DocId = _guidDocumentID;
				externalFile.FileType = "File";
				externalFile.Name = attachmentName;
				externalFile.FilePath = path;
				externalFile.FileSize = length;
				externalFile.Saved = saved;
				ExternalFile selectedFolderInExternalFiles = GetSelectedFolderInExternalFiles(ExternalFiles);
				selectedFolderInExternalFiles.ExternalFiles.Add(externalFile);
				RefreshCurrentFiles();
				SelectedFile = externalFile;
				if (SetIsPublicCommand.CanExecute(null))
				{
					SetIsPublicCommand.Execute(null);
				}
			}
			else
			{
				Guid newRowID2 = Guid.Empty;
				_ServiceAgent.AddAttachmentPath(attachmentName, path, extension, SelectedFolder.RowId, _guidDocumentID, _iObjectType, ref newRowID2);
				ExternalFile externalFile2 = new ExternalFile();
				externalFile2.RowId = newRowID2;
				externalFile2.ParentRowId = SelectedFolder.RowId;
				externalFile2.IsFolder = false;
				externalFile2.CreationDate = DateTime.Now;
				externalFile2.DocId = _guidDocumentID;
				externalFile2.FileType = "File";
				externalFile2.Name = attachmentName;
				externalFile2.FilePath = path;
				externalFile2.FileSize = 0;
				externalFile2.Saved = saved;
				ExternalFile selectedFolderInExternalFiles2 = GetSelectedFolderInExternalFiles(ExternalFiles);
				selectedFolderInExternalFiles2.ExternalFiles.Add(externalFile2);
				RefreshCurrentFiles();
				SelectedFile = externalFile2;
				if (SetIsPublicCommand.CanExecute(null))
				{
					SetIsPublicCommand.Execute(null);
				}
			}
		}
		catch (Exception ex)
		{
			Dialogs.Error(ex.Message);
		}
	}

	private bool CanExecuteAddFileCommand(object o)
	{
		if (CanEdit)
		{
			return SelectedFolder != null;
		}
		return false;
	}

	private void ExecuteRenameFileCommand(object o)
	{
		try
		{
			string name = SelectedFile.Name;
			string fileName = Path.GetTempPath() + name;
			FileInfo fileInfo = new FileInfo(fileName);
			string extension = fileInfo.Extension;
			name = name.Substring(0, name.Length - extension.Length);
			string text = Dialogs.InputBox(Resources.StringRename, Resources.StringRename, name, 0, 0);
			if (text != string.Empty && text != name)
			{
				if (_ServiceAgent.ExistsAttachment(text, _guidDocumentID, SelectedFolder.RowId))
				{
					Dialogs.Error(Resources.StringDocumentExists);
					return;
				}
				text += extension;
				_ServiceAgent.RenameAttachment(SelectedFile.RowId, text);
				SelectedFile.Name = text;
			}
		}
		catch (Exception ex)
		{
			Dialogs.Error(ex.Message);
		}
	}

	private bool CanExecuteRenameFileCommand(object o)
	{
		if (CanEdit && SelectedFolder != null)
		{
			if (SelectedFile != null)
			{
				return !SelectedFile.IsFolder;
			}
			return false;
		}
		return false;
	}

	private void ExecuteRemoveFileCommand(object o)
	{
		try
		{
			string messageBoxText = string.Format(Resources.StringConfirmDeleteQuestion, SelectedFile.Name);
			string stringConfirmDelete = Resources.StringConfirmDelete;
			if (Dialogs.Confirm(messageBoxText, stringConfirmDelete) == MessageBoxResult.Yes)
			{
				_ServiceAgent.RemoveAttachment(SelectedFile.RowId);
				ExternalFile selectedFolderInExternalFiles = GetSelectedFolderInExternalFiles(ExternalFiles);
				selectedFolderInExternalFiles.ExternalFiles.Remove(SelectedFile);
				RefreshCurrentFiles();
				RefreshFileSizeLimitsSummary();
			}
		}
		catch (Exception ex)
		{
			Dialogs.Error(ex.Message);
		}
	}

	private bool CanExecuteRemoveFileCommand(object o)
	{
		if (CanEdit && SelectedFolder != null && SelectedFile != null)
		{
			return !SelectedFile.IsFolder;
		}
		return false;
	}

	private void ExecuteViewFileCommand(object o)
	{
		try
		{
			if (SelectedFile.Saved)
			{
				byte[] fileBlob = null;
				string name = string.Empty;
				string filePath = string.Empty;
				string fileExtension = string.Empty;
				_ServiceAgent.GetAttachment(SelectedFile.RowId, ref fileBlob, ref name, ref filePath, ref fileExtension);
				string fileName = Path.GetFileName(filePath);
				fileName = $"{Path.GetTempPath()}{fileName}";
				if (!_filesOpened.ContainsKey(fileName))
				{
					_filesOpened.Add(fileName, SelectedFile.RowId);
				}
				FileTools.CreateFile(fileName, fileBlob);
				Process process = new Process();
				process.EnableRaisingEvents = true;
				process.Exited += ViewFile_Exited;
				process.StartInfo = new ProcessStartInfo(fileName);
				process.Start();
			}
			else
			{
				Process process2 = new Process();
				process2.EnableRaisingEvents = true;
				process2.StartInfo = new ProcessStartInfo(SelectedFile.FilePath);
				process2.Start();
			}
		}
		catch (Exception ex)
		{
			Dialogs.Error(ex.Message);
		}
	}

	private void ViewFile_Exited(object sender, EventArgs e)
	{
		if (CanEdit)
		{
			SaveFileChanges();
		}
	}

	private void SaveFileChanges()
	{
		try
		{
			foreach (KeyValuePair<string, Guid> item in _filesOpened)
			{
				string key = item.Key;
				byte[] fileBlob = null;
				string name = string.Empty;
				string filePath = string.Empty;
				string fileExtension = string.Empty;
				_ServiceAgent.GetAttachment(item.Value, ref fileBlob, ref name, ref filePath, ref fileExtension);
				string text = name;
				text = text.Replace(" ", "_");
				FileInfo fileInfo = new FileInfo(key);
				string extension = fileInfo.Extension;
				string text2 = Path.GetTempPath() + text + ".copy" + extension;
				FileTools.CreateFile(text2, fileBlob);
				if (!FileTools.CompareFiles(key, text2))
				{
					string stringConfirm = Resources.StringConfirm;
					string messageBoxText = string.Format(Resources.WarningFileChangesPending, name);
					if (Dialogs.Confirm(messageBoxText, stringConfirm) == MessageBoxResult.Yes)
					{
						byte[] buffer = FileTools.GetBuffer(key);
						_ServiceAgent.InsertAttachmentBlob(buffer, item.Value);
					}
				}
			}
			_filesOpened.Clear();
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "Preference.Attachments.ControlLibrary");
			throw;
		}
	}

	private bool CanExecuteViewFileCommand(object o)
	{
		if (SelectedFile != null)
		{
			return !SelectedFile.IsFolder;
		}
		return false;
	}

	private void ExecuteDownloadFileCommand(object o)
	{
		try
		{
			byte[] fileBlob = null;
			string name = string.Empty;
			string filePath = string.Empty;
			string fileExtension = string.Empty;
			_ServiceAgent.GetAttachment(SelectedFile.RowId, ref fileBlob, ref name, ref filePath, ref fileExtension);
			string extension = Path.GetExtension(name);
			if (string.IsNullOrEmpty(extension))
			{
				extension = Path.GetExtension(fileExtension);
				if (string.IsNullOrEmpty(extension))
				{
					extension = Path.GetExtension(filePath);
				}
				if (!string.IsNullOrEmpty(extension))
				{
					name = $"{name}{extension}";
				}
			}
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.FileName = name;
			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				string fileName = saveFileDialog.FileName;
				FileStream fileStream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
				fileStream.Write(fileBlob, 0, fileBlob.Length);
				fileStream.Close();
			}
		}
		catch (Exception ex)
		{
			Dialogs.Error(ex.Message);
		}
	}

	private bool CanExecuteDownloadFileCommand(object o)
	{
		if (SelectedFile != null && !SelectedFile.IsFolder)
		{
			return SelectedFile.Saved;
		}
		return false;
	}

	private void SetIsPublicCommandExecute(object o)
	{
		bool? isPublic = !SelectedFile.IsPublic;
		Guid attachmentID = SelectedFile.RowId;
		_ServiceAgent.SetAttachmentPublic(ref attachmentID, isPublic);
		SelectedFile.RowId = attachmentID;
		SelectedFile.IsPublic = isPublic.Value;
		RefreshFileSizeLimitsSummary();
	}

	private bool CanSetIsPublicCommandExecute(object o)
	{
		if (CanEdit && SelectedFile != null && !SelectedFile.IsFolder)
		{
			if (SelectedFile.IsPublic)
			{
				return true;
			}
			if (SelectedFile.Saved)
			{
				if (SelectedFile.FileSize + TotalSizePublicDocumentsUsedInBytes > TotalSizePublicDocumentsTotalInBytes)
				{
					return false;
				}
				return true;
			}
		}
		return false;
	}

	private void ExecuteSyncronizeFileCommand(object o)
	{
		try
		{
			SaveFileChanges();
		}
		catch (Exception ex)
		{
			Dialogs.Error(ex.Message);
		}
	}

	private bool CanExecuteSyncronizeFileCommand(object o)
	{
		if (CanEdit)
		{
			return _filesOpened.Count > 0;
		}
		return false;
	}
}
