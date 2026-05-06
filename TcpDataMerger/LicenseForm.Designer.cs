namespace TcpDataMerger;

partial class LicenseForm
{
    private const string ServerUrl = "https://license-board.onrender.com";

#nullable disable
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        var padding = 12;

        // --- 主布局 ---
        var mainPanel = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(padding),
            RowCount = 5,
            ColumnCount = 1
        };
        mainPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        mainPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        mainPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        mainPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        // --- 标题 ---
        var lblTitle = new Label
        {
            Text = "软件授权激活",
            Font = new Font("Microsoft YaHei UI", 16F, FontStyle.Bold),
            AutoSize = true,
            Margin = new Padding(0, 0, 0, padding)
        };
        mainPanel.Controls.Add(lblTitle, 0, 0);

        // --- 设备信息 ---
        var devicePanel = new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            AutoSize = true,
            FlowDirection = FlowDirection.LeftToRight,
            Margin = new Padding(0, 0, 0, padding)
        };
        var lblDeviceCaption = new Label
        {
            Text = "设备指纹:",
            AutoSize = true,
            TextAlign = ContentAlignment.MiddleLeft
        };
        var lblDeviceFingerprint = new Label
        {
            Text = "获取中...",
            Font = new Font("Consolas", 9F),
            ForeColor = Color.Gray,
            AutoSize = true,
            TextAlign = ContentAlignment.MiddleLeft
        };
        var btnCopyFingerprint = new Button
        {
            Text = "复制",
            AutoSize = true,
            Height = 25
        };
        btnCopyFingerprint.Click += (s, e) =>
        {
            var fp = lblDeviceFingerprint.Text;
            if (!string.IsNullOrEmpty(fp) && fp != "获取中...")
            {
                try { Clipboard.SetText(fp); } catch { /* ignore */ }
            }
        };
        devicePanel.Controls.AddRange(new Control[] { lblDeviceCaption, lblDeviceFingerprint, btnCopyFingerprint });
        mainPanel.Controls.Add(devicePanel, 0, 1);

        // --- Step 1: 获取申请码 ---
        var groupStep1 = new GroupBox
        {
            Text = "Step 1 — 获取申请码",
            Dock = DockStyle.Top,
            Padding = new Padding(padding),
            Margin = new Padding(0, 0, 0, padding),
            Height = 120
        };
        var step1Panel = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 3,
            RowCount = 2
        };
        step1Panel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        step1Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        step1Panel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));

        var btnGetRequest = new Button
        {
            Text = "获取申请码",
            AutoSize = true,
            Height = 32
        };
        var txtRequestCodeLocal = new TextBox
        {
            ReadOnly = true,
            Font = new Font("Consolas", 10F),
            BackColor = SystemColors.Control,
            Dock = DockStyle.Fill
        };
        var btnCopyRequest = new Button
        {
            Text = "复制",
            AutoSize = true,
            Height = 32,
            Enabled = false
        };
        var lblHint = new Label
        {
            Text = "※ 请将此申请码发送给管理员以获取授权码",
            ForeColor = Color.Gray,
            AutoSize = true,
            Dock = DockStyle.Fill
        };

        step1Panel.Controls.Add(btnGetRequest, 0, 0);
        step1Panel.Controls.Add(txtRequestCodeLocal, 1, 0);
        step1Panel.Controls.Add(btnCopyRequest, 2, 0);
        step1Panel.SetColumnSpan(lblHint, 3);
        step1Panel.Controls.Add(lblHint, 0, 1);

        groupStep1.Controls.Add(step1Panel);
        mainPanel.Controls.Add(groupStep1, 0, 2);

        // --- Step 2: 输入授权码 ---
        var groupStep2 = new GroupBox
        {
            Text = "Step 2 — 输入授权码",
            Dock = DockStyle.Top,
            Padding = new Padding(padding),
            Margin = new Padding(0, 0, 0, padding),
            Height = 100
        };
        var step2Panel = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 1
        };
        step2Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        step2Panel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));

        var txtLicenseCodeLocal = new TextBox
        {
            Font = new Font("Consolas", 10F),
            PlaceholderText = "请输入管理员提供的授权码",
            Dock = DockStyle.Fill,
            CharacterCasing = CharacterCasing.Upper
        };
        var btnActivate = new Button
        {
            Text = "激活",
            AutoSize = true,
            Height = 32,
            Enabled = false
        };

        step2Panel.Controls.Add(txtLicenseCodeLocal, 0, 0);
        step2Panel.Controls.Add(btnActivate, 1, 0);

        groupStep2.Controls.Add(step2Panel);
        mainPanel.Controls.Add(groupStep2, 0, 3);

        // --- 状态栏 ---
        var lblStatusLocal = new Label
        {
            Text = "○ 未激活",
            ForeColor = Color.Gray,
            Dock = DockStyle.Bottom,
            AutoSize = true
        };
        mainPanel.Controls.Add(lblStatusLocal, 0, 4);

        // --- 事件绑定 ---
        btnGetRequest.Click += async (s, e) =>
        {
            btnGetRequest.Enabled = false;
            btnGetRequest.Text = "获取中...";
            txtRequestCodeLocal.Text = "";
            lblStatusLocal.Text = "正在获取申请码...";
            lblStatusLocal.ForeColor = Color.Blue;

            try
            {
                var code = await Services.LicenseService.GetRequestCodeAsync();
                if (!string.IsNullOrEmpty(code))
                {
                    txtRequestCodeLocal.Text = code;
                    btnCopyRequest.Enabled = true;
                    lblStatusLocal.Text = "申请码已获取，请发送给管理员";
                    lblStatusLocal.ForeColor = Color.Green;
                }
                else
                {
                    lblStatusLocal.Text = "获取失败：服务器未返回申请码";
                    lblStatusLocal.ForeColor = Color.Red;
                    btnGetRequest.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                lblStatusLocal.Text = $"获取失败：{ex.Message}";
                lblStatusLocal.ForeColor = Color.Red;
                btnGetRequest.Enabled = true;
            }
            finally
            {
                btnGetRequest.Text = "获取申请码";
            }
        };

        btnCopyRequest.Click += (s, e) =>
        {
            if (!string.IsNullOrEmpty(txtRequestCodeLocal.Text))
            {
                try { Clipboard.SetText(txtRequestCodeLocal.Text); } catch { /* ignore */ }
            }
        };

        txtLicenseCodeLocal.TextChanged += (s, e) =>
        {
            btnActivate.Enabled = !string.IsNullOrWhiteSpace(txtLicenseCodeLocal.Text);
        };

        btnActivate.Click += async (s, e) =>
        {
            var code = txtLicenseCodeLocal.Text.Trim();
            if (string.IsNullOrEmpty(code)) return;

            btnActivate.Enabled = false;
            btnActivate.Text = "激活中...";
            lblStatusLocal.Text = "正在激活...";
            lblStatusLocal.ForeColor = Color.Blue;

            try
            {
                var result = await Services.LicenseService.ActivateAsync(code);
                if (result.Success)
                {
                    lblStatusLocal.Text = "✓ 激活成功";
                    lblStatusLocal.ForeColor = Color.Green;
                    MessageBox.Show("授权激活成功！", "成功",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    lblStatusLocal.Text = $"激活失败：{result.Message}";
                    lblStatusLocal.ForeColor = Color.Red;
                    btnActivate.Enabled = true;
                    btnActivate.Text = "激活";
                }
            }
            catch (Exception ex)
            {
                lblStatusLocal.Text = $"激活失败：{ex.Message}";
                lblStatusLocal.ForeColor = Color.Red;
                btnActivate.Enabled = true;
                btnActivate.Text = "激活";
            }
        };

        // --- 窗体设置 ---
        Controls.Add(mainPanel);
        Text = "TCP数据合并处理系统 — 授权激活";
        Size = new Size(560, 480);
        MinimumSize = new Size(480, 420);
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        AcceptButton = btnActivate;

        // 加载设备指纹
        Load += (s, e) =>
        {
            try
            {
                var tempManager = new Licensing.Sdk.LicenseManager(ServerUrl, "TcpDataMerger");
                lblDeviceFingerprint.Text = tempManager.DeviceFingerprint;
            }
            catch
            {
                lblDeviceFingerprint.Text = "获取失败";
            }
        };
    }
}
