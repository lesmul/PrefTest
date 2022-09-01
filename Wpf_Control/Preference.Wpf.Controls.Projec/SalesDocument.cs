using System;

namespace Preference.Wpf.Controls.Projects.AppLogic;

public class SalesDocument : Document
{
	private int _Number;

	private int _Version;

	private DateTime _EntryDate = DateTime.MinValue;

	private string _SalesDocTypeName;

	private int _OrderNumber;

	private string _VersionName;

	public int OrderNumber
	{
		get
		{
			return _OrderNumber;
		}
		set
		{
			_OrderNumber = value;
		}
	}

	public string VersionName
	{
		get
		{
			return _VersionName;
		}
		set
		{
			_VersionName = value;
			base.Description = value;
		}
	}

	public string SalesDocTypeName
	{
		get
		{
			return _SalesDocTypeName;
		}
		set
		{
			_SalesDocTypeName = value;
		}
	}

	public int Number
	{
		get
		{
			return _Number;
		}
		set
		{
			_Number = value;
		}
	}

	public int Version
	{
		get
		{
			return _Version;
		}
		set
		{
			_Version = value;
		}
	}

	public DateTime EntryDate
	{
		get
		{
			return _EntryDate;
		}
		set
		{
			_EntryDate = value;
		}
	}
}
