namespace Canthenos.DataAccessLibrary;

public interface ICookies
{
    Task WriteCookie(string name, string value, DateTime expires);
    Task<string> ReadCookie(string name);
}