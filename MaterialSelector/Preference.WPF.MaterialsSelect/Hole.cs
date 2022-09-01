namespace Preference.WPF.MaterialsSelector.Models;

public class Hole : Item
{
	private bool _bIsSash;

	public bool IsSash
	{
		get
		{
			return _bIsSash;
		}
		set
		{
			if (_bIsSash != value)
			{
				_bIsSash = value;
				OnPropertyChanged("IsSash");
			}
		}
	}

	public Hole(Item parent)
		: base(ItemTypes.Hole, parent)
	{
	}

	public void Add(Submodel item)
	{
		base.Items.Add(item);
	}

	public void Add(Hole item)
	{
		base.Items.Add(item);
	}

	public void Add(Delimiter item)
	{
		base.Items.Add(item);
	}

	public void Add(Glass item)
	{
		base.Items.Add(item);
	}

	public void Add(Rod item)
	{
		base.Items.Add(item);
	}

	public void Add(SashHardware item)
	{
		base.Items.Add(item);
	}

	public void Add(FrameHardware item)
	{
		base.Items.Add(item);
	}

	public void Add(AluClip item)
	{
		base.Items.Add(item);
	}
}
