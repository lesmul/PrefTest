namespace Preference.WPF.MaterialsSelector.Models;

public class Rod : Item
{
	public Rod(Item parent)
		: base(ItemTypes.Rod, parent)
	{
	}

	public void Add(ProfilePiece item)
	{
		base.Items.Add(item);
	}
}
