using Licensing.Sdk;

namespace TcpDataMerger.Services;

public static class LicenseService
{
    private const string ServerUrl = "https://license-board.onrender.com";
    private const string SoftwareId = "TcpDataMerger";

    private static readonly LicenseManager Manager = new(ServerUrl, SoftwareId);

    public static async Task<bool> CheckLicenseAsync()
    {
        var result = await Manager.CheckLicenseAsync();
        return result.IsLicensed && !result.IsExpired;
    }

    public static async Task<string?> GetRequestCodeAsync()
    {
        return await Manager.SubmitRequestAsync();
    }

    public static async Task<(bool Success, string Message)> ActivateAsync(string code)
    {
        var result = await Manager.ActivateAsync(code);
        return (result.Success, result.Message);
    }
}
