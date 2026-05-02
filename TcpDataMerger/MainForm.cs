using TcpDataMerger.Models;
using TcpDataMerger.Services;

namespace TcpDataMerger;

public partial class MainForm : Form
{
    private readonly TcpClientService _tcpClient1;
    private readonly TcpClientService _tcpClient2;
    private readonly DataMergeService _mergeService;
    private readonly CsvStorageService _storageService;
    private readonly LinkedList<string> _logMessages = new();
    private readonly object _logLock = new();
    private const int MaxLogCount = 100;

    private int _mergedCount;
    private string _cam1YieldRate = "-";
    private string _cam2YieldRate = "-";
    private readonly object _statsLock = new();

    public MainForm()
    {
        InitializeComponent();
        
        _tcpClient1 = new TcpClientService(1);
        _tcpClient2 = new TcpClientService(2);
        _mergeService = new DataMergeService();
        _storageService = new CsvStorageService();
        
        SetupEventHandlers();
    }

    private void SetupEventHandlers()
    {
        _tcpClient1.ConnectionStateChanged += (s, connected) =>
            Invoke(() => UpdateConnectionStatus(1, connected));
        _tcpClient2.ConnectionStateChanged += (s, connected) =>
            Invoke(() => UpdateConnectionStatus(2, connected));
        
        _tcpClient1.DataReceived += TcpClient_DataReceived;
        _tcpClient2.DataReceived += TcpClient_DataReceived;
        
        _tcpClient1.ErrorOccurred += (s, error) => Invoke(() => AddLog($"[Server1错误] {error}"));
        _tcpClient2.ErrorOccurred += (s, error) => Invoke(() => AddLog($"[Server2错误] {error}"));
        
        _mergeService.DataMerged += (s, data) => Invoke(() => OnDataMerged(data));
        _mergeService.UnmatchedData += (s, data) => Invoke(() => AddLog($"[未匹配或超时] {data}"));
    }

    private void TcpClient_DataReceived(object? sender, string data)
    {
        var client = (TcpClientService)sender!;
        var cameraData = CameraData.Parse(data, client.ServerId);
        
        if (cameraData != null)
        {
            AddLog($"[接收] Server{client.ServerId}: {data}");
            _mergeService.AddData(cameraData);
            
            lock (_statsLock)
            {
                if (client.ServerId == 1)
                    _cam1YieldRate = cameraData.YieldRate;
                else
                    _cam2YieldRate = cameraData.YieldRate;
            }
            UpdateStatistics();
        }
        else
        {
            AddLog($"[解析失败] Server{client.ServerId}: {data}");
        }
    }

    private void OnDataMerged(MergedData data)
    {
        _storageService.SaveMergedData(data);
        AddLog($"[合并存储] {data.ToLogString()}");
        
        lock (_statsLock)
        {
            _mergedCount++;
        }
        UpdateStatistics();
    }

    private void UpdateStatistics()
    {
        if (InvokeRequired)
        {
            Invoke(UpdateStatistics);
            return;
        }

        lock (_statsLock)
        {
            lblCam1Rate.Text = $"Cam1良率: {_cam1YieldRate}";
            lblCam2Rate.Text = $"Cam2良率: {_cam2YieldRate}";
            lblMergedCount.Text = $"合并成功: {_mergedCount}";
        }
    }

    private void UpdateConnectionStatus(int serverId, bool connected)
    {
        var logMessage = connected ? $"[Server{serverId}] 连接成功" : $"[Server{serverId}] 连接断开";
        AddLog(logMessage);
        
        if (serverId == 1)
        {
            btnConnect1.Text = connected ? "断开" : "连接";
            lblStatus1.Text = connected ? "● 已连接" : "○ 未连接";
            lblStatus1.ForeColor = connected ? Color.Green : Color.Gray;
            txtIp1.Enabled = !connected;
            txtPort1.Enabled = !connected;
        }
        else
        {
            btnConnect2.Text = connected ? "断开" : "连接";
            lblStatus2.Text = connected ? "● 已连接" : "○ 未连接";
            lblStatus2.ForeColor = connected ? Color.Green : Color.Gray;
            txtIp2.Enabled = !connected;
            txtPort2.Enabled = !connected;
        }
    }

    private void AddLog(string message)
    {
        if (InvokeRequired)
        {
            Invoke(() => AddLog(message));
            return;
        }

        lock (_logLock)
        {
            var timestamp = DateTime.Now.ToString("HH:mm:ss.ffff");
            var logEntry = $"[{timestamp}] {message}";
            
            _logMessages.AddFirst(logEntry);
            
            while (_logMessages.Count > MaxLogCount)
            {
                _logMessages.RemoveLast();
            }
            
            lstLog.Items.Clear();
            foreach (var log in _logMessages)
            {
                lstLog.Items.Add(log);
            }
            
            if (lstLog.Items.Count > 0)
            {
                lstLog.TopIndex = 0;
            }
        }
    }

    private async void BtnConnect1_Click(object? sender, EventArgs e)
    {
        if (_tcpClient1.IsConnected)
        {
            _tcpClient1.Disconnect();
        }
        else
        {
            if (string.IsNullOrWhiteSpace(txtIp1.Text) || string.IsNullOrWhiteSpace(txtPort1.Text))
            {
                MessageBox.Show("请输入IP地址和端口号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            if (!int.TryParse(txtPort1.Text, out var port))
            {
                MessageBox.Show("端口号格式不正确", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            btnConnect1.Enabled = false;
            var success = await _tcpClient1.ConnectAsync(txtIp1.Text.Trim(), port);
            btnConnect1.Enabled = true;
            
            if (!success)
            {
                MessageBox.Show("连接Server1失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    private async void BtnConnect2_Click(object? sender, EventArgs e)
    {
        if (_tcpClient2.IsConnected)
        {
            _tcpClient2.Disconnect();
        }
        else
        {
            if (string.IsNullOrWhiteSpace(txtIp2.Text) || string.IsNullOrWhiteSpace(txtPort2.Text))
            {
                MessageBox.Show("请输入IP地址和端口号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            if (!int.TryParse(txtPort2.Text, out var port))
            {
                MessageBox.Show("端口号格式不正确", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            btnConnect2.Enabled = false;
            var success = await _tcpClient2.ConnectAsync(txtIp2.Text.Trim(), port);
            btnConnect2.Enabled = true;
            
            if (!success)
            {
                MessageBox.Show("连接Server2失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        _tcpClient1.Dispose();
        _tcpClient2.Dispose();
        _mergeService.Dispose();
        _storageService.Dispose();
        base.OnFormClosing(e);
    }
}
