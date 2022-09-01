namespace Preference.Wpf.Controls.Projects.AppLogic;

public class WarehouseDocument : Document
{
	private int m_nNumber;

	public int Number
	{
		get
		{
			return m_nNumber;
		}
		set
		{
			m_nNumber = value;
		}
	}
}
