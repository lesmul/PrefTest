using System.Data.SqlClient;

namespace Preference.Graphics.XAML;

public class XamlSymbol
{
	private const string constSelectQuery = "SELECT XamlSilverlight FROM Symbols WHERE Name = N'{0}'";

	private const string constUpdateQuery = "UPDATE Symbols SET XamlSilverlight = @vbBuffer WHERE Name = N'{0}'";

	public static string GetXamlFromDB(string strConnection, string strName)
	{
		return XamlObject.GetXamlFromDB(strConnection, $"SELECT XamlSilverlight FROM Symbols WHERE Name = N'{strName}'");
	}

	public static string GetXamlFromDB(SqlConnection sqlConn, string strName)
	{
		return XamlObject.GetXamlFromDB(sqlConn, $"SELECT XamlSilverlight FROM Symbols WHERE Name = N'{strName}'");
	}

	public static bool UpdateXamlToDB(string strConnection, string strName, string strXAMLCode)
	{
		return XamlObject.UpdateXamlToDB(strConnection, $"UPDATE Symbols SET XamlSilverlight = @vbBuffer WHERE Name = N'{strName}'", strXAMLCode);
	}

	public static bool UpdateXamlToDB(SqlConnection connection, string strName, string strXAMLCode)
	{
		return XamlObject.UpdateXamlToDB(connection, $"UPDATE Symbols SET XamlSilverlight = @vbBuffer WHERE Name = N'{strName}'", strXAMLCode);
	}

	public static bool UpdateXamlToDB(string strConnection, string strName, byte[] buffer)
	{
		return XamlObject.UpdateXamlToDB(strConnection, $"UPDATE Symbols SET XamlSilverlight = @vbBuffer WHERE Name = N'{strName}'", buffer);
	}

	public static bool UpdateXamlToDB(SqlConnection connection, string strName, byte[] buffer)
	{
		return XamlObject.UpdateXamlToDB(connection, $"UPDATE Symbols SET XamlSilverlight = @vbBuffer WHERE Name = N'{strName}'", buffer);
	}
}
