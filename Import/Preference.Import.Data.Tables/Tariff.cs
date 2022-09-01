namespace Preference.Import.Data.Tables;

internal class Tariff : BaseTable
{
	public Tariff()
		: base("dbo", "Tariff")
	{
	}

	public override string GetDeleteQuery(int nNumber, int nVersion)
	{
		return $"DELETE FROM [{base.Schema}].[{base.Name}] WHERE [SalesDocumentNumber] = {nNumber.ToString()} AND [SalesDocumentVersion] = {nVersion.ToString()}";
	}

	public override string GetSelectQuery(int nNumber, int nVersion)
	{
		return $"SELECT * FROM [{base.Schema}].[{base.Name}] WHERE [SalesDocumentNumber] = {nNumber.ToString()} AND [SalesDocumentVersion] = {nVersion.ToString()}";
	}
}
