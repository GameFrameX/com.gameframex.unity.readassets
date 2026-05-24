<div align="center">
  <img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />
</div>

# Game Frame X Read Assets

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.readassets)](https://github.com/GameFrameX/com.gameframex.unity.readassets/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.readassets)](https://github.com/GameFrameX/com.gameframex.unity.readassets/releases)
[![Documentation](https://img.shields.io/badge/Documentation-docs-blue)](https://gameframex.doc.alianblank.com)

> 獨立遊戲前後端一體化解決方案 · 獨立遊戲開發者的圓夢大使

[文檔](https://gameframex.doc.alianblank.com) · [快速開始](#快速開始) · [QQ群](https://qm.qq.com/q/5U9Fvebw) · [語言](#語言)

---

## 語言

[English](README.md) | [简体中文](README.zh-CN.md) | **繁體中文** | [日本語](README.ja.md) | [한국어](README.ko.md)

---

## 項目簡介

在 Unity 中透過非 `WWW` 的方式讀取 `StreamingAssets` 下的檔案。

此套件主要服務於 `https://github.com/AlianBlank/GameFrameX` 作為子套件使用。

## 快速開始

### 使用方式（三種方式）

1. 直接在 `manifest.json` 檔案中新增以下內容
   ```json
   {"com.gameframex.unity.readassets": "https://github.com/AlianBlank/com.gameframex.unity.readassets.git"}
   ```

2. 在 Unity 的 `Packages Manager` 中使用 `Git URL` 的方式新增套件，地址為：https://github.com/AlianBlank/com.gameframex.unity.readassets.git

3. 直接下載倉庫放置到 Unity 專案的 `Packages` 目錄下。會自動載入識別。

## 使用範例

### 傳入路徑為相對目錄

- **ReadBuffer** - 讀取 `byte[]` 陣列：

```csharp
byte[] buffer = BlankReadAssets.Read(string path);
```

- **IsFileExists** - 檔案是否存在：

```csharp
bool isFileExists = BlankReadAssets.IsFileExists(string path);
```

## 更新日誌

詳見 [CHANGELOG.md](CHANGELOG.md)。

## 開源協議

本專案基於 MIT 協議開源，詳見 [LICENSE.md](LICENSE.md) 檔案。
