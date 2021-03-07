using System.IO;

namespace ToolInstall
{
    internal static class DirectoryHelper
    {
        public static DirectoryInfo CreateTemporaryDirectory()
        {
            string tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            return Directory.CreateDirectory(tempDirectory);

        }
    }
}
