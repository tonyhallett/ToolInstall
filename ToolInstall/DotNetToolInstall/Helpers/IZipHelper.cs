using System;
using System.IO;

namespace ToolInstall
{
    internal interface IZipHelper
    {
        void CreateZipFromTempDirectory(string zipPath, Action<DirectoryInfo> tempDirectoryCallback);
    }
}