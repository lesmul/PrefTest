using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Xml.Linq;
using Preference.Graphics;
using Preference.Graphics.Converters;
using Preference.Graphics.XAML;
using Preference.Wpf.Controls.Core;
using Preference.Wpf.Controls.Properties;
using zlib;

namespace Preference.Wpf.Controls;

public class EMFtoXAMLWindow : Window, IComponentConnector
{
	private enum ConversionType
	{
		EmfToXaml,
		DxFtoXaml
	}

	private const string EmfMaterialsQuery = "SELECT RTRIM(ReferenciaBase) as ReferenciaBase FROM MaterialesBase WHERE Esquema IS NOT NULL";

	private const string EmfModelsQuery = "SELECT RTRIM(Codigo) as Codigo FROM Dibujos WHERE Metafile IS NOT NULL";

	private const string EmfOptionValuesQuery = "SELECT DataVerId, RTRIM(Opcion) as Opcion, Orden, RTRIM(Valor) as Valor FROM ContenidoOpciones WHERE Esquema IS NOT NULL";

	private const string EmfSymbolsQuery = "SELECT RTRIM(Name) as Name FROM Symbols WHERE Metafile IS NOT NULL";

	private const string EmfGlobalVariableQuery = "SELECT RTRIM(Nombre) as Nombre FROM dbo.VariablesGlobales WHERE [Buffer] IS NOT NULL";

	private const string DxfMaterialsQuery = "SELECT RTRIM(ReferenciaBase) as ReferenciaBase FROM MaterialesBase WHERE ISNULL(CONVERT(nvarchar,DXF),N'') <> N''";

	private const string DxfModelsQuery = "SELECT RTRIM(Codigo) as Codigo FROM Dibujos WHERE Buffer IS NOT NULL";

	private const string EmfCountMaterialsQuery = "SELECT COUNT(*) FROM MaterialesBase WHERE Esquema IS NOT NULL";

	private const string EmfCountModelsQuery = "SELECT COUNT(*) FROM Dibujos WHERE Metafile IS NOT NULL";

	private const string EmfCountOptionValuesQuery = "SELECT COUNT(*) FROM ContenidoOpciones WHERE Esquema IS NOT NULL";

	private const string EmfCountSymbolsQuery = "SELECT COUNT(*) FROM Symbols WHERE Metafile IS NOT NULL";

	private const string EmfCountGlobalVariableQuery = "SELECT COUNT(*) FROM dbo.VariablesGlobales WHERE [Buffer] IS NOT NULL";

	private readonly SqlConnection _conn;

	private readonly BackgroundWorker _worker;

	private string _statusText;

	private readonly string _strOledbConnectionString;

	private List<string> _logErrors;

	private bool _bMaterials;

	private bool _bModels;

	private bool _bOptions;

	private bool _bSymbols;

	private bool _bOverwriteAll;

	private readonly XNamespace _xamlNamespace = "http://schemas.microsoft.com/winfx/2006/xaml/presentation";

	private ConversionType _convType;

	internal GroupBox ConversionTypeGroupBox;

	internal RadioButton EMFToXAML_RadioButton;

	internal RadioButton DXFToXAML_RadioButton;

	internal GroupBox ItemsGroupBox;

	internal CheckBox MaterialsCheck;

	internal CheckBox ModelsCheck;

	internal CheckBox OptionsCheck;

	internal CheckBox SymbolsCheck;

	internal CheckBox OverwriteAllCheck;

	internal Button AcceptButton;

	internal Button CancelButton;

	internal CheckBox ProcessCheck;

	internal TextBlock StatusBarText;

	internal ProgressBar ProgressBar;

	internal TextBlock ProgressBarText;

	private bool _contentLoaded;

	public ProgressManager Progress { get; set; }

	public EMFtoXAMLWindow(string strSqlConnection, string strOledbConnection)
	{
		Progress = new ProgressManager();
		InitializeComponent();
		TranslateComponent();
		_strOledbConnectionString = strOledbConnection;
		_conn = new SqlConnection(strSqlConnection);
		_worker = new BackgroundWorker();
		_worker.WorkerReportsProgress = true;
		_worker.WorkerSupportsCancellation = true;
		_worker.DoWork += WorkerDoWork;
		_worker.ProgressChanged += Worker_ProgressChanged;
		_worker.RunWorkerCompleted += WorkerRunWorkerCompleted;
	}

	protected override void OnClosing(CancelEventArgs e)
	{
		base.Cursor = Cursors.Arrow;
		if (!_worker.IsBusy)
		{
			return;
		}
		if (ConfirmCancel())
		{
			_worker.CancelAsync();
			if (_conn.State == ConnectionState.Open)
			{
				_conn.Close();
			}
			return;
		}
		base.OnClosing(e);
		e.Cancel = true;
		if (_worker.IsBusy)
		{
			base.Cursor = Cursors.Wait;
		}
	}

	private void WorkerDoWork(object sender, DoWorkEventArgs e)
	{
		if (_conn.State == ConnectionState.Closed)
		{
			_conn.Open();
		}
		_logErrors = new List<string>();
		if (_bMaterials)
		{
			BulkMaterials(_bOverwriteAll, e);
		}
		if (e.Cancel)
		{
			return;
		}
		if (_bModels)
		{
			BulkModels(_bOverwriteAll, e);
		}
		if (!e.Cancel)
		{
			if (_bOptions)
			{
				BulkOptionValues(_bOverwriteAll, e);
			}
			if (!e.Cancel && _bSymbols)
			{
				BulkSymbols(_bOverwriteAll, e);
			}
		}
	}

	private void AcceptButton_Click(object sender, RoutedEventArgs e)
	{
		AcceptButton.IsEnabled = false;
		base.Cursor = Cursors.Wait;
		Progress.Success = null;
		ProcessCheck.Visibility = Visibility.Visible;
		_bOverwriteAll = OverwriteAllCheck.IsChecked.Value;
		_bMaterials = MaterialsCheck.IsChecked.Value;
		_bModels = ModelsCheck.IsChecked.Value;
		_bOptions = OptionsCheck.IsChecked.Value;
		_bSymbols = SymbolsCheck.IsChecked.Value;
		_convType = ((!EMFToXAML_RadioButton.IsChecked.Value) ? ConversionType.DxFtoXaml : ConversionType.EmfToXaml);
		_worker.RunWorkerAsync();
	}

	private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
	{
		StatusBarText.Text = _statusText;
		ProgressBarText.Text = $"( {e.ProgressPercentage}% )";
		ProgressBar.Value = e.ProgressPercentage;
	}

	private void WorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
	{
		base.Cursor = Cursors.Arrow;
		ProgressBar.Value = 0.0;
		AcceptButton.IsEnabled = true;
		ProgressBarText.Text = string.Empty;
		if (!e.Cancelled)
		{
			_statusText = Preference.Wpf.Controls.Properties.Resources.XamlConversorStatusBarReady;
			Progress.Success = true;
		}
		else
		{
			_statusText = Preference.Wpf.Controls.Properties.Resources.XamlConversorStatusBarCanceled;
			Progress.Success = false;
		}
		StatusBarText.Text = _statusText;
		if (!e.Cancelled && _logErrors.Any())
		{
			ErrorDialogLog errorDialogLog = new ErrorDialogLog(Preference.Wpf.Controls.Properties.Resources.XamlConversorFinishWithErrors, _logErrors);
			errorDialogLog.Owner = this;
			errorDialogLog.ShowDialog();
		}
	}

	private void BulkMaterials(bool bAll, DoWorkEventArgs e)
	{
		_statusText = $"{Preference.Wpf.Controls.Properties.Resources.XamlConversorMaterials}...";
		if (_convType == ConversionType.EmfToXaml)
		{
			string cmdText = "SELECT COUNT(*) FROM MaterialesBase WHERE Esquema IS NOT NULL";
			string commandText = "SELECT RTRIM(ReferenciaBase) as ReferenciaBase FROM MaterialesBase WHERE Esquema IS NOT NULL";
			if (!bAll)
			{
				cmdText = "SELECT COUNT(*) FROM MaterialesBase WHERE Esquema IS NOT NULL AND XamlSilverlight IS NULL";
				commandText = "SELECT RTRIM(ReferenciaBase) as ReferenciaBase FROM MaterialesBase WHERE Esquema IS NOT NULL AND XamlSilverlight IS NULL";
			}
			using SqlConnection sqlConnection = new SqlConnection(_conn.ConnectionString);
			sqlConnection.Open();
			SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection);
			int num = (int)sqlCommand.ExecuteScalar();
			sqlCommand.CommandText = commandText;
			List<string> list = new List<string>();
			using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
			{
				while (sqlDataReader.Read())
				{
					string item = sqlDataReader["ReferenciaBase"].ToString();
					list.Add(item);
				}
			}
			int num2 = 0;
			byte[] buffer = default(byte[]);
			foreach (string item2 in list)
			{
				if (_worker.CancellationPending)
				{
					e.Cancel = true;
					break;
				}
				commandText = (sqlCommand.CommandText = $"SELECT Esquema FROM dbo.MaterialesBase WHERE ReferenciaBase = N'{item2}'");
				byte[] array = (byte[])sqlCommand.ExecuteScalar();
				string text2;
				if (CPrefZipNET.IsZipped(array))
				{
					CPrefZipNET.UnzipBLOB(array, ref buffer);
					text2 = ConvertEmfToSilverlightXaml(item2, "MaterialesBase", buffer);
				}
				else
				{
					text2 = ConvertEmfToSilverlightXaml(item2, "MaterialesBase", array);
				}
				if (text2 != null)
				{
					XamlMaterial.UpdateXamlToDB(_conn, item2, text2);
				}
				num2++;
				_worker.ReportProgress(num2 * 100 / num);
			}
			sqlConnection.Close();
			return;
		}
		string strQuery = "SELECT RTRIM(ReferenciaBase) as ReferenciaBase FROM MaterialesBase WHERE ISNULL(CONVERT(nvarchar,DXF),N'') <> N''";
		if (!bAll)
		{
			strQuery = "SELECT RTRIM(ReferenciaBase) as ReferenciaBase FROM MaterialesBase WHERE ISNULL(CONVERT(nvarchar,DXF),N'') <> N'' AND XamlSilverlight IS NULL";
		}
		List<string> simpleList = GetSimpleList(strQuery);
		MaterialRenderer.ConnectionString = _strOledbConnectionString;
		int num3 = 0;
		foreach (string item3 in simpleList)
		{
			if (_worker.CancellationPending)
			{
				e.Cancel = true;
				break;
			}
			MaterialRenderer.UpdateSilverlightXamlFromDXF(item3);
			num3++;
			_worker.ReportProgress(num3 * 100 / simpleList.Count);
		}
	}

	private void BulkModels(bool bAll, DoWorkEventArgs e)
	{
		_statusText = $"{Preference.Wpf.Controls.Properties.Resources.XamlConversorModels}...";
		if (_convType == ConversionType.EmfToXaml)
		{
			string cmdText = "SELECT COUNT(*) FROM Dibujos WHERE Metafile IS NOT NULL";
			string commandText = "SELECT RTRIM(Codigo) as Codigo FROM Dibujos WHERE Metafile IS NOT NULL";
			if (!bAll)
			{
				cmdText = "SELECT COUNT(*) FROM Dibujos WHERE Metafile IS NOT NULL AND XamlSilverlight IS NULL";
				commandText = "SELECT RTRIM(Codigo) as Codigo FROM Dibujos WHERE Metafile IS NOT NULL AND XamlSilverlight IS NULL";
			}
			using SqlConnection sqlConnection = new SqlConnection(_conn.ConnectionString);
			sqlConnection.Open();
			SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection);
			int num = (int)sqlCommand.ExecuteScalar();
			sqlCommand.CommandText = commandText;
			List<string> list = new List<string>();
			using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
			{
				while (sqlDataReader.Read())
				{
					string item = sqlDataReader["Codigo"].ToString();
					list.Add(item);
				}
			}
			int num2 = 0;
			byte[] buffer = default(byte[]);
			foreach (string item2 in list)
			{
				if (_worker.CancellationPending)
				{
					e.Cancel = true;
					break;
				}
				commandText = (sqlCommand.CommandText = $"SELECT Metafile FROM dbo.Dibujos WHERE Codigo = N'{item2}'");
				byte[] array = (byte[])sqlCommand.ExecuteScalar();
				string text2;
				if (CPrefZipNET.IsZipped(array))
				{
					CPrefZipNET.UnzipBLOB(array, ref buffer);
					text2 = ConvertEmfToSilverlightXaml(item2, "Dibujos", buffer);
				}
				else
				{
					text2 = ConvertEmfToSilverlightXaml(item2, "Dibujos", array);
				}
				if (text2 != null)
				{
					XamlModel.UpdateXamlToDB(_conn, item2, text2);
				}
				num2++;
				_worker.ReportProgress(num2 * 100 / num);
			}
			sqlConnection.Close();
			return;
		}
		string strQuery = "SELECT RTRIM(Codigo) as Codigo FROM Dibujos WHERE Buffer IS NOT NULL";
		if (!bAll)
		{
			strQuery = "SELECT RTRIM(Codigo) as Codigo FROM Dibujos WHERE Buffer IS NOT NULL AND XamlSilverlight IS NULL";
		}
		List<string> simpleList = GetSimpleList(strQuery);
		ModelRenderer.ConnectionString = _strOledbConnectionString;
		int num3 = 0;
		foreach (string item3 in simpleList)
		{
			if (_worker.CancellationPending)
			{
				e.Cancel = true;
				break;
			}
			string silverlightXaml = ModelRenderer.GetSilverlightXaml(item3);
			if (!string.IsNullOrEmpty(silverlightXaml))
			{
				XamlModel.UpdateXamlToDB(_conn, item3, silverlightXaml);
			}
			num3++;
			_worker.ReportProgress(num3 * 100 / simpleList.Count);
		}
	}

	private void BulkOptionValues(bool bAll, DoWorkEventArgs e)
	{
		if (_convType != 0)
		{
			return;
		}
		_statusText = $"{Preference.Wpf.Controls.Properties.Resources.XamlConversorOptions}...";
		string text = "SELECT COUNT(*) FROM ContenidoOpciones WHERE Esquema IS NOT NULL";
		string commandText = "SELECT DataVerId, RTRIM(Opcion) as Opcion, Orden, RTRIM(Valor) as Valor FROM ContenidoOpciones WHERE Esquema IS NOT NULL";
		if (!bAll)
		{
			text += " AND XamlSilverlight IS NULL";
			commandText = "SELECT DataVerId, RTRIM(Opcion) as Opcion, Orden, RTRIM(Valor) as Valor FROM ContenidoOpciones WHERE Esquema IS NOT NULL AND XamlSilverlight IS NULL";
		}
		using SqlConnection sqlConnection = new SqlConnection(_conn.ConnectionString);
		sqlConnection.Open();
		SqlCommand sqlCommand = new SqlCommand(text, sqlConnection);
		int num = (int)sqlCommand.ExecuteScalar();
		sqlCommand.CommandText = commandText;
		List<Tuple<string, string, string, string>> list = new List<Tuple<string, string, string, string>>();
		using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
		{
			while (sqlDataReader.Read())
			{
				string item = sqlDataReader["DataVerId"].ToString();
				string item2 = sqlDataReader["Opcion"].ToString();
				string item3 = sqlDataReader["Orden"].ToString();
				string item4 = sqlDataReader["Valor"].ToString();
				list.Add(new Tuple<string, string, string, string>(item, item2, item3, item4));
			}
		}
		int num2 = 0;
		byte[] buffer = default(byte[]);
		foreach (Tuple<string, string, string, string> item9 in list)
		{
			if (_worker.CancellationPending)
			{
				e.Cancel = true;
				break;
			}
			string item5 = item9.Item1;
			string item6 = item9.Item2;
			string item7 = item9.Item3;
			string item8 = item9.Item4;
			commandText = (sqlCommand.CommandText = $"SELECT Esquema FROM dbo.ContenidoOpciones WHERE DataVerId = '{item5}'AND Opcion = N'{item6}' AND Orden = '{item7}' AND Valor = N'{item8}'");
			byte[] array = (byte[])sqlCommand.ExecuteScalar();
			string objectName = item6 + "-" + item8;
			string text3;
			if (CPrefZipNET.IsZipped(array))
			{
				CPrefZipNET.UnzipBLOB(array, ref buffer);
				text3 = ConvertEmfToSilverlightXaml(objectName, "ContenidoOpciones", buffer);
			}
			else
			{
				text3 = ConvertEmfToSilverlightXaml(objectName, "ContenidoOpciones", array);
			}
			if (text3 != null)
			{
				XamlOption.UpdateXamlToDB(_conn, item5, item6, item7, text3);
			}
			num2++;
			_worker.ReportProgress(num2 * 100 / num);
		}
		sqlConnection.Close();
	}

	private void BulkSymbols(bool bAll, DoWorkEventArgs e)
	{
		if (_convType != 0)
		{
			return;
		}
		_statusText = $"{Preference.Wpf.Controls.Properties.Resources.XamlConversorSymbols}...";
		string text = "SELECT COUNT(*) FROM Symbols WHERE Metafile IS NOT NULL";
		string text2 = "SELECT COUNT(*) FROM dbo.VariablesGlobales WHERE [Buffer] IS NOT NULL";
		string text3 = "SELECT RTRIM(Name) as Name FROM Symbols WHERE Metafile IS NOT NULL";
		string text4 = "SELECT RTRIM(Nombre) as Nombre FROM dbo.VariablesGlobales WHERE [Buffer] IS NOT NULL";
		if (!bAll)
		{
			text += " AND XamlSilverlight IS NULL";
			text2 += " AND ISNULL(CONVERT(nvarchar(max), Silverlight), '') = ''";
			text3 += " AND XamlSilverlight IS NULL";
			text4 += " AND ISNULL(CONVERT(nvarchar(max), Silverlight), '') = ''";
		}
		using SqlConnection sqlConnection = new SqlConnection(_conn.ConnectionString);
		sqlConnection.Open();
		SqlCommand sqlCommand = new SqlCommand(text, sqlConnection);
		int num = (int)sqlCommand.ExecuteScalar();
		sqlCommand.CommandText = text2;
		num += (int)sqlCommand.ExecuteScalar();
		sqlCommand.CommandText = text3;
		List<string> list = new List<string>();
		using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
		{
			while (sqlDataReader.Read())
			{
				string item = sqlDataReader["Name"].ToString();
				list.Add(item);
			}
		}
		int num2 = 0;
		byte[] buffer = default(byte[]);
		foreach (string item3 in list)
		{
			if (_worker.CancellationPending)
			{
				e.Cancel = true;
				break;
			}
			text3 = (sqlCommand.CommandText = $"SELECT Metafile FROM dbo.Symbols WHERE Name = N'{item3}'");
			byte[] array = (byte[])sqlCommand.ExecuteScalar();
			string text6;
			if (CPrefZipNET.IsZipped(array))
			{
				CPrefZipNET.UnzipBLOB(array, ref buffer);
				text6 = ConvertEmfToSilverlightXaml(item3, "Symbol", buffer);
			}
			else
			{
				text6 = ConvertEmfToSilverlightXaml(item3, "Symbol", array);
			}
			if (text6 != null)
			{
				XamlSymbol.UpdateXamlToDB(_conn, item3, text6);
			}
			num2++;
			_worker.ReportProgress(num2 * 100 / num);
		}
		sqlCommand.CommandText = text4;
		List<string> list2 = new List<string>();
		using (SqlDataReader sqlDataReader2 = sqlCommand.ExecuteReader())
		{
			while (sqlDataReader2.Read())
			{
				string item2 = sqlDataReader2["Nombre"].ToString();
				list2.Add(item2);
			}
		}
		byte[] buffer2 = default(byte[]);
		foreach (string item4 in list2)
		{
			if (_worker.CancellationPending)
			{
				e.Cancel = true;
				break;
			}
			text3 = (sqlCommand.CommandText = $"SELECT [Buffer] FROM dbo.VariablesGlobales WHERE Nombre = N'{item4}'");
			byte[] array2 = (byte[])sqlCommand.ExecuteScalar();
			string text6;
			if (CPrefZipNET.IsZipped(array2))
			{
				CPrefZipNET.UnzipBLOB(array2, ref buffer2);
				text6 = ConvertEmfToSilverlightXaml(item4, "VariablesGlobales", buffer2);
			}
			else
			{
				text6 = ConvertEmfToSilverlightXaml(item4, "VariablesGlobales", array2);
			}
			if (text6 != null)
			{
				XamlGlobalVariable.UpdateXamlToDB(_conn, item4, text6);
			}
			num2++;
			_worker.ReportProgress(num2 * 100 / num);
		}
		sqlConnection.Close();
	}

	private string ConvertEmfToSilverlightXaml(string objectName, string objectType, byte[] buffer)
	{
		string text = null;
		try
		{
			text = Emf.EmfToSilverlightXaml(buffer);
		}
		catch (Exception ex)
		{
			string text2 = (string.IsNullOrEmpty(ex.Message) ? string.Empty : $" Exception Message: '{ex.Message}'.");
			string text3 = ((ex.InnerException == null) ? string.Empty : $" Inner Exception: '{ex.InnerException}'.");
			_logErrors.Add($"The metafile '{objectName}' from table '{objectType}' has launched exception.{text2}{text3}");
		}
		if (string.IsNullOrEmpty(text))
		{
			return null;
		}
		XElement xElement = XElement.Parse(text);
		if (!xElement.Descendants(_xamlNamespace + "BitmapImage").Attributes("UriSource").Any())
		{
			return text;
		}
		_logErrors.Add($"The metafile '{objectName}' from table '{objectType}' contains a bitmap and can't import to database.");
		return null;
	}

	private List<string> GetSimpleList(string strQuery)
	{
		SqlCommand sqlCommand = new SqlCommand(strQuery, _conn);
		SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
		List<string> list = new List<string>();
		while (sqlDataReader.Read())
		{
			string item = sqlDataReader.GetValue(0).ToString();
			list.Add(item);
		}
		sqlDataReader.Dispose();
		return list;
	}

	private void CancelButton_Click(object sender, RoutedEventArgs e)
	{
		if (_worker.IsBusy)
		{
			if (ConfirmCancel())
			{
				_worker.CancelAsync();
			}
		}
		else
		{
			Close();
		}
	}

	private bool ConfirmCancel()
	{
		QuestionWindow questionWindow = new QuestionWindow();
		questionWindow.Title = Preference.Wpf.Controls.Properties.Resources.XamlConversorWindowHeader;
		questionWindow.QuestionText = Preference.Wpf.Controls.Properties.Resources.XamlConversorCancelMessage;
		questionWindow.DontAskCheckbox.Visibility = Visibility.Hidden;
		return questionWindow.ShowDialog().Value;
	}

	private void TranslateComponent()
	{
		base.Title = Preference.Wpf.Controls.Properties.Resources.XamlConversorWindowHeader;
		ConversionTypeGroupBox.Header = Preference.Wpf.Controls.Properties.Resources.XamlConversorConvTypeGroupBoxHeader;
		EMFToXAML_RadioButton.Content = Preference.Wpf.Controls.Properties.Resources.XamlConversorEMFtoXAMLRadioButton;
		DXFToXAML_RadioButton.Content = Preference.Wpf.Controls.Properties.Resources.XamlConversorDXFtoXAMLRadioButton;
		ItemsGroupBox.Header = Preference.Wpf.Controls.Properties.Resources.XamlConversorItemsGroupBoxHeader;
		MaterialsCheck.Content = Preference.Wpf.Controls.Properties.Resources.XamlConversorMaterials;
		ModelsCheck.Content = Preference.Wpf.Controls.Properties.Resources.XamlConversorModels;
		OptionsCheck.Content = Preference.Wpf.Controls.Properties.Resources.XamlConversorOptions;
		SymbolsCheck.Content = Preference.Wpf.Controls.Properties.Resources.XamlConversorSymbols;
		OverwriteAllCheck.Content = Preference.Wpf.Controls.Properties.Resources.XamlConversorOverwriteAllCheck;
		StatusBarText.Text = Preference.Wpf.Controls.Properties.Resources.XamlConversorStatusBarReady;
		AcceptButton.Content = Preference.Wpf.Controls.Properties.Resources.StringButtonOk;
		CancelButton.Content = Preference.Wpf.Controls.Properties.Resources.StringButtonCancel;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/Preference.Wpf.Controls;component/emftoxamlwindow.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	void IComponentConnector.Connect(int connectionId, object target)
	{
		switch (connectionId)
		{
		case 1:
			ConversionTypeGroupBox = (GroupBox)target;
			break;
		case 2:
			EMFToXAML_RadioButton = (RadioButton)target;
			break;
		case 3:
			DXFToXAML_RadioButton = (RadioButton)target;
			break;
		case 4:
			ItemsGroupBox = (GroupBox)target;
			break;
		case 5:
			MaterialsCheck = (CheckBox)target;
			break;
		case 6:
			ModelsCheck = (CheckBox)target;
			break;
		case 7:
			OptionsCheck = (CheckBox)target;
			break;
		case 8:
			SymbolsCheck = (CheckBox)target;
			break;
		case 9:
			OverwriteAllCheck = (CheckBox)target;
			break;
		case 10:
			AcceptButton = (Button)target;
			AcceptButton.Click += AcceptButton_Click;
			break;
		case 11:
			CancelButton = (Button)target;
			CancelButton.Click += CancelButton_Click;
			break;
		case 12:
			ProcessCheck = (CheckBox)target;
			break;
		case 13:
			StatusBarText = (TextBlock)target;
			break;
		case 14:
			ProgressBar = (ProgressBar)target;
			break;
		case 15:
			ProgressBarText = (TextBlock)target;
			break;
		default:
			_contentLoaded = true;
			break;
		}
	}
}
