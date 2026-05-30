<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />

# Game Frame X Read Assets

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.readassets)](https://github.com/GameFrameX/com.gameframex.unity.readassets/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.readassets)](https://github.com/GameFrameX/com.gameframex.unity.readassets/releases)
[![Documentation](https://img.shields.io/badge/Documentation-docs-blue)](https://gameframex.doc.alianblank.com)

> All-in-One Solution for Indie Game Development · Empowering Indie Developers' Dreams

[Documentation](https://gameframex.doc.alianblank.com) · [Quick Start](#quick-start) · [QQ Group](https://qm.qq.com/q/5U9Fvebw) · [Language](#language)


</div>

---

## Language

**English** | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | [日本語](README.ja.md) | [한국어](README.ko.md)

---

## Project Overview

Access StreamingAssets directly in a uniform and thread-safe way with tiny overhead. Based on [BetterStreamingAssets](https://github.com/gwiazdorrr/BetterStreamingAssets), provides `System.IO`-style APIs for all platforms including Android APK.

All public APIs are annotated with `[Preserve]` to prevent stripping in IL2CPP builds.

This library primarily serves as a sub-library for [GameFrameX](https://github.com/AlianBlank/GameFrameX).

## Quick Start

### Installation (three methods)

1. Add the following content directly to `manifest.json`:
   ```json
   {"com.gameframex.unity.readassets": "https://github.com/AlianBlank/com.gameframex.unity.readassets.git"}
   ```

2. Add via Unity's `Package Manager` using `Git URL`: https://github.com/AlianBlank/com.gameframex.unity.readassets.git

3. Download the repository directly and place it in the Unity project's `Packages` directory. It will be auto-loaded.

## Usage Examples

### Basic Usage

No manual initialization needed — APIs auto-initialize on first call. To initialize in advance:

```csharp
// Manual initialization (main thread)
BlankReadAssets.Initialize();

// Get StreamingAssets root path
string root = BlankReadAssets.Root;
```

### Reading Files

```csharp
// Read all bytes
byte[] data = BlankReadAssets.ReadAllBytes("Foo/bar.data");

// Read as stream
using (var stream = BlankReadAssets.OpenRead("Foo/bar.data"))
{
    // read from stream...
}

// Read all text
string text = BlankReadAssets.ReadAllText("Foo/config.xml");

// Read all lines
string[] lines = BlankReadAssets.ReadAllLines("Foo/data.txt");
```

### File & Directory Queries

```csharp
// Check existence
bool exists = BlankReadAssets.FileExists("Config/settings.json");
bool dirExists = BlankReadAssets.DirectoryExists("Config");

// List files
string[] allXmls = BlankReadAssets.GetFiles("/", "*.xml", SearchOption.AllDirectories);
string[] configs = BlankReadAssets.GetFiles("Config", "*.xml");
```

### Asset Bundles

```csharp
// Synchronous
var bundle = BlankReadAssets.LoadAssetBundle(path);

// Asynchronous
var bundleOp = BlankReadAssets.LoadAssetBundleAsync(path);
```

### Editor Extensions (Editor Only)

```csharp
// Initialize with an external APK (for testing Android builds in Editor)
BlankReadAssets.InitializeWithExternalApk("/path/to/app.apk");

// Initialize with custom directories
BlankReadAssets.InitializeWithExternalDirectories(dataPath, streamingAssetsPath);
```

## Platform Notes

### Android & App Bundles

- Keep all file names in StreamingAssets lowercase
- Do not use non-ASCII characters in file names

### WebGL

WebGL is not supported.

## Changelog

See [CHANGELOG.md](CHANGELOG.md) for details.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.
