namespace Interop.PrefSales;

public enum SalesDocDeliveryDateChecking
{
	SDDDC_Ok,
	SDDDC_NonWorkingDay,
	SDDDC_LockedDay,
	SDDDC_NonDeliverableDay,
	SDDDC_ShippingAfterDelivery,
	SD2DDC_TaskBeforeConfirmation,
	SD2DDC_NoProductionCapacity,
	SD2DDC_NoInstallationCapacity,
	SD2DDC_TaskAlreadyDone
}
