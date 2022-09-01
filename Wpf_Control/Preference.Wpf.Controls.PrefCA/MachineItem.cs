using System.Windows;
using System.Windows.Controls;

namespace Preference.Wpf.Controls.PrefCAM;

public sealed class MachineItem : SimulatorItem
{
	private Point _ptInsertionPoint;

	public Point InsertionPoint
	{
		get
		{
			return _ptInsertionPoint;
		}
		set
		{
			_ptInsertionPoint = value;
		}
	}

	public MachineItem(string strId, string strXaml, Point ptInsertionPoint)
	{
		base.Id = strId;
		base.Xaml = strXaml;
		base.InitialPosition = new Point(0.0, 0.0);
		InsertionPoint = ptInsertionPoint;
		Canvas.SetLeft(this, base.InitialPosition.X);
		Canvas.SetBottom(this, base.InitialPosition.Y);
	}
}
