using System;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls.GridView;

namespace Preference.Wpf.Controls.Scheduler;

public class WeekDayDataTemplateSelector : DataTemplateSelector
{
	public DataTemplate MondayTemplate { get; set; }

	public DataTemplate TuesdayTemplate { get; set; }

	public DataTemplate WednesdayTemplate { get; set; }

	public DataTemplate ThursdayTemplate { get; set; }

	public DataTemplate FridayTemplate { get; set; }

	public DataTemplate SaturdayTemplate { get; set; }

	public DataTemplate SundayTemplate { get; set; }

	public override DataTemplate SelectTemplate(object item, DependencyObject container)
	{
		GridViewCell val = (GridViewCell)(object)((container is GridViewCell) ? container : null);
		if (val != null && ((GridViewCellBase)val).get_Column() is WeekDayGridColumn weekDayGridColumn)
		{
			switch (weekDayGridColumn.WeekDay)
			{
			case DayOfWeek.Monday:
				return MondayTemplate;
			case DayOfWeek.Tuesday:
				return TuesdayTemplate;
			case DayOfWeek.Wednesday:
				return WednesdayTemplate;
			case DayOfWeek.Thursday:
				return ThursdayTemplate;
			case DayOfWeek.Friday:
				return FridayTemplate;
			case DayOfWeek.Saturday:
				return SaturdayTemplate;
			case DayOfWeek.Sunday:
				return SundayTemplate;
			}
		}
		return base.SelectTemplate(item, container);
	}
}
