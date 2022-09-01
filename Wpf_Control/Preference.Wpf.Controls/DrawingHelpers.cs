using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml;

namespace Preference.Wpf.Controls;

public static class DrawingHelpers
{
	public static readonly double dos_pi = Math.PI * 2.0;

	public static readonly double pi_2 = Math.PI / 2.0;

	public static void DrawPolyline(Panel canvas, Viewbox viewBox, string strPolylineCode, int nDimensionsSide, bool bWidthDimension)
	{
		canvas.Children.Clear();
		if (strPolylineCode == "")
		{
			return;
		}
		DrawingGroup drawingGroup = new DrawingGroup();
		DrawingImage source = new DrawingImage(drawingGroup);
		Image image = new Image();
		image.Source = source;
		canvas.Children.Add(image);
		if (strPolylineCode == null)
		{
			return;
		}
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(strPolylineCode);
		XmlNode xmlNode = xmlDocument.SelectSingleNode(XMLHelpers.LOGOXML_ELEMENT_POLYLINE2D);
		if (xmlNode == null)
		{
			return;
		}
		XmlNode xmlNode2 = xmlNode.SelectSingleNode(XMLHelpers.LOGOXML_ELEMENT_LOGOCODE);
		if (xmlNode2 == null)
		{
			return;
		}
		PathFigure pathFigure = new PathFigure();
		pathFigure.StartPoint = new Point(0.0, 0.0);
		PathSegmentCollection pathSegmentCollection2 = (pathFigure.Segments = new PathSegmentCollection());
		double num = 0.0;
		Point point = new Point(0.0, 0.0);
		Point point2 = new Point(0.0, 0.0);
		Point point3 = new Point(0.0, 0.0);
		foreach (XmlNode childNode in xmlNode2.ChildNodes)
		{
			if (childNode == null)
			{
				continue;
			}
			XmlNode namedItem = childNode.Attributes.GetNamedItem(XMLHelpers.LOGOXML_ATTR_PARAMETER);
			if (namedItem == null)
			{
				continue;
			}
			if (childNode.Name == XMLHelpers.LOGOXML_ELEMENT_BK || childNode.Name == XMLHelpers.LOGOXML_ELEMENT_FD)
			{
				double num2 = Convert.ToDouble(namedItem.Value, NumberFormatInfo.InvariantInfo);
				if (num2 != 0.0)
				{
					if (childNode.Name == "FD")
					{
						PolarDesp(ref point, num, num2);
					}
					else
					{
						PolarDesp(ref point, num, 0.0 - num2);
					}
					LineSegment lineSegment = new LineSegment();
					lineSegment.Point = new Point(point.X, 0.0 - point.Y);
					pathSegmentCollection2.Add(lineSegment);
					point2 = point3;
					point3 = point;
				}
			}
			if (childNode.Name == XMLHelpers.LOGOXML_ELEMENT_RT)
			{
				Point point4 = default(Point);
				point4.X = point3.X - point2.X;
				point4.Y = point3.Y - point2.Y;
				num = Argument(point4);
				double num3 = Convert.ToDouble(namedItem.Value, NumberFormatInfo.InvariantInfo);
				if (num3 != 0.0)
				{
					num -= num3;
				}
			}
			if (childNode.Name == XMLHelpers.LOGOXML_ELEMENT_LT)
			{
				Point point5 = default(Point);
				point5.X = point3.X - point2.X;
				point5.Y = point3.Y - point2.Y;
				num = Argument(point5);
				double num4 = Convert.ToDouble(namedItem.Value, NumberFormatInfo.InvariantInfo);
				if (num4 != 0.0)
				{
					num += num4;
				}
			}
		}
		PathFigureCollection pathFigureCollection = new PathFigureCollection();
		pathFigureCollection.Add(pathFigure);
		PathGeometry pathGeometry = new PathGeometry();
		pathGeometry.Figures = pathFigureCollection;
		double num5 = 1.0 / (viewBox.ActualWidth / pathGeometry.Bounds.Width);
		Pen pen = new Pen(Brushes.Black, 1.0 * num5);
		GeometryDrawing geometryDrawing = new GeometryDrawing();
		geometryDrawing.Geometry = pathGeometry;
		geometryDrawing.Pen = pen;
		drawingGroup.Children.Add(geometryDrawing);
		point2 = pathFigure.StartPoint;
		foreach (LineSegment segment in pathFigure.Segments)
		{
			point3 = segment.Point;
			DrawDimension(drawingGroup, point2, point3, num5, nDimensionsSide, bDrawTicks: true);
			point2 = point3;
		}
		if (bWidthDimension)
		{
			point2 = pathFigure.StartPoint;
			DrawDimension(drawingGroup, point2, point3, num5, 0, bDrawTicks: false);
		}
	}

	public static void DrawDimension(DrawingGroup group, Point pointA, Point pointB, double dpiFactor, int nSide, bool bDrawTicks)
	{
		int num = 50;
		Point point = default(Point);
		point.X = pointB.X - pointA.X;
		point.Y = pointB.Y - pointA.Y;
		double num2 = Math.Sqrt(point.X * point.X + point.Y * point.Y);
		string text = num2.ToString();
		if ((double)(int)num2 != num2)
		{
			text = num2.ToString("F4", CultureInfo.InvariantCulture);
		}
		if ((double)(num * text.Length) > num2)
		{
			num = (int)(num2 / (double)text.Length * 1.5);
		}
		double dAngle = ((nSide == 0) ? (Argument(point) + 90.0) : (Argument(point) - 90.0));
		Point point2 = pointA;
		Point point3 = pointB;
		if (bDrawTicks)
		{
			PolarDesp(ref point2, dAngle, num + 3);
			PolarDesp(ref point3, dAngle, num + 3);
		}
		LineGeometry lineGeometry = new LineGeometry();
		lineGeometry.StartPoint = point2;
		lineGeometry.EndPoint = point3;
		GeometryDrawing geometryDrawing = new GeometryDrawing();
		geometryDrawing.Geometry = lineGeometry;
		geometryDrawing.Pen = new Pen(Brushes.Blue, 1.0 * dpiFactor);
		group.Children.Add(geometryDrawing);
		if (bDrawTicks)
		{
			PolarDesp(ref point2, dAngle, 1.0);
			LineGeometry lineGeometry2 = new LineGeometry();
			lineGeometry2.StartPoint = pointA;
			lineGeometry2.EndPoint = point2;
			GeometryDrawing geometryDrawing2 = new GeometryDrawing();
			geometryDrawing2.Geometry = lineGeometry2;
			geometryDrawing2.Pen = new Pen(Brushes.Blue, 1.0 * dpiFactor);
			group.Children.Add(geometryDrawing2);
			PolarDesp(ref point3, dAngle, 1.0);
			LineGeometry lineGeometry3 = new LineGeometry();
			lineGeometry3.StartPoint = pointB;
			lineGeometry3.EndPoint = point3;
			GeometryDrawing geometryDrawing3 = new GeometryDrawing();
			geometryDrawing3.Geometry = lineGeometry3;
			geometryDrawing3.Pen = new Pen(Brushes.Blue, 1.0 * dpiFactor);
			group.Children.Add(geometryDrawing3);
		}
		if (num > 0)
		{
			DrawingGroup drawingGroup = new DrawingGroup();
			group.Children.Add(drawingGroup);
			DrawingContext drawingContext = drawingGroup.Open();
			FormattedText formattedText = new FormattedText(text, CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, new Typeface("Verdana"), num, Brushes.Blue);
			formattedText.TextAlignment = TextAlignment.Center;
			formattedText.SetFontWeight(FontWeights.Normal);
			Point point4 = new Point((pointA.X + pointB.X) * 0.5, (pointA.Y + pointB.Y) * 0.5);
			if (nSide == 0)
			{
				PolarDesp(ref point4, dAngle, 1.0);
			}
			else
			{
				PolarDesp(ref point4, dAngle, num + 3);
			}
			drawingContext.DrawText(formattedText, point4);
			drawingContext.Close();
			double num3 = Argument(point);
			if (num3 != 0.0)
			{
				RotateTransform rotateTransform = new RotateTransform();
				rotateTransform.Angle = num3;
				rotateTransform.CenterX = point4.X;
				rotateTransform.CenterY = point4.Y;
				drawingGroup.Transform = rotateTransform;
			}
		}
	}

	public static void PolarDesp(ref Point point, double dAngle, double dDesp)
	{
		dAngle = dAngle * Math.PI / 180.0;
		double num;
		double num2;
		if (dDesp == 0.0)
		{
			num = 0.0;
			num2 = 0.0;
		}
		else
		{
			dAngle %= dos_pi;
			if (dAngle < 0.0)
			{
				dAngle += dos_pi;
			}
			if (dAngle == 0.0)
			{
				num = dDesp;
				num2 = 0.0;
			}
			else if (dAngle == Math.PI)
			{
				num = 0.0 - dDesp;
				num2 = 0.0;
			}
			else if (dAngle == pi_2)
			{
				num = 0.0;
				num2 = dDesp;
			}
			else if (dAngle == Math.PI + pi_2)
			{
				num = 0.0;
				num2 = 0.0 - dDesp;
			}
			else
			{
				num = dDesp * Math.Cos(dAngle);
				num2 = dDesp * Math.Sin(dAngle);
			}
		}
		point.X += num;
		point.Y += num2;
	}

	public static double Argument(Point point)
	{
		double num = 10.0;
		if (point.X == 0.0 || point.Y == 0.0)
		{
			num = ((point.X == 0.0 && point.Y != 0.0) ? (pi_2 + ((point.Y >= 0.0) ? 0.0 : Math.PI)) : ((point.X == 0.0 || point.Y != 0.0) ? 0.0 : ((point.X > 0.0) ? 0.0 : Math.PI)));
		}
		else
		{
			double num2 = point.Y / point.X;
			if (point.X < 0.0)
			{
				num2 *= -1.0;
			}
			num = Math.Atan(num2);
			if (point.X < 0.0)
			{
				num = ((!(num >= 0.0)) ? (-Math.PI - num) : (Math.PI - num));
			}
			if (num < 0.0)
			{
				num = Math.PI * 2.0 + num;
			}
		}
		return num * 180.0 / Math.PI;
	}
}
