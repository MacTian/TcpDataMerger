using TcpDataMerger.Services;

namespace TcpDataMerger;

static class Program
{
    [STAThread]
    static async Task Main()
    {
        ApplicationConfiguration.Initialize();

        bool licenseOk;
        try
        {
            licenseOk = await LicenseService.CheckLicenseAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"授权检查失败：{ex.Message}\n\n请确保网络连接正常，或联系管理员。",
                "授权错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            licenseOk = false;
        }

        if (!licenseOk)
        {
            using var licenseForm = new LicenseForm();
            if (licenseForm.ShowDialog() != DialogResult.OK)
                return;
        }

        Application.Run(new MainForm());
    }
}
