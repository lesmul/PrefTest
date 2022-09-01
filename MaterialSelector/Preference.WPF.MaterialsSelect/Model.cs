using System.Collections.ObjectModel;
using Preference.WPF.MaterialsSelector.Core;

namespace Preference.WPF.MaterialsSelector.Models;

public class Model : Notifier
{
	private ObservableCollection<Item> _Items;

	private ObservableCollection<GeneratedMaterial> _GeneratedMaterials;

	private ObservableCollection<Square> _Squares;

	private string _strWpfDrawing = string.Empty;

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

	public ObservableCollection<Item> Items => _Items;

	public ObservableCollection<GeneratedMaterial> GeneratedMaterials => _GeneratedMaterials;

	public ObservableCollection<Square> Squares => _Squares;

	public string StructureDigest { get; set; }

	public Model()
	{
		_Items = new ObservableCollection<Item>();
		_Squares = new ObservableCollection<Square>();
		_GeneratedMaterials = new ObservableCollection<GeneratedMaterial>();
	}
}
