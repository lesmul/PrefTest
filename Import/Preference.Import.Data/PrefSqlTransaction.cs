using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Preference.Import.Data;

internal class PrefSqlTransaction : IDisposable
{
	internal enum ExecuteType
	{
		Source,
		Dest
	}

	private SqlConnection SqlConnectionDest { get; set; }

	private SqlConnection SqlConnectionSource { get; set; }

	private SqlTransaction SqlTransactionDest { get; set; }

	private SqlTransaction SqlTransactionSource { get; set; }

	private int CommandTimeout { get; set; }

	public event ProgressChangedEventHandler ProgressChanged;

	public PrefSqlTransaction(SqlConnection sqlConnectionSource, SqlConnection sqlConnectionDestination, int commandTimeout)
	{
		SqlConnectionSource = sqlConnectionSource;
		SqlTransactionSource = SqlConnectionSource.BeginTransaction("SqlTransactionSource");
		SqlConnectionDest = sqlConnectionDestination;
		SqlTransactionDest = SqlConnectionDest.BeginTransaction("SqlTransactionDest");
		CommandTimeout = commandTimeout;
	}

	public void Commit()
	{
		SqlTransactionSource.Commit();
		SqlTransactionDest.Commit();
	}

	public void BulkCopy(IDictionary<string, string> commands, bool live)
	{
		string text = null;
		try
		{
			using SqlCommand sqlCommand = new SqlCommand();
			sqlCommand.CommandTimeout = CommandTimeout;
			SqlBulkCopyOptions sqlBulkCopyOptions = SqlBulkCopyOptions.KeepNulls;
			if (live)
			{
				sqlBulkCopyOptions |= SqlBulkCopyOptions.FireTriggers;
			}
			int count = commands.Count;
			int num = 1;
			foreach (KeyValuePair<string, string> command in commands)
			{
				text = (sqlCommand.CommandText = command.Value);
				if (command.Key.StartsWith("DELETE"))
				{
					sqlCommand.Connection = SqlConnectionDest;
					sqlCommand.Transaction = SqlTransactionDest;
					sqlCommand.ExecuteNonQuery();
				}
				else
				{
					using SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(SqlConnectionDest, sqlBulkCopyOptions, SqlTransactionDest);
					sqlCommand.Connection = SqlConnectionSource;
					sqlCommand.Transaction = SqlTransactionSource;
					SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
					sqlBulkCopy.DestinationTableName = command.Key;
					sqlBulkCopy.WriteToServer(sqlDataReader);
					sqlDataReader.Close();
				}
				int nPercentage = num * 100 / count;
				OnProgressChanged(command.Key, nPercentage);
				num++;
			}
		}
		catch (Exception ex)
		{
			string arg = string.Empty;
			if (!string.IsNullOrEmpty(text))
			{
				arg = $"Command : {text}";
			}
			throw new Exception($"\n\nError Executing Command \n\n{arg} \n\nSql Exception : {ex.Message}");
		}
	}

	public void Execute(ExecuteType exType, IDictionary<string, string> commands)
	{
		string text = string.Empty;
		SqlConnection connection;
		SqlTransaction transaction;
		if (exType == ExecuteType.Source)
		{
			connection = SqlConnectionSource;
			transaction = SqlTransactionSource;
		}
		else
		{
			connection = SqlConnectionDest;
			transaction = SqlTransactionDest;
		}
		try
		{
			using SqlCommand sqlCommand = new SqlCommand();
			sqlCommand.Connection = connection;
			sqlCommand.Transaction = transaction;
			sqlCommand.CommandTimeout = CommandTimeout;
			int count = commands.Count;
			int num = 1;
			foreach (KeyValuePair<string, string> command in commands)
			{
				text = (sqlCommand.CommandText = command.Value);
				int nPercentage = num * 100 / count;
				OnProgressChanged(command.Key, nPercentage);
				sqlCommand.ExecuteNonQuery();
				num++;
			}
		}
		catch (Exception ex)
		{
			Dispose();
			string arg = string.Empty;
			if (!string.IsNullOrEmpty(text))
			{
				arg = $"Command : {text}";
			}
			throw new Exception($"\n\nError Executing Command \n\n{arg} \n\nSql Exception : {ex.Message}");
		}
	}

	public void Rollback()
	{
		if (SqlTransactionSource != null)
		{
			SqlTransactionSource.Rollback();
		}
		if (SqlTransactionDest != null)
		{
			SqlTransactionDest.Rollback();
		}
	}

	public void Dispose()
	{
		SqlTransactionSource = null;
		SqlTransactionDest = null;
	}

	private void OnProgressChanged(string strMessage, int nPercentage)
	{
		if (this.ProgressChanged != null)
		{
			this.ProgressChanged(this, new ProgressChangedEventArgs(strMessage, nPercentage));
		}
	}
}
