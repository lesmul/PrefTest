using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Preference.Wpf.Controls.Behaviors;

public static class TextBoxBehavior
{
	public static readonly DependencyProperty SelectedTextOnFocusProperty = DependencyProperty.RegisterAttached("SelectedTextOnFocus", typeof(bool), typeof(TextBoxBehavior), new UIPropertyMetadata(false, OnIsSelectedTextOnFocusChanged));

	public static readonly DependencyProperty TextTypesAllowedProperty = DependencyProperty.RegisterAttached("TextTypesAllowed", typeof(TextsAllowed), typeof(TextBoxBehavior), new UIPropertyMetadata(TextsAllowed.AllTexts, OnTextTypesAllowedChanged));

	internal static string RegExIntegerWriting = "^-?[0-9]{0,10}$";

	internal static string RegExPositiveIntegerWriting = "^[0-9]{0,10}$";

	public static bool GetSelectedTextOnFocus(TextBox textBox)
	{
		return (bool)textBox.GetValue(SelectedTextOnFocusProperty);
	}

	public static void SetSelectedTextOnFocus(TextBox textBox, bool value)
	{
		textBox.SetValue(SelectedTextOnFocusProperty, value);
	}

	private static void OnIsSelectedTextOnFocusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is TextBox textBox && e.NewValue is bool)
		{
			if ((bool)e.NewValue)
			{
				textBox.PreviewMouseLeftButtonDown += SelectivelyIgnoreMouseButton;
				textBox.GotKeyboardFocus += SelectAllText;
				textBox.MouseDoubleClick += SelectAllText;
			}
			else
			{
				textBox.PreviewMouseLeftButtonDown -= SelectivelyIgnoreMouseButton;
				textBox.GotKeyboardFocus -= SelectAllText;
				textBox.MouseDoubleClick -= SelectAllText;
			}
		}
	}

	private static void SelectivelyIgnoreMouseButton(object sender, MouseButtonEventArgs e)
	{
		DependencyObject dependencyObject = e.OriginalSource as UIElement;
		while (dependencyObject != null && !(dependencyObject is TextBox))
		{
			dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
		}
		if (dependencyObject != null)
		{
			TextBox textBox = (TextBox)dependencyObject;
			if (!textBox.IsKeyboardFocusWithin)
			{
				textBox.Focus();
				e.Handled = true;
			}
		}
	}

	private static void SelectAllText(object sender, RoutedEventArgs e)
	{
		if (e.OriginalSource is TextBox textBox)
		{
			textBox.SelectAll();
		}
	}

	internal static bool CheckTextBoxNewText(TextBox textBox, string strNewText, string strRegEx, bool bEmptyAsTrue)
	{
		if (textBox == null)
		{
			return true;
		}
		string currentText = GetCurrentText(textBox, strNewText);
		if (bEmptyAsTrue && currentText.Length == 0)
		{
			return true;
		}
		Regex regex = new Regex(strRegEx);
		return regex.IsMatch(currentText);
	}

	internal static string GetCurrentText(TextBox textBox, string strNewText)
	{
		if (textBox == null)
		{
			return "";
		}
		string text = textBox.Text;
		int caretIndex = textBox.CaretIndex;
		text = text.Remove(caretIndex, textBox.SelectionLength);
		return text.Insert(caretIndex, strNewText);
	}

	private static void PreviewKeyDownCancelSpace(object sender, KeyEventArgs e)
	{
		if (e.Key == Key.Space)
		{
			e.Handled = true;
		}
	}

	public static TextsAllowed GetTextTypesAllowed(TextBox textBox)
	{
		return (TextsAllowed)textBox.GetValue(TextTypesAllowedProperty);
	}

	public static void SetTextTypesAllowed(TextBox textBox, TextsAllowed value)
	{
		textBox.SetValue(TextTypesAllowedProperty, value);
	}

	private static void OnTextTypesAllowedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is TextBox textBox && e.NewValue is TextsAllowed)
		{
			if ((TextsAllowed)e.NewValue != 0)
			{
				textBox.PreviewTextInput += PreviewTextAllowedInput;
				textBox.PreviewKeyDown += PreviewKeyDownCancelSpace;
				DataObject.AddPastingHandler(textBox, CheckPasteTextAllowed);
			}
			else
			{
				textBox.PreviewTextInput -= PreviewTextAllowedInput;
				textBox.PreviewKeyDown -= PreviewKeyDownCancelSpace;
				DataObject.RemovePastingHandler(textBox, CheckPasteTextAllowed);
			}
		}
	}

	internal static string GetRegExDoubleWriting()
	{
		return "^-?[0-9]*([" + CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator + "][0-9]*)?$";
	}

	internal static string GetRegExForTextAllowed(TextsAllowed allowedText)
	{
		return allowedText switch
		{
			TextsAllowed.OnlyIntegers => RegExIntegerWriting, 
			TextsAllowed.OnlyPositiveIntegers => RegExPositiveIntegerWriting, 
			TextsAllowed.OnlyDoubles => GetRegExDoubleWriting(), 
			_ => "", 
		};
	}

	private static TextsAllowed GetTextBoxAllowedText(TextBox textBox)
	{
		if (textBox == null)
		{
			return TextsAllowed.AllTexts;
		}
		return GetTextTypesAllowed(textBox);
	}

	private static void CheckPasteTextAllowed(object sender, DataObjectPastingEventArgs e)
	{
		TextBox textBox = sender as TextBox;
		TextsAllowed textBoxAllowedText = GetTextBoxAllowedText(textBox);
		if (textBoxAllowedText != 0 && !CheckTextBoxNewText(textBox, e.DataObject.GetData(typeof(string)).ToString(), GetRegExForTextAllowed(textBoxAllowedText), bEmptyAsTrue: true))
		{
			e.CancelCommand();
		}
	}

	private static void PreviewTextAllowedInput(object sender, TextCompositionEventArgs e)
	{
		TextBox textBox = sender as TextBox;
		TextsAllowed textBoxAllowedText = GetTextBoxAllowedText(textBox);
		if (textBoxAllowedText != 0 && !CheckTextBoxNewText(textBox, e.Text, GetRegExForTextAllowed(textBoxAllowedText), bEmptyAsTrue: true))
		{
			e.Handled = true;
		}
	}
}
