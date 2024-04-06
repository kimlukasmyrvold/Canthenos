namespace Canthenos.DataAccessLibrary;

public interface IGithubData
{
    Task<(string url, string version)> GetLatestVersion();
}