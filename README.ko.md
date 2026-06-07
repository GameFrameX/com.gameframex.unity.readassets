<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />

# Game Frame X Read Assets

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.readassets)](https://github.com/GameFrameX/com.gameframex.unity.readassets/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.readassets)](https://github.com/GameFrameX/com.gameframex.unity.readassets/releases)
[![Unity Version](https://img.shields.io/badge/Unity-2019.4-black?logo=unity)](https://unity.com/)
[![Documentation](https://img.shields.io/badge/Documentation-docs-blue)](https://gameframex.doc.alianblank.com)

인디 게임 개발자를 위한 올인원 솔루션 · 인디 개발자의 꿈을 실현

<br />

[문서](https://gameframex.doc.alianblank.com) · [빠른 시작](#빠른-시작) · QQ 그룹: 467608841 / 233840761

<br />

[English](README.md) | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | [日本語](README.ja.md) | **한국어**

</div>

## 언어

[English](README.md) | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | [日本語](README.ja.md) | **한국어**

---

## 프로젝트 개요

StreamingAssets에 통일되고 스레드 안전한 방식으로 직접 액세스하며, 오버헤드가 매우 적습니다. [BetterStreamingAssets](https://github.com/gwiazdorrr/BetterStreamingAssets)를 기반으로 Android APK를 포함한 모든 플랫폼에서 `System.IO` 스타일의 API를 제공합니다.

모든 공개 API에 `[Preserve]` 특성이 지정되어 있어 IL2CPP 빌드에서도 안전하게 사용할 수 있습니다.

이 라이브러리는 주로 [GameFrameX](https://github.com/AlianBlank/GameFrameX)의 하위 라이브러리로 사용됩니다.

## 빠른 시작

### 설치

Unity 프로젝트의 `Packages/manifest.json`을 편집하여 `scopedRegistries` 섹션을 추가하세요:

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
  ]
}
```

`scopes`는 이 레지스트리를 통해 어떤 패키지를 해석할지 제어합니다. `com.gameframex`로 시작하는 패키지만 이 레지스트리에서 가져옵니다.

Then add the package to `dependencies`:

```json
{
  "dependencies": {
    "com.gameframex.unity.readassets": "1.2.0"
  }
}
```

## 사용 예제

### 기본 사용법

수동 초기화가 필요하지 않습니다. 첫 번째 API 호출 시 자동으로 초기화가 수행됩니다. 미리 초기화하려면 수동으로 호출하세요:

```csharp
// 수동 초기화 (메인 스레드)
BlankReadAssets.Initialize();

// StreamingAssets 루트 경로 가져오기
string root = BlankReadAssets.Root;
```

### 파일 읽기

```csharp
// 전체 바이트 읽기
byte[] data = BlankReadAssets.ReadAllBytes("Foo/bar.data");

// 스트림으로 읽기
using (var stream = BlankReadAssets.OpenRead("Foo/bar.data"))
{
    // 스트림에서 읽기...
}

// 전체 텍스트 읽기
string text = BlankReadAssets.ReadAllText("Foo/config.xml");

// 전체 줄 읽기
string[] lines = BlankReadAssets.ReadAllLines("Foo/data.txt");
```

### 파일 및 디렉토리 쿼리

```csharp
// 존재 여부 확인
bool exists = BlankReadAssets.FileExists("Config/settings.json");
bool dirExists = BlankReadAssets.DirectoryExists("Config");

// 파일 목록 가져오기
string[] allXmls = BlankReadAssets.GetFiles("/", "*.xml", SearchOption.AllDirectories);
string[] configs = BlankReadAssets.GetFiles("Config", "*.xml");
```

### 에셋 번들

```csharp
// 동기 로드
var bundle = BlankReadAssets.LoadAssetBundle(path);

// 비동기 로드
var bundleOp = BlankReadAssets.LoadAssetBundleAsync(path);
```

### 에디터 확장 (에디터 전용)

```csharp
// 외부 APK를 사용하여 초기화 (에디터에서 Android 빌드 테스트용)
BlankReadAssets.InitializeWithExternalApk("/path/to/app.apk");

// 사용자 정의 디렉토리로 초기화
BlankReadAssets.InitializeWithExternalDirectories(dataPath, streamingAssetsPath);
```

## 플랫폼 참고 사항

### Android 및 App Bundle

- StreamingAssets의 모든 파일 이름은 소문자를 사용하세요
- 파일 이름에 비 ASCII 문자를 사용하지 마세요

### WebGL

WebGL 플랫폼은 지원되지 않습니다.

## 변경 로그

자세한 내용은 [CHANGELOG.md](CHANGELOG.md)를 참조하세요.

## 라이선스

자세한 내용은 [LICENSE.md](LICENSE.md) 파일을 참조하세요.
