using System;

namespace Preference.Wpf.Controls.Attachments;

public interface IServiceAgent
{
	string ConnectionString { get; set; }

	int AddAttachmentBlob(string name, string filePath, string fileExtension, Guid parentID, Guid docID, int objectType, byte[] fileBlob, ref Guid newRowID);

	int AddAttachmentPath(string name, string filePath, string fileExtension, Guid parentID, Guid docID, int objectType, ref Guid newRowID);

	int AddAttachmentFolder(string folderName, Guid parentID, Guid docID, ref Guid newRowID);

	bool ExistsAttachment(string name, Guid docID, Guid parentID);

	int GetAttachment(Guid rowID, ref byte[] fileBlob, ref string name, ref string filePath, ref string fileExtension);

	Guid GetAttachmentRowId(string name, Guid docID, Guid parentID);

	int GetAttachments(Guid docID, ref string xml);

	string GetDocumentName(Guid documentID, int objectType);

	void InsertAttachmentBlob(byte[] fileblob, Guid rowID);

	bool IsFolderEmpty(Guid rowID);

	int RemoveAttachment(Guid attachmentID);

	int RenameAttachment(Guid attachmentID, string newName);

	bool GetNumberAndVersion(Guid documentID, int objectType, out int number, out int version);

	WarrantyTypes GetWarrantyType(int number, int version);

	bool SalesIsPublic(Guid documentID, int objectType);

	bool SalesIsPublicDocumentOwned(Guid documentID, int objectType);

	bool SalesIsPublicDocumentOwned(int number, int version, int objectType);

	int SetAttachmentPublic(ref Guid attachmentID, bool? isPublic);

	object GetGlobalVariableValue(int company, string name);
}
