using System;

namespace Preference.Wpf.Controls.Scheduler;

public class TestData3 : IWeekDayScheduledItem
{
	public string Ciudad { get; set; }

	public string Pais { get; set; }

	public DateTime ItemDate { get; set; }

	public TestData3()
	{
		Ciudad = "Mordor";
		Pais = "España";
	}
}
