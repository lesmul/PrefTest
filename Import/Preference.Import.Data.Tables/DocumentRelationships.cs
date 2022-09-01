namespace Preference.Import.Data.Tables;

internal class DocumentRelationships : BaseTable
{
	public DocumentRelationships()
		: base("dbo", "DocumentRelationships")
	{
	}

	public override string GetDeleteQuery(int nNumber, int nVersion)
	{
		return $"DELETE dr FROM [{base.Schema}].[{base.Name}] dr INNER JOIN PAF p ON p.RowId = dr.SrcDocumentId WHERE p.Numero = {nNumber.ToString()} AND p.Version = {nVersion.ToString()}";
	}

	public override string GetSelectQuery(int nNumber, int nVersion)
	{
		return $"SELECT * FROM [{base.Schema}].[{base.Name}] WHERE [SrcDocumentId] IN (SELECT [RowId] FROM [dbo].[PAF] WHERE [Numero] = {nNumber.ToString()} AND [Version] = {nVersion.ToString()})";
	}
}
