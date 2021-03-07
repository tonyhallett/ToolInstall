using System;

namespace ZipDependencyIncludeInVsixTask
{
    public class DotNetToolInstallAndZip : IDotNetToolInstallAndZip
    {
        private readonly IZipHelper zipHelper;
        private readonly IDotNetToolInstall dotNetToolInstall;

        internal DotNetToolInstallAndZip(IZipHelper zipHelper, IDotNetToolInstall dotNetToolInstall)
        {
            this.zipHelper = zipHelper;
            this.dotNetToolInstall = dotNetToolInstall;
        }

        public DotNetToolInstallAndZip() : this(new ZipHelper(), new DotNetToolInstall()) { }


        public void Zip(string zipPath, string packageId, string version = null)
        {
            zipHelper.CreateZipFromTempDirectory(zipPath, tempDirectory =>
            {
                var installResult = dotNetToolInstall.InstallToolPath(packageId, tempDirectory.FullName, version);
                if (installResult.ExitCode != 0)
                {
                    throw new Exception(installResult.Output);
                }
            });
        }
    }
}
