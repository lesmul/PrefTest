using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Windows.Media.Imaging;
using zlib;

namespace Preference.Wpf.Controls;

public static class GalleryInputAdapter
{
	public static Collection<GalleryItem> FromSql(string strQuery, string strConnection)
	{
		Collection<GalleryItem> result = new Collection<GalleryItem>();
		DataSet dataSet = new DataSet();
		dataSet.Locale = CultureInfo.InvariantCulture;
		SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(strQuery, strConnection);
		sqlDataAdapter.Fill(dataSet);
		if (dataSet.Tables.Count == 1)
		{
			result = FromDataSet(dataSet);
		}
		return result;
	}

	public static Collection<GalleryItem> FromDataSet(DataSet dataSet)
	{
		if (dataSet == null)
		{
			return null;
		}
		string strName = string.Empty;
		string strValue = string.Empty;
		string strDescription = string.Empty;
		string strTooltip = string.Empty;
		BitmapImage bitmapImage = null;
		if (dataSet.Tables.Count > 1)
		{
			throw new ArgumentException("The dataset is not correct");
		}
		Collection<GalleryItem> collection = new Collection<GalleryItem>();
		foreach (DataRow row in dataSet.Tables[0].Rows)
		{
			if (dataSet.Tables[0].Columns.IndexOf("Name") != -1)
			{
				strName = row["Name"].ToString();
			}
			if (dataSet.Tables[0].Columns.IndexOf("Value") != -1)
			{
				strValue = row["Value"].ToString();
			}
			if (dataSet.Tables[0].Columns.IndexOf("Description") != -1)
			{
				strDescription = row["Description"].ToString();
			}
			if (dataSet.Tables[0].Columns.IndexOf("Tooltip") != -1)
			{
				strTooltip = row["Tooltip"].ToString();
			}
			if (dataSet.Tables[0].Columns.IndexOf("Image") != -1)
			{
				if (row["Image"] is byte[] vImage)
				{
					bitmapImage = UnzipDataBaseImage(vImage);
					bitmapImage.Freeze();
				}
				else
				{
					bitmapImage = null;
				}
			}
			GalleryItem item = new GalleryItem(bitmapImage, strName, strDescription, strTooltip, strValue);
			collection.Add(item);
		}
		return collection;
	}

	public static BitmapImage UnzipDataBaseImage(byte[] vImage)
	{
		MemoryStream memoryStream = new MemoryStream(vImage);
		MemoryStream memoryStream2 = new MemoryStream();
		CPrefZipNET.UnzipBLOB((Stream)memoryStream, (Stream)memoryStream2);
		BitmapImage bitmapImage = new BitmapImage();
		bitmapImage.BeginInit();
		bitmapImage.StreamSource = memoryStream2;
		bitmapImage.EndInit();
		memoryStream.Dispose();
		return bitmapImage;
	}

	public static BitmapImage ConvertByteArrayToImageSource(byte[] vImage)
	{
		MemoryStream memoryStream = new MemoryStream(vImage);
		BitmapImage bitmapImage = new BitmapImage();
		bitmapImage.BeginInit();
		bitmapImage.StreamSource = memoryStream;
		bitmapImage.EndInit();
		memoryStream.Dispose();
		return bitmapImage;
	}

	public static BitmapImage ConvertMemoryStreamToImageSource(MemoryStream imageStream)
	{
		BitmapImage bitmapImage = new BitmapImage();
		bitmapImage.BeginInit();
		bitmapImage.StreamSource = imageStream;
		bitmapImage.EndInit();
		return bitmapImage;
	}
}
