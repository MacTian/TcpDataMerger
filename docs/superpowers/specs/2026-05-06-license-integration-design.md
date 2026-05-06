# TcpDataMerger 授权集成设计

## 目标

在 TcpDataMerger 中集成 `Licensing.Sdk`，实现启动时授权验证。未激活用户必须先完成激活才能进入主界面。

## 决策记录

| 决策 | 选择 |
|------|------|
| 触发时机 | 启动时拦截 — 模态弹窗，激活成功后进入主界面 |
| 服务器 | 已部署的线上服务器 |
| 授权码获取 | 手动流程 — 用户复制申请码发给管理员，管理员审批后返回授权码 |
| 持久化 | SDK 默认 SecureStorage（AES-256 加密 + 设备指纹绑定） |

## 架构

```
Program.cs
  └─ LicenseService.InitializeAsync()
       └─ LicenseManager.CheckLicenseAsync()
            ├─ IsLicensed → Application.Run(new MainForm())
            └─ !IsLicensed → ShowDialog(new LicenseForm())
                              ├─ SubmitRequestAsync() → 显示申请码
                              └─ ActivateAsync(code) → 成功 → DialogResult.OK
```

## 新增文件

### Services/LicenseService.cs

封装 SDK 调用，提供简洁的授权接口。

```csharp
public static class LicenseService
{
    public static async Task<bool> CheckLicenseAsync();
    public static async Task<string?> GetRequestCodeAsync();
    public static async Task<(bool Success, string Message)> ActivateAsync(string code);
}
```

### LicenseForm.cs + LicenseForm.Designer.cs

模态对话框，两步激活流程。

**布局：**
- 设备指纹显示（只读，可复制）
- Step 1 区域：获取申请码按钮 + 申请码文本框 + 复制按钮 + 提示文字
- Step 2 区域：授权码输入框 + 激活按钮
- 状态栏：显示当前授权状态（未激活/激活成功/激活失败原因）

**交互：**
- 获取申请码按钮：调用 `LicenseService.GetRequestCodeAsync()`，禁用按钮防止重复点击
- 复制按钮：将申请码复制到剪贴板
- 激活按钮：调用 `LicenseService.ActivateAsync(code)`，成功则 `DialogResult.OK`，失败显示错误信息

## 修改文件

### Program.cs

```csharp
[STAThread]
static async Task Main()
{
    ApplicationConfiguration.Initialize();

    var licenseOk = await LicenseService.CheckLicenseAsync();
    if (!licenseOk)
    {
        using var licenseForm = new LicenseForm();
        if (licenseForm.ShowDialog() != DialogResult.OK)
            return; // 用户关闭授权窗口，退出程序
    }

    Application.Run(new MainForm());
}
```

注意：`Main` 改为 `async Task`，需要 .NET 8 支持（已支持）。

### TcpDataMerger.csproj

添加项目引用：

```xml
<ProjectReference Include="..\..\..\software-licensing-system\src\Licensing.Sdk\Licensing.Sdk.csproj" />
```

SDK 会自动带入 `Licensing.Shared` 传递依赖。

## 配置

服务器地址硬编码在 `LicenseService.cs` 中：

```csharp
private const string ServerUrl = "https://license-board.onrender.com";
private const string ApiSecret = ""; // 留空表示不签名
```

后续可改为从配置文件读取。

## 错误处理

| 场景 | 处理 |
|------|------|
| 服务器不可达 | 显示"无法连接授权服务器，请检查网络" |
| 申请码获取失败 | 显示具体错误信息，保留重试能力 |
| 授权码无效 | 显示"授权码无效"，留在 Step 2 |
| 设备不匹配 | SDK 自动处理，显示"设备不匹配" |
| 用户关闭窗口 | 退出程序 |

## 安全

- 授权缓存由 SDK 的 `SecureStorage` 处理
- AES-256 加密，密钥派生自 MachineName
- 设备指纹绑定（CPU + 主板 + 磁盘 + MAC 的 SHA-256 前 32 位）
- 换机器需重新激活
- 管理员可在后台吊销授权

## 后续优化（不在本次范围）

- 配置文件支持自定义服务器地址
- 定期心跳验证（临时授权到期检查）
- 授权状态在主界面显示（过期时间等）
