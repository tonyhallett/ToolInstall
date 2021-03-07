using System.Collections.Generic;
using System.Threading.Tasks;
using NuGet.Versioning;

namespace ZipDependencyIncludeInVsixTask
{
    public interface INugetClientHelper
    {
        Task<bool> DownloadLatestNuPkg(string packageId, string nuPkgPath, bool includePreRelease = false);
        Task<bool> DownloadNuPkg(string packageId, NuGetVersion nuGetVersion, string nuPkgPath);
        Task<NuGetVersion> GetLatestVersion(string packageId, bool includePreRelease = false);
        Task<IEnumerable<NuGetVersion>> GetVersions(string packageId, bool includePreRelease = false);
    }
}