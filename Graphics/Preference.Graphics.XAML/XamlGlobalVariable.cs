using System.Data.SqlClient;

namespace Preference.Graphics.XAML;

public class XamlGlobalVariable
{
	private const string constSelectQuery = "SELECT XamlSilverlight FROM dbo.VariablesGlobales WHERE Nombre = N'{0}'";

	private const string constUpdateQuery = "UPDATE dbo.VariablesGlobales SET Silverlight = @vbXAML WHERE Nombre = N'{0}'";

	public static string GetXamlFromDB(string strConnection, string strName)
	{
		return XamlObject.GetXamlFromDB(strConnection, $"SELECT XamlSilverlight FROM dbo.VariablesGlobales WHERE Nombre = N'{strName}'");
	}

	public static string GetXamlFromDB(SqlConnection sqlConn, string strName)
	{
		return XamlObject.GetXamlFromDB(sqlConn, $"SELECT XamlSilverlight FROM dbo.VariablesGlobales WHERE Nombre = N'{strName}'");
	}

	public static bool UpdateXamlToDB(string strConnection, string strName, string strXAMLCode)
	{
		return XamlObject.UpdateXamlToDB(strConnection, $"UPDATE dbo.VariablesGlobales SET Silverlight = @vbXAML WHERE Nombre = N'{strName}'", strXAMLCode, updateZipped: false);
	}

	public static bool UpdateXamlToDB(SqlConnection connection, string strName, string strXAMLCode)
	{
		return XamlObject.UpdateXamlToDB(connection, $"UPDATE dbo.VariablesGlobales SET Silverlight = @vbXAML WHERE Nombre = N'{strName}'", strXAMLCode, updateZipped: false);
	}
}
