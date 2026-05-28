<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />

# Game Frame X Read Assets

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.readassets)](https://github.com/GameFrameX/com.gameframex.unity.readassets/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.readassets)](https://github.com/GameFrameX/com.gameframex.unity.readassets/releases)
[![Documentation](https://img.shields.io/badge/Documentation-docs-blue)](https://gameframex.doc.alianblank.com)

> 인디 게임 개발자를 위한 올인원 솔루션 · 인디 개발자의 꿈을 실현

[문서](https://gameframex.doc.alianblank.com) · [빠른 시작](#빠른-시작) · [QQ 그룹](https://qm.qq.com/q/5U9Fvebw) · [언어](#언어)


</div>

---

## 언어

[English](README.md) | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | [日本語](README.ja.md) | **한국어**

---

## 프로젝트 개요

Unity에서 `WWW`를 사용하지 않고 `StreamingAssets` 아래의 파일을 읽습니다.

이 라이브러리는 주로 `https://github.com/AlianBlank/GameFrameX`의 하위 라이브러리로 사용됩니다.

## 빠른 시작

### 설치 (세 가지 방법)

1. `manifest.json` 파일에 다음 내용을 직접 추가합니다:
   ```json
   {"com.gameframex.unity.readassets": "https://github.com/AlianBlank/com.gameframex.unity.readassets.git"}
   ```

2. Unity의 `Package Manager`에서 `Git URL`을 사용하여 추가: https://github.com/AlianBlank/com.gameframex.unity.readassets.git

3. 저장소를 직접 다운로드하여 Unity 프로젝트의 `Packages` 디렉토리에 배치합니다. 자동으로 로드됩니다.

## 사용 예시

### 상대 경로 입력

- **ReadBuffer** - `byte[]` 배열로 읽기:

```csharp
byte[] buffer = BlankReadAssets.Read(string path);
```

- **IsFileExists** - 파일 존재 여부 확인:

```csharp
bool isFileExists = BlankReadAssets.IsFileExists(string path);
```

## 변경 로그

자세한 내용은 [CHANGELOG.md](CHANGELOG.md)를 참조하세요.

## 라이선스

이 프로젝트는 MIT 라이선스에 따라 배포됩니다. 자세한 내용은 [LICENSE.md](LICENSE.md) 파일을 참조하세요.
