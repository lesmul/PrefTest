using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Preference.Wpf.Controls.Sales;

public class SalesDocHeaderInfo : UserControl, IComponentConnector
{
	internal SalesDocHeaderInfo UserControl;

	internal Grid LayoutRoot;

	internal ContentControl iconDocumentType;

	internal TextBlock textDocumentType;

	internal TextBlock textNumberVersion;

	internal TextBlock textOrderNumber;

	internal TextBlock textProductionPhase;

	internal TextBlock textOrderAlias;

	internal TextBlock textDataVersion;

	internal ContentControl iconPlausibility;

	internal ContentControl iconProject;

	internal TextBlock textNumberProject;

	internal ContentControl iconPDE;

	internal TextBlock textPrice;

	private bool _contentLoaded;

	public SalesDocHeaderInfo()
	{
		InitializeComponent();
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/Preference.Wpf.Controls;component/sales/salesdocheaderinfo.xaml", UriKind.Relative);
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
			UserControl = (SalesDocHeaderInfo)target;
			break;
		case 2:
			LayoutRoot = (Grid)target;
			break;
		case 3:
			iconDocumentType = (ContentControl)target;
			break;
		case 4:
			textDocumentType = (TextBlock)target;
			break;
		case 5:
			textNumberVersion = (TextBlock)target;
			break;
		case 6:
			textOrderNumber = (TextBlock)target;
			break;
		case 7:
			textProductionPhase = (TextBlock)target;
			break;
		case 8:
			textOrderAlias = (TextBlock)target;
			break;
		case 9:
			textDataVersion = (TextBlock)target;
			break;
		case 10:
			iconPlausibility = (ContentControl)target;
			break;
		case 11:
			iconProject = (ContentControl)target;
			break;
		case 12:
			textNumberProject = (TextBlock)target;
			break;
		case 13:
			iconPDE = (ContentControl)target;
			break;
		case 14:
			textPrice = (TextBlock)target;
			break;
		default:
			_contentLoaded = true;
			break;
		}
	}
}
