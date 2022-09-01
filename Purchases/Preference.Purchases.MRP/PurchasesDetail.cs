using System;

namespace Preference.Purchases.MRP;

public class PurchasesDetail
{
	public string Reference { get; set; }

	public int ColorConfiguration { get; set; }

	public long Quantity { get; set; }

	public long PackingUnit1 { get; set; }

	public double PackingUnit2 { get; set; }

	public double TotalPacking { get; set; }

	public double PurchasePrice { get; set; }

	public int AverageDelivery { get; set; }

	public DateTime ScheduledPurchase { get; set; }

	public DateTime ScheduledDelivery { get; set; }

	public DateTime ConsumptionDate { get; set; }

	public int WarehouseCode { get; set; }

	public short ToNegotiate { get; set; }

	public double RodLength { get; set; }

	public double SurfaceHeight { get; set; }

	public double ItemKg { get; set; }

	public double TotalKg { get; set; }

	public string SupplierReference { get; set; }

	public DateTime EstimatedReceptionDate { get; set; }

	public double LengthToInvoice { get; set; }

	public double HeightToInvoice { get; set; }

	public double MinimumToInvoice { get; set; }

	public double TotalToInvoice { get; set; }

	public string GlassId { get; set; }

	public Guid MaterialNeedId { get; set; }

	public PurchasesDetail()
	{
		ToNegotiate = 0;
	}
}
