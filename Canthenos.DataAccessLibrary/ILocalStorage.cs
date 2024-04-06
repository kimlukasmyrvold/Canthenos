namespace Canthenos.DataAccessLibrary;

public interface ILocalStorage
{
    Task<string> Get(string key);
    Task Set(string key, string value);
    Task Remove(string key);
    Task Clear();
}