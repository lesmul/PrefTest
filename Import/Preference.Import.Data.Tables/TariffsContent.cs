namespace Preference.Import.Data.Tables;

internal class TariffsContent : BaseTable
{
	public TariffsContent()
		: base("dbo", "TariffsContent")
	{
	}

	public override string GetDeleteQuery(int nNumber, int nVersion)
	{
		return $"DELETE tc FROM [{base.Schema}].[{base.Name}] tc INNER JOIN Tariff t ON t.RowId = tc.TariffRowId WHERE t.SalesDocumentNumber = {nNumber.ToString()} AND t.SalesDocumentVersion = {nVersion.ToString()}";
	}

	public override string GetSelectQuery(int nNumber, int nVersion)
	{
		return $"SELECT * FROM [{base.Schema}].[{base.Name}] WHERE [TariffRowId] IN (SELECT [RowId] FROM [dbo].[Tariff] WHERE [SalesDocumentNumber] = {nNumber.ToString()} AND [SalesDocumentVersion] = {nVersion.ToString()})";
	}
}
