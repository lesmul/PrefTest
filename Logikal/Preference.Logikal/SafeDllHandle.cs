using System;
using Microsoft.Win32.SafeHandles;

namespace Preference.Logikal;

internal class SafeDllHandle : SafeHandleZeroOrMinusOneIsInvalid
{
	private SafeDllHandle()
		: base(ownsHandle: true)
	{
	}

	protected override bool ReleaseHandle()
	{
		if (handle == IntPtr.Zero)
		{
			return true;
		}
		return NativeMethods.FreeLibrary(handle) != 0;
	}
}
