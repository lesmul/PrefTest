namespace Preference.WPF.MaterialsSelector.Models;

public class Submodel : Hole
{
	private int _iCompositeOrder = -1;

	private string _strCompositeCode = string.Empty;

	public int CompositeOrder
	{
		get
		{
			return _iCompositeOrder;
		}
		set
		{
			if (_iCompositeOrder != value)
			{
				_iCompositeOrder = value;
				OnPropertyChanged("CompositeOrder");
			}
		}
	}

	public string CompositeCode
	{
		get
		{
			return _strCompositeCode;
		}
		set
		{
			if (_strCompositeCode != value)
			{
				_strCompositeCode = value;
				OnPropertyChanged("CompositeCode");
			}
		}
	}

	public Submodel(Item parent)
		: base(parent)
	{
		base.ItemType = ItemTypes.Submodel;
	}
}
