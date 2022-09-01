namespace Preference.Wpf.Controls.Projects.AppLogic;

public class PurchaseDocument : Document
{
	private int _Number;

	private int _Numeration;

	private int _PurchaseDocTypeCode;

	private string _PurchaseDocTypeName = string.Empty;

	public string PurchaseDocTypeName
	{
		get
		{
			return _PurchaseDocTypeName;
		}
		set
		{
			_PurchaseDocTypeName = value;
		}
	}

	public int PurchaseDocTypeCode
	{
		get
		{
			return _PurchaseDocTypeCode;
		}
		set
		{
			_PurchaseDocTypeCode = value;
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

	public int Numeration
	{
		get
		{
			return _Numeration;
		}
		set
		{
			_Numeration = value;
		}
	}
}
