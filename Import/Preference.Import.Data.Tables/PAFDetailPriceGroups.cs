namespace Preference.Import.Data.Tables;

internal class PAFDetailPriceGroups : BaseTable
{
	public PAFDetailPriceGroups()
		: base("dbo", "PAFDetailPriceGroups")
	{
	}

	public override string GetDeleteQuery(int nNumber, int nVersion)
	{
		return $"DELETE pdpg FROM [{base.Schema}].[{base.Name}] pdpg INNER JOIN ContenidoPAF cp ON pdpg.IdPos = cp.IdPos WHERE cp.Numero = {nNumber.ToString()} AND cp.Version = {nVersion.ToString()}";
	}

	public override string GetSelectQuery(int nNumber, int nVersion)
	{
		return $"SELECT * FROM [{base.Schema}].[{base.Name}] WHERE [IdPos] IN (SELECT [IdPos] FROM [dbo].[ContenidoPAF] WHERE [Numero] = {nNumber.ToString()} AND [Version] = {nVersion.ToString()})";
	}
}
