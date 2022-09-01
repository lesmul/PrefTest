using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Preference.Wpf.Controls.Units;

namespace Preference.Wpf.Controls.Behaviors;

public static class UnitsTextBoxBehavior
{
	internal static IEnumerable<string> RegExImperialFractions = new List<string> { "^-?[0-9]*(\\s(([1-9]|[1-5][0-9]|6[0-3])([/](1|2|3|4|6|8|16|32|64)?)?)?)?$", "^-?((([1-9]|[1-5][0-9]|6[0-3])([/](1|2|3|4|6|8|16|32|64)?)?)?)$" };

	public static readonly DependencyProperty OnlyUnitStringsAllowedProperty = DependencyProperty.RegisterAttached("OnlyUnitStringsAllowed", typeof(bool), typeof(UnitsTextBoxBehavior), new UIPropertyMetadata(false, OnOnlyUnitStringsAllowedChanged));

	internal static bool CheckUnitsTextBoxNewText(UnitsTextBox textBox, string strNewText, bool bEmptyAsTrue)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Invalid comparison between Unknown and I4
		if (textBox == null)
		{
			return true;
		}
		if ((int)textBox.UnitsMode == 2)
		{
			return RegExImperialFractions.Any((string regexp) => TextBoxBehavior.CheckTextBoxNewText(textBox, strNewText, regexp, bEmptyAsTrue: true));
		}
		return TextBoxBehavior.CheckTextBoxNewText(textBox, strNewText, TextBoxBehavior.GetRegExDoubleWriting(), bEmptyAsTrue: true);
	}

	public static bool GetOnlyUnitStringsAllowed(TextBox textBox)
	{
		return (bool)textBox.GetValue(OnlyUnitStringsAllowedProperty);
	}

	public static void SetOnlyUnitStringsAllowed(TextBox textBox, bool value)
	{
		textBox.SetValue(OnlyUnitStringsAllowedProperty, value);
	}

	private static void OnOnlyUnitStringsAllowedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is UnitsTextBox unitsTextBox && e.NewValue is bool)
		{
			if ((bool)e.NewValue)
			{
				unitsTextBox.PreviewTextInput += PreviewTextInputUnits;
				unitsTextBox.PreviewKeyDown += PreviewKeyDownUnitsTextBox;
				DataObject.AddPastingHandler(unitsTextBox, CheckPasteUnits);
			}
			else
			{
				unitsTextBox.PreviewTextInput -= PreviewTextInputUnits;
				unitsTextBox.PreviewKeyDown -= PreviewKeyDownUnitsTextBox;
				DataObject.RemovePastingHandler(unitsTextBox, CheckPasteUnits);
			}
		}
	}

	private static void PreviewKeyDownUnitsTextBox(object sender, KeyEventArgs e)
	{
		if (e.Key == Key.Space)
		{
			UnitsTextBox textBox = sender as UnitsTextBox;
			if (!CheckUnitsTextBoxNewText(textBox, " ", bEmptyAsTrue: true))
			{
				e.Handled = true;
			}
		}
	}

	private static void CheckPasteUnits(object sender, DataObjectPastingEventArgs e)
	{
		UnitsTextBox textBox = sender as UnitsTextBox;
		if (!CheckUnitsTextBoxNewText(textBox, e.DataObject.GetData(typeof(string)).ToString(), bEmptyAsTrue: true))
		{
			e.CancelCommand();
		}
	}

	private static void PreviewTextInputUnits(object sender, TextCompositionEventArgs e)
	{
		UnitsTextBox unitsTextBox = (UnitsTextBox)sender;
		string text = e.Text;
		if ((text == "." || text == ",") && e.Text != Thread.CurrentThread.CurrentUICulture.NumberFormat.NumberDecimalSeparator)
		{
			text = Thread.CurrentThread.CurrentUICulture.NumberFormat.NumberDecimalSeparator;
		}
		if (!CheckUnitsTextBoxNewText(unitsTextBox, text, bEmptyAsTrue: true))
		{
			e.Handled = true;
		}
		else if (string.CompareOrdinal(text, e.Text) != 0)
		{
			int selectionStart = unitsTextBox.SelectionStart;
			unitsTextBox.Text = unitsTextBox.Text.Insert(unitsTextBox.SelectionStart, text);
			unitsTextBox.SelectionStart = selectionStart + 1;
			e.Handled = true;
		}
	}
}
