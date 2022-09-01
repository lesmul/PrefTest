using System;
using System.Data;
using System.Data.SqlClient;
using System.Security;
using Preference.Data.SqlClient;
using Preference.Diagnostics;
using Preference.Wpf.Controls.Properties;

namespace Preference.Wpf.Controls.Attachments;

public class ServiceAgent : IServiceAgent
{
	private string _strConnectionString = string.Empty;

	public string ConnectionString
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

	public object GetGlobalVariableValue(int company, string name)
	{
		string text = "SELECT [Valor]";
		text += " FROM [VariablesGlobales]";
		text += $" WHERE [Empresa] = {company}";
		text += $" AND [Nombre] = N'{name}'";
		DataSet dataSet = SqlHelper.ExecuteDataset(_strConnectionString, CommandType.Text, text);
		if (dataSet.Tables[0].Rows.Count == 1)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			if (!dataRow.IsNull("Valor"))
			{
				return dataRow["Valor"];
			}
		}
		return null;
	}

	public WarrantyTypes GetWarrantyType(int number, int version)
	{
		string text = "SELECT [WarrantyType]";
		text += " FROM [PAF]";
		text += $" WHERE [Numero] = {number}";
		text += $" AND [Version] = {version}";
		DataSet dataSet = SqlHelper.ExecuteDataset(_strConnectionString, CommandType.Text, text);
		if (dataSet.Tables[0].Rows.Count == 1)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			if (dataRow.IsNull("WarrantyType"))
			{
				return WarrantyTypes.None;
			}
			switch (Convert.ToInt32(dataRow["WarrantyType"]))
			{
			case 1:
				return WarrantyTypes.Service;
			case 2:
				return WarrantyTypes.Warranty;
			}
		}
		return WarrantyTypes.None;
	}

	public bool GetNumberAndVersion(Guid documentID, int objectType, out int number, out int version)
	{
		number = 0;
		version = 0;
		if (objectType == 1)
		{
			string text = $"SELECT Numero, Version FROM PAF WHERE RowId = '{documentID}'";
			DataSet dataSet = SqlHelper.ExecuteDataset(_strConnectionString, CommandType.Text, text);
			if (dataSet.Tables[0].Rows.Count == 1)
			{
				DataRow dataRow = dataSet.Tables[0].Rows[0];
				number = Convert.ToInt32(dataRow["Numero"]);
				version = Convert.ToInt32(dataRow["Version"]);
				return true;
			}
		}
		return false;
	}

	public bool SalesIsPublic(Guid documentID, int objectType)
	{
		if (objectType == 1)
		{
			int number = 0;
			int version = 0;
			if (GetNumberAndVersion(documentID, objectType, out number, out version))
			{
				string text = $"SELECT 1 FROM [PAF] WHERE [Numero] = {number} AND [Version] = {version} AND [Public] = 1";
				DataSet dataSet = SqlHelper.ExecuteDataset(_strConnectionString, CommandType.Text, text);
				return dataSet.Tables[0].Rows.Count > 0;
			}
		}
		return false;
	}

	public bool SalesIsPublicDocumentOwned(Guid documentID, int objectType)
	{
		int number = 0;
		int version = 0;
		if (GetNumberAndVersion(documentID, objectType, out number, out version))
		{
			return SalesIsPublicDocumentOwned(number, version, objectType);
		}
		return false;
	}

	public bool SalesIsPublicDocumentOwned(int number, int version, int objectType)
	{
		try
		{
			if (objectType == 1)
			{
				string text = $"select dbo.Sales_IsPublicDocumentOwned ({number}, {version}) AS SalesIsPublicDocumentOwned";
				DataSet dataSet = SqlHelper.ExecuteDataset(_strConnectionString, CommandType.Text, text);
				if (dataSet.Tables[0].Rows.Count == 1)
				{
					DataRow dataRow = dataSet.Tables[0].Rows[0];
					if (!dataRow.IsNull("SalesIsPublicDocumentOwned"))
					{
						return Convert.ToInt32(dataRow["SalesIsPublicDocumentOwned"]) == 1;
					}
				}
			}
			return false;
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "Preference.Attachments");
			throw;
		}
	}

	public string GetDocumentName(Guid documentID, int objectType)
	{
		try
		{
			if (objectType == 1)
			{
				string text = $"SELECT Numero, Version FROM PAF WHERE RowId = '{documentID}'";
				DataSet dataSet = SqlHelper.ExecuteDataset(_strConnectionString, CommandType.Text, text);
				if (dataSet.Tables[0].Rows.Count == 1)
				{
					DataRow dataRow = dataSet.Tables[0].Rows[0];
					int num = Convert.ToInt32(dataRow["Numero"]);
					int num2 = Convert.ToInt32(dataRow["Version"]);
					return string.Format(Resources.StringAttachDocPAF, num, num2);
				}
			}
			return Resources.StringAttachRoot;
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "Preference.Attachments");
			throw;
		}
	}

	public int AddAttachmentBlob(string name, string filePath, string fileExtension, Guid parentID, Guid docID, int objectType, byte[] fileBlob, ref Guid newRowID)
	{
		try
		{
			int num = 0;
			string text = "SELECT ISNULL(MAX(Position), 0) + 1 AS Position";
			text += $" FROM ExternalFiles WHERE DocId = '{docID}'";
			DataSet dataSet = SqlHelper.ExecuteDataset(_strConnectionString, CommandType.Text, text);
			if (dataSet.Tables[0].Rows.Count == 0)
			{
				return 1;
			}
			num = Convert.ToInt32(dataSet.Tables[0].Rows[0]["Position"]);
			newRowID = Guid.NewGuid();
			text = "INSERT INTO ExternalFiles";
			text += "(";
			text += "   RowId";
			text += " , DocId";
			text += " , ObjectTypeCode";
			text += " , Position";
			text += " , Name";
			text += " , FileBlob";
			text += " , FilePath";
			text += " , FileExtension";
			text += " , Saved";
			text += " , ParentRowId";
			text += " , IsPublic";
			text += ")";
			text += " VALUES";
			text += "(";
			text += $"   '{newRowID}'";
			text += $" , '{docID}'";
			text += $" , {objectType}";
			text += $" , {num}";
			text += $" , N'{name}'";
			text += " , @FileBlob";
			text += $" , N'{filePath}'";
			text += $" , N'{fileExtension}'";
			text += " , 1";
			text += string.Format(" , {0}", (parentID == Guid.Empty) ? "NULL" : $"'{parentID}'");
			text += " , NULL";
			text += ")";
			SqlParameter[] array = new SqlParameter[1]
			{
				new SqlParameter("@FileBlob", fileBlob)
			};
			return SqlHelper.ExecuteNonQuery(_strConnectionString, CommandType.Text, text, array);
		}
		catch (SecurityException ex)
		{
			Logger.Instance.WriteError((Exception)ex, "Preference.Attachments");
			throw;
		}
		catch (Exception ex2)
		{
			Logger.Instance.WriteError(ex2, "Preference.Attachments");
			throw;
		}
	}

	public int AddAttachmentPath(string name, string filePath, string fileExtension, Guid parentID, Guid docID, int objectType, ref Guid newRowID)
	{
		try
		{
			int num = 0;
			string text = "SELECT ISNULL(MAX(Position), 0) + 1 AS Position";
			text += $" FROM ExternalFiles WHERE DocId = '{docID}'";
			DataSet dataSet = SqlHelper.ExecuteDataset(_strConnectionString, CommandType.Text, text);
			if (dataSet.Tables[0].Rows.Count == 0)
			{
				return 1;
			}
			num = Convert.ToInt32(dataSet.Tables[0].Rows[0]["Position"]);
			newRowID = Guid.NewGuid();
			text = "INSERT INTO ExternalFiles";
			text += "(";
			text += "   RowId";
			text += " , DocId";
			text += " , ObjectTypeCode";
			text += " , Position";
			text += " , Name";
			text += " , FileBlob";
			text += " , FilePath";
			text += " , FileExtension";
			text += " , Saved";
			text += " , ParentRowId";
			text += " , IsPublic";
			text += ")";
			text += " VALUES";
			text += "(";
			text += $"   '{newRowID}'";
			text += $" , '{docID}'";
			text += $" , {objectType}";
			text += $" , {num}";
			text += $" , N'{name}'";
			text += " , NULL";
			text += $" , N'{filePath}'";
			text += $" , N'{fileExtension}'";
			text += " , 0";
			text += string.Format(" , {0}", (parentID == Guid.Empty) ? "NULL" : $"'{parentID}'");
			text += " , NULL";
			text += ")";
			return SqlHelper.ExecuteNonQuery(_strConnectionString, CommandType.Text, text);
		}
		catch (SecurityException ex)
		{
			Logger.Instance.WriteError((Exception)ex, "Preference.Attachments");
			throw;
		}
		catch (Exception ex2)
		{
			Logger.Instance.WriteError(ex2, "Preference.Attachments");
			throw;
		}
	}

	public int AddAttachmentFolder(string folderName, Guid parentID, Guid docID, ref Guid newRowID)
	{
		try
		{
			string text = "[dbo].[pa_Attachments_AddFolder]";
			SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(_strConnectionString, text, true);
			spParameterSet[1].Value = folderName;
			spParameterSet[2].Value = parentID;
			spParameterSet[3].Value = docID;
			SqlHelper.ExecuteNonQuery(_strConnectionString, CommandType.StoredProcedure, text, spParameterSet);
			newRowID = (Guid)spParameterSet[4].Value;
			return Convert.ToInt32(spParameterSet[0].Value);
		}
		catch (SecurityException ex)
		{
			Logger.Instance.WriteError((Exception)ex, "Preference.Attachments");
			throw;
		}
		catch (Exception ex2)
		{
			Logger.Instance.WriteError(ex2, "Preference.Attachments");
			throw;
		}
	}

	public bool ExistsAttachment(string name, Guid docID, Guid parentID)
	{
		try
		{
			string text = "select [dbo].[Attachments_ExistsAttachment](@strName, @docId, @parentId)";
			SqlParameter[] array = new SqlParameter[3]
			{
				new SqlParameter("@strName", name),
				new SqlParameter("@docId", docID),
				new SqlParameter("@parentId", parentID)
			};
			short num = (short)SqlHelper.ExecuteScalar(_strConnectionString, CommandType.Text, text, array);
			return num == 1;
		}
		catch (SecurityException ex)
		{
			Logger.Instance.WriteError((Exception)ex, "Preference.Attachments");
			throw;
		}
		catch (Exception ex2)
		{
			Logger.Instance.WriteError(ex2, "Preference.Attachments");
			throw;
		}
	}

	public int GetAttachment(Guid rowID, ref byte[] fileBlob, ref string name, ref string filePath, ref string fileExtension)
	{
		try
		{
			string text = "[dbo].[pa_Attachments_GetAttachment]";
			SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(_strConnectionString, text, true);
			spParameterSet[1].Value = rowID;
			SqlHelper.ExecuteNonQuery(_strConnectionString, CommandType.StoredProcedure, text, spParameterSet);
			fileBlob = (byte[])spParameterSet[2].Value;
			name = (string)spParameterSet[3].Value;
			filePath = (string)spParameterSet[4].Value;
			if (spParameterSet[5].Value != DBNull.Value)
			{
				fileExtension = (string)spParameterSet[5].Value;
			}
			return Convert.ToInt32(spParameterSet[0].Value);
		}
		catch (SecurityException ex)
		{
			Logger.Instance.WriteError((Exception)ex, "Preference.Attachments");
			throw;
		}
		catch (Exception ex2)
		{
			Logger.Instance.WriteError(ex2, "Preference.Attachments");
			throw;
		}
	}

	public Guid GetAttachmentRowId(string name, Guid docID, Guid parentID)
	{
		try
		{
			string text = "select [dbo].[Attachments_GetAttachmentRowId](@strName, @docId, @parentId)";
			SqlParameter[] array = new SqlParameter[3]
			{
				new SqlParameter("@strName", name),
				new SqlParameter("@docId", docID),
				new SqlParameter("@parentId", parentID)
			};
			return (Guid)SqlHelper.ExecuteScalar(_strConnectionString, CommandType.Text, text, array);
		}
		catch (SecurityException ex)
		{
			Logger.Instance.WriteError((Exception)ex, "Preference.Attachments");
			throw;
		}
		catch (Exception ex2)
		{
			Logger.Instance.WriteError(ex2, "Preference.Attachments");
			throw;
		}
	}

	public int GetAttachments(Guid docID, ref string xml)
	{
		try
		{
			string text = "[dbo].[pa_Attachments_GetAttachmentsXml]";
			SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(_strConnectionString, text, true);
			spParameterSet[1].Value = docID;
			spParameterSet[2].Value = "<Attachments/>";
			SqlHelper.ExecuteNonQuery(_strConnectionString, CommandType.StoredProcedure, text, spParameterSet);
			xml = (string)spParameterSet[2].Value;
			return Convert.ToInt32(spParameterSet[0].Value);
		}
		catch (SecurityException ex)
		{
			Logger.Instance.WriteError((Exception)ex, "Preference.Attachments");
			throw;
		}
		catch (Exception ex2)
		{
			Logger.Instance.WriteError(ex2, "Preference.Attachments");
			throw;
		}
	}

	public void InsertAttachmentBlob(byte[] fileblob, Guid rowID)
	{
		try
		{
			string text = "[dbo].[pa_Attachments_InsertBlob]";
			SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(_strConnectionString, text, false);
			spParameterSet[0].Value = fileblob;
			spParameterSet[1].Value = rowID;
			SqlHelper.ExecuteNonQuery(_strConnectionString, CommandType.StoredProcedure, text, spParameterSet);
		}
		catch (SecurityException ex)
		{
			Logger.Instance.WriteError((Exception)ex, "Preference.Attachments");
			throw;
		}
		catch (Exception ex2)
		{
			Logger.Instance.WriteError(ex2, "Preference.Attachments");
			throw;
		}
	}

	public bool IsFolderEmpty(Guid rowID)
	{
		try
		{
			string text = "select [dbo].[Attachments_IsFolderEmpty](@rowId)";
			SqlParameter[] array = new SqlParameter[1]
			{
				new SqlParameter("@rowId", rowID)
			};
			short num = (short)SqlHelper.ExecuteScalar(_strConnectionString, CommandType.Text, text, array);
			return num == 1;
		}
		catch (SecurityException ex)
		{
			Logger.Instance.WriteError((Exception)ex, "Preference.Attachments");
			throw;
		}
		catch (Exception ex2)
		{
			Logger.Instance.WriteError(ex2, "Preference.Attachments");
			throw;
		}
	}

	public int RemoveAttachment(Guid attachmentID)
	{
		try
		{
			string text = "[dbo].[pa_Attachments_Remove]";
			SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(_strConnectionString, text, true);
			spParameterSet[1].Value = attachmentID;
			SqlHelper.ExecuteNonQuery(_strConnectionString, CommandType.StoredProcedure, text, spParameterSet);
			return Convert.ToInt32(spParameterSet[0].Value);
		}
		catch (SecurityException ex)
		{
			Logger.Instance.WriteError((Exception)ex, "Preference.Attachments");
			throw;
		}
		catch (Exception ex2)
		{
			Logger.Instance.WriteError(ex2, "Preference.Attachments");
			throw;
		}
	}

	public int RenameAttachment(Guid attachmentID, string newName)
	{
		try
		{
			string text = "[dbo].[pa_Attachments_Rename]";
			SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(_strConnectionString, text, true);
			spParameterSet[1].Value = attachmentID;
			spParameterSet[2].Value = newName;
			SqlHelper.ExecuteNonQuery(_strConnectionString, CommandType.StoredProcedure, text, spParameterSet);
			return Convert.ToInt32(spParameterSet[0].Value);
		}
		catch (SecurityException ex)
		{
			Logger.Instance.WriteError((Exception)ex, "Preference.Attachments");
			throw;
		}
		catch (Exception ex2)
		{
			Logger.Instance.WriteError(ex2, "Preference.Attachments");
			throw;
		}
	}

	public int SetAttachmentPublic(ref Guid attachmentID, bool? isPublic)
	{
		try
		{
			string text = "[dbo].[pa_Attachments_SetIsPublic]";
			SqlParameter[] spParameterSet = SqlHelperParameterCache.GetSpParameterSet(_strConnectionString, text, true);
			spParameterSet[1].Value = attachmentID;
			if (isPublic.HasValue)
			{
				spParameterSet[2].Value = isPublic.Value;
			}
			SqlHelper.ExecuteNonQuery(_strConnectionString, CommandType.StoredProcedure, text, spParameterSet);
			attachmentID = new Guid(Convert.ToString(spParameterSet[1].Value));
			return Convert.ToInt32(spParameterSet[0].Value);
		}
		catch (SecurityException ex)
		{
			Logger.Instance.WriteError((Exception)ex, "Preference.Attachments");
			throw;
		}
		catch (Exception ex2)
		{
			Logger.Instance.WriteError(ex2, "Preference.Attachments");
			throw;
		}
	}
}
