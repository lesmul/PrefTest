namespace Preference.Import.Data.Tables;

internal class PAFDetailTariffs : BaseTable
{
	public PAFDetailTariffs()
		: base("dbo", "PAFDetailTariffs")
	{
	}

	public override string GetDeleteQuery(int nNumber, int nVersion)
	{
		return $"DELETE pdt FROM [{base.Schema}].[{base.Name}] pdt INNER JOIN ContenidoPAF cp ON pdt.IdPos = cp.IdPos WHERE cp.Numero = {nNumber.ToString()} AND cp.Version = {nVersion.ToString()}";
	}

	public override string GetSelectQuery(int nNumber, int nVersion)
	{
		return $"SELECT * FROM [{base.Schema}].[{base.Name}] WHERE [IdPos] IN (SELECT [IdPos] FROM [dbo].[ContenidoPAF] WHERE [Numero] = {nNumber.ToString()} AND [Version] = {nVersion.ToString()})";
	}
}
