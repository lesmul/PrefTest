namespace Preference.Wpf.Controls.Attachments.Views;

public interface IAttachmentsView
{
	bool AskForFilePath(out string path);

	bool AskForAttachmentNameAndTypeOfSerialization(ref string attachmentName, out bool saved);
}
