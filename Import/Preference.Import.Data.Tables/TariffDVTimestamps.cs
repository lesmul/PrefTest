namespace Preference.Import.Data.Tables;

internal class TariffDVTimestamps : BaseTable
{
	public TariffDVTimestamps()
		: base("dbo", "TariffDVTimestamps")
	{
	}

	public override string GetDeleteQuery(int nNumber, int nVersion)
	{
		return $"DELETE tdvt FROM [{base.Schema}].[{base.Name}] tdvt INNER JOIN dbo.Tariff t ON t.RowId = tdvt.TariffRowId WHERE [t].[SalesDocumentNumber] = {nNumber.ToString()} AND [t].[SalesDocumentVersion] = {nVersion.ToString()}";
	}

	public override string GetSelectQuery(int nNumber, int nVersion)
	{
		return $"SELECT tdvt.* FROM [{base.Schema}].[{base.Name}] tdvt INNER JOIN dbo.Tariff t ON t.RowId = tdvt.TariffRowId WHERE [t].[SalesDocumentNumber] = {nNumber.ToString()} AND [t].[SalesDocumentVersion] = {nVersion.ToString()}";
	}
}
