using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace Preference.Wpf.Controls.Attachments;

internal class IconHelper
{
	public enum IconSize
	{
		Large,
		Small
	}

	public enum FolderType
	{
		Open,
		Closed
	}

	public static Icon GetFileIcon(string name, IconSize size, bool linkOverlay)
	{
		Shell32.SHFILEINFO psfi = default(Shell32.SHFILEINFO);
		uint num = 272u;
		if (linkOverlay)
		{
			num += 32768;
		}
		num = ((IconSize.Small != size) ? num : (num + 1));
		Shell32.SHGetFileInfo(name, 128u, ref psfi, (uint)Marshal.SizeOf(psfi), num);
		Icon result = (Icon)Icon.FromHandle(psfi.hIcon).Clone();
		User32.DestroyIcon(psfi.hIcon);
		return result;
	}

	public static Icon GetFolderIcon(IconSize size, FolderType folderType)
	{
		uint num = 272u;
		if (folderType == FolderType.Open)
		{
			num += 2;
		}
		num = ((IconSize.Small != size) ? num : (num + 1));
		Shell32.SHFILEINFO psfi = default(Shell32.SHFILEINFO);
		Shell32.SHGetFileInfo(null, 16u, ref psfi, (uint)Marshal.SizeOf(psfi), num);
		Icon.FromHandle(psfi.hIcon);
		Icon result = (Icon)Icon.FromHandle(psfi.hIcon).Clone();
		User32.DestroyIcon(psfi.hIcon);
		return result;
	}

	public static Icon ExtractIconForExtension(string extension, IconSize size)
	{
		if (extension != null)
		{
			string name = "0" + extension;
			return GetFileIcon(name, size, linkOverlay: false);
		}
		throw new ArgumentException("Invalid file or extension.", "fileOrExtension");
	}

	public static string GetExtension(string extensionOrFile)
	{
		try
		{
			string extension = Path.GetExtension(extensionOrFile);
			extension = extension.Replace("#", "_");
			return extension.Replace("&", "_");
		}
		catch
		{
		}
		return "";
	}

	public static Bitmap DataToAlphaBitmap(byte[] a_data, int sizeX, int sizeY)
	{
		using Icon ico = new Icon(new MemoryStream(a_data, writable: false), sizeX, sizeY);
		return IconToAlphaBitmap(ico);
	}

	public static Bitmap IconToAlphaBitmap(Icon ico)
	{
		User32.ICONINFO piconinfo = default(User32.ICONINFO);
		User32.GetIconInfo(ico.Handle, out piconinfo);
		Bitmap bitmap = Image.FromHbitmap(piconinfo.hbmColor);
		User32.DestroyIcon(piconinfo.hbmColor);
		User32.DestroyIcon(piconinfo.hbmMask);
		if (Image.GetPixelFormatSize(bitmap.PixelFormat) < 32)
		{
			return ico.ToBitmap();
		}
		Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
		BitmapData bitmapData = bitmap.LockBits(rect, ImageLockMode.ReadOnly, bitmap.PixelFormat);
		Bitmap original = new Bitmap(bitmapData.Width, bitmapData.Height, bitmapData.Stride, PixelFormat.Format32bppArgb, bitmapData.Scan0);
		bool flag = false;
		for (int i = 0; i <= bitmapData.Height - 1; i++)
		{
			for (int j = 0; j <= bitmapData.Width - 1; j++)
			{
				Color color = Color.FromArgb(Marshal.ReadInt32(bitmapData.Scan0, bitmapData.Stride * i + 4 * j));
				if ((color.A > 0) & (color.A < byte.MaxValue))
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				break;
			}
		}
		bitmap.UnlockBits(bitmapData);
		if (flag)
		{
			return new Bitmap(original);
		}
		return new Bitmap(ico.ToBitmap());
	}
}
