using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using Preference.PDE;
using Preference.Web;
using Preference.Wpf.Controls.Properties;

namespace Preference.Wpf.Controls;

public class TestWebSiteWindow : Window, IComponentConnector
{
	private HashSet<Uri> _sites;

	private BackgroundWorker _worker;

	private TestWebSiteResult _results;

	internal Label InternetConnectionLabel;

	internal CheckBox InternetConnectionCheck;

	internal Label DnsResolutionLabel;

	internal CheckBox DnsResolutionCheck;

	internal Label PortsLabel;

	internal CheckBox PortsCheck;

	internal Label SiteReachableLabel;

	internal CheckBox SiteReachableCheck;

	internal Label CertificateLabel;

	internal CheckBox CertificateCheck;

	internal Label TimeLabel;

	internal CheckBox TimeCheck;

	internal TextBox MoreInformationTextBlock;

	internal Button RetryButton;

	internal Button AcceptButton;

	private bool _contentLoaded;

	private BackgroundWorker Worker
	{
		get
		{
			return _worker;
		}
		set
		{
			_worker = value;
		}
	}

	private HashSet<Uri> Sites
	{
		get
		{
			return _sites;
		}
		set
		{
			_sites = value;
		}
	}

	public TestWebSiteResult Results
	{
		get
		{
			return _results;
		}
		set
		{
			_results = value;
		}
	}

	public TestWebSiteWindow()
	{
		Results = new TestWebSiteResult();
		Sites = new HashSet<Uri>();
		InitializeComponent();
		TranslateComponent();
		Worker = new BackgroundWorker();
		Worker.DoWork += WorkerDoWork;
		Worker.RunWorkerCompleted += WorkerRunWorkerCompleted;
	}

	public void AddSite(Uri site)
	{
		Sites.Add(site);
	}

	private void OnAcceptButtonClick(object sender, RoutedEventArgs e)
	{
		Close();
	}

	private void OnRetryButtonClick(object sender, RoutedEventArgs e)
	{
		if (Worker.IsBusy)
		{
			MessageBox.Show("There is already a test running.", "PrefSuite");
		}
		else
		{
			Worker.RunWorkerAsync();
		}
	}

	private void OnWindowLoaded(object sender, RoutedEventArgs e)
	{
		RunTestInBackground();
	}

	private void RunTestInBackground()
	{
		base.Cursor = Cursors.Wait;
		Worker.RunWorkerAsync();
	}

	private bool TestInternetConnection(TestWebSiteResult results)
	{
		results.ExtendedInfo = results.ExtendedInfo + Preference.Wpf.Controls.Properties.Resources.CheckingInternetConnection + " ..... ";
		results.IsConnectedToInternet = ConnectionTester.IsConnectedToInternet();
		results.IsConnectedToInternetTestRunning = false;
		results.ExtendedInfo += (results.IsConnectedToInternet.Value ? (Preference.Wpf.Controls.Properties.Resources.TestSuccess + "\t\n") : (Preference.Wpf.Controls.Properties.Resources.TestFailed + "\t\n"));
		return results.IsConnectedToInternet.Value;
	}

	private bool TestPortsOpen(TestWebSiteResult results, HashSet<Uri> sites)
	{
		HashSet<Uri> hashSet = new HashSet<Uri>();
		foreach (Uri site in sites)
		{
			hashSet.Add(new Uri($"{site.Scheme}://{site.DnsSafeHost}"));
		}
		foreach (Uri item in hashSet)
		{
			string text = $"{Preference.Wpf.Controls.Properties.Resources.CheckingPorts} : {item.ToString()}  ({item.Port}) ..... ";
			results.ExtendedInfo += text;
			if (!results.ArePortsOpen.HasValue)
			{
				results.ArePortsOpen = ConnectionTester.TestPort(item);
			}
			else
			{
				bool? arePortsOpen = results.ArePortsOpen;
				results.ArePortsOpen = ConnectionTester.TestPort(item) & arePortsOpen;
			}
			results.ExtendedInfo += (results.ArePortsOpen.Value ? (Preference.Wpf.Controls.Properties.Resources.TestSuccess + "\t\n") : (Preference.Wpf.Controls.Properties.Resources.TestFailed + "\t\n"));
			if (!results.ArePortsOpen.Value)
			{
				break;
			}
		}
		results.ArePortsOpenTestRunning = false;
		return results.ArePortsOpen.Value;
	}

	private bool TestSitesReachables(TestWebSiteResult results, HashSet<Uri> sites)
	{
		foreach (Uri site in sites)
		{
			results.ExtendedInfo += $"{Preference.Wpf.Controls.Properties.Resources.CheckingSiteOnline} ({site.ToString()}) ..... ";
			WebExceptionStatus webExceptionStatus = WebExceptionStatus.Success;
			bool flag = ConnectionTester.TestWebSite(site, ref webExceptionStatus);
			if (!results.AreSitesReachables.HasValue)
			{
				results.AreSitesReachables = flag || webExceptionStatus == WebExceptionStatus.TrustFailure;
			}
			else
			{
				bool? areSitesReachables = results.AreSitesReachables;
				results.AreSitesReachables = (flag || webExceptionStatus == WebExceptionStatus.TrustFailure) & areSitesReachables;
			}
			results.ExtendedInfo += (results.AreSitesReachables.Value ? (Preference.Wpf.Controls.Properties.Resources.TestSuccess + "\t\n") : (webExceptionStatus.ToString() + "\t\n"));
			if (!results.AreSitesReachables.Value)
			{
				break;
			}
			results.ExtendedInfo += $"{Preference.Wpf.Controls.Properties.Resources.CheckingCertificate} ({site.ToString()}) ..... ";
			if (!results.AreCertificatesValid.HasValue)
			{
				results.AreCertificatesValid = webExceptionStatus != WebExceptionStatus.TrustFailure;
			}
			else
			{
				bool? areSitesReachables = results.AreCertificatesValid;
				results.AreCertificatesValid = ((webExceptionStatus != WebExceptionStatus.TrustFailure) ? areSitesReachables : new bool?(false));
			}
			results.ExtendedInfo += (results.AreCertificatesValid.Value ? (Preference.Wpf.Controls.Properties.Resources.TestSuccess + "\t\n") : (Preference.Wpf.Controls.Properties.Resources.TestFailed + "\t\n"));
			if (!results.AreCertificatesValid.Value)
			{
				break;
			}
		}
		results.AreSitesReachablesTestRunning = false;
		results.AreCertificatesValidTestRunning = false;
		if (results.AreCertificatesValid.Value)
		{
			return results.AreSitesReachables.Value;
		}
		return false;
	}

	private bool TestUriResoulution(TestWebSiteResult results, HashSet<Uri> sites)
	{
		HashSet<Uri> hashSet = new HashSet<Uri>();
		foreach (Uri site in sites)
		{
			hashSet.Add(new Uri($"{site.Scheme}://{site.DnsSafeHost}"));
		}
		foreach (Uri item in hashSet)
		{
			results.ExtendedInfo += $"{Preference.Wpf.Controls.Properties.Resources.CheckingDnsResolution} ({item}) ..... ";
			if (!results.CanResolveUris.HasValue)
			{
				results.CanResolveUris = ConnectionTester.CanUriBeResolved(item);
			}
			else
			{
				bool? canResolveUris = results.CanResolveUris;
				results.CanResolveUris = ConnectionTester.CanUriBeResolved(item) & canResolveUris;
			}
			results.ExtendedInfo += (results.CanResolveUris.Value ? (Preference.Wpf.Controls.Properties.Resources.TestSuccess + "\t\n") : (Preference.Wpf.Controls.Properties.Resources.TestFailed + "\t\n"));
			if (!results.CanResolveUris.Value)
			{
				break;
			}
		}
		results.CanResolveUrisTestRunning = false;
		return results.CanResolveUris.Value;
	}

	private bool TestTime(TestWebSiteResult results, HashSet<Uri> Sites)
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Expected O, but got Unknown
		try
		{
			foreach (Uri Site in Sites)
			{
				if (Site.ToString().Contains("Preference.PDE.DocumentsService/service.svc"))
				{
					results.ExtendedInfo = results.ExtendedInfo + Preference.Wpf.Controls.Properties.Resources.CheckingTime + " .... ";
					SyncClient val = new SyncClient();
					((Base)val).set_BaseUri($"{Site.Scheme}://{Site.DnsSafeHost}");
					results.IsTimeSynchronized = val.IsTimeSynchronized();
					break;
				}
			}
		}
		catch
		{
			Results.IsTimeSynchronized = false;
		}
		results.ExtendedInfo += (results.IsTimeSynchronized.Value ? (Preference.Wpf.Controls.Properties.Resources.TestSuccess + "\t\n") : (Preference.Wpf.Controls.Properties.Resources.TestFailed + "\t\n"));
		Results.IsTimeSynchronizedTestRunning = false;
		return results.IsTimeSynchronized.Value;
	}

	private void TranslateComponent()
	{
		InternetConnectionLabel.Content = Preference.Wpf.Controls.Properties.Resources.InternetConnection;
		DnsResolutionLabel.Content = Preference.Wpf.Controls.Properties.Resources.DnsResolution;
		PortsLabel.Content = Preference.Wpf.Controls.Properties.Resources.PortsOpen;
		SiteReachableLabel.Content = Preference.Wpf.Controls.Properties.Resources.SiteReachable;
		CertificateLabel.Content = Preference.Wpf.Controls.Properties.Resources.CertificateLabel;
		RetryButton.Content = Preference.Wpf.Controls.Properties.Resources.Retry;
		AcceptButton.Content = Preference.Wpf.Controls.Properties.Resources.StringButtonOk;
		TimeLabel.Content = Preference.Wpf.Controls.Properties.Resources.TimeDifference;
	}

	private void WorkerDoWork(object sender, DoWorkEventArgs e)
	{
		Results.Reset();
		if (TestInternetConnection(Results) && TestUriResoulution(Results, Sites) && TestPortsOpen(Results, Sites) && TestSitesReachables(Results, Sites))
		{
			TestTime(Results, Sites);
		}
		e.Result = true;
	}

	private void WorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
	{
		Results.IsTestRunning = false;
		base.Cursor = Cursors.Arrow;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/Preference.Wpf.Controls;component/testwebsitewindow.xaml", UriKind.Relative);
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
			((TestWebSiteWindow)target).Loaded += OnWindowLoaded;
			break;
		case 2:
			InternetConnectionLabel = (Label)target;
			break;
		case 3:
			InternetConnectionCheck = (CheckBox)target;
			break;
		case 4:
			DnsResolutionLabel = (Label)target;
			break;
		case 5:
			DnsResolutionCheck = (CheckBox)target;
			break;
		case 6:
			PortsLabel = (Label)target;
			break;
		case 7:
			PortsCheck = (CheckBox)target;
			break;
		case 8:
			SiteReachableLabel = (Label)target;
			break;
		case 9:
			SiteReachableCheck = (CheckBox)target;
			break;
		case 10:
			CertificateLabel = (Label)target;
			break;
		case 11:
			CertificateCheck = (CheckBox)target;
			break;
		case 12:
			TimeLabel = (Label)target;
			break;
		case 13:
			TimeCheck = (CheckBox)target;
			break;
		case 14:
			MoreInformationTextBlock = (TextBox)target;
			break;
		case 15:
			RetryButton = (Button)target;
			RetryButton.Click += OnRetryButtonClick;
			break;
		case 16:
			AcceptButton = (Button)target;
			AcceptButton.Click += OnAcceptButtonClick;
			break;
		default:
			_contentLoaded = true;
			break;
		}
	}
}
