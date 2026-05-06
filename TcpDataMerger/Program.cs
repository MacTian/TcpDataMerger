using TcpDataMerger.Services;

namespace TcpDataMerger;

static class Program
{
    [STAThread]
    static async Task Main()
    {
        ApplicationConfiguration.Initialize();

        var licenseOk = await LicenseService.CheckLicenseAsync();
        if (!licenseOk)
        {
            using var licenseForm = new LicenseForm();
            if (licenseForm.ShowDialog() != DialogResult.OK)
                return;
        }

        Application.Run(new MainForm());
    }
}
