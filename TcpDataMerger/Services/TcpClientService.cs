using System.Net.Sockets;
using System.Text;

namespace TcpDataMerger.Services;

public class TcpClientService : IDisposable
{
    private TcpClient? _tcpClient;
    private NetworkStream? _stream;
    private CancellationTokenSource? _cts;
    private readonly int _serverId;
    private readonly object _lock = new();

    public int ServerId => _serverId;
    public bool IsConnected => _tcpClient?.Connected ?? false;
    
    public event EventHandler<bool>? ConnectionStateChanged;
    public event EventHandler<string>? DataReceived;
    public event EventHandler<string>? ErrorOccurred;

    public TcpClientService(int serverId)
    {
        _serverId = serverId;
    }

    public async Task<bool> ConnectAsync(string ip, int port)
    {
        try
        {
            lock (_lock)
            {
                if (_tcpClient?.Connected ?? false)
                {
                    Disconnect();
                }

                _tcpClient = new TcpClient();
                _cts = new CancellationTokenSource();
            }

            await _tcpClient.ConnectAsync(ip, port, _cts.Token);
            
            lock (_lock)
            {
                _stream = _tcpClient.GetStream();
            }

            ConnectionStateChanged?.Invoke(this, true);
            
            _ = ReceiveDataAsync(_cts.Token);
            
            return true;
        }
        catch (Exception ex)
        {
            ErrorOccurred?.Invoke(this, $"连接失败: {ex.Message}");
            ConnectionStateChanged?.Invoke(this, false);
            return false;
        }
    }

    public void Disconnect()
    {
        lock (_lock)
        {
            try
            {
                _cts?.Cancel();
                _stream?.Close();
                _tcpClient?.Close();
            }
            catch { }

            _stream = null;
            _tcpClient = null;
            _cts = null;
        }

        ConnectionStateChanged?.Invoke(this, false);
    }

    private async Task ReceiveDataAsync(CancellationToken cancellationToken)
    {
        var buffer = new byte[4096];
        var sb = new StringBuilder();

        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                NetworkStream? stream;
                lock (_lock)
                {
                    stream = _stream;
                }

                if (stream == null)
                    break;

                int bytesRead;
                try
                {
                    bytesRead = await stream.ReadAsync(buffer, cancellationToken);
                }
                catch (IOException)
                {
                    break;
                }

                if (bytesRead == 0)
                    break;

                var data = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                sb.Append(data);

                var content = sb.ToString();
                var lines = content.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

                sb.Clear();

                foreach (var line in lines)
                {
                    var trimmedLine = line.Trim();
                    if (!string.IsNullOrEmpty(trimmedLine))
                    {
                        DataReceived?.Invoke(this, trimmedLine);
                    }
                }
            }
        }
        catch (OperationCanceledException)
        {
        }
        catch (Exception ex)
        {
            ErrorOccurred?.Invoke(this, $"接收数据错误: {ex.Message}");
        }
        finally
        {
            ConnectionStateChanged?.Invoke(this, false);
        }
    }

    public void Dispose()
    {
        Disconnect();
        _cts?.Dispose();
    }
}
