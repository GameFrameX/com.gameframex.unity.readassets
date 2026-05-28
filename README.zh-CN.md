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

Unity 中通过非 `WWW` 的方式读取 `StreamingAssets` 下的文件。

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

### 传入路径为相对目录

- **ReadBuffer** - 读取 `byte[]` 数组：

```csharp
byte[] buffer = BlankReadAssets.Read(string path);
```

- **IsFileExists** - 文件是否存在：

```csharp
bool isFileExists = BlankReadAssets.IsFileExists(string path);
```

## 更新日志

详见 [CHANGELOG.md](CHANGELOG.md)。

## 开源协议

本项目基于 MIT 协议开源，详见 [LICENSE.md](LICENSE.md) 文件。
