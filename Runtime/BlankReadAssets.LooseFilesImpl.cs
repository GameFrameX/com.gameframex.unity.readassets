using System;
using System.IO;
using System.Linq;
using UnityEngine;

#if UNITY_EDITOR || !UNITY_ANDROID

namespace GameFrameX.ReadAssets.Runtime
{
    public static partial class BlankReadAssets
    {
        internal static class LooseFilesImpl
        {
            public static string s_root;
            private static string[] s_emptyArray = new string[0];

            public static void Initialize(string dataPath, string streamingAssetsPath)
            {
                s_root = Path.GetFullPath(streamingAssetsPath).Replace('\\', '/').TrimEnd('/');
            }

            public static string[] GetFiles(string path, string searchPattern, SearchOption searchOption)
            {
                if (!Directory.Exists(s_root))
                {
                    return s_emptyArray;
                }

                path = PathUtil.NormalizeRelativePath(path, forceTrailingSlash : true);

                Debug.Assert(s_root.Last() != '\\' && s_root.Last() != '/' && path.StartsWith("/"));

                var files = Directory.GetFiles(s_root + path, searchPattern ?? "*", searchOption);

                for ( int i = 0; i < files.Length; ++i )
                {
                    Debug.Assert(files[i].StartsWith(s_root));
                    files[i] = files[i].Substring(s_root.Length + 1).Replace('\\', '/');
                }

#if UNITY_EDITOR
                {
                    int j = 0;
                    for ( int i = 0; i < files.Length; ++i )
                    {
                        if ( !files[i].EndsWith(".meta") )
                        {
                            files[j++] = files[i];
                        }
                    }
                    Array.Resize(ref files, j);
                }
#endif
                return files;
            }

            public static bool TryGetInfo(string path, out ReadInfo info)
            {
                path = PathUtil.NormalizeRelativePath(path);

                info = new ReadInfo();

                var fullPath = s_root + path;
                if ( !File.Exists(fullPath) )
                {
                    return false;
                }

                info.readPath = fullPath;
                return true;
            }

            public static bool DirectoryExists(string path)
            {
                var normalized = PathUtil.NormalizeRelativePath(path);
                return Directory.Exists(s_root + normalized);
            }

            public static byte[] ReadAllBytes(string path)
            {
                ReadInfo info;

                if ( !TryGetInfo(path, out info) )
                {
                    ThrowFileNotFound(path);
                }

                return File.ReadAllBytes(info.readPath);
            }

            public static Stream OpenRead(string path)
            {
                ReadInfo info;
                if ( !TryGetInfo(path, out info) )
                {
                    ThrowFileNotFound(path);
                }

                Stream fileStream = File.OpenRead(info.readPath);
                try
                {
                    return new SubReadOnlyStream(fileStream, leaveOpen: false);
                }
                catch ( System.Exception )
                {
                    fileStream.Dispose();
                    throw;
                }
            }
        }
    }
}

#endif
