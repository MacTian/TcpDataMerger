namespace TcpDataMerger.Models;

public class MergedData
{
    public DateTime Timestamp { get; set; }
    public CameraData? Data1 { get; set; }
    public CameraData? Data2 { get; set; }

    public static MergedData Merge(CameraData data1, CameraData data2)
    {
        var timestamp = data1.ReceiveTime < data2.ReceiveTime ? data1.ReceiveTime : data2.ReceiveTime;

        return new MergedData
        {
            Timestamp = timestamp, // Fixed: Removed the incorrect ToString() call
            Data1 = data1,
            Data2 = data2
        };
    }

    public string ToLogString()
    {
        var timeStr = Timestamp.ToString("HH:mm:ss.ffff");
        var cam1 = Data1?.CameraId ?? "-";
        var cam2 = Data2?.CameraId ?? "-";
        var model = Data1?.Model ?? Data2?.Model ?? "-";
        var result1 = Data1?.Result ?? "-";
        var result2 = Data2?.Result ?? "-";
        return $"[{timeStr}] {cam1}({result1}) + {cam2}({result2}) | {model}";
    }
}
