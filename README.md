<div align="center">
  <img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />
</div>

# Game Frame X Read Assets

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.readassets)](https://github.com/GameFrameX/com.gameframex.unity.readassets/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.readassets)](https://github.com/GameFrameX/com.gameframex.unity.readassets/releases)
[![Documentation](https://img.shields.io/badge/Documentation-docs-blue)](https://gameframex.doc.alianblank.com)

> All-in-One Solution for Indie Game Development · Empowering Indie Developers' Dreams

[Documentation](https://gameframex.doc.alianblank.com) · [Quick Start](#quick-start) · [QQ Group](https://qm.qq.com/q/5U9Fvebw) · [Language](#language)

---

## Language

**English** | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | [日本語](README.ja.md) | [한국어](README.ko.md)

---

## Project Overview

Read files under `StreamingAssets` in Unity through synchronous interfaces (without using `WWW`).

This library primarily serves as a sub-library for `https://github.com/AlianBlank/GameFrameX`.

## Quick Start

### Installation (three methods)

1. Add the following content directly to `manifest.json`:
   ```json
   {"com.gameframex.unity.readassets": "https://github.com/AlianBlank/com.gameframex.unity.readassets.git"}
   ```

2. Add via Unity's `Package Manager` using `Git URL`: https://github.com/AlianBlank/com.gameframex.unity.readassets.git

3. Download the repository directly and place it in the Unity project's `Packages` directory. It will be auto-loaded.

## Usage Examples

### Relative path input

- **ReadBuffer** - Read as `byte[]` array:

```csharp
byte[] buffer = BlankReadAssets.Read(string path);
```

- **IsFileExists** - Check if file exists:

```csharp
bool isFileExists = BlankReadAssets.IsFileExists(string path);
```

## Changelog

See [CHANGELOG.md](CHANGELOG.md) for details.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.
