using System;

namespace Preference.Import.Data;

public class ProgressChangedEventArgs : EventArgs
{
	public string Message { get; private set; }

	public int Percentage { get; private set; }

	public ProgressChangedEventArgs(string strMessage, int nPercentage)
	{
		Message = strMessage;
		Percentage = nPercentage;
	}
}
