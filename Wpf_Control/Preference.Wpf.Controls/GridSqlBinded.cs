using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using Xceed.Wpf.DataGrid;

namespace Preference.Wpf.Controls;

public class GridSqlBinded : DataGridControl, IDisposable, IComponentConnector
{
	private string _strSql;

	private string _strConnection;

	private DataSet _dataSet;

	private SqlDataAdapter _dataAdapter;

	private SqlCommandBuilder _commandBuilder;

	private bool _contentLoaded;

	public int Count
	{
		get
		{
			if (IsValid)
			{
				return DataSet.Tables[0].Rows.Count;
			}
			return 0;
		}
	}

	public string Connection
	{
		get
		{
			return _strConnection;
		}
		set
		{
			_strConnection = value;
		}
	}

	public string SelectCommand
	{
		get
		{
			return _strSql;
		}
		set
		{
			_strSql = value;
			Load();
		}
	}

	private SqlCommandBuilder CommandBuilder
	{
		get
		{
			return _commandBuilder;
		}
		set
		{
			_commandBuilder = value;
		}
	}

	private DataTable DataTable
	{
		get
		{
			if (IsValid)
			{
				return DataSet.Tables[0];
			}
			return null;
		}
	}

	private DataSet DataSet
	{
		get
		{
			return _dataSet;
		}
		set
		{
			_dataSet = value;
		}
	}

	private SqlDataAdapter DataAdapter
	{
		get
		{
			return _dataAdapter;
		}
		set
		{
			_dataAdapter = value;
		}
	}

	private bool IsValid
	{
		get
		{
			if (DataSet != null)
			{
				return DataSet.Tables[0] != null;
			}
			return false;
		}
	}

	public event EventHandler DataChanged;

	public GridSqlBinded()
	{
		InitializeComponent();
	}

	public void Clear()
	{
		((Collection<Column>)(object)((DataGridControl)this).get_Columns()).Clear();
		((ItemsControl)this).ItemsSource = null;
	}

	public void Dispose()
	{
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}

	public void OnDataChanged()
	{
		if (this.DataChanged != null)
		{
			this.DataChanged(this, EventArgs.Empty);
		}
	}

	public void Save()
	{
		if (DataAdapter != null)
		{
			DataAdapter.Update(DataSet);
		}
	}

	protected virtual void Dispose(bool disposing)
	{
		if (disposing)
		{
			if (DataSet != null)
			{
				DataSet.Dispose();
			}
			if (DataAdapter != null)
			{
				DataAdapter.Dispose();
			}
		}
	}

	private void DataTableChanged(object sender, DataRowChangeEventArgs e)
	{
		OnDataChanged();
	}

	private void Load()
	{
		Clear();
		if (string.IsNullOrEmpty(SelectCommand) || string.IsNullOrEmpty(Connection))
		{
			return;
		}
		try
		{
			DataSet = new DataSet();
			DataSet.Locale = Thread.CurrentThread.CurrentUICulture;
			DataAdapter = new SqlDataAdapter(SelectCommand, Connection);
			DataAdapter.Fill(DataSet);
			if (IsValid)
			{
				CommandBuilder = new SqlCommandBuilder(DataAdapter);
				CommandBuilder.QuotePrefix = "[";
				CommandBuilder.QuoteSuffix = "]";
				BindingListCollectionView bindingListCollectionView = (BindingListCollectionView)(((ItemsControl)this).ItemsSource = new BindingListCollectionView(DataSet.Tables[0].DefaultView));
				DataTable.RowChanged += DataTableChanged;
				DataTable.RowDeleted += DataTableChanged;
			}
		}
		catch (SqlException ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/Preference.Wpf.Controls;component/gridsqlbinded.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	void IComponentConnector.Connect(int connectionId, object target)
	{
		_contentLoaded = true;
	}
}
