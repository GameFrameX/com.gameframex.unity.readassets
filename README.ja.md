<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />

# Game Frame X Read Assets

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.readassets)](https://github.com/GameFrameX/com.gameframex.unity.readassets/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.readassets)](https://github.com/GameFrameX/com.gameframex.unity.readassets/releases)
[![Documentation](https://img.shields.io/badge/Documentation-docs-blue)](https://gameframex.doc.alianblank.com)

> インディゲーム開発者向けオールインワンソリューション · インディ開発者の夢を支援

[ドキュメント](https://gameframex.doc.alianblank.com) · [クイックスタート](#クイックスタート) · [QQグループ](https://qm.qq.com/q/5U9Fvebw) · [言語](#言語)


</div>

---

## 言語

[English](README.md) | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | **日本語** | [한국어](README.ko.md)

---

## プロジェクト概要

Unity で `WWW` を使用せずに `StreamingAssets` 下のファイルを読み取ります。

このライブラリは主に `https://github.com/AlianBlank/GameFrameX` のサブライブラリとして使用されます。

## クイックスタート

### インストール（3つの方法）

1. `manifest.json` ファイルに以下の内容を直接追加します：
   ```json
   {"com.gameframex.unity.readassets": "https://github.com/AlianBlank/com.gameframex.unity.readassets.git"}
   ```

2. Unity の `Package Manager` で `Git URL` を使用して追加：https://github.com/AlianBlank/com.gameframex.unity.readassets.git

3. リポジトリを直接ダウンロードして、Unity プロジェクトの `Packages` ディレクトリに配置します。自動的に読み込まれます。

## 使用例

### 相対パスを入力

- **ReadBuffer** - `byte[]` 配列として読み取る：

```csharp
byte[] buffer = BlankReadAssets.Read(string path);
```

- **IsFileExists** - ファイルが存在するか確認：

```csharp
bool isFileExists = BlankReadAssets.IsFileExists(string path);
```

## 変更履歴

詳細は [CHANGELOG.md](CHANGELOG.md) をご覧ください。

## ライセンス

このプロジェクトは MIT ライセンスの下で公開されています。詳細は [LICENSE.md](LICENSE.md) ファイルをご覧ください。
