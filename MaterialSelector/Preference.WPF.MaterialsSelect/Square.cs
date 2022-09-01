using System.Collections.ObjectModel;
using Preference.WPF.MaterialsSelector.Core;

namespace Preference.WPF.MaterialsSelector.Models;

public class Square : Notifier
{
	private ObservableCollection<Square> _Squares;

	private ObservableCollection<ProfilePiece> _ProfilePieces;

	private Square _Parent;

	private string _strId = string.Empty;

	private SquareRoles _Role;

	private bool _bIsExpanded = true;

	private bool _bIsSelected;

	private bool _bIsIncluded = true;

	private string _strWpfDrawing = string.Empty;

	public ObservableCollection<Square> Squares => _Squares;

	public ObservableCollection<ProfilePiece> ProfilePieces => _ProfilePieces;

	public Square Parent => _Parent;

	public string Id
	{
		get
		{
			return _strId;
		}
		set
		{
			_strId = value;
			OnPropertyChanged("Id");
		}
	}

	public SquareRoles Role
	{
		get
		{
			return _Role;
		}
		set
		{
			_Role = value;
			OnPropertyChanged("Role");
		}
	}

	public bool IsExpanded
	{
		get
		{
			return _bIsExpanded;
		}
		set
		{
			_bIsExpanded = value;
			OnPropertyChanged("IsExpanded");
		}
	}

	public bool IsIncluded
	{
		get
		{
			return _bIsIncluded;
		}
		set
		{
			_bIsIncluded = value;
			OnPropertyChanged("IsIncluded");
		}
	}

	public bool IsSelected
	{
		get
		{
			return _bIsSelected;
		}
		set
		{
			_bIsSelected = value;
			OnPropertyChanged("IsSelected");
		}
	}

	public string WpfDrawing
	{
		get
		{
			return _strWpfDrawing;
		}
		set
		{
			_strWpfDrawing = value;
			OnPropertyChanged("WpfDrawing");
		}
	}

	public Square()
		: this(null)
	{
	}

	public Square(Square parent)
	{
		_Parent = parent;
		_Squares = new ObservableCollection<Square>();
		_ProfilePieces = new ObservableCollection<ProfilePiece>();
	}

	public override string ToString()
	{
		return _strId;
	}
}
