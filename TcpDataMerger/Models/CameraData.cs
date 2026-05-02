namespace TcpDataMerger.Models;

public class CameraData
{
    public string CameraId { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string Result { get; set; } = string.Empty;
    public int PassCount { get; set; }
    public int FailCount { get; set; }
    public int TotalCount { get; set; }
    public string YieldRate { get; set; } = string.Empty;
    public DateTime ReceiveTime { get; set; }
    public int ServerId { get; set; }

    public static CameraData? Parse(string data, int serverId)
    {
        if (string.IsNullOrWhiteSpace(data))
            return null;

        var parts = data.Split(',');
        if (parts.Length < 7)
            return null;

        try
        {
            return new CameraData
            {
                CameraId = parts[0].Trim(),
                Model = parts[1].Trim(),
                Result = parts[2].Trim().ToUpper(),
                PassCount = int.Parse(parts[3].Trim()),
                FailCount = int.Parse(parts[4].Trim()),
                TotalCount = int.Parse(parts[5].Trim()),
                YieldRate = parts[6].Trim(),
                ReceiveTime = DateTime.Now,
                ServerId = serverId
            };
        }
        catch
        {
            return null;
        }
    }

    public override string ToString()
    {
        return $"{CameraId},{Model},{Result},{PassCount},{FailCount},{TotalCount},{YieldRate}";
    }
}
