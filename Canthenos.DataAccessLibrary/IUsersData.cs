using Canthenos.DataAccessLibrary.Models;

namespace Canthenos.DataAccessLibrary;

public interface IUsersData
{
    Task<List<UsersModel>> GetUsers(int roleId = 0);
    Task<bool> UserExists(string username);
    Task<(string? salt, string? hash)> UserPassword(string username);
    Task<(bool success, string value)> CreateSessionToken(string username, string userAgent);
    Task<bool> GetSessionToken(string token);
    Task RevokeSessionToken(string token);
    Task<bool> CheckSessionToken(string token);
}