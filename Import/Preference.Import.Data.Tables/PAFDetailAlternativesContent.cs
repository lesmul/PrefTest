namespace Preference.Import.Data.Tables;

internal class PAFDetailAlternativesContent : BaseTable
{
	public PAFDetailAlternativesContent()
		: base("dbo", "PAFDetailAlternativesContent")
	{
	}

	public override string GetDeleteQuery(int nNumber, int nVersion)
	{
		return $"DELETE pda FROM [{base.Schema}].[{base.Name}] pda INNER JOIN ContenidoPAF cp ON pda.IdPos = cp.IdPos WHERE cp.Numero = {nNumber.ToString()} AND cp.Version = {nVersion.ToString()}";
	}

	public override string GetSelectQuery(int nNumber, int nVersion)
	{
		return $"SELECT * FROM [{base.Schema}].[{base.Name}] WHERE [IdPos] IN (SELECT [IdPos] FROM dbo.ContenidoPAF WHERE Numero = {nNumber.ToString()} AND Version = {nVersion.ToString()})";
	}
}
