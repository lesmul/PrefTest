using System;
using System.IO;
using Preference.Exceptions;
using Preference.Logikal.Api;

namespace Preference.Logikal;

internal class InputOfElements : IDisposable
{
	public byte[] ExportData
	{
		get
		{
			int num = LogiDll.IoeGetReturnPL(Handle, null);
			if (num <= 0)
			{
				return null;
			}
			byte[] array = new byte[num];
			num = LogiDll.IoeGetReturnPL(Handle, array);
			if (num <= 0)
			{
				return null;
			}
			return array;
		}
	}

	public byte[] Serialization
	{
		get
		{
			int num = LogiDll.IoeGetReturn(Handle, null);
			if (num <= 0)
			{
				return null;
			}
			byte[] array = new byte[num];
			num = LogiDll.IoeGetReturn(Handle, array);
			if (num <= 0)
			{
				return null;
			}
			return array;
		}
		set
		{
			LogiDll.IoeSetPosition(Handle, value, value.Length);
		}
	}

	public string ProjectBlob { get; set; }

	private int Handle { get; set; }

	public InputOfElements()
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		Handle = LogiDll.IoeInit();
		if (Handle < 0)
		{
			throw new PrefException("Error initializing new input of elements.");
		}
	}

	public void Dispose()
	{
		Dispose(bDisposing: true);
		GC.SuppressFinalize(this);
	}

	public void NewPosition(ElementType type)
	{
		LogiDll.IoeNewPosition(Handle, type);
	}

	public bool EditPosition(InputOfElementsExecuteMode mode)
	{
		return LogiDll.IoeExecute(Handle, mode);
	}

	public void SetProjectBlob(string strProjectBlob)
	{
		ProjectBlob = strProjectBlob;
		LogiDll.POSetObjDataF(Handle, strProjectBlob);
	}

	public void GetProjectBlob()
	{
		LogiDll.POGetObjDataF(Handle, ProjectBlob);
	}

	protected virtual void Dispose(bool bDisposing)
	{
		int handle = Handle;
		Handle = -1;
		if (handle >= 0)
		{
			LogiDll.IoeDone(handle);
		}
		if (File.Exists(ProjectBlob))
		{
			File.Delete(ProjectBlob);
		}
	}
}
