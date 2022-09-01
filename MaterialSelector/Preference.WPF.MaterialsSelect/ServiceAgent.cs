using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Xml;
using Preference.Data.SqlClient;
using Preference.Diagnostics;

namespace Preference.WPF.MaterialsSelector.Core;

public class ServiceAgent : IServiceAgent
{
	private string _strConnectionString = string.Empty;

	private string _strADOConnectionString = string.Empty;

	private bool _bIsDocumentLoaded;

	private UnitsMode _UnitsMode;

	private XmlDocument _xmldoc;

	private XmlNamespaceManager _nsmgr;

	public UnitsMode UnitsMode
	{
		get
		{
			return _UnitsMode;
		}
		set
		{
			_UnitsMode = value;
		}
	}

	public bool IsDocumentLoaded => _bIsDocumentLoaded;

	public XmlDocument XmlDocument => _xmldoc;

	public XmlNamespaceManager XmlNamespaceManager => _nsmgr;

	public string ConnectionString
	{
		get
		{
			return _strConnectionString;
		}
		set
		{
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0013: Expected O, but got Unknown
			_strConnectionString = value;
			SqlConnectionStringBuilder val = new SqlConnectionStringBuilder(_strConnectionString);
			_strADOConnectionString = val.get_ADOConnectionString();
		}
	}

	public string XmlDescriptive
	{
		get
		{
			if (_xmldoc != null)
			{
				return _xmldoc.OuterXml;
			}
			return null;
		}
		set
		{
			CreateXmlDocument(value, out _xmldoc, out _nsmgr);
			_bIsDocumentLoaded = true;
		}
	}

	public List<string> GetDummyReferences()
	{
		try
		{
			List<string> list = new List<string>();
			string text = "select ReferenciaBase from MaterialesBase where IsDummy=1";
			foreach (DataRow row in SqlHelper.ExecuteDataset(_strConnectionString, CommandType.Text, text).Tables[0].Rows)
			{
				string item = Convert.ToString(row["ReferenciaBase"]).Trim();
				list.Add(item);
			}
			return list;
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefGest");
			throw;
		}
	}

	public string GetName()
	{
		try
		{
			string xpath = "/Model/dsc:Model";
			XmlNode xmlNode = _xmldoc.SelectSingleNode(xpath, _nsmgr);
			if (xmlNode != null && xmlNode.Attributes.GetNamedItem("name") != null)
			{
				return xmlNode.Attributes.GetNamedItem("name").Value;
			}
			return null;
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefCIM");
			throw;
		}
	}

	public string GetSystem()
	{
		try
		{
			string xpath = "/Model/dsc:Model";
			XmlNode xmlNode = _xmldoc.SelectSingleNode(xpath, _nsmgr);
			if (xmlNode != null && xmlNode.Attributes.GetNamedItem("system") != null)
			{
				return xmlNode.Attributes.GetNamedItem("system").Value;
			}
			return null;
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefCIM");
			throw;
		}
	}

	public string GetDescription()
	{
		try
		{
			string xpath = "/Model/dsc:Model";
			XmlNode xmlNode = _xmldoc.SelectSingleNode(xpath, _nsmgr);
			if (xmlNode != null && xmlNode.Attributes.GetNamedItem("description") != null)
			{
				return xmlNode.Attributes.GetNamedItem("description").Value;
			}
			return null;
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefCIM");
			throw;
		}
	}

	public string GetColor()
	{
		try
		{
			string xpath = "/Model/dsc:Model";
			XmlNode xmlNode = _xmldoc.SelectSingleNode(xpath, _nsmgr);
			if (xmlNode != null && xmlNode.Attributes.GetNamedItem("color") != null)
			{
				return xmlNode.Attributes.GetNamedItem("color").Value;
			}
			return null;
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefCIM");
			throw;
		}
	}

	public string GetFamily()
	{
		try
		{
			string xpath = "/Model/dsc:Model";
			XmlNode xmlNode = _xmldoc.SelectSingleNode(xpath, _nsmgr);
			if (xmlNode != null && xmlNode.Attributes.GetNamedItem("family") != null)
			{
				return xmlNode.Attributes.GetNamedItem("family").Value;
			}
			return null;
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefCIM");
			throw;
		}
	}

	public string GetProductType()
	{
		try
		{
			string xpath = "/Model/dsc:Model";
			XmlNode xmlNode = _xmldoc.SelectSingleNode(xpath, _nsmgr);
			if (xmlNode != null && xmlNode.Attributes.GetNamedItem("productType") != null)
			{
				return xmlNode.Attributes.GetNamedItem("productType").Value;
			}
			return null;
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefCIM");
			throw;
		}
	}

	public string GetModelStructureIDs()
	{
		try
		{
			XmlAttribute xmlAttribute = null;
			XmlDocument xmlDocument = new XmlDocument();
			XmlElement xmlElement = xmlDocument.CreateElement("ModelDrawing");
			xmlDocument.AppendChild(xmlElement);
			XmlElement xmlElement2 = xmlDocument.CreateElement("Model");
			xmlElement.AppendChild(xmlElement2);
			XmlElement xmlElement3 = xmlDocument.CreateElement("Squares");
			xmlElement2.AppendChild(xmlElement3);
			string xpath = "descendant::dsc:Square[not(@parentSquare)]";
			foreach (XmlNode item in _xmldoc.SelectNodes(xpath, _nsmgr))
			{
				CreateElemSquare(_nsmgr, xmlDocument, xmlElement3, _xmldoc, item);
			}
			XmlElement xmlElement4 = xmlDocument.CreateElement("SectionNodes");
			xmlElement2.AppendChild(xmlElement4);
			xpath = "/Model/dsc:Model/dsc:FrameNodes/dsc:FrameNode";
			foreach (XmlNode item2 in _xmldoc.SelectNodes(xpath, _nsmgr))
			{
				string value = item2.Attributes.GetNamedItem("equivalenceClass").Value;
				xpath = $"/ModelDrawing/Model/SectionNodes/SectionNode[@id = '{value}']";
				XmlNode xmlNode = xmlDocument.SelectSingleNode(xpath);
				if (xmlNode == null)
				{
					XmlElement xmlElement5 = xmlDocument.CreateElement("SectionNode");
					xmlElement4.AppendChild(xmlElement5);
					xmlAttribute = xmlDocument.CreateAttribute("id");
					xmlAttribute.Value = value;
					xmlElement5.Attributes.SetNamedItem(xmlAttribute);
					xmlNode = xmlElement5;
				}
				string value2 = item2.Attributes.GetNamedItem("idPiece").Value;
				xpath = $"child::Piece[@id = {value2}]";
				if (xmlNode.SelectSingleNode(xpath) == null)
				{
					XmlElement xmlElement6 = xmlDocument.CreateElement("Piece");
					xmlNode.AppendChild(xmlElement6);
					xmlAttribute = xmlDocument.CreateAttribute("id");
					xmlAttribute.Value = value2;
					xmlElement6.Attributes.SetNamedItem(xmlAttribute);
				}
			}
			return xmlDocument.OuterXml;
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefCIM");
			throw;
		}
	}

	public string GetWpfDrawing()
	{
		try
		{
			return InvokeGetWPFDrawing(null);
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefCIM");
			return null;
		}
	}

	public string GetWpfDrawing(string itemID)
	{
		try
		{
			string text = string.Empty;
			if (itemID.Substring(0, 2) == "SQ")
			{
				text = $"<VisualProperties><ModelVisualProperties view='inner' textSize='6' scale='10' printWidth='60' printHeight='70' printQuality='metafile'><DimensionProperties><RadiusDimensions visible='1'/><AngularDimensions visible='1'/></DimensionProperties><SquaresVisualProperties><SquareGlasses visible='0'/><SquareSticks visible='0'/><SquareDimensions visible='1' type='ToAxis'/><SquareSticksDimensions visible='0'/><SquareDelimitersDimensions visible='0'/></SquaresVisualProperties><OtherProperties><Openings visible='1'/></OtherProperties></ModelVisualProperties><ModelReportProperties><ElementsProperties visible='0'/><SquaresProperties visible='0'><SquareProperties id='{itemID}' visible='1' squareCategory='SquareCategoryName'/></SquaresProperties></ModelReportProperties></VisualProperties>";
			}
			else if (itemID.Substring(0, 1) == "G")
			{
				text = "<VisualProperties><ModelVisualProperties view='inner' textSize='6' scale='10' printWidth='60' printHeight='70' printQuality='metafile'><DimensionProperties><RadiusDimensions visible='1'/><AngularDimensions visible='1'/></DimensionProperties><GlassesProperties><Glasses visible='1'/><GlassesDimensions visible='1'/><GlazingLedge visible='0' /></GlassesProperties><RolePieceIdProperties visible='1' Role='ALL'/></ModelVisualProperties><ModelReportProperties><ElementsProperties visible='0'><ElementProperties id='" + itemID + "' visible='1'/></ElementsProperties><SquaresProperties visible='0'/></ModelReportProperties></VisualProperties>";
			}
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			return InvokeGetWPFDrawing(text);
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefCIM");
			return null;
		}
	}

	public byte[] GetImageAsPng(string reference, int width, int height)
	{
		try
		{
			string text = $"SELECT [zlib].[UnzipBLOB](MB.Jpeg) AS Jpeg FROM Materiales M INNER JOIN MaterialesBase MB ON MB.referenciaBase=M.ReferenciaBase WHERE M.Referencia = '{reference}'";
			DataSet dataSet = SqlHelper.ExecuteDataset(_strConnectionString, CommandType.Text, text);
			if (dataSet.Tables[0].Rows.Count == 0)
			{
				return null;
			}
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			if (dataRow.IsNull("Jpeg"))
			{
				return null;
			}
			byte[] inputBuffer = (byte[])dataRow["Jpeg"];
			return ConvertImage(inputBuffer, ImageFormat.Png, width, height);
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefCIM");
			throw;
		}
	}

	private string InvokeGetWPFDrawing(string visualproperties)
	{
		try
		{
			object obj = LateBinding.CreateObjectFromCLSID(new Guid("C530FCFA-D2F5-42D8-806A-67CBD25A9815"));
			LateBinding.SetProperty(obj, "ConnectionString", _strADOConnectionString);
			LateBinding.InvokeMethod(obj, "SetXMLDraw", _xmldoc.OuterXml);
			if (!string.IsNullOrEmpty(visualproperties))
			{
				LateBinding.InvokeMethod(obj, "SetXMLVisualProperties", visualproperties);
			}
			LateBinding.SetProperty(obj, "UnitsMode", Convert.ToInt32(_UnitsMode));
			return Convert.ToString(LateBinding.InvokeMethod(obj, "GetWPFDrawing"));
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefCIM");
			return null;
		}
	}

	private void CreateElemSquare(XmlNamespaceManager nsmgr, XmlDocument newXmlDoc, XmlElement elemSquares, XmlDocument xmldoc, XmlNode nodeSquare)
	{
		try
		{
			if (nodeSquare == null)
			{
				return;
			}
			string value = nodeSquare.Attributes.GetNamedItem("id").Value;
			XmlElement xmlElement = newXmlDoc.CreateElement("Square");
			elemSquares.AppendChild(xmlElement);
			XmlAttribute xmlAttribute = newXmlDoc.CreateAttribute("id");
			xmlAttribute.Value = value;
			xmlElement.Attributes.SetNamedItem(xmlAttribute);
			string empty = string.Empty;
			string xpath = "ancestor::dsc:GlazingLedge";
			if (nodeSquare.SelectSingleNode(xpath, nsmgr) != null)
			{
				empty = "GLAZING STOP";
			}
			else
			{
				xpath = "ancestor::dsc:Hole/child::dsc:Opening";
				empty = ((nodeSquare.SelectSingleNode(xpath, nsmgr) == null) ? "FRAME" : "SASH");
			}
			xmlAttribute = newXmlDoc.CreateAttribute("role");
			xmlAttribute.Value = empty;
			xmlElement.Attributes.SetNamedItem(xmlAttribute);
			XmlElement xmlElement2 = newXmlDoc.CreateElement("Tags");
			xmlElement.AppendChild(xmlElement2);
			xpath = "(";
			xpath += "descendant::dsc:ProfilePiece";
			xpath += $"/child::dsc:GeneratedMaterial[@method = 'self' and @square = '{value}']";
			xpath += "/parent::dsc:ProfilePiece";
			xpath += " | ";
			xpath += $"/descendant::dsc:Square[@id = '{value}']";
			xpath += "/parent::dsc:Rod";
			xpath += "/child::dsc:ProfilePiece";
			xpath += ")";
			XmlNodeList xmlNodeList = xmldoc.SelectNodes(xpath, nsmgr);
			if (xmlNodeList.Count > 0)
			{
				for (int i = 0; i < xmlNodeList.Count; i++)
				{
					XmlNode xmlNode = xmlNodeList[i];
					XmlElement xmlElement3 = newXmlDoc.CreateElement("Tag");
					xmlElement2.AppendChild(xmlElement3);
					xmlAttribute = newXmlDoc.CreateAttribute("name");
					xmlAttribute.Value = xmlNode.Attributes.GetNamedItem("id").Value;
					xmlElement3.Attributes.SetNamedItem(xmlAttribute);
				}
			}
			xpath = "descendant::dsc:Square[@parentSquare = '" + value + "']";
			XmlNodeList xmlNodeList2 = xmldoc.SelectNodes(xpath, nsmgr);
			if (xmlNodeList2.Count <= 0)
			{
				return;
			}
			XmlElement xmlElement4 = newXmlDoc.CreateElement("Squares");
			xmlElement.AppendChild(xmlElement4);
			foreach (XmlNode item in xmlNodeList2)
			{
				CreateElemSquare(nsmgr, newXmlDoc, xmlElement4, xmldoc, item);
			}
		}
		catch (Exception ex)
		{
			Logger.Instance.WriteError(ex, "PrefCIM");
			throw;
		}
	}

	private void CreateXmlDocument(string xmldescriptive, out XmlDocument xmldoc, out XmlNamespaceManager nsmgr)
	{
		xmldoc = new XmlDocument();
		xmldoc.LoadXml(xmldescriptive);
		nsmgr = new XmlNamespaceManager(xmldoc.NameTable);
		nsmgr.AddNamespace("draw", "ModelDraw");
		nsmgr.AddNamespace("dsc", "ModelDescriptive");
		nsmgr.AddNamespace("thd", "Model3D");
		string xpath = "/Model/dsc:Model/draw:AfterDrawEvent";
		XmlNode xmlNode = xmldoc.SelectSingleNode(xpath, nsmgr);
		xmlNode?.ParentNode.RemoveChild(xmlNode);
	}

	public byte[] ConvertImage(byte[] inputBuffer, ImageFormat destFormat, int thumbWidth, int thumbHeight)
	{
		using MemoryStream stream = new MemoryStream(inputBuffer);
		using MemoryStream memoryStream = new MemoryStream();
		Bitmap bitmap = null;
		Metafile metafile = null;
		try
		{
			Image image = Image.FromStream(stream);
			if (image is Metafile)
			{
				metafile = image as Metafile;
				Bitmap bitmap2 = new Bitmap(metafile, thumbWidth, thumbHeight);
				Graphics graphics = Graphics.FromImage(bitmap2);
				graphics.Clear(Color.White);
				graphics.DrawImage(metafile, 0, 0, thumbWidth, thumbWidth);
				bitmap = bitmap2.GetThumbnailImage(thumbWidth, thumbHeight, ThumbnailCallback, IntPtr.Zero) as Bitmap;
				bitmap.Save(memoryStream, destFormat);
			}
			else if (image is Bitmap)
			{
				bitmap = image.GetThumbnailImage(thumbWidth, thumbHeight, ThumbnailCallback, IntPtr.Zero) as Bitmap;
				bitmap.Save(memoryStream, destFormat);
			}
		}
		catch (ArgumentException)
		{
			metafile = null;
			bitmap = null;
		}
		catch (Exception)
		{
			metafile = null;
			bitmap = null;
		}
		if (bitmap != null || metafile != null)
		{
			return memoryStream.GetBuffer();
		}
		return null;
	}

	private bool ThumbnailCallback()
	{
		return false;
	}
}
