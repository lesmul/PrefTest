namespace Preference.Import.Data.Tables;

internal class ExternalFiles : BaseTable
{
	public ExternalFiles()
		: base("dbo", "ExternalFiles")
	{
	}

	public override string GetDeleteQuery(int nNumber, int nVersion)
	{
		return $"DELETE ef FROM [{base.Schema}].[{base.Name}] ef INNER JOIN PAF p ON ef.DocId = p.RowId WHERE p.Numero = {nNumber.ToString()} AND p.Version = {nVersion.ToString()}";
	}

	public override string GetSelectQuery(int nNumber, int nVersion)
	{
		return $"SELECT * FROM [{base.Schema}].[{base.Name}] WHERE [DocId] IN (SELECT [RowId] FROM [dbo].[PAF] WHERE [Numero] = {nNumber.ToString()} AND [Version] = {nVersion.ToString()})";
	}
}
