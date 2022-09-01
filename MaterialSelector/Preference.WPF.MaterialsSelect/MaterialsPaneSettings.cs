using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.Win32;
using Preference.Diagnostics;
using Preference.WPF.MaterialsSelector.Core;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Data;

namespace Preference.WPF.MaterialsSelector.Settings;

public class MaterialsPaneSettings
{
	private const string REGISTRY_FILTER_BOM_NAME_SORT_DESCRIPTOR = "SortDescriptor";

	private const string REGISTRY_FILTER_BOM_NAME_GROUP_DESCRIPTORS = "GroupDescriptors";

	private const string REGISTRY_FILTER_BOM_NAME_RADGRIDVIEW = "MaterialsPaneGrid";

	public void SetSortDescriptor(string member, SortingState state)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Invalid comparison between Unknown and I4
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Invalid comparison between Unknown and I4
		try
		{
			RegistryHelper.SetValue("SOFTWARE\\Preference\\PrefGest\\PrefFilterBOM", "SortDescriptor", string.Empty, RegistryValueKind.String);
			if ((int)state == 0 || (int)state == 1)
			{
				ListSortDirection listSortDirection = ListSortDirection.Ascending;
				if ((int)state == 1)
				{
					listSortDirection = ListSortDirection.Descending;
				}
				string value = $"{member};{Convert.ToInt32(listSortDirection)}";
				RegistryHelper.SetValue("SOFTWARE\\Preference\\PrefGest\\PrefFilterBOM", "SortDescriptor", value, RegistryValueKind.String);
			}
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefGest");
		}
	}

	public SortDescriptor GetSortDescriptor()
	{
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Expected O, but got Unknown
		try
		{
			object value = RegistryHelper.GetValue("SOFTWARE\\Preference\\PrefGest\\PrefFilterBOM", "SortDescriptor");
			if (value != null)
			{
				string[] array = value.ToString().Split(new string[1] { ";" }, StringSplitOptions.RemoveEmptyEntries);
				if (array.Length == 2)
				{
					string member = array[0];
					int result = 0;
					if (int.TryParse(array[1], out result) && Enum.IsDefined(typeof(ListSortDirection), result))
					{
						ListSortDirection sortDirection = (ListSortDirection)result;
						SortDescriptor val = new SortDescriptor();
						val.set_Member(member);
						((SortDescriptorBase)val).set_SortDirection(sortDirection);
						return val;
					}
				}
			}
			return null;
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefGest");
			return null;
		}
	}

	public void SetGroupDescriptors(GroupDescriptorCollection groupDescriptors)
	{
		try
		{
			RegistryHelper.SetValue("SOFTWARE\\Preference\\PrefGest\\PrefFilterBOM", "GroupDescriptors", string.Empty, RegistryValueKind.String);
			string text = string.Empty;
			for (int i = 0; i < ((Collection<IGroupDescriptor>)(object)groupDescriptors).Count; i++)
			{
				if (((Collection<IGroupDescriptor>)(object)groupDescriptors)[i] is ColumnGroupDescriptor)
				{
					IGroupDescriptor obj = ((Collection<IGroupDescriptor>)(object)groupDescriptors)[i];
					IGroupDescriptor obj2 = ((obj is ColumnGroupDescriptor) ? obj : null);
					ListSortDirection? sortDirection = ((GroupDescriptorBase)obj2).get_SortDirection();
					string uniqueName = ((ColumnGroupDescriptor)obj2).get_Column().get_UniqueName();
					string empty = string.Empty;
					empty = ((!sortDirection.HasValue) ? $"{uniqueName};null" : $"{uniqueName};{Convert.ToInt32(sortDirection.Value)}");
					text = text + "|" + empty;
				}
				else if (((Collection<IGroupDescriptor>)(object)groupDescriptors)[i] is GroupDescriptor)
				{
					IGroupDescriptor obj3 = ((Collection<IGroupDescriptor>)(object)groupDescriptors)[i];
					IGroupDescriptor obj4 = ((obj3 is GroupDescriptor) ? obj3 : null);
					ListSortDirection? sortDirection2 = ((GroupDescriptorBase)obj4).get_SortDirection();
					string member = ((GroupDescriptor)obj4).get_Member();
					string empty2 = string.Empty;
					empty2 = ((!sortDirection2.HasValue) ? $"{member};null" : $"{member};{Convert.ToInt32(sortDirection2.Value)}");
					text = text + "|" + empty2;
				}
			}
			RegistryHelper.SetValue("SOFTWARE\\Preference\\PrefGest\\PrefFilterBOM", "GroupDescriptors", text, RegistryValueKind.String);
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefGest");
		}
	}

	public IList<GroupDescriptor> GetGroupDescriptors()
	{
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Expected O, but got Unknown
		try
		{
			IList<GroupDescriptor> list = new List<GroupDescriptor>();
			object value = RegistryHelper.GetValue("SOFTWARE\\Preference\\PrefGest\\PrefFilterBOM", "GroupDescriptors");
			if (value != null)
			{
				string[] array = value.ToString().Split(new string[1] { "|" }, StringSplitOptions.RemoveEmptyEntries);
				for (int i = 0; i < array.Length; i++)
				{
					string[] array2 = array[i].Split(new string[1] { ";" }, StringSplitOptions.None);
					if (array2.Length == 2)
					{
						string member = array2[0];
						ListSortDirection? sortDirection = null;
						int result = 0;
						if (int.TryParse(array2[1], out result) && Enum.IsDefined(typeof(ListSortDirection), result))
						{
							sortDirection = (ListSortDirection)result;
						}
						GroupDescriptor val = new GroupDescriptor();
						val.set_Member(member);
						((GroupDescriptorBase)val).set_SortDirection(sortDirection);
						list.Add(val);
					}
				}
			}
			return list;
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefGest");
			return null;
		}
	}

	public void SetColumnWidth(string uniqueName, GridViewLength gridViewLength)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		RadGridViewSerializationHelper.SerializeColumnWidth("MaterialsPaneGrid", uniqueName, gridViewLength);
	}

	public void ApplySerializedColumnWidths(RadGridView gridview)
	{
		RadGridViewSerializationHelper.ApplySerializedColumnWidths("MaterialsPaneGrid", gridview);
	}
}
