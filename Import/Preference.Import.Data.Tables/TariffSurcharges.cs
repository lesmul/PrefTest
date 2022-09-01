namespace Preference.Import.Data.Tables;

internal class TariffSurcharges : BaseTable
{
	public TariffSurcharges()
		: base("dbo", "TariffSurcharges")
	{
	}

	public override string GetDeleteQuery(int nNumber, int nVersion)
	{
		return $"DELETE ts FROM [{base.Schema}].[{base.Name}] ts INNER JOIN dbo.Tariff t ON t.RowId = ts.TariffRowId WHERE [t].[SalesDocumentNumber] = {nNumber.ToString()} AND [t].[SalesDocumentVersion] = {nVersion.ToString()}";
	}

	public override string GetSelectQuery(int nNumber, int nVersion)
	{
		return $"SELECT t.* FROM [{base.Schema}].[{base.Name}] ts INNER JOIN dbo.Tariff t ON t.RowId = ts.TariffRowId WHERE [t].[SalesDocumentNumber] = {nNumber.ToString()} AND [t].[SalesDocumentVersion] = {nVersion.ToString()}";
	}
}
