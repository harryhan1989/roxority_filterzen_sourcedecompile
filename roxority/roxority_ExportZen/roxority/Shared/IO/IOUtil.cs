namespace roxority.Shared.IO
{
    using roxority.Shared;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Threading;

    internal static class IOUtil
    {
        internal const int DEFAULT_BUFFERSIZE = 0x1000;

        internal static void CopyFiles(string containingDirectoryPath, string targetDirectoryPath, string filePattern, string subFilePattern, string directoryPattern, string subDirectoryPattern)
        {
            DirectoryInfo info = new DirectoryInfo(containingDirectoryPath);
            Action<FileInfo> action = delegate (FileInfo item) {
                try
                {
                    item.CopyTo(Path.Combine(targetDirectoryPath, Path.GetFileName(item.FullName)), true);
                }
                catch
                {
                }
            };
            if (!Directory.Exists(targetDirectoryPath))
            {
                CreateDirectory(new DirectoryInfo(targetDirectoryPath));
            }
            foreach (FileInfo info2 in info.GetFiles(filePattern, SearchOption.TopDirectoryOnly))
            {
                action(info2);
            }
            if (!string.IsNullOrEmpty(directoryPattern))
            {
                foreach (DirectoryInfo info3 in info.GetDirectories(directoryPattern, SearchOption.TopDirectoryOnly))
                {
                    CopyFiles(info3.FullName, Path.Combine(targetDirectoryPath, info3.Name), subFilePattern, subFilePattern, subDirectoryPattern, subDirectoryPattern);
                }
            }
        }

        internal static bool CopyStream(Stream source, Stream target)
        {
            return CopyStream(source, target, null);
        }

        internal static bool CopyStream(Stream source, Stream target, Operation<int, bool> shouldCancel)
        {
            SharedUtil.ThrowIfEmpty(source, "source");
            return CopyStream(source, target, 0, 0L, SeekOrigin.Current, source.Length, 0L, SeekOrigin.Begin, shouldCancel);
        }

        internal static bool CopyStream(Stream source, Stream target, int bufferSize, long sourceSeekOffset, SeekOrigin sourceSeekOrigin, long targetSetLength, long targetSeekOffset, SeekOrigin targetSeekOrigin, Operation<int, bool> shouldCancel)
        {
            SharedUtil.ThrowIfEmpty(source, "source");
            SharedUtil.ThrowIfEmpty(target, "target");
            if (!source.CanRead || !target.CanWrite)
            {
                throw new IOException();
            }
            byte[] buffer = new byte[bufferSize = (bufferSize <= 0) ? 0x1000 : bufferSize];
            if (((sourceSeekOffset > 0L) || ((sourceSeekOffset == 0L) && (sourceSeekOrigin != SeekOrigin.Current))) && source.CanSeek)
            {
                source.Seek(sourceSeekOffset, sourceSeekOrigin);
            }
            if (target.CanSeek)
            {
                if (targetSetLength > 0L)
                {
                    target.SetLength(targetSetLength);
                }
                target.Seek(targetSeekOffset, targetSeekOrigin);
            }
            while (source.Position < source.Length)
            {
                long num;
                if ((num = source.Length - source.Position) < bufferSize)
                {
                    bufferSize = (int) num;
                    buffer = new byte[bufferSize];
                }
                source.Read(buffer, 0, bufferSize);
                target.Write(buffer, 0, bufferSize);
                target.Flush();
                if ((shouldCancel != null) && shouldCancel(SharedUtil.Percent(source.Position, source.Length)))
                {
                    return false;
                }
            }
            return true;
        }

        internal static DirectoryInfo CreateDirectory(DirectoryInfo dir)
        {
            if (!dir.Exists)
            {
                if (dir.Parent != null)
                {
                    CreateDirectory(dir.Parent);
                }
                Directory.CreateDirectory(dir.FullName);
            }
            return dir;
        }

        internal static string CreateDirectory(string path)
        {
            return CreateDirectory(new DirectoryInfo(path)).FullName;
        }

        internal static void DeleteFiles(string dirPath, SearchOption searchOption, int loops, params string[] filters)
        {
            string[] strArray = null;
            if (!string.IsNullOrEmpty(dirPath) && Directory.Exists(dirPath))
            {
                for (int i = 0; i < loops; i++)
                {
                    foreach (string str in filters)
                    {
                        if (!string.IsNullOrEmpty(str))
                        {
                            try
                            {
                                strArray = Directory.GetFiles(dirPath, str, searchOption);
                            }
                            catch
                            {
                                strArray = null;
                            }
                        }
                        if (strArray != null)
                        {
                            foreach (string str2 in strArray)
                            {
                                TryDeleteFile(str2, (i == 0) ? 0 : 1);
                            }
                        }
                    }
                }
            }
        }

        internal static string FormatFileSize(Duo<double, FileSize> fileSize)
        {
            return string.Format("{0} {1}", fileSize.Value1.Equals((double) ((long) fileSize.Value1)) ? ((long) fileSize.Value1).ToString() : fileSize.Value1.ToString("N2"), fileSize.Value2);
        }

        internal static string FormatFileSize(long fileLength)
        {
            return FormatFileSize(GetFileSize(fileLength));
        }

        internal static IEnumerable<string> GetAllFiles(string dirPath, Predicate<string> includeFile, Predicate<string> includeDirectory)
        {
            foreach (string iteratorVariable1 in Directory.GetFiles(dirPath))
            {
                if ((includeFile == null) || includeFile(iteratorVariable1))
                {
                    yield return iteratorVariable1;
                }
            }
            foreach (string iteratorVariable2 in Directory.GetDirectories(dirPath))
            {
                IEnumerable<string> iteratorVariable0;
                if (((includeDirectory == null) || includeDirectory(iteratorVariable2)) && ((iteratorVariable0 = GetAllFiles(iteratorVariable2, includeFile, includeDirectory)) != null))
                {
                    foreach (string iteratorVariable3 in iteratorVariable0)
                    {
                        yield return iteratorVariable3;
                    }
                }
            }
        }

        internal static ulong GetDirectorySize(string dirPath, ulong testAddSize, ulong testMaxSize)
        {
            ulong num = 0L;
            string[] files = new string[0];
            try
            {
                files = Directory.GetFiles(dirPath);
            }
            catch
            {
            }
            foreach (string str in files)
            {
                try
                {
                    using (Stream stream = File.OpenRead(str))
                    {
                        num += (ulong) stream.Length;
                    }
                }
                catch
                {
                }
                if ((num + testAddSize) >= testMaxSize)
                {
                    return num;
                }
            }
            try
            {
                files = new string[0];
                files = Directory.GetDirectories(dirPath);
            }
            catch
            {
            }
            foreach (string str2 in files)
            {
                if ((num += GetDirectorySize(str2, testAddSize + num, testMaxSize)) >= testMaxSize)
                {
                    return num;
                }
            }
            return num;
        }

        internal static Duo<double, FileSize> GetFileSize(long fileLength)
        {
            long num = 0x400L;
            long num2 = 0x100000L;
            long num3 = 0x40000000L;
            long num4 = 0x10000000000L;
            if (fileLength < num)
            {
                return new Duo<double, FileSize>((double) fileLength, FileSize.B);
            }
            if (fileLength < num2)
            {
                return new Duo<double, FileSize>(((double) fileLength) / ((double) num), FileSize.KB);
            }
            if (fileLength < num3)
            {
                return new Duo<double, FileSize>(((double) fileLength) / ((double) num2), FileSize.MB);
            }
            if (fileLength < num4)
            {
                return new Duo<double, FileSize>(((double) fileLength) / ((double) num3), FileSize.GB);
            }
            return new Duo<double, FileSize>(((double) fileLength) / ((double) num4), FileSize.TB);
        }

        internal static string NormalizePath(string path)
        {
            path = (SharedUtil.IsEmpty(path) || (path == ".")) ? Environment.CurrentDirectory.Trim().ToLower() : (path = path.Trim().ToLower());
            if (path.StartsWith("file:"))
            {
                path = PathFromUri(path);
            }
            while ((path.Length > 0) && path.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                path = path.Substring(0, path.Length - 1);
            }
            return path;
        }

        internal static bool PathEquals(string one, string two)
        {
            if ((one != null) || (two != null))
            {
                if ((one == null) || (two == null))
                {
                    return false;
                }
                if (!object.ReferenceEquals(one, two) && !one.Equals(two))
                {
                    return one.Trim().Equals(two.Trim(), StringComparison.InvariantCultureIgnoreCase);
                }
            }
            return true;
        }

        internal static string PathFromUri(string uri)
        {
            return uri.Replace("file:///", string.Empty).Replace("file://", string.Empty).Replace('/', '\\');
        }

        internal static void TryDeleteFile(string path)
        {
            TryDeleteFile(path, 0);
        }

        internal static void TryDeleteFile(string path, int sleep)
        {
            try
            {
                File.Delete(path);
            }
            catch
            {
            }
            finally
            {
                if (sleep > 0)
                {
                    Thread.Sleep(sleep);
                }
            }
        }

    }
}

