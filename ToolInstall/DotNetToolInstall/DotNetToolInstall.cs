using System.Diagnostics;

namespace ZipDependencyIncludeInVsixTask
{
    public class DotNetToolInstall : IDotNetToolInstall
    {
        public DotNetToolInstallResult InstallToolPath(string packageId, string toolPath, string version = null)
        {
            //todo this should go in Execute
            var versionArgument = version == null ? "" : $"--version {version}";
            var toolPathArgument = $@"--tool-path ""{toolPath}""";
            return Execute(packageId, $"{toolPathArgument} {versionArgument}");
        }

        private DotNetToolInstallResult Execute(string packageId, string additionalArguments, string workingDirectory = null)
        {
            var processStartInfo = new ProcessStartInfo
            {
                FileName = "dotnet",
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                WindowStyle = ProcessWindowStyle.Hidden,
                WorkingDirectory = workingDirectory,
                Arguments = $"tool install {packageId} {additionalArguments}",
            };

            if (workingDirectory != null)
            {
                processStartInfo.WorkingDirectory = workingDirectory;
            }

            var process = Process.Start(processStartInfo);

            process.WaitForExit();

            return new DotNetToolInstallResult
            {
                Output = process.GetOutput(),
                ExitCode = process.ExitCode
            };

        }
    }
}
