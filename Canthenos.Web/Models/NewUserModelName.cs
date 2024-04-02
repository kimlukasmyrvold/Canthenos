using System.ComponentModel.DataAnnotations;

namespace Canthenos.Web.Models;

public class NewUserModelName
{
    [Required(ErrorMessage = "First name is required.")]
    [MinLength(1, ErrorMessage = "First name is too short.")]
    public string? FirstName { get; set; }

    [Required(ErrorMessage = "Last name is required.")]
    [MinLength(1, ErrorMessage = "Last name is too short.")]
    public string? LastName { get; set; }
}