using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameFrameX.ReadAssets.Runtime
{
    /// <summary>
    /// Android 平台资产文件读取工具，通过 JNI 直接调用 Android AssetManager API 读取 APK 内 assets 目录下的文件。
    /// 无需原生插件（AAR/JAR），纯 C# 实现。
    /// </summary>
    public static class BlankReadAssets
    {
        private static readonly object _lock = new object();
        private static readonly HashSet<string> _fileCache = new HashSet<string>();

#if UNITY_ANDROID
        private static AndroidJavaObject _assetManager;

        private static void EnsureAssetManager()
        {
            if (_assetManager == null)
            {
                lock (_lock)
                {
                    if (_assetManager == null)
                    {
                        using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
                        using (var activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
                        using (var application = activity.Call<AndroidJavaObject>("getApplication"))
                        {
                            _assetManager = application.Call<AndroidJavaObject>("getAssets");
                        }
                    }
                }
            }
        }
#endif

        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="path">相对目录</param>
        /// <returns>文件字节数组，失败返回 null</returns>
        public static byte[] Read(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("Path cannot be null or empty.", "path");
            }

#if UNITY_ANDROID
            EnsureAssetManager();

            AndroidJavaObject inputStream = null;
            try
            {
                inputStream = _assetManager.Call<AndroidJavaObject>("open", path);
                int size = inputStream.Call<int>("available");

                IntPtr rawStream = inputStream.GetRawObject();
                IntPtr rawClass = AndroidJNI.GetObjectClass(rawStream);
                IntPtr readMethodId = AndroidJNIHelper.GetMethodID(rawClass, "read", "([B)I");

                IntPtr javaBuffer = AndroidJNI.NewByteArray(size);
                jvalue[] readArgs = new jvalue[] { new jvalue { l = javaBuffer } };
                AndroidJNI.CallIntMethod(rawStream, readMethodId, readArgs);

                byte[] result = AndroidJNI.FromByteArray(javaBuffer);
                AndroidJNI.DeleteLocalRef(javaBuffer);

                inputStream.Call("close");
                _fileCache.Add(path);
                return result;
            }
            catch (Exception e)
            {
                Debug.LogError("[BlankReadAssets] Failed to read file '" + path + "': " + e.Message);
                return null;
            }
            finally
            {
                if (inputStream != null)
                {
                    inputStream.Dispose();
                }
            }
#else
            Debug.LogWarning("[BlankReadAssets] Read is only supported on Android platform.");
            return null;
#endif
        }

        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        /// <param name="path">相对目录</param>
        /// <returns>文件是否存在</returns>
        public static bool IsFileExists(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return false;
            }

            if (_fileCache.Contains(path))
            {
                return true;
            }

#if UNITY_ANDROID
            EnsureAssetManager();

            AndroidJavaObject inputStream = null;
            try
            {
                inputStream = _assetManager.Call<AndroidJavaObject>("open", path);
                inputStream.Call("close");
                _fileCache.Add(path);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (inputStream != null)
                {
                    inputStream.Dispose();
                }
            }
#else
            return false;
#endif
        }
    }
}
