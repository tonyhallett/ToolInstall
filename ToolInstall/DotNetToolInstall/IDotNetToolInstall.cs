namespace ZipDependencyIncludeInVsixTask
{
    public class DotNetToolInstallResult
    {
        public int ExitCode { get; set; }
        public string Output { get; set; }
    }

    public interface IDotNetToolInstall
    {
        DotNetToolInstallResult InstallToolPath(string packageId, string toolPath, string version = null);
    }
}