using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;
using System.Xml;
using Preference.Wpf.Controls.Properties;

namespace Preference.Wpf.Controls.Projects.AppLogic;

public class PrefPricesPolicy : INotifyPropertyChanged
{
	private PrefCollection<PrefProjectExpenditure> m_listExpenditures = new PrefCollection<PrefProjectExpenditure>();

	private PrefCollection<PrefProjectPerGroupExpenditure> m_listPerGroupExpenditures = new PrefCollection<PrefProjectPerGroupExpenditure>();

	private double m_dTotalEffectiveCost;

	private double m_dTotalSale;

	private double m_dSaleDiscount;

	private double m_dTotalEffectiveSale;

	private double m_dIndustrialBenefit;

	private double m_dBenefitMargin;

	private bool m_bChangedByUser;

	private bool m_bIsPriceDetailLoaded;

	private bool m_bIsEveryGroupVisible;

	private bool _IsSaleAmountFixed;

	private string providerDiscountTariffName = "ProviderDiscount";

	private string costIncrementTariffName = "CostIncrement";

	private string salesIncrementTariffName = "SalesIncrement";

	public string SalesIncrementTariffName
	{
		get
		{
			return salesIncrementTariffName;
		}
		set
		{
			salesIncrementTariffName = value;
		}
	}

	public string CostIncrementTariffName
	{
		get
		{
			return costIncrementTariffName;
		}
		set
		{
			costIncrementTariffName = value;
		}
	}

	public string ProviderDiscountTariffName
	{
		get
		{
			return providerDiscountTariffName;
		}
		set
		{
			providerDiscountTariffName = value;
		}
	}

	public bool IsSaleAmountFixed
	{
		get
		{
			return _IsSaleAmountFixed;
		}
		set
		{
			_IsSaleAmountFixed = value;
			foreach (PrefProjectPerGroupExpenditure listPerGroupExpenditure in m_listPerGroupExpenditures)
			{
				if (value)
				{
					listPerGroupExpenditure.PreviousCoefficient = listPerGroupExpenditure.CoefficientAsFactor;
					listPerGroupExpenditure.PreviousProviderDiscount = listPerGroupExpenditure.ProviderDiscountAsFactor;
					listPerGroupExpenditure.PreviousRemnantIncrement = listPerGroupExpenditure.RemnantIncrementAsFactor;
				}
				listPerGroupExpenditure.IsSaleAmountFixed = value;
			}
		}
	}

	public bool IsEveryGroupVisible
	{
		get
		{
			return m_bIsEveryGroupVisible;
		}
		set
		{
			m_bIsEveryGroupVisible = value;
			CollectionView collectionView = (CollectionView)CollectionViewSource.GetDefaultView(PerGroupExpenditures);
			if (collectionView.CanFilter)
			{
				collectionView.Filter = (value ? null : new Predicate<object>(GroupContainsCost));
			}
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs("IsEveryGroupVisible"));
			}
		}
	}

	public bool IsPriceDetailLoaded
	{
		get
		{
			return m_bIsPriceDetailLoaded;
		}
		set
		{
			m_bIsPriceDetailLoaded = value;
		}
	}

	public bool ChangedByUser
	{
		get
		{
			return m_bChangedByUser;
		}
		set
		{
			m_bChangedByUser = value;
		}
	}

	public double TotalEffectiveCost
	{
		get
		{
			return m_dTotalEffectiveCost;
		}
		set
		{
			if (m_dTotalEffectiveCost != value)
			{
				m_dTotalEffectiveCost = value;
				OnPropertyChanged("TotalEffectiveCost");
			}
		}
	}

	public double TotalSale
	{
		get
		{
			return m_dTotalSale;
		}
		set
		{
			if (m_dTotalSale != value)
			{
				m_dTotalSale = value;
				OnPropertyChanged("TotalSale");
			}
		}
	}

	public double SaleDiscount
	{
		get
		{
			return m_dSaleDiscount;
		}
		set
		{
			if (m_dSaleDiscount != value)
			{
				m_dSaleDiscount = value;
				OnPropertyChanged("SaleDiscount");
			}
		}
	}

	public double TotalEffectiveSale
	{
		get
		{
			return m_dTotalEffectiveSale;
		}
		set
		{
			if (m_dTotalEffectiveSale != value)
			{
				m_dTotalEffectiveSale = value;
				OnPropertyChanged("TotalEffectiveSale");
			}
		}
	}

	public double IndustrialBenefit
	{
		get
		{
			return m_dIndustrialBenefit;
		}
		set
		{
			if (m_dIndustrialBenefit != value)
			{
				m_dIndustrialBenefit = value;
				OnPropertyChanged("IndustrialBenefit");
			}
		}
	}

	public double BenefitMargin
	{
		get
		{
			return m_dBenefitMargin;
		}
		set
		{
			if (m_dBenefitMargin != value)
			{
				m_dBenefitMargin = value;
				OnPropertyChanged("BenefitMargin");
			}
		}
	}

	public PrefCollection<PrefProjectExpenditure> Expenditures => m_listExpenditures;

	public PrefCollection<PrefProjectPerGroupExpenditure> PerGroupExpenditures => m_listPerGroupExpenditures;

	public event PropertyChangedEventHandler PropertyChanged;

	public void LoadPerGroupExpenditures(SalesDocument salesdoc)
	{
		PerGroupExpenditures.Clear();
		if (salesdoc != null)
		{
			ServiceAgent.LoadPurchaseGroupExpenditures(PerGroupExpenditures, ProviderDiscountTariffName, CostIncrementTariffName, SalesIncrementTariffName);
		}
		foreach (PrefGroup priceGroup in PrefPriceGroupList.PriceGroups)
		{
			PrefProjectPerGroupExpenditure prefProjectPerGroupExpenditure = new PrefProjectPerGroupExpenditure();
			prefProjectPerGroupExpenditure.Group = priceGroup;
			prefProjectPerGroupExpenditure.ParentCollection = PerGroupExpenditures;
			prefProjectPerGroupExpenditure.IsProviderDiscountEditable = false;
			prefProjectPerGroupExpenditure.IsCostIncrementEditable = false;
			prefProjectPerGroupExpenditure.IsSalesIncrementEditable = true;
			prefProjectPerGroupExpenditure.Group.Supplier = Resources.IDS_WORKFORCE;
			PerGroupExpenditures.Add(prefProjectPerGroupExpenditure);
		}
		m_listPerGroupExpenditures.CollectionItemsChanged += m_listPerGroupExpenditures_CollectionChanged;
	}

	public PrefPricesPolicy()
	{
		PrefProjectExpenditure item = new PrefProjectExpenditure
		{
			Name = Resources.IDS_CONTINGENCIES,
			Key = "Contingencies",
			CoefficientAsFactor = 0.01,
			ParentCollection = Expenditures
		};
		Expenditures.Add(item);
		item = new PrefProjectExpenditure
		{
			Name = Resources.IDS_GENERALEXPENDITURES,
			Key = "GeneralExpenditures",
			CoefficientAsFactor = 0.02,
			ParentCollection = Expenditures
		};
		Expenditures.Add(item);
		item = new PrefProjectExpenditure
		{
			Name = Resources.IDS_FINANCIALEXPENDITURES,
			Key = "FinancialExpenditures",
			CoefficientAsFactor = 0.03,
			ParentCollection = Expenditures
		};
		Expenditures.Add(item);
		item = new PrefProjectExpenditure
		{
			Name = Resources.IDS_COMMISSIONS,
			Key = "Commissions",
			CoefficientAsFactor = 0.04,
			ParentCollection = Expenditures
		};
		Expenditures.Add(item);
		item = new PrefProjectExpenditure
		{
			Name = "Total",
			Key = "Total",
			CoefficientAsFactor = 0.1,
			ParentCollection = Expenditures
		};
		Expenditures.Add(item);
		m_listExpenditures.CollectionItemsChanged += m_listExpenditures_CollectionChanged;
	}

	private void m_listPerGroupExpenditures_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
	{
		OnPropertyChanged("PerGroupExpenditures");
	}

	private void m_listExpenditures_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
	{
		double num = 0.0;
		foreach (PrefProjectExpenditure listExpenditure in m_listExpenditures)
		{
			if (listExpenditure.Key != "Total")
			{
				num += listExpenditure.CoefficientAsFactor;
			}
		}
		if (m_listExpenditures.Count > 4)
		{
			m_listExpenditures[4].CoefficientAsFactor = num;
		}
		OnPropertyChanged("Expenditures");
	}

	public bool GroupContainsCost(object obj)
	{
		if (obj is PrefProjectPerGroupExpenditure prefProjectPerGroupExpenditure)
		{
			return prefProjectPerGroupExpenditure.EffectiveCost > 0.0;
		}
		return true;
	}

	public bool ValuateSalesDocument(SalesDocument salesdoc)
	{
		XmlDocument xmlDocument = null;
		XmlNamespaceManager xmlNamespaceManager = null;
		if (salesdoc == null)
		{
			return false;
		}
		bool flag = false;
		xmlDocument = new XmlDocument();
		ServiceAgent serviceAgent = new ServiceAgent(Globals.ConnectionString);
		string strResults = "";
		string strErrors = "";
		string strCommandXml = Globals.CommandsHeaderXML + "\r\n\t\t\t<cmd:Command name=\"GetSalesDocumentPerGroupMaterialCosts\"> \r\n\t\t\t<cmd:Parameter name=\"Number\" value=\"" + salesdoc.Number + "\"/>\r\n\t\t\t<cmd:Parameter name=\"Version\" value=\"" + salesdoc.Version + "\"/>\r\n\t\t\t</cmd:Command>\r\n\t\t<cmd:Command name=\"GetSalesDocumentPerGroupLabourCosts\">\r\n\t\t\t<cmd:Parameter name=\"Number\" value=\"" + salesdoc.Number + "\"/>\r\n\t\t\t<cmd:Parameter name=\"Version\" value=\"" + salesdoc.Version + "\"/>\r\n\t\t</cmd:Command>\r\n\t</cmd:Commands>";
		flag = serviceAgent.ExecuteCommand(strCommandXml, ref strResults, ref strErrors);
		if (!flag)
		{
			return false;
		}
		xmlDocument.LoadXml(strResults);
		xmlNamespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
		xmlNamespaceManager.AddNamespace("cmd", Globals.PrefCADCommandNamespaceUri);
		xmlNamespaceManager.AddNamespace("pmsg", Globals.MessageNamespaceUri);
		string empty = string.Empty;
		XmlNode xmlNode = null;
		ChangedByUser = true;
		foreach (PrefProjectPerGroupExpenditure perGroupExpenditure in PerGroupExpenditures)
		{
			switch (perGroupExpenditure.Group.Type)
			{
			case enGroupType.Offering:
				empty = "descendant::cmd:CommandResult[@name=\"GetSalesDocumentPerGroupLabourCosts\"]/descendant::cmd:Item[@name=\"" + Globals.ItemNamePriceGroup + "\"]/cmd:Item[@name=\"Code\"][@value=\"" + perGroupExpenditure.Group.Code + "\"]/..";
				xmlNode = xmlDocument.SelectSingleNode(empty, xmlNamespaceManager);
				if (xmlNode != null)
				{
					perGroupExpenditure.EffectiveCost = Convert.ToDouble(xmlNode.ChildNodes[1].Attributes["value"].Value, NumberFormatInfo.InvariantInfo);
					perGroupExpenditure.Group.Supplier = Resources.IDS_WORKFORCE;
				}
				else
				{
					perGroupExpenditure.EffectiveCost = 0.0;
				}
				break;
			case enGroupType.None:
			case enGroupType.Purchases:
			{
				if (perGroupExpenditure.Color != "")
				{
					empty = "descendant::cmd:CommandResult[@name=\"GetSalesDocumentPerGroupMaterialCosts\"]/descendant::cmd:Item[@name=\"" + Globals.ItemNamePurchaseGroup + "\"][cmd:Item[@name=\"Code\" and @value=\"" + perGroupExpenditure.Group.Code + "\"] and cmd:Item[@name=\"ProviderName\" and (@value=\"" + perGroupExpenditure.Group.Supplier + "\" or @value=\"\")] and cmd:Item[@name=\"Color\" and @value=\"" + perGroupExpenditure.Color + "\"]]";
				}
				else if (perGroupExpenditure.ListColorsRedefined == null)
				{
					empty = "descendant::cmd:CommandResult[@name=\"GetSalesDocumentPerGroupMaterialCosts\"]/descendant::cmd:Item[@name=\"" + Globals.ItemNamePurchaseGroup + "\"][cmd:Item[@name=\"Code\" and @value=\"" + perGroupExpenditure.Group.Code + "\"] and cmd:Item[@name=\"ProviderName\" and (@value=\"" + perGroupExpenditure.Group.Supplier + "\" or @value=\"\")]]";
				}
				else
				{
					empty = "descendant::cmd:CommandResult[@name=\"GetSalesDocumentPerGroupMaterialCosts\"]/descendant::cmd:Item[@name=\"" + Globals.ItemNamePurchaseGroup + "\"][cmd:Item[@name=\"Code\" and @value=\"" + perGroupExpenditure.Group.Code + "\"] and cmd:Item[@name=\"ProviderName\" and (@value=\"" + perGroupExpenditure.Group.Supplier + "\" or @value=\"\")]";
					foreach (PrefProjectPerGroupExpenditure item2 in perGroupExpenditure.ListColorsRedefined)
					{
						empty = empty + " and not(cmd:Item[@name=\"Color\" and @value=\"" + item2.Color + "\"]) ";
					}
					empty += "]";
				}
				XmlNodeList xmlNodeList = xmlDocument.SelectNodes(empty, xmlNamespaceManager);
				perGroupExpenditure.EffectiveCost = 0.0;
				foreach (XmlNode item3 in xmlNodeList)
				{
					perGroupExpenditure.EffectiveCost += Convert.ToDouble(item3.ChildNodes[2].Attributes["value"].Value, NumberFormatInfo.InvariantInfo);
				}
				break;
			}
			default:
				perGroupExpenditure.EffectiveCost = 0.0;
				break;
			}
		}
		ChangedByUser = true;
		CollectionView collectionView = (CollectionView)CollectionViewSource.GetDefaultView(PerGroupExpenditures);
		if (collectionView.CanGroup)
		{
			collectionView.GroupDescriptions.Clear();
			PropertyGroupDescription item = new PropertyGroupDescription("Group.Supplier");
			collectionView.GroupDescriptions.Add(item);
		}
		IsEveryGroupVisible = IsEveryGroupVisible;
		return flag;
	}

	protected void OnPropertyChanged(string propName)
	{
		double num = 0.0;
		double num2 = 0.0;
		double num3 = 0.0;
		double dTotalSale = m_dTotalSale;
		double num4 = 0.0;
		bool changedByUser = ChangedByUser;
		switch (propName)
		{
		case "SaleDiscount":
			if (ChangedByUser)
			{
				TotalEffectiveSale = m_dTotalSale * (100.0 - m_dSaleDiscount) / 100.0;
			}
			break;
		case "PerGroupExpenditures":
			if (!ChangedByUser)
			{
				break;
			}
			m_dTotalEffectiveCost = 0.0;
			m_dTotalSale = 0.0;
			foreach (PrefProjectPerGroupExpenditure listPerGroupExpenditure in m_listPerGroupExpenditures)
			{
				m_dTotalEffectiveCost += listPerGroupExpenditure.EffectiveCost;
				m_dTotalSale += listPerGroupExpenditure.Sale;
			}
			ChangedByUser = false;
			OnPropertyChanged("TotalEffectiveCost");
			ChangedByUser = false;
			TotalEffectiveSale = m_dTotalSale * (100.0 - m_dSaleDiscount) / 100.0;
			ChangedByUser = false;
			OnPropertyChanged("TotalSale");
			ChangedByUser = changedByUser;
			break;
		case "Expenditures":
		case "TotalEffectiveCost":
			if (!ChangedByUser)
			{
				break;
			}
			foreach (PrefProjectExpenditure listExpenditure in m_listExpenditures)
			{
				if (listExpenditure.Key != "Total")
				{
					num3 += listExpenditure.Result;
				}
			}
			ChangedByUser = false;
			IndustrialBenefit = m_dTotalEffectiveSale - num3 - m_dTotalEffectiveCost;
			if (m_dTotalEffectiveSale != 0.0)
			{
				BenefitMargin = m_dIndustrialBenefit / m_dTotalEffectiveSale * 100.0;
			}
			else
			{
				BenefitMargin = 0.0;
			}
			ChangedByUser = changedByUser;
			break;
		case "TotalSale":
			ChangedByUser = false;
			TotalEffectiveSale = m_dTotalSale * (100.0 - m_dSaleDiscount) / 100.0;
			foreach (PrefProjectExpenditure listExpenditure2 in m_listExpenditures)
			{
				listExpenditure2.Source = TotalEffectiveSale;
				if (listExpenditure2.Key != "Total")
				{
					num3 += listExpenditure2.Result;
					num += listExpenditure2.CoefficientAsFactor;
				}
			}
			IndustrialBenefit = m_dTotalEffectiveSale - num3 - m_dTotalEffectiveCost;
			if (m_dTotalEffectiveSale != 0.0)
			{
				BenefitMargin = m_dIndustrialBenefit / m_dTotalEffectiveSale * 100.0;
			}
			else
			{
				BenefitMargin = 0.0;
			}
			ChangedByUser = changedByUser;
			if (!ChangedByUser)
			{
				break;
			}
			ChangedByUser = false;
			dTotalSale = 0.0;
			foreach (PrefProjectPerGroupExpenditure perGroupExpenditure in PerGroupExpenditures)
			{
				if (perGroupExpenditure.EffectiveCost > 0.0 && !perGroupExpenditure.IsWithoutGroup)
				{
					num2 += perGroupExpenditure.Sale;
				}
				dTotalSale += perGroupExpenditure.Sale;
			}
			num4 = dTotalSale - num2;
			foreach (PrefProjectPerGroupExpenditure perGroupExpenditure2 in PerGroupExpenditures)
			{
				if (!(perGroupExpenditure2.EffectiveCost > 0.0) || perGroupExpenditure2.IsWithoutGroup || !perGroupExpenditure2.IsSalesIncrementEditable)
				{
					continue;
				}
				double num10 = 0.0;
				double num11 = perGroupExpenditure2.EffectiveCost;
				if (num2 != 0.0)
				{
					double num12 = perGroupExpenditure2.Sale;
					if (perGroupExpenditure2.ListColorsRedefined != null)
					{
						foreach (PrefProjectPerGroupExpenditure item in perGroupExpenditure2.ListColorsRedefined)
						{
							if (!item.IsSalesIncrementEditable)
							{
								num12 += item.Sale;
								num11 += item.EffectiveCost;
							}
						}
					}
					num10 = num12 / num2;
				}
				else
				{
					if (perGroupExpenditure2.ListColorsRedefined != null)
					{
						foreach (PrefProjectPerGroupExpenditure item2 in perGroupExpenditure2.ListColorsRedefined)
						{
							if (!item2.IsSalesIncrementEditable)
							{
								num11 += item2.EffectiveCost;
							}
						}
					}
					num10 = num11 / m_dTotalEffectiveCost;
				}
				double num13 = (TotalSale - num4) * num10;
				perGroupExpenditure2.CoefficientAsFactor = num13 / num11;
				if (perGroupExpenditure2.ListColorsRedefined == null)
				{
					continue;
				}
				foreach (PrefProjectPerGroupExpenditure item3 in perGroupExpenditure2.ListColorsRedefined)
				{
					if (!item3.IsSalesIncrementEditable)
					{
						item3.CoefficientAsFactor = perGroupExpenditure2.CoefficientAsFactor;
					}
				}
			}
			ChangedByUser = changedByUser;
			break;
		case "TotalEffectiveSale":
			if (ChangedByUser)
			{
				SaleDiscount = 100.0 - 100.0 * TotalEffectiveSale / TotalSale;
			}
			ChangedByUser = false;
			foreach (PrefProjectExpenditure listExpenditure3 in m_listExpenditures)
			{
				listExpenditure3.Source = TotalEffectiveSale;
				if (listExpenditure3.Key != "Total")
				{
					num3 += listExpenditure3.Result;
					num += listExpenditure3.CoefficientAsFactor;
				}
			}
			IndustrialBenefit = m_dTotalEffectiveSale - num3 - m_dTotalEffectiveCost;
			if (m_dTotalEffectiveSale != 0.0)
			{
				BenefitMargin = m_dIndustrialBenefit / m_dTotalEffectiveSale * 100.0;
			}
			else
			{
				BenefitMargin = 0.0;
			}
			ChangedByUser = changedByUser;
			break;
		case "BenefitMargin":
		{
			if (!ChangedByUser)
			{
				break;
			}
			foreach (PrefProjectExpenditure listExpenditure4 in m_listExpenditures)
			{
				if (listExpenditure4.Key != "Total")
				{
					num += listExpenditure4.CoefficientAsFactor;
				}
			}
			double num5 = 1.0 - m_dBenefitMargin * 0.01 - num;
			if (num5 == 0.0)
			{
				num5 = 0.001;
			}
			double num6 = m_dTotalEffectiveCost / num5;
			double num7 = num6 * 100.0 / (100.0 - m_dSaleDiscount);
			dTotalSale = 0.0;
			num2 = 0.0;
			foreach (PrefProjectPerGroupExpenditure perGroupExpenditure3 in PerGroupExpenditures)
			{
				if (perGroupExpenditure3.EffectiveCost > 0.0 && !perGroupExpenditure3.IsWithoutGroup)
				{
					num2 += perGroupExpenditure3.Sale;
				}
				dTotalSale += perGroupExpenditure3.Sale;
			}
			num4 = dTotalSale - num2;
			ChangedByUser = true;
			foreach (PrefProjectPerGroupExpenditure perGroupExpenditure4 in PerGroupExpenditures)
			{
				if (perGroupExpenditure4.EffectiveCost > 0.0 && !perGroupExpenditure4.IsWithoutGroup)
				{
					double num8 = 0.0;
					num8 = ((num2 == 0.0) ? (perGroupExpenditure4.EffectiveCost / m_dTotalEffectiveCost) : (perGroupExpenditure4.Sale / num2));
					double num9 = (num7 - num4) * num8;
					perGroupExpenditure4.CoefficientAsFactor = num9 / perGroupExpenditure4.EffectiveCost;
				}
			}
			ChangedByUser = changedByUser;
			break;
		}
		}
		if (this.PropertyChanged != null)
		{
			this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
		}
	}

	internal void Reset()
	{
		PerGroupExpenditures.CollectionStatus = enStatus.Unchanged;
		foreach (PrefProjectPerGroupExpenditure perGroupExpenditure in PerGroupExpenditures)
		{
			perGroupExpenditure.ProviderDiscountAsPercentage = 0.0;
			perGroupExpenditure.CoefficientAsPercentage = 0.0;
			perGroupExpenditure.RemnantIncrementAsPercentage = 0.0;
		}
	}
}
