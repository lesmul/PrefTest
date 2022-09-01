namespace Preference.Wpf.Controls.Expenses;

internal static class Globals
{
	private static string _strConnectionString = string.Empty;

	public static string ConnectionString
	{
		get
		{
			return _strConnectionString;
		}
		set
		{
			_strConnectionString = value;
		}
	}
}
