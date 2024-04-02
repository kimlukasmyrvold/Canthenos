namespace Canthenos.DataAccessLibrary.Models;

public class UsersModel
{
    public int UserId { get; set; }
    public int RoleId { get; set; }
    public string? RoleName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Username { get; set; }
    public string? Salt { get; set; }
    public string? Hash { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public string FormattedCreatedAt => CreatedAt.ToString("yyyy-MM-dd HH:mm:ss");
    public string FormattedUpdatedAt => UpdatedAt.ToString("yyyy-MM-dd HH:mm:ss");
}