using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NuGet.Common;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;
using NuGet.Versioning;

namespace ToolInstall
{
    public class NugetClientHelper : INugetClientHelper
    {
        public async Task<IEnumerable<NuGetVersion>> GetVersions(string packageId, bool includePreRelease = false)
        {
            ILogger logger = NullLogger.Instance;
            CancellationToken cancellationToken = CancellationToken.None;

            SourceCacheContext cache = new SourceCacheContext();
            SourceRepository repository = Repository.Factory.GetCoreV3("https://api.nuget.org/v3/index.json");
            FindPackageByIdResource resource = await repository.GetResourceAsync<FindPackageByIdResource>();

            IEnumerable<NuGetVersion> versions = await resource.GetAllVersionsAsync(
                packageId,
                cache,
                logger,
                cancellationToken);

            if (!includePreRelease)
            {
                versions = versions.Where(v => !v.IsPrerelease);
            }

            return versions;
        }
        public async Task<NuGetVersion> GetLatestVersion(string packageId, bool includePreRelease = false)
        {
            IEnumerable<NuGetVersion> versions = await GetVersions(packageId, includePreRelease);

            return versions.Max();
        }

        public async Task<bool> DownloadNuPkg(string packageId, NuGetVersion nuGetVersion, string nuPkgPath)
        {
            ILogger logger = NullLogger.Instance;
            CancellationToken cancellationToken = CancellationToken.None;

            SourceCacheContext cache = new SourceCacheContext();
            SourceRepository repository = Repository.Factory.GetCoreV3("https://api.nuget.org/v3/index.json");
            FindPackageByIdResource resource = await repository.GetResourceAsync<FindPackageByIdResource>();
            var packageDownloader = await resource.GetPackageDownloaderAsync(new NuGet.Packaging.Core.PackageIdentity(packageId, nuGetVersion), cache, logger, cancellationToken);
            var result = await packageDownloader.CopyNupkgFileToAsync(nuPkgPath, cancellationToken);
            return result;
        }

        public async Task<bool> DownloadLatestNuPkg(string packageId, string nuPkgPath, bool includePreRelease = false)
        {
            return await DownloadNuPkg(packageId, await GetLatestVersion(packageId, includePreRelease), nuPkgPath);
        }

    }
}
