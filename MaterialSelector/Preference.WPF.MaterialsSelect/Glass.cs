namespace Preference.WPF.MaterialsSelector.Models;

public class Glass : Item
{
	public Glass(Item parent)
		: base(ItemTypes.Glass, parent)
	{
	}

	public void Add(GlazingLedge item)
	{
		base.Items.Add(item);
	}
}
