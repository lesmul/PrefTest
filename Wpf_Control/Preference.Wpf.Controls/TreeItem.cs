using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media;

namespace Preference.Wpf.Controls;

public class TreeItem : INotifyPropertyChanged
{
	private string _strHeader;

	private string _strValue;

	private string _strDescription;

	private ImageSource _image;

	private Collection<TreeItem> _collectionChildren;

	private TreeItem _parent;

	private bool _bIsExpanded;

	private bool _bIsEnableEdit;

	private bool _bIsSelected;

	private bool _bIsChecked;

	private string _strType;

	public string Header
	{
		get
		{
			return _strHeader;
		}
		set
		{
			_strHeader = value;
			OnPropertyChanged("Header");
		}
	}

	public string Value
	{
		get
		{
			return _strValue;
		}
		set
		{
			_strValue = value;
			OnPropertyChanged("Value");
		}
	}

	public string Description
	{
		get
		{
			return _strDescription;
		}
		set
		{
			_strDescription = value;
			OnPropertyChanged("Description");
		}
	}

	public ImageSource Image
	{
		get
		{
			return _image;
		}
		set
		{
			_image = value;
			OnPropertyChanged("Image");
		}
	}

	public Collection<TreeItem> Children
	{
		get
		{
			if (_collectionChildren == null)
			{
				_collectionChildren = new Collection<TreeItem>();
			}
			return _collectionChildren;
		}
	}

	public TreeItem Parent
	{
		get
		{
			return _parent;
		}
		set
		{
			_parent = value;
			OnPropertyChanged("Parent");
		}
	}

	public string Type
	{
		get
		{
			return _strType;
		}
		set
		{
			_strType = value;
		}
	}

	public bool IsExpanded
	{
		get
		{
			return _bIsExpanded;
		}
		set
		{
			_bIsExpanded = value;
		}
	}

	public bool IsSelected
	{
		get
		{
			return _bIsSelected;
		}
		set
		{
			_bIsSelected = value;
		}
	}

	public bool IsEnableEdit
	{
		get
		{
			return _bIsEnableEdit;
		}
		set
		{
			_bIsEnableEdit = value;
		}
	}

	public bool IsChecked
	{
		get
		{
			return _bIsChecked;
		}
		set
		{
			_bIsChecked = value;
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	public TreeItem()
	{
	}

	public TreeItem(string strHeader, string strValue, string strDescription, TreeItem parent)
	{
		Header = strHeader;
		Value = strValue;
		Description = strDescription;
		Parent = parent;
	}

	public TreeItem(string strHeader, string strValue, string strDescription, ImageSource image, TreeItem parent)
	{
		Header = strHeader;
		Value = strValue;
		Description = strDescription;
		Image = image;
		Parent = parent;
	}

	public TreeItem(string strHeader, string strValue, string strDescription, ImageSource image, TreeItem parent, string strType)
	{
		Header = strHeader;
		Value = strValue;
		Description = strDescription;
		Image = image;
		Parent = parent;
		Type = strType;
	}

	public bool Contains(string strToSearch)
	{
		string value = strToSearch.ToUpper();
		if (Header.ToUpper().Contains(value) || Description.ToUpper().Contains(value) || Value.ToUpper().Contains(value))
		{
			return true;
		}
		return false;
	}

	protected void OnPropertyChanged(string propName)
	{
		if (this.PropertyChanged != null)
		{
			this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
		}
	}
}
