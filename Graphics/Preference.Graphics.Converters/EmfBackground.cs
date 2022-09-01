using System;
using System.IO;
using Ab2d;
using Ab2d.Common.ReaderWmf;
using zlib;

namespace Preference.Graphics.Converters;

internal class EmfBackground
{
	private enum InputType
	{
		File,
		Stream,
		Buffer
	}

	private InputType _type;

	private string _strXaml;

	private string _file;

	private Stream _stream;

	private byte[] _buffer;

	private Exception _ex;

	internal EmfBackground(string fileName)
	{
		_type = InputType.File;
		_file = fileName;
	}

	internal EmfBackground(Stream stream)
	{
		_type = InputType.Stream;
		_stream = stream;
	}

	internal EmfBackground(byte[] buffer)
	{
		_type = InputType.Buffer;
		_buffer = buffer;
	}

	internal void Convert()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Expected O, but got Unknown
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Expected O, but got Unknown
		ReaderWmf val = new ReaderWmf();
		SilverlightXamlWriterSettings val2 = new SilverlightXamlWriterSettings(4);
		try
		{
			if (_type == InputType.File)
			{
				if (!string.IsNullOrEmpty(_file))
				{
					val.Read(_file);
				}
			}
			else
			{
				if (_stream != null)
				{
					_buffer = StreamToByteArray(_stream);
				}
				if (_buffer != null)
				{
					if (CPrefZipNET.IsZipped(_buffer))
					{
						CPrefZipNET.UnzipBLOB(_buffer, ref _buffer);
					}
					MemoryStream memoryStream = new MemoryStream(_buffer);
					val.Read((Stream)memoryStream);
				}
			}
			_strXaml = val.GetXaml((BaseXamlWriterSettings)(object)val2);
		}
		catch (Exception ex)
		{
			Exception ex2 = (_ex = ex);
		}
		finally
		{
			val.Dispose();
		}
	}

	internal string GetXamlCode()
	{
		if (_ex != null)
		{
			throw _ex;
		}
		return _strXaml;
	}

	private byte[] StreamToByteArray(Stream NonSeekableStream)
	{
		MemoryStream memoryStream = new MemoryStream();
		byte[] array = new byte[1024];
		int count;
		while ((count = NonSeekableStream.Read(array, 0, array.Length)) > 0)
		{
			memoryStream.Write(array, 0, count);
		}
		return memoryStream.ToArray();
	}
}
