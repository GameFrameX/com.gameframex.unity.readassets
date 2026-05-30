using System.IO;

#if UNITY_EDITOR

namespace GameFrameX.ReadAssets.Runtime
{
    public static partial class BlankReadAssets
    {
        internal static class EditorImpl
        {
            public static bool ApkMode = false;

            public static string s_root
            {
                get { return ApkMode ? ApkImpl.s_root : LooseFilesImpl.s_root; }
            }

            internal static void Initialize(string dataPath, string streamingAssetsPath)
            {
                if ( ApkMode )
                {
                    ApkImpl.Initialize(dataPath, streamingAssetsPath);
                }
                else
                {
                    LooseFilesImpl.Initialize(dataPath, streamingAssetsPath);
                }
            }

            internal static bool TryGetInfo(string path, out ReadInfo info)
            {
                if ( ApkMode )
                {
                    return ApkImpl.TryGetInfo(path, out info);
                }
                else
                {
                    return LooseFilesImpl.TryGetInfo(path, out info);
                }
            }

            internal static bool DirectoryExists(string path)
            {
                if ( ApkMode )
                {
                    return ApkImpl.DirectoryExists(path);
                }
                else
                {
                    return LooseFilesImpl.DirectoryExists(path);
                }
            }

            internal static Stream OpenRead(string path)
            {
                if ( ApkMode )
                {
                    return ApkImpl.OpenRead(path);
                }
                else
                {
                    return LooseFilesImpl.OpenRead(path);
                }
            }

            internal static byte[] ReadAllBytes(string path)
            {
                if ( ApkMode )
                {
                    return ApkImpl.ReadAllBytes(path);
                }
                else
                {
                    return LooseFilesImpl.ReadAllBytes(path);
                }
            }

            internal static string[] GetFiles(string path, string searchPattern, SearchOption searchOption)
            {
                if ( ApkMode )
                {
                    return ApkImpl.GetFiles(path, searchPattern, searchOption);
                }
                else
                {
                    return LooseFilesImpl.GetFiles(path, searchPattern, searchOption);
                }
            }
        }
    }
}

#endif
