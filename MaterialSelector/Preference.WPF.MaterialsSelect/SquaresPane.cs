using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;

namespace Preference.WPF.MaterialsSelector.UsersControls;

public class SquaresPane : UserControl, IComponentConnector
{
	internal RadTreeListView treelistview;

	private bool _contentLoaded;

	public SquaresPane()
	{
		InitializeComponent();
		((GridViewDataControl)treelistview).add_DataLoaded((EventHandler<EventArgs>)RadTreeListView_DataLoaded);
	}

	private void RadTreeListView_DataLoaded(object sender, EventArgs e)
	{
		((GridViewDataControl)treelistview).remove_DataLoaded((EventHandler<EventArgs>)RadTreeListView_DataLoaded);
		((GridViewDataControl)treelistview).ExpandAllHierarchyItems();
		((GridViewDataControl)treelistview).add_DataLoaded((EventHandler<EventArgs>)RadTreeListView_DataLoaded);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/Preference.WPF.MaterialsSelector;component/userscontrols/squarespane.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	void IComponentConnector.Connect(int connectionId, object target)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Expected O, but got Unknown
		if (connectionId == 1)
		{
			treelistview = (RadTreeListView)target;
		}
		else
		{
			_contentLoaded = true;
		}
	}
}
