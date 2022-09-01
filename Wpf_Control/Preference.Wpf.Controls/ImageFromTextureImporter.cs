using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Preference.Wpf.Controls.Properties;

namespace Preference.Wpf.Controls;

public class ImageFromTextureImporter : ICustomProgress
{
	public string ConnectionString { get; private set; }

	public string CurrentText { get; private set; }

	public double Maximum { get; private set; }

	public string OutputMessage { get; set; }

	public CustomProgressResult Result { get; set; }

	public event EventHandler Canceled;

	public event ProgressChangedEventHandler ProgressChanged;

	public ImageFromTextureImporter(string sqlConnectionString)
	{
		ConnectionString = sqlConnectionString;
		Maximum = GetMaximum();
	}

	public void Cancel()
	{
		Result = CustomProgressResult.Cancel;
	}

	public void Run()
	{
		Result = CustomProgressResult.InProgress;
		string cmdText = "SELECT RTRIM(Nombre) as Name, Texture FROM dbo.Colores WHERE Texture IS NOT NULL";
		using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
		{
			sqlConnection.Open();
			using (SqlConnection sqlConnection2 = new SqlConnection(ConnectionString))
			{
				sqlConnection2.Open();
				using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
				{
					SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
					int num = 1;
					while (sqlDataReader.Read())
					{
						string text = (string)sqlDataReader["Name"];
						byte[] byteArrayIn = (byte[])sqlDataReader["Texture"];
						Image imgToResize = ByteArrayToImage(byteArrayIn);
						Image image = ResizeImage(imgToResize, new Size(100, 100));
						byte[] array = ImageToByteArray(image);
						CurrentText = $"{Resources.Updating} \"{text}\"...";
						OnProgressChanged(num);
						string cmdText2 = $"UPDATE dbo.Colores SET Image = @buffer WHERE Nombre = '{text}'";
						using (SqlCommand sqlCommand2 = new SqlCommand(cmdText2, sqlConnection2))
						{
							sqlCommand2.Parameters.Add("@buffer", SqlDbType.VarBinary, array.Length).Value = array;
							sqlCommand2.ExecuteNonQuery();
						}
						if (Result == CustomProgressResult.Cancel)
						{
							OnCanceled();
							break;
						}
						num++;
					}
				}
				sqlConnection2.Close();
			}
			sqlConnection.Close();
		}
		if (Result == CustomProgressResult.InProgress)
		{
			Result = CustomProgressResult.Success;
			OutputMessage = Resources.ImagesFromTexturesSuccessMessage;
		}
		else
		{
			OutputMessage = Resources.ImagesFromTexturesCancelMessage;
		}
	}

	private Image ByteArrayToImage(byte[] byteArrayIn)
	{
		MemoryStream stream = new MemoryStream(byteArrayIn);
		return Image.FromStream(stream);
	}

	private double GetMaximum()
	{
		string cmdText = "SELECT COUNT(*) FROM dbo.Colores WHERE Texture IS NOT NULL";
		using SqlConnection sqlConnection = new SqlConnection(ConnectionString);
		sqlConnection.Open();
		double result;
		using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
		{
			result = Convert.ToDouble(sqlCommand.ExecuteScalar());
		}
		sqlConnection.Close();
		return result;
	}

	private byte[] ImageToByteArray(Image image)
	{
		MemoryStream memoryStream = new MemoryStream();
		image.Save(memoryStream, ImageFormat.Png);
		return memoryStream.ToArray();
	}

	private void OnCanceled()
	{
		if (this.Canceled != null)
		{
			this.Canceled(this, EventArgs.Empty);
		}
	}

	private void OnProgressChanged(int progressPercentage)
	{
		if (this.ProgressChanged != null)
		{
			this.ProgressChanged(this, new ProgressChangedEventArgs(progressPercentage, null));
		}
	}

	private Image ResizeImage(Image imgToResize, Size size)
	{
		return new Bitmap(imgToResize, size);
	}
}
