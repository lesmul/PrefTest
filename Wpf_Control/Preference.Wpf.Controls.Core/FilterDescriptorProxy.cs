using Telerik.Windows.Data;

namespace Preference.Wpf.Controls.Core;

public class FilterDescriptorProxy
{
	public FilterOperator Operator { get; set; }

	public object Value { get; set; }

	public bool IsCaseSensitive { get; set; }
}
