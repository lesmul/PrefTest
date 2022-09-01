using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using Preference.Collections;
using Preference.Extensions;
using Preference.Wpf.Controls.Core.Commands;

namespace Preference.Wpf.Controls.Scheduler;

public class SchedulerViewModel : INotifyPropertyChanged
{
	private DateTime _currentDate;

	private int _nWeeksShownPreCurrentDate;

	private int _nWeeksShownPostCurrentDate;

	private SmartObservableCollection<SchedulerWeek> _colWeeks;

	private CultureInfo _cultureInfo;

	private ObservableCollection<IWeekDayScheduledItem> _scheduledItems;

	private ObservableCollection<IMonthViewDefinition> _monthViewDefinitions;

	private DelegateCommand _previousMonthCommand;

	private DelegateCommand _nextMonthCommand;

	public DateTime CurrentDate
	{
		get
		{
			return _currentDate;
		}
		set
		{
			if (!(_currentDate == value))
			{
				bool flag = true;
				if (DateTimeExtension.StartOfWeek(value, SchedulerCultureInfo.DateTimeFormat.FirstDayOfWeek) == DateTimeExtension.StartOfWeek(_currentDate, SchedulerCultureInfo.DateTimeFormat.FirstDayOfWeek))
				{
					flag = false;
				}
				_currentDate = value;
				if (flag)
				{
					ResetObservableCollection();
				}
				OnPropertyChanged("CurrentDate");
			}
		}
	}

	internal int WeeksShownPreCurrentDate
	{
		get
		{
			return _nWeeksShownPreCurrentDate;
		}
		set
		{
			if (_nWeeksShownPreCurrentDate != value)
			{
				_nWeeksShownPreCurrentDate = value;
				ResetObservableCollection();
			}
		}
	}

	internal int WeeksShownPostCurrentDate
	{
		get
		{
			return _nWeeksShownPostCurrentDate;
		}
		set
		{
			if (_nWeeksShownPostCurrentDate != value)
			{
				_nWeeksShownPostCurrentDate = value;
				ResetObservableCollection();
			}
		}
	}

	public SmartObservableCollection<SchedulerWeek> WeeksCollection
	{
		get
		{
			return _colWeeks;
		}
		set
		{
			if (_colWeeks != value)
			{
				_colWeeks = value;
				OnPropertyChanged("WeeksCollection");
			}
		}
	}

	private SchedulerWeekPool WeekPool { get; set; }

	public SchedulerWeekConfiguration WeekConfiguration { get; private set; }

	public CultureInfo SchedulerCultureInfo
	{
		get
		{
			return _cultureInfo;
		}
		set
		{
			if (_cultureInfo != value)
			{
				_cultureInfo = value;
				bool flag = WeekPool.ResetToCultureInfo(_cultureInfo, CurrentDate, WeeksShownPreCurrentDate, WeeksShownPostCurrentDate);
				WeekConfiguration.ResetCultureDependantInfo(_cultureInfo);
				if (flag)
				{
					ResetObservableCollection();
				}
				OnPropertyChanged("WeekConfiguration");
			}
		}
	}

	public ObservableCollection<IWeekDayScheduledItem> ScheduledItems
	{
		private get
		{
			return _scheduledItems;
		}
		set
		{
			if (_scheduledItems != value)
			{
				if (_scheduledItems != null)
				{
					_scheduledItems.CollectionChanged -= _scheduledItems_CollectionChanged;
				}
				_scheduledItems = value;
				_scheduledItems.CollectionChanged += _scheduledItems_CollectionChanged;
				WeekPool.ResetWeeksContent(_scheduledItems);
			}
		}
	}

	public ObservableCollection<IMonthViewDefinition> MonthViewDefinitions
	{
		private get
		{
			return _monthViewDefinitions;
		}
		set
		{
			if (_monthViewDefinitions != value)
			{
				_monthViewDefinitions = value;
				OnPropertyChanged("MonthViewDefinitions");
			}
		}
	}

	public DelegateCommand PreviousMonthCommand
	{
		get
		{
			if (_previousMonthCommand == null)
			{
				_previousMonthCommand = new DelegateCommand(ExecutePreviousMonthCommand);
			}
			return _previousMonthCommand;
		}
	}

	public DelegateCommand NextMonthCommand
	{
		get
		{
			if (_nextMonthCommand == null)
			{
				_nextMonthCommand = new DelegateCommand(ExecuteNextMonthCommand);
			}
			return _nextMonthCommand;
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	public event EventHandler<VisibleRange> VisibleRangeChange;

	public SchedulerViewModel()
	{
		_cultureInfo = CultureInfo.InvariantCulture;
		WeekConfiguration = new SchedulerWeekConfiguration(_cultureInfo);
		WeeksCollection = new SmartObservableCollection<SchedulerWeek>();
		WeekPool = new SchedulerWeekPool(_cultureInfo);
		_nWeeksShownPreCurrentDate = 1;
		_nWeeksShownPostCurrentDate = 1;
		_currentDate = DateTime.Today;
		ResetObservableCollection();
	}

	private void ResetObservableCollection()
	{
		IEnumerable<SchedulerWeek> enumerable = WeekPool.FillWeeks(CurrentDate, WeeksShownPreCurrentDate, WeeksShownPostCurrentDate);
		WeeksCollection.Reset(enumerable);
		OnVisibleRangeChange(new VisibleRange
		{
			StartDate = enumerable.First().StartingDay,
			EndDate = enumerable.Last().StartingDay.AddDays(6.0)
		});
	}

	protected void OnPropertyChanged(string name)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
	}

	private void _scheduledItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
	{
		WeekPool.ResetWeeksContent(_scheduledItems);
	}

	private void ExecutePreviousMonthCommand(object obj)
	{
		CurrentDate = CurrentDate.AddMonths(-1);
	}

	private void ExecuteNextMonthCommand(object obj)
	{
		CurrentDate = CurrentDate.AddMonths(1);
	}

	protected void OnVisibleRangeChange(VisibleRange visibleRange)
	{
		this.VisibleRangeChange?.Invoke(this, visibleRange);
	}
}
