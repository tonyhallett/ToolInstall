using System;
using System.IO;
using System.IO.Compression;

namespace ZipDependencyIncludeInVsixTask
{
    internal class ZipHelper : IZipHelper
    {
        public void CreateZipFromTempDirectory(string zipPath, Action<DirectoryInfo> tempDirectoryCallback)
        {
            var tempDirectory = DirectoryHelper.CreateTemporaryDirectory();

            tempDirectoryCallback(tempDirectory);
            ZipFile.CreateFromDirectory(tempDirectory.FullName, zipPath);
            try
            {
                tempDirectory.Delete(true);
            }
            catch { }

        }
    }
}
