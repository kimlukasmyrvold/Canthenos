namespace Canthenos.Login;

public interface IPassword
{
    bool CheckPassword(string password, string salt, string hash, int stretchInterval = 0);
    (byte[], string) NewPassword(string password, int saltSize, int stretchInterval = 0);
    Task<(bool success, string? errorMessage, int? roleId)> Login(string username, string password, string? existingToken = null);
    Task<(bool loggedIn, int? roleId)> LoggedIn();
}