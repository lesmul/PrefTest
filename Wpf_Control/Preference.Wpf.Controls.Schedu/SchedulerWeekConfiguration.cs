using System;
using System.Globalization;

namespace Preference.Wpf.Controls.Scheduler;

public class SchedulerWeekConfiguration
{
	public DayOfWeek FirstDay { get; private set; }

	public DayOfWeek SecondDay { get; private set; }

	public DayOfWeek ThirdDay { get; private set; }

	public DayOfWeek FourthDay { get; private set; }

	public DayOfWeek FifthDay { get; private set; }

	public DayOfWeek SixthDay { get; private set; }

	public DayOfWeek SeventhDay { get; private set; }

	public string FirstDayName { get; private set; }

	public string SecondDayName { get; private set; }

	public string ThirdDayName { get; private set; }

	public string FourthDayName { get; private set; }

	public string FifthDayName { get; private set; }

	public string SixthDayName { get; private set; }

	public string SeventhDayName { get; private set; }

	public SchedulerWeekConfiguration(CultureInfo _cultureInfo)
	{
		ResetCultureDependantInfo(_cultureInfo);
	}

	public void ResetCultureDependantInfo(CultureInfo schedulerCultureInfo)
	{
		FirstDay = schedulerCultureInfo.DateTimeFormat.FirstDayOfWeek;
		SecondDay = GetNextDay(FirstDay);
		ThirdDay = GetNextDay(SecondDay);
		FourthDay = GetNextDay(ThirdDay);
		FifthDay = GetNextDay(FourthDay);
		SixthDay = GetNextDay(FifthDay);
		SeventhDay = GetNextDay(SixthDay);
		FirstDayName = schedulerCultureInfo.DateTimeFormat.GetDayName(FirstDay);
		SecondDayName = schedulerCultureInfo.DateTimeFormat.GetDayName(SecondDay);
		ThirdDayName = schedulerCultureInfo.DateTimeFormat.GetDayName(ThirdDay);
		FourthDayName = schedulerCultureInfo.DateTimeFormat.GetDayName(FourthDay);
		FifthDayName = schedulerCultureInfo.DateTimeFormat.GetDayName(FifthDay);
		SixthDayName = schedulerCultureInfo.DateTimeFormat.GetDayName(SixthDay);
		SeventhDayName = schedulerCultureInfo.DateTimeFormat.GetDayName(SeventhDay);
	}

	private DayOfWeek GetNextDay(DayOfWeek weekDay)
	{
		return weekDay switch
		{
			DayOfWeek.Monday => DayOfWeek.Tuesday, 
			DayOfWeek.Tuesday => DayOfWeek.Wednesday, 
			DayOfWeek.Wednesday => DayOfWeek.Thursday, 
			DayOfWeek.Thursday => DayOfWeek.Friday, 
			DayOfWeek.Friday => DayOfWeek.Saturday, 
			DayOfWeek.Saturday => DayOfWeek.Sunday, 
			DayOfWeek.Sunday => DayOfWeek.Monday, 
			_ => weekDay, 
		};
	}
}
