namespace Preference.Import.Data.Tables;

internal class TariffChainAggregations : BaseTable
{
	public TariffChainAggregations()
		: base("dbo", "TariffChainAggregations")
	{
	}

	public override string GetDeleteQuery(int nNumber, int nVersion)
	{
		return $"DELETE tca FROM [{base.Schema}].[{base.Name}] tca INNER JOIN PAF p ON tca.ObjectId = p.RowId WHERE p.Numero = {nNumber.ToString()} AND p.Version = {nVersion.ToString()}";
	}

	public override string GetSelectQuery(int nNumber, int nVersion)
	{
		return $"SELECT * FROM [{base.Schema}].[{base.Name}] WHERE [ObjectId] IN (SELECT [RowId] FROM [dbo].[PAF] WHERE [Numero] = {nNumber.ToString()} AND [Version] = {nVersion.ToString()})";
	}
}
