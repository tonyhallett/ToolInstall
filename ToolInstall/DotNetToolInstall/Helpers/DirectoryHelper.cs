using System.IO;

namespace ZipDependencyIncludeInVsixTask
{
    public static class DirectoryHelper
    {
        public static DirectoryInfo CreateTemporaryDirectory()
        {
            string tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            return Directory.CreateDirectory(tempDirectory);

        }
    }
}
