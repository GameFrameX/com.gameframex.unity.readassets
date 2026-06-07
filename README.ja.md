<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />

# Game Frame X Read Assets

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.readassets)](https://github.com/GameFrameX/com.gameframex.unity.readassets/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.readassets)](https://github.com/GameFrameX/com.gameframex.unity.readassets/releases)
[![Unity Version](https://img.shields.io/badge/Unity-2019.4-black?logo=unity)](https://unity.com/)
[![Documentation](https://img.shields.io/badge/Documentation-docs-blue)](https://gameframex.doc.alianblank.com)

インディゲーム開発者向けオールインワンソリューション · インディ開発者の夢を支援

<br />

[ドキュメント](https://gameframex.doc.alianblank.com) · [クイックスタート](#クイックスタート) · QQグループ: 467608841 / 233840761

<br />

[English](README.md) | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | **日本語** | [한국어](README.ko.md)

</div>

## 言語

[English](README.md) | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | **日本語** | [한국어](README.ko.md)

---

## プロジェクト概要

StreamingAssets に統一的かつスレッドセーフな方法で直接アクセスし、最小限のオーバーヘッドで実現します。[BetterStreamingAssets](https://github.com/gwiazdorrr/BetterStreamingAssets) をベースに、Android APK を含むすべてのプラットフォームに対応する `System.IO` スタイルの API を提供します。

すべての公開 API には `[Preserve]` 属性が付与されており、IL2CPP ビルドでも安全に使用できます。

このライブラリは主に [GameFrameX](https://github.com/AlianBlank/GameFrameX) のサブライブラリとして使用されます。

## クイックスタート

### インストール（3つの方法）

1. `manifest.json` に以下を直接追加：
   ```json
   {"com.gameframex.unity.readassets": "https://github.com/AlianBlank/com.gameframex.unity.readassets.git"}
   ```

2. Unity の `Package Manager` で `Git URL` を使用して追加：https://github.com/AlianBlank/com.gameframex.unity.readassets.git

3. リポジトリを直接ダウンロードして Unity プロジェクトの `Packages` ディレクトリに配置。自動的に読み込まれます。

## 使用例

### 基本的な使い方

手動での初期化は不要です。初めて API を呼び出す際に自動的に初期化が行われます。事前に初期化したい場合は手動で呼び出してください：

```csharp
// 手動初期化（メインスレッド）
BlankReadAssets.Initialize();

// StreamingAssets のルートパスを取得
string root = BlankReadAssets.Root;
```

### ファイルの読み込み

```csharp
// 全バイトを読み込み
byte[] data = BlankReadAssets.ReadAllBytes("Foo/bar.data");

// ストリームとして読み込み
using (var stream = BlankReadAssets.OpenRead("Foo/bar.data"))
{
    // ストリームから読み込み...
}

// 全テキストを読み込み
string text = BlankReadAssets.ReadAllText("Foo/config.xml");

// 全行を読み込み
string[] lines = BlankReadAssets.ReadAllLines("Foo/data.txt");
```

### ファイルとディレクトリのクエリ

```csharp
// 存在確認
bool exists = BlankReadAssets.FileExists("Config/settings.json");
bool dirExists = BlankReadAssets.DirectoryExists("Config");

// ファイル一覧の取得
string[] allXmls = BlankReadAssets.GetFiles("/", "*.xml", SearchOption.AllDirectories);
string[] configs = BlankReadAssets.GetFiles("Config", "*.xml");
```

### アセットバンドル

```csharp
// 同期読み込み
var bundle = BlankReadAssets.LoadAssetBundle(path);

// 非同期読み込み
var bundleOp = BlankReadAssets.LoadAssetBundleAsync(path);
```

### エディタ拡張（エディタのみ）

```csharp
// 外部 APK を使用して初期化（エディタ内で Android ビルドをテストする場合）
BlankReadAssets.InitializeWithExternalApk("/path/to/app.apk");

// カスタムディレクトリを使用して初期化
BlankReadAssets.InitializeWithExternalDirectories(dataPath, streamingAssetsPath);
```

## プラットフォームに関する注意

### Android と App Bundle

- StreamingAssets 内のファイル名はすべて小文字にしてください
- ファイル名に非 ASCII 文字を使用しないでください

### WebGL

WebGL プラットフォームはサポートされていません。

## 変更履歴

詳細は [CHANGELOG.md](CHANGELOG.md) をご覧ください。

## ライセンス

このプロジェクトは MIT ライセンスの下で公開されています。詳細は [LICENSE.md](LICENSE.md) をご覧ください。
