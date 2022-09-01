namespace Preference.Import.Data.Tables;

internal class AuditDetail : BaseTable
{
	public AuditDetail()
		: base("dbo", "AuditDetail")
	{
	}

	public override string GetDeleteQuery(int nNumber, int nVersion)
	{
		return $"DELETE ad FROM [{base.Schema}].[{base.Name}] ad INNER JOIN Audit a ON a.RowId = ad.ParentRowId INNER JOIN PAF p ON a.ElementId = p.RowId WHERE p.Numero = {nNumber.ToString()} AND p.Version = {nVersion.ToString()}";
	}

	public override string GetSelectQuery(int nNumber, int nVersion)
	{
		return $"SELECT * FROM [{base.Schema}].[{base.Name}] WHERE [ParentRowId] IN (SELECT a.RowId FROM [dbo].[Audit] a INNER JOIN [dbo].[PAF] p ON a.ElementId = p.RowId WHERE p.Numero = {nNumber.ToString()} AND p.Version = {nVersion.ToString()})";
	}
}
