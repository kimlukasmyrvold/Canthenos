using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Canthenos.Web.Models;

public class LoginModel
{
    [Required]
    [MinLength(1, ErrorMessage = "Username is too short.")]
    public string? Username { get; set; }

    [Required]
    [PasswordPropertyText]
    [MinLength(1, ErrorMessage = "Password is too short.")]
    public string? Password { get; set; }
}