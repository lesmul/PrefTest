using System;
using System.Windows;
using Telerik.Windows.Controls;

namespace Preference.Wpf.Controls.Scheduler;

public class WeekDayGridColumn : GridViewDataColumn
{
	public static readonly DependencyProperty WeekDayProperty = DependencyProperty.Register("WeekDay", typeof(DayOfWeek), typeof(WeekDayGridColumn), new PropertyMetadata(DayOfWeek.Sunday));

	public DayOfWeek WeekDay
	{
		get
		{
			return (DayOfWeek)((DependencyObject)this).GetValue(WeekDayProperty);
		}
		set
		{
			((DependencyObject)this).SetValue(WeekDayProperty, (object)value);
		}
	}
}
