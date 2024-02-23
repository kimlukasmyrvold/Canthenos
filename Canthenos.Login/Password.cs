namespace Canthenos.Login;

public abstract class Password
{
    public static bool CheckPassword(string password, byte[] salt, string hash, int stretchInterval)
    {
        var newHash = Hashing.KeyStretch(password, salt, stretchInterval);
        return newHash == hash;
    }

    public static (byte[], string) NewPassword(string password, int saltSize, int stretchInterval)
    {
        var salt = Salting.GenerateSalt(saltSize);
        var hash = Hashing.KeyStretch(password, salt, stretchInterval);

        return (salt, hash);
    }
}