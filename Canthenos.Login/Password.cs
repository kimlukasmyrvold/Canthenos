using Canthenos.DataAccessLibrary;
using Microsoft.JSInterop;

namespace Canthenos.Login;

public class Password : IPassword
{
    private readonly IUsersData _usersData;
    private readonly IJSRuntime _jsRuntime;

    public Password(IUsersData usersData, IJSRuntime jsRuntime)
    {
        _usersData = usersData;
        _jsRuntime = jsRuntime;
    }

    public async Task<(bool loggedIn, int? roleId)> LoggedIn()
    {
        try
        {
            var cookie = await ReadCookie("session-token");
            if (cookie == "") return (false, null);

            var role = int.Parse(cookie.Split(".")[0]);
            var validToken = await _usersData.CheckSessionToken(cookie);

            return (validToken, role);
        }
        catch
        {
            return (false, null);
        }
    }

    public async Task<(bool success, string? errorMessage, int? roleId)> Login(string username, string password,
        string? existingToken = null)
    {
        if (password.Contains(' ')) return (false, "Password cannot include spaces", null);

        if (await _usersData.UserExists(username) is false)
        {
            Console.WriteLine("Username does not exists.");
            return (false, null, null);
        }

        var (salt, hash) = await _usersData.UserPassword(username);
        if (salt is null)
        {
            Console.WriteLine($"Unable to fetch salt for {username}");
            return (false, null, null);
        }

        if (hash is null)
        {
            Console.WriteLine($"Unable to fetch hash for {username}");
            return (false, null, null);
        }

        var correctPassword = CheckPassword(password, salt, hash);
        if (!correctPassword)
        {
            Console.WriteLine("Wrong password!");
            return (false, null, null);
        }

        var userAgent = await _jsRuntime.InvokeAsync<string>("getUserAgent");
        var (tokenSuccess, token) = await _usersData.CreateSessionToken(username, userAgent);
        if (!tokenSuccess)
        {
            return (false, token, null);
        }

        await WriteCookie("session-token", token, DateTime.UtcNow.AddMinutes(30));
        var roleId = int.Parse(token.Split(".")[0]);

        Console.WriteLine($"Salt: {salt}");
        Console.WriteLine($"Hash: {hash}");
        Console.WriteLine(
            $"Username: \"{username}\" => {await _usersData.UserExists(username)}, Password: \"{password}\" => {correctPassword}");
        Console.WriteLine("Correct login credentials");
        Console.WriteLine($"Token: {token}");

        return (correctPassword, null, roleId);
    }


    private async Task WriteCookie(string name, string value, DateTime expires)
    {
        await _jsRuntime.InvokeAsync<object>("WriteCookie.WriteCookie", name, value, expires);
    }

    private async Task<string> ReadCookie(string name)
    {
        return await _jsRuntime.InvokeAsync<string>("ReadCookie.ReadCookie", name);
    }


    private static readonly int StretchInterval = Default.StretchInterval;

    public bool CheckPassword(string password, string salt, string hash, int stretchInterval = 0)
    {
        var bytedSalt = Salting.ToByte(salt);
        if (stretchInterval == 0) stretchInterval = StretchInterval;
        var newHash = Hashing.KeyStretch(password, bytedSalt, stretchInterval);
        return newHash == hash;
    }

    public (byte[], string) NewPassword(string password, int saltSize, int stretchInterval = 0)
    {
        if (stretchInterval == 0) stretchInterval = StretchInterval;
        var salt = Salting.GenerateSalt(saltSize);
        var hash = Hashing.KeyStretch(password, salt, stretchInterval);

        return (salt, hash);
    }
}