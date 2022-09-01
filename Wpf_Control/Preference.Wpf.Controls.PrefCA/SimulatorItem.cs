using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using Preference.Wpf.Controls.Properties;

namespace Preference.Wpf.Controls.PrefCAM;

public class SimulatorItem : Canvas
{
	private string _strId;

	private string _strXaml;

	private Point _pntInitialPosition;

	public string Id
	{
		get
		{
			return _strId;
		}
		set
		{
			_strId = value;
		}
	}

	public string Xaml
	{
		get
		{
			return _strXaml;
		}
		set
		{
			_strXaml = value;
			if (!string.IsNullOrEmpty(_strXaml))
			{
				byte[] bytes = Encoding.UTF32.GetBytes(_strXaml);
				Stream stream = new MemoryStream(bytes);
				try
				{
					Canvas canvas = XamlReader.Load(stream) as Canvas;
					base.Children.Add(canvas);
					base.Width = canvas.Width;
					base.Height = canvas.Height;
				}
				catch (XamlParseException innerException)
				{
					throw new XamlParseException(Preference.Wpf.Controls.Properties.Resources.ErrorLoadingXAML, innerException);
				}
			}
		}
	}

	public Point InitialPosition
	{
		get
		{
			return _pntInitialPosition;
		}
		set
		{
			_pntInitialPosition = value;
		}
	}
}
