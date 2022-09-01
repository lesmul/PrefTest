using System.Collections.Generic;
using System.Xml;

namespace Preference.WPF.MaterialsSelector.Core;

public interface IServiceAgent
{
	UnitsMode UnitsMode { get; set; }

	bool IsDocumentLoaded { get; }

	XmlDocument XmlDocument { get; }

	XmlNamespaceManager XmlNamespaceManager { get; }

	string ConnectionString { get; set; }

	string XmlDescriptive { get; set; }

	List<string> GetDummyReferences();

	string GetName();

	string GetModelStructureIDs();

	string GetSystem();

	string GetDescription();

	string GetColor();

	string GetFamily();

	string GetProductType();

	string GetWpfDrawing();

	string GetWpfDrawing(string itemID);

	byte[] GetImageAsPng(string reference, int width, int height);
}
