using System;
using System.Globalization;
using System.Windows;
using System.Windows.Markup;
using System.Xml.XPath;
using Preference.Wpf.Controls.Properties;

namespace Preference.Wpf.Controls.PrefCAM;

public static class MOSimulatorHelper
{
	public static MachineOperationsSimulator CreateSimulatorFromXml(XPathNavigator xmlNavigator)
	{
		if (xmlNavigator == null)
		{
			return null;
		}
		try
		{
			MachineOperationsSimulator machineOperationsSimulator = new MachineOperationsSimulator();
			XPathNavigator xPathNavigator = xmlNavigator.SelectSingleNode("/Simulator/Machine");
			if (xPathNavigator == null)
			{
				MachineException("Machine");
				return null;
			}
			machineOperationsSimulator.Machine = CreateMachine(xPathNavigator);
			XPathNavigator xPathNavigator2 = xPathNavigator.SelectSingleNode("Profile");
			if (xPathNavigator2 == null)
			{
				MachineException("Profile piece");
				return null;
			}
			machineOperationsSimulator.Profile = CreateProfile(xPathNavigator2, machineOperationsSimulator.Machine);
			XPathNavigator xPathNavigator3 = xPathNavigator2.SelectSingleNode("Operation");
			if (xPathNavigator3 == null)
			{
				MachineException("Operation");
				return null;
			}
			machineOperationsSimulator.Operation = CreateOperation(xPathNavigator3);
			return machineOperationsSimulator;
		}
		catch (FormatException ex)
		{
			throw new FormatException(Resources.ErrorLoaginXmlFile, ex.InnerException);
		}
		catch (OverflowException ex2)
		{
			throw new OverflowException(Resources.ErrorLoaginXmlFile, ex2.InnerException);
		}
		catch (XPathException ex3)
		{
			throw new XPathException(Resources.ErrorLoaginXmlFile, ex3.InnerException);
		}
		catch (XamlParseException ex4)
		{
			throw new XPathException(Resources.ErrorLoaginXmlFile, ex4.InnerException);
		}
	}

	private static MachineItem CreateMachine(XPathNavigator machineNavigator)
	{
		if (machineNavigator == null)
		{
			return null;
		}
		string attribute = machineNavigator.GetAttribute("Id", "");
		Point ptInsertionPoint = default(Point);
		ptInsertionPoint.X = Convert.ToDouble(machineNavigator.GetAttribute("InsertPointX", ""), CultureInfo.CurrentCulture);
		ptInsertionPoint.Y = Convert.ToDouble(machineNavigator.GetAttribute("InsertPointY", ""), CultureInfo.CurrentCulture);
		XPathNavigator xaml = GetXaml(machineNavigator);
		if (xaml == null)
		{
			MachineException("XAML Machine");
			return null;
		}
		return new MachineItem(attribute, xaml.InnerXml, ptInsertionPoint);
	}

	private static ProfileItem CreateProfile(XPathNavigator profileNavigator, MachineItem machine)
	{
		if (profileNavigator == null || machine == null)
		{
			return null;
		}
		string attribute = profileNavigator.GetAttribute("Id", "");
		Point point = default(Point);
		point.X = Convert.ToDouble(profileNavigator.GetAttribute("OffsetX", ""), CultureInfo.CurrentCulture);
		point.Y = Convert.ToDouble(profileNavigator.GetAttribute("OffsetY", ""), CultureInfo.CurrentCulture);
		Point ptInitialPosition = default(Point);
		ptInitialPosition.X = point.X + machine.InsertionPoint.X;
		ptInitialPosition.Y = point.Y + machine.InsertionPoint.Y;
		double dOrientation = Convert.ToDouble(profileNavigator.GetAttribute("Orientation", ""), CultureInfo.CurrentCulture);
		Vector vector = new Vector(1.0, 1.0);
		bool flag = Convert.ToBoolean(profileNavigator.GetAttribute("FlipX", ""), CultureInfo.CurrentCulture);
		bool flag2 = Convert.ToBoolean(profileNavigator.GetAttribute("FlipY", ""), CultureInfo.CurrentCulture);
		int num = 0;
		int num2 = 0;
		if (flag)
		{
			num = 1;
		}
		if (flag2)
		{
			num2 = 1;
		}
		Vector vFlip = new Vector(num, num2);
		XPathNavigator xaml = GetXaml(profileNavigator);
		if (xaml == null)
		{
			MachineException("XAML Profile piece");
			return null;
		}
		return new ProfileItem(attribute, xaml.InnerXml, ptInitialPosition, dOrientation, vFlip);
	}

	private static OperationItem CreateOperation(XPathNavigator operationNavigator)
	{
		if (operationNavigator == null)
		{
			return null;
		}
		string attribute = operationNavigator.GetAttribute("Id", "");
		Point ptInitialPosition = default(Point);
		ptInitialPosition.X = Convert.ToDouble(operationNavigator.GetAttribute("OffsetX", ""), CultureInfo.CurrentCulture);
		ptInitialPosition.Y = Convert.ToDouble(operationNavigator.GetAttribute("OffsetY", ""), CultureInfo.CurrentCulture);
		double dOrientation = Convert.ToDouble(operationNavigator.GetAttribute("Orientation", ""), CultureInfo.CurrentCulture);
		Vector vector = new Vector(1.0, 1.0);
		bool flag = Convert.ToBoolean(operationNavigator.GetAttribute("FlipX", ""), CultureInfo.CurrentCulture);
		bool flag2 = Convert.ToBoolean(operationNavigator.GetAttribute("FlipY", ""), CultureInfo.CurrentCulture);
		int num = 0;
		int num2 = 0;
		if (flag)
		{
			num = 1;
		}
		if (flag2)
		{
			num2 = 1;
		}
		Vector vFlip = new Vector(num, num2);
		XPathNavigator xaml = GetXaml(operationNavigator);
		if (xaml == null)
		{
			MachineException("XAML Operation");
			return null;
		}
		double dDepth = Convert.ToDouble(operationNavigator.GetAttribute("AnimationOffset", ""), CultureInfo.CurrentCulture);
		return new OperationItem(attribute, xaml.InnerXml, ptInitialPosition, dOrientation, vFlip, dDepth);
	}

	private static XPathNavigator GetXaml(XPathNavigator xmlNavigator)
	{
		return xmlNavigator?.SelectSingleNode("Xaml");
	}

	private static void MachineException(string strInput)
	{
		MessageBox.Show(string.Format(CultureInfo.CurrentCulture, Resources.StringXmlMachineExceptionText, strInput), Resources.StringXmlMachineExceptionTitle, MessageBoxButton.OK, MessageBoxImage.Hand);
	}
}
