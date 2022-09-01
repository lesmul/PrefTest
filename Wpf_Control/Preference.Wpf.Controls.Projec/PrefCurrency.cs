namespace Preference.Wpf.Controls.Projects.AppLogic;

public class PrefCurrency
{
	private string m_strName = string.Empty;

	private string m_strSymbol = string.Empty;

	public string Symbol
	{
		get
		{
			return m_strSymbol;
		}
		set
		{
			m_strSymbol = value;
		}
	}

	public string Name
	{
		get
		{
			return m_strName;
		}
		set
		{
			m_strName = value;
		}
	}

	public override string ToString()
	{
		return m_strName;
	}
}
