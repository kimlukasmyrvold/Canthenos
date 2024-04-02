using System.Diagnostics;

namespace Canthenos.Login;

public class Tests : ITests
{
    private readonly IPassword _password;

    public Tests(IPassword password)
    {
        _password = password;
    }
    
    public void MainTest()
    {
        const string password = "Password1234";
        var salt = Salting.GenerateSalt(32);
        byte[] savedSalt =
        {
            141, 43, 22, 206, 89, 202, 185, 21, 122, 30, 244, 135, 225, 4, 43, 53, 166, 242, 143, 49, 151, 136, 96, 166,
            90, 192, 102, 197, 67, 143, 108, 38
        };

        Console.WriteLine($"\nPassword:             {password}");
        Console.WriteLine($"Salt:                 {string.Join(", ", salt)}");
        Console.WriteLine($"Saved Salt:           {string.Join(", ", savedSalt)}\n");

        Console.WriteLine($"Hashed no-salt:       {Hashing.GenerateHash(password)}");
        Console.WriteLine($"Hashed:               {Hashing.HashPassword(password, salt)}");
        Console.WriteLine($"Hashed Saved:         {Hashing.HashPassword(password, savedSalt)}\n");

        var timer = new Stopwatch();

        timer.Start();
        Console.WriteLine($"Stretched:            {Hashing.KeyStretch(password, salt, Default.StretchInterval)}");
        timer.Stop();
        Console.WriteLine($"Took: {timer.ElapsedMilliseconds}ms\n");

        timer.Restart();
        Console.WriteLine($"Stretched saved:      {Hashing.KeyStretch(password, savedSalt, Default.StretchInterval)}");
        timer.Stop();
        Console.WriteLine($"Took: {timer.ElapsedMilliseconds}ms\n");
    }

    public void NewPassword()
    {
        const string password = "admin";
        var (salt, hash) = _password.NewPassword(password, Default.SaltSize, Default.StretchInterval);
        var salt64 = Salting.To64(salt);
        Console.WriteLine($"Salt: {salt64}, Hash: {hash}");
    }

    public void CheckPassword()
    {
        const string password = "admin";
        const string salt = "yC/ObWiifytcDrGTHJzb93pundmZzUVyIgt2sNznDEU=";
        const string hash = "LtKXyC6TiOlw/B4q5vQnXAgdwK5e9v3Sasb/yL026E3EVkR97m4CXjsTujYYty76xvDb8u5e9KkwE6xQyFmI+g==";
        
        Console.WriteLine(_password.CheckPassword(password, salt, hash, Default.StretchInterval));
    }
}