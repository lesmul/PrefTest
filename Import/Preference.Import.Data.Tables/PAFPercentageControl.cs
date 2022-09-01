namespace Preference.Import.Data.Tables;

internal class PAFPercentageControl : BaseTable
{
	public PAFPercentageControl()
		: base("dbo", "PAFPercentageControl")
	{
	}

	public override string GetDeleteQuery(int nNumber, int nVersion)
	{
		return $"DELETE FROM [{base.Schema}].[{base.Name}] WHERE [FromNumber] = {nNumber.ToString()} AND [FromVersion] = {nVersion.ToString()}";
	}

	public override string GetSelectQuery(int nNumber, int nVersion)
	{
		return $"SELECT * FROM [{base.Schema}].[{base.Name}] WHERE [FromNumber] = {nNumber.ToString()} AND [FromVersion] = {nVersion.ToString()}";
	}
}
