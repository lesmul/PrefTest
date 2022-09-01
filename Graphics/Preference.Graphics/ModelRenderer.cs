using System;
using System.Runtime.InteropServices;
using Interop.PrefCAD;
using Interop.PrefView;

namespace Preference.Graphics;

public class ModelRenderer
{
	private static Application _prefCad;

	public static string ConnectionString
	{
		set
		{
			SetPrefCadApplication(value);
		}
	}

	public static string GetSilverlightXaml(string strCodeModel)
	{
		IDualModelo dualModelo = (Modelo)Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("A08D8220-EC32-11CF-8C7E-00A0242924B1")));
		PrefModelRenderer prefModelRenderer = (PrefModelRenderer)Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("C530FCFA-D2F5-42D8-806A-67CBD25A9815")));
		prefModelRenderer.ConnectionString = _prefCad.ConnectionString;
		if (dualModelo.CargaModelo(strCodeModel))
		{
			string xMLCode = dualModelo.GetXMLCode(XMLOptionEnum.xmlFullModelFor2D);
			if (!string.IsNullOrEmpty(xMLCode))
			{
				prefModelRenderer.SetXMLDraw(xMLCode);
				return prefModelRenderer.GetWPF(WPFKind.wkSilverlight);
			}
		}
		return null;
	}

	private static void SetPrefCadApplication(string strConnectionString)
	{
		_prefCad = (Application)Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("369FC322-D0BA-11D2-86A2-0060081FCD9C")));
		_prefCad.ConnectionString = strConnectionString;
		_prefCad.Init();
	}
}
