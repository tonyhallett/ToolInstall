using System;
using System.IO;

namespace ZipDependencyIncludeInVsixTask
{
    internal interface IZipHelper
    {
        void CreateZipFromTempDirectory(string zipPath, Action<DirectoryInfo> tempDirectoryCallback);
    }
}