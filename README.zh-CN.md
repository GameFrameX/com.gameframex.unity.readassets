<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />

# Game Frame X Read Assets

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.readassets)](https://github.com/GameFrameX/com.gameframex.unity.readassets/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.readassets)](https://github.com/GameFrameX/com.gameframex.unity.readassets/releases)
[![Unity Version](https://img.shields.io/badge/Unity-2019.4-black?logo=unity)](https://unity.com/)
[![Documentation](https://img.shields.io/badge/Documentation-docs-blue)](https://gameframex.doc.alianblank.com)

独立游戏前后端一体化解决方案 · 独立游戏开发者的圆梦大使

<br />

[文档](https://gameframex.doc.alianblank.com) · [快速开始](#快速开始) · QQ群: 467608841 / 233840761

<br />

[English](README.md) | **简体中文** | [繁體中文](README.zh-TW.md) | [日本語](README.ja.md) | [한국어](README.ko.md)

</div>

## 语言

[English](README.md) | **简体中文** | [繁體中文](README.zh-TW.md) | [日本語](README.ja.md) | [한국어](README.ko.md)

---

## 项目简介

以统一且线程安全的方式直接访问 StreamingAssets，开销极小。基于 [BetterStreamingAssets](https://github.com/gwiazdorrr/BetterStreamingAssets)，提供 `System.IO` 风格的 API，支持包括 Android APK 在内的所有平台。

所有公开 API 均标注了 `[Preserve]` 特性，可安全用于 IL2CPP 构建。

该库主要服务于 [GameFrameX](https://github.com/AlianBlank/GameFrameX) 作为子库使用。

## 快速开始

### 安装

选择以下任一方式：

1. 编辑 Unity 项目的 `Packages/manifest.json`，添加 `scopedRegistries` 部分：
   ```json
   {
     "scopedRegistries": [
       {
         "name": "GameFrameX",
         "url": "https://gameframex.upm.alianblank.uk",
         "scopes": [
           "com.gameframex"
         ]
       }
     ],
     "dependencies": {
       "com.gameframex.unity.readassets": "1.2.0"
     }
   }
   ```

   `scopes` 控制哪些包通过此注册表解析。只有以 `com.gameframex` 开头的包才会从这个注册表获取。

2. 直接在 `manifest.json` 的 `dependencies` 节点下添加以下内容：
   ```json
   {
      "com.gameframex.unity.readassets": "https://github.com/gameframex/com.gameframex.unity.readassets.git"
   }
   ```
3. 在 Unity 的 `Package Manager` 中使用 `Git URL` 的方式添加库，地址为：`https://github.com/gameframex/com.gameframex.unity.readassets.git`
4. 直接下载仓库放置到 Unity 项目的 `Packages` 目录下，会自动加载识别。
## 使用示例

### 基础用法

无需手动初始化——首次调用 API 时会自动完成初始化。如需提前初始化，可手动调用：

```csharp
// 手动初始化（主线程）
BlankReadAssets.Initialize();

// 获取 StreamingAssets 根路径
string root = BlankReadAssets.Root;
```

### 读取文件

```csharp
// 读取全部字节
byte[] data = BlankReadAssets.ReadAllBytes("Foo/bar.data");

// 以流方式读取
using (var stream = BlankReadAssets.OpenRead("Foo/bar.data"))
{
    // 从流中读取...
}

// 读取全部文本
string text = BlankReadAssets.ReadAllText("Foo/config.xml");

// 读取全部行
string[] lines = BlankReadAssets.ReadAllLines("Foo/data.txt");
```

### 文件与目录查询

```csharp
// 检查存在性
bool exists = BlankReadAssets.FileExists("Config/settings.json");
bool dirExists = BlankReadAssets.DirectoryExists("Config");

// 列出文件
string[] allXmls = BlankReadAssets.GetFiles("/", "*.xml", SearchOption.AllDirectories);
string[] configs = BlankReadAssets.GetFiles("Config", "*.xml");
```

### Asset Bundle

```csharp
// 同步加载
var bundle = BlankReadAssets.LoadAssetBundle(path);

// 异步加载
var bundleOp = BlankReadAssets.LoadAssetBundleAsync(path);
```

### Editor 扩展（仅编辑器可用）

```csharp
// 使用外部 APK 初始化（用于编辑器内测试 Android 构建）
BlankReadAssets.InitializeWithExternalApk("/path/to/app.apk");

// 使用自定义目录初始化
BlankReadAssets.InitializeWithExternalDirectories(dataPath, streamingAssetsPath);
```

## 平台说明

### Android 与 App Bundle

- StreamingAssets 中的文件名请全部使用小写
- 不要在文件名中使用非 ASCII 字符

### WebGL

不支持 WebGL 平台。

## 更新日志

详见 [CHANGELOG.md](CHANGELOG.md)。


## 依赖

| 包 | 说明 |
|----|------|
| (无) | - |


## 文档与资源

- [官方文档](https://gameframex.doc.alianblank.com)

## 社区与支持

- QQ群: 467608841 / 233840761
## 开源协议

详见 [LICENSE.md](LICENSE.md) 文件。
