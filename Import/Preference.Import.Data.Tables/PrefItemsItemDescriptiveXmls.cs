namespace Preference.Import.Data.Tables;

internal class PrefItemsItemDescriptiveXmls : BaseTable
{
	public PrefItemsItemDescriptiveXmls()
		: base("PrefItems", "ItemDescriptiveXmls")
	{
	}

	public override string GetDeleteQuery(int nNumber, int nVersion)
	{
		return $"DELETE pid FROM [{base.Schema}].[{base.Name}] pid INNER JOIN [dbo].[ContenidoPAFBlob] cpb ON pid.ItemId = cpb.RowId WHERE cpb.Numero = {nNumber.ToString()} AND cpb.Version = {nVersion.ToString()}";
	}

	public override string GetSelectQuery(int nNumber, int nVersion)
	{
		return $"SELECT * FROM [{base.Schema}].[{base.Name}] WHERE ItemId IN (SELECT RowId FROM [dbo].[ContenidoPAFBlob] WHERE Numero = {nNumber.ToString()} AND Version = {nVersion.ToString()})";
	}
}
