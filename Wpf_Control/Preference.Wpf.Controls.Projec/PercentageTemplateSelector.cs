using System.Windows;
using System.Windows.Controls;

namespace Preference.Wpf.Controls.Projects.AppLogic;

public class PercentageTemplateSelector : DataTemplateSelector
{
	public DataTemplate PercentageTemplateText { get; set; }

	public DataTemplate PercentageTemplate { get; set; }

	public override DataTemplate SelectTemplate(object item, DependencyObject container)
	{
		if (item != null && item is PrefProjectExpenditure)
		{
			PrefProjectExpenditure prefProjectExpenditure = item as PrefProjectExpenditure;
			if (prefProjectExpenditure.Key == "Total")
			{
				return PercentageTemplateText;
			}
			return PercentageTemplate;
		}
		return null;
	}
}
