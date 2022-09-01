using System;
using System.Data.SqlClient;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Preference.Import.Data;

public static class Manager
{
	public static void CreateDatabase(string strSqlConnectionString, string strDatabaseName, string strCollation)
	{
		ExecuteNonQuery(strSqlConnectionString, $"CREATE DATABASE [{strDatabaseName}] COLLATE {strCollation}", 0);
	}

	public static void DeleteDatabase(string strSqlConnectionString, string strDatabaseName)
	{
		ExecuteNonQuery(strSqlConnectionString, $"DROP DATABASE [{strDatabaseName}]");
	}

	public static void ExecuteNonQuery(string strSqlConnectionString, string strCommandQuery, int? timeout = null)
	{
		using SqlConnection sqlConnection = new SqlConnection(strSqlConnectionString);
		using SqlCommand sqlCommand = new SqlCommand(strCommandQuery, sqlConnection);
		if (timeout.HasValue)
		{
			sqlCommand.CommandTimeout = timeout.Value;
		}
		sqlConnection.Open();
		sqlCommand.ExecuteNonQuery();
		sqlConnection.Close();
	}

	public static string GetCollation(string strSqlConnectionString)
	{
		SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder(strSqlConnectionString);
		string strSqlCommand = $"SELECT DATABASEPROPERTYEX('{sqlConnectionStringBuilder.InitialCatalog}', 'Collation')";
		return GetScalarStringValue(strSqlConnectionString, strSqlCommand);
	}

	public static string GetGlobalVariable(string strSqlConnectionString, string strGlobalVariableName, string strFieldName)
	{
		string strSqlCommand = $"SELECT {strFieldName} FROM dbo.VariablesGlobales WHERE Empresa = 1 AND Nombre = N'{strGlobalVariableName}'";
		return GetScalarStringValue(strSqlConnectionString, strSqlCommand);
	}

	public static bool RunDBManager(string strOLEDBConnectionString, string strPrefUserDllName)
	{
		string arg = string.Empty;
		if (!string.IsNullOrEmpty(strPrefUserDllName))
		{
			arg = $"<cmd:defaultProperties><cmd:prefUserDll name='{strPrefUserDllName}'/></cmd:defaultProperties>";
		}
		string text = $"<cmd:batch xmlns:cmd='PrefCommand' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'>{arg}<cmd:commands><cmd:updateDBStructure><cmd:connection connectionString='{strOLEDBConnectionString}'/></cmd:updateDBStructure></cmd:commands></cmd:batch>";
		object obj = null;
		object obj2 = null;
		bool flag = false;
		try
		{
			Type typeFromProgID = Type.GetTypeFromProgID("PrefCommand.PrefCommand");
			if (typeFromProgID == null)
			{
				throw new Exception("PrefCommand.dll not found or not registered on your computer.");
			}
			obj = Activator.CreateInstance(typeFromProgID);
			flag = Convert.ToBoolean(typeFromProgID.InvokeMember("ParseXml", BindingFlags.InvokeMethod, null, obj, new object[1] { text }));
			if (!flag)
			{
				Type typeFromProgID2 = Type.GetTypeFromProgID("PrefComponents.PrefMessages");
				if (typeFromProgID2 == null)
				{
					throw new Exception("PrefComponents.dll not found or not registered on your computer.");
				}
				obj2 = typeFromProgID.InvokeMember("Messages", BindingFlags.GetProperty, null, obj, null);
				typeFromProgID2.InvokeMember("ShowDialog", BindingFlags.InvokeMethod, null, obj2, new object[1] { "PrefDBManager Log" });
				return flag;
			}
			return flag;
		}
		finally
		{
			if (obj != null)
			{
				Marshal.ReleaseComObject(obj);
			}
			if (obj2 != null)
			{
				Marshal.ReleaseComObject(obj2);
			}
		}
	}

	public static void SetGlobalVariable(string strSqlConnectionString, string strGlobalVariableName, string strFieldName, string strFieldValue)
	{
		string strCommandQuery = string.Format("IF EXISTS(SELECT * FROM VariablesGlobales WHERE Empresa = 1 AND Nombre = N'{0}') BEGIN UPDATE VariablesGlobales SET {1} = N'{2}' WHERE Nombre = N'{0}' END ELSE BEGIN INSERT INTO VariablesGlobales (Empresa, Nombre, {1}) values (1, N'{0}', N'{2}') END", strGlobalVariableName, strFieldName, strFieldValue);
		ExecuteNonQuery(strSqlConnectionString, strCommandQuery);
	}

	public static string GetScalarStringValue(string strSqlConnectionString, string strSqlCommand)
	{
		try
		{
			using SqlConnection sqlConnection = new SqlConnection(strSqlConnectionString);
			using SqlCommand sqlCommand = new SqlCommand(strSqlCommand, sqlConnection);
			sqlConnection.Open();
			object obj = sqlCommand.ExecuteScalar();
			sqlConnection.Close();
			return obj?.ToString();
		}
		catch
		{
			return null;
		}
	}
}
