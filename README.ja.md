<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />

# Game Frame X Read Assets

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.readassets)](https://github.com/GameFrameX/com.gameframex.unity.readassets/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.readassets)](https://github.com/GameFrameX/com.gameframex.unity.readassets/releases)
[![Documentation](https://img.shields.io/badge/Documentation-docs-blue)](https://gameframex.doc.alianblank.com)

> インディーゲーム開発のオールインワンソリューション · インディー開発者の夢を応援

[ドキュメント](https://gameframex.doc.alianblank.com) · [クイックスタート](#クイックスタート) · [QQグループ](https://qm.qq.com/q/5U9Fvebw) · [言語](#言語)


</div>

---

## 言語

[English](README.md) | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | **日本語** | [한국어](README.ko.md)

---

## プロジェクト概要

StreamingAssets に統一的かつスレッドセーフな方法で直接アクセスし、最小限のオーバーヘッドでアクセスできます。[BetterStreamingAssets](https://github.com/gwiazdorrr/BetterStreamingAssets) をベースに、Android APK を含むすべてのプラットフォームに対応する `System.IO` スタイルの API を提供します。

このライブラリは主に `https://github.com/AlianBlank/GameFrameX` のサブライブラリとして使用されます。

## クイックスタート

### インストール（3つの方法）

1. `manifest.json` に以下を直接追加：
   ```json
   {"com.gameframex.unity.readassets": "https://github.com/AlianBlank/com.gameframex.unity.readassets.git"}
   ```

2. Unity の `Package Manager` で `Git URL` を使用して追加：https://github.com/AlianBlank/com.gameframex.unity.readassets.git

3. リポジトリを直接ダウンロードして Unity プロジェクトの `Packages` ディレクトリに配置。自動的に読み込まれます。

## 使用例

### 後方互換 API（BlankReadAssets）

- **Read** - `byte[]` 配列として読み込み（失敗時は `null` を返す）：

```csharp
byte[] buffer = BlankReadAssets.Read(string path);
```

- **IsFileExists** - ファイルの存在確認：

```csharp
bool isFileExists = BlankReadAssets.IsFileExists(string path);
```

### フル API（BetterStreamingAssets）

```csharp
// 初回使用前に初期化（メインスレッドで呼び出す必要があります）
BetterStreamingAssets.Initialize();
```

#### ファイルの読み込み

```csharp
// 全バイトを読み込み
byte[] data = BetterStreamingAssets.ReadAllBytes("Foo/bar.data");

// ストリームとして読み込み
using (var stream = BetterStreamingAssets.OpenRead("Foo/bar.data"))
{
    // ストリームから読み込み...
}

// 全テキストを読み込み
string text = BetterStreamingAssets.ReadAllText("Foo/config.xml");

// 全行を読み込み
string[] lines = BetterStreamingAssets.ReadAllLines("Foo/data.txt");
```

#### ファイルとディレクトリのクエリ

```csharp
// 存在確認
bool exists = BetterStreamingAssets.FileExists("Config/settings.json");
bool dirExists = BetterStreamingAssets.DirectoryExists("Config");

// ファイル一覧の取得
string[] allXmls = BetterStreamingAssets.GetFiles("/", "*.xml", SearchOption.AllDirectories);
string[] configs = BetterStreamingAssets.GetFiles("Config", "*.xml");
```

#### アセットバンドル

```csharp
// 同期読み込み
var bundle = BetterStreamingAssets.LoadAssetBundle(path);

// 非同期読み込み
var bundleOp = BetterStreamingAssets.LoadAssetBundleAsync(path);
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
