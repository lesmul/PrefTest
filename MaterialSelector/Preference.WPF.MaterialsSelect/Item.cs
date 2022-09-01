using System.Collections.ObjectModel;
using Preference.WPF.MaterialsSelector.Core;

namespace Preference.WPF.MaterialsSelector.Models;

public class Item : Notifier
{
	private Item _Parent;

	private ItemTypes _ItemType;

	private string _strID = string.Empty;

	private string _strSquareID = string.Empty;

	private string _strElementName = string.Empty;

	private ObservableCollection<string> _DrawingIdentifiers;

	private ObservableCollection<GeneratedMaterial> _GeneratedMaterials;

	private ObservableCollection<Item> _Items;

	private bool? _bIsIncluded = true;

	private string _strWpfDrawing = string.Empty;

	private bool _bIsExpanded;

	private bool _bIsSelected;

	public Item Parent => _Parent;

	public string ID
	{
		get
		{
			return _strID;
		}
		set
		{
			if (_strID != value)
			{
				_strID = value;
				OnPropertyChanged("ID");
			}
		}
	}

	public string SquareID
	{
		get
		{
			return _strSquareID;
		}
		set
		{
			if (_strSquareID != value)
			{
				_strSquareID = value;
				OnPropertyChanged("SquareID");
			}
		}
	}

	public string ElementName
	{
		get
		{
			return _strElementName;
		}
		set
		{
			if (_strElementName != value)
			{
				_strElementName = value;
				OnPropertyChanged("ElementName");
			}
		}
	}

	public string WpfDrawing
	{
		get
		{
			return _strWpfDrawing;
		}
		set
		{
			_strWpfDrawing = value;
			OnPropertyChanged("WpfDrawing");
		}
	}

	public ObservableCollection<string> DrawingIdentifiers => _DrawingIdentifiers;

	public ObservableCollection<GeneratedMaterial> GeneratedMaterials => _GeneratedMaterials;

	public ObservableCollection<Item> Items => _Items;

	public ItemTypes ItemType
	{
		get
		{
			return _ItemType;
		}
		set
		{
			if (_ItemType != value)
			{
				_ItemType = value;
				OnPropertyChanged("ItemType");
			}
		}
	}

	public bool? IsIncluded
	{
		get
		{
			return _bIsIncluded;
		}
		set
		{
			if (_bIsIncluded != value)
			{
				_bIsIncluded = value;
				OnPropertyChanged("IsIncluded");
			}
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
			if (_bIsExpanded != value)
			{
				_bIsExpanded = value;
				OnPropertyChanged("IsExpanded");
			}
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
			if (_bIsSelected != value)
			{
				_bIsSelected = value;
				OnPropertyChanged("IsSelected");
			}
		}
	}

	public Item(ItemTypes itemType, Item parent)
	{
		_ItemType = itemType;
		_Parent = parent;
		_DrawingIdentifiers = new ObservableCollection<string>();
		_GeneratedMaterials = new ObservableCollection<GeneratedMaterial>();
		_Items = new ObservableCollection<Item>();
	}
}
