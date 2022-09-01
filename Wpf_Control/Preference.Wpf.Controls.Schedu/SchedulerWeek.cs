using System;

namespace Preference.Wpf.Controls.Scheduler;

public class SchedulerWeek
{
	public SchedulerWeekDay Monday { get; set; }

	public SchedulerWeekDay Tuesday { get; set; }

	public SchedulerWeekDay Wednesday { get; set; }

	public SchedulerWeekDay Thursday { get; set; }

	public SchedulerWeekDay Friday { get; set; }

	public SchedulerWeekDay Saturday { get; set; }

	public SchedulerWeekDay Sunday { get; set; }

	public DateTime StartingDay { get; set; }

	public SchedulerWeek(DateTime startingDay, SchedulerWeekDay monday, SchedulerWeekDay tuesday, SchedulerWeekDay wednesday, SchedulerWeekDay thursday, SchedulerWeekDay friday, SchedulerWeekDay saturday, SchedulerWeekDay sunday)
	{
		Monday = monday;
		Tuesday = tuesday;
		Wednesday = wednesday;
		Thursday = thursday;
		Friday = friday;
		Saturday = saturday;
		Sunday = sunday;
		StartingDay = startingDay;
	}

	internal void ClearItems()
	{
		Monday.ScheduledItems.Clear();
		Tuesday.ScheduledItems.Clear();
		Wednesday.ScheduledItems.Clear();
		Thursday.ScheduledItems.Clear();
		Friday.ScheduledItems.Clear();
		Saturday.ScheduledItems.Clear();
		Sunday.ScheduledItems.Clear();
	}
}
