namespace ToolInstall
{
    public interface IDotNetToolInstallAndZip
    {
        void Zip(string zipPath, string packageId, string version = null);
    }
}