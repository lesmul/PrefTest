using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Preference.Wpf.Controls.PrefCAM;

public sealed class OperationItem : SimulatorItem
{
	private double _dAnimationOffset;

	private TranslateTransform _tTranslation;

	private Point _ptOrientationPoint;

	public double AnimationOffset
	{
		get
		{
			return _dAnimationOffset;
		}
		set
		{
			_dAnimationOffset = value;
		}
	}

	public TranslateTransform Translation
	{
		get
		{
			return _tTranslation;
		}
		set
		{
			_tTranslation = value;
		}
	}

	public Point OrientationPoint
	{
		get
		{
			return _ptOrientationPoint;
		}
		set
		{
			_ptOrientationPoint = value;
		}
	}

	public OperationItem(string strId, string strXaml, Point ptInitialPosition, double dOrientation, Vector vFlip, double dDepth)
	{
		base.Id = strId;
		base.Xaml = strXaml;
		base.InitialPosition = ptInitialPosition;
		AnimationOffset = dDepth;
		TransformGroup transformGroup = new TransformGroup();
		RotateTransform rotateTransform = new RotateTransform(dOrientation);
		ScaleTransform scaleTransform = new ScaleTransform(1.0, 1.0);
		_tTranslation = new TranslateTransform();
		if (vFlip.X != 0.0)
		{
			scaleTransform.ScaleX = 0.0 - scaleTransform.ScaleX;
		}
		if (vFlip.Y != 0.0)
		{
			scaleTransform.ScaleY = 0.0 - scaleTransform.ScaleY;
		}
		transformGroup.Children.Add(scaleTransform);
		transformGroup.Children.Add(rotateTransform);
		transformGroup.Children.Add(_tTranslation);
		base.RenderTransformOrigin = new Point(0.5, 0.5);
		base.RenderTransform = transformGroup;
		Point point = new Point(1.0, 0.0);
		OrientationPoint = rotateTransform.Transform(point);
		Canvas.SetLeft(this, ptInitialPosition.X);
		Canvas.SetBottom(this, ptInitialPosition.Y);
	}
}
