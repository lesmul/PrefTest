using System;
using System.ComponentModel;

namespace Preference.Wpf.Controls.Attachments.ViewModels;

public class ExternalFile : ICloneable, INotifyPropertyChanged
{
	private Guid _RowId;

	private Guid _DocId;

	private int _ObjectTypeCode;

	private int _Position;

	private string _Name;

	private object[] _FileBlob;

	private int _FileSize;

	private string _FilePath;

	private string _FileType;

	private bool _Saved;

	private Guid _ParentRowId;

	private bool _IsFolder;

	private DateTime _CreationDate;

	private bool _IsExpanded;

	private bool _IsSelected;

	private ExternalFileList _ExternalFiles;

	private bool _IsRootDocument;

	private bool _bIsPublic;

	public Guid RowId
	{
		get
		{
			return _RowId;
		}
		set
		{
			if (_RowId != value)
			{
				_RowId = value;
				OnPropertyChanged("RowId");
			}
		}
	}

	public Guid DocId
	{
		get
		{
			return _DocId;
		}
		set
		{
			if (_DocId != value)
			{
				_DocId = value;
				OnPropertyChanged("DocId");
			}
		}
	}

	public int ObjectTypeCode
	{
		get
		{
			return _ObjectTypeCode;
		}
		set
		{
			if (_ObjectTypeCode != value)
			{
				_ObjectTypeCode = value;
				OnPropertyChanged("ObjectTypeCode");
			}
		}
	}

	public int Position
	{
		get
		{
			return _Position;
		}
		set
		{
			if (_Position != value)
			{
				_Position = value;
				OnPropertyChanged("Position");
			}
		}
	}

	public string Name
	{
		get
		{
			return _Name;
		}
		set
		{
			if (_Name != value)
			{
				_Name = value;
				OnPropertyChanged("Name");
			}
		}
	}

	public object[] FileBlob
	{
		get
		{
			return _FileBlob;
		}
		set
		{
			if (_FileBlob != value)
			{
				_FileBlob = value;
				OnPropertyChanged("FileBlob");
			}
		}
	}

	public int FileSize
	{
		get
		{
			return _FileSize;
		}
		set
		{
			if (_FileSize != value)
			{
				_FileSize = value;
				OnPropertyChanged("FileSize");
			}
		}
	}

	public string FilePath
	{
		get
		{
			return _FilePath;
		}
		set
		{
			if (_FilePath != value)
			{
				_FilePath = value;
				OnPropertyChanged("FilePath");
			}
		}
	}

	public string FileType
	{
		get
		{
			return _FileType;
		}
		set
		{
			if (_FileType != value)
			{
				_FileType = value;
				OnPropertyChanged("FileType");
			}
		}
	}

	public bool Saved
	{
		get
		{
			return _Saved;
		}
		set
		{
			if (_Saved != value)
			{
				_Saved = value;
				OnPropertyChanged("Saved");
			}
		}
	}

	public Guid ParentRowId
	{
		get
		{
			return _ParentRowId;
		}
		set
		{
			if (_ParentRowId != value)
			{
				_ParentRowId = value;
				OnPropertyChanged("ParentRowId");
			}
		}
	}

	public bool IsFolder
	{
		get
		{
			return _IsFolder;
		}
		set
		{
			if (_IsFolder != value)
			{
				_IsFolder = value;
				OnPropertyChanged("IsFolder");
			}
		}
	}

	public DateTime CreationDate
	{
		get
		{
			return _CreationDate;
		}
		set
		{
			if (_CreationDate != value)
			{
				_CreationDate = value;
				OnPropertyChanged("CreationDate");
			}
		}
	}

	public bool IsExpanded
	{
		get
		{
			return _IsExpanded;
		}
		set
		{
			if (_IsExpanded != value)
			{
				_IsExpanded = value;
				OnPropertyChanged("IsExpanded");
			}
		}
	}

	public bool IsSelected
	{
		get
		{
			return _IsSelected;
		}
		set
		{
			if (_IsSelected != value)
			{
				_IsSelected = value;
				OnPropertyChanged("IsSelected");
			}
		}
	}

	public bool IsRootDocument
	{
		get
		{
			return _IsRootDocument;
		}
		set
		{
			if (_IsRootDocument != value)
			{
				_IsRootDocument = value;
				OnPropertyChanged("IsRootDocument");
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

	public bool IsPublic
	{
		get
		{
			return _bIsPublic;
		}
		set
		{
			if (_bIsPublic != value)
			{
				_bIsPublic = value;
				OnPropertyChanged("IsPublic");
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	public ExternalFile()
	{
		ExternalFiles = new ExternalFileList();
	}

	public object Clone()
	{
		ExternalFile externalFile = (ExternalFile)MemberwiseClone();
		externalFile.ExternalFiles = new ExternalFileList();
		return externalFile;
	}

	protected virtual void OnPropertyChanged(string propertyName)
	{
		if (this.PropertyChanged != null)
		{
			this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
