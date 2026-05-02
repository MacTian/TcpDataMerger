using System.Collections.Concurrent;
using TcpDataMerger.Models;

namespace TcpDataMerger.Services;

public class DataMergeService : IDisposable
{
    private readonly ConcurrentQueue<CameraData> _buffer1 = new();
    private readonly ConcurrentQueue<CameraData> _buffer2 = new();
    private readonly TimeSpan _matchWindow = TimeSpan.FromMilliseconds(1000);
    private readonly System.Timers.Timer _cleanupTimer;
    private readonly object _lock = new();

    public event EventHandler<MergedData>? DataMerged;
    public event EventHandler<CameraData>? UnmatchedData;

    public DataMergeService()
    {
        _cleanupTimer = new System.Timers.Timer(2000);
        _cleanupTimer.Elapsed += CleanupTimer_Elapsed;
        _cleanupTimer.Start();
    }

    public void AddData(CameraData data)
    {
        if (data.ServerId == 1)
        {
            _buffer1.Enqueue(data);
            TryMatch(data, _buffer2);
        }
        else if (data.ServerId == 2)
        {
            _buffer2.Enqueue(data);
            TryMatch(data, _buffer1);
        }
    }

    private void TryMatch(CameraData newData, ConcurrentQueue<CameraData> otherBuffer)
    {
        var otherDataList = new List<CameraData>();
        
        while (otherBuffer.TryDequeue(out var otherData))
        {
            var timeDiff = Math.Abs((newData.ReceiveTime - otherData.ReceiveTime).TotalMilliseconds);
            
            if (timeDiff <= _matchWindow.TotalMilliseconds)
            {
                if ( otherData.TotalCount == newData.TotalCount)
                {
                    var merged = newData.ServerId == 1 
                        ? MergedData.Merge(newData, otherData)
                        : MergedData.Merge(otherData, newData);
                    
                    DataMerged?.Invoke(this, merged);
                    return;
                }
            }
            
            if (DateTime.Now - otherData.ReceiveTime < _matchWindow)
            {
                otherDataList.Add(otherData);
            }
        }
        
        foreach (var item in otherDataList)
        {
            otherBuffer.Enqueue(item);
        }
    }

    private void CleanupTimer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
    {
        CleanupExpiredData(_buffer1);
        CleanupExpiredData(_buffer2);
    }

    private void CleanupExpiredData(ConcurrentQueue<CameraData> buffer)
    {
        var validItems = new List<CameraData>();
        
        while (buffer.TryDequeue(out var data))
        {
            if (DateTime.Now - data.ReceiveTime < TimeSpan.FromSeconds(5))
            {
                validItems.Add(data);
            }
            else
            {
                UnmatchedData?.Invoke(this, data);
            }
        }
        
        foreach (var item in validItems)
        {
            buffer.Enqueue(item);
        }
    }

    public void Dispose()
    {
        _cleanupTimer?.Stop();
        _cleanupTimer?.Dispose();
    }
}
