namespace Preference.Wpf.Controls.Expenses.Models;

public interface IServiceAgent
{
	bool Execute(string CommandsXml, ref string ResultsXml, ref string MessagesXml);
}
