using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using Preference.Diagnostics;
using Preference.WPF.MaterialsSelector.Settings;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Data;

namespace Preference.WPF.MaterialsSelector.UsersControls;

public class MaterialsPane : UserControl, IComponentConnector
{
	internal RadGridView radGridView;

	private bool _contentLoaded;

	public MaterialsPane()
	{
		InitializeComponent();
		ApplySerializedColumnWidths();
		ApplySerializedSortDescriptor();
		ApplySerializedGroupDescriptors();
	}

	private void radGridView_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		new MaterialsPaneSettings().SetColumnWidth(e.get_Column().get_UniqueName(), e.get_NewWidth());
	}

	private void radGridView_Grouped(object sender, GridViewGroupedEventArgs e)
	{
		new MaterialsPaneSettings().SetGroupDescriptors(((GridViewDataControl)radGridView).get_GroupDescriptors());
	}

	private void radGridView_Sorted(object sender, GridViewSortedEventArgs e)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		new MaterialsPaneSettings().SetSortDescriptor(e.get_Column().get_UniqueName(), e.get_Column().get_SortingState());
	}

	public void Rebind()
	{
		((DataControl)radGridView).Rebind();
	}

	private void ApplySerializedColumnWidths()
	{
		new MaterialsPaneSettings().ApplySerializedColumnWidths(radGridView);
	}

	private void ApplySerializedSortDescriptor()
	{
		try
		{
			SortDescriptor sortDescriptor = new MaterialsPaneSettings().GetSortDescriptor();
			if (sortDescriptor != null)
			{
				((Collection<ISortDescriptor>)(object)((GridViewDataControl)radGridView).get_SortDescriptors()).Add((ISortDescriptor)(object)sortDescriptor);
			}
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefGest");
		}
	}

	private void ApplySerializedGroupDescriptors()
	{
		try
		{
			IList<GroupDescriptor> groupDescriptors = new MaterialsPaneSettings().GetGroupDescriptors();
			if (groupDescriptors == null)
			{
				return;
			}
			foreach (GroupDescriptor item in groupDescriptors)
			{
				((Collection<IGroupDescriptor>)(object)((GridViewDataControl)radGridView).get_GroupDescriptors()).Add((IGroupDescriptor)(object)item);
			}
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefGest");
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/Preference.WPF.MaterialsSelector;component/userscontrols/materialspane.xaml", UriKind.Relative);
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
			radGridView = (RadGridView)target;
			((GridViewDataControl)radGridView).add_ColumnWidthChanged((EventHandler<ColumnWidthChangedEventArgs>)radGridView_ColumnWidthChanged);
			((GridViewDataControl)radGridView).add_Sorted((EventHandler<GridViewSortedEventArgs>)radGridView_Sorted);
			((GridViewDataControl)radGridView).add_Grouped((EventHandler<GridViewGroupedEventArgs>)radGridView_Grouped);
		}
		else
		{
			_contentLoaded = true;
		}
	}
}
