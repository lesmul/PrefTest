using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Preference.Purchases;

internal static class PurchasesCore
{
	public static double GetPesoLineal(SqlConnection conn, SqlTransaction transaction, string reference)
	{
		using (SqlCommand sqlCommand = new SqlCommand("SELECT P.PesoLineal FROM Perfiles P INNER JOIN MaterialesBase MB ON MB.ReferenciaBase = P.ReferenciaBase INNER JOIN Materiales M ON M.ReferenciaBase = MB.ReferenciaBase WHERE MB.TipoCalculo = N'Barras' AND M.Referencia = @reference", conn, transaction))
		{
			SqlParameter value = new SqlParameter
			{
				ParameterName = "@reference",
				SqlDbType = SqlDbType.NChar,
				Value = reference
			};
			sqlCommand.Parameters.Add(value);
			object obj = sqlCommand.ExecuteScalar();
			if (obj != null && !(obj is DBNull))
			{
				return Convert.ToDouble(obj, CultureInfo.InvariantCulture);
			}
		}
		return 0.0;
	}

	public static Dictionary<int, double> GetProviderShare(SqlConnection conn, SqlTransaction transaction, string reference)
	{
		Dictionary<int, double> dictionary = new Dictionary<int, double>();
		SqlCommand obj = new SqlCommand
		{
			CommandText = "SELECT SupplierCode, ISNULL(Percentage, 0.0) AS Percentage, CASE WHEN SupplierCode = (SELECT ISNULL(M.MaterialSupplierCode, MB.CodigoProveedor) FROM dbo.Materiales M INNER JOIN MaterialesBase MB ON MB.ReferenciaBase = M.Referencia WHERE M.Referencia = @reference) THEN 1 ELSE 0 END AS IsDefaultSupplier FROM dbo.ReferenceSuppliers WHERE Reference = @reference ORDER BY IsDefaultSupplier DESC, Percentage",
			Connection = conn,
			Transaction = transaction
		};
		SqlParameter value = new SqlParameter
		{
			ParameterName = "@reference",
			SqlDbType = SqlDbType.NChar,
			Value = reference
		};
		obj.Parameters.Add(value);
		SqlDataReader sqlDataReader = obj.ExecuteReader();
		while (sqlDataReader.Read())
		{
			int @int = sqlDataReader.GetInt32(0);
			double @double = sqlDataReader.GetDouble(1);
			dictionary.Add(@int, @double);
		}
		sqlDataReader.Close();
		return dictionary;
	}

	public static void GetBestPurchasePrice(SqlConnection conn, SqlTransaction transaction, long lProviderCode, string strReference, string strKind, double fLength, double fHeight, double fVolume, ref long lQuantity, out long lPackingUnit1, out double dblPackingUnit2, out double dblTotalPacking, int nUnitsMode, out double dblPurchasePrice, out int nAverageDelivery, out DateTime tScheduledPurchase, out string supplierReference, DateTime tScheduledDelivery, string defaultCurrency)
	{
		string text = "Referencia = N'" + strReference.Replace("'", "''") + "' AND Proveedor = " + lProviderCode;
		dblTotalPacking = 0.0;
		nAverageDelivery = 0;
		tScheduledPurchase = DateTime.Today;
		supplierReference = "";
		SqlCommand obj = new SqlCommand
		{
			CommandText = "SELECT APartir, UP1, UP2, FechaUltimaCompra, ISNULL(dbo.Currencies_Convert(PrecioUltimaCompra, Divisa, @currency), 0.0) as PrecioUltimaCompra, FechaEVPrecioSC, ISNULL(dbo.Currencies_Convert(PrecioSC, DivisaPrecioSC, @currency), 0.0) as PrecioSC, ISNULL(SchedulerTime, 0), dbo.Schedule_SubstractDays(@tScheduledDelivery, ISNULL(SchedulerTime, 0)) AS ScheduledPurchaseDate, ISNULL(ReferenciaProveedor, N'') AS SupplierReference FROM dbo.Compras WHERE " + text
		};
		SqlParameter value = new SqlParameter
		{
			ParameterName = "@currency",
			SqlDbType = SqlDbType.NChar,
			Value = defaultCurrency
		};
		obj.Parameters.Add(value);
		SqlParameter value2 = new SqlParameter
		{
			ParameterName = "@tScheduledDelivery",
			SqlDbType = SqlDbType.Date,
			Value = tScheduledDelivery
		};
		obj.Parameters.Add(value2);
		obj.Connection = conn;
		obj.Transaction = transaction;
		SqlDataReader sqlDataReader = obj.ExecuteReader();
		if (!sqlDataReader.HasRows)
		{
			sqlDataReader.Close();
			lPackingUnit1 = 1L;
			dblPackingUnit2 = GetBaseUnit(nUnitsMode, strKind);
			dblPurchasePrice = 0.0;
			return;
		}
		bool flag = true;
		double num = 0.0;
		long num2 = 0L;
		double num3 = 0.0;
		double num4 = 0.0;
		double num5 = 0.0;
		long num6 = 0L;
		int num7 = 0;
		DateTime dateTime = DateTime.Today;
		while (sqlDataReader.Read())
		{
			DateTime today = DateTime.Today;
			double @double;
			if (!sqlDataReader.IsDBNull(5) && today >= sqlDataReader.GetDateTime(5))
			{
				@double = sqlDataReader.GetDouble(6);
				@double = Math.Round(@double, 5);
			}
			else
			{
				@double = sqlDataReader.GetDouble(4);
				@double = Math.Round(@double, 5);
			}
			long num8 = sqlDataReader.GetInt32(1);
			double num9 = sqlDataReader.GetDouble(2);
			long num10 = sqlDataReader.GetInt32(0);
			if (num8 < 1)
			{
				num8 = 1L;
			}
			if (num9 <= 0.0)
			{
				num9 = 1.0;
			}
			if (num8 > 0 && !(num9 <= 0.0))
			{
				double value3 = (double)lQuantity / ((double)num8 * num9);
				value3 = Math.Round(value3, 6);
				long num11 = (long)value3;
				if (Math.Round((double)num11, 6) < value3)
				{
					num11++;
				}
				if (num10 > num11)
				{
					num11 = num10;
				}
				double totalPacking = GetTotalPacking(strKind, num11, num8, num9, fLength, fHeight, fVolume);
				nAverageDelivery = sqlDataReader.GetInt32(7);
				tScheduledPurchase = sqlDataReader.GetDateTime(8);
				if (flag)
				{
					num = @double * totalPacking;
					num2 = num8;
					num3 = num9;
					num4 = @double;
					num5 = totalPacking;
					num6 = num11;
					num7 = nAverageDelivery;
					dateTime = tScheduledPurchase;
					supplierReference = sqlDataReader.GetString(9);
					flag = false;
				}
				else if (@double * totalPacking < num)
				{
					num = @double * totalPacking;
					num2 = num8;
					num3 = num9;
					num4 = @double;
					num5 = totalPacking;
					num6 = num11;
					num7 = nAverageDelivery;
					dateTime = tScheduledPurchase;
					supplierReference = sqlDataReader.GetString(9);
				}
			}
		}
		sqlDataReader.Close();
		lQuantity = num6;
		lPackingUnit1 = num2;
		dblPackingUnit2 = num3;
		dblTotalPacking = num5;
		dblPurchasePrice = num4;
		nAverageDelivery = num7;
		tScheduledPurchase = dateTime;
	}

	private static double GetBaseUnit(int nUnitsMode, string strKind)
	{
		if (strKind != "Metros")
		{
			return 1.0;
		}
		switch (nUnitsMode)
		{
		case 0:
			return 1.0;
		case 1:
		case 2:
			return 0.3048;
		default:
			return 1.0;
		}
	}

	private static double GetTotalPacking(string strKind, long lLineQuantity, long lUP1, double dblUP2, double fLength, double fHeight, double fVolume)
	{
		double num = (double)(lLineQuantity * lUP1) * dblUP2;
		if (strKind == "Barras")
		{
			num = num * fLength / 1000.0;
		}
		if (strKind == "Superficies")
		{
			num *= fLength * fHeight / 1000000.0;
		}
		if (strKind == "Volume")
		{
			num *= fVolume;
		}
		return num;
	}

	[DllImport("Preference.Purchases.Core.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto, EntryPoint = "GetReferenceWeight")]
	internal static extern double CalculateMaterialWeight(string connectionString, bool useRemoteFactory, string baseReference, string color, double lenght, double height, double volume);

	public static void GetSurfaceToInvoice(SqlConnection conn, SqlTransaction transaction, string reference, double length, double height, out double lengthToInvoice, out double heightToInvoice, out double minimumToInvoice)
	{
		lengthToInvoice = length;
		heightToInvoice = height;
		minimumToInvoice = 0.0;
		SqlCommand obj = new SqlCommand
		{
			CommandText = "SELECT ISNULL(S.MultiploHorizontal, 0.0) as LengthToInvoice, ISNULL(S.MultiploVertical, 0.0) as HeightToInvoice, ISNULL(S.MinimoM2, 0.0) as MinimumToInvoice FROM Superficies S INNER JOIN MaterialesBase MB ON MB.ReferenciaBase=S.ReferenciaBase INNER JOIN Materiales M ON M.ReferenciaBase=MB.ReferenciaBase WHERE MB.TipoCalculo = N'Superficies' AND M.Referencia = @reference",
			Connection = conn,
			Transaction = transaction
		};
		SqlParameter value = new SqlParameter
		{
			ParameterName = "@reference",
			SqlDbType = SqlDbType.NChar,
			Value = reference
		};
		obj.Parameters.Add(value);
		using SqlDataReader sqlDataReader = obj.ExecuteReader();
		while (sqlDataReader.Read())
		{
			double @double = sqlDataReader.GetDouble(0);
			double double2 = sqlDataReader.GetDouble(1);
			double double3 = sqlDataReader.GetDouble(2);
			if (0.0 < @double)
			{
				double num = length % @double;
				if (0.0 < num)
				{
					lengthToInvoice = length + (@double - num);
				}
			}
			if (0.0 < double2)
			{
				double num2 = height % double2;
				if (0.0 < num2)
				{
					heightToInvoice = height + (double2 - num2);
				}
			}
			if (0.0 < double3)
			{
				minimumToInvoice = double3;
			}
		}
	}

	public static void CalculateTotalToInvoice(long quantity, long packingUnit1, double packingUnit2, double lengthToInvoice, double heightToInvoice, double minimumToInvoice, out double totalToInvoice)
	{
		double num = lengthToInvoice * heightToInvoice / 1000000.0;
		if (num < minimumToInvoice)
		{
			num = minimumToInvoice;
		}
		totalToInvoice = num * (double)quantity * (double)packingUnit1 * packingUnit2;
	}

	[DllImport("Preference.Purchases.Core.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto)]
	internal static extern bool FillGlassData(string connectionString, string materialNeedGuid, string elementId, double width, double height, int number, int numeration, int lineId);
}
