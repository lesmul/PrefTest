using System.Windows;
using Microsoft.VisualBasic;
using Preference.Wpf.Controls.Properties;

namespace Preference.Wpf.Controls.Core;

internal class Dialogs
{
	public static MessageBoxResult Confirm(string messageBoxText, string caption)
	{
		return MessageBox.Show(messageBoxText, caption, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
	}

	public static void Inform(string messageBoxText)
	{
		MessageBox.Show(messageBoxText, Resources.StringInformation, MessageBoxButton.OK, MessageBoxImage.Asterisk);
	}

	public static void Exclaim(string messageBoxText)
	{
		MessageBox.Show(messageBoxText, Resources.StringWarning, MessageBoxButton.OK, MessageBoxImage.Exclamation);
	}

	public static void Error(string messageBoxText)
	{
		MessageBox.Show(messageBoxText, Resources.StringError, MessageBoxButton.OK, MessageBoxImage.Hand);
	}

	public static string InputBox(string Prompt, string Title, string DefaultResponse, int XPos, int YPos)
	{
		return Interaction.InputBox(Prompt, Title, DefaultResponse, XPos, YPos);
	}
}
