using System;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Interop.PrefCAD;
using Telerik.Windows.Zip;

namespace Preference.Logikal;

internal class DescriptiveXmlBuilder
{
	public string strXMLOriginalProfMem = "";

	public string ExportFilePath { get; set; }

	public XMLOptionEnum XmlOptions { get; set; }

	public bool GenerateLogFiles { get; set; }

	public string PrefSuiteConnectionString { get; set; }

	public int nGMid { get; set; }

	public int nRodid { get; set; }

	public int nPPid { get; set; }

	public int nGlassid { get; set; }

	public int nHoleid { get; set; }

	public int nSquareid { get; set; }

	public int nPhysicHoleid { get; set; }

	public int nDelimitationid { get; set; }

	public int nGlazingLedgeid { get; set; }

	public int nGasketId { get; set; }

	public double dGreaterProfilePieceX { get; set; }

	public double dGreaterProfilePieceY { get; set; }

	public double dLowerProfilePieceX { get; set; }

	public double dLowerProfilePieceY { get; set; }

	public string DescriptiveXml
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Expected O, but got Unknown
			//IL_018a: Unknown result type (might be due to invalid IL or missing references)
			GeneratedMaterials val = (GeneratedMaterials)new GeneratedMaterialsClass();
			XmlDocument xmlDesc = new XmlDocument();
			if (string.IsNullOrWhiteSpace(ExportFilePath))
			{
				throw new InvalidOperationException("The property ExportFilePath could not be emtpy.");
			}
			try
			{
				using OleDbConnection oleDbConnection = new OleDbConnection("PROVIDER=Microsoft.Jet.OLEDB.4.0;Data Source=" + ExportFilePath);
				oleDbConnection.Open();
				strXMLOriginalProfMem = GetProfMemXML(oleDbConnection);
				string xml = RemoveFirstLine(strXMLOriginalProfMem);
				string modelColor = GetModelColor(oleDbConnection);
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.LoadXml(xml);
				using DataSet dataSet = new DataSet();
				dataSet.Locale = CultureInfo.InvariantCulture;
				using (OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter("SELECT [Profiles].[ArticleCode] AS ReferenciaFinal, [Profiles].[Amount] AS Cantidad, [Profiles].[Length_Output] AS Longitud, [Profiles].[CutLeft] AS AnguloA, [Profiles].[CutRight] AS AnguloB, [Profiles].[Color] AS Color, [Profiles].[Weight_Output] AS Peso, InsertionId, AssemblyInfo, AssemblyInfoNo, xSashGUID, xGUID, PieceListType, PieceId,JobId, InnerColorTypeInternal, OuterColorTypeInternal FROM Profiles ORDER BY [Profiles].[ArticleCode]", oleDbConnection))
				{
					oleDbDataAdapter.Fill(dataSet, "Perfiles");
					InsertMaterialsToMaterialList(dataSet, "Perfil", val);
				}
				using (OleDbDataAdapter oleDbDataAdapter2 = new OleDbDataAdapter("SELECT [Articles].[ArticleCode] AS ReferenciaFinal, [Articles].[Amount] AS Cantidad, [Articles].[Length_Output] AS Longitud, [Articles].[Color] AS Color, Weight, ArticleType, InsertionId, xSashGUID, PieceListType FROM Articles ORDER BY [Articles].[ArticleCode]", oleDbConnection))
				{
					oleDbDataAdapter2.Fill(dataSet, "Articulos");
					InsertMaterialsToMaterialList(dataSet, "Articulo", val);
				}
				using (OleDbDataAdapter oleDbDataAdapter3 = new OleDbDataAdapter("SELECT InsertionId, Name, NameShort, xGUID, Amount AS Cantidad, Width, Height, Width_Output, Height_Output, Perimeter_Output FROM Glass", oleDbConnection))
				{
					oleDbDataAdapter3.Fill(dataSet, "Glass");
					InsertMaterialsToMaterialList(dataSet, "Vidrio", val);
				}
				using (OleDbDataAdapter oleDbDataAdapter4 = new OleDbDataAdapter("SELECT xGUID, InsertionID, ElevationId, InsertionNo, TypeNr, DIN, Width_Output, Height_Output FROM Insertions", oleDbConnection))
				{
					oleDbDataAdapter4.Fill(dataSet, "Insertions");
				}
				using (OleDbDataAdapter oleDbDataAdapter5 = new OleDbDataAdapter("SELECT InsertionId, xGUID, Width_Output, Height_Output, SliderDirection, DIN, MasterLeaf, SlaveLeaf From Sashes", oleDbConnection))
				{
					oleDbDataAdapter5.Fill(dataSet, "Sashes");
				}
				XmlDocument xmlDocument2 = new XmlDocument();
				xmlDocument2.LoadXml(((IGeneratedMaterials)val).GetXMLCode(XmlOptions).ToString());
				using (OleDbDataAdapter oleDbDataAdapter6 = new OleDbDataAdapter("SELECT Description FROM ReportOfferTexts", oleDbConnection))
				{
					oleDbDataAdapter6.Fill(dataSet, "ReportOfferTexts");
					xmlDesc = AddModelAttributes(dataSet, xmlDocument2, modelColor, xmlDocument);
				}
				nGMid = 0;
				nRodid = 0;
				nPPid = 0;
				nGlassid = 1;
				nHoleid = 0;
				nSquareid = 1;
				nDelimitationid = 0;
				nGlazingLedgeid = 0;
				nGasketId = 0;
				xmlDesc = DeleteModelGeneratedMaterials(xmlDesc);
				xmlDesc = AddModelContour(xmlDesc);
				xmlDesc = AddContourData(xmlDesc);
				xmlDesc = AddContourHoleData(xmlDesc, dataSet, xmlDocument);
				xmlDesc = AddHoleData(xmlDesc, dataSet, xmlDocument);
				XmlNodeList xmlNodeList = xmlDocument.SelectNodes("PROFMEMBASE/PROFMEM/PROFMEM");
				if (xmlNodeList.Count > 0)
				{
					xmlDesc = AddPartitionHoles(xmlDesc, xmlNodeList, dataSet, xmlDocument);
				}
				if (GenerateLogFiles)
				{
					string destFileName = Path.GetTempPath() + "Position.mdb";
					string filename = Path.GetTempPath() + "ProfMem.xml";
					string filename2 = Path.GetTempPath() + "Descriptive.xml";
					File.Copy(ExportFilePath, destFileName, overwrite: true);
					xmlDocument.Save(filename);
					xmlDesc.Save(filename2);
				}
				return xmlDesc.InnerXml.ToString();
			}
			catch
			{
				return "<Model xmlns:dsc='ModelDescriptive'><dsc:Model prefSuiteItem='custom' name='Logikal' description='Item from LogiKal'/></Model>";
			}
			finally
			{
				Marshal.ReleaseComObject(val);
			}
		}
	}

	public DescriptiveXmlBuilder()
	{
		XmlOptions = (XMLOptionEnum)0;
	}

	private void InsertMaterialsToMaterialList(DataSet DataSet, string strMaterialType, GeneratedMaterials listGm)
	{
		switch (strMaterialType)
		{
		case "Perfil":
		{
			foreach (DataRow row in DataSet.Tables["Perfiles"].Rows)
			{
				GeneratedMaterial val3 = ((IGeneratedMaterials)listGm).AddMaterial(row["ReferenciaFinal"].ToString().TrimEnd(), (Elemento)null);
				try
				{
					((IGeneratedMaterial)val3).set_Quantity((int)row["Cantidad"]);
					((IGeneratedMaterial)val3).set_AngleA((double)row["AnguloA"]);
					((IGeneratedMaterial)val3).set_AngleB((double)row["AnguloB"]);
					((IGeneratedMaterial)val3).set_Length((double)row["Longitud"]);
				}
				finally
				{
					Marshal.ReleaseComObject(val3);
				}
			}
			break;
		}
		case "Articulo":
		{
			foreach (DataRow row2 in DataSet.Tables["Articulos"].Rows)
			{
				GeneratedMaterial val2 = ((IGeneratedMaterials)listGm).AddMaterial(row2["ReferenciaFinal"].ToString().TrimEnd(), (Elemento)null);
				try
				{
					((IGeneratedMaterial)val2).set_Quantity((int)row2["Cantidad"]);
					if (!string.IsNullOrEmpty(row2["Longitud"].ToString()))
					{
						((IGeneratedMaterial)val2).set_Length((double)row2["Longitud"] * 1000.0);
					}
				}
				finally
				{
					Marshal.ReleaseComObject(val2);
				}
			}
			break;
		}
		case "Vidrio":
		{
			string text = "";
			{
				foreach (DataRow row3 in DataSet.Tables["Glass"].Rows)
				{
					text = GetTranslateGlassName(row3["Name"].ToString());
					GeneratedMaterial val = ((IGeneratedMaterials)listGm).AddMaterial(text.TrimEnd(), (Elemento)null);
					try
					{
						((IGeneratedMaterial)val).set_Quantity((int)row3["Cantidad"]);
						if (!string.IsNullOrEmpty(row3["Width_Output"].ToString()))
						{
							((IGeneratedMaterial)val).set_Length((double)row3["Width_Output"]);
						}
						((IGeneratedMaterial)val).set_height((double)row3["Height_Output"]);
					}
					finally
					{
						Marshal.ReleaseComObject(val);
					}
				}
				break;
			}
		}
		}
	}

	private string GetTranslateGlassName(string strLogikalGlassName)
	{
		string text = "";
		using (OleDbConnection oleDbConnection = new OleDbConnection(PrefSuiteConnectionString))
		{
			oleDbConnection.Open();
			using OleDbCommand oleDbCommand = new OleDbCommand("SELECT ReferenciaBase FROM MaterialesBaseOrg WHERE ReferenciaBaseExt = N'" + strLogikalGlassName + "'", oleDbConnection);
			OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
			if (oleDbDataReader.HasRows)
			{
				while (oleDbDataReader.Read())
				{
					text = oleDbDataReader[0].ToString();
				}
			}
		}
		if (string.IsNullOrEmpty(text))
		{
			return strLogikalGlassName;
		}
		return text;
	}

	private static XmlDocument DeleteModelGeneratedMaterials(XmlDocument xmlDesc)
	{
		try
		{
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDesc.NameTable);
			string uri = "ModelDescriptive";
			xmlNamespaceManager.AddNamespace("dsc", uri);
			foreach (XmlNode item in xmlDesc.SelectNodes("Model/dsc:Model/dsc:GeneratedMaterial", xmlNamespaceManager))
			{
				item.ParentNode.RemoveChild(item);
			}
			return xmlDesc;
		}
		catch
		{
			return xmlDesc;
		}
	}

	private static XmlDocument AddModelAttributes(DataSet DataSet, XmlDocument xmlDesc, string strModelColor, XmlDocument xmlProfMem)
	{
		try
		{
			new XmlDocument();
			return AddModelDimensions(AddModelDrawBrush(AddModelDrawPen(AddModelprocessingType(AddModelVisualFlags(AddModelPrefSuiteItem(AddModelColor(AddModelDescription(DataSet, xmlDesc), strModelColor)))))), xmlProfMem);
		}
		catch
		{
			return xmlDesc;
		}
	}

	private static XmlDocument AddModelDescription(DataSet DataSet, XmlDocument xmlDesc)
	{
		try
		{
			string text = "";
			foreach (DataRow row in DataSet.Tables["ReportOfferTexts"].Rows)
			{
				text = row["Description"].ToString();
			}
			if (text != null && File.Exists(text))
			{
				using (RichTextBox richTextBox = new RichTextBox())
				{
					string text3 = (richTextBox.Rtf = File.ReadAllText(text));
					string text4 = richTextBox.Text;
					string[] separator = new string[1] { "\n" };
					string[] array = text4.Split(separator, StringSplitOptions.None);
					string text5 = "";
					text5 = ((array[0].Length <= 100) ? array[0] : array[0].Substring(0, 100));
					XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDesc.NameTable);
					string uri = "ModelDescriptive";
					xmlNamespaceManager.AddNamespace("dsc", uri);
					XmlElement obj = (XmlElement)xmlDesc.SelectSingleNode("Model/dsc:Model", xmlNamespaceManager);
					XmlAttribute xmlAttribute = xmlDesc.CreateAttribute("description");
					xmlAttribute.Value = text5;
					obj.SetAttributeNode(xmlAttribute);
				}
				return xmlDesc;
			}
			return xmlDesc;
		}
		catch
		{
			return xmlDesc;
		}
	}

	private static XmlDocument AddModelColor(XmlDocument xmlDesc, string strModelColor)
	{
		try
		{
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDesc.NameTable);
			string uri = "ModelDescriptive";
			xmlNamespaceManager.AddNamespace("dsc", uri);
			XmlElement obj = (XmlElement)xmlDesc.SelectSingleNode("Model/dsc:Model", xmlNamespaceManager);
			XmlAttribute xmlAttribute = xmlDesc.CreateAttribute("color");
			xmlAttribute.Value = strModelColor;
			obj.SetAttributeNode(xmlAttribute);
			return xmlDesc;
		}
		catch
		{
			return xmlDesc;
		}
	}

	private static XmlDocument AddModelDrawPen(XmlDocument xmlDesc)
	{
		try
		{
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDesc.NameTable);
			string uri = "ModelDescriptive";
			xmlNamespaceManager.AddNamespace("dsc", uri);
			xmlNamespaceManager.AddNamespace("draw", "ModelDraw");
			XmlElement obj = (XmlElement)xmlDesc.SelectSingleNode("Model/dsc:Model", xmlNamespaceManager);
			XmlNode xmlNode = xmlDesc.CreateNode(XmlNodeType.Element, "draw:Pen", "ModelDraw");
			XmlAttribute xmlAttribute = xmlDesc.CreateAttribute("rgb");
			xmlAttribute.Value = "000000";
			xmlNode.Attributes.Append(xmlAttribute);
			XmlAttribute xmlAttribute2 = xmlDesc.CreateAttribute("width");
			xmlAttribute2.Value = "1";
			xmlNode.Attributes.Append(xmlAttribute2);
			XmlAttribute xmlAttribute3 = xmlDesc.CreateAttribute("alpha");
			xmlAttribute3.Value = "1";
			xmlNode.Attributes.Append(xmlAttribute3);
			XmlAttribute xmlAttribute4 = xmlDesc.CreateAttribute("type");
			xmlAttribute4.Value = "solid";
			xmlNode.Attributes.Append(xmlAttribute4);
			obj.AppendChild(xmlNode);
			return xmlDesc;
		}
		catch
		{
			return xmlDesc;
		}
	}

	private static XmlDocument AddModelDimensions(XmlDocument xmlDesc, XmlDocument xmlProfMem)
	{
		try
		{
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDesc.NameTable);
			string uri = "ModelDescriptive";
			xmlNamespaceManager.AddNamespace("dsc", uri);
			xmlNamespaceManager.AddNamespace("draw", "ModelDraw");
			XmlElement xmlElement = (XmlElement)xmlDesc.SelectSingleNode("Model/dsc:Model", xmlNamespaceManager);
			XmlNode xmlNode = xmlDesc.CreateNode(XmlNodeType.Element, "dsc:Dimensions", "ModelDescriptive");
			int num = 0;
			int num2 = 1;
			XmlNodeList xmlNodeList = xmlProfMem.SelectNodes("PROFMEMBASE/PROFMEM/MEASUREMENTS/MEASUREMENT[PROXYMEASURE/LABEL/@DIRECTIONX > 0]");
			foreach (XmlNode item in xmlNodeList)
			{
				if (xmlNodeList.Count == 1 || num2 <= xmlNodeList.Count - 1)
				{
					if (num == 0)
					{
						XmlNode xmlNodeProfMemMeasurement2 = xmlNodeList[xmlNodeList.Count - 1];
						XmlNode xmlNodeDimension = xmlDesc.CreateNode(XmlNodeType.Element, "dsc:Dimension", "ModelDescriptive");
						xmlNode.AppendChild(AddDimensionData(xmlDesc, xmlNodeDimension, "E" + num.ToString(CultureInfo.InvariantCulture), xmlNodeProfMemMeasurement2, "Horizontal"));
						num++;
					}
					if (xmlNodeList.Count > 1)
					{
						xmlDesc.CreateNode(XmlNodeType.Element, "dsc:SubDimension", "ModelDescriptive");
						XmlNode xmlNodeDimension2 = xmlNode.SelectSingleNode("dsc:Dimension[@name='L']", xmlNamespaceManager);
						XmlNode xmlNodeDrawDimension = xmlNode.SelectSingleNode("dsc:Dimension[@name='L']/draw:Dimension", xmlNamespaceManager);
						xmlNode.AppendChild(AddSubDimensionData(xmlDesc, xmlNodeDimension2, xmlNodeDrawDimension, num2.ToString(CultureInfo.InvariantCulture), item, "Horizontal"));
						num2++;
					}
				}
			}
			num2 = 1;
			XmlNodeList xmlNodeList2 = xmlProfMem.SelectNodes("PROFMEMBASE/PROFMEM/MEASUREMENTS/MEASUREMENT[PROXYMEASURE/LABEL/@DIRECTIONY > 0]");
			foreach (XmlNode item2 in xmlNodeList2)
			{
				if (xmlNodeList2.Count == 1 || num2 <= xmlNodeList2.Count - 1)
				{
					if (num == 1)
					{
						XmlNode xmlNodeProfMemMeasurement4 = xmlNodeList2[xmlNodeList2.Count - 1];
						XmlNode xmlNodeDimension3 = xmlDesc.CreateNode(XmlNodeType.Element, "dsc:Dimension", "ModelDescriptive");
						xmlNode.AppendChild(AddDimensionData(xmlDesc, xmlNodeDimension3, "E" + num.ToString(CultureInfo.InvariantCulture), xmlNodeProfMemMeasurement4, "Vertical"));
						num++;
					}
					if (xmlNodeList2.Count > 1)
					{
						xmlDesc.CreateNode(XmlNodeType.Element, "dsc:SubDimension", "ModelDescriptive");
						XmlNode xmlNodeDimension4 = xmlNode.SelectSingleNode("dsc:Dimension[@name='A']", xmlNamespaceManager);
						XmlNode xmlNodeDrawDimension2 = xmlNode.SelectSingleNode("dsc:Dimension[@name='A']/draw:Dimension", xmlNamespaceManager);
						xmlNode.AppendChild(AddSubDimensionData(xmlDesc, xmlNodeDimension4, xmlNodeDrawDimension2, num2.ToString(CultureInfo.InvariantCulture), item2, "Vertical"));
						num2++;
					}
				}
			}
			xmlElement.AppendChild(xmlNode);
			return xmlDesc;
		}
		catch
		{
			return xmlDesc;
		}
	}

	private static XmlNode AddDimensionData(XmlDocument xmlDesc, XmlNode xmlNodeDimension, string strDimensionId, XmlNode xmlNodeProfMemMeasurement, string strOrientation)
	{
		try
		{
			string text = "";
			text = ((!(strOrientation == "Horizontal")) ? "A" : "L");
			string value = ((XmlElement)xmlNodeProfMemMeasurement).GetAttribute("VALUE").ToString();
			XmlAttribute xmlAttribute = xmlDesc.CreateAttribute("name");
			xmlAttribute.Value = text;
			xmlNodeDimension.Attributes.Append(xmlAttribute);
			XmlAttribute xmlAttribute2 = xmlDesc.CreateAttribute("id");
			xmlAttribute2.Value = strDimensionId;
			xmlNodeDimension.Attributes.Append(xmlAttribute2);
			XmlAttribute xmlAttribute3 = xmlDesc.CreateAttribute("orientation");
			xmlAttribute3.Value = strOrientation;
			xmlNodeDimension.Attributes.Append(xmlAttribute3);
			XmlAttribute xmlAttribute4 = xmlDesc.CreateAttribute("value");
			xmlAttribute4.Value = value;
			xmlNodeDimension.Attributes.Append(xmlAttribute4);
			XmlAttribute xmlAttribute5 = xmlDesc.CreateAttribute("visible");
			xmlAttribute5.Value = "1";
			xmlNodeDimension.Attributes.Append(xmlAttribute5);
			XmlAttribute xmlAttribute6 = xmlDesc.CreateAttribute("constructive");
			xmlAttribute6.Value = "1";
			xmlNodeDimension.Attributes.Append(xmlAttribute6);
			XmlAttribute xmlAttribute7 = xmlDesc.CreateAttribute("level");
			xmlAttribute7.Value = "1";
			xmlNodeDimension.Attributes.Append(xmlAttribute7);
			XmlNode xmlNode = xmlDesc.CreateNode(XmlNodeType.Element, "SEGMENT2D", "");
			XmlNode xmlNode2 = xmlNodeProfMemMeasurement.SelectSingleNode("DIMENSIONLINE_P1");
			XmlNode xmlNode3 = xmlNodeProfMemMeasurement.SelectSingleNode("DIMENSIONLINE_P2");
			XmlElement xmlElement = (XmlElement)xmlNode2;
			XmlElement obj = (XmlElement)xmlNode3;
			string text2 = xmlElement.GetAttribute("X").ToString();
			string text3 = xmlElement.GetAttribute("Y").ToString();
			string text4 = obj.GetAttribute("X").ToString();
			string text5 = obj.GetAttribute("Y").ToString();
			XmlAttribute xmlAttribute8 = xmlDesc.CreateAttribute("ax");
			xmlAttribute8.Value = text2;
			xmlNode.Attributes.Append(xmlAttribute8);
			XmlAttribute xmlAttribute9 = xmlDesc.CreateAttribute("ay");
			xmlAttribute9.Value = text3;
			xmlNode.Attributes.Append(xmlAttribute9);
			XmlAttribute xmlAttribute10 = xmlDesc.CreateAttribute("bx");
			xmlAttribute10.Value = text4;
			xmlNode.Attributes.Append(xmlAttribute10);
			XmlAttribute xmlAttribute11 = xmlDesc.CreateAttribute("by");
			xmlAttribute11.Value = text5;
			xmlNode.Attributes.Append(xmlAttribute11);
			xmlNodeDimension.AppendChild(xmlNode);
			XmlNode xmlNode4 = xmlDesc.CreateNode(XmlNodeType.Element, "draw:Dimension", "ModelDraw");
			XmlAttribute xmlAttribute12 = xmlDesc.CreateAttribute("order");
			xmlAttribute12.Value = "1E+099";
			xmlNode4.Attributes.Append(xmlAttribute12);
			xmlNode4 = AddNodePolyline2DToDrawDimension(xmlNodeProfMemMeasurement, xmlDesc, xmlNode4);
			xmlNode4 = AddNodeDrawTextToDrawDimension(xmlNodeProfMemMeasurement, xmlDesc, xmlNode4, strOrientation, text2, text3, text4, text5);
			xmlNodeDimension.AppendChild(xmlNode4);
			return xmlNodeDimension;
		}
		catch
		{
			return xmlNodeDimension;
		}
	}

	private static XmlNode AddSubDimensionData(XmlDocument xmlDesc, XmlNode xmlNodeDimension, XmlNode xmlNodeDrawDimension, string strSubDimensionId, XmlNode xmlNodeProfMemMeasurement, string strOrientation)
	{
		try
		{
			string text = "";
			text = ((!(strOrientation == "Horizontal")) ? "A" : "L");
			string value = ((XmlElement)xmlNodeProfMemMeasurement).GetAttribute("VALUE").ToString();
			XmlNode xmlNode = xmlDesc.CreateNode(XmlNodeType.Element, "dsc:SubDimension", "ModelDescriptive");
			XmlAttribute xmlAttribute = xmlDesc.CreateAttribute("name");
			xmlAttribute.Value = text + strSubDimensionId;
			xmlNode.Attributes.Append(xmlAttribute);
			XmlAttribute xmlAttribute2 = xmlDesc.CreateAttribute("level");
			xmlAttribute2.Value = "1";
			xmlNode.Attributes.Append(xmlAttribute2);
			XmlAttribute xmlAttribute3 = xmlDesc.CreateAttribute("value");
			xmlAttribute3.Value = value;
			xmlNode.Attributes.Append(xmlAttribute3);
			XmlNode xmlNode2 = xmlDesc.CreateNode(XmlNodeType.Element, "SEGMENT2D", "");
			XmlNode xmlNode3 = xmlNodeProfMemMeasurement.SelectSingleNode("DIMENSIONLINE_P1");
			XmlNode xmlNode4 = xmlNodeProfMemMeasurement.SelectSingleNode("DIMENSIONLINE_P2");
			XmlElement xmlElement = (XmlElement)xmlNode3;
			XmlElement obj = (XmlElement)xmlNode4;
			string text2 = xmlElement.GetAttribute("X").ToString();
			string text3 = xmlElement.GetAttribute("Y").ToString();
			string text4 = obj.GetAttribute("X").ToString();
			string text5 = obj.GetAttribute("Y").ToString();
			XmlAttribute xmlAttribute4 = xmlDesc.CreateAttribute("ax");
			xmlAttribute4.Value = text2;
			xmlNode2.Attributes.Append(xmlAttribute4);
			XmlAttribute xmlAttribute5 = xmlDesc.CreateAttribute("ay");
			xmlAttribute5.Value = text3;
			xmlNode2.Attributes.Append(xmlAttribute5);
			XmlAttribute xmlAttribute6 = xmlDesc.CreateAttribute("bx");
			xmlAttribute6.Value = text4;
			xmlNode2.Attributes.Append(xmlAttribute6);
			XmlAttribute xmlAttribute7 = xmlDesc.CreateAttribute("by");
			xmlAttribute7.Value = text5;
			xmlNode2.Attributes.Append(xmlAttribute7);
			xmlNode.AppendChild(xmlNode2);
			xmlNodeDimension.AppendChild(xmlNode);
			xmlNodeDrawDimension = AddNodePolyline2DToDrawDimension(xmlNodeProfMemMeasurement, xmlDesc, xmlNodeDrawDimension);
			xmlNodeDrawDimension = AddNodeDrawTextToDrawDimension(xmlNodeProfMemMeasurement, xmlDesc, xmlNodeDrawDimension, strOrientation, text2, text3, text4, text5);
			return xmlNodeDimension;
		}
		catch
		{
			return xmlNodeDimension;
		}
	}

	private static XmlNode AddNodeDrawTextToDrawDimension(XmlNode xmlNodeProfMemMeasurement, XmlDocument xmlDesc, XmlNode xmlNodeDrawDimension, string strOrientation, string strAX, string strAY, string strBX, string strBY)
	{
		try
		{
			string text = "";
			string text2 = "";
			string text3 = "";
			string text4 = "";
			string text5 = "72";
			XmlElement obj = (XmlElement)xmlNodeProfMemMeasurement.SelectSingleNode("PROXYMEASURE/LABEL");
			text = obj.GetAttribute("POSITIONX").ToString();
			text2 = obj.GetAttribute("POSITIONY").ToString();
			text3 = obj.GetAttribute("CAPTION").ToString();
			text5 = obj.GetAttribute("SIZE").ToString();
			XmlNode xmlNode = xmlDesc.CreateNode(XmlNodeType.Element, "draw:Text", "ModelDraw");
			XmlAttribute xmlAttribute = xmlDesc.CreateAttribute("x");
			xmlAttribute.Value = text;
			xmlNode.Attributes.Append(xmlAttribute);
			XmlAttribute xmlAttribute2 = xmlDesc.CreateAttribute("y");
			xmlAttribute2.Value = text2;
			xmlNode.Attributes.Append(xmlAttribute2);
			text4 = ((!(strOrientation == "Horizontal")) ? "1.570796" : "0");
			XmlAttribute xmlAttribute3 = xmlDesc.CreateAttribute("angle");
			xmlAttribute3.Value = text4;
			xmlNode.Attributes.Append(xmlAttribute3);
			XmlAttribute xmlAttribute4 = xmlDesc.CreateAttribute("size");
			xmlAttribute4.Value = text5;
			xmlNode.Attributes.Append(xmlAttribute4);
			XmlAttribute xmlAttribute5 = xmlDesc.CreateAttribute("font-family");
			xmlAttribute5.Value = "Verdana";
			xmlNode.Attributes.Append(xmlAttribute5);
			XmlAttribute xmlAttribute6 = xmlDesc.CreateAttribute("align");
			xmlAttribute6.Value = "middle";
			xmlNode.Attributes.Append(xmlAttribute6);
			XmlAttribute xmlAttribute7 = xmlDesc.CreateAttribute("lineAlignment");
			xmlAttribute7.Value = "end";
			xmlNode.Attributes.Append(xmlAttribute7);
			XmlAttribute xmlAttribute8 = xmlDesc.CreateAttribute("scaleX");
			xmlAttribute8.Value = "1";
			xmlNode.Attributes.Append(xmlAttribute8);
			XmlAttribute xmlAttribute9 = xmlDesc.CreateAttribute("scaleY");
			xmlAttribute9.Value = "1";
			xmlNode.Attributes.Append(xmlAttribute9);
			xmlNode.InnerText = text3;
			XmlNode xmlNode2 = xmlDesc.CreateNode(XmlNodeType.Element, "draw:Brush", "ModelDraw");
			XmlAttribute xmlAttribute10 = xmlDesc.CreateAttribute("rgb");
			xmlAttribute10.Value = "000000";
			xmlNode2.Attributes.Append(xmlAttribute10);
			XmlAttribute xmlAttribute11 = xmlDesc.CreateAttribute("type");
			xmlAttribute11.Value = "solid";
			xmlNode2.Attributes.Append(xmlAttribute11);
			xmlNode.AppendChild(xmlNode2);
			XmlNode xmlNode3 = xmlDesc.CreateNode(XmlNodeType.Element, "draw:LineText", "ModelDraw");
			XmlAttribute xmlAttribute12 = xmlDesc.CreateAttribute("ax");
			xmlAttribute12.Value = strAX;
			xmlNode3.Attributes.Append(xmlAttribute12);
			XmlAttribute xmlAttribute13 = xmlDesc.CreateAttribute("ay");
			xmlAttribute13.Value = strAY;
			xmlNode3.Attributes.Append(xmlAttribute13);
			XmlAttribute xmlAttribute14 = xmlDesc.CreateAttribute("bx");
			xmlAttribute14.Value = strBX;
			xmlNode3.Attributes.Append(xmlAttribute14);
			XmlAttribute xmlAttribute15 = xmlDesc.CreateAttribute("by");
			xmlAttribute15.Value = strBY;
			xmlNode3.Attributes.Append(xmlAttribute15);
			xmlNode.AppendChild(xmlNode3);
			xmlNodeDrawDimension.AppendChild(xmlNode);
			return xmlNodeDrawDimension;
		}
		catch
		{
			return xmlNodeDrawDimension;
		}
	}

	private static XmlNode AddNodePolyline2DToDrawDimension(XmlNode xmlNodeProfMemMeasurement, XmlDocument xmlDesc, XmlNode xmlNodeDrawDimension)
	{
		try
		{
			foreach (XmlNode item in xmlNodeProfMemMeasurement.SelectNodes("PROXYMEASURE/LINE"))
			{
				string text = "";
				XmlNode xmlNode = xmlDesc.CreateNode(XmlNodeType.Element, "POLYLINE2D", "");
				XmlNode xmlNode2 = item.SelectSingleNode("VERTICES");
				XmlNode xmlNode3 = item.SelectSingleNode("VERTICES/VERTEX");
				XmlElement obj2 = (XmlElement)xmlNode2;
				string text2 = obj2.GetAttribute("SX").ToString();
				string text3 = obj2.GetAttribute("SY").ToString();
				text = "P" + text2 + " " + text3;
				XmlElement obj3 = (XmlElement)xmlNode3;
				text2 = obj3.GetAttribute("X").ToString();
				text3 = obj3.GetAttribute("Y").ToString();
				text = text + "P" + text2 + " " + text3;
				XmlAttribute xmlAttribute = xmlDesc.CreateAttribute("filled");
				xmlAttribute.Value = "0";
				xmlNode.Attributes.Append(xmlAttribute);
				XmlAttribute xmlAttribute2 = xmlDesc.CreateAttribute("path");
				xmlAttribute2.Value = text;
				xmlNode.Attributes.Append(xmlAttribute2);
				XmlNode xmlNode4 = xmlDesc.CreateNode(XmlNodeType.Element, "draw:Pen", "ModelDraw");
				XmlAttribute xmlAttribute3 = xmlDesc.CreateAttribute("width");
				xmlAttribute3.Value = "1";
				xmlNode4.Attributes.Append(xmlAttribute3);
				XmlAttribute xmlAttribute4 = xmlDesc.CreateAttribute("rgb");
				xmlAttribute4.Value = "000000";
				xmlNode4.Attributes.Append(xmlAttribute4);
				xmlNode.AppendChild(xmlNode4);
				xmlNodeDrawDimension.AppendChild(xmlNode);
			}
			return xmlNodeDrawDimension;
		}
		catch
		{
			return xmlNodeDrawDimension;
		}
	}

	private static XmlDocument AddModelDrawBrush(XmlDocument xmlDesc)
	{
		try
		{
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDesc.NameTable);
			string uri = "ModelDescriptive";
			xmlNamespaceManager.AddNamespace("dsc", uri);
			xmlNamespaceManager.AddNamespace("draw", "ModelDraw");
			XmlElement obj = (XmlElement)xmlDesc.SelectSingleNode("Model/dsc:Model", xmlNamespaceManager);
			XmlNode xmlNode = xmlDesc.CreateNode(XmlNodeType.Element, "draw:Brush", "ModelDraw");
			XmlAttribute xmlAttribute = xmlDesc.CreateAttribute("rgb");
			xmlAttribute.Value = "000000";
			xmlNode.Attributes.Append(xmlAttribute);
			XmlAttribute xmlAttribute2 = xmlDesc.CreateAttribute("type");
			xmlAttribute2.Value = "solid";
			xmlNode.Attributes.Append(xmlAttribute2);
			obj.AppendChild(xmlNode);
			return xmlDesc;
		}
		catch
		{
			return xmlDesc;
		}
	}

	private static XmlDocument AddModelPrefSuiteItem(XmlDocument xmlDesc)
	{
		try
		{
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDesc.NameTable);
			string uri = "ModelDescriptive";
			xmlNamespaceManager.AddNamespace("dsc", uri);
			XmlElement obj = (XmlElement)xmlDesc.SelectSingleNode("Model/dsc:Model", xmlNamespaceManager);
			XmlAttribute xmlAttribute = xmlDesc.CreateAttribute("prefSuiteItem");
			xmlAttribute.Value = "custom";
			obj.SetAttributeNode(xmlAttribute);
			return xmlDesc;
		}
		catch
		{
			return xmlDesc;
		}
	}

	private static XmlDocument AddModelVisualFlags(XmlDocument xmlDesc)
	{
		try
		{
			string text = "";
			text = "77353";
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDesc.NameTable);
			string uri = "ModelDescriptive";
			xmlNamespaceManager.AddNamespace("dsc", uri);
			XmlElement obj = (XmlElement)xmlDesc.SelectSingleNode("Model/dsc:Model", xmlNamespaceManager);
			XmlAttribute xmlAttribute = xmlDesc.CreateAttribute("VisualFlags");
			xmlAttribute.Value = text;
			obj.SetAttributeNode(xmlAttribute);
			return xmlDesc;
		}
		catch
		{
			return xmlDesc;
		}
	}

	private static XmlDocument AddModelprocessingType(XmlDocument xmlDesc)
	{
		try
		{
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDesc.NameTable);
			string uri = "ModelDescriptive";
			xmlNamespaceManager.AddNamespace("dsc", uri);
			XmlElement obj = (XmlElement)xmlDesc.SelectSingleNode("Model/dsc:Model", xmlNamespaceManager);
			XmlAttribute xmlAttribute = xmlDesc.CreateAttribute("processingType");
			xmlAttribute.Value = "assembled";
			obj.SetAttributeNode(xmlAttribute);
			return xmlDesc;
		}
		catch
		{
			return xmlDesc;
		}
	}

	private static XmlDocument AddModelContour(XmlDocument xmlDesc)
	{
		try
		{
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDesc.NameTable);
			string text = "ModelDescriptive";
			xmlNamespaceManager.AddNamespace("dsc", text);
			XmlElement xmlElement = (XmlElement)xmlDesc.SelectSingleNode("Model/dsc:Model", xmlNamespaceManager);
			XmlNode newChild = xmlDesc.CreateNode(XmlNodeType.Element, "dsc:Contour", text);
			xmlElement.AppendChild(newChild);
			xmlDesc.DocumentElement.AppendChild(xmlElement);
			return xmlDesc;
		}
		catch
		{
			return xmlDesc;
		}
	}

	private static XmlDocument AddNode(XmlDocument xmlDesc, string strNodeName, string strXPath)
	{
		try
		{
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDesc.NameTable);
			string text = "ModelDescriptive";
			xmlNamespaceManager.AddNamespace("dsc", text);
			XmlElement obj = (XmlElement)xmlDesc.SelectSingleNode(strXPath, xmlNamespaceManager);
			XmlNode newChild = xmlDesc.CreateNode(XmlNodeType.Element, strNodeName, text);
			obj.AppendChild(newChild);
			return xmlDesc;
		}
		catch
		{
			return xmlDesc;
		}
	}

	private XmlDocument AddGeneratedMaterials(XmlDocument xmlDesc, string strXPath, DataSet DataSet, string strNumeroInsertion, string strSashGUID, string strSquare)
	{
		try
		{
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDesc.NameTable);
			string text = "ModelDescriptive";
			xmlNamespaceManager.AddNamespace("dsc", text);
			XmlElement xmlElement = (XmlElement)xmlDesc.SelectSingleNode(strXPath, xmlNamespaceManager);
			string text2 = "";
			string text3 = "";
			string text4 = "";
			string text5 = "";
			string text6 = "";
			string text7 = "";
			string text8 = "";
			string text9 = "";
			string text10 = " AND (PieceListType <> 6 AND  PieceListType <> 7)";
			if (!string.IsNullOrEmpty(strSashGUID))
			{
				text9 = " AND xSashGUID='" + strSashGUID + "'";
			}
			DataRow[] array = DataSet.Tables["Articulos"].Select("InsertionId='" + strNumeroInsertion + "'" + text9 + text10);
			foreach (DataRow obj in array)
			{
				text2 = obj["ReferenciaFinal"].ToString();
				text3 = obj["Color"].ToString();
				text4 = obj["Longitud"].ToString();
				text7 = obj["ArticleType"].ToString();
				if (!string.IsNullOrEmpty(text4))
				{
					double num = Convert.ToDouble(text4, CultureInfo.CurrentCulture);
					text4 = (num * 1000.0).ToString(CultureInfo.CurrentCulture);
				}
				text5 = obj["Cantidad"].ToString();
				text6 = obj["Weight"].ToString();
				if (!string.IsNullOrEmpty(text6) && text6.Contains(','))
				{
					text6 = text6.Replace(',', '.');
				}
				XmlNode xmlNode = xmlDesc.CreateNode(XmlNodeType.Element, "dsc:GeneratedMaterial", text);
				XmlAttribute xmlAttribute = xmlDesc.CreateAttribute("id");
				xmlAttribute.Value = "GM" + nGMid.ToString(CultureInfo.CurrentCulture);
				xmlNode.Attributes.Append(xmlAttribute);
				XmlAttribute xmlAttribute2 = xmlDesc.CreateAttribute("material");
				xmlAttribute2.Value = text2;
				xmlNode.Attributes.Append(xmlAttribute2);
				XmlAttribute xmlAttribute3 = xmlDesc.CreateAttribute("color");
				xmlAttribute3.Value = text3;
				xmlNode.Attributes.Append(xmlAttribute3);
				XmlAttribute xmlAttribute4 = xmlDesc.CreateAttribute("count");
				xmlAttribute4.Value = text5;
				xmlNode.Attributes.Append(xmlAttribute4);
				switch (text7)
				{
				case "10":
				case "17":
				case "18":
				case "19":
				case "20":
					text8 = "Hardware conector";
					break;
				default:
					text8 = "logikal";
					break;
				}
				XmlAttribute xmlAttribute5 = xmlDesc.CreateAttribute("method");
				xmlAttribute5.Value = text8;
				xmlNode.Attributes.Append(xmlAttribute5);
				XmlAttribute xmlAttribute6 = xmlDesc.CreateAttribute("unmounted");
				xmlAttribute6.Value = "0";
				xmlNode.Attributes.Append(xmlAttribute6);
				XmlAttribute xmlAttribute7 = xmlDesc.CreateAttribute("square");
				xmlAttribute7.Value = "SQ" + strSquare;
				xmlNode.Attributes.Append(xmlAttribute7);
				if (!string.IsNullOrEmpty(text4))
				{
					XmlAttribute xmlAttribute8 = xmlDesc.CreateAttribute("pricingWidth");
					xmlAttribute8.Value = text4;
					xmlNode.Attributes.Append(xmlAttribute8);
					XmlAttribute xmlAttribute9 = xmlDesc.CreateAttribute("length");
					xmlAttribute9.Value = text4;
					xmlNode.Attributes.Append(xmlAttribute9);
					XmlAttribute xmlAttribute10 = xmlDesc.CreateAttribute("long");
					xmlAttribute10.Value = text4;
					xmlNode.Attributes.Append(xmlAttribute10);
				}
				XmlAttribute xmlAttribute11 = xmlDesc.CreateAttribute("weight");
				xmlAttribute11.Value = text6;
				xmlNode.Attributes.Append(xmlAttribute11);
				xmlElement.AppendChild(xmlNode);
				nGMid++;
			}
			return xmlDesc;
		}
		catch
		{
			return xmlDesc;
		}
	}

	private static XmlDocument AddContourData(XmlDocument xmlDesc)
	{
		try
		{
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDesc.NameTable);
			string uri = "ModelDescriptive";
			xmlNamespaceManager.AddNamespace("dsc", uri);
			xmlNamespaceManager.AddNamespace("draw", "ModelDraw");
			XmlElement obj = (XmlElement)xmlDesc.SelectSingleNode("Model/dsc:Model/dsc:Contour", xmlNamespaceManager);
			XmlAttribute xmlAttribute = xmlDesc.CreateAttribute("id");
			xmlAttribute.Value = "C0";
			obj.SetAttributeNode(xmlAttribute);
			xmlDesc = AddNode(xmlDesc, "dsc:Hole", "Model/dsc:Model/dsc:Contour");
			return xmlDesc;
		}
		catch
		{
			return xmlDesc;
		}
	}

	private XmlDocument AddContourHoleData(XmlDocument xmlDesc, DataSet DataSet, XmlDocument xmlProfMem)
	{
		try
		{
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDesc.NameTable);
			string uri = "ModelDescriptive";
			xmlNamespaceManager.AddNamespace("dsc", uri);
			xmlNamespaceManager.AddNamespace("draw", "ModelDraw");
			XmlElement obj = (XmlElement)xmlDesc.SelectSingleNode("Model/dsc:Model/dsc:Contour/dsc:Hole", xmlNamespaceManager);
			dGreaterProfilePieceX = 0.0;
			dGreaterProfilePieceY = 0.0;
			dLowerProfilePieceX = 9999999.0;
			dLowerProfilePieceY = 9999999.0;
			XmlAttribute xmlAttribute = xmlDesc.CreateAttribute("id");
			xmlAttribute.Value = "H" + nHoleid.ToString(CultureInfo.CurrentCulture);
			obj.SetAttributeNode(xmlAttribute);
			XmlAttribute xmlAttribute2 = xmlDesc.CreateAttribute("compositeOrder");
			xmlAttribute2.Value = "1";
			obj.SetAttributeNode(xmlAttribute2);
			XmlAttribute xmlAttribute3 = xmlDesc.CreateAttribute("compositeCode");
			xmlAttribute3.Value = "Submodel1";
			obj.SetAttributeNode(xmlAttribute3);
			xmlDesc = AddNode(xmlDesc, "dsc:Hole", "Model/dsc:Model/dsc:Contour/dsc:Hole");
			xmlDesc = AddProfilePieces(xmlDesc, "Model/dsc:Model/dsc:Contour/dsc:Hole", xmlNamespaceManager, DataSet, "1", xmlProfMem, "", bFirstSash: false, "1", "", bGlazingLedge: false);
			xmlDesc = AddRodAxis(xmlDesc, "Model/dsc:Model/dsc:Contour/dsc:Hole/dsc:Rod", xmlNamespaceManager);
			XmlNodeList xmlNLInnerInsertions = xmlProfMem.SelectNodes("PROFMEMBASE/PROFMEM/PROFMEM");
			xmlDesc = AddSquare(xmlDesc, "Model/dsc:Model/dsc:Contour/dsc:Hole/dsc:Rod", xmlNamespaceManager, DataSet, "1", xmlNLInnerInsertions, "");
			xmlDesc = AddGaskets(xmlDesc, "Model/dsc:Model/dsc:Contour/dsc:Hole/dsc:Rod", xmlNamespaceManager, DataSet, "1", "", "1");
			xmlDesc = AddGeneratedMaterials(xmlDesc, "Model/dsc:Model/dsc:Contour/dsc:Hole/dsc:Rod/dsc:Square", DataSet, "1", "", "1");
			nHoleid++;
			return xmlDesc;
		}
		catch
		{
			return xmlDesc;
		}
	}

	private XmlDocument AddHoleData(XmlDocument xmlDesc, DataSet DataSet, XmlDocument xmlProfMem)
	{
		try
		{
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDesc.NameTable);
			string uri = "ModelDescriptive";
			xmlNamespaceManager.AddNamespace("dsc", uri);
			XmlElement xmlElement = (XmlElement)xmlDesc.SelectSingleNode("Model/dsc:Model/dsc:Contour/dsc:Hole/dsc:Hole", xmlNamespaceManager);
			XmlElement xmlElement2 = null;
			if (DataSet.Tables["Perfiles"].Select("PieceListType='16'").Count() > 0)
			{
				AddNode(xmlDesc, "dsc:Delimitations", "Model/dsc:Model/dsc:Contour/dsc:Hole/dsc:Rod/dsc:Square");
				xmlElement2 = (XmlElement)xmlDesc.SelectSingleNode("Model/dsc:Model/dsc:Contour/dsc:Hole/dsc:Rod/dsc:Square/dsc:Delimitations", xmlNamespaceManager);
			}
			XmlAttribute xmlAttribute = xmlDesc.CreateAttribute("id");
			xmlAttribute.Value = "H" + nHoleid.ToString(CultureInfo.CurrentCulture);
			xmlElement.SetAttributeNode(xmlAttribute);
			XmlAttribute xmlAttribute2 = xmlDesc.CreateAttribute("partition");
			xmlAttribute2.Value = "1";
			xmlElement.SetAttributeNode(xmlAttribute2);
			if (DataSet.Tables["Perfiles"].Select("PieceListType='16' OR PieceListType='3'").Count() > 0)
			{
				DataRow[] array = DataSet.Tables["Perfiles"].Select("PieceListType='16' OR PieceListType='3'");
				foreach (DataRow obj in array)
				{
					XmlNode xmlNode = xmlDesc.CreateNode(XmlNodeType.Element, "dsc:Delimitation", "ModelDescriptive");
					XmlAttribute xmlAttribute3 = xmlDesc.CreateAttribute("id");
					xmlAttribute3.Value = "D" + nDelimitationid.ToString(CultureInfo.CurrentCulture);
					xmlNode.Attributes.Append(xmlAttribute3);
					string strMaterial = obj["ReferenciaFinal"].ToString();
					string text = obj["xGUID"].ToString();
					string value = obj["xSashGUID"].ToString();
					string text2 = obj["PieceListType"].ToString();
					string name = "mullion";
					if (text2 == "3")
					{
						name = "rebate";
					}
					XmlAttribute xmlAttribute4 = xmlDesc.CreateAttribute(name);
					xmlAttribute4.Value = "1";
					xmlNode.Attributes.Append(xmlAttribute4);
					XmlAttribute xmlAttribute5 = xmlDesc.CreateAttribute("square");
					xmlAttribute5.Value = "SQ1";
					xmlNode.Attributes.Append(xmlAttribute5);
					XmlAttribute xmlAttribute6 = xmlDesc.CreateAttribute("SashGUID");
					xmlAttribute6.Value = value;
					xmlNode.Attributes.Append(xmlAttribute6);
					string text3 = obj["AssemblyInfo"].ToString();
					string text4 = "";
					text4 = ((text3 == "H") ? "horizontal" : ((!(text3 == "V")) ? "" : "vertical"));
					XmlAttribute xmlAttribute7 = xmlDesc.CreateAttribute("orientation");
					xmlAttribute7.Value = text4;
					xmlNode.Attributes.Append(xmlAttribute7);
					xmlNode.AppendChild(AddNodeDelimitationMatrixPolyline(xmlDesc, xmlProfMem, text, text4));
					xmlElement.AppendChild(xmlNode);
					xmlDesc = AddRod(xmlDesc, "Model/dsc:Model/dsc:Contour/dsc:Hole/dsc:Hole/dsc:Delimitation[@id='D" + nDelimitationid.ToString(CultureInfo.CurrentCulture) + "']", xmlNamespaceManager, strMaterial);
					xmlDesc = AddProfilePieces(xmlDesc, "Model/dsc:Model/dsc:Contour/dsc:Hole/dsc:Hole/dsc:Delimitation[@id='D" + nDelimitationid.ToString(CultureInfo.CurrentCulture) + "']", xmlNamespaceManager, DataSet, "1", xmlProfMem, "", bFirstSash: true, "1", text, bGlazingLedge: false);
					if (text2 == "16")
					{
						XmlNode xmlNode2 = xmlDesc.CreateNode(XmlNodeType.Element, "dsc:Delimitation", "ModelDescriptive");
						XmlAttribute xmlAttribute8 = xmlDesc.CreateAttribute("id");
						xmlAttribute8.Value = "D" + nDelimitationid.ToString(CultureInfo.CurrentCulture);
						xmlNode2.Attributes.Append(xmlAttribute8);
						xmlElement2.AppendChild(xmlNode2);
					}
					nDelimitationid++;
				}
			}
			nHoleid++;
			return xmlDesc;
		}
		catch
		{
			return xmlDesc;
		}
	}

	private XmlDocument AddPartitionHolesData(XmlDocument xmlDesc, DataSet DataSet, string strInsertionId, XmlDocument xmlProfMem, string strxSashGUID, string strInsertionGUID, XmlNodeList xmlNodeListGlass, bool bIsOnlyGlass, bool bFirstSash, bool bInsertionInsideInsertion, string strContourInsertionIdReplicated)
	{
		XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDesc.NameTable);
		string text = "ModelDescriptive";
		xmlNamespaceManager.AddNamespace("dsc", text);
		XmlElement obj = (XmlElement)xmlDesc.SelectSingleNode("Model/dsc:Model/dsc:Contour/dsc:Hole/dsc:Hole", xmlNamespaceManager);
		dGreaterProfilePieceX = 0.0;
		dGreaterProfilePieceY = 0.0;
		dLowerProfilePieceX = 9999999.0;
		dLowerProfilePieceY = 9999999.0;
		XmlNode xmlNode = xmlDesc.CreateNode(XmlNodeType.Element, "dsc:Hole", text);
		XmlAttribute xmlAttribute = xmlDesc.CreateAttribute("id");
		xmlAttribute.Value = "H" + nHoleid.ToString(CultureInfo.CurrentCulture);
		xmlNode.Attributes.Append(xmlAttribute);
		obj.AppendChild(xmlNode);
		xmlDesc = AddProfilePieces(xmlDesc, "Model/dsc:Model/dsc:Contour/dsc:Hole/dsc:Hole/dsc:Hole[@id='H" + nHoleid + "']", xmlNamespaceManager, DataSet, strInsertionId, xmlProfMem, strxSashGUID, bFirstSash: false, nSquareid.ToString(CultureInfo.CurrentCulture), "", bGlazingLedge: false);
		xmlDesc = AddGaskets(xmlDesc, "Model/dsc:Model/dsc:Contour/dsc:Hole/dsc:Hole/dsc:Hole[@id='H" + nHoleid + "']/dsc:Rod", xmlNamespaceManager, DataSet, strInsertionId, strxSashGUID, nSquareid.ToString(CultureInfo.CurrentCulture));
		xmlDesc = AddRodAxis(xmlDesc, "Model/dsc:Model/dsc:Contour/dsc:Hole/dsc:Hole/dsc:Hole[@id='H" + nHoleid + "']/dsc:Rod", xmlNamespaceManager);
		xmlDesc = AddGeneratedMaterials(xmlDesc, "Model/dsc:Model/dsc:Contour/dsc:Hole/dsc:Hole/dsc:Hole[@id='H" + nHoleid + "']", DataSet, strInsertionId, strxSashGUID, nSquareid.ToString(CultureInfo.CurrentCulture));
		if (bFirstSash)
		{
			xmlDesc = AddProfilePieces(xmlDesc, "Model/dsc:Model/dsc:Contour/dsc:Hole/dsc:Hole/dsc:Hole[@id='H" + nHoleid + "']", xmlNamespaceManager, DataSet, strInsertionId, xmlProfMem, "{00000000-0000-0000-0000-000000000000}", bFirstSash: true, nSquareid.ToString(CultureInfo.CurrentCulture), "", bGlazingLedge: false);
			xmlDesc = AddGeneratedMaterials(xmlDesc, "Model/dsc:Model/dsc:Contour/dsc:Hole/dsc:Hole/dsc:Hole[@id='H" + nHoleid + "']", DataSet, strInsertionId, "{00000000-0000-0000-0000-000000000000}", nSquareid.ToString(CultureInfo.CurrentCulture));
		}
		if (bInsertionInsideInsertion)
		{
			xmlDesc = AddGaskets(xmlDesc, "Model/dsc:Model/dsc:Contour/dsc:Hole/dsc:Hole/dsc:Hole[@id='H" + nHoleid + "']/dsc:Rod", xmlNamespaceManager, DataSet, strContourInsertionIdReplicated, "{00000000-0000-0000-0000-000000000000}", nSquareid.ToString(CultureInfo.CurrentCulture));
			xmlDesc = AddProfilePieces(xmlDesc, "Model/dsc:Model/dsc:Contour/dsc:Hole/dsc:Hole/dsc:Hole[@id='H" + nHoleid + "']", xmlNamespaceManager, DataSet, strContourInsertionIdReplicated, xmlProfMem, "{00000000-0000-0000-0000-000000000000}", bFirstSash: true, nSquareid.ToString(CultureInfo.CurrentCulture), "", bGlazingLedge: false);
			xmlDesc = AddGeneratedMaterials(xmlDesc, "Model/dsc:Model/dsc:Contour/dsc:Hole/dsc:Hole/dsc:Hole[@id='H" + nHoleid + "']", DataSet, strContourInsertionIdReplicated, "{00000000-0000-0000-0000-000000000000}", nSquareid.ToString(CultureInfo.CurrentCulture));
		}
		if (!bIsOnlyGlass)
		{
			XmlNodeList xmlNLInnerInsertions = xmlProfMem.SelectNodes("descendant::PROFMEM[@GUID='" + strxSashGUID + "']");
			xmlDesc = AddSquare(xmlDesc, "Model/dsc:Model/dsc:Contour/dsc:Hole/dsc:Hole/dsc:Hole[@id='H" + nHoleid + "']/dsc:Rod", xmlNamespaceManager, DataSet, strInsertionId, xmlNLInnerInsertions, strxSashGUID);
			int nDelimitationSquare = nSquareid - 1;
			xmlDesc = AddSquareReference(xmlDesc, "//dsc:PhysicHole[@GUID='" + strInsertionGUID + "']", xmlNamespaceManager, nDelimitationSquare.ToString(CultureInfo.CurrentCulture));
			xmlDesc = AddDelimitationsInHoles(xmlDesc, "Model/dsc:Model/dsc:Contour/dsc:Hole/dsc:Hole/dsc:Hole[@id='H" + nHoleid + "']/dsc:Rod/dsc:Square", xmlNamespaceManager, DataSet, strInsertionId, strxSashGUID, nDelimitationSquare);
		}
		string text2 = "";
		foreach (XmlNode item in xmlNodeListGlass)
		{
			text2 = ((XmlElement)item).GetAttributeNode("GUID").Value;
			xmlDesc = AddNode(xmlDesc, "dsc:Glass", "Model/dsc:Model/dsc:Contour/dsc:Hole/dsc:Hole/dsc:Hole[@id='H" + nHoleid + "']");
			xmlDesc = AddGlass(xmlDesc, "Model/dsc:Model/dsc:Contour/dsc:Hole/dsc:Hole/dsc:Hole[@id='H" + nHoleid + "']/dsc:Glass", xmlNamespaceManager, DataSet, xmlProfMem, text2, strxSashGUID, strInsertionId);
		}
		if (!bIsOnlyGlass)
		{
			xmlDesc = AddOpening(xmlDesc, xmlNamespaceManager, DataSet, strInsertionId, nHoleid, strxSashGUID);
		}
		nHoleid++;
		return xmlDesc;
	}

	private XmlDocument AddPartitionHoles(XmlDocument xmlDesc, XmlNodeList xmlInsertionList, DataSet DataSet, XmlDocument xmlProfMem)
	{
		try
		{
			string text = "";
			string text2 = "";
			string text3 = "";
			string text4 = "";
			string text5 = "";
			string text6 = "";
			bool flag = false;
			foreach (XmlNode xmlInsertion in xmlInsertionList)
			{
				text2 = "";
				text3 = "";
				text4 = "";
				text6 = "";
				flag = false;
				text5 = "";
				text = ((XmlElement)xmlInsertion).GetAttributeNode("GUID").Value;
				DataRow[] array = DataSet.Tables["Insertions"].Select("xGUID='" + text + "'");
				foreach (DataRow obj in array)
				{
					text3 = obj["InsertionID"].ToString();
					text6 = obj["TypeNr"].ToString();
				}
				XmlNodeList xmlNodeList;
				switch (text6)
				{
				case "28":
				case "29":
				case "30":
					xmlNodeList = xmlInsertion.SelectNodes("PROFMEM/PROFMEM/PROFMEM");
					break;
				default:
					xmlNodeList = xmlInsertion.SelectNodes("PROFMEM");
					break;
				}
				if (xmlNodeList.Count == 0)
				{
					XmlNodeList xmlNodeListGlass = xmlInsertion.SelectNodes("ELEMENTS/GLASS");
					xmlDesc = AddPartitionHolesData(xmlDesc, DataSet, text3, xmlProfMem, text2, text, xmlNodeListGlass, bIsOnlyGlass: true, bFirstSash: false, flag, text5);
					continue;
				}
				int num = 1;
				foreach (XmlNode item in xmlNodeList)
				{
					text2 = ((XmlElement)item).GetAttributeNode("GUID").Value;
					switch (text6)
					{
					case "28":
					case "29":
					case "30":
					{
						flag = true;
						array = DataSet.Tables["Sashes"].Select("xGUID='" + text2 + "'");
						for (int i = 0; i < array.Length; i++)
						{
							text3 = array[i]["InsertionID"].ToString();
						}
						array = DataSet.Tables["Insertions"].Select("InsertionId='" + text3 + "'");
						for (int i = 0; i < array.Length; i++)
						{
							text4 = array[i]["ElevationId"].ToString();
						}
						array = DataSet.Tables["Insertions"].Select("ElevationId='" + text4 + "' AND InsertionNo='0'");
						for (int i = 0; i < array.Length; i++)
						{
							text5 = array[i]["InsertionID"].ToString();
						}
						break;
					}
					}
					XmlNodeList xmlNodeListGlass = item.SelectNodes("ELEMENTS/GLASS");
					xmlDesc = ((num != 1) ? AddPartitionHolesData(xmlDesc, DataSet, text3, xmlProfMem, text2, text, xmlNodeListGlass, bIsOnlyGlass: false, bFirstSash: false, flag, text5) : AddPartitionHolesData(xmlDesc, DataSet, text3, xmlProfMem, text2, text, xmlNodeListGlass, bIsOnlyGlass: false, bFirstSash: true, flag, text5));
					num++;
				}
			}
			return xmlDesc;
		}
		catch
		{
			return xmlDesc;
		}
	}

	private XmlDocument AddRod(XmlDocument xmlDesc, string strPath, XmlNamespaceManager xmlnsManager, string strMaterial)
	{
		try
		{
			xmlDesc = AddNode(xmlDesc, "dsc:Rod", strPath);
			XmlElement obj = (XmlElement)xmlDesc.SelectSingleNode(strPath + "/dsc:Rod", xmlnsManager);
			XmlAttribute xmlAttribute = xmlDesc.CreateAttribute("id");
			xmlAttribute.Value = "R" + nRodid.ToString(CultureInfo.CurrentCulture);
			obj.SetAttributeNode(xmlAttribute);
			XmlAttribute xmlAttribute2 = xmlDesc.CreateAttribute("material");
			xmlAttribute2.Value = strMaterial;
			obj.SetAttributeNode(xmlAttribute2);
			nRodid++;
			return xmlDesc;
		}
		catch
		{
			return xmlDesc;
		}
	}

	private static XmlDocument AddSquareReference(XmlDocument xmlDesc, string strPath, XmlNamespaceManager xmlnsManager, string strSquareId)
	{
		try
		{
			XmlElement obj = (XmlElement)xmlDesc.SelectSingleNode(strPath, xmlnsManager);
			XmlNode xmlNode = xmlDesc.CreateNode(XmlNodeType.Element, "dsc:SquareReference", "ModelDescriptive");
			XmlAttribute xmlAttribute = xmlDesc.CreateAttribute("id");
			xmlAttribute.Value = "SQ" + strSquareId;
			xmlNode.Attributes.Append(xmlAttribute);
			obj.AppendChild(xmlNode);
			return xmlDesc;
		}
		catch
		{
			return xmlDesc;
		}
	}

	private XmlDocument AddRodAxis(XmlDocument xmlDesc, string strPath, XmlNamespaceManager xmlnsManager)
	{
		try
		{
			XmlElement xmlElement = (XmlElement)xmlDesc.SelectSingleNode(strPath, xmlnsManager);
			XmlNode xmlNode = xmlDesc.CreateNode(XmlNodeType.Element, "dsc:Axis", "ModelDescriptive");
			XmlNode xmlNode2 = xmlDesc.CreateNode(XmlNodeType.Element, "POLYLINE2D", "");
			XmlAttribute xmlAttribute = xmlDesc.CreateAttribute("filled");
			xmlAttribute.Value = "0";
			xmlNode2.Attributes.Append(xmlAttribute);
			XmlAttribute xmlAttribute2 = xmlDesc.CreateAttribute("path");
			xmlAttribute2.Value = "P" + dLowerProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dLowerProfilePieceY.ToString(CultureInfo.InvariantCulture) + "P" + dGreaterProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dLowerProfilePieceY.ToString(CultureInfo.InvariantCulture) + "P" + dGreaterProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dGreaterProfilePieceY.ToString(CultureInfo.InvariantCulture) + "P" + dLowerProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dGreaterProfilePieceY.ToString(CultureInfo.InvariantCulture) + "P" + dLowerProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dLowerProfilePieceY.ToString(CultureInfo.InvariantCulture);
			xmlNode2.Attributes.Append(xmlAttribute2);
			xmlNode.AppendChild(xmlNode2);
			xmlElement.AppendChild(xmlNode);
			return xmlDesc;
		}
		catch
		{
			return xmlDesc;
		}
	}

	private XmlDocument AddGaskets(XmlDocument xmlDesc, string strPath, XmlNamespaceManager xmlnsManager, DataSet DataSet, string strNumeroInsertion, string strSashGUID, string strSquare)
	{
		try
		{
			XmlElement xmlElement = (XmlElement)xmlDesc.SelectSingleNode(strPath, xmlnsManager);
			string text = "";
			string text2 = "";
			string text3 = "";
			string text4 = "";
			string text5 = "";
			string text6 = "";
			text6 = ((!(strNumeroInsertion == "1")) ? ("InsertionId='" + strNumeroInsertion + "' AND xSashGUID='" + strSashGUID + "' AND (PieceListType=6 OR PieceListType=7)") : "xSashGUID='{00000000-0000-0000-0000-000000000000}'  AND (PieceListType=6 OR PieceListType=7)");
			DataRow[] array = DataSet.Tables["Articulos"].Select(text6);
			foreach (DataRow obj in array)
			{
				XmlNode xmlNode = xmlDesc.CreateNode(XmlNodeType.Element, "dsc:Gasket", "ModelDescriptive");
				text = obj["ReferenciaFinal"].ToString();
				text2 = obj["Color"].ToString();
				text3 = obj["Longitud"].ToString();
				if (!string.IsNullOrEmpty(text3))
				{
					double num = Convert.ToDouble(text3, CultureInfo.CurrentCulture);
					text3 = (num * 1000.0).ToString(CultureInfo.CurrentCulture);
				}
				text4 = obj["Cantidad"].ToString();
				text5 = obj["Weight"].ToString();
				if (!string.IsNullOrEmpty(text5) && text5.Contains(','))
				{
					text5 = text5.Replace(',', '.');
				}
				XmlAttribute xmlAttribute = xmlDesc.CreateAttribute("id");
				xmlAttribute.Value = "GK" + nGasketId.ToString(CultureInfo.CurrentCulture);
				xmlNode.Attributes.Append(xmlAttribute);
				XmlAttribute xmlAttribute2 = xmlDesc.CreateAttribute("material");
				xmlAttribute2.Value = text;
				xmlNode.Attributes.Append(xmlAttribute2);
				XmlAttribute xmlAttribute3 = xmlDesc.CreateAttribute("color");
				xmlAttribute3.Value = text2;
				xmlNode.Attributes.Append(xmlAttribute3);
				XmlNode xmlNodeGM = xmlDesc.CreateNode(XmlNodeType.Element, "dsc:GeneratedMaterial", "ModelDescriptive");
				xmlNode.AppendChild(AddGeneratedMaterialGasket(xmlDesc, xmlNodeGM, text, text4, text3, strSquare, text5));
				xmlElement.AppendChild(xmlNode);
				nGasketId++;
			}
			return xmlDesc;
		}
		catch
		{
			return xmlDesc;
		}
	}

	private static XmlDocument AddDelimitationsInHoles(XmlDocument xmlDesc, string strPath, XmlNamespaceManager xmlnsManager, DataSet DataSet, string strNumeroInsertion, string strSashGUID, int nDelimitationSquare)
	{
		try
		{
			string filterExpression = "InsertionId='" + strNumeroInsertion + "' AND xSashGUID='" + strSashGUID + "' AND PieceListType=3";
			if (DataSet.Tables["Perfiles"].Select(filterExpression).Count() > 0)
			{
				AddNode(xmlDesc, "dsc:Delimitations", strPath);
				XmlElement obj = (XmlElement)xmlDesc.SelectSingleNode(strPath + "/dsc:Delimitations", xmlnsManager);
				XmlElement obj2 = (XmlElement)xmlDesc.SelectSingleNode("//dsc:Delimitation[@SashGUID='" + strSashGUID + "']", xmlnsManager);
				XmlElement xmlElement = (XmlElement)xmlDesc.SelectSingleNode("//dsc:Delimitation[@SashGUID='" + strSashGUID + "']/dsc:Rod/dsc:ProfilePiece/dsc:GeneratedMaterial", xmlnsManager);
				obj2.Attributes["square"].Value = "SQ" + nDelimitationSquare.ToString(CultureInfo.CurrentCulture);
				xmlElement.Attributes["square"].Value = "SQ" + nDelimitationSquare.ToString(CultureInfo.CurrentCulture);
				string value = obj2.Attributes.GetNamedItem("id").Value;
				XmlNode xmlNode = xmlDesc.CreateNode(XmlNodeType.Element, "dsc:Delimitation", "ModelDescriptive");
				XmlAttribute xmlAttribute = xmlDesc.CreateAttribute("id");
				xmlAttribute.Value = value;
				xmlNode.Attributes.Append(xmlAttribute);
				obj.AppendChild(xmlNode);
			}
			return xmlDesc;
		}
		catch
		{
			return xmlDesc;
		}
	}

	private XmlDocument AddSquare(XmlDocument xmlDesc, string strPath, XmlNamespaceManager xmlnsManager, DataSet DataSet, string strNumeroInsertion, XmlNodeList xmlNLInnerInsertions, string strSashGUID)
	{
		try
		{
			string text = "";
			string text2 = "";
			xmlDesc = AddNode(xmlDesc, "dsc:Square", strPath);
			XmlElement xmlElement = (XmlElement)xmlDesc.SelectSingleNode(strPath + "/dsc:Square", xmlnsManager);
			if (strNumeroInsertion == "1")
			{
				DataRow[] array = DataSet.Tables["Insertions"].Select("InsertionId='" + strNumeroInsertion + "'");
				foreach (DataRow obj in array)
				{
					text = obj["Width_Output"].ToString();
					text2 = obj["Height_Output"].ToString();
				}
			}
			else
			{
				DataRow[] array = DataSet.Tables["Sashes"].Select("xGUID='" + strSashGUID + "'");
				foreach (DataRow obj2 in array)
				{
					text = obj2["Width_Output"].ToString();
					text2 = obj2["Height_Output"].ToString();
				}
			}
			if (!string.IsNullOrEmpty(text) && text.Contains(','))
			{
				text = text.Replace(',', '.');
			}
			if (!string.IsNullOrEmpty(text2) && text2.Contains(','))
			{
				text2 = text2.Replace(',', '.');
			}
			XmlAttribute xmlAttribute = xmlDesc.CreateAttribute("id");
			xmlAttribute.Value = "SQ" + nSquareid.ToString(CultureInfo.CurrentCulture);
			xmlElement.SetAttributeNode(xmlAttribute);
			XmlAttribute xmlAttribute2 = xmlDesc.CreateAttribute("height");
			xmlAttribute2.Value = text2;
			xmlElement.SetAttributeNode(xmlAttribute2);
			XmlAttribute xmlAttribute3 = xmlDesc.CreateAttribute("width");
			xmlAttribute3.Value = text;
			xmlElement.SetAttributeNode(xmlAttribute3);
			XmlAttribute xmlAttribute4 = xmlDesc.CreateAttribute("generate");
			xmlAttribute4.Value = "1";
			xmlElement.SetAttributeNode(xmlAttribute4);
			foreach (XmlNode xmlNLInnerInsertion in xmlNLInnerInsertions)
			{
				string text3 = "";
				text3 = ((XmlElement)xmlNLInnerInsertion).GetAttribute("GUID").ToString();
				XmlNode xmlNode = xmlDesc.CreateNode(XmlNodeType.Element, "dsc:PhysicHole", "ModelDescriptive");
				XmlAttribute xmlAttribute5 = xmlDesc.CreateAttribute("id");
				xmlAttribute5.Value = "PH" + nPhysicHoleid.ToString(CultureInfo.CurrentCulture);
				xmlNode.Attributes.Append(xmlAttribute5);
				XmlAttribute xmlAttribute6 = xmlDesc.CreateAttribute("GUID");
				xmlAttribute6.Value = text3;
				xmlNode.Attributes.Append(xmlAttribute6);
				XmlAttribute xmlAttribute7 = xmlDesc.CreateAttribute("x");
				xmlAttribute7.Value = "0";
				xmlNode.Attributes.Append(xmlAttribute7);
				XmlAttribute xmlAttribute8 = xmlDesc.CreateAttribute("y");
				xmlAttribute8.Value = "0";
				xmlNode.Attributes.Append(xmlAttribute8);
				XmlAttribute xmlAttribute9 = xmlDesc.CreateAttribute("width");
				xmlAttribute9.Value = "0";
				xmlNode.Attributes.Append(xmlAttribute9);
				XmlAttribute xmlAttribute10 = xmlDesc.CreateAttribute("height");
				xmlAttribute10.Value = "0";
				xmlNode.Attributes.Append(xmlAttribute10);
				xmlElement.AppendChild(xmlNode);
				nPhysicHoleid++;
			}
			nSquareid++;
			return xmlDesc;
		}
		catch
		{
			return xmlDesc;
		}
	}

	private XmlDocument AddProfilePieces(XmlDocument xmlDesc, string strPath, XmlNamespaceManager xmlnsManager, DataSet DataSet, string strNumeroInsertion, XmlDocument xmlProfMem, string strxSashGUID, bool bFirstSash, string strSquare, string strProfileGUID, bool bGlazingLedge)
	{
		try
		{
			string text = "";
			string text2 = "";
			string text3 = "";
			string text4 = "";
			string text5 = "";
			string text6 = "";
			string text7 = "";
			string text8 = "";
			string text9 = "";
			string text10 = "";
			string text11 = "";
			string text12 = "";
			string text13 = "";
			string text14 = "";
			string strMaterial = "";
			int num = 0;
			int num2 = 0;
			text14 = ((!string.IsNullOrEmpty(strProfileGUID)) ? ("xGUID='" + strProfileGUID + "'") : (bGlazingLedge ? (string.IsNullOrEmpty(strxSashGUID) ? ("InsertionId='" + strNumeroInsertion + "' AND xSashGUID='{00000000-0000-0000-0000-000000000000}' AND  PieceListType = 4 ") : ("InsertionId='" + strNumeroInsertion + "' AND xSashGUID='" + strxSashGUID + "' AND  PieceListType = 4 ")) : (string.IsNullOrEmpty(strxSashGUID) ? ("InsertionId='" + strNumeroInsertion + "' AND  (PieceListType <> 3 AND PieceListType <> 16 AND PieceListType <> 4)") : ("InsertionId='" + strNumeroInsertion + "' AND xSashGUID='" + strxSashGUID + "' AND  (PieceListType <> 3 AND PieceListType <> 16 AND PieceListType <> 4)"))));
			DataRow[] array;
			if (!bFirstSash)
			{
				if (DataSet.Tables["Perfiles"].Select(text14).Count() > 0)
				{
					if (!string.IsNullOrEmpty(strxSashGUID))
					{
						array = DataSet.Tables["Perfiles"].Select("InsertionId='" + strNumeroInsertion + "' AND PieceListType='2'");
						int num3 = 0;
						if (num3 < array.Length)
						{
							strMaterial = array[num3]["ReferenciaFinal"].ToString();
						}
						xmlDesc = AddRod(xmlDesc, strPath, xmlnsManager, strMaterial);
					}
					else
					{
						array = DataSet.Tables["Perfiles"].Select("InsertionId='" + strNumeroInsertion + "' AND PieceListType='1'");
						int num3 = 0;
						if (num3 < array.Length)
						{
							strMaterial = array[num3]["ReferenciaFinal"].ToString();
						}
						xmlDesc = AddRod(xmlDesc, strPath, xmlnsManager, strMaterial);
					}
				}
				else
				{
					if (!(strNumeroInsertion == "1") || DataSet.Tables["Perfiles"].Select("InsertionId='1' AND PieceListType='1'").Count() != 0 || DataSet.Tables["Perfiles"].Select("InsertionId='1' AND PieceListType='16'").Count() <= 0)
					{
						return xmlDesc;
					}
					xmlDesc = AddRod(xmlDesc, strPath, xmlnsManager, strMaterial);
				}
			}
			XmlElement xmlElement = (XmlElement)xmlDesc.SelectSingleNode(strPath + "/dsc:Rod", xmlnsManager);
			array = DataSet.Tables["Perfiles"].Select(text14);
			foreach (DataRow obj in array)
			{
				text = obj["ReferenciaFinal"].ToString();
				text2 = obj["Color"].ToString();
				text3 = obj["Longitud"].ToString();
				text10 = obj["xGUID"].ToString();
				XmlNode xmlNode = xmlProfMem.SelectSingleNode("//PROFILE[@GUID='" + text10 + "']/PROXYFACES/FACE/VERTICES");
				XmlNodeList xmlNodeList = xmlProfMem.SelectNodes("//PROFILE[@GUID='" + text10 + "']/PROXYFACES/FACE/VERTICES/*");
				if (!string.IsNullOrEmpty(text3) && text3.Contains(','))
				{
					text3 = text3.Replace(',', '.');
				}
				text4 = obj["Cantidad"].ToString();
				text6 = obj["AnguloA"].ToString();
				text7 = obj["AnguloB"].ToString();
				text8 = obj["AssemblyInfoNo"].ToString();
				text5 = obj["Peso"].ToString();
				if (!string.IsNullOrEmpty(text5) && text5.Contains(','))
				{
					text5 = text5.Replace(',', '.');
				}
				XmlNode xmlNode2 = xmlDesc.CreateNode(XmlNodeType.Element, "dsc:ProfilePiece", "ModelDescriptive");
				XmlAttribute xmlAttribute = xmlDesc.CreateAttribute("id");
				xmlAttribute.Value = "PP" + nPPid.ToString(CultureInfo.CurrentCulture);
				xmlNode2.Attributes.Append(xmlAttribute);
				XmlAttribute xmlAttribute2 = xmlDesc.CreateAttribute("material");
				xmlAttribute2.Value = text;
				xmlNode2.Attributes.Append(xmlAttribute2);
				XmlAttribute xmlAttribute3 = xmlDesc.CreateAttribute("color");
				xmlAttribute3.Value = text2;
				xmlNode2.Attributes.Append(xmlAttribute3);
				text9 = text8 switch
				{
					"1" => "180", 
					"2" => "0", 
					"3" => "270", 
					"4" => "90", 
					_ => "0", 
				};
				XmlAttribute xmlAttribute4 = xmlDesc.CreateAttribute("angle");
				xmlAttribute4.Value = text9;
				xmlNode2.Attributes.Append(xmlAttribute4);
				XmlAttribute xmlAttribute5 = xmlDesc.CreateAttribute("parameter");
				xmlAttribute5.Value = text10;
				xmlNode2.Attributes.Append(xmlAttribute5);
				XmlAttribute xmlAttribute6 = xmlDesc.CreateAttribute("order");
				xmlAttribute6.Value = num.ToString(CultureInfo.CurrentCulture);
				xmlNode2.Attributes.Append(xmlAttribute6);
				num++;
				XmlAttribute xmlAttribute7 = xmlDesc.CreateAttribute("long");
				xmlAttribute7.Value = text3;
				xmlNode2.Attributes.Append(xmlAttribute7);
				XmlAttribute xmlAttribute8 = xmlDesc.CreateAttribute("lengthWithWelding");
				xmlAttribute8.Value = text3;
				xmlNode2.Attributes.Append(xmlAttribute8);
				XmlAttribute xmlAttribute9 = xmlDesc.CreateAttribute("lengthWithoutWelding");
				xmlAttribute9.Value = text3;
				xmlNode2.Attributes.Append(xmlAttribute9);
				XmlAttribute xmlAttribute10 = xmlDesc.CreateAttribute("roundedLength");
				xmlAttribute10.Value = text3;
				xmlNode2.Attributes.Append(xmlAttribute10);
				XmlAttribute xmlAttribute11 = xmlDesc.CreateAttribute("lengthGeometrical");
				xmlAttribute11.Value = text3;
				xmlNode2.Attributes.Append(xmlAttribute11);
				XmlAttribute xmlAttribute12 = xmlDesc.CreateAttribute("angleA");
				xmlAttribute12.Value = text6;
				xmlNode2.Attributes.Append(xmlAttribute12);
				XmlAttribute xmlAttribute13 = xmlDesc.CreateAttribute("angleB");
				xmlAttribute13.Value = text7;
				xmlNode2.Attributes.Append(xmlAttribute13);
				XmlAttribute xmlAttribute14 = xmlDesc.CreateAttribute("drawOrder");
				xmlAttribute14.Value = num2.ToString(CultureInfo.CurrentCulture);
				xmlNode2.Attributes.Append(xmlAttribute14);
				num2++;
				XmlNode xmlNode3 = xmlDesc.CreateNode(XmlNodeType.Element, "dsc:GeneratedMaterial", "ModelDescriptive");
				XmlAttribute xmlAttribute15 = xmlDesc.CreateAttribute("id");
				xmlAttribute15.Value = "GM" + nGMid.ToString(CultureInfo.CurrentCulture);
				xmlNode3.Attributes.Append(xmlAttribute15);
				XmlAttribute xmlAttribute16 = xmlDesc.CreateAttribute("material");
				xmlAttribute16.Value = text;
				xmlNode3.Attributes.Append(xmlAttribute16);
				XmlAttribute xmlAttribute17 = xmlDesc.CreateAttribute("color");
				xmlAttribute17.Value = text2;
				xmlNode3.Attributes.Append(xmlAttribute17);
				XmlAttribute xmlAttribute18 = xmlDesc.CreateAttribute("count");
				xmlAttribute18.Value = text4;
				xmlNode3.Attributes.Append(xmlAttribute18);
				XmlAttribute xmlAttribute19 = xmlDesc.CreateAttribute("method");
				xmlAttribute19.Value = "self";
				xmlNode3.Attributes.Append(xmlAttribute19);
				XmlAttribute xmlAttribute20 = xmlDesc.CreateAttribute("unmounted");
				xmlAttribute20.Value = "0";
				xmlNode3.Attributes.Append(xmlAttribute20);
				if (!string.IsNullOrEmpty(strSquare))
				{
					XmlAttribute xmlAttribute21 = xmlDesc.CreateAttribute("square");
					xmlAttribute21.Value = "SQ" + strSquare;
					xmlNode3.Attributes.Append(xmlAttribute21);
				}
				XmlAttribute xmlAttribute22 = xmlDesc.CreateAttribute("parameter");
				xmlAttribute22.Value = text10;
				xmlNode3.Attributes.Append(xmlAttribute22);
				XmlAttribute xmlAttribute23 = xmlDesc.CreateAttribute("pricingWidth");
				xmlAttribute23.Value = text3;
				xmlNode3.Attributes.Append(xmlAttribute23);
				XmlAttribute xmlAttribute24 = xmlDesc.CreateAttribute("length");
				xmlAttribute24.Value = text3;
				xmlNode3.Attributes.Append(xmlAttribute24);
				XmlAttribute xmlAttribute25 = xmlDesc.CreateAttribute("long");
				xmlAttribute25.Value = text3;
				xmlNode3.Attributes.Append(xmlAttribute25);
				XmlAttribute xmlAttribute26 = xmlDesc.CreateAttribute("angleA");
				xmlAttribute26.Value = text6;
				xmlNode3.Attributes.Append(xmlAttribute26);
				XmlAttribute xmlAttribute27 = xmlDesc.CreateAttribute("angleB");
				xmlAttribute27.Value = text7;
				xmlNode3.Attributes.Append(xmlAttribute27);
				XmlAttribute xmlAttribute28 = xmlDesc.CreateAttribute("angle");
				xmlAttribute28.Value = text9;
				xmlNode3.Attributes.Append(xmlAttribute28);
				XmlAttribute xmlAttribute29 = xmlDesc.CreateAttribute("weight");
				xmlAttribute29.Value = text5;
				xmlNode3.Attributes.Append(xmlAttribute29);
				xmlNode2.AppendChild(xmlNode3);
				if (xmlNode != null)
				{
					XmlNode xmlNode4 = xmlDesc.CreateNode(XmlNodeType.Element, "draw:ContourPolyline", "ModelDraw");
					xmlNode2.AppendChild(xmlNode4);
					XmlNode xmlNode5 = xmlDesc.CreateNode(XmlNodeType.Element, "POLYLINE2D", "");
					XmlAttribute xmlAttribute30 = xmlDesc.CreateAttribute("filled");
					xmlAttribute30.Value = "1";
					xmlNode5.Attributes.Append(xmlAttribute30);
					if (text10 != "{00000000-0000-0000-0000-000000000000}")
					{
						XmlElement obj2 = (XmlElement)xmlNode;
						text11 = obj2.GetAttribute("SX").ToString();
						text12 = obj2.GetAttribute("SY").ToString();
						double dX = Convert.ToDouble(obj2.GetAttribute("SX"), CultureInfo.InvariantCulture);
						double dY = Convert.ToDouble(obj2.GetAttribute("SY"), CultureInfo.InvariantCulture);
						SaveGreaterPoints(dX, dY);
						SaveLowerPoints(dX, dY);
						text13 = "P" + text11 + " " + text12;
						foreach (XmlNode item in xmlNodeList)
						{
							text11 = ((XmlElement)item).GetAttribute("X").ToString();
							text12 = ((XmlElement)item).GetAttribute("Y").ToString();
							dX = Convert.ToDouble(((XmlElement)item).GetAttribute("X"), CultureInfo.InvariantCulture);
							dY = Convert.ToDouble(((XmlElement)item).GetAttribute("Y"), CultureInfo.InvariantCulture);
							SaveGreaterPoints(dX, dY);
							SaveLowerPoints(dX, dY);
							text13 = text13 + "P" + text11 + " " + text12;
						}
					}
					else
					{
						text13 = "";
					}
					XmlAttribute xmlAttribute31 = xmlDesc.CreateAttribute("path");
					xmlAttribute31.Value = text13;
					xmlNode5.Attributes.Append(xmlAttribute31);
					xmlNode4.AppendChild(xmlNode5);
				}
				xmlElement.AppendChild(xmlNode2);
				nGMid++;
				nPPid++;
			}
			return xmlDesc;
		}
		catch
		{
			return xmlDesc;
		}
	}

	private void SaveGreaterPoints(double dX, double dY)
	{
		if (dX > dGreaterProfilePieceX)
		{
			dGreaterProfilePieceX = dX;
		}
		if (dY > dGreaterProfilePieceY)
		{
			dGreaterProfilePieceY = dY;
		}
	}

	private void SaveLowerPoints(double dX, double dY)
	{
		if (dX < dLowerProfilePieceX)
		{
			dLowerProfilePieceX = dX;
		}
		if (dY < dLowerProfilePieceY)
		{
			dLowerProfilePieceY = dY;
		}
	}

	private XmlDocument AddGlass(XmlDocument xmlDesc, string strPath, XmlNamespaceManager xmlnsManager, DataSet DataSet, XmlDocument xmlProfMem, string strGlassGUID, string strSashGUID, string strInsertionId)
	{
		try
		{
			XmlElement xmlElement = (XmlElement)xmlDesc.SelectSingleNode(strPath, xmlnsManager);
			string text = "";
			string text2 = "";
			string text3 = "";
			string text4 = "";
			string text5 = "";
			DataRow[] array = DataSet.Tables["Glass"].Select("xGUID='" + strGlassGUID + "'");
			foreach (DataRow dataRow in array)
			{
				text = GetTranslateGlassName(dataRow["Name"].ToString());
				text2 = dataRow["Height"].ToString();
				if (!string.IsNullOrEmpty(text2))
				{
					double num = Convert.ToDouble(text2, CultureInfo.CurrentCulture);
					text2 = (num * 1000.0).ToString(CultureInfo.CurrentCulture);
				}
				text3 = dataRow["Width"].ToString();
				if (!string.IsNullOrEmpty(text3))
				{
					double num2 = Convert.ToDouble(text3, CultureInfo.CurrentCulture);
					text3 = (num2 * 1000.0).ToString(CultureInfo.CurrentCulture);
				}
				text5 = dataRow["Cantidad"].ToString();
				text4 = dataRow["xGUID"].ToString();
				XmlAttribute xmlAttribute = xmlDesc.CreateAttribute("id");
				xmlAttribute.Value = "G" + nGlassid.ToString(CultureInfo.CurrentCulture);
				xmlElement.SetAttributeNode(xmlAttribute);
				XmlAttribute xmlAttribute2 = xmlDesc.CreateAttribute("material");
				xmlAttribute2.Value = text;
				xmlElement.SetAttributeNode(xmlAttribute2);
				XmlAttribute xmlAttribute3 = xmlDesc.CreateAttribute("width");
				xmlAttribute3.Value = text3;
				xmlElement.SetAttributeNode(xmlAttribute3);
				XmlAttribute xmlAttribute4 = xmlDesc.CreateAttribute("height");
				xmlAttribute4.Value = text2;
				xmlElement.SetAttributeNode(xmlAttribute4);
				XmlAttribute xmlAttribute5 = xmlDesc.CreateAttribute("color");
				xmlAttribute5.Value = "";
				xmlElement.SetAttributeNode(xmlAttribute5);
				XmlAttribute xmlAttribute6 = xmlDesc.CreateAttribute("number");
				xmlAttribute6.Value = nGlassid.ToString(CultureInfo.CurrentCulture);
				xmlElement.SetAttributeNode(xmlAttribute6);
				XmlNode xmlNodeGM = xmlDesc.CreateNode(XmlNodeType.Element, "dsc:GeneratedMaterial", "ModelDescriptive");
				xmlElement.AppendChild(AddGeneratedMaterialGlass(xmlDesc, xmlNodeGM, text, text5, text3, text2));
				XmlNode xmlNode = xmlProfMem.SelectSingleNode("//GLASS[@GUID='" + text4 + "']/INNERCONTOUR/VERTICES");
				XmlNode xmlNode2 = xmlProfMem.SelectSingleNode("//GLASS[@GUID='" + text4 + "']/OUTERCONTOUR/VERTICES");
				XmlNodeList xmlNodeListVertices = xmlProfMem.SelectNodes("//GLASS[@GUID='" + text4 + "']/INNERCONTOUR/VERTICES/*");
				XmlNodeList xmlNodeListVertices2 = xmlProfMem.SelectNodes("//GLASS[@GUID='" + text4 + "']/OUTERCONTOUR/VERTICES/*");
				if (xmlNode == null || xmlNode2 == null)
				{
					nGlassid++;
					nGMid++;
					continue;
				}
				XmlNode xmlNodePolyline2D = xmlDesc.CreateNode(XmlNodeType.Element, "POLYLINE2D", "");
				xmlElement.AppendChild(AddPolyline2DGlass(xmlDesc, xmlNodePolyline2D, "0", xmlNode, xmlNodeListVertices, text4));
				XmlNode xmlNodePolyline2D2 = xmlDesc.CreateNode(XmlNodeType.Element, "POLYLINE2D", "");
				xmlElement.AppendChild(AddPolyline2DGlass(xmlDesc, xmlNodePolyline2D2, "1", xmlNode2, xmlNodeListVertices2, text4));
				XmlNode xmlNodeGlazingLedge = xmlDesc.CreateNode(XmlNodeType.Element, "dsc:GlazingLedge", "ModelDescriptive");
				xmlElement.AppendChild(AddGlazingLedge(xmlDesc, xmlNodeGlazingLedge));
				string firstMaterialForSashId = GetFirstMaterialForSashId(strSashGUID, strInsertionId, DataSet);
				xmlDesc = AddRod(xmlDesc, strPath + "/dsc:GlazingLedge", xmlnsManager, firstMaterialForSashId);
				xmlDesc = AddProfilePieces(xmlDesc, strPath + "/dsc:GlazingLedge", xmlnsManager, DataSet, strInsertionId, xmlProfMem, strSashGUID, bFirstSash: true, "", "", bGlazingLedge: true);
				nGlassid++;
			}
			return xmlDesc;
		}
		catch
		{
			return xmlDesc;
		}
	}

	private XmlNode AddGeneratedMaterialGlass(XmlDocument xmlDesc, XmlNode xmlNodeGM, string strReferencia, string strCantidad, string strAncho, string strAlto)
	{
		try
		{
			XmlAttribute xmlAttribute = xmlDesc.CreateAttribute("id");
			xmlAttribute.Value = "GM" + nGMid.ToString(CultureInfo.CurrentCulture);
			xmlNodeGM.Attributes.Append(xmlAttribute);
			XmlAttribute xmlAttribute2 = xmlDesc.CreateAttribute("material");
			xmlAttribute2.Value = strReferencia;
			xmlNodeGM.Attributes.Append(xmlAttribute2);
			XmlAttribute xmlAttribute3 = xmlDesc.CreateAttribute("count");
			xmlAttribute3.Value = strCantidad;
			xmlNodeGM.Attributes.Append(xmlAttribute3);
			XmlAttribute xmlAttribute4 = xmlDesc.CreateAttribute("glassNumber");
			xmlAttribute4.Value = nGlassid.ToString(CultureInfo.CurrentCulture);
			xmlNodeGM.Attributes.Append(xmlAttribute4);
			XmlAttribute xmlAttribute5 = xmlDesc.CreateAttribute("color");
			xmlAttribute5.Value = "";
			xmlNodeGM.Attributes.Append(xmlAttribute5);
			XmlAttribute xmlAttribute6 = xmlDesc.CreateAttribute("method");
			xmlAttribute6.Value = "self";
			xmlNodeGM.Attributes.Append(xmlAttribute6);
			XmlAttribute xmlAttribute7 = xmlDesc.CreateAttribute("unmounted");
			xmlAttribute7.Value = "0";
			xmlNodeGM.Attributes.Append(xmlAttribute7);
			XmlAttribute xmlAttribute8 = xmlDesc.CreateAttribute("width");
			xmlAttribute8.Value = strAncho;
			xmlNodeGM.Attributes.Append(xmlAttribute8);
			XmlAttribute xmlAttribute9 = xmlDesc.CreateAttribute("height");
			xmlAttribute9.Value = strAlto;
			xmlNodeGM.Attributes.Append(xmlAttribute9);
			XmlAttribute xmlAttribute10 = xmlDesc.CreateAttribute("pricingWidth");
			xmlAttribute10.Value = strAncho;
			xmlNodeGM.Attributes.Append(xmlAttribute10);
			XmlAttribute xmlAttribute11 = xmlDesc.CreateAttribute("pricingHeight");
			xmlAttribute11.Value = strAlto;
			xmlNodeGM.Attributes.Append(xmlAttribute11);
			nGMid++;
			return xmlNodeGM;
		}
		catch
		{
			return xmlNodeGM;
		}
	}

	private XmlNode AddGeneratedMaterialGasket(XmlDocument xmlDesc, XmlNode xmlNodeGM, string strReferencia, string strCantidad, string strLength, string strSquare, string strWeight)
	{
		try
		{
			XmlAttribute xmlAttribute = xmlDesc.CreateAttribute("id");
			xmlAttribute.Value = "GM" + nGMid.ToString(CultureInfo.CurrentCulture);
			xmlNodeGM.Attributes.Append(xmlAttribute);
			XmlAttribute xmlAttribute2 = xmlDesc.CreateAttribute("material");
			xmlAttribute2.Value = strReferencia;
			xmlNodeGM.Attributes.Append(xmlAttribute2);
			XmlAttribute xmlAttribute3 = xmlDesc.CreateAttribute("count");
			xmlAttribute3.Value = strCantidad;
			xmlNodeGM.Attributes.Append(xmlAttribute3);
			XmlAttribute xmlAttribute4 = xmlDesc.CreateAttribute("color");
			xmlAttribute4.Value = "";
			xmlNodeGM.Attributes.Append(xmlAttribute4);
			XmlAttribute xmlAttribute5 = xmlDesc.CreateAttribute("method");
			xmlAttribute5.Value = "Meter rule";
			xmlNodeGM.Attributes.Append(xmlAttribute5);
			XmlAttribute xmlAttribute6 = xmlDesc.CreateAttribute("unmounted");
			xmlAttribute6.Value = "0";
			xmlNodeGM.Attributes.Append(xmlAttribute6);
			XmlAttribute xmlAttribute7 = xmlDesc.CreateAttribute("square");
			xmlAttribute7.Value = "SQ" + strSquare;
			xmlNodeGM.Attributes.Append(xmlAttribute7);
			XmlAttribute xmlAttribute8 = xmlDesc.CreateAttribute("long");
			xmlAttribute8.Value = strLength;
			xmlNodeGM.Attributes.Append(xmlAttribute8);
			XmlAttribute xmlAttribute9 = xmlDesc.CreateAttribute("pricingWidth");
			xmlAttribute9.Value = strLength;
			xmlNodeGM.Attributes.Append(xmlAttribute9);
			XmlAttribute xmlAttribute10 = xmlDesc.CreateAttribute("length");
			xmlAttribute10.Value = strLength;
			xmlNodeGM.Attributes.Append(xmlAttribute10);
			XmlAttribute xmlAttribute11 = xmlDesc.CreateAttribute("weight");
			xmlAttribute11.Value = strWeight;
			xmlNodeGM.Attributes.Append(xmlAttribute11);
			nGMid++;
			return xmlNodeGM;
		}
		catch
		{
			return xmlNodeGM;
		}
	}

	private static XmlNode AddNodeDelimitationMatrixPolyline(XmlDocument xmlDesc, XmlDocument xmlProfMem, string strDelimitationGUID, string strOrientation)
	{
		try
		{
			string text = "";
			double num = 0.0;
			double num2 = 0.0;
			double num3 = 9999999.0;
			double num4 = 9999999.0;
			XmlNode xmlNode = xmlProfMem.SelectSingleNode("//PROFILE[@GUID='" + strDelimitationGUID + "']/PROXYFACES/FACE/VERTICES");
			XmlNodeList xmlNodeList = xmlProfMem.SelectNodes("//PROFILE[@GUID='" + strDelimitationGUID + "']/PROXYFACES/FACE/VERTICES/*");
			XmlNode xmlNode2 = xmlDesc.CreateNode(XmlNodeType.Element, "dsc:MatrixPolyline", "ModelDescriptive");
			XmlNode xmlNode3 = xmlDesc.CreateNode(XmlNodeType.Element, "POLYLINE2D", "");
			if (xmlNode != null)
			{
				XmlAttribute xmlAttribute = xmlDesc.CreateAttribute("filled");
				xmlAttribute.Value = "0";
				xmlNode3.Attributes.Append(xmlAttribute);
				if (strDelimitationGUID != "{00000000-0000-0000-0000-000000000000}")
				{
					XmlElement obj = (XmlElement)xmlNode;
					double num5 = Convert.ToDouble(obj.GetAttribute("SX"), CultureInfo.InvariantCulture);
					double num6 = Convert.ToDouble(obj.GetAttribute("SY"), CultureInfo.InvariantCulture);
					if (num5 > num)
					{
						num = num5;
					}
					if (num6 > num2)
					{
						num2 = num6;
					}
					if (num5 < num3)
					{
						num3 = num5;
					}
					if (num6 < num4)
					{
						num4 = num6;
					}
					foreach (XmlNode item in xmlNodeList)
					{
						num5 = Convert.ToDouble(((XmlElement)item).GetAttribute("X"), CultureInfo.InvariantCulture);
						num6 = Convert.ToDouble(((XmlElement)item).GetAttribute("Y"), CultureInfo.InvariantCulture);
						if (num5 > num)
						{
							num = num5;
						}
						if (num6 > num2)
						{
							num2 = num6;
						}
						if (num5 < num3)
						{
							num3 = num5;
						}
						if (num6 < num4)
						{
							num4 = num6;
						}
					}
					double num7 = (num - num3) / 2.0 + num3;
					double num8 = (num2 - num4) / 2.0 + num4;
					text = ((!(strOrientation == "horizontal")) ? ("P" + num7.ToString(CultureInfo.InvariantCulture) + " " + num4.ToString(CultureInfo.InvariantCulture) + "P" + num7.ToString(CultureInfo.InvariantCulture) + " " + num2.ToString(CultureInfo.InvariantCulture)) : ("P" + num3.ToString(CultureInfo.InvariantCulture) + " " + num8.ToString(CultureInfo.InvariantCulture) + "P" + num.ToString(CultureInfo.InvariantCulture) + " " + num8.ToString(CultureInfo.InvariantCulture)));
					XmlAttribute xmlAttribute2 = xmlDesc.CreateAttribute("path");
					xmlAttribute2.Value = text;
					xmlNode3.Attributes.Append(xmlAttribute2);
					xmlNode2.AppendChild(xmlNode3);
				}
			}
			return xmlNode2;
		}
		catch
		{
			return null;
		}
	}

	private XmlNode AddGlazingLedge(XmlDocument xmlDesc, XmlNode xmlNodeGlazingLedge)
	{
		try
		{
			XmlAttribute xmlAttribute = xmlDesc.CreateAttribute("id");
			xmlAttribute.Value = "GL" + nGlazingLedgeid.ToString(CultureInfo.CurrentCulture);
			xmlNodeGlazingLedge.Attributes.Append(xmlAttribute);
			nGlazingLedgeid++;
			return xmlNodeGlazingLedge;
		}
		catch
		{
			return xmlNodeGlazingLedge;
		}
	}

	private static string GetFirstMaterialForSashId(string strSashGUID, string strInsertionid, DataSet DataSet)
	{
		try
		{
			string result = "";
			DataRow[] array = DataSet.Tables["Perfiles"].Select("InsertionId='" + strInsertionid + "' AND xSashGUID='" + strSashGUID + "' AND PieceListType='4'");
			int num = 0;
			if (num < array.Length)
			{
				result = array[num]["ReferenciaFinal"].ToString();
			}
			array = DataSet.Tables["Perfiles"].Select("InsertionId='" + strInsertionid + "' AND xSashGUID='{00000000-0000-0000-0000-000000000000}' AND PieceListType='4'");
			num = 0;
			if (num < array.Length)
			{
				result = array[num]["ReferenciaFinal"].ToString();
			}
			return result;
		}
		catch
		{
			return "";
		}
	}

	private static XmlNode AddPolyline2DGlass(XmlDocument xmlDesc, XmlNode xmlNodePolyline2D, string strSide, XmlNode xmlNodeInitialPoints, XmlNodeList xmlNodeListVertices, string strXGUID)
	{
		XmlAttribute xmlAttribute = xmlDesc.CreateAttribute("filled");
		xmlAttribute.Value = "1";
		xmlNodePolyline2D.Attributes.Append(xmlAttribute);
		XmlAttribute xmlAttribute2 = xmlDesc.CreateAttribute("outer");
		xmlAttribute2.Value = strSide;
		xmlNodePolyline2D.Attributes.Append(xmlAttribute2);
		XmlAttribute xmlAttribute3 = xmlDesc.CreateAttribute("color");
		xmlAttribute3.Value = "";
		xmlNodePolyline2D.Attributes.Append(xmlAttribute3);
		string text = "";
		text = ((!(strXGUID != "{00000000-0000-0000-0000-000000000000}")) ? "" : GetPath(xmlNodeInitialPoints, xmlNodeListVertices));
		XmlAttribute xmlAttribute4 = xmlDesc.CreateAttribute("path");
		xmlAttribute4.Value = text;
		xmlNodePolyline2D.Attributes.Append(xmlAttribute4);
		XmlNode xmlNode = xmlDesc.CreateNode(XmlNodeType.Element, "draw:Pen", "ModelDraw");
		XmlAttribute xmlAttribute5 = xmlDesc.CreateAttribute("width");
		xmlAttribute5.Value = "0";
		xmlNode.Attributes.Append(xmlAttribute5);
		XmlAttribute xmlAttribute6 = xmlDesc.CreateAttribute("alpha");
		xmlAttribute6.Value = "0";
		xmlNode.Attributes.Append(xmlAttribute6);
		xmlNodePolyline2D.AppendChild(xmlNode);
		XmlNode xmlNode2 = xmlDesc.CreateNode(XmlNodeType.Element, "draw:Brush", "ModelDraw");
		XmlAttribute xmlAttribute7 = xmlDesc.CreateAttribute("type");
		xmlAttribute7.Value = "gradient";
		xmlNode2.Attributes.Append(xmlAttribute7);
		XmlAttribute xmlAttribute8 = xmlDesc.CreateAttribute("alpha");
		xmlAttribute8.Value = "0.79";
		xmlNode2.Attributes.Append(xmlAttribute8);
		XmlNode xmlNode3 = xmlDesc.CreateNode(XmlNodeType.Element, "draw:Vector", "ModelDraw");
		XmlAttribute xmlAttribute9 = xmlDesc.CreateAttribute("ax");
		xmlAttribute9.Value = "-20";
		xmlNode3.Attributes.Append(xmlAttribute9);
		XmlAttribute xmlAttribute10 = xmlDesc.CreateAttribute("ay");
		xmlAttribute10.Value = "180";
		xmlNode3.Attributes.Append(xmlAttribute10);
		XmlAttribute xmlAttribute11 = xmlDesc.CreateAttribute("bx");
		xmlAttribute11.Value = "1100";
		xmlNode3.Attributes.Append(xmlAttribute11);
		XmlAttribute xmlAttribute12 = xmlDesc.CreateAttribute("by");
		xmlAttribute12.Value = "740";
		xmlNode3.Attributes.Append(xmlAttribute12);
		xmlNode2.AppendChild(xmlNode3);
		XmlNode xmlNode4 = xmlDesc.CreateNode(XmlNodeType.Element, "draw:GradientSteps", "ModelDraw");
		XmlAttribute xmlAttribute13 = xmlDesc.CreateAttribute("steps");
		xmlAttribute13.Value = "80ffff 0.00 ffffff 0.40 80ffff 0.80 ";
		xmlNode4.Attributes.Append(xmlAttribute13);
		xmlNode2.AppendChild(xmlNode4);
		xmlNodePolyline2D.AppendChild(xmlNode2);
		return xmlNodePolyline2D;
	}

	private static string GetPath(XmlNode xmlNodeInitialPoint, XmlNodeList xmlNodeListPoints)
	{
		try
		{
			string text = "";
			string text2 = "";
			string text3 = "";
			XmlElement obj = (XmlElement)xmlNodeInitialPoint;
			text = obj.GetAttribute("SX").ToString();
			text2 = obj.GetAttribute("SY").ToString();
			text3 = "P" + text + " " + text2;
			foreach (XmlNode xmlNodeListPoint in xmlNodeListPoints)
			{
				text = ((XmlElement)xmlNodeListPoint).GetAttribute("X").ToString();
				text2 = ((XmlElement)xmlNodeListPoint).GetAttribute("Y").ToString();
				text3 = text3 + "P" + text + " " + text2;
			}
			return text3;
		}
		catch
		{
			return "";
		}
	}

	private XmlDocument AddOpening(XmlDocument xmlDesc, XmlNamespaceManager xmlnsManager, DataSet DataSet, string strNumeroInsertion, int nHoleId, string strSashGUID)
	{
		try
		{
			string text = "";
			string text2 = "";
			string text3 = "";
			string text4 = "";
			string text5 = "";
			string text6 = "";
			string text7 = "";
			string text8 = "";
			string value = "";
			string value2 = "";
			double num = (dGreaterProfilePieceX - dLowerProfilePieceX) / 2.0 + dLowerProfilePieceX;
			double dhalfGreaterY = (dGreaterProfilePieceY - dLowerProfilePieceY) / 2.0 + dLowerProfilePieceY;
			DataRow[] array = DataSet.Tables["Insertions"].Select("InsertionID='" + strNumeroInsertion + "'");
			foreach (DataRow obj in array)
			{
				text = obj["TypeNr"].ToString();
				text2 = obj["DIN"].ToString();
			}
			array = DataSet.Tables["Sashes"].Select("xGUID='" + strSashGUID + "'");
			foreach (DataRow obj2 in array)
			{
				text5 = obj2["SliderDirection"].ToString();
				text6 = obj2["DIN"].ToString();
				text7 = obj2["MasterLeaf"].ToString();
			}
			switch (text)
			{
			case "40":
			case "41":
			case "42":
			case "43":
			case "48":
			case "49":
			case "8":
				text8 = GetArrowPath(text5, dhalfGreaterY);
				text3 = ((!(text5 == "L")) ? ((!(text5 == "R")) ? "fix" : "right") : "left");
				break;
			case "35":
			case "36":
			case "37":
			case "38":
			case "39":
				text3 = ((text6 == "R") ? "right" : ((!(text6 == "L")) ? "fix" : "left"));
				break;
			default:
				text3 = ((text2 == "R") ? "right" : ((!(text2 == "L")) ? "fix" : "left"));
				break;
			}
			switch (text)
			{
			case "3":
				text4 = "tiltnturn";
				if (text3 == "right")
				{
					value = "P" + dGreaterProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dGreaterProfilePieceY.ToString(CultureInfo.InvariantCulture) + "P" + dLowerProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dhalfGreaterY.ToString(CultureInfo.InvariantCulture) + "P" + dGreaterProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dLowerProfilePieceY.ToString(CultureInfo.InvariantCulture) + "P" + num.ToString(CultureInfo.InvariantCulture) + " " + dGreaterProfilePieceY.ToString(CultureInfo.InvariantCulture) + "P" + dLowerProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dLowerProfilePieceY.ToString(CultureInfo.InvariantCulture) + "P" + dGreaterProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dLowerProfilePieceY.ToString(CultureInfo.InvariantCulture) + "P" + dGreaterProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dGreaterProfilePieceY.ToString(CultureInfo.InvariantCulture);
				}
				else if (text3 == "left")
				{
					value = "P" + dLowerProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dGreaterProfilePieceY.ToString(CultureInfo.InvariantCulture) + "P" + dGreaterProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dhalfGreaterY.ToString(CultureInfo.InvariantCulture) + "P" + dLowerProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dLowerProfilePieceY.ToString(CultureInfo.InvariantCulture) + "P" + num.ToString(CultureInfo.InvariantCulture) + " " + dGreaterProfilePieceY.ToString(CultureInfo.InvariantCulture) + "P" + dGreaterProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dLowerProfilePieceY.ToString(CultureInfo.InvariantCulture) + "P" + dLowerProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dLowerProfilePieceY.ToString(CultureInfo.InvariantCulture) + "P" + dLowerProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dGreaterProfilePieceY.ToString(CultureInfo.InvariantCulture);
				}
				break;
			case "4":
				text4 = "casement";
				if (text3 == "right")
				{
					value = "P" + dGreaterProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dGreaterProfilePieceY.ToString(CultureInfo.InvariantCulture) + "P" + dLowerProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dhalfGreaterY.ToString(CultureInfo.InvariantCulture) + "P" + dGreaterProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dLowerProfilePieceY.ToString(CultureInfo.InvariantCulture) + "P" + dGreaterProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dGreaterProfilePieceY.ToString(CultureInfo.InvariantCulture);
				}
				else if (text3 == "left")
				{
					value = "P" + dLowerProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dLowerProfilePieceY.ToString(CultureInfo.InvariantCulture) + "P" + dGreaterProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dhalfGreaterY.ToString(CultureInfo.InvariantCulture) + "P" + dLowerProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dGreaterProfilePieceY.ToString(CultureInfo.InvariantCulture) + "P" + dLowerProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dLowerProfilePieceY.ToString(CultureInfo.InvariantCulture);
				}
				break;
			case "5":
				text4 = "tiltnturn";
				value = "P" + dLowerProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dLowerProfilePieceY.ToString(CultureInfo.InvariantCulture) + "P" + num.ToString(CultureInfo.InvariantCulture) + " " + dGreaterProfilePieceY.ToString(CultureInfo.InvariantCulture) + "P" + dGreaterProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dLowerProfilePieceY.ToString(CultureInfo.InvariantCulture) + "P" + dLowerProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dLowerProfilePieceY.ToString(CultureInfo.InvariantCulture);
				break;
			case "8":
				text4 = "tiltnturn";
				value = "P" + dLowerProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dLowerProfilePieceY.ToString(CultureInfo.InvariantCulture) + "P" + num.ToString(CultureInfo.InvariantCulture) + " " + dGreaterProfilePieceY.ToString(CultureInfo.InvariantCulture) + "P" + dGreaterProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dLowerProfilePieceY.ToString(CultureInfo.InvariantCulture) + "P" + dLowerProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dLowerProfilePieceY.ToString(CultureInfo.InvariantCulture);
				value2 = text8;
				if (text3 == "right")
				{
					text3 = "left";
				}
				else if (text3 == "left")
				{
					text3 = "right";
				}
				break;
			case "20":
				text4 = "casement";
				if (text3 == "right")
				{
					value = "P" + dGreaterProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dGreaterProfilePieceY.ToString(CultureInfo.InvariantCulture) + "P" + dLowerProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dhalfGreaterY.ToString(CultureInfo.InvariantCulture) + "P" + dGreaterProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dLowerProfilePieceY.ToString(CultureInfo.InvariantCulture) + "P" + dGreaterProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dGreaterProfilePieceY.ToString(CultureInfo.InvariantCulture);
				}
				else if (text3 == "left")
				{
					value = "P" + dLowerProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dLowerProfilePieceY.ToString(CultureInfo.InvariantCulture) + "P" + dGreaterProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dhalfGreaterY.ToString(CultureInfo.InvariantCulture) + "P" + dLowerProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dGreaterProfilePieceY.ToString(CultureInfo.InvariantCulture) + "P" + dLowerProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dLowerProfilePieceY.ToString(CultureInfo.InvariantCulture);
				}
				break;
			case "21":
			{
				text4 = "casement";
				double num2 = dLowerProfilePieceX + 50.0;
				double num3 = dGreaterProfilePieceX - 50.0;
				double num4 = dLowerProfilePieceY + 50.0;
				double num5 = dGreaterProfilePieceY - 50.0;
				if (text3 == "left")
				{
					value = "P" + dGreaterProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dGreaterProfilePieceY.ToString(CultureInfo.InvariantCulture) + "P" + dLowerProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dhalfGreaterY.ToString(CultureInfo.InvariantCulture) + "P" + dGreaterProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dLowerProfilePieceY.ToString(CultureInfo.InvariantCulture) + "P" + dGreaterProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + num4.ToString(CultureInfo.InvariantCulture) + "P" + num2.ToString(CultureInfo.InvariantCulture) + " " + dhalfGreaterY.ToString(CultureInfo.InvariantCulture) + "P" + dGreaterProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + num5.ToString(CultureInfo.InvariantCulture) + "P" + dGreaterProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dGreaterProfilePieceY.ToString(CultureInfo.InvariantCulture);
				}
				else if (text3 == "right")
				{
					value = "P" + dLowerProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dLowerProfilePieceY.ToString(CultureInfo.InvariantCulture) + "P" + dGreaterProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dhalfGreaterY.ToString(CultureInfo.InvariantCulture) + "P" + dLowerProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dGreaterProfilePieceY.ToString(CultureInfo.InvariantCulture) + "P" + dLowerProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + num5.ToString(CultureInfo.InvariantCulture) + "P" + num3.ToString(CultureInfo.InvariantCulture) + " " + dhalfGreaterY.ToString(CultureInfo.InvariantCulture) + "P" + dLowerProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + num4.ToString(CultureInfo.InvariantCulture) + "P" + dLowerProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dLowerProfilePieceY.ToString(CultureInfo.InvariantCulture);
				}
				break;
			}
			case "35":
				text4 = "casement";
				if (text3 == "right")
				{
					value = "P" + dGreaterProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dGreaterProfilePieceY.ToString(CultureInfo.InvariantCulture) + "P" + dLowerProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dhalfGreaterY.ToString(CultureInfo.InvariantCulture) + "P" + dGreaterProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dLowerProfilePieceY.ToString(CultureInfo.InvariantCulture) + "P" + dGreaterProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dGreaterProfilePieceY.ToString(CultureInfo.InvariantCulture);
				}
				else if (text3 == "left")
				{
					value = "P" + dLowerProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dLowerProfilePieceY.ToString(CultureInfo.InvariantCulture) + "P" + dGreaterProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dhalfGreaterY.ToString(CultureInfo.InvariantCulture) + "P" + dLowerProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dGreaterProfilePieceY.ToString(CultureInfo.InvariantCulture) + "P" + dLowerProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dLowerProfilePieceY.ToString(CultureInfo.InvariantCulture);
				}
				break;
			case "36":
				if (text2 == text6)
				{
					text4 = "tiltnturn";
					if (text3 == "right")
					{
						value = "P" + dGreaterProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dGreaterProfilePieceY.ToString(CultureInfo.InvariantCulture) + "P" + dLowerProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dhalfGreaterY.ToString(CultureInfo.InvariantCulture) + "P" + dGreaterProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dLowerProfilePieceY.ToString(CultureInfo.InvariantCulture) + "P" + num.ToString(CultureInfo.InvariantCulture) + " " + dGreaterProfilePieceY.ToString(CultureInfo.InvariantCulture) + "P" + dLowerProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dLowerProfilePieceY.ToString(CultureInfo.InvariantCulture) + "P" + dGreaterProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dLowerProfilePieceY.ToString(CultureInfo.InvariantCulture) + "P" + dGreaterProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dGreaterProfilePieceY.ToString(CultureInfo.InvariantCulture);
					}
					else if (text3 == "left")
					{
						value = "P" + dLowerProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dGreaterProfilePieceY.ToString(CultureInfo.InvariantCulture) + "P" + dGreaterProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dhalfGreaterY.ToString(CultureInfo.InvariantCulture) + "P" + dLowerProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dLowerProfilePieceY.ToString(CultureInfo.InvariantCulture) + "P" + num.ToString(CultureInfo.InvariantCulture) + " " + dGreaterProfilePieceY.ToString(CultureInfo.InvariantCulture) + "P" + dGreaterProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dLowerProfilePieceY.ToString(CultureInfo.InvariantCulture) + "P" + dLowerProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dLowerProfilePieceY.ToString(CultureInfo.InvariantCulture) + "P" + dLowerProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dGreaterProfilePieceY.ToString(CultureInfo.InvariantCulture);
					}
				}
				else
				{
					text4 = "casement";
					if (text3 == "right")
					{
						value = "P" + dGreaterProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dGreaterProfilePieceY.ToString(CultureInfo.InvariantCulture) + "P" + dLowerProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dhalfGreaterY.ToString(CultureInfo.InvariantCulture) + "P" + dGreaterProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dLowerProfilePieceY.ToString(CultureInfo.InvariantCulture) + "P" + dGreaterProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dGreaterProfilePieceY.ToString(CultureInfo.InvariantCulture);
					}
					else if (text3 == "left")
					{
						value = "P" + dLowerProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dLowerProfilePieceY.ToString(CultureInfo.InvariantCulture) + "P" + dGreaterProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dhalfGreaterY.ToString(CultureInfo.InvariantCulture) + "P" + dLowerProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dGreaterProfilePieceY.ToString(CultureInfo.InvariantCulture) + "P" + dLowerProfilePieceX.ToString(CultureInfo.InvariantCulture) + " " + dLowerProfilePieceY.ToString(CultureInfo.InvariantCulture);
					}
				}
				break;
			case "40":
			case "41":
			case "42":
			case "43":
			case "48":
			case "49":
				text4 = "sliding";
				value = text8;
				break;
			default:
				text4 = "";
				break;
			}
			if (text3 != "fix")
			{
				xmlDesc = AddNode(xmlDesc, "dsc:Opening", "Model/dsc:Model/dsc:Contour/dsc:Hole/dsc:Hole/dsc:Hole[@id='H" + nHoleId + "']");
				XmlElement xmlElement = (XmlElement)xmlDesc.SelectSingleNode("Model/dsc:Model/dsc:Contour/dsc:Hole/dsc:Hole/dsc:Hole[@id='H" + nHoleId + "']/dsc:Opening", xmlnsManager);
				XmlAttribute xmlAttribute = xmlDesc.CreateAttribute("order");
				xmlAttribute.Value = "1E+099";
				xmlElement.SetAttributeNode(xmlAttribute);
				if (text7 == "True")
				{
					XmlAttribute xmlAttribute2 = xmlDesc.CreateAttribute("active");
					xmlAttribute2.Value = "1";
					xmlElement.SetAttributeNode(xmlAttribute2);
				}
				if (text != "5")
				{
					XmlAttribute xmlAttribute3 = xmlDesc.CreateAttribute(text3);
					xmlAttribute3.Value = "1";
					xmlElement.SetAttributeNode(xmlAttribute3);
				}
				XmlAttribute xmlAttribute4 = xmlDesc.CreateAttribute(text4);
				xmlAttribute4.Value = "1";
				xmlElement.SetAttributeNode(xmlAttribute4);
				if (text == "3")
				{
					XmlAttribute xmlAttribute5 = xmlDesc.CreateAttribute("casement");
					xmlAttribute5.Value = "1";
					xmlElement.SetAttributeNode(xmlAttribute5);
				}
				if (text == "5")
				{
					XmlAttribute xmlAttribute6 = xmlDesc.CreateAttribute("bottom");
					xmlAttribute6.Value = "1";
					xmlElement.SetAttributeNode(xmlAttribute6);
				}
				if (text == "8")
				{
					XmlAttribute xmlAttribute7 = xmlDesc.CreateAttribute("bottom");
					xmlAttribute7.Value = "1";
					xmlElement.SetAttributeNode(xmlAttribute7);
					XmlAttribute xmlAttribute8 = xmlDesc.CreateAttribute("sliding");
					xmlAttribute8.Value = "1";
					xmlElement.SetAttributeNode(xmlAttribute8);
				}
				if (text == "21")
				{
					XmlAttribute xmlAttribute9 = xmlDesc.CreateAttribute("outer");
					xmlAttribute9.Value = "1";
					xmlElement.SetAttributeNode(xmlAttribute9);
				}
				XmlNode xmlNode = xmlDesc.CreateNode(XmlNodeType.Element, "POLYLINE2D", "");
				XmlAttribute xmlAttribute10 = xmlDesc.CreateAttribute("filled");
				xmlAttribute10.Value = "1";
				xmlNode.Attributes.Append(xmlAttribute10);
				XmlAttribute xmlAttribute11 = xmlDesc.CreateAttribute("path");
				xmlAttribute11.Value = value;
				xmlNode.Attributes.Append(xmlAttribute11);
				XmlAttribute xmlAttribute12 = xmlDesc.CreateAttribute("smoothing");
				xmlAttribute12.Value = "1";
				xmlNode.Attributes.Append(xmlAttribute12);
				XmlNode xmlNode2 = xmlDesc.CreateNode(XmlNodeType.Element, "draw:Pen", "ModelDraw");
				XmlAttribute xmlAttribute13 = xmlDesc.CreateAttribute("rgb");
				xmlAttribute13.Value = "000080";
				xmlNode2.Attributes.Append(xmlAttribute13);
				XmlAttribute xmlAttribute14 = xmlDesc.CreateAttribute("width");
				xmlAttribute14.Value = "1";
				xmlNode2.Attributes.Append(xmlAttribute14);
				xmlNode.AppendChild(xmlNode2);
				xmlElement.AppendChild(xmlNode);
				if (text == "8")
				{
					XmlNode xmlNode3 = xmlDesc.CreateNode(XmlNodeType.Element, "POLYLINE2D", "");
					XmlAttribute xmlAttribute15 = xmlDesc.CreateAttribute("filled");
					xmlAttribute15.Value = "1";
					xmlNode3.Attributes.Append(xmlAttribute15);
					XmlAttribute xmlAttribute16 = xmlDesc.CreateAttribute("path");
					xmlAttribute16.Value = value2;
					xmlNode3.Attributes.Append(xmlAttribute16);
					XmlAttribute xmlAttribute17 = xmlDesc.CreateAttribute("smoothing");
					xmlAttribute17.Value = "1";
					xmlNode3.Attributes.Append(xmlAttribute17);
					XmlNode xmlNode4 = xmlDesc.CreateNode(XmlNodeType.Element, "draw:Pen", "ModelDraw");
					XmlAttribute xmlAttribute18 = xmlDesc.CreateAttribute("rgb");
					xmlAttribute18.Value = "000080";
					xmlNode4.Attributes.Append(xmlAttribute18);
					XmlAttribute xmlAttribute19 = xmlDesc.CreateAttribute("width");
					xmlAttribute19.Value = "1";
					xmlNode4.Attributes.Append(xmlAttribute19);
					xmlNode3.AppendChild(xmlNode4);
					xmlElement.AppendChild(xmlNode3);
				}
			}
			return xmlDesc;
		}
		catch
		{
			return xmlDesc;
		}
	}

	private string GetArrowPath(string strSliderDirection, double dhalfGreaterY)
	{
		try
		{
			double num = 0.0;
			double num2 = 0.0;
			double num3 = 0.0;
			double num4 = 0.0;
			double num5 = 0.0;
			double num6 = 0.0;
			double num7 = 0.0;
			double num8 = 0.0;
			double num9 = 0.0;
			double num10 = 0.0;
			double num11 = 0.0;
			double num12 = 0.0;
			double num13 = 0.0;
			double num14 = 0.0;
			if (strSliderDirection == "R")
			{
				num = dLowerProfilePieceX + 100.0;
				num2 = dhalfGreaterY + 8.0;
				num3 = dLowerProfilePieceX + 100.0 + 105.0;
				num4 = dhalfGreaterY + 8.0;
				num5 = dLowerProfilePieceX + 100.0 + 105.0;
				num6 = dhalfGreaterY + 8.0 + 22.0;
				num7 = dLowerProfilePieceX + 100.0 + 105.0 + 45.0;
				num8 = dhalfGreaterY;
				num9 = dLowerProfilePieceX + 100.0 + 105.0;
				num10 = dhalfGreaterY - 22.0 - 8.0;
				num11 = dLowerProfilePieceX + 100.0 + 105.0;
				num12 = dhalfGreaterY - 8.0;
				num13 = dLowerProfilePieceX + 100.0;
				num14 = dhalfGreaterY - 8.0;
			}
			else if (strSliderDirection == "L")
			{
				num = dGreaterProfilePieceX - 100.0;
				num2 = dhalfGreaterY + 8.0;
				num3 = dGreaterProfilePieceX - 100.0 - 105.0;
				num4 = dhalfGreaterY + 8.0;
				num5 = dGreaterProfilePieceX - 100.0 - 105.0;
				num6 = dhalfGreaterY + 8.0 + 22.0;
				num7 = dGreaterProfilePieceX - 100.0 - 105.0 - 45.0;
				num8 = dhalfGreaterY;
				num9 = dGreaterProfilePieceX - 100.0 - 105.0;
				num10 = dhalfGreaterY - 22.0 - 8.0;
				num11 = dGreaterProfilePieceX - 100.0 - 105.0;
				num12 = dhalfGreaterY - 8.0;
				num13 = dGreaterProfilePieceX - 100.0;
				num14 = dhalfGreaterY - 8.0;
			}
			return "P" + num.ToString(CultureInfo.InvariantCulture) + " " + num2.ToString(CultureInfo.InvariantCulture) + "P" + num3.ToString(CultureInfo.InvariantCulture) + " " + num4.ToString(CultureInfo.InvariantCulture) + "P" + num5.ToString(CultureInfo.InvariantCulture) + " " + num6.ToString(CultureInfo.InvariantCulture) + "P" + num7.ToString(CultureInfo.InvariantCulture) + " " + num8.ToString(CultureInfo.InvariantCulture) + "P" + num9.ToString(CultureInfo.InvariantCulture) + " " + num10.ToString(CultureInfo.InvariantCulture) + "P" + num11.ToString(CultureInfo.InvariantCulture) + " " + num12.ToString(CultureInfo.InvariantCulture) + "P" + num13.ToString(CultureInfo.InvariantCulture) + " " + num14.ToString(CultureInfo.InvariantCulture) + "P" + num.ToString(CultureInfo.InvariantCulture) + " " + num2.ToString(CultureInfo.InvariantCulture);
		}
		catch
		{
			return "";
		}
	}

	private static string RemoveFirstLine(string strOriginalProfMemXML)
	{
		try
		{
			string[] separator = new string[1] { "\r" };
			string[] array = strOriginalProfMemXML.Split(separator, StringSplitOptions.None);
			return strOriginalProfMemXML.Remove(0, array[0].Length);
		}
		catch
		{
			return strOriginalProfMemXML;
		}
	}

	private static string GetWidthDimension(OleDbConnection dbConnection)
	{
		using OleDbCommand oleDbCommand = new OleDbCommand("SELECT Width_Output FROM Elevations", dbConnection);
		return oleDbCommand.ExecuteScalar().ToString();
	}

	private static string GetHeightDimension(OleDbConnection dbConnection)
	{
		using OleDbCommand oleDbCommand = new OleDbCommand("SELECT Height_Output FROM Elevations", dbConnection);
		return oleDbCommand.ExecuteScalar().ToString();
	}

	private static string GetModelColor(OleDbConnection dbConnection)
	{
		using OleDbCommand oleDbCommand = new OleDbCommand("SELECT ColorBase FROM Elevations", dbConnection);
		return oleDbCommand.ExecuteScalar().ToString();
	}

	private static string GetProfMemXML(OleDbConnection dbConnection)
	{
		try
		{
			bool num = CheckColumnExists(dbConnection, "Elevations", "ProfileMemBlobIsZipped");
			string text = "";
			string result = "";
			if (num)
			{
				using OleDbCommand oleDbCommand = new OleDbCommand("SELECT ProfileMemBlobIsZipped FROM Elevations", dbConnection);
				text = oleDbCommand.ExecuteScalar().ToString();
			}
			using (OleDbCommand oleDbCommand2 = new OleDbCommand("SELECT ProfileMemBlob FROM Elevations", dbConnection))
			{
				byte[] array = oleDbCommand2.ExecuteScalar() as byte[];
				if (text != "True")
				{
					byte[] bytes = Encoding.Convert(Encoding.Unicode, Encoding.UTF8, array);
					result = Encoding.UTF8.GetString(bytes);
				}
				else
				{
					string text2 = "";
					string text3 = Path.GetTempPath() + "ProfMemZipFile.zip";
					using (FileStream fileStream = new FileStream(text3, FileMode.Create, FileAccess.Write))
					{
						fileStream.Write(array, 0, array.Length);
					}
					foreach (ZipPackageEntry zipPackageEntry in ZipPackage.OpenFile(text3, FileAccess.Read).get_ZipPackageEntries())
					{
						using Stream stream = zipPackageEntry.OpenInputStream();
						byte[] array2 = new byte[zipPackageEntry.get_UncompressedSize()];
						stream.Read(array2, 0, zipPackageEntry.get_UncompressedSize());
						text2 = new UnicodeEncoding().GetString(array2);
					}
					if (File.Exists(text3))
					{
						File.Delete(text3);
					}
					result = text2;
				}
			}
			return result;
		}
		catch
		{
			return "";
		}
	}

	public static bool CheckColumnExists(OleDbConnection dbConnection, string strTable, string strColumn)
	{
		if (dbConnection.GetSchema("COLUMNS").Select("TABLE_NAME='" + strTable + "'AND COLUMN_NAME='" + strColumn + "'").Length != 0)
		{
			return true;
		}
		return false;
	}
}
