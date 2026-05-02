# TcpDataMerger

TCP 数据合并处理系统 — 连接两台工业相机的 TCP 服务器，实时接收检测数据，按计数和时间窗口自动配对，合并结果写入每日 CSV 文件。

## 功能

- 同时连接两台 TCP 相机服务器（Cam1 / Cam2）
- 按 `TotalCount` 匹配 + 1 秒时间窗口自动配对
- 合并结果实时写入 `yyyyMMdd.csv` 每日文件
- 实时显示连接状态、良率、合并计数
- 最近 100 条操作日志滚动显示
- 超过 5 秒未匹配的数据自动过期并记录

## 技术栈

- .NET 8 / C#
- Windows Forms
- 无第三方依赖

## 项目结构

```
TcpDataMerger/
├── Models/
│   ├── CameraData.cs        # 相机数据模型，解析 TCP 接收的 CSV 行
│   └── MergedData.cs        # 合并数据模型
├── Services/
│   ├── TcpClientService.cs  # TCP 客户端连接与数据接收
│   ├── DataMergeService.cs  # 数据配对与合并引擎
│   └── CsvStorageService.cs # CSV 文件存储
├── MainForm.cs              # 主窗体逻辑
├── MainForm.Designer.cs     # 窗体布局
└── Program.cs               # 入口
```

## 数据流

```
TCP Server 1 ──┐
               ├── TcpClientService (按行接收)
TCP Server 2 ──┘
               │
               ▼
        CameraData.Parse() (解析 CSV)
               │
               ▼
        DataMergeService.AddData() (入队 + 匹配)
               │
               ├── 匹配成功 → MergedData → CsvStorageService → CSV 文件
               └── 超时过期 → 记录日志
```

## TCP 数据格式

相机服务器发送逗号分隔的文本行（`\n` 结尾）：

```
相机ID,作业号,结果,合格数,不良数,总数,良率
```

示例：
```
CAM-001,JobA,OK,95,5,100,95.0%
```

## CSV 输出格式

每日生成 `yyyyMMdd.csv`，15 列：

| 时间戳 | 相机1 | 相机1作业号 | 相机1结果 | 相机1合格数 | 相机1不良数 | 相机1总数 | 相机1良率 | 相机2 | 相机2作业号 | 相机2结果 | 相机2合格数 | 相机2不良数 | 相机2总数 | 相机2良率 |
|--------|-------|------------|----------|------------|------------|----------|----------|-------|------------|----------|------------|------------|----------|----------|

## 构建与运行

```bash
# 需要 .NET 8 SDK + Windows
dotnet build
dotnet run --project TcpDataMerger
```

或在 Visual Studio 2022 中打开 `TcpDataMerger.slnx` 直接运行。

## License

MIT
