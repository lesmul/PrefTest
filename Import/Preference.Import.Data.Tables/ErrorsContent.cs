namespace Preference.Import.Data.Tables;

internal class ErrorsContent : BaseTable
{
	public ErrorsContent()
		: base("dbo", "ErrorsContent")
	{
	}

	public override string GetDeleteQuery(int nNumber, int nVersion)
	{
		return $"DELETE ec FROM [{base.Schema}].[{base.Name}] ec INNER JOIN dbo.ContenidoPAFBlob cpb ON cpb.RowId = ec.ElementId WHERE cpb.Numero = {nNumber.ToString()} AND cpb.Version = {nVersion.ToString()}";
	}

	public override string GetSelectQuery(int nNumber, int nVersion)
	{
		return $"SELECT ec.* FROM [{base.Schema}].[{base.Name}] ec INNER JOIN dbo.ContenidoPAFBlob cpb ON cpb.RowId = ec.ElementId WHERE cpb.Numero = {nNumber.ToString()} AND cpb.Version = {nVersion.ToString()}";
	}
}
