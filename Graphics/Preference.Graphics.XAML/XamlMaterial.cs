using System.Data.SqlClient;

namespace Preference.Graphics.XAML;

public class XamlMaterial
{
	private const string constSelectQuery = "SELECT XamlSilverlight FROM vwMaterialesBase WHERE ReferenciaBase = N'{0}'";

	private const string constUpdateQuery = "UPDATE MaterialesBase SET XamlSilverlight = @vbBuffer WHERE ReferenciaBase = N'{0}' AND MakerId = dbo.GetMakerId()";

	public static string GetXamlFromDB(string strConnection, string strBaseRef)
	{
		return XamlObject.GetXamlFromDB(strConnection, $"SELECT XamlSilverlight FROM vwMaterialesBase WHERE ReferenciaBase = N'{strBaseRef}'");
	}

	public static string GetXamlFromDB(SqlConnection sqlConn, string strBaseRef)
	{
		return XamlObject.GetXamlFromDB(sqlConn, $"SELECT XamlSilverlight FROM vwMaterialesBase WHERE ReferenciaBase = N'{strBaseRef}'");
	}

	public static bool UpdateXamlToDB(string strConnection, string strBaseRef, string strXAMLCode)
	{
		return XamlObject.UpdateXamlToDB(strConnection, $"UPDATE MaterialesBase SET XamlSilverlight = @vbBuffer WHERE ReferenciaBase = N'{strBaseRef}' AND MakerId = dbo.GetMakerId()", strXAMLCode);
	}

	public static bool UpdateXamlToDB(SqlConnection connection, string strBaseRef, string strXAMLCode)
	{
		return XamlObject.UpdateXamlToDB(connection, $"UPDATE MaterialesBase SET XamlSilverlight = @vbBuffer WHERE ReferenciaBase = N'{strBaseRef}' AND MakerId = dbo.GetMakerId()", strXAMLCode);
	}

	public static bool UpdateXamlToDB(string strConnection, string strBaseRef, byte[] buffer)
	{
		return XamlObject.UpdateXamlToDB(strConnection, $"UPDATE MaterialesBase SET XamlSilverlight = @vbBuffer WHERE ReferenciaBase = N'{strBaseRef}' AND MakerId = dbo.GetMakerId()", buffer);
	}

	public static bool UpdateXamlToDB(SqlConnection connection, string strBaseRef, byte[] buffer)
	{
		return XamlObject.UpdateXamlToDB(connection, $"UPDATE MaterialesBase SET XamlSilverlight = @vbBuffer WHERE ReferenciaBase = N'{strBaseRef}' AND MakerId = dbo.GetMakerId()", buffer);
	}
}
