using System.ComponentModel;
using System.Globalization;
using ConvertMetricImperial;

namespace Preference.Wpf.Controls.Units;

public class UnitsValueData : INotifyPropertyChanged
{
	private double _dValue;

	private UnitsMode _unitsMode;

	private DataStoredKind _dsk;

	public double Value
	{
		get
		{
			return _dValue;
		}
		set
		{
			if (_dValue != value)
			{
				_dValue = value;
				OnPropertyChanged("Value");
				OnPropertyChanged("Text");
			}
		}
	}

	public UnitsMode UnitsMode
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return _unitsMode;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			if (_unitsMode != value)
			{
				_unitsMode = value;
				OnPropertyChanged("UnitsMode");
				OnPropertyChanged("Text");
			}
		}
	}

	public DataStoredKind Dsk
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return _dsk;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			if (_dsk != value)
			{
				_dsk = value;
				OnPropertyChanged("Dsk");
				OnPropertyChanged("Text");
			}
		}
	}

	public string Text
	{
		get
		{
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Invalid comparison between Unknown and I4
			//IL_0031: Expected I4, but got Unknown
			return CConvertMetricImperial.GetUnitsString(Value, (int)UnitsMode, Dsk, CConvertMetricImperial.GetDefaultDecimalsForUnitsMode(UnitsMode, (int)Dsk == 9), CultureInfo.CurrentUICulture);
		}
		set
		{
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0030: Unknown result type (might be due to invalid IL or missing references)
			if (!(Text == value))
			{
				if (string.IsNullOrWhiteSpace(value))
				{
					Value = 0.0;
				}
				else
				{
					Value = CConvertMetricImperial.GetMetricValueFromString(value, UnitsMode, Dsk, CultureInfo.CurrentUICulture);
				}
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	public UnitsValueData()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		_dValue = 0.0;
		_unitsMode = (UnitsMode)0;
	}

	protected void OnPropertyChanged(string name)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
	}
}
