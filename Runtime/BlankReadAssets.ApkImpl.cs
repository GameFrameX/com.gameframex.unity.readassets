using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using GameFrameX.ReadAssets.Runtime.ZipArchive;

#if UNITY_EDITOR || UNITY_ANDROID

namespace GameFrameX.ReadAssets.Runtime
{
    public static partial class BlankReadAssets
    {
        internal static class ApkImpl
        {
            private static string[] s_paths;
            private static PartInfo[] s_streamingAssets;
            public static string s_root;

            private struct PartInfo
            {
                public long size;
                public long offset;
                public uint crc32;
            }

            public static void Initialize(string dataPath, string streamingAssetsPath)
            {
                s_root = dataPath;

                List<string> paths = new List<string>();
                List<PartInfo> parts = new List<PartInfo>();

                if (dataPath.EndsWith("pram-shadow-files"))
                {
                    Debug.Assert(streamingAssetsPath.EndsWith("pram-shadow-files/assets"));
                    GetStreamingAssetsFromPatch(dataPath, paths, parts);
                }
                else
                {
                    GetStreamingAssetsInfoFromJar(s_root, paths, parts);

                    if (paths.Count == 0 && !Application.isEditor && Path.GetFileName(dataPath) != "base.apk")
                    {
                        var newDataPath = Path.GetDirectoryName(dataPath) + "/base.apk";
                        if (File.Exists(newDataPath))
                        {
                            s_root = newDataPath;
                            GetStreamingAssetsInfoFromJar(newDataPath, paths, parts);
                        }
                    }
                }

                s_paths = paths.ToArray();
                s_streamingAssets = parts.ToArray();
            }

            public static bool TryGetInfo(string path, out ReadInfo info)
            {
                path = PathUtil.NormalizeRelativePath(path);
                info = new ReadInfo();

                var index = Array.BinarySearch(s_paths, path, StringComparer.OrdinalIgnoreCase);
                if ( index < 0 )
                {
                    return false;
                }

                var dataInfo = s_streamingAssets[index];
                info.size = dataInfo.size;

                if (dataInfo.offset < 0)
                {
                    info.readPath = s_root + "/assets" + path;
                }
                else
                {
                    info.crc32 = dataInfo.crc32;
                    info.offset = dataInfo.offset;
                    info.readPath = s_root;
                }

                return true;
            }

            public static bool DirectoryExists(string path)
            {
                var normalized = PathUtil.NormalizeRelativePath(path, forceTrailingSlash : true);
                var dirIndex = GetDirectoryIndex(normalized);
                return dirIndex >= 0 && dirIndex < s_paths.Length;
            }

            public static string[] GetFiles(string path, string searchPattern, SearchOption searchOption)
            {
                if ( path == null )
                {
                    throw new ArgumentNullException("path");
                }

                var actualDirPath = PathUtil.NormalizeRelativePath(path, forceTrailingSlash : true);

                var index = GetDirectoryIndex(actualDirPath);
                if ( index < 0 )
                {
                    throw new IOException();
                }
                if ( index == s_paths.Length )
                {
                    throw new DirectoryNotFoundException();
                }

                Predicate<string> filter;
                if ( string.IsNullOrEmpty(searchPattern) || searchPattern == "*" )
                {
                    filter = null;
                }
                else if ( searchPattern.IndexOf('*') >= 0 || searchPattern.IndexOf('?') >= 0 )
                {
                    var regex = PathUtil.WildcardToRegex(searchPattern);
                    filter = (x) => regex.IsMatch(x);
                }
                else
                {
                    filter = (x) => string.Compare(x, searchPattern, true) == 0;
                }

                List<string> results = new List<string>();

                for ( int i = index; i < s_paths.Length; ++i )
                {
                    var filePath = s_paths[i];

                    if ( !filePath.StartsWith(actualDirPath) )
                    {
                        break;
                    }

                    string fileName;

                    var dirSeparatorIndex = filePath.LastIndexOf('/', filePath.Length - 1, filePath.Length - actualDirPath.Length);
                    if ( dirSeparatorIndex >= 0 )
                    {
                        if ( searchOption == SearchOption.TopDirectoryOnly )
                        {
                            continue;
                        }

                        fileName = filePath.Substring(dirSeparatorIndex + 1);
                    }
                    else
                    {
                        fileName = filePath.Substring(actualDirPath.Length);
                    }

                    if ( filter == null || filter(fileName) )
                    {
                        Debug.Assert(filePath[0] == '/');
                        results.Add(filePath.Substring(1));
                    }
                }

                return results.ToArray();
            }

            public static byte[] ReadAllBytes(string path)
            {
                ReadInfo info;
                if ( !TryGetInfo(path, out info) )
                {
                    ThrowFileNotFound(path);
                }

                byte[] buffer;
                using ( var fileStream = File.OpenRead(info.readPath) )
                {
                    if ( info.offset != 0 )
                    {
                        if ( fileStream.Seek(info.offset, SeekOrigin.Begin) != info.offset )
                        {
                            throw new IOException();
                        }
                    }

                    if ( info.size > (long)int.MaxValue )
                    {
                        throw new IOException();
                    }

                    int count = (int)info.size;
                    int offset = 0;

                    buffer = new byte[count];
                    while ( count > 0 )
                    {
                        int num = fileStream.Read(buffer, offset, count);
                        if ( num == 0 )
                        {
                            throw new EndOfStreamException();
                        }
                        offset += num;
                        count -= num;
                    }
                }

                return buffer;
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
                    return new SubReadOnlyStream(fileStream, info.offset, info.size, leaveOpen : false);
                }
                catch ( System.Exception )
                {
                    fileStream.Dispose();
                    throw;
                }
            }

            private static int GetDirectoryIndex(string path)
            {
                Debug.Assert(s_paths != null);

                var index = Array.BinarySearch(s_paths, path, StringComparer.OrdinalIgnoreCase);
                if ( index >= 0 )
                {
                    return ~index;
                }

                index = ~index;
                if ( index == s_paths.Length )
                {
                    return index;
                }

                for ( int i = index; i < s_paths.Length && s_paths[i].StartsWith(path); ++i )
                {
                    Debug.Assert(s_paths[i].Length > path.Length);

                    if ( path[path.Length - 1] == '/' )
                    {
                        return i;
                    }

                    if ( s_paths[i][path.Length] == '/' )
                    {
                        return i;
                    }
                }

                return s_paths.Length;
            }

            private static void GetStreamingAssetsFromPatch(string dataPath, List<string> paths, List<PartInfo> parts)
            {
                string assetsPath = dataPath + "/assets";
                foreach (var dir in Directory.GetDirectories(assetsPath))
                {
                    if (dir.EndsWith("/bin"))
                    {
                        continue;
                    }

                    foreach (var file in Directory.GetFiles(dir, "*", SearchOption.AllDirectories))
                    {
                        var relativePath = file.Substring(assetsPath.Length);
                        var entry = new PartInfo()
                        {
                            crc32 = 0,
                            offset = -1,
                            size = new FileInfo(file).Length
                        };

                        var index = paths.BinarySearch(relativePath, StringComparer.OrdinalIgnoreCase);
                        if ( index >= 0 )
                        {
                            throw new System.InvalidOperationException("Paths duplicate! " + file);
                        }

                        paths.Insert(~index, relativePath);
                        parts.Insert(~index, entry);
                    }
                }
            }

            private static void GetStreamingAssetsInfoFromJar(string apkPath, List<string> paths, List<PartInfo> parts)
            {
                using ( var stream = File.OpenRead(apkPath) )
                using ( var reader = new BinaryReader(stream) )
                {
                    if ( !stream.CanRead )
                    {
                        throw new ArgumentException();
                    }
                    if ( !stream.CanSeek )
                    {
                        throw new ArgumentException();
                    }

                    long expectedNumberOfEntries;
                    long centralDirectoryStart;
                    ZipArchiveUtils.ReadEndOfCentralDirectory(stream, reader, out expectedNumberOfEntries, out centralDirectoryStart);

                    try
                    {
                        stream.Seek(centralDirectoryStart, SeekOrigin.Begin);

                        long numberOfEntries = 0;

                        ZipCentralDirectoryFileHeader header;

                        const int prefixLength = 7;
                        const string prefix = "assets/";
                        const string assetsPrefix = "assets/bin/";
                        Debug.Assert(prefixLength == prefix.Length);

                        while ( ZipCentralDirectoryFileHeader.TryReadBlock(reader, out header) )
                        {
                            if ( header.CompressedSize != header.UncompressedSize )
                            {
#if UNITY_ASSERTIONS
                                var fileName = Encoding.UTF8.GetString(header.Filename);
                                if (fileName.StartsWith(prefix) && !fileName.StartsWith(assetsPrefix))
                                {
                                    bool isStreamingAsset = true;
                                    AndroidIsCompressedFileStreamingAsset(fileName, ref isStreamingAsset);
                                    if (!isStreamingAsset)
                                    {
                                        // partial method ignored it
                                    }
                                    else if (CompressedStreamingAssetFound != null && CompressedStreamingAssetFound(fileName))
                                    {
                                        // handler ignored it
                                    }
                                    else
                                    {
                                        Debug.LogAssertionFormat("BlankReadAssets: file {0} is where Streaming Assets are put, but is compressed. " +
                                            "If this is a App Bundle build, see README.md for a possible workaround. " +
                                            "If this file is not a Streaming Asset (has been on purpose by hand or by another plug-in), handle " +
                                            "CompressedStreamingAssetFound event or implement " +
                                            "AndroidIsCompressedFileStreamingAsset partial method to prevent " +
                                            "this message from appearing again. ", fileName);
                                    }
                                }
#endif
                            }
                            else
                            {
                                var fileName = Encoding.UTF8.GetString(header.Filename);

                                if (fileName.EndsWith("/"))
                                {
                                    Debug.Assert(header.UncompressedSize == 0);
                                }
                                else if ( fileName.StartsWith(prefix) )
                                {
                                    if ( fileName.StartsWith(assetsPrefix) )
                                    {
                                        // ignore bin directory
                                    }
                                    else
                                    {
                                        var relativePath = fileName.Substring(prefixLength - 1);
                                        var entry = new PartInfo()
                                        {
                                            crc32 = header.Crc32,
                                            offset = header.RelativeOffsetOfLocalHeader,
                                            size = header.UncompressedSize
                                        };

                                        var index = paths.BinarySearch(relativePath, StringComparer.OrdinalIgnoreCase);
                                        if ( index >= 0 )
                                        {
                                            throw new System.InvalidOperationException("Paths duplicate! " + fileName);
                                        }

                                        paths.Insert(~index, relativePath);
                                        parts.Insert(~index, entry);
                                    }
                                }
                            }

                            numberOfEntries++;
                        }

                        if ( numberOfEntries != expectedNumberOfEntries )
                        {
                            throw new ZipArchiveException("Number of entries does not match");
                        }

                    }
                    catch ( EndOfStreamException ex )
                    {
                        throw new ZipArchiveException("CentralDirectoryInvalid", ex);
                    }

                    for ( int i = 0; i < parts.Count; ++i )
                    {
                        var entry = parts[i];
                        stream.Seek(entry.offset, SeekOrigin.Begin);

                        if ( !ZipLocalFileHeader.TrySkipBlock(reader) )
                        {
                            throw new ZipArchiveException("Local file header corrupt");
                        }

                        entry.offset = stream.Position;

                        parts[i] = entry;
                    }
                }
            }
        }
    }
}

#endif
