using System;
using System.Globalization;
using System.Xml;
using Preference.PrefGS;
using Preference.Wpf.Controls.Properties;

namespace Preference.Wpf.Controls;

internal class BayAndBowHelpers
{
	public static string GetFrontAndFlankerDimension(double dWidth, double dDepth, double dAngle, ref double dFront, ref double dFlanker)
	{
		dFront = (dFlanker = 0.0);
		if (dWidth <= 0.0)
		{
			return Resources.ResourceManager.GetString("ErrorWidth");
		}
		if (dDepth <= 0.0)
		{
			return Resources.ResourceManager.GetString("ErrorDepth");
		}
		if (dAngle < 90.0 || dAngle >= 180.0)
		{
			return Resources.ResourceManager.GetString("ErrorAngle");
		}
		double num = dAngle * Math.PI / 180.0;
		dFlanker = dDepth / Math.Sin(Math.PI - num);
		double num2 = Math.Cos(Math.PI - num) * dFlanker;
		dFront = dWidth - num2 * 2.0;
		if (dFront <= 0.0)
		{
			return Resources.ResourceManager.GetString("ErrorFront");
		}
		return "";
	}

	public static string GetAngleAndFlankerDimension(double dWidth, double dDepth, double dFront, ref double dAngle, ref double dFlanker)
	{
		dAngle = (dFlanker = 0.0);
		if (dWidth <= 0.0)
		{
			return Resources.ResourceManager.GetString("ErrorWidth");
		}
		if (dDepth <= 0.0)
		{
			return Resources.ResourceManager.GetString("ErrorDepth");
		}
		if (dFront <= 0.0)
		{
			return Resources.ResourceManager.GetString("ErrorFront");
		}
		if (dFront > dWidth)
		{
			return Resources.ResourceManager.GetString("ErrorFront");
		}
		double num = (dWidth - dFront) / 2.0;
		dAngle = Math.PI - Math.Atan(dDepth / num);
		dFlanker = dDepth / Math.Sin(Math.PI - dAngle);
		dAngle = dAngle * 180.0 / Math.PI;
		return "";
	}

	public static string GetAngleAndFrontDimension(double dWidth, double dDepth, double dFlanker, ref double dAngle, ref double dFront)
	{
		dAngle = (dFront = 0.0);
		if (dWidth <= 0.0)
		{
			return Resources.ResourceManager.GetString("ErrorWidth");
		}
		if (dDepth <= 0.0)
		{
			return Resources.ResourceManager.GetString("ErrorDepth");
		}
		if (dFlanker <= 0.0)
		{
			return Resources.ResourceManager.GetString("ErrorFlanker");
		}
		if (dFlanker < dDepth)
		{
			return Resources.ResourceManager.GetString("ErrorFlanker");
		}
		dAngle = Math.PI - Math.Asin(dDepth / dFlanker);
		double num = Math.Cos(Math.PI - dAngle) * dFlanker;
		dAngle = dAngle * 180.0 / Math.PI;
		dFront = dWidth - num * 2.0;
		if (dFront <= 0.0)
		{
			return Resources.ResourceManager.GetString("ErrorFront");
		}
		return "";
	}

	public static string GetDepthAndSideDimension(double dWidth, double dAngle, int nCountSide, ref double dDepth, ref double dHoleDim)
	{
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Expected O, but got Unknown
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Expected O, but got Unknown
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Expected O, but got Unknown
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Expected O, but got Unknown
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Expected O, but got Unknown
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Expected O, but got Unknown
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Expected O, but got Unknown
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Expected O, but got Unknown
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Expected O, but got Unknown
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Expected O, but got Unknown
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Expected O, but got Unknown
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Expected O, but got Unknown
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Expected O, but got Unknown
		dDepth = (dHoleDim = 0.0);
		if (dWidth <= 0.0)
		{
			return Resources.ResourceManager.GetString("ErrorWidth");
		}
		if (dAngle < 90.0 || dAngle >= 180.0)
		{
			return Resources.ResourceManager.GetString("ErrorAngle");
		}
		if (nCountSide <= 1)
		{
			return Resources.ResourceManager.GetString("ErrorCountSide");
		}
		Real val = new Real(dWidth);
		Real val2 = val / new Real(2.0);
		Point2D val3 = new Point2D(val2, Real.cero);
		Point2D val4 = new Point2D(-val2, Real.cero);
		double num = (180.0 - dAngle) * (double)nCountSide;
		int num2 = -1;
		if (num > 360.0)
		{
			return "";
		}
		if (num > 180.0)
		{
			num = 360.0 - num;
			num2 = 1;
		}
		Real val5 = new Real(90.0 - num / 2.0);
		Real val6 = new Real(Math.Tan((double)val5.ToRadians()));
		Real val7 = val2 * val6.Abs();
		Point2D val8 = new Point2D(Real.cero, val7 * new Real(num2));
		Arc2D val9 = new Arc2D(val3, val8, val4, Arc2D.POSITIVE);
		double num3 = (double)val9.Angle() / (double)nCountSide;
		num3 *= (double)(nCountSide - 1) / 2.0;
		Point2D val10 = new Point2D();
		val9.GoOverAngle(new Real(num3), ref val10);
		dDepth = (double)val10.y;
		val9.GoOverAngle(val9.Angle() / new Real(nCountSide), ref val10);
		Segment2D val11 = new Segment2D(val3, val10);
		dHoleDim = (double)val11.Length();
		return "";
	}

	public static string GetAngleAndSideDimension(double dWidth, double dDepth, int nCountSide, ref double dAngle, ref double dHoleDim)
	{
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Expected O, but got Unknown
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Expected O, but got Unknown
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Expected O, but got Unknown
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Expected O, but got Unknown
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Expected O, but got Unknown
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Expected O, but got Unknown
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Expected O, but got Unknown
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Expected O, but got Unknown
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Expected O, but got Unknown
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Expected O, but got Unknown
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Expected O, but got Unknown
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Expected O, but got Unknown
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Expected O, but got Unknown
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Expected O, but got Unknown
		//IL_017a: Expected O, but got Unknown
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Expected O, but got Unknown
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Expected O, but got Unknown
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Expected O, but got Unknown
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Expected O, but got Unknown
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Expected O, but got Unknown
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Expected O, but got Unknown
		//IL_020a: Expected O, but got Unknown
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Expected O, but got Unknown
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Expected O, but got Unknown
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Expected O, but got Unknown
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Expected O, but got Unknown
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Expected O, but got Unknown
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Expected O, but got Unknown
		dAngle = (dHoleDim = 0.0);
		if (dWidth <= 0.0)
		{
			return Resources.ResourceManager.GetString("ErrorWidth");
		}
		if (dDepth <= 0.0)
		{
			return Resources.ResourceManager.GetString("ErrorDepth");
		}
		if (nCountSide <= 1)
		{
			return Resources.ResourceManager.GetString("ErrorCountSide");
		}
		Real val = new Real(dWidth);
		Real val2 = val / new Real(2.0);
		Point2D val3 = new Point2D(val2, Real.cero);
		Point2D val4 = new Point2D(-val2, Real.cero);
		Arc2D val5 = null;
		if (nCountSide % 2 == 0)
		{
			Real val6 = new Real(dDepth);
			Real val7 = (val2 * val2 + val6 * val6) / (new Real(2) * val6);
			Point2D val8 = new Point2D(Real.cero, val6 - val7);
			val5 = new Arc2D(val3, val8, val4, Arc2D.POSITIVE);
		}
		else
		{
			Real val9 = new Real(dDepth);
			Real val10 = (val2 * val2 + val9 * val9) / (new Real(2) * val9);
			Point2D val11 = new Point2D(Real.cero, val9 - val10);
			val5 = new Arc2D(val3, val11, val4, Arc2D.POSITIVE);
			Real val12 = val5.Angle() / new Real(nCountSide);
			val12 *= new Real(nCountSide - 1) / new Real(2);
			Point2D val13 = new Point2D();
			val5.GoOverAngle(val12, ref val13);
			Real val14 = new Real(1.0);
			Real y = val13.y;
			while (y != val9)
			{
				val10 -= val14;
				val11 = new Point2D(Real.cero, val9 - val10);
				val5 = new Arc2D(val3, val11, val4, Arc2D.POSITIVE);
				val12 = val5.Angle() / new Real(nCountSide);
				val12 *= new Real(nCountSide - 1) / new Real(2);
				val13 = new Point2D();
				val5.GoOverAngle(val12, ref val13);
				y = val13.y;
				if (y > val9)
				{
					val10 += val14;
					val14 /= new Real(10);
				}
			}
		}
		double num = (double)val5.Angle() / (double)nCountSide;
		Real val15 = (Real.pi - new Real(num)).ToDegrees();
		dAngle = (double)val15;
		Point2D val16 = new Point2D();
		val5.GoOverAngle(val5.Angle() / new Real(nCountSide), ref val16);
		Segment2D val17 = new Segment2D(val3, val16);
		dHoleDim = (double)val17.Length();
		return "";
	}

	public static string GetBayLogoCode(double dAngle, double dFront, double dFlanker)
	{
		if (dFront <= 0.0)
		{
			return "";
		}
		if (dFlanker <= 0.0)
		{
			return "";
		}
		if (dAngle < 90.0 || dAngle >= 180.0)
		{
			return "";
		}
		XmlDocument xmlDocument = new XmlDocument();
		XmlElement xmlElement = xmlDocument.CreateElement(XMLHelpers.LOGOXML_ELEMENT_POLYLINE2D);
		xmlDocument.AppendChild(xmlElement);
		XmlElement xmlElement2 = xmlDocument.CreateElement(XMLHelpers.LOGOXML_ELEMENT_LOGOCODE);
		xmlElement.AppendChild(xmlElement2);
		double num = 180.0 - dAngle;
		XmlElement xmlElement3 = xmlDocument.CreateElement(XMLHelpers.LOGOXML_ELEMENT_LT);
		xmlElement2.AppendChild(xmlElement3);
		string value = Convert.ToString(num, NumberFormatInfo.InvariantInfo);
		xmlElement3.SetAttribute(XMLHelpers.LOGOXML_ATTR_PARAMETER, value);
		xmlElement3 = xmlDocument.CreateElement(XMLHelpers.LOGOXML_ELEMENT_FD);
		xmlElement2.AppendChild(xmlElement3);
		value = Convert.ToString(dFlanker, NumberFormatInfo.InvariantInfo);
		xmlElement3.SetAttribute(XMLHelpers.LOGOXML_ATTR_PARAMETER, value);
		xmlElement3 = xmlDocument.CreateElement(XMLHelpers.LOGOXML_ELEMENT_RT);
		xmlElement2.AppendChild(xmlElement3);
		value = Convert.ToString(num, NumberFormatInfo.InvariantInfo);
		xmlElement3.SetAttribute(XMLHelpers.LOGOXML_ATTR_PARAMETER, value);
		xmlElement3 = xmlDocument.CreateElement(XMLHelpers.LOGOXML_ELEMENT_FD);
		xmlElement2.AppendChild(xmlElement3);
		value = Convert.ToString(dFront, NumberFormatInfo.InvariantInfo);
		xmlElement3.SetAttribute(XMLHelpers.LOGOXML_ATTR_PARAMETER, value);
		xmlElement3 = xmlDocument.CreateElement(XMLHelpers.LOGOXML_ELEMENT_RT);
		xmlElement2.AppendChild(xmlElement3);
		value = Convert.ToString(num, NumberFormatInfo.InvariantInfo);
		xmlElement3.SetAttribute(XMLHelpers.LOGOXML_ATTR_PARAMETER, value);
		xmlElement3 = xmlDocument.CreateElement(XMLHelpers.LOGOXML_ELEMENT_FD);
		xmlElement2.AppendChild(xmlElement3);
		value = Convert.ToString(dFlanker, NumberFormatInfo.InvariantInfo);
		xmlElement3.SetAttribute(XMLHelpers.LOGOXML_ATTR_PARAMETER, value);
		return xmlDocument.InnerXml;
	}

	public static string GetBowLogoCode(double dAngle, double dSide, double dCountSide)
	{
		if (dSide <= 0.0)
		{
			return "";
		}
		if (dCountSide <= 0.0)
		{
			return "";
		}
		if (dAngle < 90.0 || dAngle >= 180.0)
		{
			return "";
		}
		XmlDocument xmlDocument = new XmlDocument();
		XmlElement xmlElement = xmlDocument.CreateElement(XMLHelpers.LOGOXML_ELEMENT_POLYLINE2D);
		xmlDocument.AppendChild(xmlElement);
		XmlElement xmlElement2 = xmlDocument.CreateElement(XMLHelpers.LOGOXML_ELEMENT_LOGOCODE);
		xmlElement.AppendChild(xmlElement2);
		double num = 180.0 - dAngle;
		XmlElement xmlElement3 = xmlDocument.CreateElement(XMLHelpers.LOGOXML_ELEMENT_LT);
		xmlElement2.AppendChild(xmlElement3);
		double num2 = num * (dCountSide - 1.0) % 360.0;
		num2 /= 2.0;
		string value = Convert.ToString(num2, NumberFormatInfo.InvariantInfo);
		xmlElement3.SetAttribute(XMLHelpers.LOGOXML_ATTR_PARAMETER, value);
		for (int i = 0; (double)i < dCountSide; i++)
		{
			xmlElement3 = xmlDocument.CreateElement(XMLHelpers.LOGOXML_ELEMENT_FD);
			xmlElement2.AppendChild(xmlElement3);
			value = Convert.ToString(dSide, NumberFormatInfo.InvariantInfo);
			xmlElement3.SetAttribute(XMLHelpers.LOGOXML_ATTR_PARAMETER, value);
			if ((double)i != dCountSide - 1.0)
			{
				xmlElement3 = xmlDocument.CreateElement(XMLHelpers.LOGOXML_ELEMENT_RT);
				xmlElement2.AppendChild(xmlElement3);
				value = Convert.ToString(num, NumberFormatInfo.InvariantInfo);
				xmlElement3.SetAttribute(XMLHelpers.LOGOXML_ATTR_PARAMETER, value);
			}
		}
		return xmlDocument.InnerXml;
	}
}
