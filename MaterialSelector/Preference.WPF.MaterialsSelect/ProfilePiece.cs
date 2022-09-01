namespace Preference.WPF.MaterialsSelector.Models;

public class ProfilePiece : Item
{
	private double _dAngle;

	private int _iOrder = -1;

	public double Angle
	{
		get
		{
			return _dAngle;
		}
		set
		{
			if (_dAngle != value)
			{
				_dAngle = value;
				OnPropertyChanged("Angle");
			}
		}
	}

	public int Order
	{
		get
		{
			return _iOrder;
		}
		set
		{
			if (_iOrder != value)
			{
				_iOrder = value;
				OnPropertyChanged("Order");
			}
		}
	}

	public ProfilePiece(Item parent)
		: base(ItemTypes.ProfilePiece, parent)
	{
	}

	public void Add(Rod item)
	{
		base.Items.Add(item);
	}

	public void Add(OuterRod item)
	{
		base.Items.Add(item);
	}
}
