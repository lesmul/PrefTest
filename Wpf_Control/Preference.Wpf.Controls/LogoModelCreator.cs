using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Preference.Wpf.Controls;

public class LogoModelCreator : UserControl, IComponentConnector
{
	private double _dModelHeight;

	internal TextBlock FloorTextBlock;

	internal LogoPolylineCreator LogoPolylineCreator;

	internal TextBlock HeightTextBlock;

	internal NumericTextBox HeightTextBox;

	private bool _contentLoaded;

	public double ModelHeight
	{
		get
		{
			return _dModelHeight;
		}
		set
		{
			_dModelHeight = value;
			HeightTextBox.Text = _dModelHeight.ToString();
		}
	}

	public string XmlCode
	{
		get
		{
			return LogoPolylineCreator.XmlCode;
		}
		set
		{
			LogoPolylineCreator.XmlCode = value;
		}
	}

	public LogoModelCreator()
	{
		InitializeComponent();
	}

	private void HeightTextBox_TextChanged(object sender, TextChangedEventArgs e)
	{
		_dModelHeight = Convert.ToDouble(HeightTextBox.Text, NumberFormatInfo.InvariantInfo);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/Preference.Wpf.Controls;component/logomodelcreator.xaml", UriKind.Relative);
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
			FloorTextBlock = (TextBlock)target;
			break;
		case 2:
			LogoPolylineCreator = (LogoPolylineCreator)target;
			break;
		case 3:
			HeightTextBlock = (TextBlock)target;
			break;
		case 4:
			HeightTextBox = (NumericTextBox)target;
			break;
		default:
			_contentLoaded = true;
			break;
		}
	}
}
