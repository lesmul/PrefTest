using System.Data.SqlClient;

namespace Preference.Graphics.XAML;

public class XamlModel
{
	private const string constSelectQuery = "SELECT XamlSilverlight FROM vwDibujos WHERE Codigo = N'{0}'";

	private const string constUpdateQuery = "UPDATE Dibujos SET XamlSilverlight = @vbBuffer WHERE Codigo = N'{0}' AND MakerId = dbo.GetMakerId()";

	public static string GetXamlFromDB(string strConnection, string strCode)
	{
		return XamlObject.GetXamlFromDB(strConnection, $"SELECT XamlSilverlight FROM vwDibujos WHERE Codigo = N'{strCode}'");
	}

	public static string GetXamlFromDB(SqlConnection sqlConn, string strCode)
	{
		return XamlObject.GetXamlFromDB(sqlConn, $"SELECT XamlSilverlight FROM vwDibujos WHERE Codigo = N'{strCode}'");
	}

	public static bool UpdateXamlToDB(string strConnection, string strCode, string strXAMLCode)
	{
		return XamlObject.UpdateXamlToDB(strConnection, $"UPDATE Dibujos SET XamlSilverlight = @vbBuffer WHERE Codigo = N'{strCode}' AND MakerId = dbo.GetMakerId()", strXAMLCode);
	}

	public static bool UpdateXamlToDB(SqlConnection connection, string strCode, string strXAMLCode)
	{
		return XamlObject.UpdateXamlToDB(connection, $"UPDATE Dibujos SET XamlSilverlight = @vbBuffer WHERE Codigo = N'{strCode}' AND MakerId = dbo.GetMakerId()", strXAMLCode);
	}

	public static bool UpdateXamlToDB(string strConnection, string strCode, byte[] buffer)
	{
		return XamlObject.UpdateXamlToDB(strConnection, $"UPDATE Dibujos SET XamlSilverlight = @vbBuffer WHERE Codigo = N'{strCode}' AND MakerId = dbo.GetMakerId()", buffer);
	}

	public static bool UpdateXamlToDB(SqlConnection connection, string strCode, byte[] buffer)
	{
		return XamlObject.UpdateXamlToDB(connection, $"UPDATE Dibujos SET XamlSilverlight = @vbBuffer WHERE Codigo = N'{strCode}' AND MakerId = dbo.GetMakerId()", buffer);
	}
}
