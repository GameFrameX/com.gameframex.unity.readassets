# 简介

Unity 中通过非 `WWW` 的方式读取 `StreamingAssets` 下的文件

该库主要服务于 `https://github.com/AlianBlank/GameFrameX` 作为子库使用。

# 使用方式(三种方式)

1. 直接在 `manifest.json` 文件中添加以下内容
   ```json
      {"com.alianblank.gameframex.unity.readassets": "https://github.com/AlianBlank/com.alianblank.gameframex.unity.readassets.git"}
    ```
2. 在Unity 的`Packages Manager` 中使用`Git URL` 的方式添加库,地址为：https://github.com/AlianBlank/com.alianblank.gameframex.unity.readassets.git

3. 直接下载仓库放置到Unity 项目的`Packages` 目录下。会自动加载识别

# 使用方式

## `传入路径为相对目录`

- ReadBuffer 读取Byte[] 数组

```
 byte[] buffer = BlankReadAssets.Read(string path)
```

- IsFileExists 文件是否存在

```
 bool isFileExists = BlankReadAssets.IsFileExists(string path)
```
