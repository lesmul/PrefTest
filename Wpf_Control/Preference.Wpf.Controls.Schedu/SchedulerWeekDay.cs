using System;
using System.Collections.ObjectModel;

namespace Preference.Wpf.Controls.Scheduler;

public class SchedulerWeekDay
{
	public DateTime DayDate { get; set; }

	public ObservableCollection<IWeekDayScheduledItem> ScheduledItems { get; set; }

	public SchedulerWeekDay(DateTime date)
	{
		DayDate = date;
		ScheduledItems = new ObservableCollection<IWeekDayScheduledItem>();
	}
}
