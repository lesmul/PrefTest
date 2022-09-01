namespace Preference.Logikal;

public class ComboItem
{
	private int _nTag;

	private string strName;

	public int Tag => _nTag;

	public ComboItem(string name, int tag)
	{
		_nTag = tag;
		strName = name;
	}

	public override string ToString()
	{
		return strName;
	}
}
