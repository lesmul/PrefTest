using System;

namespace Preference.Purchases.MRP;

public struct ReferenceToBuy
{
	public string Reference { get; set; }

	public int ColorConfiguration { get; set; }

	public DateTime ControlDate { get; set; }

	public double RodLength { get; set; }

	public double SurfaceHeight { get; set; }

	public double Quantity { get; set; }

	public int ProviderCode { get; set; }

	public int WarehouseCode { get; set; }

	public string GlassId { get; set; }

	public Guid MaterialNeedId { get; set; }
}
