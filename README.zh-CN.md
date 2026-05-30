<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />

# Game Frame X Read Assets

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.readassets)](https://github.com/GameFrameX/com.gameframex.unity.readassets/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.readassets)](https://github.com/GameFrameX/com.gameframex.unity.readassets/releases)
[![Documentation](https://img.shields.io/badge/Documentation-docs-blue)](https://gameframex.doc.alianblank.com)

> 独立游戏前后端一体化解决方案 · 独立游戏开发者的圆梦大使

[文档](https://gameframex.doc.alianblank.com) · [快速开始](#快速开始) · [QQ群](https://qm.qq.com/q/5U9Fvebw) · [语言](#语言)


</div>

---

## 语言

[English](README.md) | **简体中文** | [繁體中文](README.zh-TW.md) | [日本語](README.ja.md) | [한국어](README.ko.md)

---

## 项目简介

以统一且线程安全的方式直接访问 StreamingAssets，开销极小。基于 [BetterStreamingAssets](https://github.com/gwiazdorrr/BetterStreamingAssets)，提供 `System.IO` 风格的 API，支持包括 Android APK 在内的所有平台。

该库主要服务于 `https://github.com/AlianBlank/GameFrameX` 作为子库使用。

## 快速开始

### 使用方式（三种方式）

1. 直接在 `manifest.json` 文件中添加以下内容
   ```json
   {"com.gameframex.unity.readassets": "https://github.com/AlianBlank/com.gameframex.unity.readassets.git"}
   ```

2. 在 Unity 的 `Packages Manager` 中使用 `Git URL` 的方式添加库，地址为：https://github.com/AlianBlank/com.gameframex.unity.readassets.git

3. 直接下载仓库放置到 Unity 项目的 `Packages` 目录下。会自动加载识别。

## 使用示例

### 向后兼容 API（BlankReadAssets）

- **Read** - 读取 `byte[]` 数组（失败返回 `null`）：

```csharp
byte[] buffer = BlankReadAssets.Read(string path);
```

- **IsFileExists** - 文件是否存在：

```csharp
bool isFileExists = BlankReadAssets.IsFileExists(string path);
```

### 完整 API（BetterStreamingAssets）

```csharp
// 首次使用前初始化（需要在主线程调用）
BetterStreamingAssets.Initialize();
```

#### 读取文件

```csharp
// 读取全部字节
byte[] data = BetterStreamingAssets.ReadAllBytes("Foo/bar.data");

// 以流方式读取
using (var stream = BetterStreamingAssets.OpenRead("Foo/bar.data"))
{
    // 从流中读取...
}

// 读取全部文本
string text = BetterStreamingAssets.ReadAllText("Foo/config.xml");

// 读取全部行
string[] lines = BetterStreamingAssets.ReadAllLines("Foo/data.txt");
```

#### 文件与目录查询

```csharp
// 检查存在性
bool exists = BetterStreamingAssets.FileExists("Config/settings.json");
bool dirExists = BetterStreamingAssets.DirectoryExists("Config");

// 列出文件
string[] allXmls = BetterStreamingAssets.GetFiles("/", "*.xml", SearchOption.AllDirectories);
string[] configs = BetterStreamingAssets.GetFiles("Config", "*.xml");
```

#### Asset Bundle

```csharp
// 同步加载
var bundle = BetterStreamingAssets.LoadAssetBundle(path);

// 异步加载
var bundleOp = BetterStreamingAssets.LoadAssetBundleAsync(path);
```

## 平台说明

### Android 与 App Bundle

- StreamingAssets 中的文件名请全部使用小写
- 不要在文件名中使用非 ASCII 字符

### WebGL

不支持 WebGL 平台。

## 更新日志

详见 [CHANGELOG.md](CHANGELOG.md)。

## 开源协议

本项目基于 MIT 协议开源，详见 [LICENSE.md](LICENSE.md) 文件。
