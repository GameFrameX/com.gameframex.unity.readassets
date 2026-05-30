<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />

# Game Frame X Read Assets

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.readassets)](https://github.com/GameFrameX/com.gameframex.unity.readassets/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.readassets)](https://github.com/GameFrameX/com.gameframex.unity.readassets/releases)
[![Documentation](https://img.shields.io/badge/Documentation-docs-blue)](https://gameframex.doc.alianblank.com)

> 獨立遊戲前後端一體化解決方案 · 獨立遊戲開發者的圓夢大使

[文件](https://gameframex.doc.alianblank.com) · [快速開始](#快速開始) · [QQ群](https://qm.qq.com/q/5U9Fvebw) · [語言](#語言)


</div>

---

## 語言

[English](README.md) | [简体中文](README.zh-CN.md) | **繁體中文** | [日本語](README.ja.md) | [한국어](README.ko.md)

---

## 專案簡介

以統一且執行緒安全的方式直接存取 StreamingAssets，開銷極小。基於 [BetterStreamingAssets](https://github.com/gwiazdorrr/BetterStreamingAssets)，提供 `System.IO` 風格的 API，支援包括 Android APK 在內的所有平台。

本函式庫主要服務於 `https://github.com/AlianBlank/GameFrameX` 作為子庫使用。

## 快速開始

### 安裝方式（三種方式）

1. 直接在 `manifest.json` 檔案中加入以下內容
   ```json
   {"com.gameframex.unity.readassets": "https://github.com/AlianBlank/com.gameframex.unity.readassets.git"}
   ```

2. 在 Unity 的 `Packages Manager` 中使用 `Git URL` 的方式新增函式庫，網址為：https://github.com/AlianBlank/com.gameframex.unity.readassets.git

3. 直接下載倉庫放置到 Unity 專案的 `Packages` 目錄下。會自動載入識別。

## 使用範例

### 向後相容 API（BlankReadAssets）

- **Read** - 讀取 `byte[]` 陣列（失敗傳回 `null`）：

```csharp
byte[] buffer = BlankReadAssets.Read(string path);
```

- **IsFileExists** - 檔案是否存在：

```csharp
bool isFileExists = BlankReadAssets.IsFileExists(string path);
```

### 完整 API（BetterStreamingAssets）

```csharp
// 首次使用前初始化（需要在主執行緒呼叫）
BetterStreamingAssets.Initialize();
```

#### 讀取檔案

```csharp
// 讀取全部位元組
byte[] data = BetterStreamingAssets.ReadAllBytes("Foo/bar.data");

// 以串流方式讀取
using (var stream = BetterStreamingAssets.OpenRead("Foo/bar.data"))
{
    // 從串流中讀取...
}

// 讀取全部文字
string text = BetterStreamingAssets.ReadAllText("Foo/config.xml");

// 讀取全部行
string[] lines = BetterStreamingAssets.ReadAllLines("Foo/data.txt");
```

#### 檔案與目錄查詢

```csharp
// 檢查存在性
bool exists = BetterStreamingAssets.FileExists("Config/settings.json");
bool dirExists = BetterStreamingAssets.DirectoryExists("Config");

// 列出檔案
string[] allXmls = BetterStreamingAssets.GetFiles("/", "*.xml", SearchOption.AllDirectories);
string[] configs = BetterStreamingAssets.GetFiles("Config", "*.xml");
```

#### Asset Bundle

```csharp
// 同步載入
var bundle = BetterStreamingAssets.LoadAssetBundle(path);

// 非同步載入
var bundleOp = BetterStreamingAssets.LoadAssetBundleAsync(path);
```

## 平台說明

### Android 與 App Bundle

- StreamingAssets 中的檔案名稱請全部使用小寫
- 不要在檔案名稱中使用非 ASCII 字元

### WebGL

不支援 WebGL 平台。

## 更新日誌

詳見 [CHANGELOG.md](CHANGELOG.md)。

## 開源協議

本專案基於 MIT 協議開源，詳見 [LICENSE.md](LICENSE.md) 檔案。
