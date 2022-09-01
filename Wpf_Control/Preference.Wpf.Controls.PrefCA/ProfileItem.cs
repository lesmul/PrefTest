using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Preference.Wpf.Controls.PrefCAM;

public sealed class ProfileItem : SimulatorItem
{
	public ProfileItem(string strId, string strXaml, Point ptInitialPosition, double dOrientation, Vector vFlip)
	{
		base.Id = strId;
		base.Xaml = strXaml;
		base.InitialPosition = ptInitialPosition;
		TransformGroup transformGroup = new TransformGroup();
		RotateTransform value = new RotateTransform(dOrientation);
		ScaleTransform scaleTransform = new ScaleTransform(1.0, 1.0);
		if (vFlip.X != 0.0)
		{
			scaleTransform.ScaleX = 0.0 - scaleTransform.ScaleX;
		}
		if (vFlip.Y != 0.0)
		{
			scaleTransform.ScaleY = 0.0 - scaleTransform.ScaleY;
		}
		transformGroup.Children.Add(scaleTransform);
		transformGroup.Children.Add(value);
		base.RenderTransformOrigin = new Point(0.5, 0.5);
		base.RenderTransform = transformGroup;
		Canvas.SetLeft(this, ptInitialPosition.X);
		Canvas.SetBottom(this, ptInitialPosition.Y);
	}
}
