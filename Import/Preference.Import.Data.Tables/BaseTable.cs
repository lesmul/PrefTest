namespace Preference.Import.Data.Tables;

internal abstract class BaseTable
{
	public string FullName
	{
		get
		{
			if (Schema != null && Name != null)
			{
				return $"[{Schema}].[{Name}]";
			}
			if (Name != null)
			{
				return Name;
			}
			return null;
		}
	}

	public string Name { get; protected set; }

	public string Schema { get; protected set; }

	private string NumberFieldName { get; set; }

	public BaseTable(string strSchemaName, string strTableName, string strNumberFieldName = "Numero")
	{
		Schema = strSchemaName;
		Name = strTableName;
		NumberFieldName = strNumberFieldName;
	}

	public virtual string GetDeleteQuery(int nNumber, int nVersion)
	{
		return $"DELETE FROM [{Schema}].[{Name}] WHERE [{NumberFieldName}] = {nNumber} AND [Version] = {nVersion}";
	}

	public virtual string GetDisableTriggersQuery()
	{
		return $"ALTER TABLE [{Schema}].[{Name}] DISABLE TRIGGER ALL";
	}

	public virtual string GetEnableTriggersQuery()
	{
		return $"ALTER TABLE [{Schema}].[{Name}] ENABLE TRIGGER ALL";
	}

	public virtual string GetSelectQuery(int nNumber, int nVersion)
	{
		return $"SELECT * FROM [{Schema}].[{Name}] WHERE [{NumberFieldName}] = {nNumber} AND [Version] = {nVersion}";
	}
}
