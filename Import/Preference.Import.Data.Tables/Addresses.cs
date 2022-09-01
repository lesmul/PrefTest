namespace Preference.Import.Data.Tables;

internal class Addresses : BaseTable
{
	public Addresses()
		: base("dbo", "Addresses")
	{
	}

	public override string GetDeleteQuery(int nNumber, int nVersion)
	{
		return $"DELETE ad FROM [{base.Schema}].[{base.Name}] ad INNER JOIN AddressesPAF adp ON adp.AddressId = ad.AddressId WHERE adp.Number = {nNumber.ToString()} AND adp.Version = {nVersion.ToString()}";
	}

	public override string GetSelectQuery(int nNumber, int nVersion)
	{
		return $"SELECT * FROM [{base.Schema}].[{base.Name}] WHERE [AddressId] IN (SELECT [AddressId] FROM [dbo].[AddressesPAF] WHERE [Number] = {nNumber.ToString()} AND [Version] = {nVersion.ToString()})";
	}
}
