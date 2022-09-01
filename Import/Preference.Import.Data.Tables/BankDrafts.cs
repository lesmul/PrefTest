namespace Preference.Import.Data.Tables;

internal class BankDrafts : BaseTable
{
	public BankDrafts()
		: base("dbo", "BankDrafts")
	{
	}

	public override string GetDeleteQuery(int nNumber, int nVersion)
	{
		return $"DELETE bd FROM [{base.Schema}].[{base.Name}] bd INNER JOIN PAF p ON p.RowId = bd.ElementId WHERE p.Numero = {nNumber.ToString()} AND p.Version = {nVersion.ToString()}";
	}

	public override string GetSelectQuery(int nNumber, int nVersion)
	{
		return $"SELECT * FROM [{base.Schema}].[{base.Name}] WHERE [ElementId] IN (SELECT [RowId] FROM [dbo].[PAF] WHERE [Numero] = {nNumber.ToString()} AND [Version] = {nVersion.ToString()})";
	}
}
