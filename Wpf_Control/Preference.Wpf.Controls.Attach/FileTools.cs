using System;
using System.IO;
using zlib;

namespace Preference.Wpf.Controls.Attachments;

public class FileTools
{
	public static void CreateFile(string filename, byte[] buffer)
	{
		FileStream fileStream = new FileStream(filename, FileMode.Create, FileAccess.Write);
		fileStream.Write(buffer, 0, buffer.Length);
		fileStream.Close();
	}

	public static byte[] GetBuffer(string filename)
	{
		FileStream fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read);
		int num = Convert.ToInt32(fileStream.Length);
		byte[] array = new byte[num];
		fileStream.Read(array, 0, num);
		fileStream.Close();
		return array;
	}

	public static byte[] GetZippedBuffer(byte[] buffer)
	{
		byte[] result = null;
		if (CPrefZipNET.ZipBLOB(buffer, ref result))
		{
			return result;
		}
		return null;
	}

	public static byte[] GetZippedBuffer(string filename)
	{
		FileStream fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read);
		int num = Convert.ToInt32(fileStream.Length);
		byte[] array = new byte[num];
		fileStream.Read(array, 0, num);
		fileStream.Close();
		byte[] result = null;
		if (CPrefZipNET.UnzipBLOB(array, ref result))
		{
			return result;
		}
		return null;
	}

	public static bool CompareFiles(string file1, string file2)
	{
		if (file1 == file2)
		{
			return true;
		}
		FileStream fileStream = new FileStream(file1, FileMode.Open);
		FileStream fileStream2 = new FileStream(file2, FileMode.Open);
		if (fileStream.Length != fileStream2.Length)
		{
			fileStream.Close();
			fileStream2.Close();
			return false;
		}
		int num;
		int num2;
		do
		{
			num = fileStream.ReadByte();
			num2 = fileStream2.ReadByte();
		}
		while (num == num2 && num != -1);
		fileStream.Close();
		fileStream2.Close();
		return num - num2 == 0;
	}
}
