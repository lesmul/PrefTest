using System.Collections.Generic;
using Telerik.Windows.Data;

namespace Preference.Wpf.Controls.Core;

public class FilterSetting
{
	private List<object> selectedDistinctValue;

	public string ColumnUniqueName { get; set; }

	public List<object> SelectedDistinctValues
	{
		get
		{
			if (selectedDistinctValue == null)
			{
				selectedDistinctValue = new List<object>();
			}
			return selectedDistinctValue;
		}
	}

	public FilterDescriptorProxy Filter1 { get; set; }

	public FilterCompositionLogicalOperator FieldFilterLogicalOperator { get; set; }

	public FilterDescriptorProxy Filter2 { get; set; }
}
