using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using GameFrameX.ReadAssets.Runtime.ZipArchive;
using UnityEngine.Scripting;

#if UNITY_EDITOR
using BlankReadAssetsImpl = GameFrameX.ReadAssets.Runtime.BlankReadAssets.EditorImpl;
#elif UNITY_ANDROID
using BlankReadAssetsImpl = GameFrameX.ReadAssets.Runtime.BlankReadAssets.ApkImpl;
#else
using BlankReadAssetsImpl = GameFrameX.ReadAssets.Runtime.BlankReadAssets.LooseFilesImpl;
#endif

namespace GameFrameX.ReadAssets.Runtime
{
    /// <summary>
    /// StreamingAssets 文件读取工具，以统一且线程安全的方式直接访问 StreamingAssets。
    /// 支持包括 Android APK 在内的所有平台。首次调用 API 时自动初始化。
    /// </summary>
    public static partial class BlankReadAssets
    {
        internal struct ReadInfo
        {
            public string readPath;
            public long size;
            public long offset;
            public uint crc32;
        }

        private static bool s_initialized;

        private static void EnsureInitialized()
        {
            if (!s_initialized)
            {
                Initialize();
                s_initialized = true;
            }
        }

        [Preserve]
        public static string Root
        {
            get
            {
                EnsureInitialized();
                return BlankReadAssetsImpl.s_root;
            }
        }

        [Preserve]
        public static void Initialize()
        {
            BlankReadAssetsImpl.Initialize(Application.dataPath, Application.streamingAssetsPath);
            s_initialized = true;
        }

        [Preserve]
        public static void Initialize(string dataPath, string streamingAssetsPath)
        {
            BlankReadAssetsImpl.Initialize(dataPath, streamingAssetsPath);
            s_initialized = true;
        }

        /// <summary>
        /// Android only: raised when there's a Streaming Asset that is compressed.
        /// </summary>
        public static event Func<string, bool> CompressedStreamingAssetFound
#if UNITY_EDITOR || UNITY_ANDROID
            ;
#else
            { add { } remove { } }
#endif

#if UNITY_EDITOR
        [Preserve]
        public static void InitializeWithExternalApk(string apkPath)
        {
            BlankReadAssetsImpl.ApkMode = true;
            BlankReadAssetsImpl.Initialize(apkPath, "jar:file://" + apkPath + "!/assets/");
            s_initialized = true;
        }

        [Preserve]
        public static void InitializeWithExternalDirectories(string dataPath, string streamingAssetsPath)
        {
            BlankReadAssetsImpl.ApkMode = false;
            BlankReadAssetsImpl.Initialize(dataPath, streamingAssetsPath);
            s_initialized = true;
        }
#endif

        /// <summary>
        /// 检查文件是否存在
        /// </summary>
        /// <param name="path">相对路径</param>
        /// <returns>文件是否存在</returns>
        [Preserve]
        public static bool FileExists(string path)
        {
            EnsureInitialized();
            ReadInfo info;
            return BlankReadAssetsImpl.TryGetInfo(path, out info);
        }

        [Preserve]
        public static bool DirectoryExists(string path)
        {
            EnsureInitialized();
            return BlankReadAssetsImpl.DirectoryExists(path);
        }

        [Preserve]
        public static AssetBundleCreateRequest LoadAssetBundleAsync(string path, uint crc = 0)
        {
            var info = GetInfoOrThrow(path);
            return AssetBundle.LoadFromFileAsync(info.readPath, crc, (ulong)info.offset);
        }

        [Preserve]
        public static AssetBundle LoadAssetBundle(string path, uint crc = 0)
        {
            var info = GetInfoOrThrow(path);
            return AssetBundle.LoadFromFile(info.readPath, crc, (ulong)info.offset);
        }

        [Preserve]
        public static Stream OpenRead(string path)
        {
            if ( path == null )
            {
                throw new ArgumentNullException("path");
            }
            if ( path.Length == 0 )
            {
                throw new ArgumentException("Empty path", "path");
            }

            EnsureInitialized();
            return BlankReadAssetsImpl.OpenRead(path);
        }

        [Preserve]
        public static StreamReader OpenText(string path)
        {
            Stream str = OpenRead(path);
            try
            {
                return new StreamReader(str);
            }
            catch (System.Exception)
            {
                if (str != null)
                {
                    str.Dispose();
                }
                throw;
            }
        }

        [Preserve]
        public static string ReadAllText(string path)
        {
            using ( var sr = OpenText(path) )
            {
                return sr.ReadToEnd();
            }
        }

        [Preserve]
        public static string[] ReadAllLines(string path)
        {
            string line;
            var lines = new List<string>();

            using ( var sr = OpenText(path) )
            {
                while ( ( line = sr.ReadLine() ) != null )
                {
                    lines.Add(line);
                }
            }

            return lines.ToArray();
        }

        /// <summary>
        /// 读取文件全部字节
        /// </summary>
        /// <param name="path">相对路径</param>
        /// <returns>文件字节数组</returns>
        [Preserve]
        public static byte[] ReadAllBytes(string path)
        {
            if ( path == null )
            {
                throw new ArgumentNullException("path");
            }
            if ( path.Length == 0 )
            {
                throw new ArgumentException("Empty path", "path");
            }

            EnsureInitialized();
            return BlankReadAssetsImpl.ReadAllBytes(path);
        }

        [Preserve]
        public static string[] GetFiles(string path, string searchPattern, SearchOption searchOption)
        {
            EnsureInitialized();
            return BlankReadAssetsImpl.GetFiles(path, searchPattern, searchOption);
        }

        [Preserve]
        public static string[] GetFiles(string path)
        {
            return GetFiles(path, null);
        }

        [Preserve]
        public static string[] GetFiles(string path, string searchPattern)
        {
            return GetFiles(path, searchPattern, SearchOption.TopDirectoryOnly);
        }

        private static ReadInfo GetInfoOrThrow(string path)
        {
            EnsureInitialized();
            ReadInfo result;
            if ( !BlankReadAssetsImpl.TryGetInfo(path, out result) )
            {
                ThrowFileNotFound(path);
            }
            return result;
        }

        private static void ThrowFileNotFound(string path)
        {
            throw new FileNotFoundException("File not found", path);
        }

        static partial void AndroidIsCompressedFileStreamingAsset(string path, ref bool result);
    }
}
