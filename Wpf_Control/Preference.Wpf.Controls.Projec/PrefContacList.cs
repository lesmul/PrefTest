using System.Data;

namespace Preference.Wpf.Controls.Projects.AppLogic;

public static class PrefContacList
{
	public static PrefCollection<Contact> Contacts;

	static PrefContacList()
	{
		Initialize();
	}

	public static void Initialize()
	{
		Contacts = new PrefCollection<Contact>();
		ServiceAgent serviceAgent = new ServiceAgent(Globals.ConnectionString);
		DataSet contactList = serviceAgent.GetContactList();
		foreach (DataRow row in contactList.Tables[0].Rows)
		{
			Contact item = PrefProject.DataRowToContact(row);
			Contacts.Add(item);
		}
	}
}
