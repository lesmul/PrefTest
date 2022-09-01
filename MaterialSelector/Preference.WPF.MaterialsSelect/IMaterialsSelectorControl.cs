using System;
using Telerik.Windows.Controls;

namespace Preference.WPF.MaterialsSelector;

public interface IMaterialsSelectorControl
{
	void RebindMaterials();

	void ResetItems();

	void ResetWindowLayout();

	void ShowCursorWait(bool show);

	void ScrollIntoViewItem(object item);

	void ViewGeneralPanel();

	void ViewItemsPanel();

	void ViewModelPanel();

	void ViewItemPanel();

	void ViewMaterialsPanel();

	void ViewMaterialPanel();

	void InformAboutWrongFilter(EventHandler<WindowClosedEventArgs> closed);
}
