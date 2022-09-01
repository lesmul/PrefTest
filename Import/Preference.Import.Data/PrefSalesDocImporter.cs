using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Xml.Linq;
using Preference.Import.Data.Tables;

namespace Preference.Import.Data;

public class PrefSalesDocImporter
{
	private enum ETypeCommand
	{
		Delete,
		DisableTriggers,
		EnableTriggers,
		Insert,
		DeletePlusInsert
	}

	public int Number { get; set; }

	public int Version { get; set; }

	public ImportType Type { get; set; }

	private static List<BaseTable> Tables { get; set; }

	private static List<BaseTable> LiveTables { get; set; }

	private string DestDatabaseName { get; set; }

	private string DestServerName { get; set; }

	private List<BaseTable> CustomTables { get; set; }

	private string SourceDatabaseName { get; set; }

	private string SourceServerName { get; set; }

	public event ProgressChangedEventHandler ProgressChanged;

	protected virtual void OnProgressChanged(string strMessage, int nPercentage)
	{
		if (this.ProgressChanged != null)
		{
			this.ProgressChanged(this, new ProgressChangedEventArgs(strMessage, nPercentage));
		}
	}

	public void Import(SqlConnection sqlConnSource, SqlConnection sqlConnDestination, int nNumber, int nVersion, ImportType iType, bool live, int commandTimeout)
	{
		SourceServerName = sqlConnSource.DataSource;
		DestServerName = sqlConnDestination.DataSource;
		SourceDatabaseName = sqlConnSource.Database;
		DestDatabaseName = sqlConnDestination.Database;
		Number = nNumber;
		Version = nVersion;
		Type = iType;
		if (Tables == null)
		{
			ReadTables();
		}
		if (live && LiveTables == null)
		{
			ReadLiveTables();
		}
		using PrefSqlTransaction prefSqlTransaction = new PrefSqlTransaction(sqlConnSource, sqlConnDestination, commandTimeout);
		Dictionary<string, string> commandsDeleteRegistryMoveDocumentToDestination = GetCommandsDeleteRegistryMoveDocumentToDestination(nNumber, nVersion);
		if (!live)
		{
			commandsDeleteRegistryMoveDocumentToDestination.AddRange(GetCommands(Tables, ETypeCommand.DisableTriggers));
			commandsDeleteRegistryMoveDocumentToDestination.AddRange(GetCommands(Tables, ETypeCommand.Delete));
		}
		prefSqlTransaction.ProgressChanged += OnImportProgressChanged;
		prefSqlTransaction.Execute(PrefSqlTransaction.ExecuteType.Dest, commandsDeleteRegistryMoveDocumentToDestination);
		commandsDeleteRegistryMoveDocumentToDestination = ((!live) ? GetCommands(Tables, ETypeCommand.Insert) : GetCommands(LiveTables, ETypeCommand.DeletePlusInsert));
		prefSqlTransaction.BulkCopy(commandsDeleteRegistryMoveDocumentToDestination, live);
		if (!live)
		{
			commandsDeleteRegistryMoveDocumentToDestination = GetCommands(Tables, ETypeCommand.EnableTriggers);
			prefSqlTransaction.Execute(PrefSqlTransaction.ExecuteType.Dest, commandsDeleteRegistryMoveDocumentToDestination);
		}
		if (iType == ImportType.Move)
		{
			commandsDeleteRegistryMoveDocumentToDestination.Clear();
			commandsDeleteRegistryMoveDocumentToDestination.AddRange(GetCommandsRegistryMoveDocumentToSource(nNumber, nVersion));
			if (!live)
			{
				commandsDeleteRegistryMoveDocumentToDestination.AddRange(GetCommands(Tables, ETypeCommand.DisableTriggers));
			}
			commandsDeleteRegistryMoveDocumentToDestination.AddRange(GetCommands(Tables, ETypeCommand.Delete));
			if (!live)
			{
				commandsDeleteRegistryMoveDocumentToDestination.AddRange(GetCommands(Tables, ETypeCommand.EnableTriggers));
			}
			prefSqlTransaction.Execute(PrefSqlTransaction.ExecuteType.Source, commandsDeleteRegistryMoveDocumentToDestination);
		}
		prefSqlTransaction.Commit();
	}

	public void AddCustomTables(string customTablesXml)
	{
		XElement xElement;
		try
		{
			xElement = XElement.Parse(customTablesXml);
		}
		catch
		{
			throw new Exception("\n\nThe custom tables xml haven't a valid format");
		}
		IEnumerable<XElement> enumerable = xElement.Elements("Table");
		CustomTables = new List<BaseTable>();
		foreach (XElement item2 in enumerable)
		{
			XAttribute xAttribute = item2.Attribute("Schema");
			XAttribute xAttribute2 = item2.Attribute("Name");
			XElement xElement2 = item2.Element("Select");
			XElement xElement3 = item2.Element("Delete");
			string text = xAttribute?.Value;
			string text2 = xAttribute2?.Value;
			string text3 = xElement2?.Value;
			string text4 = xElement3?.Value;
			if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(text2) || string.IsNullOrEmpty(text3) || string.IsNullOrEmpty(text4))
			{
				string text5 = $"\n\nThe current node for custom table not is completed.\n\nSchema : '{text}'\nName : '{text2}'\nSelect :'{text3}'\nDelete : {text4}\n";
				if (string.IsNullOrEmpty(text))
				{
					text5 += "\nSchema not is defined.";
				}
				if (string.IsNullOrEmpty(text2))
				{
					text5 += "\nName not is defined.";
				}
				if (string.IsNullOrEmpty(text3))
				{
					text5 += "\nSelect not is defined.";
				}
				if (string.IsNullOrEmpty(text4))
				{
					text5 += "\nDelete not is defined.";
				}
				throw new Exception(text5);
			}
			CustomTable item = new CustomTable(text, text2, text3, text4);
			CustomTables.Add(item);
		}
	}

	private void OnImportProgressChanged(object sender, ProgressChangedEventArgs e)
	{
		OnProgressChanged(e.Message, e.Percentage);
	}

	private Dictionary<string, string> GetCommands(IEnumerable<BaseTable> tables, ETypeCommand typeCommand)
	{
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		foreach (BaseTable table in tables)
		{
			switch (typeCommand)
			{
			case ETypeCommand.Insert:
				dictionary.Add(table.FullName, table.GetSelectQuery(Number, Version));
				break;
			case ETypeCommand.Delete:
				dictionary.Add(table.FullName, table.GetDeleteQuery(Number, Version));
				break;
			case ETypeCommand.DeletePlusInsert:
				dictionary.Add("DELETE " + table.FullName, table.GetDeleteQuery(Number, Version));
				dictionary.Add(table.FullName, table.GetSelectQuery(Number, Version));
				break;
			case ETypeCommand.DisableTriggers:
				dictionary.Add("DISABLE TRIGGERS " + table.FullName, table.GetDisableTriggersQuery());
				break;
			case ETypeCommand.EnableTriggers:
				dictionary.Add("ENABLE TRIGGERS " + table.FullName, table.GetEnableTriggersQuery());
				break;
			}
		}
		return dictionary;
	}

	private void ReadTables()
	{
		Tables = new List<BaseTable>();
		if (CustomTables != null)
		{
			Tables.AddRange(CustomTables);
		}
		Tables.Add(new EstadoModelosLocations());
		Tables.Add(new EstadoModelosPAF());
		Tables.Add(new EstadoSubModelosPAF());
		Tables.Add(new ContenidoPAFDescriptions());
		Tables.Add(new ContenidoPAFOnDemand());
		Tables.Add(new ContenidoPAFSubModels());
		Tables.Add(new ContenidoPAFTimestamps());
		Tables.Add(new MaterialesPAF());
		Tables.Add(new MaterialNeeds());
		Tables.Add(new MaterialNeedsMaster());
		Tables.Add(new ManoObraPAF());
		Tables.Add(new OpcionesPAF());
		Tables.Add(new AuditDetail());
		Tables.Add(new ErrorsContent());
		Tables.Add(new PAFAlternatives());
		Tables.Add(new PAFDataVersions());
		Tables.Add(new PAFDeliveryDatesLog());
		Tables.Add(new PAFDescriptions());
		Tables.Add(new PAFDetailAlternativesContent());
		Tables.Add(new PAFDetailModelVariables());
		Tables.Add(new PAFDetailSubModelVariables());
		Tables.Add(new PAFDetailAmountDiscounts());
		Tables.Add(new PAFDetailPartitionClasses());
		Tables.Add(new PAFDetailPriceGroups());
		Tables.Add(new PAFDetailServiceCodes());
		Tables.Add(new PAFDetailTariffs());
		Tables.Add(new PAFMaterialAmounts());
		Tables.Add(new PAFPercentageControl());
		Tables.Add(new PAFSubTotals());
		Tables.Add(new PAFTariffs());
		Tables.Add(new PAFOrg());
		Tables.Add(new PAFCurrencies());
		Tables.Add(new PAFLabeledTariffs());
		Tables.Add(new PAFLanguages());
		Tables.Add(new PAFTimestamps());
		Tables.Add(new PrefItemsItemDescriptiveXmls());
		Tables.Add(new PrefDiscountsPAFDetailAfterDiscounts());
		Tables.Add(new PrefDiscountsPAFAfterDiscounts());
		Tables.Add(new TariffChainAggregations());
		Tables.Add(new TariffsContent());
		Tables.Add(new PAFUserDates());
		Tables.Add(new ExternalFiles());
		Tables.Add(new Diario());
		Tables.Add(new BankDrafts());
		Tables.Add(new DocumentRelationships());
		Tables.Add(new Locks());
		Tables.Add(new PAFLocations());
		Tables.Add(new SalesDocAddWorks());
		Tables.Add(new SalesDocItemAddWorks());
		Tables.Add(new TariffsBase());
		Tables.Add(new TariffSurcharges());
		Tables.Add(new TariffDVTimestamps());
		Tables.Add(new MontajeMaterialAEmplear());
		Tables.Add(new PrefDocumentsPDGenericGaskets());
		Tables.Add(new PrefDocumentsPDGenericGeorgianBars());
		Tables.Add(new PrefDocumentsPDGenericGlassChambers());
		Tables.Add(new PrefDocumentsPDGenericGlassComponents());
		Tables.Add(new PrefDocumentsPDGenericGlasses());
		Tables.Add(new PrefDocumentsPDGenericGlassLites());
		Tables.Add(new PrefDocumentsPDGenericHoles());
		Tables.Add(new PrefDocumentsPDGenericMullions());
		Tables.Add(new PrefDocumentsPDGenericOperations());
		Tables.Add(new PrefDocumentsPDGenericParts());
		Tables.Add(new PrefDocumentsPDGenericPieces());
		Tables.Add(new PrefDocumentsPDGenericRollerBoxBlades());
		Tables.Add(new PrefDocumentsPDGenericRollerBoxes());
		Tables.Add(new PrefDocumentsPDGenericRollerBoxPanels());
		Tables.Add(new PrefDocumentsPDGenericSquares());
		Tables.Add(new PrefDocumentsPDGenericSticks());
		Tables.Add(new PrefDocumentsPDInstanceGaskets());
		Tables.Add(new PrefDocumentsPDInstanceGeorgianBars());
		Tables.Add(new PrefDocumentsPDInstanceGlassComponents());
		Tables.Add(new PrefDocumentsPDInstanceGlasses());
		Tables.Add(new PrefDocumentsPDInstanceHoles());
		Tables.Add(new PrefDocumentsPDInstanceMullions());
		Tables.Add(new PrefDocumentsPDInstancePieces());
		Tables.Add(new PrefDocumentsPDInstanceRollerBoxes());
		Tables.Add(new PrefDocumentsPDInstanceRollerBoxPanels());
		Tables.Add(new PrefDocumentsPDInstanceSquares());
		Tables.Add(new PrefDocumentsPDInstanceSticks());
		Tables.Add(new Addresses());
		Tables.Add(new Audit());
		Tables.Add(new Tariff());
		Tables.Add(new AddressesPAF());
		Tables.Add(new ContenidoPAF());
		Tables.Add(new ContenidoPAFBLOB());
		Tables.Add(new PAF());
	}

	private void ReadLiveTables()
	{
		LiveTables = new List<BaseTable>();
		if (CustomTables != null)
		{
			LiveTables.AddRange(CustomTables);
		}
		LiveTables.Add(new PAF());
		LiveTables.Add(new ContenidoPAFBLOB());
		LiveTables.Add(new ContenidoPAF());
		LiveTables.Add(new Addresses());
		LiveTables.Add(new Audit());
		LiveTables.Add(new Tariff());
		LiveTables.Add(new AddressesPAF());
		LiveTables.Add(new EstadoModelosLocations());
		LiveTables.Add(new EstadoModelosPAF());
		LiveTables.Add(new EstadoSubModelosPAF());
		LiveTables.Add(new ContenidoPAFDescriptions());
		LiveTables.Add(new ContenidoPAFOnDemand());
		LiveTables.Add(new ContenidoPAFSubModels());
		LiveTables.Add(new ContenidoPAFTimestamps());
		LiveTables.Add(new MaterialesPAF());
		LiveTables.Add(new MaterialNeeds());
		LiveTables.Add(new MaterialNeedsMaster());
		LiveTables.Add(new ManoObraPAF());
		LiveTables.Add(new OpcionesPAF());
		LiveTables.Add(new AuditDetail());
		LiveTables.Add(new ErrorsContent());
		LiveTables.Add(new PAFAlternatives());
		LiveTables.Add(new PAFDataVersions());
		LiveTables.Add(new PAFDeliveryDatesLog());
		LiveTables.Add(new PAFDescriptions());
		LiveTables.Add(new PAFDetailAlternativesContent());
		LiveTables.Add(new PAFDetailModelVariables());
		LiveTables.Add(new PAFDetailSubModelVariables());
		LiveTables.Add(new PAFDetailAmountDiscounts());
		LiveTables.Add(new PAFDetailPartitionClasses());
		LiveTables.Add(new PAFDetailPriceGroups());
		LiveTables.Add(new PAFDetailServiceCodes());
		LiveTables.Add(new PAFDetailTariffs());
		LiveTables.Add(new PAFMaterialAmounts());
		LiveTables.Add(new PAFPercentageControl());
		LiveTables.Add(new PAFSubTotals());
		LiveTables.Add(new PAFTariffs());
		LiveTables.Add(new PAFOrg());
		LiveTables.Add(new PAFCurrencies());
		LiveTables.Add(new PAFLabeledTariffs());
		LiveTables.Add(new PAFLanguages());
		LiveTables.Add(new PAFTimestamps());
		LiveTables.Add(new PrefItemsItemDescriptiveXmls());
		LiveTables.Add(new PrefDiscountsPAFDetailAfterDiscounts());
		LiveTables.Add(new PrefDiscountsPAFAfterDiscounts());
		LiveTables.Add(new TariffChainAggregations());
		LiveTables.Add(new TariffsContent());
		LiveTables.Add(new PAFUserDates());
		LiveTables.Add(new ExternalFiles());
		LiveTables.Add(new Diario());
		LiveTables.Add(new BankDrafts());
		LiveTables.Add(new DocumentRelationships());
		LiveTables.Add(new Locks());
		LiveTables.Add(new PAFLocations());
		LiveTables.Add(new SalesDocAddWorks());
		LiveTables.Add(new SalesDocItemAddWorks());
		LiveTables.Add(new TariffsBase());
		LiveTables.Add(new TariffSurcharges());
		LiveTables.Add(new TariffDVTimestamps());
		LiveTables.Add(new MontajeMaterialAEmplear());
		LiveTables.Add(new PrefDocumentsPDGenericGaskets());
		LiveTables.Add(new PrefDocumentsPDGenericGeorgianBars());
		LiveTables.Add(new PrefDocumentsPDGenericGlassChambers());
		LiveTables.Add(new PrefDocumentsPDGenericGlassComponents());
		LiveTables.Add(new PrefDocumentsPDGenericGlasses());
		LiveTables.Add(new PrefDocumentsPDGenericGlassLites());
		LiveTables.Add(new PrefDocumentsPDGenericHoles());
		LiveTables.Add(new PrefDocumentsPDGenericMullions());
		LiveTables.Add(new PrefDocumentsPDGenericOperations());
		LiveTables.Add(new PrefDocumentsPDGenericParts());
		LiveTables.Add(new PrefDocumentsPDGenericPieces());
		LiveTables.Add(new PrefDocumentsPDGenericRollerBoxBlades());
		LiveTables.Add(new PrefDocumentsPDGenericRollerBoxes());
		LiveTables.Add(new PrefDocumentsPDGenericRollerBoxPanels());
		LiveTables.Add(new PrefDocumentsPDGenericSquares());
		LiveTables.Add(new PrefDocumentsPDGenericSticks());
		LiveTables.Add(new PrefDocumentsPDInstanceGaskets());
		LiveTables.Add(new PrefDocumentsPDInstanceGeorgianBars());
		LiveTables.Add(new PrefDocumentsPDInstanceGlassComponents());
		LiveTables.Add(new PrefDocumentsPDInstanceGlasses());
		LiveTables.Add(new PrefDocumentsPDInstanceHoles());
		LiveTables.Add(new PrefDocumentsPDInstanceMullions());
		LiveTables.Add(new PrefDocumentsPDInstancePieces());
		LiveTables.Add(new PrefDocumentsPDInstanceRollerBoxes());
		LiveTables.Add(new PrefDocumentsPDInstanceRollerBoxPanels());
		LiveTables.Add(new PrefDocumentsPDInstanceSquares());
		LiveTables.Add(new PrefDocumentsPDInstanceSticks());
	}

	private IDictionary<string, string> GetCommandsRegistryMoveDocumentToSource(int nNumber, int nVersion)
	{
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		dictionary.Add("PAFArchived Delete", $"DELETE [dbo].[PAFArchived] WHERE [Number] = {nNumber} AND [Version] = {nVersion}");
		dictionary.Add("PAFArchived Insert", $"INSERT INTO [dbo].[PAFArchived] ([Number], [Version], [Server], [Database], [OrderNumber]) SELECT {nNumber}, {nVersion}, N'{DestServerName}', N'{DestDatabaseName}', NumeroPedido FROM dbo.PAF WHERE Numero={nNumber} AND Version={nVersion}");
		return dictionary;
	}

	private Dictionary<string, string> GetCommandsDeleteRegistryMoveDocumentToDestination(int nNumber, int nVersion)
	{
		return new Dictionary<string, string> { 
		{
			"PAFArchived",
			$"DELETE [dbo].[PAFArchived] WHERE [Number] = {nNumber} AND [Version] = {nVersion}"
		} };
	}
}
