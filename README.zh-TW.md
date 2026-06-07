<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />

# Game Frame X Read Assets

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.readassets)](https://github.com/GameFrameX/com.gameframex.unity.readassets/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.readassets)](https://github.com/GameFrameX/com.gameframex.unity.readassets/releases)
[![Documentation](https://img.shields.io/badge/Documentation-docs-blue)](https://gameframex.doc.alianblank.com)

獨立遊戲前後端一體化解決方案 · 獨立遊戲開發者的圓夢大使

<br />

[文檔](https://gameframex.doc.alianblank.com) · [快速開始](#快速開始) · [QQ群](https://qm.qq.com/q/5U9Fvebw)

<br />

[English](README.md) | [简体中文](README.zh-CN.md) | **繁體中文** | [日本語](README.ja.md) | [한국어](README.ko.md)

</div>
## 語言

[English](README.md) | [简体中文](README.zh-CN.md) | **繁體中文** | [日本語](README.ja.md) | [한국어](README.ko.md)

---

## 專案簡介

以統一且執行緒安全的方式直接存取 StreamingAssets，開銷極小。基於 [BetterStreamingAssets](https://github.com/gwiazdorrr/BetterStreamingAssets)，提供 `System.IO` 風格的 API，支援包括 Android APK 在內的所有平台。

所有公開 API 均標注了 `[Preserve]` 特性，可安全用於 IL2CPP 構建。

本函式庫主要服務於 [GameFrameX](https://github.com/AlianBlank/GameFrameX) 作為子庫使用。

## 快速開始

### 安裝方式（三種方式）

1. 直接在 `manifest.json` 檔案中加入以下內容：
   ```json
   {"com.gameframex.unity.readassets": "https://github.com/AlianBlank/com.gameframex.unity.readassets.git"}
   ```

2. 在 Unity 的 `Package Manager` 中使用 `Git URL` 新增：https://github.com/AlianBlank/com.gameframex.unity.readassets.git

3. 直接下載倉庫放置到 Unity 專案的 `Packages` 目錄下，會自動載入識別。

## 使用範例

### 基礎用法

無需手動初始化——首次呼叫 API 時會自動完成初始化。如需提前初始化，可手動呼叫：

```csharp
// 手動初始化（主執行緒）
BlankReadAssets.Initialize();

// 取得 StreamingAssets 根路徑
string root = BlankReadAssets.Root;
```

### 讀取檔案

```csharp
// 讀取全部位元組
byte[] data = BlankReadAssets.ReadAllBytes("Foo/bar.data");

// 以串流方式讀取
using (var stream = BlankReadAssets.OpenRead("Foo/bar.data"))
{
    // 從串流中讀取...
}

// 讀取全部文字
string text = BlankReadAssets.ReadAllText("Foo/config.xml");

// 讀取全部行
string[] lines = BlankReadAssets.ReadAllLines("Foo/data.txt");
```

### 檔案與目錄查詢

```csharp
// 檢查存在性
bool exists = BlankReadAssets.FileExists("Config/settings.json");
bool dirExists = BlankReadAssets.DirectoryExists("Config");

// 列出檔案
string[] allXmls = BlankReadAssets.GetFiles("/", "*.xml", SearchOption.AllDirectories);
string[] configs = BlankReadAssets.GetFiles("Config", "*.xml");
```

### Asset Bundle

```csharp
// 同步載入
var bundle = BlankReadAssets.LoadAssetBundle(path);

// 非同步載入
var bundleOp = BlankReadAssets.LoadAssetBundleAsync(path);
```

### Editor 擴展（僅編輯器可用）

```csharp
// 使用外部 APK 初始化（用於編輯器內測試 Android 建置）
BlankReadAssets.InitializeWithExternalApk("/path/to/app.apk");

// 使用自訂目錄初始化
BlankReadAssets.InitializeWithExternalDirectories(dataPath, streamingAssetsPath);
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
