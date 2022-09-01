namespace Preference.WPF.MaterialsSelector.Models;

public class OuterRod : Item
{
	public OuterRod(Item parent)
		: base(ItemTypes.OuterRod, parent)
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
