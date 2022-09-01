using System.IO;
using System.Threading;

namespace Preference.Graphics.Converters;

public class Emf
{
	private static EmfBackground _backConverter;

	public static string EmfToSilverlightXaml(string fileName)
	{
		_backConverter = new EmfBackground(fileName);
		RunConvertThread();
		return _backConverter.GetXamlCode();
	}

	public static string EmfToSilverlightXaml(Stream stream)
	{
		_backConverter = new EmfBackground(stream);
		RunConvertThread();
		return _backConverter.GetXamlCode();
	}

	public static string EmfToSilverlightXaml(byte[] buffer)
	{
		_backConverter = new EmfBackground(buffer);
		RunConvertThread();
		return _backConverter.GetXamlCode();
	}

	private static void RunConvertThread()
	{
		Thread thread = new Thread(_backConverter.Convert);
		thread.IsBackground = true;
		thread.SetApartmentState(ApartmentState.STA);
		thread.Start();
		thread.Join();
	}
}
