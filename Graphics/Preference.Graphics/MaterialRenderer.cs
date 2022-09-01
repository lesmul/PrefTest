using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using Interop.PrefView;
using Interop.PROFILEVIEWERSERVERLib;

namespace Preference.Graphics;

public class MaterialRenderer
{
	private static string _strConnectionString;

	public static string ConnectionString
	{
		set
		{
			_strConnectionString = value;
		}
	}

	public static void UpdateSilverlightXamlFromDXF(string strBaseReferenceMaterial)
	{
		PrefDXF obj = (PrefDXF)Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("D37BF4F0-0974-434A-80C2-1941E1521BE8")));
		obj.ConnectionString = _strConnectionString;
		obj.WhereSentence = $"ReferenciaBase = N'{strBaseReferenceMaterial}'";
		obj.UpdateSilverlightXamls();
	}

	public static string GetSilverlightXamlFromXmlDxf(string strXmlDxf)
	{
		if (string.IsNullOrEmpty(strXmlDxf))
		{
			return null;
		}
		string value = XElement.Load((TextReader)new StringReader(strXmlDxf)).Attribute("ID").Value;
		string xMLDraw = $"<Model xmlns:dsc='ModelDescriptive' xmlns:thd='Model3D'><dsc:Model><dsc:Defs><dsc:Profiles><dsc:Profile ref='{value}'> {strXmlDxf}</dsc:Profile></dsc:Profiles></dsc:Defs></dsc:Model></Model>";
		PrefModelRenderer obj = (PrefModelRenderer)Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("C530FCFA-D2F5-42D8-806A-67CBD25A9815")));
		obj.SetXMLDraw(xMLDraw);
		return obj.GetWPFMaterial(WPFKind.wkSilverlight, value);
	}
}
