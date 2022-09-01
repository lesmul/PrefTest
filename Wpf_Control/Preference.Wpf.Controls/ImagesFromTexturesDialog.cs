using Preference.Wpf.Controls.Properties;

namespace Preference.Wpf.Controls;

public class ImagesFromTexturesDialog
{
	private string ConnectionString { get; set; }

	public ImagesFromTexturesDialog(string connectionString)
	{
		ConnectionString = connectionString;
	}

	public void ShowDialog()
	{
		ImageFromTextureImporter customProgress = new ImageFromTextureImporter(ConnectionString);
		CustomProgressDialog customProgressDialog = new CustomProgressDialog(Resources.ImagesFromTexturesHeaderDialog, customProgress);
		customProgressDialog.Start();
	}
}
