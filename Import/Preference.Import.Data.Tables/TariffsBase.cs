namespace Preference.Import.Data.Tables;

internal class TariffsBase : BaseTable
{
	public TariffsBase()
		: base("dbo", "TariffsBase")
	{
	}

	public override string GetDeleteQuery(int nNumber, int nVersion)
	{
		return $"DELETE tb FROM [{base.Schema}].[{base.Name}] tb INNER JOIN dbo.Tariff t ON t.RowId = tb.TariffRowId WHERE [t].[SalesDocumentNumber] = {nNumber.ToString()} AND [t].[SalesDocumentVersion] = {nVersion.ToString()}";
	}

	public override string GetSelectQuery(int nNumber, int nVersion)
	{
		return $"SELECT tb.* FROM [{base.Schema}].[{base.Name}] tb INNER JOIN dbo.Tariff t ON t.RowId = tb.TariffRowId WHERE [t].[SalesDocumentNumber] = {nNumber.ToString()} AND [t].[SalesDocumentVersion] = {nVersion.ToString()}";
	}
}
