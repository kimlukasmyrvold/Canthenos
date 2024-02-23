using System.Security.Cryptography;
using System.Text;

namespace Canthenos.Login;

public abstract class Hashing
{
    public static string GenerateHash(string data)
    {
        var hashedBytes = SHA512.HashData(Encoding.UTF8.GetBytes(data));
        return Convert.ToBase64String(hashedBytes);
    }

    public static string HashPassword(string password, byte[] salt)
    {
        var combinedBytes = new byte[salt.Length + Encoding.UTF8.GetBytes(password).Length];
        Buffer.BlockCopy(salt, 0, combinedBytes, 0, salt.Length);
        Buffer.BlockCopy(Encoding.UTF8.GetBytes(password), 0, combinedBytes, salt.Length, Encoding.UTF8.GetBytes(password).Length);

        return GenerateHash(Convert.ToBase64String(combinedBytes));
    }

    public static string KeyStretch(string password, byte[] salt, int amount)
    {
        var hashedPassword = password;

        for (var i = 0; i < amount; i++)
        {
            hashedPassword = HashPassword(hashedPassword, salt);
        }

        return hashedPassword;
    }
}