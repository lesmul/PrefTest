using System;
using System.Runtime.InteropServices;

namespace Preference.Wpf.Controls.Attachments;

public class User32
{
	public struct ICONINFO
	{
		public bool fIcon;

		public int xHotspot;

		public int yHotspot;

		public IntPtr hbmMask;

		public IntPtr hbmColor;
	}

	[DllImport("user32.dll", SetLastError = true)]
	public static extern int DestroyIcon(IntPtr hIcon);

	[DllImport("user32.dll")]
	public static extern bool GetIconInfo(IntPtr hIcon, out ICONINFO piconinfo);
}
