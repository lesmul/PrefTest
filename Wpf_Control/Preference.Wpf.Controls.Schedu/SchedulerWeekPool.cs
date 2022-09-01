using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using Preference.Extensions;

namespace Preference.Wpf.Controls.Scheduler;

internal class SchedulerWeekPool
{
	internal SchedulerDaysPool DaysPool { get; private set; }

	internal SortedDictionary<DateTime, SchedulerWeek> WeeksPool { get; set; }

	private CultureInfo CurrentCulture { get; set; }

	internal SchedulerWeekPool(CultureInfo currentCulture)
	{
		CurrentCulture = currentCulture;
		DaysPool = new SchedulerDaysPool();
		WeeksPool = new SortedDictionary<DateTime, SchedulerWeek>();
	}

	internal void ResetWeeksContent(ObservableCollection<IWeekDayScheduledItem> scheduledItems)
	{
		DaysPool.ResetDaysContent(scheduledItems);
	}

	internal bool ResetToCultureInfo(CultureInfo _cultureInfo, DateTime currentDate, int weeksShownPreCurrentDate, int weeksShownPostCurrentDate)
	{
		if (_cultureInfo == null)
		{
			return false;
		}
		if (CurrentCulture != null && _cultureInfo.DateTimeFormat.FirstDayOfWeek == CurrentCulture.DateTimeFormat.FirstDayOfWeek)
		{
			CurrentCulture = _cultureInfo;
			return false;
		}
		WeeksPool.Clear();
		CurrentCulture = _cultureInfo;
		return true;
	}

	private SchedulerWeek CreateWeek(DateTime weekInitialDate)
	{
		SchedulerWeekDay monday = null;
		SchedulerWeekDay tuesday = null;
		SchedulerWeekDay wednesday = null;
		SchedulerWeekDay thursday = null;
		SchedulerWeekDay friday = null;
		SchedulerWeekDay saturday = null;
		SchedulerWeekDay sunday = null;
		for (int i = 0; i < 7; i++)
		{
			DateTime date = weekInitialDate.AddDays(i);
			SchedulerWeekDay weekDay = DaysPool.GetWeekDay(date);
			switch (date.DayOfWeek)
			{
			case DayOfWeek.Monday:
				monday = weekDay;
				break;
			case DayOfWeek.Tuesday:
				tuesday = weekDay;
				break;
			case DayOfWeek.Wednesday:
				wednesday = weekDay;
				break;
			case DayOfWeek.Thursday:
				thursday = weekDay;
				break;
			case DayOfWeek.Friday:
				friday = weekDay;
				break;
			case DayOfWeek.Saturday:
				saturday = weekDay;
				break;
			case DayOfWeek.Sunday:
				sunday = weekDay;
				break;
			}
		}
		return new SchedulerWeek(weekInitialDate, monday, tuesday, wednesday, thursday, friday, saturday, sunday);
	}

	internal IEnumerable<SchedulerWeek> FillWeeks(DateTime currentDate, int weeksShownPreCurrentDate, int weeksShownPostCurrentDate)
	{
		DateTime dateTime = DateTimeExtension.StartOfWeek(currentDate, CurrentCulture.DateTimeFormat.FirstDayOfWeek);
		DateTime dateTime2 = ((weeksShownPreCurrentDate > 0) ? dateTime.AddDays(-7 * weeksShownPreCurrentDate) : dateTime);
		DateTime dateTime3 = ((weeksShownPreCurrentDate > 0) ? dateTime.AddDays(7 * weeksShownPostCurrentDate) : dateTime);
		List<SchedulerWeek> list = new List<SchedulerWeek>();
		dateTime = dateTime2;
		while (dateTime <= dateTime3)
		{
			if (!WeeksPool.TryGetValue(dateTime, out var value))
			{
				value = CreateWeek(dateTime);
				WeeksPool.Add(dateTime, value);
			}
			list.Add(value);
			dateTime = dateTime.AddDays(7.0);
		}
		return list;
	}
}
