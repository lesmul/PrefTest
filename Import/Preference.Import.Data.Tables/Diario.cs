namespace Preference.Import.Data.Tables;

internal class Diario : BaseTable
{
	public Diario()
		: base("dbo", "Diario")
	{
	}

	public override string GetDeleteQuery(int nNumber, int nVersion)
	{
		return $"DELETE FROM [{base.Schema}].[{base.Name}] WHERE [Numero] = {nNumber.ToString()}";
	}

	public override string GetSelectQuery(int nNumber, int nVersion)
	{
		return $"SELECT * FROM [{base.Schema}].[{base.Name}] WHERE [Numero] = {nNumber.ToString()}";
	}
}
