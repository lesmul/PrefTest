using System;

namespace Preference.Wpf.Controls.Projects.AppLogic;

public class Document : Notifier
{
	protected Guid _guidDocumentId = Guid.Empty;

	protected DocumentTypes _DocumentType;

	protected DocumentSubTypes _DocumentSubType = DocumentSubTypes.PurchasesDeliveryNote;

	protected string _strDocumentName = string.Empty;

	protected string _strCustomerName = string.Empty;

	protected string _strSupplierName = string.Empty;

	protected int _iRelationShipType;

	protected string _strDescription = string.Empty;

	protected enStatus _Status = enStatus.Created;

	public enStatus Status
	{
		get
		{
			return _Status;
		}
		set
		{
			if (_Status != value)
			{
				_Status = value;
				OnPropertyChanged("Status");
			}
		}
	}

	public Guid DocumentId
	{
		get
		{
			return _guidDocumentId;
		}
		set
		{
			if (_guidDocumentId != value)
			{
				_guidDocumentId = value;
				OnPropertyChanged("DocumentId");
			}
		}
	}

	public DocumentTypes DocumentType
	{
		get
		{
			return _DocumentType;
		}
		set
		{
			if (_DocumentType != value)
			{
				_DocumentType = value;
				OnPropertyChanged("DocumentType");
			}
		}
	}

	public DocumentSubTypes DocumentSubType
	{
		get
		{
			return _DocumentSubType;
		}
		set
		{
			if (_DocumentSubType != value)
			{
				_DocumentSubType = value;
				OnPropertyChanged("DocumentSubType");
			}
		}
	}

	public string DocumentName
	{
		get
		{
			return _strDocumentName;
		}
		set
		{
			if (_strDocumentName != value)
			{
				_strDocumentName = value;
				OnPropertyChanged("DocumentName");
			}
		}
	}

	public string CustomerName
	{
		get
		{
			return _strCustomerName;
		}
		set
		{
			if (_strCustomerName != value)
			{
				_strCustomerName = value;
				OnPropertyChanged("CustomerName");
			}
		}
	}

	public string SupplierName
	{
		get
		{
			return _strSupplierName;
		}
		set
		{
			if (_strSupplierName != value)
			{
				_strSupplierName = value;
				OnPropertyChanged("SupplierName");
			}
		}
	}

	public string Description
	{
		get
		{
			return _strDescription;
		}
		set
		{
			if (_strDescription != value)
			{
				_strDescription = value;
				OnPropertyChanged("Description");
			}
		}
	}

	public int RelationShipType
	{
		get
		{
			return _iRelationShipType;
		}
		set
		{
			if (_iRelationShipType != value)
			{
				_iRelationShipType = value;
				OnPropertyChanged("RelationShipType");
			}
		}
	}
}
