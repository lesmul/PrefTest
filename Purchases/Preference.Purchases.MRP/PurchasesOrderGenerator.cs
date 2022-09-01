using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
using Preference.Exceptions;
using Preference.MSMQ.Client;
using Preference.Sql;
using PrefRegistry;

namespace Preference.Purchases.MRP;

public class PurchasesOrderGenerator
{
	private enum SupplierTasks
	{
		Ordering,
		Delivery
	}

	public string ConnectionString { get; set; }

	private string DefaultCurrency { get; set; }

	private Dictionary<string, ReferenceData> MapReferenceData { get; set; }

	private Dictionary<PurchasesHeader, List<PurchasesDetail>> MapOrders { get; set; }

	private Dictionary<PurchasesHeader, List<PurchasesDetail>> MapProviderOrders { get; set; }

	private Dictionary<PurchasesHeader, PurchasesDocKey> MapOrderKey { get; set; }

	public int Numeration { get; set; }

	public int Number { get; set; }

	public bool CreateUrgentOrders { get; set; }

	private List<ReferenceToBuy> ListToBuy { get; set; }

	public string ErrorMessage { get; set; }

	public int UserCode { get; set; }

	private short ShareAmongSuppliers { get; set; }

	public bool UseRemoteFactory { get; set; }

	private bool OneOrderPerSupplier { get; set; }

	private bool FillOpenOrders { get; set; }

	private Dictionary<Guid, int> SalesDocProviderCode { get; set; }

	public PurchasesOrderGenerator()
	{
		MapReferenceData = new Dictionary<string, ReferenceData>();
		MapOrders = new Dictionary<PurchasesHeader, List<PurchasesDetail>>();
		MapProviderOrders = new Dictionary<PurchasesHeader, List<PurchasesDetail>>();
		MapOrderKey = new Dictionary<PurchasesHeader, PurchasesDocKey>();
		ListToBuy = new List<ReferenceToBuy>();
		SalesDocProviderCode = new Dictionary<Guid, int>();
		Numeration = 0;
		Number = 0;
		UserCode = -1;
		ShareAmongSuppliers = 0;
		UseRemoteFactory = false;
	}

	public void AddReferenceToBuy(ReferenceToBuy reference)
	{
		ListToBuy.Add(reference);
	}

	public bool GenerateOrdersForAllMaterials(int nNumeration)
	{
		using SqlConnection sqlConnection = new SqlConnection(ConnectionString);
		sqlConnection.Open();
		SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
		SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("SELECT Siguiente FROM Numeraciones WHERE Purchase = 1 AND id = @numeration SELECT Reference, ColorConfiguration, ControlDate, RodLength, SurfaceHeight, ToBuy, WarehouseCode, ProviderCode, ElementId, MaterialNeedId FROM dbo.vwMRPData; ", sqlConnection);
		try
		{
			sqlDataAdapter.SelectCommand.Parameters.AddWithValue("numeration", nNumeration);
			sqlDataAdapter.SelectCommand.Transaction = sqlTransaction;
			DataSet dataSet = new DataSet();
			try
			{
				sqlDataAdapter.Fill(dataSet);
				if (dataSet.Tables.Count != 2)
				{
					ErrorMessage = "Error in MRP workflow query.";
					return false;
				}
				DataTable dataTable = dataSet.Tables[0];
				if (dataTable.Rows.Count < 1)
				{
					ErrorMessage = "Error in MRP: wrong numeration provided.";
					return false;
				}
				Number = Convert.ToInt32(dataTable.Rows[0].ItemArray[0], CultureInfo.InvariantCulture);
				Numeration = nNumeration;
				foreach (DataRow row in dataSet.Tables[1].Rows)
				{
					ReferenceToBuy referenceToBuy = default(ReferenceToBuy);
					referenceToBuy.Reference = Convert.ToString(row.ItemArray[0], CultureInfo.InvariantCulture);
					referenceToBuy.ColorConfiguration = Convert.ToInt32(row.ItemArray[1], CultureInfo.InvariantCulture);
					referenceToBuy.ControlDate = Convert.ToDateTime(row.ItemArray[2], CultureInfo.InvariantCulture);
					referenceToBuy.RodLength = Convert.ToDouble(row.ItemArray[3], CultureInfo.InvariantCulture);
					referenceToBuy.SurfaceHeight = Convert.ToDouble(row.ItemArray[4], CultureInfo.InvariantCulture);
					referenceToBuy.Quantity = Convert.ToDouble(row.ItemArray[5], CultureInfo.InvariantCulture);
					referenceToBuy.WarehouseCode = Convert.ToInt32(row.ItemArray[6], CultureInfo.InvariantCulture);
					referenceToBuy.ProviderCode = Convert.ToInt32(row.ItemArray[7], CultureInfo.InvariantCulture);
					referenceToBuy.GlassId = Convert.ToString(row.ItemArray[8], CultureInfo.InvariantCulture);
					referenceToBuy.MaterialNeedId = Guid.Parse(Convert.ToString(row.ItemArray[9], CultureInfo.InvariantCulture));
					ReferenceToBuy reference = referenceToBuy;
					AddReferenceToBuy(reference);
				}
			}
			finally
			{
				((IDisposable)dataSet)?.Dispose();
			}
		}
		finally
		{
			((IDisposable)(object)sqlDataAdapter)?.Dispose();
		}
		if (GenerateOrders(sqlConnection, sqlTransaction))
		{
			sqlTransaction.Commit();
			return SendToMsmqClient(bSuccess: true);
		}
		sqlTransaction.Rollback();
		SendToMsmqClient(bSuccess: false);
		return false;
	}

	public bool GenerateOrders()
	{
		using SqlConnection sqlConnection = new SqlConnection(ConnectionString);
		sqlConnection.Open();
		SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
		if (GenerateOrders(sqlConnection, sqlTransaction))
		{
			sqlTransaction.Commit();
			return SendToMsmqClient(bSuccess: true);
		}
		sqlTransaction.Rollback();
		SendToMsmqClient(bSuccess: false);
		return false;
	}

	private bool GenerateOrders(SqlConnection conn, SqlTransaction transaction)
	{
		LoadSettings(conn, transaction);
		LoadMRPReferencesData(conn, transaction);
		CreateOrders(conn, transaction);
		if (!CreatePurchaseDocs(conn, transaction))
		{
			return false;
		}
		if (!CreateDocumentRelationships(conn, transaction))
		{
			return false;
		}
		return true;
	}

	private void UpdateDocumentHeaderData(SqlConnection conn, SqlTransaction transaction, int number, int numeration)
	{
		using SqlCommand sqlCommand = new SqlCommand(";WITH cteTotalDetail (Number, Numeration, TotalAmount) AS  ( \tSELECT PD.Number, PD.Numeration, SUM(PD.RO_Amount) \tFROM dbo.PurchasesDetail PD \tWHERE PD.Number = @number AND PD.Numeration = @numeration \tGROUP BY PD.Number, PD.Numeration  )  UPDATE P  SET RO_NetTotal = CTE.TotalAmount,  RO_TaxTotal = ROUND(ISNULL(P.Tax, 0.0) / 100.0 * ISNULL(CTE.TotalAmount, 0.0), ISNULL(M.Decimales, 2)) + ROUND(ISNULL(P.Tax2, 0.0) / 100.0 * ISNULL(CTE.TotalAmount, 0.0), ISNULL(M.Decimales, 2)),  RO_Total = ISNULL(CTE.TotalAmount, 0.0) + ROUND(ISNULL(P.Tax, 0.0) / 100.0 * ISNULL(CTE.TotalAmount, 0.0), ISNULL(M.Decimales, 2)) + ROUND(ISNULL(P.Tax2, 0.0) / 100.0 * ISNULL(CTE.TotalAmount, 0.0), ISNULL(M.Decimales, 2))  FROM dbo.Purchases P  INNER JOIN cteTotalDetail CTE ON CTE.Number = P.Number AND CTE.Numeration = P.Numeration  INNER JOIN dbo.Monedas M ON P.Currency = M.Nombre  WHERE P.Number = @number AND P.Numeration = @numeration;", conn, transaction);
		sqlCommand.Parameters.Add(new SqlParameter("@number", number));
		sqlCommand.Parameters.Add(new SqlParameter("@numeration", numeration));
		sqlCommand.ExecuteNonQuery();
	}

	private bool SendToMsmqClient(bool bSuccess)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Expected O, but got Unknown
		PrefMsmqClient val = new PrefMsmqClient();
		if (val.Initialize(ConnectionString))
		{
			try
			{
				val.OnPurchasesAfterExecuteMRP(bSuccess, ErrorMessage);
			}
			catch (Exception ex)
			{
				ErrorMessage = PrefException.GetFormattedMessage(ex);
				return false;
			}
		}
		return true;
	}

	private void CreateOrders(SqlConnection conn, SqlTransaction transaction)
	{
		MapOrders.Clear();
		Dictionary<ReferenceMaterialKey, double> dictionary = new Dictionary<ReferenceMaterialKey, double>();
		foreach (ReferenceToBuy item in ListToBuy)
		{
			if (!MapReferenceData.TryGetValue(item.Reference, out var value))
			{
				continue;
			}
			double num = item.Quantity;
			if (value.CalculationType == "Barras")
			{
				num = item.RodLength * num / 1000.0;
			}
			if (value.CalculationType == "Superficies")
			{
				num = item.RodLength * item.SurfaceHeight * num / 1000000.0;
			}
			ReferenceMaterialKey referenceMaterialKey = default(ReferenceMaterialKey);
			referenceMaterialKey.Reference = item.Reference;
			referenceMaterialKey.ColorConfiguration = item.ColorConfiguration;
			ReferenceMaterialKey referenceMaterialKey2 = referenceMaterialKey;
			if (dictionary.TryGetValue(referenceMaterialKey2, out var value2))
			{
				if (value2 >= num)
				{
					dictionary[referenceMaterialKey2] = value2 - num;
					if (value2 == num)
					{
						dictionary.Remove(referenceMaterialKey2);
					}
					continue;
				}
				num -= value2;
				dictionary.Remove(referenceMaterialKey2);
			}
			long num2 = (long)item.Quantity;
			DateTime tScheduledDelivery = SubstractDays(conn, transaction, item.ControlDate, 1);
			Dictionary<int, double> dictionary2 = SetProviderShare(conn, transaction, item);
			double pendingQuantity = num2;
			foreach (KeyValuePair<int, double> item2 in dictionary2)
			{
				long num3 = (long)((double)num2 * item2.Value / 100.0);
				if (num3 > (long)pendingQuantity)
				{
					num3 = (long)pendingQuantity;
				}
				if (num3 > 0)
				{
					AddProviderLine(conn, transaction, item2, ref tScheduledDelivery, item, value, num3, num, dictionary, referenceMaterialKey2, ref pendingQuantity);
				}
			}
			if (pendingQuantity >= 1.0)
			{
				KeyValuePair<int, double> sharePair = dictionary2.Aggregate((KeyValuePair<int, double> l, KeyValuePair<int, double> r) => (!(l.Value > r.Value)) ? r : l);
				AddProviderLine(conn, transaction, sharePair, ref tScheduledDelivery, item, value, (long)pendingQuantity, num, dictionary, referenceMaterialKey2, ref pendingQuantity);
			}
		}
	}

	private Dictionary<int, double> SetProviderShare(SqlConnection conn, SqlTransaction transaction, ReferenceToBuy reference)
	{
		Dictionary<int, double> dictionary = new Dictionary<int, double>();
		switch (ShareAmongSuppliers)
		{
		case 0:
		case 2:
			dictionary.Add(reference.ProviderCode, 100.0);
			break;
		case 1:
			if (!string.IsNullOrWhiteSpace(reference.GlassId) && reference.MaterialNeedId != Guid.Empty)
			{
				Guid salesDocId = GetSalesDocId(conn, transaction, reference.MaterialNeedId);
				if (salesDocId != Guid.Empty)
				{
					if (SalesDocProviderCode.TryGetValue(salesDocId, out var value))
					{
						dictionary.Add(value, 100.0);
					}
					else
					{
						value = GetProviderCodeForGlass(conn, transaction, reference);
						dictionary.Add(value, 100.0);
						SalesDocProviderCode.Add(salesDocId, value);
					}
					return dictionary;
				}
			}
			dictionary = PurchasesCore.GetProviderShare(conn, transaction, reference.Reference);
			if (dictionary.Count <= 0)
			{
				dictionary.Add(reference.ProviderCode, 100.0);
			}
			break;
		}
		return dictionary;
	}

	private int GetProviderCodeForGlass(SqlConnection conn, SqlTransaction transaction, ReferenceToBuy reference)
	{
		Dictionary<int, double> providerShare = PurchasesCore.GetProviderShare(conn, transaction, reference.Reference);
		long num = (from detail in MapOrders.SelectMany((KeyValuePair<PurchasesHeader, List<PurchasesDetail>> order) => order.Value)
			where detail.Reference == reference.Reference
			select detail).Sum((PurchasesDetail det) => det.Quantity);
		if (num <= 0)
		{
			return providerShare.Aggregate((KeyValuePair<int, double> l, KeyValuePair<int, double> r) => (!(l.Value > r.Value)) ? r : l).Key;
		}
		Dictionary<int, double> dictionary = new Dictionary<int, double>();
		foreach (KeyValuePair<int, double> sharedProvider in providerShare)
		{
			double num2 = (double)(from detail in MapOrders.Where((KeyValuePair<PurchasesHeader, List<PurchasesDetail>> t) => t.Key.Provider == sharedProvider.Key).SelectMany((KeyValuePair<PurchasesHeader, List<PurchasesDetail>> order) => order.Value)
				where detail.Reference == reference.Reference
				select detail).Sum((PurchasesDetail det) => det.Quantity) * 100.0 / (double)num;
			double value = sharedProvider.Value - num2;
			dictionary.Add(sharedProvider.Key, value);
		}
		return dictionary.Aggregate((KeyValuePair<int, double> l, KeyValuePair<int, double> r) => (!(l.Value > r.Value)) ? r : l).Key;
	}

	private Guid GetSalesDocId(SqlConnection conn, SqlTransaction transaction, Guid materialNeedId)
	{
		using (SqlCommand sqlCommand = new SqlCommand("SELECT RowId FROM dbo.PAF P INNER JOIN dbo.MaterialNeeds MN ON MN.Number = P.Numero AND MN.Version = P.Version WHERE GUID = @materialNeedId", conn, transaction))
		{
			SqlParameter value = new SqlParameter
			{
				ParameterName = "@materialNeedId",
				SqlDbType = SqlDbType.UniqueIdentifier,
				Value = materialNeedId
			};
			sqlCommand.Parameters.Add(value);
			object obj = sqlCommand.ExecuteScalar();
			if (obj != null && !(obj is DBNull))
			{
				return new Guid(obj.ToString());
			}
		}
		return Guid.Empty;
	}

	private void AddProviderLine(SqlConnection conn, SqlTransaction transaction, KeyValuePair<int, double> sharePair, ref DateTime tScheduledDelivery, ReferenceToBuy reference, ReferenceData refData, long sharedQuantity, double dblNeededAmount, Dictionary<ReferenceMaterialKey, double> mapExceedingQuantities, ReferenceMaterialKey keyRef, ref double pendingQuantity)
	{
		GetNearestSupplierDate(conn, transaction, sharePair.Key, SupplierTasks.Delivery, ref tScheduledDelivery);
		if (tScheduledDelivery < DateTime.Today.Date)
		{
			tScheduledDelivery = DateTime.Today.Date;
		}
		PurchasesCore.GetBestPurchasePrice(conn, transaction, sharePair.Key, reference.Reference, refData.CalculationType, reference.RodLength, reference.SurfaceHeight, 0.0, ref sharedQuantity, out var lPackingUnit, out var dblPackingUnit, out var dblTotalPacking, CPrefRegistry.GetUnitsMode(), out var dblPurchasePrice, out var nAverageDelivery, out var tScheduledPurchase, out var supplierReference, tScheduledDelivery, DefaultCurrency);
		PurchasesDetail purchasesDetail = new PurchasesDetail
		{
			Reference = reference.Reference,
			ColorConfiguration = reference.ColorConfiguration,
			RodLength = reference.RodLength,
			SurfaceHeight = reference.SurfaceHeight,
			AverageDelivery = nAverageDelivery,
			PackingUnit1 = lPackingUnit,
			PackingUnit2 = dblPackingUnit,
			PurchasePrice = dblPurchasePrice,
			Quantity = sharedQuantity,
			TotalPacking = dblTotalPacking,
			SupplierReference = supplierReference,
			EstimatedReceptionDate = tScheduledDelivery,
			LengthToInvoice = reference.RodLength,
			HeightToInvoice = reference.SurfaceHeight,
			MinimumToInvoice = 0.0,
			GlassId = reference.GlassId,
			MaterialNeedId = reference.MaterialNeedId
		};
		if (tScheduledPurchase < DateTime.Today.Date && CreateUrgentOrders)
		{
			purchasesDetail.ToNegotiate = 1;
			tScheduledPurchase = DateTime.Today.Date;
		}
		if (purchasesDetail.TotalPacking > dblNeededAmount)
		{
			mapExceedingQuantities.Add(keyRef, purchasesDetail.TotalPacking - dblNeededAmount);
		}
		purchasesDetail.ScheduledPurchase = tScheduledPurchase;
		purchasesDetail.ScheduledDelivery = tScheduledDelivery;
		purchasesDetail.ConsumptionDate = reference.ControlDate;
		purchasesDetail.WarehouseCode = reference.WarehouseCode;
		purchasesDetail.ItemKg = PurchasesCore.CalculateMaterialWeight(ConnectionString, UseRemoteFactory, refData.BaseReference, refData.Color, reference.RodLength, reference.SurfaceHeight, 0.0);
		purchasesDetail.TotalKg = purchasesDetail.ItemKg * (double)lPackingUnit * dblPackingUnit * (double)sharedQuantity;
		if (refData.CalculationType == "Superficies")
		{
			FillDetailToInvoice(conn, transaction, reference, purchasesDetail);
			FillDetailTotalToInvoice(purchasesDetail);
		}
		else
		{
			purchasesDetail.TotalToInvoice = purchasesDetail.TotalPacking;
		}
		AddDetailLine(refData.TargetLevel, sharePair.Key, purchasesDetail);
		pendingQuantity -= (double)lPackingUnit * dblPackingUnit * (double)sharedQuantity;
	}

	private static void FillDetailTotalToInvoice(PurchasesDetail detailLine)
	{
		PurchasesCore.CalculateTotalToInvoice(detailLine.Quantity, detailLine.PackingUnit1, detailLine.PackingUnit2, detailLine.LengthToInvoice, detailLine.HeightToInvoice, detailLine.MinimumToInvoice, out var totalToInvoice);
		detailLine.TotalToInvoice = totalToInvoice;
	}

	private static void FillDetailToInvoice(SqlConnection conn, SqlTransaction transaction, ReferenceToBuy reference, PurchasesDetail detailLine)
	{
		PurchasesCore.GetSurfaceToInvoice(conn, transaction, reference.Reference, reference.RodLength, reference.SurfaceHeight, out var lengthToInvoice, out var heightToInvoice, out var minimumToInvoice);
		detailLine.HeightToInvoice = heightToInvoice;
		detailLine.LengthToInvoice = lengthToInvoice;
		detailLine.MinimumToInvoice = minimumToInvoice;
	}

	private DateTime SubstractDays(SqlConnection conn, SqlTransaction transaction, DateTime date, int days)
	{
		using SqlCommand sqlCommand = new SqlCommand("SELECT dbo.Schedule_SubstractDays(@date, @days)", conn, transaction);
		SqlParameter value = new SqlParameter
		{
			ParameterName = "@date",
			SqlDbType = SqlDbType.Date,
			Value = date
		};
		sqlCommand.Parameters.Add(value);
		SqlParameter value2 = new SqlParameter
		{
			ParameterName = "@days",
			SqlDbType = SqlDbType.Int,
			Value = days
		};
		sqlCommand.Parameters.Add(value2);
		object obj = sqlCommand.ExecuteScalar();
		if (obj != null)
		{
			if (!(obj is DBNull))
			{
				return Convert.ToDateTime(obj, CultureInfo.InvariantCulture);
			}
			return date;
		}
		return date;
	}

	private bool GetNearestSupplierDate(SqlConnection conn, SqlTransaction transaction, long supplierCode, SupplierTasks task, ref DateTime scheduledDelivery)
	{
		string text;
		switch (task)
		{
		case SupplierTasks.Ordering:
			text = "IsOrderingDay";
			break;
		case SupplierTasks.Delivery:
			text = "IsDeliveryDay";
			break;
		default:
			return false;
		}
		using (SqlCommand sqlCommand = new SqlCommand("SELECT TOP 1 SCH.Date FROM dbo.Schedule SCH INNER JOIN dbo.SupplierSchedule SS ON SS.[Year] = SCH.[Year] AND SS.DayOfYear = SCH.DayOfYear WHERE SS.SupplierCode = @supplierCode AND SCH.Date <= @date AND SS." + text + " = 1 ORDER BY SCH.Year DESC, SCH.DayOfYear DESC", conn, transaction))
		{
			SqlParameter value = new SqlParameter
			{
				ParameterName = "@supplierCode",
				SqlDbType = SqlDbType.Int,
				Value = supplierCode
			};
			sqlCommand.Parameters.Add(value);
			SqlParameter value2 = new SqlParameter
			{
				ParameterName = "@date",
				SqlDbType = SqlDbType.DateTime,
				Value = scheduledDelivery
			};
			sqlCommand.Parameters.Add(value2);
			object obj = sqlCommand.ExecuteScalar();
			if (obj is DBNull || obj == null)
			{
				return true;
			}
			scheduledDelivery = Convert.ToDateTime(obj, CultureInfo.InvariantCulture);
		}
		return true;
	}

	private void AddDetailLine(int targetLevel, int providerCode, PurchasesDetail detailLine)
	{
		PurchasesHeader purchasesHeader = default(PurchasesHeader);
		purchasesHeader.Provider = providerCode;
		purchasesHeader.ScheduledDate = detailLine.ScheduledPurchase;
		purchasesHeader.EstimatedReceptionDate = detailLine.EstimatedReceptionDate;
		purchasesHeader.TargetLevel = targetLevel;
		purchasesHeader.ToNegotiate = detailLine.ToNegotiate;
		PurchasesHeader key = purchasesHeader;
		if (MapOrders.TryGetValue(key, out var value))
		{
			PurchasesDetail purchasesDetail = value.Find((PurchasesDetail r) => r.Reference == detailLine.Reference && r.ColorConfiguration == detailLine.ColorConfiguration && r.WarehouseCode == detailLine.WarehouseCode && r.PackingUnit1 == detailLine.PackingUnit1 && r.PackingUnit2 == detailLine.PackingUnit2 && r.RodLength == detailLine.RodLength && r.SurfaceHeight == detailLine.SurfaceHeight && r.GlassId == detailLine.GlassId && r.MaterialNeedId == detailLine.MaterialNeedId);
			if (purchasesDetail == null)
			{
				value.Add(detailLine);
				return;
			}
			purchasesDetail.Quantity += detailLine.Quantity;
			purchasesDetail.TotalPacking += detailLine.TotalPacking;
			purchasesDetail.TotalKg += detailLine.TotalKg;
			purchasesDetail.TotalToInvoice += detailLine.TotalToInvoice;
		}
		else
		{
			value = new List<PurchasesDetail> { detailLine };
			MapOrders.Add(key, value);
		}
	}

	private bool AddtoExistingPurchasesOrder(SqlConnection conn, SqlTransaction transaction, PurchasesDetail detailLine)
	{
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		if (!FillOpenOrders)
		{
			return false;
		}
		int num = -1;
		int num2 = -1;
		int num3 = 0;
		string text = "";
		if (!OneOrderPerSupplier)
		{
			text = " AND PUR.EstimatedReceptionDate = @estimatedReceptionDate ";
		}
		using (SqlCommand sqlCommand = new SqlCommand("SELECT TOP 1 PUR.Numeration, PUR.Number, MAX(ISNULL(Id, 0)) + 1 FROM dbo.Purchases PUR LEFT OUTER JOIN dbo.PurchasesDetail PD ON PD.Number = PUR.Number AND PD.Numeration = PUR.Numeration INNER JOIN dbo.Compras C ON C.Proveedor = PUR.ProviderCode INNER JOIN dbo.ReferenceSuppliers RS ON RS.SupplierCode = PUR.ProviderCode AND RS.Reference = C.Referencia WHERE C.Referencia = @reference AND PUR.ScheduledSendingDate IS NOT NULL AND ScheduledSendingDate >= CAST(DATEDIFF(DAY, 0, GETDATE()) AS DATETIME) AND PUR.SendingDate IS NULL AND PUR.EstimatedReceptionDate IS NOT NULL " + text + "AND dbo.Schedule_SumDays(ScheduledSendingDate, C.SchedulerTime) <= @estimatedReceptionDate GROUP BY C.ByDefault, PUR.Number, PUR.Numeration, ScheduledSendingDate, DocumentDate ORDER BY C.ByDefault, ScheduledSendingDate DESC, DocumentDate DESC, Number DESC ", conn, transaction))
		{
			SqlParameter value = new SqlParameter("@reference", SqlDbType.NChar, 25)
			{
				Value = detailLine.Reference
			};
			sqlCommand.Parameters.Add(value);
			SqlParameter value2 = new SqlParameter("@estimatedReceptionDate", SqlDbType.Date)
			{
				Value = detailLine.EstimatedReceptionDate
			};
			sqlCommand.Parameters.Add(value2);
			SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
			while (sqlDataReader.Read())
			{
				if (!sqlDataReader.IsDBNull(0))
				{
					num2 = sqlDataReader.GetInt32(0);
				}
				if (!sqlDataReader.IsDBNull(1))
				{
					num = sqlDataReader.GetInt32(1);
				}
				if (!sqlDataReader.IsDBNull(2))
				{
					num3 = sqlDataReader.GetInt32(2);
				}
			}
			sqlDataReader.Close();
		}
		if (num <= 0 || num2 <= 0 || num3 < 0)
		{
			return false;
		}
		if (!InsertPurchasesDetail(conn, transaction, num, num2, num3, detailLine))
		{
			throw new PrefException($"Error inserting purchases detail for document {num}/{num2} and line {num3}");
		}
		return true;
	}

	private void LoadSettings(SqlConnection conn, SqlTransaction transaction)
	{
		LoadDefaultCurrency(conn, transaction);
		LoadShareAmongSuppliers(conn, transaction);
		LoadOneOrderPerSupplier(conn, transaction);
		LoadFillOpenOrders(conn, transaction);
	}

	private void LoadDefaultCurrency(SqlConnection conn, SqlTransaction transaction)
	{
		DefaultCurrency = GetGlobalVariable(conn, transaction, "DivisaDefecto");
	}

	private void LoadShareAmongSuppliers(SqlConnection conn, SqlTransaction transaction)
	{
		ShareAmongSuppliers = Convert.ToInt16(GetGlobalVariable(conn, transaction, "MRP.ShareSuppliers"));
	}

	private void LoadOneOrderPerSupplier(SqlConnection conn, SqlTransaction transaction)
	{
		OneOrderPerSupplier = GetGlobalVariable(conn, transaction, "MRP.OneOrderPerSupplier") == "1";
	}

	private void LoadFillOpenOrders(SqlConnection conn, SqlTransaction transaction)
	{
		FillOpenOrders = GetGlobalVariable(conn, transaction, "MRP.FillOpenOrders") == "1";
	}

	private string GetGlobalVariable(SqlConnection conn, SqlTransaction transaction, string variableName)
	{
		SqlCommand obj = new SqlCommand
		{
			CommandText = "SELECT Valor FROM dbo.VariablesGlobales WHERE Empresa = 1 AND Nombre = @variable",
			Connection = conn,
			Transaction = transaction
		};
		SqlParameter value = new SqlParameter("@variable", SqlDbType.NChar, 25)
		{
			Value = variableName
		};
		obj.Parameters.Add(value);
		object obj2 = obj.ExecuteScalar();
		if (obj2 != null && !(obj2 is DBNull))
		{
			return obj2.ToString();
		}
		return "";
	}

	private void LoadMRPReferencesData(SqlConnection conn, SqlTransaction transaction)
	{
		MapReferenceData.Clear();
		SqlDataReader sqlDataReader = new SqlCommand
		{
			CommandText = "SELECT RTRIM(M.Referencia), ISNULL(RTRIM(MB.TipoCalculo), N''), ISNULL(RTRIM(MB.Descripcion), N''), TargetLevel, ICC.Code, ISNULL(M.Color, N''), M.ReferenciaBase FROM dbo.Materiales M INNER JOIN dbo.MaterialesBase MB ON MB.ReferenciaBase = M.ReferenciaBase LEFT OUTER JOIN Intrastat.CommodityCodes ICC ON ICC.Id = MB.CommodityCode WHERE M.MRPControl = 1 AND ISNULL(M.MaterialSupplierCode, MB.CodigoProveedor) IS NOT NULL ",
			Connection = conn,
			Transaction = transaction
		}.ExecuteReader();
		while (sqlDataReader.Read())
		{
			string @string = sqlDataReader.GetString(0);
			ReferenceData referenceData = new ReferenceData
			{
				CalculationType = sqlDataReader.GetString(1),
				Description = sqlDataReader.GetString(2),
				TargetLevel = sqlDataReader.GetInt32(3),
				Color = sqlDataReader.GetString(5),
				BaseReference = sqlDataReader.GetString(6)
			};
			if (!sqlDataReader.IsDBNull(4))
			{
				referenceData.CommodityCode = sqlDataReader.GetInt32(4);
			}
			MapReferenceData.Add(@string, referenceData);
		}
		sqlDataReader.Close();
	}

	private bool CreatePurchaseDocs(SqlConnection conn, SqlTransaction transaction)
	{
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		MapOrderKey.Clear();
		if (OneOrderPerSupplier)
		{
			TransformMapOrders();
		}
		else
		{
			MapProviderOrders = MapOrders;
		}
		try
		{
			foreach (KeyValuePair<PurchasesHeader, List<PurchasesDetail>> mapProviderOrder in MapProviderOrders)
			{
				DateTime scheduledDate = mapProviderOrder.Key.ScheduledDate;
				if (scheduledDate < DateTime.Today)
				{
					continue;
				}
				bool flag = false;
				int num = 0;
				foreach (PurchasesDetail item in mapProviderOrder.Value)
				{
					if (!FillOpenOrders || !AddtoExistingPurchasesOrder(conn, transaction, item))
					{
						if (!InsertPurchasesDetail(conn, transaction, Number, Numeration, num, item))
						{
							throw new PrefException($"Error inserting purchases detail for document {Number}/{Numeration} and line {num}");
						}
						flag = true;
						num++;
					}
				}
				if (flag)
				{
					SqlCommand sqlCommand = new SqlCommand();
					sqlCommand.Connection = conn;
					sqlCommand.Transaction = transaction;
					sqlCommand.CommandType = CommandType.StoredProcedure;
					sqlCommand.CommandText = "[dbo].[pa_Purchases_NewPurchaseDocument]";
					sqlCommand.Parameters.Add("RETURN_VALUE", SqlDbType.Int);
					sqlCommand.Parameters["RETURN_VALUE"].Direction = ParameterDirection.ReturnValue;
					sqlCommand.Parameters.Add(new SqlParameter("@nNumber", SqlDbType.Int, 0, ParameterDirection.Input, isNullable: false, 0, 0, "", DataRowVersion.Default, Number));
					sqlCommand.Parameters.Add(new SqlParameter("@nNumeration", SqlDbType.Int, 0, ParameterDirection.Input, isNullable: false, 0, 0, "", DataRowVersion.Default, Numeration));
					sqlCommand.Parameters.Add(new SqlParameter("@nProviderCode", SqlDbType.Int, 0, ParameterDirection.Input, isNullable: false, 0, 0, "", DataRowVersion.Default, mapProviderOrder.Key.Provider));
					sqlCommand.Parameters.Add(new SqlParameter("@nKind", SqlDbType.SmallInt, 0, ParameterDirection.Input, isNullable: false, 0, 0, "", DataRowVersion.Default, 1));
					sqlCommand.Parameters.Add(new SqlParameter("@strCurrency", SqlDbType.NVarChar, 25, ParameterDirection.Input, isNullable: false, 0, 0, "", DataRowVersion.Default, DefaultCurrency));
					sqlCommand.Parameters.Add(new SqlParameter("@nWorkerCode", SqlDbType.Int, 0, ParameterDirection.Input, isNullable: false, 0, 0, "", DataRowVersion.Default, UserCode));
					sqlCommand.ExecuteNonQuery();
					object value = sqlCommand.Parameters["RETURN_VALUE"].Value;
					if (!(value is int) || (int)value != 0)
					{
						return false;
					}
					if (!UpdatePurchasesDocDates(conn, transaction, scheduledDate, mapProviderOrder.Key.EstimatedReceptionDate, mapProviderOrder.Key.ToNegotiate == 1))
					{
						return false;
					}
					PurchasesDocKey purchasesDocKey = default(PurchasesDocKey);
					purchasesDocKey.Number = Number;
					purchasesDocKey.Numeration = Numeration;
					PurchasesDocKey value2 = purchasesDocKey;
					MapOrderKey.Add(mapProviderOrder.Key, value2);
					UpdateDocumentHeaderData(conn, transaction, Number, Numeration);
					Number++;
				}
			}
		}
		catch (Exception ex)
		{
			ErrorMessage = PrefException.GetFormattedMessage(ex);
			return false;
		}
		return true;
	}

	private void TransformMapOrders()
	{
		foreach (int providerCode in MapOrders.Select((KeyValuePair<PurchasesHeader, List<PurchasesDetail>> t) => t.Key.Provider).Distinct())
		{
			List<PurchasesDetail> list = new List<PurchasesDetail>();
			int code = providerCode;
			foreach (KeyValuePair<PurchasesHeader, List<PurchasesDetail>> item in MapOrders.Where((KeyValuePair<PurchasesHeader, List<PurchasesDetail>> order) => order.Key.Provider == code))
			{
				list.AddRange(item.Value);
			}
			DateTime scheduledDate = MapOrders.Where((KeyValuePair<PurchasesHeader, List<PurchasesDetail>> d) => d.Key.Provider == providerCode).Min((KeyValuePair<PurchasesHeader, List<PurchasesDetail>> sd) => sd.Key.ScheduledDate);
			DateTime estimatedReceptionDate = MapOrders.Where((KeyValuePair<PurchasesHeader, List<PurchasesDetail>> d) => d.Key.Provider == providerCode).Min((KeyValuePair<PurchasesHeader, List<PurchasesDetail>> sd) => sd.Key.EstimatedReceptionDate);
			short toNegotiate = MapOrders.Where((KeyValuePair<PurchasesHeader, List<PurchasesDetail>> d) => d.Key.Provider == providerCode).Max((KeyValuePair<PurchasesHeader, List<PurchasesDetail>> sd) => sd.Key.ToNegotiate);
			PurchasesHeader purchasesHeader = default(PurchasesHeader);
			purchasesHeader.EstimatedReceptionDate = estimatedReceptionDate;
			purchasesHeader.ScheduledDate = scheduledDate;
			purchasesHeader.Provider = providerCode;
			purchasesHeader.TargetLevel = 1;
			purchasesHeader.ToNegotiate = toNegotiate;
			PurchasesHeader key = purchasesHeader;
			MapProviderOrders.Add(key, list);
		}
	}

	private bool InsertPurchasesDetail(SqlConnection conn, SqlTransaction transaction, int number, int numeration, int nId, PurchasesDetail detailLine)
	{
		//IL_0561: Unknown result type (might be due to invalid IL or missing references)
		SqlCommand sqlCommand = new SqlCommand
		{
			CommandText = "INSERT INTO dbo.PurchasesDetail ([Number], [Numeration], [Id], [Reference], [ColorConfiguration], [ProviderReference], [Description], [ReferenceChange], [ColorChangeConfiguration], [ExitWarehouse], [Quantity], [PackingUnit1], [PackingUnit2], [RO_TotalPacking], [ItemKg], [RO_TotalKg], [Length], [Height], [Volume], [LengthToInvoice], [HeightToInvoice], [MinimumToInvoice], [RO_TotalToInvoice], [Confirmed], [Warehouse], [UnitAmount], [RO_Amount], [FreightPrice], [IdPos], [Parent0IdPos], [Parent1IdPos], [Parent2IdPos], [PriceUpdated], [Type], [CommodityCode], [EstimatedReceptionDate]) VALUES (@nNumber, @nNumeration, @nId, @strReference, @nColorConfiguration, @supplierReference, @strDescription, NULL, 0, 0, @dblQuantity, @nPackingUnit1, @dblPackingUnit2, @dblTotalPacking, @dblItemKg, @dblTotalKg, @dblLength, @dblHeight, 0.0, @dblLengthToInvoice, @dblHeightToInvoice, @dblMinimumToInvoice, @dblTotalToInvoice, 0, @nWarehouse, @dblUnitPrice, @dblUnitPrice * @dblTotalPacking, 0.0, NEWID(), NULL, NULL, NULL, 0, N'MAT', @commodityCode, @estimatedReceptionDate) "
		};
		SqlParameter value = new SqlParameter
		{
			ParameterName = "@nNumber",
			SqlDbType = SqlDbType.Int,
			Value = number
		};
		sqlCommand.Parameters.Add(value);
		SqlParameter value2 = new SqlParameter
		{
			ParameterName = "@nNumeration",
			SqlDbType = SqlDbType.Int,
			Value = numeration
		};
		sqlCommand.Parameters.Add(value2);
		SqlParameter value3 = new SqlParameter
		{
			ParameterName = "@nId",
			SqlDbType = SqlDbType.Int,
			Value = nId
		};
		sqlCommand.Parameters.Add(value3);
		SqlParameter value4 = new SqlParameter
		{
			ParameterName = "@strReference",
			SqlDbType = SqlDbType.NChar,
			Value = detailLine.Reference
		};
		sqlCommand.Parameters.Add(value4);
		SqlParameter value5 = new SqlParameter
		{
			ParameterName = "@nColorConfiguration",
			SqlDbType = SqlDbType.NChar,
			Value = detailLine.ColorConfiguration
		};
		sqlCommand.Parameters.Add(value5);
		SqlParameter value6 = new SqlParameter
		{
			ParameterName = "@supplierReference",
			SqlDbType = SqlDbType.NChar,
			Value = detailLine.SupplierReference
		};
		sqlCommand.Parameters.Add(value6);
		if (!MapReferenceData.TryGetValue(detailLine.Reference, out var value7))
		{
			return false;
		}
		SqlParameter value8 = new SqlParameter
		{
			ParameterName = "@strDescription",
			SqlDbType = SqlDbType.NVarChar,
			Value = value7.Description
		};
		sqlCommand.Parameters.Add(value8);
		SqlParameter value9 = new SqlParameter
		{
			ParameterName = "@dblQuantity",
			SqlDbType = SqlDbType.Float,
			Value = detailLine.Quantity
		};
		sqlCommand.Parameters.Add(value9);
		SqlParameter value10 = new SqlParameter
		{
			ParameterName = "@nPackingUnit1",
			SqlDbType = SqlDbType.Int,
			Value = detailLine.PackingUnit1
		};
		sqlCommand.Parameters.Add(value10);
		SqlParameter value11 = new SqlParameter
		{
			ParameterName = "@dblPackingUnit2",
			SqlDbType = SqlDbType.Float,
			Value = detailLine.PackingUnit2
		};
		sqlCommand.Parameters.Add(value11);
		SqlParameter value12 = new SqlParameter
		{
			ParameterName = "@dblTotalPacking",
			SqlDbType = SqlDbType.Float,
			Value = detailLine.TotalPacking
		};
		sqlCommand.Parameters.Add(value12);
		SqlParameter value13 = new SqlParameter
		{
			ParameterName = "@dblItemKg",
			SqlDbType = SqlDbType.Float,
			Value = detailLine.ItemKg
		};
		sqlCommand.Parameters.Add(value13);
		SqlParameter value14 = new SqlParameter
		{
			ParameterName = "@dblTotalKg",
			SqlDbType = SqlDbType.Float,
			Value = detailLine.TotalKg
		};
		sqlCommand.Parameters.Add(value14);
		SqlParameter value15 = new SqlParameter
		{
			ParameterName = "@dblLength",
			SqlDbType = SqlDbType.Float,
			Value = detailLine.RodLength
		};
		sqlCommand.Parameters.Add(value15);
		SqlParameter value16 = new SqlParameter
		{
			ParameterName = "@dblHeight",
			SqlDbType = SqlDbType.Float,
			Value = detailLine.SurfaceHeight
		};
		sqlCommand.Parameters.Add(value16);
		SqlParameter value17 = new SqlParameter
		{
			ParameterName = "@dblLengthToInvoice",
			SqlDbType = SqlDbType.Float,
			Value = detailLine.LengthToInvoice
		};
		sqlCommand.Parameters.Add(value17);
		SqlParameter value18 = new SqlParameter
		{
			ParameterName = "@dblHeightToInvoice",
			SqlDbType = SqlDbType.Float,
			Value = detailLine.HeightToInvoice
		};
		sqlCommand.Parameters.Add(value18);
		SqlParameter value19 = new SqlParameter
		{
			ParameterName = "@dblMinimumToInvoice",
			SqlDbType = SqlDbType.Float,
			Value = detailLine.MinimumToInvoice
		};
		sqlCommand.Parameters.Add(value19);
		SqlParameter value20 = new SqlParameter
		{
			ParameterName = "@dblTotalToInvoice",
			SqlDbType = SqlDbType.Float,
			Value = detailLine.TotalToInvoice
		};
		sqlCommand.Parameters.Add(value20);
		SqlParameter value21 = new SqlParameter
		{
			ParameterName = "@nWarehouse",
			SqlDbType = SqlDbType.Int,
			Value = detailLine.WarehouseCode
		};
		sqlCommand.Parameters.Add(value21);
		SqlParameter value22 = new SqlParameter
		{
			ParameterName = "@dblUnitPrice",
			SqlDbType = SqlDbType.Float,
			Value = detailLine.PurchasePrice
		};
		sqlCommand.Parameters.Add(value22);
		SqlParameter value23 = new SqlParameter
		{
			ParameterName = "@commodityCode",
			SqlDbType = SqlDbType.Int,
			Value = (((SqlInt32?)value7.CommodityCode) ?? SqlInt32.Null)
		};
		sqlCommand.Parameters.Add(value23);
		SqlParameter value24 = new SqlParameter
		{
			ParameterName = "@estimatedReceptionDate",
			SqlDbType = SqlDbType.Date,
			Value = detailLine.EstimatedReceptionDate
		};
		sqlCommand.Parameters.Add(value24);
		sqlCommand.Connection = conn;
		sqlCommand.Transaction = transaction;
		sqlCommand.ExecuteNonQuery();
		PurchasesCore.FillGlassData(new ConnectionString(ConnectionString).get_Ado(), detailLine.MaterialNeedId.ToString(), detailLine.GlassId, detailLine.RodLength, detailLine.SurfaceHeight, number, numeration, nId);
		if (!string.IsNullOrWhiteSpace(detailLine.GlassId))
		{
			FillPurchasesSubDetail(number, numeration, nId, detailLine, conn, transaction);
		}
		return true;
	}

	private void FillPurchasesSubDetail(int number, int numeration, int nId, PurchasesDetail detailLine, SqlConnection conn, SqlTransaction transaction)
	{
		SqlCommand sqlCommand = new SqlCommand();
		sqlCommand.CommandText = "INSERT INTO dbo.PurchasesSubDetail ([Number], [Numeration], [LineId], [SubId], [Quantity], [MaterialNeedId], [RelationId], [ForNumber], [ForVersion], [ForProductionSet]) SELECT @nNumber, @nNumeration, @nId, 0, @quantity, @materialNeedId, NEWID(), MN.Number, MN.Version, MN.ProductionSet FROM dbo.MaterialNeeds MN WHERE [GUID] = @materialNeedId";
		SqlParameter value = new SqlParameter
		{
			ParameterName = "@nNumber",
			SqlDbType = SqlDbType.Int,
			Value = number
		};
		sqlCommand.Parameters.Add(value);
		SqlParameter value2 = new SqlParameter
		{
			ParameterName = "@nNumeration",
			SqlDbType = SqlDbType.Int,
			Value = numeration
		};
		sqlCommand.Parameters.Add(value2);
		SqlParameter value3 = new SqlParameter
		{
			ParameterName = "@nId",
			SqlDbType = SqlDbType.Int,
			Value = nId
		};
		sqlCommand.Parameters.Add(value3);
		SqlParameter value4 = new SqlParameter
		{
			ParameterName = "@quantity",
			SqlDbType = SqlDbType.Float,
			Value = detailLine.Quantity
		};
		sqlCommand.Parameters.Add(value4);
		SqlParameter value5 = new SqlParameter
		{
			ParameterName = "@materialNeedId",
			SqlDbType = SqlDbType.UniqueIdentifier,
			Value = detailLine.MaterialNeedId
		};
		sqlCommand.Parameters.Add(value5);
		sqlCommand.Connection = conn;
		sqlCommand.Transaction = transaction;
		sqlCommand.ExecuteNonQuery();
	}

	private bool UpdatePurchasesDocDates(SqlConnection conn, SqlTransaction transaction, DateTime tScheduledSendingDate, DateTime tScheduledDeliveryDate, bool toNegotiate)
	{
		SqlCommand sqlCommand = new SqlCommand();
		sqlCommand.CommandText = "UPDATE dbo.Purchases SET ScheduledSendingDate = @sendingDate, EstimatedReceptionDate = @receptionDate, ConfirmationDate = @sendingDate, ToNegotiate = @toNegotiate, RO_FullyConfirmed = 0 WHERE Number = @nNumber AND Numeration = @nNumeration";
		SqlParameter value = new SqlParameter
		{
			ParameterName = "@sendingDate",
			SqlDbType = SqlDbType.Date,
			Value = tScheduledSendingDate
		};
		sqlCommand.Parameters.Add(value);
		SqlParameter value2 = new SqlParameter
		{
			ParameterName = "@receptionDate",
			SqlDbType = SqlDbType.Date,
			Value = tScheduledDeliveryDate
		};
		sqlCommand.Parameters.Add(value2);
		SqlParameter value3 = new SqlParameter
		{
			ParameterName = "@toNegotiate",
			SqlDbType = SqlDbType.SmallInt,
			Value = (toNegotiate ? 1 : 0)
		};
		sqlCommand.Parameters.Add(value3);
		SqlParameter value4 = new SqlParameter
		{
			ParameterName = "@nNumber",
			SqlDbType = SqlDbType.Int,
			Value = Number
		};
		sqlCommand.Parameters.Add(value4);
		SqlParameter value5 = new SqlParameter
		{
			ParameterName = "@nNumeration",
			SqlDbType = SqlDbType.Int,
			Value = Numeration
		};
		sqlCommand.Parameters.Add(value5);
		sqlCommand.Connection = conn;
		sqlCommand.Transaction = transaction;
		sqlCommand.ExecuteNonQuery();
		return true;
	}

	private bool CreateDocumentRelationships(SqlConnection conn, SqlTransaction transaction)
	{
		try
		{
			foreach (KeyValuePair<PurchasesHeader, List<PurchasesDetail>> mapProviderOrder in MapProviderOrders)
			{
				if (!MapOrderKey.TryGetValue(mapProviderOrder.Key, out var value))
				{
					continue;
				}
				foreach (PurchasesDetail item in mapProviderOrder.Value)
				{
					SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
					try
					{
						if (item.MaterialNeedId == Guid.Empty)
						{
							sqlDataAdapter.SelectCommand = new SqlCommand("SELECT DISTINCT PAF.Numero, PAF.Version, PAF.RowId FROM dbo.MaterialNeeds MN INNER JOIN dbo.MaterialNeedsMaster MNM ON MNM.Number = MN.Number AND MNM.Version = MN.Version AND MNM.ProductionSet = MN.ProductionSet AND MNM.MNSet = MN.MNSet INNER JOIN dbo.PAF ON PAF.Numero = MN.Number AND PAF.Version = MN.Version INNER JOIN dbo.PAFUserDates PUS ON PUS.Number = PAF.Numero AND PUS.Version = PAF.Version AND PUS.TaskId = 10 INNER JOIN dbo.Materiales M ON M.Referencia = MN.Reference AND M.MRPControl = 1 WHERE MN.Reference = @reference AND MN.ColorConfiguration = @colorConfiguration AND MN.Length = @rodLength AND MN.Height = @surfaceHeight AND PUS.TaskDate = @controlDate AND MN.WarehouseCode = @warehouseCode AND MN.ProviderCode = @providerCode AND MN.TargetLevel = @targetLevel AND PAF.FechaEntradaTaller IS NULL AND MNM.Obsolete = 0 ", conn, transaction);
							SqlParameter value2 = new SqlParameter
							{
								ParameterName = "@reference",
								SqlDbType = SqlDbType.NChar,
								Value = item.Reference
							};
							sqlDataAdapter.SelectCommand.Parameters.Add(value2);
							SqlParameter value3 = new SqlParameter
							{
								ParameterName = "@colorConfiguration",
								SqlDbType = SqlDbType.Int,
								Value = item.ColorConfiguration
							};
							sqlDataAdapter.SelectCommand.Parameters.Add(value3);
							SqlParameter value4 = new SqlParameter
							{
								ParameterName = "@rodLength",
								SqlDbType = SqlDbType.Float,
								Value = item.RodLength
							};
							sqlDataAdapter.SelectCommand.Parameters.Add(value4);
							SqlParameter value5 = new SqlParameter
							{
								ParameterName = "@surfaceHeight",
								SqlDbType = SqlDbType.Float,
								Value = item.SurfaceHeight
							};
							sqlDataAdapter.SelectCommand.Parameters.Add(value5);
							SqlParameter value6 = new SqlParameter
							{
								ParameterName = "@controlDate",
								SqlDbType = SqlDbType.Date,
								Value = item.ConsumptionDate
							};
							sqlDataAdapter.SelectCommand.Parameters.Add(value6);
							SqlParameter value7 = new SqlParameter
							{
								ParameterName = "@warehouseCode",
								SqlDbType = SqlDbType.Int,
								Value = item.WarehouseCode
							};
							sqlDataAdapter.SelectCommand.Parameters.Add(value7);
							SqlParameter value8 = new SqlParameter
							{
								ParameterName = "@providerCode",
								SqlDbType = SqlDbType.Int,
								Value = mapProviderOrder.Key.Provider
							};
							sqlDataAdapter.SelectCommand.Parameters.Add(value8);
							SqlParameter value9 = new SqlParameter
							{
								ParameterName = "@targetLevel",
								SqlDbType = SqlDbType.Int,
								Value = mapProviderOrder.Key.TargetLevel
							};
							sqlDataAdapter.SelectCommand.Parameters.Add(value9);
						}
						else
						{
							sqlDataAdapter.SelectCommand = new SqlCommand("SELECT PAF.Numero, PAF.Version, PAF.RowId FROM dbo.MaterialNeeds MN INNER JOIN dbo.PAF ON PAF.Numero = MN.Number AND PAF.Version = MN.Version WHERE MN.GUID = @materialNeedId", conn, transaction);
							SqlParameter value10 = new SqlParameter
							{
								ParameterName = "@materialNeedId",
								SqlDbType = SqlDbType.UniqueIdentifier,
								Value = item.MaterialNeedId
							};
							sqlDataAdapter.SelectCommand.Parameters.Add(value10);
						}
						DataTable dataTable = new DataTable();
						try
						{
							sqlDataAdapter.Fill(dataTable);
							foreach (DataRow row in dataTable.Rows)
							{
								Guid rowId = new Guid(Convert.ToString(row.ItemArray.GetValue(2)));
								Guid purchasesDocId = Guid.Empty;
								GetPurchasesDocId(conn, transaction, ref value, ref purchasesDocId);
								CreateRelationship(conn, transaction, rowId, purchasesDocId);
							}
						}
						finally
						{
							((IDisposable)dataTable)?.Dispose();
						}
					}
					finally
					{
						((IDisposable)(object)sqlDataAdapter)?.Dispose();
					}
				}
			}
		}
		catch (Exception ex)
		{
			ErrorMessage = PrefException.GetFormattedMessage(ex);
			return false;
		}
		return true;
	}

	private static void CreateRelationship(SqlConnection conn, SqlTransaction transaction, Guid rowId, Guid purchasesDocId)
	{
		using SqlCommand sqlCommand = new SqlCommand();
		sqlCommand.CommandText = "IF NOT EXISTS (SELECT * FROM dbo.DocumentRelationships WHERE SrcDocumentId = @rowId AND SrcDocumentType = 1 AND DestDocumentId = @purchasesDocId AND DestDocumentType = 4 AND RelationshipType = 1) INSERT INTO dbo.DocumentRelationships (SrcDocumentId, SrcDocumentType, DestDocumentId, DestDocumentType, RelationshipType) VALUES (@rowId, 1, @purchasesDocId, 4, 1)";
		sqlCommand.Parameters.Add(new SqlParameter("@rowId", rowId));
		sqlCommand.Parameters.Add(new SqlParameter("@purchasesDocId", purchasesDocId));
		sqlCommand.Connection = conn;
		sqlCommand.Transaction = transaction;
		sqlCommand.ExecuteNonQuery();
	}

	private static void GetPurchasesDocId(SqlConnection conn, SqlTransaction transaction, ref PurchasesDocKey pdKey, ref Guid purchasesDocId)
	{
		using SqlCommand sqlCommand = new SqlCommand("SELECT DocumentId FROM dbo.Purchases WHERE Number = @number AND Numeration = @numeration", conn, transaction);
		sqlCommand.Parameters.Add(new SqlParameter("@number", pdKey.Number));
		sqlCommand.Parameters.Add(new SqlParameter("@numeration", pdKey.Numeration));
		object obj = sqlCommand.ExecuteScalar();
		if (obj != null && !(obj is DBNull))
		{
			purchasesDocId = new Guid(obj.ToString());
		}
	}
}
