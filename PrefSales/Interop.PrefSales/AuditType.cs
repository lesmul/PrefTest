namespace Interop.PrefSales;

public enum AuditType
{
	atNoAudit = 0,
	atAuditInsert = 1,
	atAuditSave = 2,
	atAuditLoad = 4,
	atAuditDelete = 8,
	atAuditAll = 15
}
