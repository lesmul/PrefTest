using System;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using Preference.Exceptions;
using Preference.Logikal.Api;
using Preference.Logikal.Properties;
using Preference.Sql;
using zlib;

namespace Preference.Logikal;

public class GeneratingProductionFiles
{
	private int _nProductionLot;

	private static int nIdd;

	private bool bLogFilesActivated;

	public CultureInfo Culture { get; set; }

	public string ConnectionString { get; set; }

	public int ProductionLot
	{
		get
		{
			return _nProductionLot;
		}
		set
		{
			_nProductionLot = value;
		}
	}

	public void GenerateCNCFiles()
	{
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Expected O, but got Unknown
		if (_nProductionLot == -1)
		{
			MessageBox.Show("Production lot number incorrect", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			return;
		}
		int num = 0;
		try
		{
			LogikalModule.EnsureLoggedIn();
			LogikalModule.LanguageId = Culture.LCID;
			num = LogikalCNCModule.CNCBegin(0);
			string projectBlobFile = GetProjectBlobFile();
			LogikalCNCModule.CNCSetObjData(num, projectBlobFile);
			string text = "";
			using (OleDbConnection oleDbConnection = new OleDbConnection(ConnectionString))
			{
				oleDbConnection.Open();
				string strXml = "";
				string text2 = "";
				string text3 = "";
				string text4 = "";
				string text5 = "";
				using OleDbCommand oleDbCommand = new OleDbCommand("SELECT pgw.Number as NumeroPAF, pgw.Version, pgw.Position, COUNT(*) AS Cantidad, CP.Nomenclatura AS Nomenclatura, CP.Descripcion AS Descripcion FROM ProdCAMWindows pcw INNER JOIN ProdGenericWindows pgw ON pcw.ProductionLot = pgw.ProductionLot AND pcw.GenericWindowId = pgw.RowId INNER JOIN ContenidoPAF CP ON CP.Numero=pgw.Number and CP.Version=pgw.Version and CP.Orden=pgw.Position WHERE CP.TIPO= N'CUS' and pcw.ProductionLot = " + _nProductionLot + "GROUP BY pgw.Number, pgw.Version, pgw.Position, CP.Nomenclatura, CP.Descripcion", oleDbConnection);
				OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
				if (oleDbDataReader.HasRows)
				{
					while (oleDbDataReader.Read())
					{
						int nAnz = Convert.ToInt16(oleDbDataReader["Cantidad"].ToString(), CultureInfo.InvariantCulture);
						text2 = oleDbDataReader["Nomenclatura"].ToString();
						text3 = oleDbDataReader["Descripcion"].ToString();
						text = oleDbDataReader["NumeroPAF"].ToString();
						text4 = oleDbDataReader["Version"].ToString();
						text5 = oleDbDataReader["Position"].ToString();
						ConnectionString val = new ConnectionString(ConnectionString);
						CPrefZipNET.UnzipXmlFromDB("SELECT Buffer FROM ContenidoPAFBlob WHERE Numero = " + text + " AND Version = " + text4 + " AND Orden = " + text5, val.get_AdoNet(), ref strXml);
						PrefItem prefItem = new PrefItem();
						prefItem.LoadFromSerializationXml(strXml);
						byte[] blob = prefItem.Blob;
						LogikalCNCModule.CNCAddPos(num, blob, blob.Length, text2, text3, nIdd++, nAnz);
					}
				}
			}
			LogikalCNCModule.CNCSetJobNumber(num, text, _nProductionLot.ToString(CultureInfo.InvariantCulture));
			LogikalCNCModule.CNCExecute(num, 1);
			LogikalCNCModule.MDBGetObjectDataF(num, projectBlobFile);
			SaveProjectBlobInPrefSuiteDataBase(projectBlobFile);
			LogikalCNCModule.CNCEnd(num);
			if (File.Exists(projectBlobFile))
			{
				File.Delete(projectBlobFile);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(PrefException.GetFormattedMessage("Error generating CNC files.", ex), "", MessageBoxButton.OK, MessageBoxImage.Hand);
		}
	}

	public void CreateLogikalBarcodesInPrefSuite()
	{
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Expected O, but got Unknown
		if (_nProductionLot == -1)
		{
			MessageBox.Show("Barcodes were not created in PrefSuite. Production lot number incorrect", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			return;
		}
		int num = 0;
		try
		{
			bLogFilesActivated = LogFilesActivated(ConnectionString);
			LogikalModule.EnsureLoggedIn();
			LogikalModule.LanguageId = Culture.LCID;
			num = LogikalCNCModule.CNCBegin(LogikalCNCModule.MDBPOTGetTypeInterface());
			string projectBlobFile = GetProjectBlobFile();
			LogikalCNCModule.CNCSetObjData(num, projectBlobFile);
			LogikalCNCModule.SetParamValue(num, "CHANGE_OPTI_VALUES", "ON");
			LogikalCNCModule.SetParamValue(num, "CUTFACTOR", "");
			string text = "";
			using (OleDbConnection oleDbConnection = new OleDbConnection(ConnectionString))
			{
				oleDbConnection.Open();
				string strXml = "";
				string text2 = "";
				string text3 = "";
				string text4 = "";
				string text5 = "";
				using OleDbCommand oleDbCommand = new OleDbCommand("SELECT pgw.Number as NumeroPAF, pgw.Version, pgw.Position, COUNT(*) AS Cantidad, CP.Nomenclatura AS Nomenclatura, CP.Descripcion AS Descripcion FROM ProdCAMWindows pcw INNER JOIN ProdGenericWindows pgw ON pcw.ProductionLot = pgw.ProductionLot AND pcw.GenericWindowId = pgw.RowId INNER JOIN ContenidoPAF CP ON CP.Numero=pgw.Number and CP.Version=pgw.Version and CP.Orden=pgw.Position WHERE CP.TIPO= N'CUS' and pcw.ProductionLot = " + _nProductionLot + "GROUP BY pgw.Number, pgw.Version, pgw.Position, CP.Nomenclatura, CP.Descripcion", oleDbConnection);
				OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
				if (oleDbDataReader.HasRows)
				{
					while (oleDbDataReader.Read())
					{
						int nAnz = Convert.ToInt16(oleDbDataReader["Cantidad"].ToString(), CultureInfo.InvariantCulture);
						text2 = oleDbDataReader["Nomenclatura"].ToString();
						text3 = oleDbDataReader["Descripcion"].ToString();
						text = oleDbDataReader["NumeroPAF"].ToString();
						text4 = oleDbDataReader["Version"].ToString();
						text5 = oleDbDataReader["Position"].ToString();
						ConnectionString val = new ConnectionString(ConnectionString);
						CPrefZipNET.UnzipXmlFromDB("SELECT Buffer FROM ContenidoPAFBlob WHERE Numero = " + text + " AND Version = " + text4 + " AND Orden = " + text5, val.get_AdoNet(), ref strXml);
						PrefItem prefItem = new PrefItem();
						prefItem.LoadFromSerializationXml(strXml);
						byte[] blob = prefItem.Blob;
						LogikalCNCModule.MDBPOAddPosEx(num, blob, blob.Length, text2, text3, nIdd++, nAnz);
					}
				}
			}
			LogikalCNCModule.CNCSetJobNumber(num, text, _nProductionLot.ToString(CultureInfo.InvariantCulture));
			LogikalCNCModule.MDBExecute(num, 2);
			string text6 = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".mdb");
			LogikalCNCModule.MDBGetReturnF(num, text6);
			LogikalCNCModule.CNCEnd(num);
			if (File.Exists(projectBlobFile))
			{
				File.Delete(projectBlobFile);
			}
			SetBarcodesToProfiles(text6);
			GenerateLogikalXMLProduction(text6);
			if (!bLogFilesActivated && File.Exists(text6))
			{
				File.Delete(text6);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(PrefException.GetFormattedMessage("Error generating production MDB file.", ex), "", MessageBoxButton.OK, MessageBoxImage.Hand);
		}
	}

	public void RecomputePieceList()
	{
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Expected O, but got Unknown
		if (_nProductionLot == -1)
		{
			MessageBox.Show("Production lot number incorrect", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			return;
		}
		int num = 0;
		try
		{
			LogikalModule.EnsureLoggedIn();
			LogikalModule.LanguageId = Culture.LCID;
			num = LogikalCNCModule.CNCBegin(0);
			string projectBlobFile = GetProjectBlobFile();
			LogikalCNCModule.CNCSetObjData(num, projectBlobFile);
			using (OleDbConnection oleDbConnection = new OleDbConnection(ConnectionString))
			{
				oleDbConnection.Open();
				string strXml = "";
				string text = "";
				string text2 = "";
				string text3 = "";
				using OleDbCommand oleDbCommand = new OleDbCommand("SELECT pgw.Number as NumeroPAF, pgw.Version, pgw.Position FROM ProdCAMWindows pcw INNER JOIN ProdGenericWindows pgw ON pcw.ProductionLot = pgw.ProductionLot AND pcw.GenericWindowId = pgw.RowId INNER JOIN ContenidoPAF CP ON CP.Numero=pgw.Number and CP.Version=pgw.Version and CP.Orden=pgw.Position WHERE CP.TIPO= N'CUS' and pcw.ProductionLot = " + _nProductionLot + "GROUP BY pgw.Number, pgw.Version, pgw.Position, CP.Nomenclatura, CP.Descripcion", oleDbConnection);
				OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
				if (oleDbDataReader.HasRows)
				{
					while (oleDbDataReader.Read())
					{
						text3 = oleDbDataReader["NumeroPAF"].ToString();
						text = oleDbDataReader["Version"].ToString();
						text2 = oleDbDataReader["Position"].ToString();
						ConnectionString val = new ConnectionString(ConnectionString);
						CPrefZipNET.UnzipXmlFromDB("SELECT Buffer FROM ContenidoPAFBlob WHERE Numero = " + text3 + " AND Version = " + text + " AND Orden = " + text2, val.get_AdoNet(), ref strXml);
						PrefItem prefItem = new PrefItem();
						prefItem.LoadFromSerializationXml(strXml);
						byte[] blob = prefItem.Blob;
						string text4 = Path.GetTempPath() + "ElevationBlob.blb";
						using (FileStream fileStream = new FileStream(text4, FileMode.Create, FileAccess.Write))
						{
							fileStream.Write(blob, 0, blob.Length);
						}
						LogikalCNCModule.IOESetPositionF(num, text4);
						LogikalCNCModule.IOEExecute(num, InputOfElementsExecuteMode.NewPieceLists);
						if (LogikalSettingsModule.ProjectBlobHasChanged(num))
						{
							LogikalSettingsModule.SettingsGetObjectDataF(num, projectBlobFile);
							SaveProjectBlobInPrefSuiteDataBase(projectBlobFile);
						}
						LogikalCNCModule.IOEDone(num);
					}
				}
			}
			if (File.Exists(projectBlobFile))
			{
				File.Delete(projectBlobFile);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(PrefException.GetFormattedMessage("Error recomputing PieceList.", ex), "", MessageBoxButton.OK, MessageBoxImage.Hand);
		}
	}

	private void SetBarcodesToProfiles(string strProductionMDBPath)
	{
		try
		{
			using OleDbConnection oleDbConnection = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + strProductionMDBPath);
			oleDbConnection.Open();
			using DataSet dataSet = new DataSet();
			dataSet.Locale = CultureInfo.InvariantCulture;
			using (OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter("Select Profiles.xGUID AS xGUID, ProfileCuts.Barcode AS Barcode, ProfileCuts.Instance AS Instance FROM(ProfileCuts) INNER JOIN Profiles ON ProfileCuts.LK_ProfileId = Profiles.ProfileID order by Instance", oleDbConnection))
			{
				oleDbDataAdapter.Fill(dataSet, "Barcodes");
			}
			ReplaceGUIDForBarcodesInProdCamPieces(dataSet);
		}
		catch (Exception ex)
		{
			MessageBox.Show(PrefException.GetFormattedMessage("Error assign barcodes to profiles.", ex), "", MessageBoxButton.OK, MessageBoxImage.Hand);
		}
	}

	private string GenerateLogikalXMLProduction(string strProductionMDBPath)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Expected O, but got Unknown
		try
		{
			string xml = "";
			ConnectionString val = new ConnectionString(ConnectionString);
			CPrefZipNET.UnzipXmlFromDB("SELECT ProductionLotXML FROM Optimizaciones WHERE NUMERO =" + _nProductionLot, val.get_AdoNet(), ref xml);
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(xml);
			string optimizationToLogikal = Resources.OptimizationToLogikal;
			XmlDocument xmlDocument2 = new XmlDocument();
			xmlDocument2.LoadXml(optimizationToLogikal);
			XPathNavigator stylesheet = xmlDocument2.CreateNavigator();
			XslCompiledTransform xslCompiledTransform = new XslCompiledTransform();
			xslCompiledTransform.Load(stylesheet, XsltSettings.TrustedXslt, null);
			MemoryStream memoryStream = new MemoryStream();
			XmlWriter results = XmlWriter.Create(memoryStream, xslCompiledTransform.OutputSettings);
			xslCompiledTransform.Transform(xmlDocument.CreateNavigator(), results);
			memoryStream.Position = 0L;
			StreamReader streamReader = new StreamReader(memoryStream);
			string strXMLLogikalProduction = $"{streamReader.ReadToEnd()}";
			memoryStream.Close();
			streamReader.Close();
			return ReplaceRodsColorCodes(strXMLLogikalProduction, strProductionMDBPath);
		}
		catch (Exception ex)
		{
			MessageBox.Show(PrefException.GetFormattedMessage("Error generating LogiKal production xml file.", ex), "", MessageBoxButton.OK, MessageBoxImage.Hand);
			return "";
		}
	}

	private static string ReplaceRodsColorCodes(string strXMLLogikalProduction, string strProductionMDBPath)
	{
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(strXMLLogikalProduction);
		try
		{
			using (OleDbConnection oleDbConnection = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + strProductionMDBPath))
			{
				oleDbConnection.Open();
				using OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter("SELECT [Profiles].[ArticleCode] AS ReferenciaFinal, [Profiles].[Amount] AS Cantidad, [Profiles].[Length_Output] AS Longitud, [Profiles].[CutLeft] AS AnguloA, [Profiles].[CutRight] AS AnguloB, [Profiles].[Color] AS Color, [Profiles].[Weight_Output] AS Peso, InsertionId, AssemblyInfo, AssemblyInfoNo, xSashGUID, xGUID, PieceListType, PieceId,JobId, InnerColorTypeInternal, OuterColorTypeInternal FROM Profiles ORDER BY [Profiles].[ArticleCode]", oleDbConnection);
				using DataTable dataTable = new DataTable();
				dataTable.Locale = CultureInfo.InvariantCulture;
				oleDbDataAdapter.Fill(dataTable);
				int count = dataTable.Rows.Count;
				if (count > 0)
				{
					for (int i = 0; i < count; i++)
					{
						string text = dataTable.Rows[i]["xGUID"].ToString();
						string value = dataTable.Rows[i]["InnerColorTypeInternal"].ToString();
						string value2 = dataTable.Rows[i]["OuterColorTypeInternal"].ToString();
						XmlNode xmlNode = xmlDocument.SelectSingleNode("Optimization/ProfileBar/ProfileCut[@ProfileGUID='" + text + "']/parent::ProfileBar");
						if (xmlNode != null)
						{
							xmlNode.Attributes["InnerColorType"].Value = value;
							xmlNode.Attributes["OuterColorType"].Value = value2;
						}
					}
				}
			}
			return xmlDocument.OuterXml;
		}
		catch (Exception ex)
		{
			MessageBox.Show(PrefException.GetFormattedMessage("Error replacing rods color codes.", ex), "", MessageBoxButton.OK, MessageBoxImage.Hand);
			return strXMLLogikalProduction;
		}
	}

	private void ReplaceGUIDForBarcodesInProdCamPieces(DataSet dbDataSet)
	{
		string text = "";
		string text2 = "";
		string text3 = "";
		foreach (DataRow row in dbDataSet.Tables["Barcodes"].Rows)
		{
			try
			{
				text = row["xGUID"].ToString();
				text2 = row["Barcode"].ToString();
				text3 = row["Instance"].ToString();
				using OleDbConnection oleDbConnection = new OleDbConnection(ConnectionString);
				oleDbConnection.Open();
				using OleDbCommand oleDbCommand = new OleDbCommand("UPDATE ProdCAMPieces SET Parameter = N'" + text2 + "' FROM ProdCAMPieces pcp INNER JOIN ProdCAMSubWindows pcsw ON pcp.SubWindowId=pcsw.RowId WHERE pcp.ProductionLot=N'" + _nProductionLot + "' AND pcsw.Instance=N'" + text3 + "' AND pcp.Parameter=N'" + text + "'", oleDbConnection);
				oleDbCommand.ExecuteNonQuery();
			}
			catch
			{
			}
		}
	}

	private static bool LogFilesActivated(string strConnectionString)
	{
		string text = "0";
		using (OleDbConnection oleDbConnection = new OleDbConnection(strConnectionString))
		{
			oleDbConnection.Open();
			using OleDbCommand oleDbCommand = new OleDbCommand("SELECT Valor FROM VariablesGlobales WHERE Nombre = N'Preference.Logikal.Log'", oleDbConnection);
			OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
			if (oleDbDataReader.HasRows)
			{
				while (oleDbDataReader.Read())
				{
					text = oleDbDataReader[0].ToString();
				}
			}
		}
		if (text == "1")
		{
			return true;
		}
		return false;
	}

	private string GetProjectBlobFile()
	{
		string text = Path.GetTempPath() + "Projectblob.blb";
		try
		{
			using (OleDbConnection oleDbConnection = new OleDbConnection(ConnectionString))
			{
				oleDbConnection.Open();
				using OleDbCommand oleDbCommand = new OleDbCommand(" SELECT DISTINCT ProjectBlob FROM PAFOrg P INNER JOIN EstadoSubModelosPAF ESMP ON ESMP.Numero = P.Number AND ESMP.Version = P.Version WHERE ESMP.ProductionLot = " + _nProductionLot, oleDbConnection);
				object obj = oleDbCommand.ExecuteScalar();
				if (obj != null && obj != DBNull.Value)
				{
					byte[] array = (byte[])oleDbCommand.ExecuteScalar();
					using FileStream fileStream = new FileStream(text, FileMode.Create, FileAccess.Write);
					fileStream.Write(array, 0, array.Length);
				}
			}
			return text;
		}
		catch (Exception ex)
		{
			EventLog.AddError(PrefException.GetFormattedMessage("Could not get project blob file in Production from db", ex));
			return text;
		}
	}

	private void SaveProjectBlobInPrefSuiteDataBase(string strProjectBlobFile)
	{
		try
		{
			using OleDbConnection oleDbConnection = new OleDbConnection(ConnectionString);
			oleDbConnection.Open();
			byte[] value = File.ReadAllBytes(strProjectBlobFile);
			OleDbCommand oleDbCommand = oleDbConnection.CreateCommand();
			oleDbCommand.CommandText = " UPDATE PAFOrg SET ProjectBlob = ? FROM PAFOrg P INNER JOIN EstadoSubModelosPAF ESMP ON ESMP.Numero = P.Number AND ESMP.Version = P.Version WHERE ESMP.ProductionLot = " + _nProductionLot;
			OleDbParameter oleDbParameter = new OleDbParameter();
			oleDbCommand.Parameters.Add(oleDbParameter);
			oleDbParameter.Value = value;
			oleDbCommand.ExecuteNonQuery();
		}
		catch (Exception ex)
		{
			EventLog.AddError(PrefException.GetFormattedMessage("Could not save project blob in db.", ex));
		}
	}
}
