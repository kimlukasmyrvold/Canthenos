namespace Canthenos.DataAccessLibrary;

public interface IGithubData
{
    Task<(string, string)> GetLatestVersion();
}