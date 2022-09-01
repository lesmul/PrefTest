namespace Preference.Import.Data.Tables;

internal class CustomTable : BaseTable
{
	public string DeleteQuery { get; private set; }

	public string SelectQuery { get; private set; }

	public CustomTable(string schema, string name, string selectQuery, string deleteQuery)
		: base(schema, name)
	{
		SelectQuery = selectQuery;
		DeleteQuery = deleteQuery;
	}

	public override string GetDeleteQuery(int nNumber, int nVersion)
	{
		return string.Format(DeleteQuery, nNumber, nVersion);
	}

	public override string GetSelectQuery(int nNumber, int nVersion)
	{
		return string.Format(SelectQuery, nNumber, nVersion);
	}
}
