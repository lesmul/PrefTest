using System;

namespace Preference.Purchases.MRP;

public struct PurchasesHeader
{
	public int Provider { get; set; }

	public DateTime ScheduledDate { get; set; }

	public DateTime EstimatedReceptionDate { get; set; }

	public int TargetLevel { get; set; }

	public short ToNegotiate { get; set; }
}
