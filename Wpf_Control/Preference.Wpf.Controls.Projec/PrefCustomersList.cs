using System;
using System.Data;

namespace Preference.Wpf.Controls.Projects.AppLogic;

public static class PrefCustomersList
{
	public static PrefCollection<Customer> Customers;

	static PrefCustomersList()
	{
		Initialize();
	}

	public static void Initialize()
	{
		Customers = new PrefCollection<Customer>();
		ServiceAgent serviceAgent = new ServiceAgent(Globals.ConnectionString);
		DataSet customerListOrderByCustomerName = serviceAgent.GetCustomerListOrderByCustomerName();
		foreach (DataRow row in customerListOrderByCustomerName.Tables[0].Rows)
		{
			Customer customer = new Customer();
			customer.CustomerCode = Convert.ToInt32(row["CustomerCode"]);
			if (!row.IsNull("CustomerName"))
			{
				customer.FullName = Convert.ToString(row["CustomerName"]).Trim();
			}
			if (!row.IsNull("MainAddress1"))
			{
				customer.Address = Convert.ToString(row["MainAddress1"]).Trim();
			}
			if (!row.IsNull("MainCity"))
			{
				customer.City = Convert.ToString(row["MainCity"]).Trim();
			}
			if (!row.IsNull("MainZipCode"))
			{
				customer.PostalCode = Convert.ToString(row["MainZipCode"]).Trim();
			}
			if (!row.IsNull("MainProvince"))
			{
				customer.Province = Convert.ToString(row["MainProvince"]).Trim();
			}
			if (!row.IsNull("MainCountry"))
			{
				customer.Country = Convert.ToString(row["MainCountry"]).Trim();
			}
			if (!row.IsNull("ShippingAddress1"))
			{
				customer.ShippingAddress.Address1 = Convert.ToString(row["ShippingAddress1"]).Trim();
			}
			if (!row.IsNull("ShippingAddress2"))
			{
				customer.ShippingAddress.Address2 = Convert.ToString(row["ShippingAddress2"]).Trim();
			}
			if (!row.IsNull("ShippingCity"))
			{
				customer.ShippingAddress.City = Convert.ToString(row["ShippingCity"]).Trim();
			}
			if (!row.IsNull("ShippingZipCode"))
			{
				customer.ShippingAddress.PostalCode = Convert.ToString(row["ShippingZipCode"]).Trim();
			}
			if (!row.IsNull("ShippingProvince"))
			{
				customer.ShippingAddress.Province = Convert.ToString(row["ShippingProvince"]).Trim();
			}
			if (!row.IsNull("ShippingCountry"))
			{
				customer.ShippingAddress.Country = Convert.ToString(row["ShippingCountry"]).Trim();
			}
			if (!row.IsNull("BillingAddress1"))
			{
				customer.InvoicingAddress.Address1 = Convert.ToString(row["BillingAddress1"]).Trim();
			}
			if (!row.IsNull("BillingAddress2"))
			{
				customer.InvoicingAddress.Address2 = Convert.ToString(row["BillingAddress2"]).Trim();
			}
			if (!row.IsNull("BillingCity"))
			{
				customer.InvoicingAddress.City = Convert.ToString(row["BillingCity"]).Trim();
			}
			if (!row.IsNull("BillingZipCode"))
			{
				customer.InvoicingAddress.PostalCode = Convert.ToString(row["BillingZipCode"]).Trim();
			}
			if (!row.IsNull("BillingProvince"))
			{
				customer.InvoicingAddress.Province = Convert.ToString(row["BillingProvince"]).Trim();
			}
			if (!row.IsNull("BillingCountry"))
			{
				customer.InvoicingAddress.Country = Convert.ToString(row["BillingCountry"]).Trim();
			}
			Customers.Add(customer);
		}
	}
}
