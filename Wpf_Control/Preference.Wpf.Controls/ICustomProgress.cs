using System;
using System.ComponentModel;

namespace Preference.Wpf.Controls;

public interface ICustomProgress
{
	string CurrentText { get; }

	double Maximum { get; }

	string OutputMessage { get; set; }

	CustomProgressResult Result { get; set; }

	event EventHandler Canceled;

	event ProgressChangedEventHandler ProgressChanged;

	void Run();

	void Cancel();
}
