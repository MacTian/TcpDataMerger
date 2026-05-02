using System.Text;
using TcpDataMerger.Models;

namespace TcpDataMerger.Services;

public class CsvStorageService : IDisposable
{
    private readonly string _dataDirectory;
    private readonly object _fileLock = new();
    private string? _currentFilePath;
    private DateTime _currentFileDate;

    private static readonly string[] Headers = new[]
    {
        "时间戳", "相机1", "相机1作业号", "相机1结果", "相机1合格数", "相机1不良数", "相机1总数", "相机1良率",
        "相机2", "相机2作业号", "相机2结果", "相机2合格数", "相机2不良数", "相机2总数", "相机2良率"
    };

    public CsvStorageService(string? dataDirectory = null)
    {
        _dataDirectory = dataDirectory ?? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
        
        if (!Directory.Exists(_dataDirectory))
        {
            Directory.CreateDirectory(_dataDirectory);
        }
    }

    public void SaveMergedData(MergedData data)
    {
        lock (_fileLock)
        {
            EnsureFileReady(data.Timestamp);
            
            var line = FormatCsvLine(data);
            File.AppendAllText(_currentFilePath!, line + Environment.NewLine, Encoding.UTF8);
        }
    }

    private void EnsureFileReady(DateTime timestamp)
    {
        var fileDate = timestamp.Date;
        
        if (_currentFilePath == null || _currentFileDate != fileDate)
        {
            _currentFileDate = fileDate;
            _currentFilePath = Path.Combine(_dataDirectory, $"{fileDate:yyyyMMdd}.csv");
        }
        
        if (!File.Exists(_currentFilePath))
        {
            var headerLine = string.Join(",", Headers);
            File.WriteAllText(_currentFilePath, headerLine + Environment.NewLine, Encoding.UTF8);
        }
    }

    private static string FormatCsvLine(MergedData data)
    {
        var timestamp = data.Timestamp.ToString("yyyy-MM-dd HH:mm:ss:ffff");
        
        var parts = new string[]
        {
            timestamp,
            EscapeCsvField(data.Data1?.CameraId ?? ""),
            EscapeCsvField(data.Data1?.Model ?? ""),
            EscapeCsvField(data.Data1?.Result ?? ""),
            data.Data1?.PassCount.ToString() ?? "0",
            data.Data1?.FailCount.ToString() ?? "0",
            data.Data1?.TotalCount.ToString() ?? "0",
            EscapeCsvField(data.Data1?.YieldRate ?? ""),
            EscapeCsvField(data.Data2?.CameraId ?? ""),
            EscapeCsvField(data.Data2?.Model ?? ""),
            EscapeCsvField(data.Data2?.Result ?? ""),
            data.Data2?.PassCount.ToString() ?? "0",
            data.Data2?.FailCount.ToString() ?? "0",
            data.Data2?.TotalCount.ToString() ?? "0",
            EscapeCsvField(data.Data2?.YieldRate ?? "")
        };

        return string.Join(",", parts);
    }

    private static string EscapeCsvField(string field)
    {
        if (string.IsNullOrEmpty(field))
            return "";
        
        if (field.Contains(',') || field.Contains('"') || field.Contains('\n') || field.Contains('\r'))
        {
            return $"\"{field.Replace("\"", "\"\"")}\"";
        }
        
        return field;
    }

    public void Dispose()
    {
    }
}
