using System.Collections.ObjectModel;

namespace Preference.Wpf.Controls;

public class LogoOrderItems : ObservableCollection<string>
{
	public LogoOrderItems()
	{
		Add("BK");
		Add("FD");
		Add("LT");
		Add("RT");
	}
}
