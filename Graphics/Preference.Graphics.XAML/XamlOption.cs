using System.Data.SqlClient;

namespace Preference.Graphics.XAML;

public class XamlOption
{
	private const string constSelectQuery = "SELECT XamlSilverlight FROM vwContenidoOpciones WHERE DataVerId = N'{0}' AND Opcion = N'{1}' AND Orden = N'{2}'";

	private const string constUpdateQuery = "UPDATE ContenidoOpciones SET XamlSilverlight = @vbBuffer WHERE DataVerId = N'{0}' AND Opcion = N'{1}' AND Orden = N'{2}'";

	public static string GetXamlFromDB(string strConnection, string strDataVerId, string strOpcion, string strOrden)
	{
		return XamlObject.GetXamlFromDB(strConnection, $"SELECT XamlSilverlight FROM vwContenidoOpciones WHERE DataVerId = N'{strDataVerId}' AND Opcion = N'{strOpcion}' AND Orden = N'{strOrden}'");
	}

	public static string GetXamlFromDB(SqlConnection sqlConn, string strDataVerId, string strOpcion, string strOrden)
	{
		return XamlObject.GetXamlFromDB(sqlConn, $"SELECT XamlSilverlight FROM vwContenidoOpciones WHERE DataVerId = N'{strDataVerId}' AND Opcion = N'{strOpcion}' AND Orden = N'{strOrden}'");
	}

	public static bool UpdateXamlToDB(string strConnection, string strDataVerId, string strOpcion, string strOrden, string strXAMLCode)
	{
		return XamlObject.UpdateXamlToDB(strConnection, $"SELECT XamlSilverlight FROM vwContenidoOpciones WHERE DataVerId = N'{strDataVerId}' AND Opcion = N'{strOpcion}' AND Orden = N'{strOrden}'", strXAMLCode);
	}

	public static bool UpdateXamlToDB(SqlConnection connection, string strDataVerId, string strOpcion, string strOrden, string strXAMLCode)
	{
		return XamlObject.UpdateXamlToDB(connection, $"UPDATE ContenidoOpciones SET XamlSilverlight = @vbBuffer WHERE DataVerId = N'{strDataVerId}' AND Opcion = N'{strOpcion}' AND Orden = N'{strOrden}'", strXAMLCode);
	}

	public static bool UpdateXamlToDB(string strConnection, string strDataVerId, string strOpcion, string strOrden, byte[] buffer)
	{
		return XamlObject.UpdateXamlToDB(strConnection, $"UPDATE ContenidoOpciones SET XamlSilverlight = @vbBuffer WHERE DataVerId = N'{strDataVerId}' AND Opcion = N'{strOpcion}' AND Orden = N'{strOrden}'", buffer);
	}

	public static bool UpdateXamlToDB(SqlConnection connection, string strDataVerId, string strOpcion, string strOrden, byte[] buffer)
	{
		return XamlObject.UpdateXamlToDB(connection, $"UPDATE ContenidoOpciones SET XamlSilverlight = @vbBuffer WHERE DataVerId = N'{strDataVerId}' AND Opcion = N'{strOpcion}' AND Orden = N'{strOrden}'", buffer);
	}
}
