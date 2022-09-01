using System.Diagnostics;
using Preference.Diagnostics;

namespace Preference.Logikal;

internal static class EventLog
{
	public static void AddError(string strMessage)
	{
		AddEvent(EventLogEntryType.Error, strMessage);
	}

	public static void AddEvent(EventLogEntryType type, string strMessage)
	{
		EventLog.AddEvent("Preference", "LogiKal", type, 0, strMessage);
	}
}
