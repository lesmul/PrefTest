using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Preference.Wpf.Controls;

public class CompositeModelCreator : UserControl, IComponentConnector
{
	private int _nSelectedTab;

	private string _xmlCode;

	private double _dModelHeight;

	private double _dWidth;

	private double _dDepth;

	private double _dAngle;

	internal TabControl TabCreator;

	internal LogoModelCreator LogoModelCreator;

	internal BayModelCreator BayModelCreator;

	internal BowModelCreator BowModelCreator;

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
			if (TabCreator.SelectedIndex == 0)
			{
				LogoModelCreator.ModelHeight = _dModelHeight;
			}
			if (TabCreator.SelectedIndex == 1)
			{
				BayModelCreator.ModelHeight = _dModelHeight;
			}
			if (TabCreator.SelectedIndex == 2)
			{
				BowModelCreator.ModelHeight = _dModelHeight;
			}
		}
	}

	public string XmlCode
	{
		get
		{
			if (TabCreator.SelectedIndex == 0)
			{
				return LogoModelCreator.XmlCode;
			}
			if (TabCreator.SelectedIndex == 1)
			{
				return BayModelCreator.XmlCode;
			}
			if (TabCreator.SelectedIndex == 2)
			{
				return BowModelCreator.XmlCode;
			}
			return "";
		}
		set
		{
			_xmlCode = value;
			if (TabCreator.SelectedIndex == 0)
			{
				LogoModelCreator.XmlCode = value;
			}
		}
	}

	public CompositeModelCreator()
	{
		InitializeComponent();
		_nSelectedTab = 0;
	}

	private void TabCreator_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		if (_nSelectedTab != TabCreator.SelectedIndex)
		{
			TransferData();
			_nSelectedTab = TabCreator.SelectedIndex;
			UpdateData();
		}
	}

	private void TransferData()
	{
		if (_nSelectedTab == 0)
		{
			_dModelHeight = LogoModelCreator.ModelHeight;
			_xmlCode = LogoModelCreator.XmlCode;
		}
		if (_nSelectedTab == 1)
		{
			_dModelHeight = BayModelCreator.ModelHeight;
			_xmlCode = BayModelCreator.XmlCode;
			_dWidth = BayModelCreator.ModelWidth;
			_dDepth = BayModelCreator.ModelDepth;
			_dAngle = BayModelCreator.ModelAngle;
		}
		if (_nSelectedTab == 2)
		{
			_dModelHeight = BowModelCreator.ModelHeight;
			_xmlCode = BowModelCreator.XmlCode;
			_dWidth = BowModelCreator.ModelWidth;
			_dDepth = BowModelCreator.ModelDepth;
			_dAngle = BowModelCreator.ModelAngle;
		}
	}

	private void UpdateData()
	{
		if (TabCreator.SelectedIndex == 0)
		{
			LogoModelCreator.ModelHeight = _dModelHeight;
			LogoModelCreator.XmlCode = _xmlCode;
		}
		if (TabCreator.SelectedIndex == 1)
		{
			BayModelCreator.ModelHeight = _dModelHeight;
			BayModelCreator.ModelWidth = _dWidth;
			BayModelCreator.ModelDepth = _dDepth;
			BayModelCreator.ModelAngle = _dAngle;
		}
		if (TabCreator.SelectedIndex == 2)
		{
			BowModelCreator.ModelHeight = _dModelHeight;
			BowModelCreator.ModelWidth = _dWidth;
			BowModelCreator.ModelDepth = _dDepth;
			BowModelCreator.ModelAngle = _dAngle;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/Preference.Wpf.Controls;component/compositemodelcreator.xaml", UriKind.Relative);
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
			TabCreator = (TabControl)target;
			TabCreator.SelectionChanged += TabCreator_SelectionChanged;
			break;
		case 2:
			LogoModelCreator = (LogoModelCreator)target;
			break;
		case 3:
			BayModelCreator = (BayModelCreator)target;
			break;
		case 4:
			BowModelCreator = (BowModelCreator)target;
			break;
		default:
			_contentLoaded = true;
			break;
		}
	}
}
