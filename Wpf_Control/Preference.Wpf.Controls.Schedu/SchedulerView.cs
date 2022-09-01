using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
using Preference.Wpf.Controls.Core.Commands;

namespace Preference.Wpf.Controls.Scheduler;

public class SchedulerView : UserControl, IComponentConnector
{
	public static readonly DependencyProperty ItemDataTemplateProperty = DependencyProperty.Register("ItemDataTemplate", typeof(DataTemplate), typeof(SchedulerView));

	public static readonly DependencyProperty ItemDataTemplateSelectorProperty = DependencyProperty.Register("ItemDataTemplateSelector", typeof(DataTemplateSelector), typeof(SchedulerView));

	public static readonly DependencyProperty VisibleRangeChangeCommandProperty = DependencyProperty.Register("VisibleRangeChangeCommand", typeof(ICommand), typeof(SchedulerView), new PropertyMetadata(OnVisibleRangeChangeCommandChanged));

	private static readonly DependencyProperty VisibleIntervalStartProperty = DependencyProperty.Register("VisibleIntervalStart", typeof(DateTime), typeof(SchedulerView), new PropertyMetadata(DateTime.Today, OnVisibleIntervalStartChanged));

	private static readonly DependencyProperty VisibleIntervalEndProperty = DependencyProperty.Register("VisibleIntervalEnd", typeof(DateTime), typeof(SchedulerView), new PropertyMetadata(DateTime.Today.AddDays(5.0)));

	public static readonly DependencyProperty ScheduledItemsProperty = DependencyProperty.Register("ScheduledItems", typeof(ObservableCollection<IWeekDayScheduledItem>), typeof(SchedulerView), new PropertyMetadata(OnScheduledItemsChanged));

	public static readonly DependencyProperty MonthViewDefinitionsProperty = DependencyProperty.Register("MonthViewDefinitions", typeof(ObservableCollection<IMonthViewDefinition>), typeof(SchedulerView), new PropertyMetadata(OnMonthViewDefinitionsChanged));

	public static readonly DependencyProperty CurrentMonthViewDefinitionProperty = DependencyProperty.Register("CurrentMonthViewDefinition", typeof(IMonthViewDefinition), typeof(SchedulerView), new PropertyMetadata(OnCurrentMonthViewDefinitionChanged));

	private DelegateCommand _monthViewCommand;

	private static readonly DependencyPropertyKey MyActualWidthPropertyKey = DependencyProperty.RegisterReadOnly("MyActualWidth", typeof(double), typeof(SchedulerView), new PropertyMetadata());

	public static readonly DependencyProperty MyActualWidthProperty = MyActualWidthPropertyKey.DependencyProperty;

	public static readonly DependencyProperty CurrentDateProperty = DependencyProperty.Register("CurrentDate", typeof(DateTime), typeof(SchedulerView), new PropertyMetadata(DateTime.Today, OnCurrentDateChanged));

	private static readonly DependencyProperty InternalCurrentDateProperty = DependencyProperty.Register("InternalCurrentDate", typeof(DateTime), typeof(SchedulerView), new PropertyMetadata(DateTime.Today, OnInternalCurrentDateChanged));

	public static readonly DependencyProperty WeeksShownPreCurrentDateProperty = DependencyProperty.Register("WeeksShownPreCurrentDate", typeof(int), typeof(SchedulerView), new PropertyMetadata(1, OnWeeksShownPreCurrentDateChanged));

	public static readonly DependencyProperty WeeksShownPostCurrentDateProperty = DependencyProperty.Register("WeeksShownPostCurrentDate", typeof(int), typeof(SchedulerView), new PropertyMetadata(1, OnWeeksShownPostCurrentDateChanged));

	public static readonly DependencyProperty SchedulerCultureInfoProperty = DependencyProperty.Register("SchedulerCultureInfo", typeof(CultureInfo), typeof(SchedulerView), new PropertyMetadata(OnSchedulerCultureInfoChanged));

	internal SchedulerView WeekDaysScheduler;

	internal Button buttonArrowLeft;

	internal Button buttonArrowRight;

	private bool _contentLoaded;

	public DataTemplate ItemDataTemplate
	{
		get
		{
			return (DataTemplate)GetValue(ItemDataTemplateProperty);
		}
		set
		{
			SetValue(ItemDataTemplateProperty, value);
		}
	}

	public DataTemplateSelector ItemDataTemplateSelector
	{
		get
		{
			return (DataTemplateSelector)GetValue(ItemDataTemplateSelectorProperty);
		}
		set
		{
			SetValue(ItemDataTemplateSelectorProperty, value);
		}
	}

	public ICommand VisibleRangeChangeCommand
	{
		get
		{
			return (ICommand)GetValue(VisibleRangeChangeCommandProperty);
		}
		set
		{
			SetValue(VisibleRangeChangeCommandProperty, value);
		}
	}

	public DateTime VisibleIntervalStart
	{
		get
		{
			return (DateTime)GetValue(VisibleIntervalStartProperty);
		}
		set
		{
			SetValue(VisibleIntervalStartProperty, value);
		}
	}

	public DateTime VisibleIntervalEnd
	{
		get
		{
			return (DateTime)GetValue(VisibleIntervalEndProperty);
		}
		set
		{
			SetValue(VisibleIntervalEndProperty, value);
		}
	}

	public ObservableCollection<IWeekDayScheduledItem> ScheduledItems
	{
		get
		{
			return (ObservableCollection<IWeekDayScheduledItem>)GetValue(ScheduledItemsProperty);
		}
		set
		{
			SetValue(ScheduledItemsProperty, value);
		}
	}

	public ObservableCollection<IMonthViewDefinition> MonthViewDefinitions
	{
		get
		{
			return (ObservableCollection<IMonthViewDefinition>)GetValue(MonthViewDefinitionsProperty);
		}
		set
		{
			SetValue(MonthViewDefinitionsProperty, value);
		}
	}

	public IMonthViewDefinition CurrentMonthViewDefinition
	{
		get
		{
			return (IMonthViewDefinition)GetValue(CurrentMonthViewDefinitionProperty);
		}
		set
		{
			SetValue(CurrentMonthViewDefinitionProperty, value);
		}
	}

	public DelegateCommand MonthViewChangedCommand
	{
		get
		{
			if (_monthViewCommand == null)
			{
				_monthViewCommand = new DelegateCommand(ExecuteMonthViewChangedCommand);
			}
			return _monthViewCommand;
		}
	}

	public double MyActualWidth
	{
		get
		{
			return (double)GetValue(MyActualWidthProperty);
		}
		protected set
		{
			SetValue(MyActualWidthProperty, value);
		}
	}

	public DateTime CurrentDate
	{
		get
		{
			return (DateTime)GetValue(CurrentDateProperty);
		}
		set
		{
			SetValue(CurrentDateProperty, value);
		}
	}

	private DateTime InternalCurrentDate
	{
		get
		{
			return (DateTime)GetValue(InternalCurrentDateProperty);
		}
		set
		{
			SetValue(InternalCurrentDateProperty, value);
		}
	}

	public int WeeksShownPreCurrentDate
	{
		get
		{
			return (int)GetValue(WeeksShownPreCurrentDateProperty);
		}
		set
		{
			SetValue(WeeksShownPreCurrentDateProperty, value);
		}
	}

	public int WeeksShownPostCurrentDate
	{
		get
		{
			return (int)GetValue(WeeksShownPostCurrentDateProperty);
		}
		set
		{
			SetValue(WeeksShownPostCurrentDateProperty, value);
		}
	}

	public CultureInfo SchedulerCultureInfo
	{
		get
		{
			return (CultureInfo)GetValue(SchedulerCultureInfoProperty);
		}
		set
		{
			SetValue(SchedulerCultureInfoProperty, value);
		}
	}

	public SchedulerViewModel SchedulerVm { get; private set; }

	public SchedulerView()
	{
		SchedulerVm = new SchedulerViewModel();
		SchedulerVm.SchedulerCultureInfo = CultureInfo.CurrentUICulture;
		SchedulerVm.VisibleRangeChange += SchedulerVm_VisibleRangeChange;
		InitializeComponent();
		Binding binding = new Binding
		{
			Source = SchedulerVm,
			Path = new PropertyPath("CurrentDate"),
			Mode = BindingMode.TwoWay
		};
		SetBinding(InternalCurrentDateProperty, binding);
		if (base.Resources["dateTimeWithFormatStringConverter"] is DateTimeWithFormatStringConverter dateTimeWithFormatStringConverter)
		{
			dateTimeWithFormatStringConverter.CultureInfoUI = SchedulerVm.SchedulerCultureInfo;
		}
		if (base.Resources["weekLapseStringConverter"] is WeekLapseStringConverter weekLapseStringConverter)
		{
			weekLapseStringConverter.CultureInfoUI = SchedulerVm.SchedulerCultureInfo;
		}
	}

	private void SchedulerVm_VisibleRangeChange(object sender, VisibleRange range)
	{
		VisibleIntervalStart = range.StartDate;
		VisibleIntervalEnd = range.EndDate;
		ICommand visibleRangeChangeCommand = VisibleRangeChangeCommand;
		if (visibleRangeChangeCommand != null && visibleRangeChangeCommand.CanExecute(range))
		{
			visibleRangeChangeCommand.Execute(range);
		}
	}

	private static void OnVisibleRangeChangeCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (!(d is SchedulerView schedulerView))
		{
			return;
		}
		ICommand visibleRangeChangeCommand = schedulerView.VisibleRangeChangeCommand;
		if (visibleRangeChangeCommand != null)
		{
			VisibleRange parameter = new VisibleRange
			{
				StartDate = schedulerView.VisibleIntervalStart,
				EndDate = schedulerView.VisibleIntervalEnd
			};
			if (visibleRangeChangeCommand.CanExecute(parameter))
			{
				visibleRangeChangeCommand.Execute(parameter);
			}
		}
	}

	private static void OnVisibleIntervalStartChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		DateTime dateTime = (DateTime)e.NewValue;
		DateTime dateTime2 = (DateTime)e.OldValue;
	}

	private static void OnScheduledItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is SchedulerView schedulerView)
		{
			schedulerView.SchedulerVm.ScheduledItems = (ObservableCollection<IWeekDayScheduledItem>)e.NewValue;
		}
	}

	private static void OnMonthViewDefinitionsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is SchedulerView schedulerView)
		{
			schedulerView.SchedulerVm.MonthViewDefinitions = (ObservableCollection<IMonthViewDefinition>)e.NewValue;
		}
	}

	private static void OnCurrentMonthViewDefinitionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
	}

	private void ExecuteMonthViewChangedCommand(object obj)
	{
		if (obj is IMonthViewDefinition currentMonthViewDefinition)
		{
			CurrentMonthViewDefinition = currentMonthViewDefinition;
		}
	}

	private static void OnCurrentDateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is SchedulerView schedulerView)
		{
			schedulerView.SchedulerVm.CurrentDate = (DateTime)e.NewValue;
		}
	}

	private static void OnInternalCurrentDateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is SchedulerView schedulerView)
		{
			schedulerView.SetValue(CurrentDateProperty, e.NewValue);
		}
	}

	private static void OnWeeksShownPreCurrentDateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is SchedulerView schedulerView)
		{
			schedulerView.SchedulerVm.WeeksShownPreCurrentDate = (int)e.NewValue;
		}
	}

	private static void OnWeeksShownPostCurrentDateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is SchedulerView schedulerView)
		{
			schedulerView.SchedulerVm.WeeksShownPostCurrentDate = (int)e.NewValue;
		}
	}

	private static void OnSchedulerCultureInfoChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is SchedulerView schedulerView)
		{
			schedulerView.SchedulerVm.SchedulerCultureInfo = (CultureInfo)e.NewValue;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/Preference.Wpf.Controls;component/scheduler/schedulerview.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	internal Delegate _CreateDelegate(Type delegateType, string handler)
	{
		return Delegate.CreateDelegate(delegateType, this, handler);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	void IComponentConnector.Connect(int connectionId, object target)
	{
		switch (connectionId)
		{
		case 1:
			WeekDaysScheduler = (SchedulerView)target;
			break;
		case 2:
			buttonArrowLeft = (Button)target;
			break;
		case 3:
			buttonArrowRight = (Button)target;
			break;
		default:
			_contentLoaded = true;
			break;
		}
	}
}
