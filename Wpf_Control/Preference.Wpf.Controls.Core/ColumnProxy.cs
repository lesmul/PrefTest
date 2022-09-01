using Telerik.Windows.Controls;

namespace Preference.Wpf.Controls.Core;

public class ColumnProxy
{
	public string UniqueName { get; set; }

	public int DisplayOrder { get; set; }

	public string Header { get; set; }

	public GridViewLength Width { get; set; }
}
