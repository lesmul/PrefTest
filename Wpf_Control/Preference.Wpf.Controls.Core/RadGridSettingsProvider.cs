using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Data;
using Telerik.Windows.Persistence.Services;

namespace Preference.Wpf.Controls.Core;

public class RadGridSettingsProvider : ICustomPropertyProvider, IPersistenceProvider
{
	private bool _serializeColumnHeaders;

	public RadGridSettingsProvider(bool serializeColumnHeaders)
	{
		_serializeColumnHeaders = serializeColumnHeaders;
	}

	public CustomPropertyInfo[] GetCustomProperties()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Expected O, but got Unknown
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Expected O, but got Unknown
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Expected O, but got Unknown
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Expected O, but got Unknown
		return (CustomPropertyInfo[])(object)new CustomPropertyInfo[4]
		{
			new CustomPropertyInfo("Columns", typeof(List<ColumnProxy>)),
			new CustomPropertyInfo("SortDescriptors", typeof(List<SortDescriptorProxy>)),
			new CustomPropertyInfo("GroupDescriptors", typeof(List<GroupDescriptorProxy>)),
			new CustomPropertyInfo("FilterDescriptors", typeof(List<FilterSetting>))
		};
	}

	public void InitializeObject(object context)
	{
		if (context is RadGridView)
		{
			RadGridView val = (RadGridView)((context is RadGridView) ? context : null);
			((Collection<ISortDescriptor>)(object)((GridViewDataControl)val).get_SortDescriptors()).Clear();
			((Collection<IGroupDescriptor>)(object)((GridViewDataControl)val).get_GroupDescriptors()).Clear();
			(from c in ((IEnumerable)((GridViewDataControl)val).get_Columns()).OfType<GridViewColumn>()
				where c.get_ColumnFilterDescriptor().get_IsActive()
				select c).ToList().ForEach(delegate(GridViewColumn c)
			{
				c.ClearFilters();
			});
		}
	}

	public object InitializeValue(CustomPropertyInfo customPropertyInfo, object context)
	{
		return null;
	}

	public object ProvideValue(CustomPropertyInfo customPropertyInfo, object context)
	{
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Expected O, but got Unknown
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Expected O, but got Unknown
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
		RadGridView val = (RadGridView)((context is RadGridView) ? context : null);
		switch (customPropertyInfo.get_Name())
		{
		case "Columns":
		{
			List<ColumnProxy> list4 = new List<ColumnProxy>();
			{
				foreach (GridViewColumn item in (Collection<GridViewColumn>)(object)((GridViewDataControl)val).get_Columns())
				{
					if (_serializeColumnHeaders)
					{
						list4.Add(new ColumnProxy
						{
							UniqueName = item.get_UniqueName(),
							Header = item.get_Header().ToString(),
							DisplayOrder = item.get_DisplayIndex(),
							Width = item.get_Width()
						});
					}
					else
					{
						list4.Add(new ColumnProxy
						{
							UniqueName = item.get_UniqueName(),
							DisplayOrder = item.get_DisplayIndex(),
							Width = item.get_Width()
						});
					}
				}
				return list4;
			}
		}
		case "SortDescriptors":
		{
			List<SortDescriptorProxy> list3 = new List<SortDescriptorProxy>();
			{
				foreach (ColumnSortDescriptor item2 in (Collection<ISortDescriptor>)(object)((GridViewDataControl)val).get_SortDescriptors())
				{
					ColumnSortDescriptor val3 = item2;
					list3.Add(new SortDescriptorProxy
					{
						ColumnUniqueName = val3.get_Column().get_UniqueName(),
						SortDirection = ((SortDescriptorBase)val3).get_SortDirection()
					});
				}
				return list3;
			}
		}
		case "GroupDescriptors":
		{
			List<GroupDescriptorProxy> list2 = new List<GroupDescriptorProxy>();
			{
				foreach (ColumnGroupDescriptor item3 in (Collection<IGroupDescriptor>)(object)((GridViewDataControl)val).get_GroupDescriptors())
				{
					ColumnGroupDescriptor val2 = item3;
					list2.Add(new GroupDescriptorProxy
					{
						ColumnUniqueName = val2.get_Column().get_UniqueName(),
						SortDirection = ((GroupDescriptorBase)val2).get_SortDirection()
					});
				}
				return list2;
			}
		}
		case "FilterDescriptors":
		{
			List<FilterSetting> list = new List<FilterSetting>();
			{
				foreach (IColumnFilterDescriptor item4 in ((IEnumerable)((GridViewDataControl)val).get_FilterDescriptors()).OfType<IColumnFilterDescriptor>())
				{
					FilterSetting filterSetting = new FilterSetting();
					filterSetting.ColumnUniqueName = item4.get_Column().get_UniqueName();
					filterSetting.SelectedDistinctValues.AddRange(item4.get_DistinctFilter().get_DistinctValues());
					if (item4.get_FieldFilter().get_Filter1().get_IsActive())
					{
						filterSetting.Filter1 = new FilterDescriptorProxy();
						filterSetting.Filter1.Operator = item4.get_FieldFilter().get_Filter1().get_Operator();
						filterSetting.Filter1.Value = item4.get_FieldFilter().get_Filter1().get_Value();
						filterSetting.Filter1.IsCaseSensitive = item4.get_FieldFilter().get_Filter1().get_IsCaseSensitive();
					}
					filterSetting.FieldFilterLogicalOperator = item4.get_FieldFilter().get_LogicalOperator();
					if (item4.get_FieldFilter().get_Filter2().get_IsActive())
					{
						filterSetting.Filter2 = new FilterDescriptorProxy();
						filterSetting.Filter2.Operator = item4.get_FieldFilter().get_Filter2().get_Operator();
						filterSetting.Filter2.Value = item4.get_FieldFilter().get_Filter2().get_Value();
						filterSetting.Filter2.IsCaseSensitive = item4.get_FieldFilter().get_Filter2().get_IsCaseSensitive();
					}
					list.Add(filterSetting);
				}
				return list;
			}
		}
		default:
			return null;
		}
	}

	public void RestoreValue(CustomPropertyInfo customPropertyInfo, object context, object value)
	{
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Expected O, but got Unknown
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Expected O, but got Unknown
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_031b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Unknown result type (might be due to invalid IL or missing references)
		RadGridView val = (RadGridView)((context is RadGridView) ? context : null);
		switch (customPropertyInfo.get_Name())
		{
		case "Columns":
		{
			List<ColumnProxy> list2 = value as List<ColumnProxy>;
			{
				foreach (ColumnProxy item in list2)
				{
					GridViewColumn val3 = ((GridViewDataControl)val).get_Columns().get_Item(item.UniqueName);
					val3.set_DisplayIndex(item.DisplayOrder);
					if (_serializeColumnHeaders)
					{
						val3.set_Header((object)item.Header);
					}
					val3.set_Width(item.Width);
				}
				break;
			}
		}
		case "SortDescriptors":
		{
			((RadObservableCollection<ISortDescriptor>)(object)((GridViewDataControl)val).get_SortDescriptors()).SuspendNotifications();
			((Collection<ISortDescriptor>)(object)((GridViewDataControl)val).get_SortDescriptors()).Clear();
			List<SortDescriptorProxy> list3 = value as List<SortDescriptorProxy>;
			foreach (SortDescriptorProxy item2 in list3)
			{
				GridViewColumn column = ((GridViewDataControl)val).get_Columns().get_Item(item2.ColumnUniqueName);
				SortDescriptorCollection sortDescriptors = ((GridViewDataControl)val).get_SortDescriptors();
				ColumnSortDescriptor val4 = new ColumnSortDescriptor();
				val4.set_Column(column);
				((SortDescriptorBase)val4).set_SortDirection(item2.SortDirection);
				((Collection<ISortDescriptor>)(object)sortDescriptors).Add((ISortDescriptor)val4);
			}
			((RadObservableCollection<ISortDescriptor>)(object)((GridViewDataControl)val).get_SortDescriptors()).ResumeNotifications();
			break;
		}
		case "GroupDescriptors":
		{
			((RadObservableCollection<IGroupDescriptor>)(object)((GridViewDataControl)val).get_GroupDescriptors()).SuspendNotifications();
			((Collection<IGroupDescriptor>)(object)((GridViewDataControl)val).get_GroupDescriptors()).Clear();
			List<GroupDescriptorProxy> list4 = value as List<GroupDescriptorProxy>;
			foreach (GroupDescriptorProxy item3 in list4)
			{
				GridViewColumn column2 = ((GridViewDataControl)val).get_Columns().get_Item(item3.ColumnUniqueName);
				GroupDescriptorCollection groupDescriptors = ((GridViewDataControl)val).get_GroupDescriptors();
				ColumnGroupDescriptor val5 = new ColumnGroupDescriptor();
				val5.set_Column(column2);
				((GroupDescriptorBase)val5).set_SortDirection(item3.SortDirection);
				((Collection<IGroupDescriptor>)(object)groupDescriptors).Add((IGroupDescriptor)val5);
			}
			((RadObservableCollection<IGroupDescriptor>)(object)((GridViewDataControl)val).get_GroupDescriptors()).ResumeNotifications();
			break;
		}
		case "FilterDescriptors":
		{
			((RadObservableCollection<IFilterDescriptor>)(object)((GridViewDataControl)val).get_FilterDescriptors()).SuspendNotifications();
			foreach (GridViewColumn item4 in (Collection<GridViewColumn>)(object)((GridViewDataControl)val).get_Columns())
			{
				if (item4.get_ColumnFilterDescriptor().get_IsActive())
				{
					item4.ClearFilters();
				}
			}
			List<FilterSetting> list = value as List<FilterSetting>;
			foreach (FilterSetting item5 in list)
			{
				GridViewColumn val2 = ((GridViewDataControl)val).get_Columns().get_Item(item5.ColumnUniqueName);
				IColumnFilterDescriptor columnFilterDescriptor = val2.get_ColumnFilterDescriptor();
				foreach (object selectedDistinctValue in item5.SelectedDistinctValues)
				{
					columnFilterDescriptor.get_DistinctFilter().AddDistinctValue(selectedDistinctValue);
				}
				if (item5.Filter1 != null)
				{
					columnFilterDescriptor.get_FieldFilter().get_Filter1().set_Operator(item5.Filter1.Operator);
					columnFilterDescriptor.get_FieldFilter().get_Filter1().set_Value(item5.Filter1.Value);
					columnFilterDescriptor.get_FieldFilter().get_Filter1().set_IsCaseSensitive(item5.Filter1.IsCaseSensitive);
				}
				columnFilterDescriptor.get_FieldFilter().set_LogicalOperator(item5.FieldFilterLogicalOperator);
				if (item5.Filter2 != null)
				{
					columnFilterDescriptor.get_FieldFilter().get_Filter2().set_Operator(item5.Filter2.Operator);
					columnFilterDescriptor.get_FieldFilter().get_Filter2().set_Value(item5.Filter2.Value);
					columnFilterDescriptor.get_FieldFilter().get_Filter2().set_IsCaseSensitive(item5.Filter2.IsCaseSensitive);
				}
			}
			((RadObservableCollection<IFilterDescriptor>)(object)((GridViewDataControl)val).get_FilterDescriptors()).ResumeNotifications();
			break;
		}
		}
	}
}
