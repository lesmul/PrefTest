using System.Windows;
using System.Windows.Controls;

namespace Preference.Wpf.Controls.ControlsForDoubles;

public abstract class GenericTextBox<T> : TextBox
{
	private static T _defaultValue;

	public static readonly DependencyProperty ValueDataProperty = DependencyProperty.Register("ValueData", typeof(T), typeof(GenericTextBox<T>), new PropertyMetadata(_defaultValue, OnValueDataChanged));

	public static readonly DependencyProperty TextValueProperty = DependencyProperty.Register("TextValue", typeof(string), typeof(GenericTextBox<T>), new PropertyMetadata("0", OnTextValueChanged));

	private bool _bChangingText;

	public T ValueData
	{
		get
		{
			return (T)GetValue(ValueDataProperty);
		}
		set
		{
			SetValue(ValueDataProperty, value);
		}
	}

	public string TextValue
	{
		get
		{
			return (string)GetValue(TextValueProperty);
		}
		private set
		{
			SetValue(TextValueProperty, value);
		}
	}

	protected bool RefreshingTextFromData { get; set; }

	protected abstract T GetValueFromText(string strText);

	protected abstract string GetTextFromValue(T valueData);

	protected static void SetDefaultValue(T defaultValue)
	{
		_defaultValue = defaultValue;
	}

	private void UpdateTextInInterface()
	{
		string text = ValueData.ToString();
		text = (TextValue = GetTextFromValue(ValueData));
	}

	protected override void OnLostFocus(RoutedEventArgs e)
	{
		UpdateTextInInterface();
		base.OnLostFocus(e);
	}

	private static void OnValueDataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is GenericTextBox<T> genericTextBox && !genericTextBox._bChangingText)
		{
			genericTextBox.UpdateTextInInterface();
		}
	}

	private static void OnTextValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is GenericTextBox<T> genericTextBox && !genericTextBox.RefreshingTextFromData)
		{
			genericTextBox._bChangingText = true;
			T valueFromText = genericTextBox.GetValueFromText(genericTextBox.TextValue);
			genericTextBox.SetValue(ValueDataProperty, valueFromText);
			genericTextBox._bChangingText = false;
		}
	}
}
