using System;
using Preference.WPF.MaterialsSelector.Core;

namespace Preference.WPF.MaterialsSelector.Models;

public class GeneratedMaterial : Notifier
{
	private string _strID = string.Empty;

	private string _strTag = string.Empty;

	private string _strSquareID = string.Empty;

	private MaterialTypes _MaterialType;

	private string _strMaterialTypeString = string.Empty;

	private string _strReference = string.Empty;

	private string _strColor = string.Empty;

	private string _strDescription = string.Empty;

	private int _iQuantityIncluded = -1;

	private int _iQuantityTotal = -1;

	private string _strWidth;

	private string _strHeight;

	private bool? _bIsIncluded = false;

	private bool _bIsComponent;

	private Item _Element;

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

	public string Tag
	{
		get
		{
			return _strTag;
		}
		set
		{
			if (_strTag != value)
			{
				_strTag = value;
				OnPropertyChanged("Tag");
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

	public MaterialTypes MaterialType
	{
		get
		{
			return _MaterialType;
		}
		set
		{
			if (_MaterialType != value)
			{
				_MaterialType = value;
				OnPropertyChanged("MaterialType");
			}
		}
	}

	public string MaterialTypeString
	{
		get
		{
			return _strMaterialTypeString;
		}
		set
		{
			if (_strMaterialTypeString != value)
			{
				_strMaterialTypeString = value;
				OnPropertyChanged("MaterialTypeString");
			}
		}
	}

	public string Reference
	{
		get
		{
			return _strReference;
		}
		set
		{
			if (_strReference != value)
			{
				_strReference = value;
				OnPropertyChanged("Reference");
			}
		}
	}

	public string Color
	{
		get
		{
			return _strColor;
		}
		set
		{
			if (_strColor != value)
			{
				_strColor = value;
				OnPropertyChanged("Color");
			}
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
			if (_strDescription != value)
			{
				_strDescription = value;
				OnPropertyChanged("Description");
			}
		}
	}

	public int QuantityIncluded
	{
		get
		{
			return _iQuantityIncluded;
		}
		set
		{
			if (_iQuantityIncluded == value)
			{
				return;
			}
			if (_iQuantityTotal > 0 && (value < 0 || value > _iQuantityTotal))
			{
				throw new ArgumentOutOfRangeException();
			}
			_iQuantityIncluded = value;
			OnPropertyChanged("QuantityIncluded");
			if (_iQuantityIncluded > 0 && _iQuantityIncluded < _iQuantityTotal)
			{
				IsIncluded = null;
			}
			else if (_iQuantityIncluded == 0)
			{
				IsIncluded = false;
			}
			else if (_iQuantityIncluded == _iQuantityTotal)
			{
				IsIncluded = true;
			}
			if (Element == null)
			{
				return;
			}
			int num = 0;
			int num2 = 0;
			foreach (GeneratedMaterial generatedMaterial in Element.GeneratedMaterials)
			{
				num += generatedMaterial.QuantityTotal;
				num2 += generatedMaterial.QuantityIncluded;
			}
			if (num2 > 0 && num2 < num)
			{
				Element.IsIncluded = null;
			}
			else if (num2 == 0)
			{
				Element.IsIncluded = false;
			}
			else if (num2 == num)
			{
				Element.IsIncluded = true;
			}
		}
	}

	public int QuantityTotal
	{
		get
		{
			return _iQuantityTotal;
		}
		set
		{
			if (_iQuantityTotal != value)
			{
				_iQuantityTotal = value;
				OnPropertyChanged("QuantityTotal");
			}
		}
	}

	public string Width
	{
		get
		{
			return _strWidth;
		}
		set
		{
			if (_strWidth != value)
			{
				_strWidth = value;
				OnPropertyChanged("Width");
			}
		}
	}

	public string Height
	{
		get
		{
			return _strHeight;
		}
		set
		{
			if (_strHeight != value)
			{
				_strHeight = value;
				OnPropertyChanged("Height");
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

	public bool IsComponent
	{
		get
		{
			return _bIsComponent;
		}
		set
		{
			if (_bIsComponent != value)
			{
				_bIsComponent = value;
				OnPropertyChanged("IsComponent");
			}
		}
	}

	public Item Element
	{
		get
		{
			return _Element;
		}
		set
		{
			if (_Element != value)
			{
				_Element = value;
				OnPropertyChanged("Element");
			}
		}
	}

	public override bool Equals(object obj)
	{
		if (obj == null || GetType() != obj.GetType())
		{
			return false;
		}
		return ((GeneratedMaterial)obj).ID == ID;
	}

	public override int GetHashCode()
	{
		return (GetType().FullName + "|" + _strID).GetHashCode();
	}

	public override string ToString()
	{
		return $"{_strID} {_strReference}";
	}
}
