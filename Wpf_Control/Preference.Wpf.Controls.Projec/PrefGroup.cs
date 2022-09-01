using System;

namespace Preference.Wpf.Controls.Projects.AppLogic;

public class PrefGroup
{
	private int m_nCode;

	private string m_strName;

	private Guid m_guidRowId;

	private enGroupType m_nType;

	private string m_strSupplier;

	public string Supplier
	{
		get
		{
			return m_strSupplier;
		}
		set
		{
			m_strSupplier = value;
		}
	}

	public enGroupType Type
	{
		get
		{
			return m_nType;
		}
		set
		{
			m_nType = value;
		}
	}

	public Guid RowId
	{
		get
		{
			return m_guidRowId;
		}
		set
		{
			m_guidRowId = value;
		}
	}

	public int Code
	{
		get
		{
			return m_nCode;
		}
		set
		{
			m_nCode = value;
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
