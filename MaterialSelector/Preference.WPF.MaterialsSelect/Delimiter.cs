namespace Preference.WPF.MaterialsSelector.Models;

public class Delimiter : Item
{
	public Delimiter(Item parent)
		: base(ItemTypes.Delimiter, parent)
	{
	}

	public void Add(Rod item)
	{
		base.Items.Add(item);
	}
}
