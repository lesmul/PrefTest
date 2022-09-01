namespace Preference.Import.Data.Tables;

internal class PAFDataVersions : BaseTable
{
	public PAFDataVersions()
		: base("dbo", "PAFDataVersions")
	{
	}

	public override string GetDeleteQuery(int nNumber, int nVersion)
	{
		return $"DELETE pdv FROM [{base.Schema}].[{base.Name}] pdv INNER JOIN PAF p ON pdv.SalesDocId = p.RowId WHERE p.Numero = {nNumber.ToString()} AND p.Version = {nVersion.ToString()}";
	}

	public override string GetSelectQuery(int nNumber, int nVersion)
	{
		return $"SELECT * FROM [{base.Schema}].[{base.Name}] WHERE [SalesDocId] IN (SELECT [RowId] FROM [dbo].[PAF] WHERE [Numero] = {nNumber.ToString()} AND [Version] = {nVersion.ToString()})";
	}
}
