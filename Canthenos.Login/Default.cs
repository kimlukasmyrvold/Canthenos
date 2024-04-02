namespace Canthenos.Login;

public class Default
{
    public static int SaltSize { get; set; } = 32;
    public static int StretchInterval { get; set; } = 1_000_000;
}