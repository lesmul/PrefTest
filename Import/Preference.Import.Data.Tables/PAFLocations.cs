namespace Preference.Import.Data.Tables;

internal class PAFLocations : BaseTable
{
	public PAFLocations()
		: base("dbo", "PAFLocations")
	{
	}

	public override string GetDeleteQuery(int nNumber, int nVersion)
	{
		return $"DELETE FROM [{base.Schema}].[{base.Name}] WHERE [SalesDocNumber] = {nNumber.ToString()}";
	}

	public override string GetSelectQuery(int nNumber, int nVersion)
	{
		return $"SELECT * FROM [{base.Schema}].[{base.Name}] WHERE [SalesDocNumber] = {nNumber.ToString()}";
	}
}
