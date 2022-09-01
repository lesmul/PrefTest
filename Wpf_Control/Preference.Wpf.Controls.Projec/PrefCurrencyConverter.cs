using System;
using System.Globalization;
using System.Windows.Data;

namespace Preference.Wpf.Controls.Projects.AppLogic;

[ValueConversion(typeof(PrefCurrency), typeof(string))]
public class PrefCurrencyConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		PrefCurrency prefCurrency = (PrefCurrency)value;
		return prefCurrency.Name;
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		string strB = value.ToString();
		foreach (PrefCurrency currency in PrefCurrencyList.Currencies)
		{
			if (string.Compare(currency.Name, strB, ignoreCase: true) == 0)
			{
				return currency;
			}
		}
		return value;
	}
}
