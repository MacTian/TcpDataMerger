namespace TcpDataMerger;

partial class MainForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        mainTableLayout = new TableLayoutPanel();
        leftPanel = new Panel();
        groupBoxStats = new GroupBox();
        lblMergedCount = new Label();
        lblCam2Rate = new Label();
        lblCam1Rate = new Label();
        groupBox2 = new GroupBox();
        lblIp2 = new Label();
        txtIp2 = new TextBox();
        lblPort2 = new Label();
        txtPort2 = new TextBox();
        btnConnect2 = new Button();
        lblStatus2 = new Label();
        groupBox1 = new GroupBox();
        lblIp1 = new Label();
        txtIp1 = new TextBox();
        lblPort1 = new Label();
        txtPort1 = new TextBox();
        btnConnect1 = new Button();
        lblStatus1 = new Label();
        rightPanel = new Panel();
        groupBoxLog = new GroupBox();
        lstLog = new ListBox();
        mainTableLayout.SuspendLayout();
        leftPanel.SuspendLayout();
        groupBoxStats.SuspendLayout();
        groupBox2.SuspendLayout();
        groupBox1.SuspendLayout();
        rightPanel.SuspendLayout();
        groupBoxLog.SuspendLayout();
        SuspendLayout();
        // 
        // mainTableLayout
        // 
        mainTableLayout.ColumnCount = 2;
        mainTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 320F));
        mainTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        mainTableLayout.Controls.Add(leftPanel, 0, 0);
        mainTableLayout.Controls.Add(rightPanel, 1, 0);
        mainTableLayout.Dock = DockStyle.Fill;
        mainTableLayout.Location = new Point(0, 0);
        mainTableLayout.Margin = new Padding(3, 4, 3, 4);
        mainTableLayout.Name = "mainTableLayout";
        mainTableLayout.RowCount = 1;
        mainTableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        mainTableLayout.Size = new Size(1170, 1024);
        mainTableLayout.TabIndex = 0;
        // 
        // leftPanel
        // 
        leftPanel.Controls.Add(groupBoxStats);
        leftPanel.Controls.Add(groupBox2);
        leftPanel.Controls.Add(groupBox1);
        leftPanel.Dock = DockStyle.Fill;
        leftPanel.Location = new Point(3, 4);
        leftPanel.Margin = new Padding(3, 4, 3, 4);
        leftPanel.Name = "leftPanel";
        leftPanel.Padding = new Padding(11, 13, 11, 13);
        leftPanel.Size = new Size(314, 1016);
        leftPanel.TabIndex = 0;
        // 
        // groupBoxStats
        // 
        groupBoxStats.Controls.Add(lblMergedCount);
        groupBoxStats.Controls.Add(lblCam2Rate);
        groupBoxStats.Controls.Add(lblCam1Rate);
        groupBoxStats.Dock = DockStyle.Top;
        groupBoxStats.Location = new Point(11, 413);
        groupBoxStats.Margin = new Padding(3, 4, 3, 4);
        groupBoxStats.Name = "groupBoxStats";
        groupBoxStats.Padding = new Padding(11, 13, 11, 13);
        groupBoxStats.Size = new Size(292, 200);
        groupBoxStats.TabIndex = 2;
        groupBoxStats.TabStop = false;
        groupBoxStats.Text = "统计信息";
        // 
        // lblCam1Rate
        // 
        lblCam1Rate.AutoSize = true;
        lblCam1Rate.Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
        lblCam1Rate.Location = new Point(17, 40);
        lblCam1Rate.Name = "lblCam1Rate";
        lblCam1Rate.Size = new Size(120, 23);
        lblCam1Rate.TabIndex = 0;
        lblCam1Rate.Text = "Cam1读取率: 0%";
        // 
        // lblCam2Rate
        // 
        lblCam2Rate.AutoSize = true;
        lblCam2Rate.Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
        lblCam2Rate.Location = new Point(17, 80);
        lblCam2Rate.Name = "lblCam2Rate";
        lblCam2Rate.Size = new Size(120, 23);
        lblCam2Rate.TabIndex = 1;
        lblCam2Rate.Text = "Cam2读取率: 0%";
        // 
        // lblMergedCount
        // 
        lblMergedCount.AutoSize = true;
        lblMergedCount.Font = new Font("Microsoft YaHei UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
        lblMergedCount.Location = new Point(17, 120);
        lblMergedCount.Name = "lblMergedCount";
        lblMergedCount.Size = new Size(110, 23);
        lblMergedCount.TabIndex = 2;
        lblMergedCount.Text = "合并成功: 0";
        // 
        // groupBox2
        // 
        groupBox2.Controls.Add(lblIp2);
        groupBox2.Controls.Add(txtIp2);
        groupBox2.Controls.Add(lblPort2);
        groupBox2.Controls.Add(txtPort2);
        groupBox2.Controls.Add(btnConnect2);
        groupBox2.Controls.Add(lblStatus2);
        groupBox2.Dock = DockStyle.Top;
        groupBox2.Location = new Point(11, 213);
        groupBox2.Margin = new Padding(3, 4, 3, 4);
        groupBox2.Name = "groupBox2";
        groupBox2.Padding = new Padding(11, 13, 11, 13);
        groupBox2.Size = new Size(292, 200);
        groupBox2.TabIndex = 1;
        groupBox2.TabStop = false;
        groupBox2.Text = "Cam2";
        // 
        // lblIp2
        // 
        lblIp2.Location = new Point(17, 33);
        lblIp2.Name = "lblIp2";
        lblIp2.Size = new Size(34, 31);
        lblIp2.TabIndex = 0;
        lblIp2.Text = "IP:";
        lblIp2.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // txtIp2
        // 
        txtIp2.Location = new Point(57, 33);
        txtIp2.Margin = new Padding(3, 4, 3, 4);
        txtIp2.Name = "txtIp2";
        txtIp2.PlaceholderText = "127.0.0.1";
        txtIp2.Size = new Size(137, 27);
        txtIp2.TabIndex = 1;
        // 
        // lblPort2
        // 
        lblPort2.Location = new Point(200, 33);
        lblPort2.Name = "lblPort2";
        lblPort2.Size = new Size(40, 31);
        lblPort2.TabIndex = 2;
        lblPort2.Text = "Port:";
        lblPort2.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // txtPort2
        // 
        txtPort2.Location = new Point(240, 33);
        txtPort2.Margin = new Padding(3, 4, 3, 4);
        txtPort2.Name = "txtPort2";
        txtPort2.PlaceholderText = "8081";
        txtPort2.Size = new Size(57, 27);
        txtPort2.TabIndex = 3;
        // 
        // btnConnect2
        // 
        btnConnect2.Location = new Point(17, 73);
        btnConnect2.Margin = new Padding(3, 4, 3, 4);
        btnConnect2.Name = "btnConnect2";
        btnConnect2.Size = new Size(86, 37);
        btnConnect2.TabIndex = 4;
        btnConnect2.Text = "连接";
        btnConnect2.UseVisualStyleBackColor = true;
        btnConnect2.Click += BtnConnect2_Click;
        // 
        // lblStatus2
        // 
        lblStatus2.ForeColor = Color.Gray;
        lblStatus2.Location = new Point(114, 73);
        lblStatus2.Name = "lblStatus2";
        lblStatus2.Size = new Size(91, 37);
        lblStatus2.TabIndex = 5;
        lblStatus2.Text = "○ 未连接";
        lblStatus2.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // groupBox1
        // 
        groupBox1.Controls.Add(lblIp1);
        groupBox1.Controls.Add(txtIp1);
        groupBox1.Controls.Add(lblPort1);
        groupBox1.Controls.Add(txtPort1);
        groupBox1.Controls.Add(btnConnect1);
        groupBox1.Controls.Add(lblStatus1);
        groupBox1.Dock = DockStyle.Top;
        groupBox1.Location = new Point(11, 13);
        groupBox1.Margin = new Padding(3, 4, 3, 4);
        groupBox1.Name = "groupBox1";
        groupBox1.Padding = new Padding(11, 13, 11, 13);
        groupBox1.Size = new Size(292, 200);
        groupBox1.TabIndex = 0;
        groupBox1.TabStop = false;
        groupBox1.Text = "Cam1";
        // 
        // lblIp1
        // 
        lblIp1.Location = new Point(17, 33);
        lblIp1.Name = "lblIp1";
        lblIp1.Size = new Size(34, 31);
        lblIp1.TabIndex = 0;
        lblIp1.Text = "IP:";
        lblIp1.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // txtIp1
        // 
        txtIp1.Location = new Point(57, 33);
        txtIp1.Margin = new Padding(3, 4, 3, 4);
        txtIp1.Name = "txtIp1";
        txtIp1.PlaceholderText = "127.0.0.1";
        txtIp1.Size = new Size(137, 27);
        txtIp1.TabIndex = 1;
        // 
        // lblPort1
        // 
        lblPort1.Location = new Point(200, 33);
        lblPort1.Name = "lblPort1";
        lblPort1.Size = new Size(40, 31);
        lblPort1.TabIndex = 2;
        lblPort1.Text = "Port:";
        lblPort1.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // txtPort1
        // 
        txtPort1.Location = new Point(240, 33);
        txtPort1.Margin = new Padding(3, 4, 3, 4);
        txtPort1.Name = "txtPort1";
        txtPort1.PlaceholderText = "8080";
        txtPort1.Size = new Size(57, 27);
        txtPort1.TabIndex = 3;
        // 
        // btnConnect1
        // 
        btnConnect1.Location = new Point(17, 73);
        btnConnect1.Margin = new Padding(3, 4, 3, 4);
        btnConnect1.Name = "btnConnect1";
        btnConnect1.Size = new Size(86, 37);
        btnConnect1.TabIndex = 4;
        btnConnect1.Text = "连接";
        btnConnect1.UseVisualStyleBackColor = true;
        btnConnect1.Click += BtnConnect1_Click;
        // 
        // lblStatus1
        // 
        lblStatus1.ForeColor = Color.Gray;
        lblStatus1.Location = new Point(114, 73);
        lblStatus1.Name = "lblStatus1";
        lblStatus1.Size = new Size(91, 37);
        lblStatus1.TabIndex = 5;
        lblStatus1.Text = "○ 未连接";
        lblStatus1.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // rightPanel
        // 
        rightPanel.Controls.Add(groupBoxLog);
        rightPanel.Dock = DockStyle.Fill;
        rightPanel.Location = new Point(323, 4);
        rightPanel.Margin = new Padding(3, 4, 3, 4);
        rightPanel.Name = "rightPanel";
        rightPanel.Padding = new Padding(11, 13, 11, 13);
        rightPanel.Size = new Size(844, 1016);
        rightPanel.TabIndex = 1;
        // 
        // groupBoxLog
        // 
        groupBoxLog.Controls.Add(lstLog);
        groupBoxLog.Dock = DockStyle.Fill;
        groupBoxLog.Location = new Point(11, 13);
        groupBoxLog.Margin = new Padding(3, 4, 3, 4);
        groupBoxLog.Name = "groupBoxLog";
        groupBoxLog.Padding = new Padding(11, 13, 11, 13);
        groupBoxLog.Size = new Size(822, 990);
        groupBoxLog.TabIndex = 0;
        groupBoxLog.TabStop = false;
        groupBoxLog.Text = "数据日志 (最近100条)";
        // 
        // lstLog
        // 
        lstLog.Dock = DockStyle.Fill;
        lstLog.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
        lstLog.HorizontalScrollbar = true;
        lstLog.IntegralHeight = false;
        lstLog.ItemHeight = 18;
        lstLog.Location = new Point(11, 33);
        lstLog.Margin = new Padding(3, 4, 3, 4);
        lstLog.Name = "lstLog";
        lstLog.Size = new Size(800, 944);
        lstLog.TabIndex = 0;
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1170, 1024);
        Controls.Add(mainTableLayout);
        Margin = new Padding(3, 4, 3, 4);
        MinimumSize = new Size(912, 784);
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "TCP数据合并处理系统";
        mainTableLayout.ResumeLayout(false);
        leftPanel.ResumeLayout(false);
        groupBoxStats.ResumeLayout(false);
        groupBoxStats.PerformLayout();
        groupBox2.ResumeLayout(false);
        groupBox2.PerformLayout();
        groupBox1.ResumeLayout(false);
        groupBox1.PerformLayout();
        rightPanel.ResumeLayout(false);
        groupBoxLog.ResumeLayout(false);
        ResumeLayout(false);
    }

    private Label lblStatus1;
    private Label lblStatus2;
    private TextBox txtIp1;
    private TextBox txtIp2;
    private TextBox txtPort1;
    private TextBox txtPort2;
    private Button btnConnect1;
    private Button btnConnect2;
    private ListBox lstLog;
    private TableLayoutPanel mainTableLayout;
    private Panel leftPanel;
    private GroupBox groupBox2;
    private Label lblIp2;
    private Label lblPort2;
    private GroupBox groupBox1;
    private Label lblIp1;
    private Label lblPort1;
    private Panel rightPanel;
    private GroupBox groupBoxLog;
    private GroupBox groupBoxStats;
    private Label lblCam1Rate;
    private Label lblCam2Rate;
    private Label lblMergedCount;
}
