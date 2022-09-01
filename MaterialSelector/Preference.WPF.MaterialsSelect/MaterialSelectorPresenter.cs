using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using System.Windows.Threading;
using System.Xml;
using Preference.Diagnostics;
using Preference.PrefItems.Filtering;
using Preference.WPF.MaterialsSelector.Core;
using Preference.WPF.MaterialsSelector.Core.Commands;
using Preference.WPF.MaterialsSelector.Models;
using Preference.WPF.MaterialsSelector.Properties;
using Preference.WPF.MaterialsSelector.Settings;
using Preference.WPF.MaterialsSelector.UsersControls;

namespace Preference.WPF.MaterialsSelector.Presenters;

internal class MaterialSelectorPresenter : Notifier
{
	private delegate void RefreshDataDelegate();

	private IMaterialsSelectorControl _View;

	private bool _bIsBusy;

	private bool _bReadOnly;

	private IServiceAgent _ServiceAgent;

	private Model _Model;

	private string _strWPFDrawing = string.Empty;

	private IList<string> _excludedDrawingIds;

	private Item _SelectedItem;

	private GeneratedMaterial _SelectedMaterial;

	private ObservableCollection<GeneratedMaterial> _ShownMaterials;

	private List<GeneratedMaterial> _Components;

	private byte[] _SelectedMaterialImage;

	private string _strSelectedIdentifier = string.Empty;

	private ObservableCollection<string> _SelectedIdentifiers1;

	private ObservableCollection<string> _SelectedIdentifiers2;

	private string _strName = string.Empty;

	private string _strProductSystem = string.Empty;

	private string _strDescription = string.Empty;

	private string _strColor = string.Empty;

	private string _strFamily = string.Empty;

	private string _strProductType = string.Empty;

	private string _strCurrentMaterialsTitle = string.Empty;

	private string _strXmlFilter = string.Empty;

	private TimeSpan _RefreshDuration;

	private ICommand _Refresh;

	private ICommand _IncludeItemCommand;

	private ICommand _IncludeMaterialCommand;

	private ICommand _IncludeAllItemsCommand;

	private ICommand _ExcludeAllItemsCommand;

	private bool _bIsModeRecursive;

	private string _strDescriptiveXmlStructureNodeDocumentXml = string.Empty;

	private bool _bShowComponents;

	private bool _isLoadingData;

	private ICommand _ViewGeneralPanelCommand;

	private ICommand _ViewItemsPanelCommand;

	private ICommand _ViewModelPanelCommand;

	private ICommand _ViewItemPanelCommand;

	private ICommand _ViewMaterialsPanelCommand;

	private ICommand _ViewMaterialPanelCommand;

	private ICommand _ResetWindowLayoutCommand;

	public string DescriptiveXmlStructureNodeDocumentXml => _strDescriptiveXmlStructureNodeDocumentXml;

	public bool IsBusy
	{
		get
		{
			return _bIsBusy;
		}
		set
		{
			if (_bIsBusy != value)
			{
				_bIsBusy = value;
				OnPropertyChanged("IsBusy");
			}
		}
	}

	public bool ReadOnly
	{
		get
		{
			return _bReadOnly;
		}
		set
		{
			if (_bReadOnly != value)
			{
				_bReadOnly = value;
				OnPropertyChanged("ReadOnly");
			}
		}
	}

	public bool IsModeRecursive
	{
		get
		{
			return _bIsModeRecursive;
		}
		set
		{
			if (_bIsModeRecursive != value)
			{
				_bIsModeRecursive = value;
				OnPropertyChanged("IsModeRecursive");
				new MaterialSelectorPresenterSettings().SetIsModeRecursive(_bIsModeRecursive);
			}
		}
	}

	public bool ShowComponents
	{
		get
		{
			return _bShowComponents;
		}
		set
		{
			if (_bShowComponents != value)
			{
				_bShowComponents = value;
				OnPropertyChanged("ShowComponents");
				new MaterialSelectorPresenterSettings().SetShowComponents(_bShowComponents);
				ShowHideComponents(_bShowComponents);
			}
		}
	}

	public IServiceAgent ServiceAgent => _ServiceAgent;

	public Collection<string> SelectedMaterials
	{
		get
		{
			Collection<string> collection = new Collection<string>();
			if (Model != null)
			{
				foreach (GeneratedMaterial generatedMaterial in Model.GeneratedMaterials)
				{
					if (!generatedMaterial.IsIncluded.HasValue)
					{
						collection.Add(generatedMaterial.ID);
					}
					else if (generatedMaterial.IsIncluded.Value)
					{
						collection.Add(generatedMaterial.ID);
					}
				}
				return collection;
			}
			return collection;
		}
	}

	public string Name
	{
		get
		{
			return _strName;
		}
		set
		{
			_strName = value;
			OnPropertyChanged("Name");
		}
	}

	public string ProductSystem
	{
		get
		{
			return _strProductSystem;
		}
		set
		{
			_strProductSystem = value;
			OnPropertyChanged("ProductSystem");
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
			_strDescription = value;
			OnPropertyChanged("Description");
		}
	}

	public string Color
	{
		get
		{
			return _strColor;
		}
		set
		{
			_strColor = value;
			OnPropertyChanged("Color");
		}
	}

	public string Family
	{
		get
		{
			return _strFamily;
		}
		set
		{
			_strFamily = value;
			OnPropertyChanged("Family");
		}
	}

	public string ProductType
	{
		get
		{
			return _strProductType;
		}
		set
		{
			_strProductType = value;
			OnPropertyChanged("ProductType");
		}
	}

	public TimeSpan RefreshDuration
	{
		get
		{
			return _RefreshDuration;
		}
		set
		{
			_RefreshDuration = value;
			OnPropertyChanged("RefreshDuration");
		}
	}

	public Model Model
	{
		get
		{
			return _Model;
		}
		set
		{
			_Model = value;
			OnPropertyChanged("Model");
		}
	}

	public Item SelectedItem
	{
		get
		{
			return _SelectedItem;
		}
		set
		{
			_SelectedItem = value;
			OnPropertyChanged("SelectedItem");
			SelectedIdentifiers1.Clear();
			if (_SelectedItem == null)
			{
				return;
			}
			GenerateWpfDrawing(_SelectedItem);
			foreach (string drawingIdentifier in _SelectedItem.DrawingIdentifiers)
			{
				SelectedIdentifiers1.Add(drawingIdentifier);
			}
		}
	}

	public GeneratedMaterial SelectedMaterial
	{
		get
		{
			return _SelectedMaterial;
		}
		set
		{
			_SelectedMaterial = value;
			OnPropertyChanged("SelectedMaterial");
			SelectedIdentifiers2.Clear();
			if (_SelectedMaterial != null)
			{
				if (!string.IsNullOrEmpty(_SelectedMaterial.Tag))
				{
					SelectedIdentifiers2.Add(_SelectedMaterial.Tag);
				}
				SelectedMaterialImage = _ServiceAgent.GetImageAsPng(SelectedMaterial.Reference, 60, 60);
				string itemID = string.Empty;
				if (_SelectedMaterial.Element != null)
				{
					itemID = _SelectedMaterial.Element.ID;
				}
				SelectItem(itemID);
			}
		}
	}

	public byte[] SelectedMaterialImage
	{
		get
		{
			return _SelectedMaterialImage;
		}
		set
		{
			if (_SelectedMaterialImage != value)
			{
				_SelectedMaterialImage = value;
				OnPropertyChanged("SelectedMaterialImage");
			}
		}
	}

	public string SelectedIdentifier
	{
		get
		{
			return _strSelectedIdentifier;
		}
		set
		{
			if (_strSelectedIdentifier != value)
			{
				_strSelectedIdentifier = value;
				OnPropertyChanged("SelectedIdentifier");
				Item item = GetItem(_strSelectedIdentifier);
				if (item != null)
				{
					SelectedItem = item;
				}
				GeneratedMaterial material = GetMaterial(_strSelectedIdentifier);
				if (material != null)
				{
					SelectedMaterial = material;
				}
			}
		}
	}

	public string CurrentMaterialsTitle
	{
		get
		{
			return _strCurrentMaterialsTitle;
		}
		set
		{
			_strCurrentMaterialsTitle = value;
			OnPropertyChanged("CurrentMaterialsTitle");
		}
	}

	public ObservableCollection<GeneratedMaterial> ShownMaterials => _ShownMaterials;

	public ObservableCollection<string> SelectedIdentifiers1
	{
		get
		{
			return _SelectedIdentifiers1;
		}
		set
		{
			_SelectedIdentifiers1 = value;
			OnPropertyChanged("SelectedIdentifiers1");
		}
	}

	public ObservableCollection<string> SelectedIdentifiers2
	{
		get
		{
			return _SelectedIdentifiers2;
		}
		set
		{
			_SelectedIdentifiers2 = value;
			OnPropertyChanged("SelectedIdentifiers2");
		}
	}

	public ICommand Refresh
	{
		get
		{
			if (_Refresh == null)
			{
				_Refresh = new AsyncDelegateCommand(ExecuteRefresh, CanExecuteRefresh);
			}
			return _Refresh;
		}
	}

	public ICommand ViewGeneralPanelCommand
	{
		get
		{
			if (_ViewGeneralPanelCommand == null)
			{
				_ViewGeneralPanelCommand = new DelegateCommand("ViewGeneralPanelCommand", delegate
				{
					_View.ViewGeneralPanel();
				});
			}
			return _ViewGeneralPanelCommand;
		}
	}

	public ICommand ViewItemsPanelCommand
	{
		get
		{
			if (_ViewItemsPanelCommand == null)
			{
				_ViewItemsPanelCommand = new DelegateCommand("ViewItemsPanelCommand", delegate
				{
					_View.ViewItemsPanel();
				});
			}
			return _ViewItemsPanelCommand;
		}
	}

	public ICommand ViewModelPanelCommand
	{
		get
		{
			if (_ViewModelPanelCommand == null)
			{
				_ViewModelPanelCommand = new DelegateCommand("ViewModelPanelCommand", delegate
				{
					_View.ViewModelPanel();
				});
			}
			return _ViewModelPanelCommand;
		}
	}

	public ICommand ViewItemPanelCommand
	{
		get
		{
			if (_ViewItemPanelCommand == null)
			{
				_ViewItemPanelCommand = new DelegateCommand("ViewItemPanelCommand", delegate
				{
					_View.ViewItemPanel();
				});
			}
			return _ViewItemPanelCommand;
		}
	}

	public ICommand ViewMaterialsPanelCommand
	{
		get
		{
			if (_ViewMaterialsPanelCommand == null)
			{
				_ViewMaterialsPanelCommand = new DelegateCommand("ViewMaterialsPanelCommand", delegate
				{
					_View.ViewMaterialsPanel();
				});
			}
			return _ViewMaterialsPanelCommand;
		}
	}

	public ICommand ViewMaterialPanelCommand
	{
		get
		{
			if (_ViewMaterialPanelCommand == null)
			{
				_ViewMaterialPanelCommand = new DelegateCommand("ViewMaterialPanelCommand", delegate
				{
					_View.ViewMaterialPanel();
				});
			}
			return _ViewMaterialPanelCommand;
		}
	}

	public ICommand IncludeItemCommand
	{
		get
		{
			if (_IncludeItemCommand == null)
			{
				_IncludeItemCommand = new DelegateCommand<Item>(ExecuteIncludeItemCommand, CanExecuteIncludeItemCommand);
			}
			return _IncludeItemCommand;
		}
	}

	public ICommand IncludeMaterialCommand
	{
		get
		{
			if (_IncludeMaterialCommand == null)
			{
				_IncludeMaterialCommand = new DelegateCommand<GeneratedMaterial>(ExecuteIncludeMaterialCommand, CanIncludeMaterialCommandExecute);
			}
			return _IncludeMaterialCommand;
		}
	}

	public ICommand IncludeAllItemsCommand
	{
		get
		{
			if (_IncludeAllItemsCommand == null)
			{
				_IncludeAllItemsCommand = new DelegateCommand("IncludeAllItemsCommand", ExecuteIncludeAllItemsCommand, CanIncludeAllItemsCommandExecute);
			}
			return _IncludeAllItemsCommand;
		}
	}

	public ICommand ExcludeAllItemsCommand
	{
		get
		{
			if (_ExcludeAllItemsCommand == null)
			{
				_ExcludeAllItemsCommand = new DelegateCommand("ExcludeAllItemsCommand", ExecuteExcludeAllItemsCommand, CanExcludeAllItemsCommandExecute);
			}
			return _ExcludeAllItemsCommand;
		}
	}

	public ICommand ResetWindowLayoutCommand
	{
		get
		{
			if (_ResetWindowLayoutCommand == null)
			{
				_ResetWindowLayoutCommand = new DelegateCommand("ResetWindowLayoutCommand", delegate
				{
					_View.ResetWindowLayout();
				});
			}
			return _ResetWindowLayoutCommand;
		}
	}

	public MaterialSelectorPresenter(IServiceAgent serviceAgent, Dispatcher dispatcher, IMaterialsSelectorControl view)
		: base(dispatcher)
	{
		_View = view;
		_ServiceAgent = serviceAgent;
		_excludedDrawingIds = new List<string>();
		SelectedIdentifiers1 = new ObservableCollection<string>();
		SelectedIdentifiers2 = new ObservableCollection<string>();
		_ShownMaterials = new ObservableCollection<GeneratedMaterial>();
		_Components = new List<GeneratedMaterial>();
		MaterialSelectorPresenterSettings materialSelectorPresenterSettings = new MaterialSelectorPresenterSettings();
		ShowComponents = materialSelectorPresenterSettings.GetShowComponents();
		IsModeRecursive = materialSelectorPresenterSettings.GetIsModeRecursive();
	}

	private void ShowHideComponents(bool show)
	{
		if (Model == null)
		{
			return;
		}
		if (show)
		{
			foreach (GeneratedMaterial component in _Components)
			{
				if (!ShownMaterials.Contains(component))
				{
					IncludeGeneratedMaterial(component, include: true, show);
					ShownMaterials.Add(component);
				}
			}
		}
		else
		{
			foreach (GeneratedMaterial component2 in _Components)
			{
				IncludeGeneratedMaterial(component2, include: false, show);
				ShownMaterials.Remove(component2);
			}
		}
		RefreshCurrentMaterialsTitle();
	}

	private void ClearData()
	{
		InvokeOnUI(delegate
		{
			CurrentMaterialsTitle = string.Empty;
		});
		InvokeOnUI(delegate
		{
			SelectedIdentifier = null;
		});
		InvokeOnUI(delegate
		{
			SelectedItem = null;
		});
		InvokeOnUI(delegate
		{
			SelectedMaterial = null;
		});
		InvokeOnUI(delegate
		{
			SelectedMaterialImage = null;
		});
		InvokeOnUI(delegate
		{
			SelectedMaterials.Clear();
		});
		InvokeOnUI(delegate
		{
			_View.ResetItems();
		});
		InvokeOnUI(delegate
		{
			SelectedIdentifiers1.Clear();
		});
		InvokeOnUI(delegate
		{
			SelectedIdentifiers2.Clear();
		});
		InvokeOnUI(delegate
		{
			ShownMaterials.Clear();
		});
		_Components.Clear();
	}

	public void RefreshData()
	{
		DateTime now = DateTime.Now;
		try
		{
			_isLoadingData = true;
			ClearData();
			Model = new Model();
			Name = _ServiceAgent.GetName();
			ProductSystem = _ServiceAgent.GetSystem();
			Description = _ServiceAgent.GetDescription();
			Color = _ServiceAgent.GetColor();
			Family = _ServiceAgent.GetFamily();
			ProductType = _ServiceAgent.GetProductType();
			RefreshModelGeneratedMaterials(_ServiceAgent.XmlDocument, _ServiceAgent.XmlNamespaceManager, _ServiceAgent.GetDummyReferences());
			InvokeOnUI(delegate
			{
				RefreshShownMaterials(Model);
			});
			InvokeOnUI(delegate
			{
				RefreshItems(_ServiceAgent.XmlDocument, _ServiceAgent.XmlNamespaceManager);
			});
			GenerateWpfDrawingOfModel();
			if (!string.IsNullOrEmpty(_strXmlFilter))
			{
				InvokeOnUI(delegate
				{
					SetXmlFilter(_strXmlFilter);
				});
			}
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefGest");
			throw;
		}
		finally
		{
			DateTime now2 = DateTime.Now;
			RefreshDuration = now2 - now;
			_isLoadingData = false;
			RefreshCurrentMaterialsTitle();
		}
	}

	private string GetElementID(string generatedMaterialId)
	{
		foreach (Item item in Model.Items)
		{
			string elementID = GetElementID(generatedMaterialId, item);
			if (!string.IsNullOrEmpty(elementID))
			{
				return elementID;
			}
		}
		return string.Empty;
	}

	private string GetElementID(string generatedMaterialId, Item item)
	{
		foreach (GeneratedMaterial generatedMaterial in item.GeneratedMaterials)
		{
			if (generatedMaterial.ID == generatedMaterialId)
			{
				string result = string.Empty;
				if (generatedMaterial.Element != null)
				{
					result = generatedMaterial.Element.ID;
				}
				return result;
			}
		}
		foreach (Item item2 in item.Items)
		{
			string elementID = GetElementID(generatedMaterialId, item2);
			if (!string.IsNullOrEmpty(elementID))
			{
				return elementID;
			}
		}
		return string.Empty;
	}

	private bool IsGeneratedMaterialInItems(string generatedMaterialId)
	{
		foreach (Item item in Model.Items)
		{
			if (IsGeneratedMaterialInItems(generatedMaterialId, item))
			{
				return true;
			}
		}
		return false;
	}

	private bool IsGeneratedMaterialInItems(string generatedMaterialId, Item item)
	{
		foreach (GeneratedMaterial generatedMaterial in item.GeneratedMaterials)
		{
			if (generatedMaterial.ID == generatedMaterialId)
			{
				return true;
			}
		}
		foreach (Item item2 in item.Items)
		{
			if (IsGeneratedMaterialInItems(generatedMaterialId, item2))
			{
				return true;
			}
		}
		return false;
	}

	private void ExecuteRefresh()
	{
		IsBusy = true;
		try
		{
			RefreshData();
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefGest");
			throw;
		}
		finally
		{
			IsBusy = false;
		}
	}

	private bool CanExecuteRefresh()
	{
		return _ServiceAgent.IsDocumentLoaded;
	}

	private bool CanExecuteIncludeItemCommand(object parameter)
	{
		if (!ReadOnly)
		{
			return _ServiceAgent.IsDocumentLoaded;
		}
		return false;
	}

	private bool CanIncludeMaterialCommandExecute(object parameter)
	{
		if (!ReadOnly)
		{
			return _ServiceAgent.IsDocumentLoaded;
		}
		return false;
	}

	private void RefreshExcludedDrawingIds(Item item, bool recursive = false)
	{
		foreach (string drawingIdentifier in item.DrawingIdentifiers)
		{
			if (item.IsIncluded.HasValue && item.IsIncluded.Value)
			{
				if (_excludedDrawingIds.Contains(drawingIdentifier))
				{
					_excludedDrawingIds.Remove(drawingIdentifier);
				}
			}
			else if (!_excludedDrawingIds.Contains(drawingIdentifier))
			{
				_excludedDrawingIds.Add(drawingIdentifier);
			}
		}
		if (!recursive)
		{
			return;
		}
		foreach (Item item2 in item.Items)
		{
			RefreshExcludedDrawingIds(item2);
		}
	}

	private void RefreshExcludedDrawingIds(GeneratedMaterial item)
	{
		if (item.IsIncluded.HasValue && item.IsIncluded.Value)
		{
			if (_excludedDrawingIds.Contains(item.Tag))
			{
				_excludedDrawingIds.Remove(item.Tag);
			}
		}
		else if (!_excludedDrawingIds.Contains(item.Tag))
		{
			_excludedDrawingIds.Add(item.Tag);
		}
	}

	private void RefreshExcludedDrawingIds()
	{
		foreach (Item item in Model.Items)
		{
			RefreshExcludedDrawingIds(item, recursive: true);
		}
		foreach (GeneratedMaterial generatedMaterial in Model.GeneratedMaterials)
		{
			RefreshExcludedDrawingIds(generatedMaterial);
		}
	}

	private void IncludeItem(Item item, bool include, bool recursive)
	{
		item.IsIncluded = include;
		if (item.GeneratedMaterials.Count > 0)
		{
			IncludeGeneratedMaterialsOfItem(item, include, ShowComponents);
		}
		RefreshExcludedDrawingIds(item);
		if (!recursive)
		{
			return;
		}
		foreach (Item item2 in item.Items)
		{
			IncludeItem(item2, include, recursive);
		}
	}

	private void ExecuteIncludeItemCommand(object parameter)
	{
		_View.ShowCursorWait(show: true);
		try
		{
			if (parameter is Item item)
			{
				bool include = false;
				if (item.IsIncluded.HasValue)
				{
					include = item.IsIncluded.Value;
				}
				IncludeItem(item, include, IsModeRecursive);
				RefreshCurrentMaterialsTitle();
				RefreshModelDrawing(_excludedDrawingIds);
				_View.RebindMaterials();
			}
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefGest");
			throw;
		}
		finally
		{
			_View.ShowCursorWait(show: false);
		}
	}

	private void IncludeGeneratedMaterialsOfItem(Item item, bool include, bool includeComponents)
	{
		foreach (GeneratedMaterial generatedMaterial in item.GeneratedMaterials)
		{
			IncludeGeneratedMaterial(generatedMaterial, include, includeComponents);
		}
	}

	private void IncludeGeneratedMaterial(GeneratedMaterial generatedMaterial, bool include, bool includeComponents)
	{
		generatedMaterial.IsIncluded = ((!generatedMaterial.IsComponent) ? include : (include && includeComponents));
		UpdateGeneratedMaterialQuantityIncluded(generatedMaterial);
		RefreshExcludedDrawingIds(generatedMaterial);
	}

	private void UpdateGeneratedMaterialQuantityIncluded(GeneratedMaterial generatedMaterial)
	{
		if (generatedMaterial.IsIncluded.HasValue)
		{
			if (generatedMaterial.IsIncluded.Value)
			{
				generatedMaterial.QuantityIncluded = generatedMaterial.QuantityTotal;
			}
			else
			{
				generatedMaterial.QuantityIncluded = 0;
			}
		}
		else
		{
			generatedMaterial.IsIncluded = false;
			generatedMaterial.QuantityIncluded = 0;
		}
	}

	private void ExecuteIncludeMaterialCommand(object parameter)
	{
		if (parameter is GeneratedMaterial generatedMaterial)
		{
			UpdateGeneratedMaterialQuantityIncluded(generatedMaterial);
			RefreshExcludedDrawingIds(generatedMaterial);
			RefreshCurrentMaterialsTitle();
			RefreshModelDrawing(_excludedDrawingIds);
		}
	}

	private bool CanIncludeAllItemsCommandExecute()
	{
		if (ReadOnly)
		{
			return false;
		}
		if (Model != null)
		{
			foreach (Item item in Model.Items)
			{
				if (IsDescendantOrSelfIncluded(item, isIncluded: false))
				{
					return true;
				}
			}
		}
		return false;
	}

	private bool CanExcludeAllItemsCommandExecute()
	{
		if (ReadOnly)
		{
			return false;
		}
		if (Model != null)
		{
			foreach (Item item in Model.Items)
			{
				if (IsDescendantOrSelfIncluded(item, isIncluded: true))
				{
					return true;
				}
			}
		}
		return false;
	}

	private void ExecuteIncludeAllItemsCommand()
	{
		_View.ShowCursorWait(show: true);
		try
		{
			foreach (Item item in Model.Items)
			{
				IncludeItem(item, include: true, recursive: true);
			}
			RefreshModelDrawing(_excludedDrawingIds);
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefGest");
			throw;
		}
		finally
		{
			_View.ShowCursorWait(show: false);
		}
	}

	private void ExecuteExcludeAllItemsCommand()
	{
		_View.ShowCursorWait(show: true);
		try
		{
			foreach (Item item in Model.Items)
			{
				IncludeItem(item, include: false, recursive: true);
			}
			RefreshModelDrawing(_excludedDrawingIds);
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefGest");
			throw;
		}
		finally
		{
			_View.ShowCursorWait(show: false);
		}
	}

	private bool IsDescendantOrSelfIncluded(Item item, bool isIncluded)
	{
		if (item.IsIncluded == isIncluded)
		{
			return true;
		}
		foreach (Item item2 in item.Items)
		{
			if (IsDescendantOrSelfIncluded(item2, isIncluded))
			{
				return true;
			}
		}
		return false;
	}

	private void RefreshShownMaterials(Model model)
	{
		_Components.Clear();
		ShownMaterials.Clear();
		foreach (GeneratedMaterial generatedMaterial in model.GeneratedMaterials)
		{
			if (generatedMaterial.IsComponent)
			{
				_Components.Add(generatedMaterial);
				if (!ShowComponents)
				{
					continue;
				}
			}
			ShownMaterials.Add(generatedMaterial);
		}
		RefreshCurrentMaterialsTitle();
	}

	private void RefreshModelGeneratedMaterials(XmlDocument xmldoc, XmlNamespaceManager nsmgr, List<string> dummyReferences)
	{
		Stopwatch stopwatch = new Stopwatch();
		stopwatch.Start();
		try
		{
			Model.GeneratedMaterials.Clear();
			string xpath = "descendant::dsc:GeneratedMaterial | descendant::dsc:FilteredGeneratedMaterial";
			foreach (XmlNode item in xmldoc.SelectNodes(xpath, nsmgr))
			{
				GeneratedMaterial generatedMaterial = GetGeneratedMaterial(item, xmldoc, nsmgr, dummyReferences);
				if (generatedMaterial != null)
				{
					Model.GeneratedMaterials.Add(generatedMaterial);
				}
			}
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefGest");
			throw;
		}
		finally
		{
			stopwatch.Stop();
			TimeSpan elapsed = stopwatch.Elapsed;
			string arg = $"{elapsed.Hours:00}:{elapsed.Minutes:00}:{elapsed.Seconds:00}.{elapsed.Milliseconds / 10:00}";
			Logger.Instance.WriteInformation($"RefreshGeneratedMaterials RunTime {arg}", "PrefGest");
		}
	}

	private void RefreshCurrentMaterialsTitle()
	{
		if (_isLoadingData)
		{
			CurrentMaterialsTitle = string.Empty;
			return;
		}
		int generatedMaterialsCount = GetGeneratedMaterialsCount(isIncluded: true);
		CurrentMaterialsTitle = string.Format(Resources.MaterialsTitleAll, generatedMaterialsCount, ShownMaterials.Count);
	}

	private void RefreshItems(XmlDocument xmldoc, XmlNamespaceManager nsmgr)
	{
		Stopwatch stopwatch = new Stopwatch();
		stopwatch.Start();
		try
		{
			RefreshItems2(xmldoc, nsmgr, Model);
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefGest");
			throw;
		}
		finally
		{
			stopwatch.Stop();
			TimeSpan elapsed = stopwatch.Elapsed;
			string arg = $"{elapsed.Hours:00}:{elapsed.Minutes:00}:{elapsed.Seconds:00}.{elapsed.Milliseconds / 10:00}";
			Logger.Instance.WriteInformation($"RefreshItems RunTime {arg}", "PrefGest");
		}
	}

	private void LoadDescriptiveXmlStructureNodeDocumentXml(DescriptiveXmlStructureNode nodeRoot)
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		DescriptiveXmlStructureNodeDocumentXmlBuilder descriptiveXmlStructureNodeDocumentXmlBuilder = new DescriptiveXmlStructureNodeDocumentXmlBuilder();
		DescriptiveXmlStructureNodeXmlBuilder parentXmlBuilder = descriptiveXmlStructureNodeDocumentXmlBuilder.AddDescriptiveXmlStructureNode(nodeRoot.get_AllMaterialsCount(), nodeRoot.get_GenerationMethod(), nodeRoot.get_Id(), nodeRoot.get_MaterialsCount(), nodeRoot.get_Name(), nodeRoot.get_SquareId(), Convert.ToString(nodeRoot.get_Type()));
		Model.StructureDigest = nodeRoot.get_Digest();
		foreach (DescriptiveXmlStructureNode child in nodeRoot.get_Children())
		{
			LoadDescriptiveXmlStructureNodeXml(child, parentXmlBuilder);
		}
		_strDescriptiveXmlStructureNodeDocumentXml = descriptiveXmlStructureNodeDocumentXmlBuilder.OuterXml;
	}

	private void LoadDescriptiveXmlStructureNodeXml(DescriptiveXmlStructureNode node, DescriptiveXmlStructureNodeXmlBuilder parentXmlBuilder)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		DescriptiveXmlStructureNodeXmlBuilder parentXmlBuilder2 = parentXmlBuilder.AddDescriptiveXmlStructureNode(node.get_AllMaterialsCount(), node.get_GenerationMethod(), node.get_Id(), node.get_MaterialsCount(), node.get_Name(), node.get_SquareId(), Convert.ToString(node.get_Type()));
		foreach (DescriptiveXmlStructureNode child in node.get_Children())
		{
			LoadDescriptiveXmlStructureNodeXml(child, parentXmlBuilder2);
		}
	}

	private void RefreshItems2(XmlDocument xmldoc, XmlNamespaceManager nsmgr, Model model)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			model.Items.Clear();
			DescriptiveXmlStructureNode val = new DescriptiveXmlStructureBuilder().Build(xmldoc.OuterXml);
			LoadDescriptiveXmlStructureNodeDocumentXml(val);
			RefreshItems2(xmldoc, nsmgr, val, null);
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefGest");
			throw;
		}
	}

	private bool IsVisible(DescriptiveXmlStructureNode node)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Invalid comparison between Unknown and I4
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Invalid comparison between Unknown and I4
		if (node.get_Children().Count == 0)
		{
			return false;
		}
		if ((int)node.get_Type() == 10)
		{
			if (node.get_Children().Count != 1)
			{
				foreach (DescriptiveXmlStructureNode child in node.get_Children())
				{
					if (IsVisible(child))
					{
						return true;
					}
				}
				return false;
			}
			if ((int)node.get_Children().ElementAt(0).get_Type() == 10)
			{
				return false;
			}
		}
		return true;
	}

	private void RefreshItems2(XmlDocument xmldoc, XmlNamespaceManager nsmgr, DescriptiveXmlStructureNode node, Item parent)
	{
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Invalid comparison between Unknown and I4
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Invalid comparison between Unknown and I4
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Invalid comparison between Unknown and I4
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Invalid comparison between Unknown and I4
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Invalid comparison between Unknown and I4
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Invalid comparison between Unknown and I4
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Invalid comparison between Unknown and I4
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Invalid comparison between Unknown and I4
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Invalid comparison between Unknown and I4
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Invalid comparison between Unknown and I4
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Invalid comparison between Unknown and I4
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Invalid comparison between Unknown and I4
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Invalid comparison between Unknown and I4
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Invalid comparison between Unknown and I4
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Invalid comparison between Unknown and I4
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Invalid comparison between Unknown and I4
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Invalid comparison between Unknown and I4
		try
		{
			if (!IsVisible(node))
			{
				foreach (DescriptiveXmlStructureNode child in node.get_Children())
				{
					RefreshItems2(xmldoc, nsmgr, child, parent);
				}
				return;
			}
			Item item = null;
			if ((int)node.get_Type() == 1)
			{
				item = new AluClip(parent);
			}
			else if ((int)node.get_Type() == 2)
			{
				item = new Clones(parent);
			}
			else if ((int)node.get_Type() == 3)
			{
				item = new Contour(parent);
			}
			else if ((int)node.get_Type() == 4)
			{
				item = new Delimiter(parent);
			}
			else if ((int)node.get_Type() == 5)
			{
				item = new FrameHardware(parent);
			}
			else if ((int)node.get_Type() == 7)
			{
				item = new Glass(parent);
			}
			else if ((int)node.get_Type() == 8)
			{
				item = new GlazingLedge(parent);
			}
			else if ((int)node.get_Type() == 9)
			{
				item = new GlobalScript(parent);
			}
			else if ((int)node.get_Type() == 10)
			{
				item = new Hole(parent);
			}
			else if ((int)node.get_Type() == 11)
			{
				item = new Modelo(parent);
			}
			else if ((int)node.get_Type() == 12)
			{
				item = new OuterRod(parent);
			}
			else if ((int)node.get_Type() == 13)
			{
				item = new ProfilePiece(parent);
				foreach (DescriptiveXmlStructureNode child2 in node.get_Children())
				{
					if ((int)child2.get_Type() == 6 && child2.get_GenerationMethod() == "self")
					{
						string xpath = string.Format("/Model/dsc:Model/dsc:Contour/descendant::dsc:GeneratedMaterial[@id = '{0}']/parent::dsc:ProfilePiece | /Model/dsc:Model/dsc:Contour/descendant::dsc:FilteredGeneratedMaterial[@id = '{0}']/parent::dsc:ProfilePiece", child2.get_Id());
						XmlNode xmlNode = xmldoc.SelectSingleNode(xpath, nsmgr);
						if (xmlNode != null)
						{
							ProfilePiece obj = item as ProfilePiece;
							obj.Angle = Convert.ToDouble(xmlNode.Attributes.GetNamedItem("angle").Value, CultureInfo.InvariantCulture);
							obj.Order = Convert.ToInt32(xmlNode.Attributes.GetNamedItem("order").Value);
						}
					}
				}
			}
			else if ((int)node.get_Type() == 14)
			{
				item = new Rod(parent);
			}
			else if ((int)node.get_Type() == 15)
			{
				item = new Roller(parent);
			}
			else if ((int)node.get_Type() == 16)
			{
				item = new SashHardware(parent);
			}
			else if ((int)node.get_Type() == 17)
			{
				item = new Submodel(parent);
			}
			if (item == null)
			{
				return;
			}
			item.ElementName = node.get_Name();
			item.ID = node.get_Id();
			if (!string.IsNullOrEmpty(node.get_SquareId()))
			{
				item.SquareID = node.get_SquareId();
			}
			if (!string.IsNullOrEmpty(node.get_Id()))
			{
				item.DrawingIdentifiers.Add(node.get_Id());
			}
			LoadGeneratedMaterials(node, ref item);
			if (parent != null)
			{
				parent.Items.Add(item);
			}
			else
			{
				Model.Items.Add(item);
			}
			foreach (DescriptiveXmlStructureNode child3 in node.get_Children())
			{
				RefreshItems2(xmldoc, nsmgr, child3, item);
			}
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefGest");
			throw;
		}
	}

	private void LoadGeneratedMaterials(DescriptiveXmlStructureNode node, ref Item item)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Invalid comparison between Unknown and I4
		try
		{
			foreach (DescriptiveXmlStructureNode child in node.get_Children())
			{
				if ((int)child.get_Type() != 6)
				{
					continue;
				}
				GeneratedMaterial generatedMaterial = GetGeneratedMaterial(child.get_Id());
				if (generatedMaterial != null)
				{
					generatedMaterial.Element = item;
					if (!item.GeneratedMaterials.Contains(generatedMaterial))
					{
						item.GeneratedMaterials.Add(generatedMaterial);
					}
				}
			}
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefGest");
			throw;
		}
	}

	private GeneratedMaterial GetGeneratedMaterial(string id)
	{
		foreach (GeneratedMaterial generatedMaterial in Model.GeneratedMaterials)
		{
			if (generatedMaterial.ID == id)
			{
				return generatedMaterial;
			}
		}
		return null;
	}

	private void SelectItem(string itemID)
	{
		try
		{
			Item item = (SelectedItem = GetItemByID(itemID));
			List<Item> list = new List<Item>();
			Item item2 = SelectedItem;
			list.Add(item2);
			while (item2 != null)
			{
				item2 = item2.Parent;
				list.Add(item2);
			}
			if (list.Count > 0)
			{
				list.RemoveAt(list.Count - 1);
				for (int num = list.Count - 1; num >= 0; num--)
				{
					if (!list[num].IsExpanded)
					{
						list[num].IsExpanded = true;
					}
				}
			}
			_View.ScrollIntoViewItem(SelectedItem);
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefGest");
			throw;
		}
	}

	private Item GetItemByID(string itemID)
	{
		if (Model == null)
		{
			return null;
		}
		foreach (Item item in Model.Items)
		{
			Item itemByID = GetItemByID(item, itemID);
			if (itemByID != null)
			{
				return itemByID;
			}
		}
		return null;
	}

	private Item GetItemByID(Item item, string itemID)
	{
		if (item.ID == itemID)
		{
			return item;
		}
		foreach (Item item2 in item.Items)
		{
			Item itemByID = GetItemByID(item2, itemID);
			if (itemByID != null)
			{
				return itemByID;
			}
		}
		return null;
	}

	private SquareRoles GetSquareRole(string role)
	{
		if (role == "SASH")
		{
			return SquareRoles.Sash;
		}
		if (role == "GLAZING STOP")
		{
			return SquareRoles.GlazingStop;
		}
		return SquareRoles.Frame;
	}

	private void GenerateWpfDrawingOfModel()
	{
		Stopwatch stopwatch = new Stopwatch();
		stopwatch.Start();
		try
		{
			_strWPFDrawing = _ServiceAgent.GetWpfDrawing();
			Model.WpfDrawing = _strWPFDrawing;
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefCIM");
			throw;
		}
		finally
		{
			stopwatch.Stop();
			TimeSpan elapsed = stopwatch.Elapsed;
			string arg = $"{elapsed.Hours:00}:{elapsed.Minutes:00}:{elapsed.Seconds:00}.{elapsed.Milliseconds / 10:00}";
			Logger.Instance.WriteInformation($"GenerateWpfDrawingOfModel RunTime {arg}", "PrefGest");
		}
	}

	private void GenerateWpfDrawing(Item item)
	{
		_View.ShowCursorWait(show: true);
		try
		{
			string text = item.ID;
			if (!string.IsNullOrEmpty(item.SquareID))
			{
				text = item.SquareID;
			}
			if (!string.IsNullOrEmpty(text) && (text.Substring(0, 2) == "SQ" || (text.Substring(0, 2) != "GM" && text.Substring(0, 1) == "G")) && string.IsNullOrEmpty(item.WpfDrawing))
			{
				item.WpfDrawing = _ServiceAgent.GetWpfDrawing(text);
			}
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefGest");
		}
		finally
		{
			_View.ShowCursorWait(show: false);
		}
	}

	private Item GetItem(string selectedIdentifier)
	{
		if (Model == null)
		{
			return null;
		}
		using (IEnumerator<Item> enumerator = Model.Items.GetEnumerator())
		{
			if (enumerator.MoveNext())
			{
				Item current = enumerator.Current;
				foreach (string drawingIdentifier in current.DrawingIdentifiers)
				{
					if (drawingIdentifier == selectedIdentifier)
					{
						return current;
					}
				}
				return GetItem(current, selectedIdentifier);
			}
		}
		return null;
	}

	private Item GetItem(Item parent, string selectedIdentifier)
	{
		using (IEnumerator<Item> enumerator = parent.Items.GetEnumerator())
		{
			if (enumerator.MoveNext())
			{
				Item current = enumerator.Current;
				foreach (string drawingIdentifier in current.DrawingIdentifiers)
				{
					if (drawingIdentifier == selectedIdentifier)
					{
						return current;
					}
				}
				return GetItem(current, selectedIdentifier);
			}
		}
		return null;
	}

	private Square GetSquare(string selectedIdentifier)
	{
		if (Model == null)
		{
			return null;
		}
		using (IEnumerator<Square> enumerator = Model.Squares.GetEnumerator())
		{
			if (enumerator.MoveNext())
			{
				Square current = enumerator.Current;
				foreach (ProfilePiece profilePiece in current.ProfilePieces)
				{
					if (profilePiece.ID == selectedIdentifier)
					{
						return current;
					}
				}
				return GetSquare(current, selectedIdentifier);
			}
		}
		return null;
	}

	private GeneratedMaterial GetMaterial(string tag)
	{
		if (Model == null)
		{
			return null;
		}
		foreach (GeneratedMaterial generatedMaterial in Model.GeneratedMaterials)
		{
			if (generatedMaterial.Tag == tag)
			{
				return generatedMaterial;
			}
		}
		return null;
	}

	private Square GetSquare(Square parent, string selectedIdentifier)
	{
		using (IEnumerator<Square> enumerator = parent.Squares.GetEnumerator())
		{
			if (enumerator.MoveNext())
			{
				Square current = enumerator.Current;
				foreach (ProfilePiece profilePiece in current.ProfilePieces)
				{
					if (profilePiece.ID == selectedIdentifier)
					{
						return current;
					}
				}
				return GetSquare(current, selectedIdentifier);
			}
		}
		return null;
	}

	private string GetGlassID(XmlNode ndGenMaterial, XmlNamespaceManager nsmgr)
	{
		if (ndGenMaterial.ParentNode.Attributes.GetNamedItem("id") != null)
		{
			return ndGenMaterial.ParentNode.Attributes.GetNamedItem("id").Value;
		}
		return null;
	}

	private string GetProfilePieceID(XmlNode ndGenMaterial, XmlNamespaceManager nsmgr)
	{
		bool flag = false;
		if (ndGenMaterial.ParentNode != null && ndGenMaterial.ParentNode.ParentNode != null && ndGenMaterial.ParentNode.ParentNode.ParentNode != null && ndGenMaterial.ParentNode.ParentNode.ParentNode.Name == "dsc:Curve")
		{
			flag = true;
		}
		if (!flag)
		{
			if (ndGenMaterial.ParentNode.Attributes.GetNamedItem("id") != null)
			{
				return ndGenMaterial.ParentNode.Attributes.GetNamedItem("id").Value;
			}
		}
		else if (ndGenMaterial.Attributes.GetNamedItem("id") != null)
		{
			string xpath = "parent::dsc:ProfilePiece/parent::dsc:Rod/parent::dsc:Curve/child::dsc:ProfilePiece";
			return ndGenMaterial.SelectSingleNode(xpath, nsmgr).Attributes.GetNamedItem("id").Value;
		}
		return null;
	}

	private void RefreshModelDrawing(IList<string> excludedIdentifiers)
	{
		string strWPFDrawing = _strWPFDrawing;
		XmlDocument xmldoc = null;
		XmlNamespaceManager nsmgr = null;
		DrawingViewer.GetXamlDocumentSynchronous(strWPFDrawing, out xmldoc, out nsmgr);
		foreach (string excludedIdentifier in excludedIdentifiers)
		{
			string xpath = $"/descendant::i:Info[@Id = '{excludedIdentifier}']";
			XmlNodeList xmlNodeList = xmldoc.SelectNodes(xpath, nsmgr);
			while (xmlNodeList.Count > 0)
			{
				XmlNode xmlNode = xmlNodeList[0];
				while (xmlNode.Name != "Canvas")
				{
					xmlNode = xmlNode.ParentNode;
					if (xmlNode == null)
					{
						break;
					}
				}
				xmlNode?.ParentNode.RemoveChild(xmlNode);
				xmlNodeList = xmldoc.SelectNodes(xpath, nsmgr);
			}
		}
		Model.WpfDrawing = xmldoc.OuterXml;
	}

	private GeneratedMaterial GetGeneratedMaterial(XmlNode ndGenMaterial, XmlDocument xmldoc, XmlNamespaceManager nsmgr, List<string> dummyReferences)
	{
		try
		{
			string value = ndGenMaterial.Attributes.GetNamedItem("material").Value;
			if (string.IsNullOrEmpty(value))
			{
				return null;
			}
			string xpath = $"descendant::dsc:Materials/child::dsc:Material[@ref = '{value}']";
			XmlNode xmlNode = xmldoc.SelectSingleNode(xpath, nsmgr);
			string value2 = xmlNode.Attributes.GetNamedItem("baseRef").Value;
			if (dummyReferences.Contains(value2))
			{
				return null;
			}
			GeneratedMaterial generatedMaterial = new GeneratedMaterial();
			generatedMaterial.ID = ndGenMaterial.Attributes.GetNamedItem("id").Value;
			generatedMaterial.Reference = value;
			generatedMaterial.Color = ndGenMaterial.Attributes.GetNamedItem("color").Value;
			generatedMaterial.Description = xmlNode.Attributes.GetNamedItem("description").Value;
			int num = Convert.ToInt32(ndGenMaterial.Attributes.GetNamedItem("count").Value);
			int num2 = 0;
			if (ndGenMaterial.Attributes.GetNamedItem("filterDecrement") != null)
			{
				num2 = Convert.ToInt32(ndGenMaterial.Attributes.GetNamedItem("filterDecrement").Value);
			}
			int num4 = (generatedMaterial.QuantityTotal = num + num2);
			generatedMaterial.QuantityIncluded = num;
			if (ndGenMaterial.Attributes.GetNamedItem("square") != null)
			{
				generatedMaterial.SquareID = ndGenMaterial.Attributes.GetNamedItem("square").Value;
			}
			generatedMaterial.IsComponent = false;
			if (ndGenMaterial.Attributes.GetNamedItem("component") != null)
			{
				generatedMaterial.IsComponent = Convert.ToInt32(ndGenMaterial.Attributes.GetNamedItem("component").Value) >= 0;
			}
			switch (xmlNode.Attributes.GetNamedItem("type").Value)
			{
			case "rod":
				generatedMaterial.MaterialTypeString = Resources.Rod;
				generatedMaterial.MaterialType = MaterialTypes.Rod;
				generatedMaterial.Width = ConvertToUnitsModeString(ndGenMaterial, "long");
				if (ndGenMaterial.ParentNode.Name == "dsc:ProfilePiece")
				{
					generatedMaterial.Tag = GetProfilePieceID(ndGenMaterial, nsmgr);
				}
				break;
			case "meter":
				generatedMaterial.MaterialTypeString = Resources.Meter;
				generatedMaterial.MaterialType = MaterialTypes.Meter;
				generatedMaterial.Width = ConvertToUnitsModeString(ndGenMaterial, "long");
				break;
			case "surface":
				generatedMaterial.MaterialTypeString = Resources.Surface;
				generatedMaterial.MaterialType = MaterialTypes.Surface;
				generatedMaterial.Width = ConvertToUnitsModeString(ndGenMaterial, "width");
				generatedMaterial.Height = ConvertToUnitsModeString(ndGenMaterial, "height");
				if (ndGenMaterial.ParentNode.Name == "dsc:Glass")
				{
					generatedMaterial.Tag = GetGlassID(ndGenMaterial, nsmgr);
				}
				break;
			case "piece":
				generatedMaterial.MaterialTypeString = Resources.Piece;
				generatedMaterial.MaterialType = MaterialTypes.Piece;
				break;
			}
			IncludeGeneratedMaterial(generatedMaterial, include: true, ShowComponents);
			return generatedMaterial;
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefGest");
			throw;
		}
	}

	private string ConvertToUnitsModeString(XmlNode node, string attribute)
	{
		try
		{
			if (node.Attributes.GetNamedItem(attribute) == null)
			{
				return string.Empty;
			}
			if (!double.TryParse(node.Attributes.GetNamedItem(attribute).Value, NumberStyles.Number, CultureInfo.InvariantCulture, out var result))
			{
				return string.Empty;
			}
			return ConvertMillimetersToUnitsModeString(result);
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefGest");
			throw;
		}
	}

	private string ConvertMillimetersToUnitsModeString(double millimeters)
	{
		try
		{
			return UnitsModeConverter.MillimetersToUnitsModeString(millimeters, ServiceAgent.UnitsMode, CultureInfo.CurrentUICulture);
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefGest");
			throw;
		}
	}

	public string GetXmlDescriptiveFiltered()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			Filter filter = GetFilter();
			DescriptiveXmlFilter val = new DescriptiveXmlFilter();
			val.set_Filter(filter);
			string xmlDescriptive = ServiceAgent.XmlDescriptive;
			DescriptiveXmlFilterResult val2 = val.FilterXml(xmlDescriptive);
			if (val2.Success)
			{
				return val2.FilteredXml;
			}
			throw new Exception();
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefGest");
			throw;
		}
	}

	public void SetXmlFilter(string xmlFilter)
	{
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		Stopwatch stopwatch = new Stopwatch();
		stopwatch.Start();
		try
		{
			_strXmlFilter = xmlFilter;
			if (string.IsNullOrEmpty(xmlFilter) || Model == null || (Model.GeneratedMaterials.Count == 0 && Model.Items.Count == 0))
			{
				return;
			}
			Filter val = new Filter();
			val.set_SerializationXml(xmlFilter);
			FilterLogic logic = val.GetLogic((FilterLogicType)1);
			StructureFilterLogic val2 = (StructureFilterLogic)(object)((logic is StructureFilterLogic) ? logic : null);
			if (val2 == null)
			{
				return;
			}
			if (!string.IsNullOrWhiteSpace(val2.get_Digest()) && val2.get_Digest() != Model.StructureDigest)
			{
				_View.InformAboutWrongFilter(delegate
				{
				});
				return;
			}
			List<StructureFilterRule> rules = new List<StructureFilterRule>(val2.get_Excludes());
			List<StructureFilterRule> rules2 = new List<StructureFilterRule>(val2.get_Includes());
			if (val2.get_IsExcludingByDefault())
			{
				foreach (GeneratedMaterial generatedMaterial in Model.GeneratedMaterials)
				{
					generatedMaterial.QuantityIncluded = 0;
				}
				SetIsIncludedRecursive(Model.Items, isIncluded: false);
			}
			StructureFilterRule val3 = null;
			foreach (GeneratedMaterial generatedMaterial2 in Model.GeneratedMaterials)
			{
				val3 = GetRule((StructureFilterItemType)6, generatedMaterial2.ID, rules);
				if (val3 != null)
				{
					if (val3.IsPartial)
					{
						generatedMaterial2.QuantityIncluded = generatedMaterial2.QuantityTotal - val3.Count;
					}
					else
					{
						generatedMaterial2.QuantityIncluded = 0;
					}
				}
				val3 = GetRule((StructureFilterItemType)6, generatedMaterial2.ID, rules2);
				if (val3 != null)
				{
					if (val3.IsPartial)
					{
						generatedMaterial2.QuantityIncluded = val3.Count;
					}
					else
					{
						generatedMaterial2.QuantityIncluded = generatedMaterial2.QuantityTotal;
					}
				}
			}
			foreach (Item item in GetItems(Model.Items))
			{
				StructureFilterItemType? structureFilterItemType = GetStructureFilterItemType(item);
				if (structureFilterItemType.HasValue)
				{
					val3 = GetRule(structureFilterItemType.Value, item.ID, rules);
					if (val3 != null)
					{
						item.IsIncluded = false;
					}
					val3 = GetRule(structureFilterItemType.Value, item.ID, rules2);
					if (val3 != null)
					{
						item.IsIncluded = true;
					}
				}
			}
			RefreshCurrentMaterialsTitle();
			RefreshExcludedDrawingIds();
			RefreshModelDrawing(_excludedDrawingIds);
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefGest");
			throw;
		}
		finally
		{
			stopwatch.Stop();
			TimeSpan elapsed = stopwatch.Elapsed;
			string arg = $"{elapsed.Hours:00}:{elapsed.Minutes:00}:{elapsed.Seconds:00}.{elapsed.Milliseconds / 10:00}";
			Logger.Instance.WriteInformation($"SetXmlFilter RunTime {arg}", "PrefGest");
		}
	}

	private StructureFilterRule GetRule(StructureFilterItemType type, string id, List<StructureFilterRule> rules)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		foreach (StructureFilterRule rule in rules)
		{
			if (rule.Id == id && rule.Type == type)
			{
				return rule;
			}
		}
		return null;
	}

	private Filter GetFilter()
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Expected O, but got Unknown
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0359: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			int count = Model.GeneratedMaterials.Count;
			int itemsCount = GetItemsCount(Model.Items);
			int num = count + itemsCount;
			int generatedMaterialsCount = GetGeneratedMaterialsCount(isIncluded: true);
			int itemsCount2 = GetItemsCount(Model.Items, isIncluded: true);
			int num2 = generatedMaterialsCount + itemsCount2;
			Filter val = new Filter();
			FilterLogic obj = val.AddNewLogic((FilterLogicType)1);
			StructureFilterLogic val2 = (StructureFilterLogic)(object)((obj is StructureFilterLogic) ? obj : null);
			val2.set_Digest(Model.StructureDigest);
			if (num / 2 > num2)
			{
				val2.set_IsExcludingByDefault(true);
				foreach (GeneratedMaterial generatedMaterial in Model.GeneratedMaterials)
				{
					if (!generatedMaterial.IsIncluded.HasValue)
					{
						val2.AddInclude((StructureFilterItemType)6, generatedMaterial.ID, generatedMaterial.QuantityIncluded);
					}
					else if (generatedMaterial.IsIncluded.Value)
					{
						val2.AddInclude((StructureFilterItemType)6, generatedMaterial.ID);
					}
				}
				foreach (GeneratedMaterial component in _Components)
				{
					if (!Model.GeneratedMaterials.Contains(component))
					{
						if (!component.IsIncluded.HasValue)
						{
							val2.AddInclude((StructureFilterItemType)6, component.ID, component.QuantityIncluded);
						}
						else if (component.IsIncluded.Value)
						{
							val2.AddInclude((StructureFilterItemType)6, component.ID);
						}
					}
				}
				foreach (Item item in GetItems(Model.Items, isIncluded: true))
				{
					StructureFilterItemType? structureFilterItemType = GetStructureFilterItemType(item);
					if (structureFilterItemType.HasValue)
					{
						val2.AddInclude(structureFilterItemType.Value, item.ID);
					}
				}
			}
			else
			{
				val2.set_IsExcludingByDefault(false);
				foreach (GeneratedMaterial generatedMaterial2 in Model.GeneratedMaterials)
				{
					if (!generatedMaterial2.IsIncluded.HasValue)
					{
						val2.AddExclude((StructureFilterItemType)6, generatedMaterial2.ID, generatedMaterial2.QuantityTotal - generatedMaterial2.QuantityIncluded);
					}
					else if (!generatedMaterial2.IsIncluded.Value)
					{
						val2.AddExclude((StructureFilterItemType)6, generatedMaterial2.ID);
					}
				}
				foreach (GeneratedMaterial component2 in _Components)
				{
					if (!Model.GeneratedMaterials.Contains(component2))
					{
						if (!component2.IsIncluded.HasValue)
						{
							val2.AddExclude((StructureFilterItemType)6, component2.ID, component2.QuantityTotal - component2.QuantityIncluded);
						}
						else if (!component2.IsIncluded.Value)
						{
							val2.AddExclude((StructureFilterItemType)6, component2.ID);
						}
					}
				}
				foreach (Item item2 in GetItems(Model.Items, isIncluded: false))
				{
					StructureFilterItemType? structureFilterItemType2 = GetStructureFilterItemType(item2);
					if (structureFilterItemType2.HasValue)
					{
						val2.AddExclude(structureFilterItemType2.Value, item2.ID);
					}
				}
			}
			return val;
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefGest");
			throw;
		}
	}

	public string GetXmlFilter()
	{
		try
		{
			return GetFilter().get_SerializationXml();
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefGest");
			throw;
		}
	}

	private int GetGeneratedMaterialsCount(bool isIncluded)
	{
		if (Model == null)
		{
			return 0;
		}
		return Model.GeneratedMaterials.Count((GeneratedMaterial gm) => gm.IsIncluded == isIncluded);
	}

	private void SetIsIncludedRecursive(ICollection<Item> items, bool isIncluded)
	{
		foreach (Item item in items)
		{
			item.IsIncluded = isIncluded;
			SetIsIncludedRecursive(item.Items, isIncluded);
		}
	}

	private List<Item> GetItems(ICollection<Item> items)
	{
		List<Item> list = new List<Item>();
		foreach (Item item in items)
		{
			list.Add(item);
			foreach (Item item2 in GetItems(item.Items))
			{
				list.Add(item2);
			}
		}
		return list;
	}

	private List<Item> GetItems(ICollection<Item> items, bool isIncluded)
	{
		List<Item> list = new List<Item>();
		foreach (Item item in items)
		{
			if (item.IsIncluded == isIncluded)
			{
				list.Add(item);
			}
			foreach (Item item2 in GetItems(item.Items, isIncluded))
			{
				list.Add(item2);
			}
		}
		return list;
	}

	private int GetItemsCount(ICollection<Item> items, bool isIncluded)
	{
		int num = 0;
		foreach (Item item in items)
		{
			if (item.IsIncluded == isIncluded)
			{
				num++;
			}
			num += GetItemsCount(item.Items, isIncluded);
		}
		return num;
	}

	private int GetItemsCount(ICollection<Item> items)
	{
		int num = items.Count;
		foreach (Item item in items)
		{
			num += GetItemsCount(item.Items);
		}
		return num;
	}

	private StructureFilterItemType? GetStructureFilterItemType(Item item)
	{
		if (item.ItemType == ItemTypes.AluClip)
		{
			return (StructureFilterItemType)1;
		}
		if (item.ItemType == ItemTypes.Contour)
		{
			return (StructureFilterItemType)3;
		}
		if (item.ItemType == ItemTypes.Delimiter)
		{
			return (StructureFilterItemType)4;
		}
		if (item.ItemType == ItemTypes.FrameHardware)
		{
			return (StructureFilterItemType)5;
		}
		if (item.ItemType == ItemTypes.Glass)
		{
			return (StructureFilterItemType)7;
		}
		if (item.ItemType == ItemTypes.GlazingLedge)
		{
			return (StructureFilterItemType)8;
		}
		if (item.ItemType == ItemTypes.GlobalScript)
		{
			return (StructureFilterItemType)9;
		}
		if (item.ItemType == ItemTypes.Hole)
		{
			return (StructureFilterItemType)10;
		}
		if (item.ItemType == ItemTypes.Model)
		{
			return (StructureFilterItemType)11;
		}
		if (item.ItemType == ItemTypes.OuterRod)
		{
			return (StructureFilterItemType)12;
		}
		if (item.ItemType == ItemTypes.ProfilePiece)
		{
			return (StructureFilterItemType)13;
		}
		if (item.ItemType == ItemTypes.Rod)
		{
			return (StructureFilterItemType)14;
		}
		if (item.ItemType == ItemTypes.SashHardware)
		{
			return (StructureFilterItemType)16;
		}
		if (item.ItemType == ItemTypes.Submodel)
		{
			return (StructureFilterItemType)17;
		}
		return null;
	}
}
