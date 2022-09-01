using System;
using System.Data;
using System.Xml;
using Preference.Wpf.Controls.Projects.AppLogic;

namespace Preference.Wpf.Controls.Projects;

public interface IServiceAgent
{
	string AdoConnectionString { set; }

	long AddNewProject(long projectCode, string projectName);

	bool AddRelatedDocument2(long projectCode, string destDocumentId, int destDocumentType, string folderId);

	bool ExecuteCommand(string strCommandXml, ref string strResults, ref string strErrors);

	bool ExecuteXMLCommand(XmlDocument xmlCommand, ref XmlDocument xmlResults, ref XmlDocument xmlErrors);

	bool ExistsProjectCode(long projectCode);

	DataSet GetContactList();

	DataSet GetSalesDocumentsOfProject(Guid projectId);

	DataSet GetPurchasesDocumentsOfProject(Guid projectId);

	DataSet GetProductionLotDocumentsOfProject(Guid projectId);

	DataSet GetCustomerDocumentsOfProject(Guid projectId);

	DataSet GetExpenseDocumentsOfProject(Guid projectId);

	DataSet GetDocumentDocuments(Guid documentId, int documentTypeCode);

	DataSet GetDocumentDocuments(Guid documentId, int documentTypeCode, int destDocumentTypeCode);

	long GetNextProjectCode();

	long GetProjectFromRelatedDocument(string documentId, int destDocumentType);

	bool RemoveDocumentFromProject(string destDocumentId, long projectCode);

	void RemoveContactsOfProject(Guid projectId);

	DataSet GetCustomerListOrderByCustomerName();

	bool TryGetFirstSalesDocumentForValuation(Guid projectId, out SalesDocument salesDoc);
}
