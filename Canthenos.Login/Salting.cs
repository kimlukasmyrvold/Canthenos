using System.Security.Cryptography;

namespace Canthenos.Login;

public abstract class Salting
{
    public static byte[] ToByte(string databaseSalt)
    {
        return Convert.FromBase64String(databaseSalt);
    }

    public static string To64(byte[] salt)
    {
        return Convert.ToBase64String(salt);
    }

    public static byte[] GenerateSalt(int saltSize)
    {
        var salt = new byte[saltSize];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(salt);

        return salt;
    }
}