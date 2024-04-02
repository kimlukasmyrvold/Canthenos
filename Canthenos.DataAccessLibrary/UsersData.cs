using Canthenos.DataAccessLibrary.Models;

namespace Canthenos.DataAccessLibrary;

public class UsersData : IUsersData
{
    private readonly ISqlDataAccess _db;

    public UsersData(ISqlDataAccess db)
    {
        _db = db;
    }

    public Task<List<UsersModel>> GetUsers(int roleId = 0)
    {
        var sql = roleId == 0 ? "select * from ViewUsers;" : "select * from ViewUsers where RoleId=@RoleId;";

        return _db.LoadData<UsersModel, dynamic>(sql, new { RoleId = roleId });
    }

    public async Task<bool> UserExists(string username)
    {
        const string sql = "select count(*) from ViewUsers where Username=@Username;";
        var parameters = new { Username = username };
        var count = (await _db.LoadData<int, dynamic>(sql, parameters)).SingleOrDefault();

        return count > 0;
    }

    public async Task<(string? salt, string? hash)> UserPassword(string username)
    {
        const string sql = "select Salt, Hash from ViewUsers where Username=@Username;";
        var parameters = new { Username = username };
        return (await _db.LoadData<(string, string), dynamic>(sql, parameters)).FirstOrDefault();
    }


    public async Task<(bool success, string value)> CreateSessionToken(string username, string userAgent)
    {
        var userId = await GetUserId(username);
        var roleId = await GetUserRole(userId);
        string token;

        var attempts = 1;
        bool tokenExists;
        do
        {
            token = $"{roleId}.{Guid.NewGuid():N}";
            tokenExists = await GetSessionToken(token);

            if (++attempts > 5) return (false, "Something went wrong, try again later");
        } while (tokenExists);


        const string sql = "insert into Sessions (UserId, UserAgent, Token) values (@UserId, @UserAgent, @Token);";
        var parameters = new { UserId = userId, UserAgent = userAgent, Token = token };
        await _db.SaveData(sql, parameters);

        return (true, token);
    }

    public async Task<bool> GetSessionToken(string token)
    {
        const string sql = "select count(*) from Sessions where Token=@Token;";
        var count = (await _db.LoadData<int, dynamic>(sql, new { Token = token })).SingleOrDefault();
        return count > 0;
    }

    public async Task<bool> CheckSessionToken(string token)
    {
        var exists = await GetSessionToken(token);
        if (!exists) return false;

        var sql = "select ExpiresAt from Sessions where Token=@Token;";
        var expires = (await _db.LoadData<DateTime, dynamic>(sql, new { Token = token })).SingleOrDefault();
        if (DateTime.UtcNow > expires) return false;

        sql = "select Revoked from Sessions where Token=@Token;";
        var revoked = (await _db.LoadData<int, dynamic>(sql, new { Token = token })).SingleOrDefault();
        return revoked != 1;
    }

    public async Task RevokeSessionToken(string token)
    {
        const string sql = "update Sessions set Sessions.Revoked = 1 where Token=@Token;";
        await _db.SaveData(sql, new { Token = token });
    }


    private async Task<int> GetUserId(string username)
    {
        const string sql = "select UserId from ViewUsers where Username=@Username;";
        var parameters = new { Username = username };
        return (await _db.LoadData<int, dynamic>(sql, parameters)).FirstOrDefault();
    }

    private async Task<int> GetUserRole(int userId)
    {
        const string sql = "select RoleId from ViewUsers where UserId=@UserId;";
        var parameters = new { UserId = userId };
        return (await _db.LoadData<int, dynamic>(sql, parameters)).FirstOrDefault();
    }
}