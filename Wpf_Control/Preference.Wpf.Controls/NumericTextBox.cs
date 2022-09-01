using System.Globalization;
using System.Windows.Controls;
using System.Windows.Input;

namespace Preference.Wpf.Controls;

public class NumericTextBox : TextBox
{
	protected override void OnPreviewTextInput(TextCompositionEventArgs e)
	{
		e.Handled = !AreAllValidNumericChars(e.Text);
		base.OnPreviewTextInput(e);
	}

	private bool AreAllValidNumericChars(string str)
	{
		bool flag = true;
		if ((str == NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator) | (str == NumberFormatInfo.CurrentInfo.CurrencyGroupSeparator) | (str == NumberFormatInfo.CurrentInfo.CurrencySymbol) | (str == NumberFormatInfo.CurrentInfo.NegativeSign) | (str == NumberFormatInfo.CurrentInfo.NegativeInfinitySymbol) | (str == NumberFormatInfo.CurrentInfo.NumberDecimalSeparator) | (str == NumberFormatInfo.CurrentInfo.NumberGroupSeparator) | (str == NumberFormatInfo.CurrentInfo.PercentDecimalSeparator) | (str == NumberFormatInfo.CurrentInfo.PercentGroupSeparator) | (str == NumberFormatInfo.CurrentInfo.PercentSymbol) | (str == NumberFormatInfo.CurrentInfo.PerMilleSymbol) | (str == NumberFormatInfo.CurrentInfo.PositiveInfinitySymbol) | (str == NumberFormatInfo.CurrentInfo.PositiveSign))
		{
			return flag;
		}
		int length = str.Length;
		for (int i = 0; i < length; i++)
		{
			char c = str[i];
			flag &= char.IsDigit(c);
		}
		return flag;
	}
}
