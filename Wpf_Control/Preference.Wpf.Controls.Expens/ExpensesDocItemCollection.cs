namespace Preference.Wpf.Controls.Expenses.Models;

public class ExpensesDocItemCollection : PrefCollection<ExpensesDocItem>
{
	protected override void InsertItem(int index, ExpensesDocItem item)
	{
		item.ParentCollection = this;
		base.InsertItem(index, item);
	}
}
