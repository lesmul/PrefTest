namespace Preference.Import.Data.Tables;

internal class Audit : BaseTable
{
	public Audit()
		: base("dbo", "Audit")
	{
	}

	public override string GetDeleteQuery(int nNumber, int nVersion)
	{
		return $"DELETE a FROM [{base.Schema}].[{base.Name}] a INNER JOIN PAF p ON a.ElementId = p.RowId WHERE p.Numero = {nNumber.ToString()} AND p.Version = {nVersion.ToString()}";
	}

	public override string GetSelectQuery(int nNumber, int nVersion)
	{
		return $"SELECT * FROM [{base.Schema}].[{base.Name}] WHERE [ElementId] IN (SELECT [RowId] FROM [dbo].[PAF] WHERE [Numero] = {nNumber.ToString()} AND [Version] = {nVersion.ToString()})";
	}
}
