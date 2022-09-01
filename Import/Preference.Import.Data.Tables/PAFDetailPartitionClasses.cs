namespace Preference.Import.Data.Tables;

internal class PAFDetailPartitionClasses : BaseTable
{
	public PAFDetailPartitionClasses()
		: base("dbo", "PAFDetailPartitionClasses")
	{
	}

	public override string GetDeleteQuery(int nNumber, int nVersion)
	{
		return $"DELETE pdp FROM [{base.Schema}].[{base.Name}] pdp INNER JOIN ContenidoPAF cp ON pdp.IdPos = cp.IdPos WHERE cp.Numero = {nNumber.ToString()} AND cp.Version = {nVersion.ToString()}";
	}

	public override string GetSelectQuery(int nNumber, int nVersion)
	{
		return $"SELECT * FROM [{base.Schema}].[{base.Name}] WHERE [IdPos] IN (SELECT [IdPos] FROM [dbo].[ContenidoPAF] WHERE [Numero] = {nNumber.ToString()} AND [Version] = {nVersion.ToString()})";
	}
}
