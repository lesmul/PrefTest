using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Preference.Wpf.Controls.Scheduler;

internal class SchedulerDaysPool
{
	internal SortedDictionary<DateTime, SchedulerWeekDay> DaysPool { get; set; }

	internal SchedulerDaysPool()
	{
		DaysPool = new SortedDictionary<DateTime, SchedulerWeekDay>();
	}

	internal void ResetDaysContent(ObservableCollection<IWeekDayScheduledItem> scheduledItems)
	{
		foreach (KeyValuePair<DateTime, SchedulerWeekDay> item in DaysPool)
		{
			item.Value.ScheduledItems.Clear();
		}
		foreach (IWeekDayScheduledItem scheduledItem in scheduledItems)
		{
			SchedulerWeekDay weekDay = GetWeekDay(scheduledItem.ItemDate);
			weekDay.ScheduledItems.Add(scheduledItem);
		}
	}

	internal SchedulerWeekDay GetWeekDay(DateTime date)
	{
		if (!DaysPool.TryGetValue(date, out var value))
		{
			value = new SchedulerWeekDay(date);
			DaysPool.Add(date, value);
		}
		return value;
	}
}
