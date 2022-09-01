namespace Interop.PrefSales;

public enum __MIDL___MIDL_itf_PrefSales_0001_0024_0008
{
	astSimple,
	astExtended
}
public enum __MIDL___MIDL_itf_PrefSales_0001_0024_0004
{
	SDState_Empty,
	SDState_Loaded,
	SDState_Modified,
	SDState_Deprecated,
	SDState_Saving,
	SDState_OnlyMasterDataLoaded
}
public enum __MIDL___MIDL_itf_PrefSales_0001_0024_0005
{
	SDT_Error,
	SDT_Offer,
	SDT_Order,
	SDT_DeliveryNote,
	SDT_Invoice,
	SDT_SubOrder
}
public enum __MIDL___MIDL_itf_PrefSales_0001_0024_0011
{
	SDIState_NotModified,
	SDIState_Modified,
	SDIState_Inserted
}
public enum __MIDL___MIDL_itf_PrefSales_0001_0024_0012
{
	SDIType_UserDefined,
	SDIType_Material,
	SDIType_Model,
	SDIType_Script,
	SDIType_Information,
	SDIType_WebItem,
	SDIType_Indirection,
	SDIType_Custom,
	SDIType_Delta
}
public enum __MIDL___MIDL_itf_PrefSales_0001_0024_0014
{
	SDIEType_None,
	SDIEType_Percentage,
	SDIEType_UnitPrice
}
public enum __MIDL___MIDL_itf_PrefSales_0001_0024_0013
{
	SDIJITMVType_Simple,
	SDIJITMVType_SimplePlusDelta,
	SDIJITMVType_Delta
}
public enum __MIDL___MIDL_itf_PrefSales_0001_0024_0010
{
	SDSK_Error,
	SDSK_IncrementPercentage,
	SDSK_IncrementFixedAmount,
	SDSK_DiscountPercentage,
	SDSK_DiscountFixedAmount,
	SDSK_SubTotal,
	SDSK_CustomerDiscount1,
	SDSK_CustomerDiscount2,
	SDSK_AdvancedPayment
}
public enum __MIDL___MIDL_itf_PrefSales_0001_0024_0006
{
	SOT_Error,
	SOT_Production,
	SOT_Measurement,
	SOT_Mounting
}
public enum __MIDL___MIDL_itf_PrefSales_0001_0024_0009
{
	tnConfirmation = 0,
	tnOrderValidation = 1,
	tnSecurityTime = 2,
	tnPurchases = 3,
	tnProduction = 4,
	tnShippingPreparation = 5,
	tnShipping = 6,
	tnDelivery = 7,
	tnInstallation = 8,
	tnMeasurement = 9,
	tnProductionPreparation = 10,
	tnLastDate = 10
}
public enum __MIDL___MIDL_itf_PrefSales_0001_0024_0015
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
public enum __MIDL___MIDL_itf_PrefSales_0001_0024_0002
{
	RT_CANCEL = 0,
	RT_OK = 1,
	RT_ERROR = -1
}
public enum __MIDL___MIDL_itf_PrefSales_0001_0024_0003
{
	TM_NEVER = 0,
	TM_TRANSLATION = 1,
	TM_RESOURCE = 3
}
public enum __MIDL___MIDL_itf_PrefSales_0001_0024_0001
{
	EL_NONE = 0,
	EL_AFRIKAANS = 1078,
	EL_ALBANIAN = 1052,
	EL_ARABIC_Saudi_Arabia = 1025,
	EL_ARABIC_Iraq = 2049,
	EL_ARABIC_Egypt = 3073,
	EL_ARABIC_Morocco = 6145,
	EL_BASQUE = 1069,
	EL_BYELORUSSIAN = 1059,
	EL_BULGARIAN = 1026,
	EL_CATALAN = 1027,
	EL_CHINESE_Taiwan = 1028,
	EL_CROATIAN = 1050,
	EL_CZECH = 1029,
	EL_DANISH = 1030,
	EL_DUTCH_Standard = 1043,
	EL_DUTCH_Belgium = 2067,
	EL_ENGLISH_US = 1033,
	EL_ENGLISH_UK = 2057,
	EL_ENGLISH_Canada = 4105,
	EL_ESTONIAN = 1061,
	EL_FAROESE = 1080,
	EL_FARSI = 1065,
	EL_FINNISH = 1035,
	EL_FRENCH_Standard = 1036,
	EL_FRENCH_Belgium = 2060,
	EL_FRENCH_Switzerland = 4108,
	EL_FRENCH_Canada = 3084,
	EL_GERMAN_Standard = 1031,
	EL_GERMAN_Switzerland = 2055,
	EL_GERMAN_Austria = 3079,
	EL_GERMAN_Luxembourg = 4103,
	EL_GERMAN_Liechtenstein = 5127,
	EL_GREEK = 1032,
	EL_HEBREW = 1037,
	EL_HUNGARIAN = 1038,
	EL_ICELANDIC = 1039,
	EL_INDONESIAN = 1057,
	EL_ITALIAN = 1040,
	EL_ITALIAN_Switzerland = 2064,
	EL_JAPANASE = 1041,
	EL_KOREAN = 1042,
	EL_LATVIAN = 1062,
	EL_LITHUANIAN = 1063,
	EL_NORWEGIAN = 1044,
	EL_POLISH = 1045,
	EL_PORTUGUESE_Standard = 2070,
	EL_PORTUGUESE_Brazil = 1046,
	EL_ROMANIAN = 1048,
	EL_RUSSIAN = 1049,
	EL_SLOVAK = 1051,
	EL_SLOVENIAN = 1060,
	EL_SPANISH_Mexico = 2058,
	EL_SPANISH_Traditional_Sort = 1034,
	EL_SPANISH_Modern_Sort = 3082,
	EL_SWEDISH = 1053,
	EL_THAI = 1054,
	EL_TURKISH = 1055,
	EL_UKRAINIAN = 1058
}
public enum __MIDL___MIDL_itf_PrefSales_0001_0024_0007
{
	atNoAudit = 0,
	atAuditInsert = 1,
	atAuditSave = 2,
	atAuditLoad = 4,
	atAuditDelete = 8,
	atAuditAll = 15
}
