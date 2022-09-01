using System.Data;
using System.Data.SqlClient;
using zlib;

namespace Preference.Graphics.XAML;

internal class XamlObject
{
	internal static string GetXamlFromDB(string strConnection, string strSelectQuery)
	{
		SqlConnection sqlConnection = new SqlConnection(strConnection);
		string result = null;
		try
		{
			sqlConnection.Open();
			CPrefZipNET.UnzipXmlFromDB(strSelectQuery, strConnection, ref result);
			return result;
		}
		catch
		{
			return result;
		}
		finally
		{
			sqlConnection.Close();
		}
	}

	internal static string GetXamlFromDB(SqlConnection sqlConn, string strSelectQuery)
	{
		string result = null;
		CPrefZipNET.UnzipXmlFromDB(strSelectQuery, sqlConn, ref result);
		return result;
	}

	internal static bool UpdateXamlToDB(string strConnection, string strUpdateQuery, string strXAMLCode, bool updateZipped = true)
	{
		SqlConnection sqlConnection = new SqlConnection(strConnection);
		bool result = true;
		try
		{
			sqlConnection.Open();
			SqlParameter sqlParameter;
			if (updateZipped)
			{
				sqlParameter = new SqlParameter("@vbBuffer", SqlDbType.VarBinary);
				byte[] sqlValue = default(byte[]);
				CPrefZipNET.ZipString(strXAMLCode, ref sqlValue);
				sqlParameter.SqlValue = sqlValue;
			}
			else
			{
				sqlParameter = new SqlParameter("@vbXAML", SqlDbType.Xml);
				sqlParameter.Value = strXAMLCode;
			}
			SqlCommand sqlCommand = new SqlCommand(strUpdateQuery, sqlConnection);
			sqlCommand.Parameters.Add(sqlParameter);
			sqlCommand.ExecuteNonQuery();
			return result;
		}
		catch
		{
			return false;
		}
		finally
		{
			sqlConnection.Close();
		}
	}

	internal static bool UpdateXamlToDB(SqlConnection connection, string strUpdateQuery, string strXAMLCode, bool updateZipped = true)
	{
		bool result = true;
		try
		{
			SqlParameter sqlParameter;
			if (updateZipped)
			{
				sqlParameter = new SqlParameter("@vbBuffer", SqlDbType.VarBinary);
				byte[] sqlValue = default(byte[]);
				CPrefZipNET.ZipString(strXAMLCode, ref sqlValue);
				sqlParameter.SqlValue = sqlValue;
			}
			else
			{
				sqlParameter = new SqlParameter("@vbXAML", SqlDbType.Xml);
				sqlParameter.Value = strXAMLCode;
			}
			SqlCommand sqlCommand = new SqlCommand(strUpdateQuery, connection);
			sqlCommand.Parameters.Add(sqlParameter);
			sqlCommand.ExecuteNonQuery();
			return result;
		}
		catch
		{
			return false;
		}
	}

	internal static bool UpdateXamlToDB(string strConnection, string strUpdateQuery, byte[] buffer)
	{
		SqlConnection sqlConnection = new SqlConnection(strConnection);
		bool result = true;
		try
		{
			sqlConnection.Open();
			SqlParameter sqlParameter = new SqlParameter("@vbBuffer", SqlDbType.VarBinary);
			byte[] sqlValue = default(byte[]);
			if (CPrefZipNET.IsZipped(buffer))
			{
				sqlParameter.SqlValue = buffer;
			}
			else if (CPrefZipNET.ZipBLOB(buffer, ref sqlValue))
			{
				sqlParameter.SqlValue = sqlValue;
			}
			SqlCommand sqlCommand = new SqlCommand(strUpdateQuery, sqlConnection);
			sqlCommand.Parameters.Add(sqlParameter);
			sqlCommand.ExecuteNonQuery();
			return result;
		}
		catch
		{
			return false;
		}
		finally
		{
			sqlConnection.Close();
		}
	}

	internal static bool UpdateXamlToDB(SqlConnection connection, string strUpdateQuery, byte[] buffer)
	{
		bool result = true;
		try
		{
			SqlParameter sqlParameter = new SqlParameter("@vbBuffer", SqlDbType.VarBinary);
			byte[] sqlValue = default(byte[]);
			if (CPrefZipNET.IsZipped(buffer))
			{
				sqlParameter.SqlValue = buffer;
			}
			else if (CPrefZipNET.ZipBLOB(buffer, ref sqlValue))
			{
				sqlParameter.SqlValue = sqlValue;
			}
			SqlCommand sqlCommand = new SqlCommand(strUpdateQuery, connection);
			sqlCommand.Parameters.Add(sqlParameter);
			sqlCommand.ExecuteNonQuery();
			return result;
		}
		catch
		{
			return false;
		}
	}
}
