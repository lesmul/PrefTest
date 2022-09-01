namespace Preference.Import.Data.Tables;

internal class Locks : BaseTable
{
	public Locks()
		: base("dbo", "Locks")
	{
	}

	public override string GetDeleteQuery(int nNumber, int nVersion)
	{
		return $"DELETE l FROM [{base.Schema}].[{base.Name}] l INNER JOIN PAF p ON p.RowId = l.ElementId WHERE p.Numero = {nNumber.ToString()} AND p.Version = {nVersion.ToString()}";
	}

	public override string GetSelectQuery(int nNumber, int nVersion)
	{
		return $"SELECT * FROM [{base.Schema}].[{base.Name}] WHERE [ElementId] IN (SELECT [RowId] FROM [dbo].[PAF] WHERE [Numero] = {nNumber.ToString()} AND [Version] = {nVersion.ToString()})";
	}
}
