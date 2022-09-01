using System.IO;
using System.Text;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Preference.Wpf.Controls.Projects.AppLogic;

public class Utilities
{
	public static string GetControlTemplateText(ControlTemplate controlTemplate)
	{
		StringBuilder stringBuilder = new StringBuilder();
		using (TextWriter writer = new StringWriter(stringBuilder))
		{
			XamlWriter.Save(controlTemplate, writer);
		}
		return stringBuilder.ToString();
	}
}
