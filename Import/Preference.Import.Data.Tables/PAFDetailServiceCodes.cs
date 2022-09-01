namespace Preference.Import.Data.Tables;

internal class PAFDetailServiceCodes : BaseTable
{
	public PAFDetailServiceCodes()
		: base("dbo", "PAFDetailServiceCodes")
	{
	}

	public override string GetDeleteQuery(int nNumber, int nVersion)
	{
		return $"DELETE pdsc FROM [{base.Schema}].[{base.Name}] pdsc INNER JOIN ContenidoPAF cp ON pdsc.IdPos = cp.IdPos WHERE cp.Numero = {nNumber.ToString()} AND cp.Version = {nVersion.ToString()}";
	}

	public override string GetSelectQuery(int nNumber, int nVersion)
	{
		return $"SELECT * FROM [{base.Schema}].[{base.Name}] WHERE [IdPos] IN (SELECT [IdPos] FROM [dbo].[ContenidoPAF] WHERE [Numero] = {nNumber.ToString()} AND [Version] = {nVersion.ToString()})";
	}
}
